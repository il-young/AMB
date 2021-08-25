using System;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;

// nesslab reader api namespace를 추가합니다.
using nesslab.reader.api;
using System.Threading;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;

namespace AMC_Test
{
    public partial class RFID_Main : Form
    {
        /// <summary>
        ///  리더객체
        /// </summary>
        private Reader reader;
        /// <summary>
        ///  동작모드
        /// </summary>
        private enum Action
        {
            Start,
            Stop
        }

        public RFID_Main()
        {
            InitializeComponent();

            #region 버전표시
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            Version ver = assembly.GetName().Version;

            //TimeSpan newTimeSpan = new TimeSpan(TimeSpan.TicksPerDay * ver.Build + TimeSpan.TicksPerSecond * 2 * ver.Revision);
            //DateTime verdate = new DateTime(2000, 1, 1).Add(newTimeSpan);

            //this.Text = "Reader Manager V" + ver.Major.ToString() + "." + verdate.ToString("M.d");
            this.Text = "Reader Manager V" + ver.Major.ToString() + "." + ver.Minor.ToString() + "." + ver.Build.ToString(); 
            #endregion
        }

        private void Main_Load(object sender, EventArgs e)
        {
            

            this.cmbBank.SelectedIndex = 1; // EPC BANK 선택
            this.numLocation.Value = 2;
            this.numLength.Value = 6;

            string[] permissions = new string[] { "ACCESSIBLE", "ALWAYS ACCESSIBLE", "SECURED ACCESSIBLE", "ALWAYS NOT ACCESSIBLE", "NO CHANGE" };
            foreach (string permission in permissions)
            {
                this.cmbKillPermissions.Items.Add(permission);
                this.cmbAccessPermissions.Items.Add(permission);
                this.cmbEpcPermissions.Items.Add(permission);
                this.cmbTidPermissions.Items.Add(permission);
                this.cmbUserPermissions.Items.Add(permission);
            }

            this.cmbKillPermissions.SelectedIndex
                = this.cmbAccessPermissions.SelectedIndex
                = this.cmbEpcPermissions.SelectedIndex
                = this.cmbTidPermissions.SelectedIndex
                = this.cmbUserPermissions.SelectedIndex = 4; // NO CHANGE 상태로 선택

            for (int i = 1; i <= 20; i++)
            {
                this.cmbSerial.Items.Add("COM" + i.ToString());
            }

            this.cmbConnectType.SelectedIndex = 0;
            this.cmbSerial.SelectedIndex = 0;


            this.tabControl1.SelectedIndex = 1;

            Insert_Run_Log("RFID_FORM_LOAD");

            if (reader == null)
                btnConnect_Click(btnConnect, e);
            else
                if (reader.IsInventorying == false)
                btnStartInventory_Click(btnStartInventory, e);
        }

        /// <summary>
        /// 폼의 OnClosing 이벤트 Override
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosing(CancelEventArgs e)
        {
            
            if (reader != null)
            {
                // 리더 쓰레드가 동작중인가?
                if (reader.IsHandling)
                {
                    this.Hide();
                    e.Cancel = true;
                    // 리더의 연결 해제
                    //reader.Close(CloseType.FormClose);
                }
                // 리더에 연결시도중인가?
                else if (reader.IsConnecting)
                {
                    e.Cancel = true;
                }
                else
                {
                    base.OnClosing(e);
                }
            }
        }

        private void cmbConnectType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox combo = sender as ComboBox;
            if (combo != null)
            {
                if (combo.SelectedIndex == 0)
                {
                    this.txtIpAddress.Enabled = true;
                    this.cmbSerial.Enabled = false;
                }
                else
                {
                    this.txtIpAddress.Enabled = false;
                    this.cmbSerial.Enabled = true;
                }
            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            Button connect = sender as Button;

            Insert_Run_Log("Connect btn click");

            if (connect.Text == "Connect")
            {
                if (this.cmbConnectType.Text == "TCP/IP")
                {
                    string ipaddress = this.txtIpAddress.Text.Trim();

                    if (!CheckIpFormat(ipaddress))
                    {
                        MessageBox.Show("올바른 IP Address 형식이 아닙니다.", "확인", MessageBoxButtons.OK);
                        Insert_Run_Log("올바른 IP Address 형식이 아닙니다.");
                        this.txtIpAddress.Focus();
                        return;
                    }
                }

                // 리더객체를 초기화하고 연결을 시작합니다.
                InitReader();
            }
            else
            {
                if (reader != null)
                    if (reader.IsHandling) // 리더 쓰레드가 동작중인가?
                        reader.Close(CloseType.Close);
            }
        }

        private void txtIpAddress_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.btnConnect_Click(this.btnConnect, EventArgs.Empty);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            // 현재 Form 닫기
            this.Close();
        }

        private void btnConfigurationRead_Click(object sender, EventArgs e)
        {
            // 리더 정보 조회
            GetConfiguration();
        }

        private void btnConfigurationWrite_Click(object sender, EventArgs e)
        {
            // 리더 설정값 변경
            SetConfiguration();
        }

        Stopwatch stopwatch = new Stopwatch();
        bool firstuse = true;

        private void btnStartInventory_Click(object sender, EventArgs e)
        {
            // 인벤토리를 시작합니다.
            Inventory(Action.Start);
            firstuse = true;
            stopwatch.Start();
        }

        private void btnStopInventory_Click(object sender, EventArgs e)
        {
            // 인벤토리를 종료합니다.
            Inventory(Action.Stop);
        }

        private void btnClearInventory_Click(object sender, EventArgs e)
        {
            this.lvwInventory.BeginUpdate();
            this.lvwInventory.Items.Clear();
            this.lvwInventory.EndUpdate();
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            uint bank = (uint)this.cmbBank.SelectedIndex;
            uint location = (uint)this.numLocation.Value;
            uint length = (uint)this.numLength.Value;
            string password = this.txtPassword.Text.Trim();

            // 해당 메모리의 값을 조회합니다.
            GetTagMemory(bank, location, length, password);
        }

        public void Read_RFID()
        {
            uint bank = (uint)this.cmbBank.SelectedIndex;
            uint location = (uint)this.numLocation.Value;
            uint length = (uint)this.numLength.Value;
            string password = this.txtPassword.Text.Trim();

            // 해당 메모리의 값을 조회합니다.
            GetTagMemory(bank, location, length, password);
        }

        private void btnWrite_Click(object sender, EventArgs e)
        {
            uint bank = (uint)this.cmbBank.SelectedIndex;
            uint location = (uint)this.numLocation.Value;
            uint length = (uint)this.numLength.Value;
            string data = this.mtxtTagHex.Text.Trim().ToUpper();
            string password = this.txtPassword.Text.Trim();

            // 해당 메모리에 값을 저장합니다.
            SetTagMemory(bank, location, length, data, password);
        }

        private void btnClearResponse_Click(object sender, EventArgs e)
        {
            this.lstResponse.Items.Clear();
        }

        private void btnLock_Click(object sender, EventArgs e)
        {
            string message = "해당 TAG에 대하여 LOCK을 수행하시겠습니까?";

            if (MessageBox.Show(message, "확인", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                string killpermissions = this.cmbKillPermissions.SelectedIndex.ToString();
                string accesspermissions = this.cmbAccessPermissions.SelectedIndex.ToString();
                string epcpermissions = this.cmbEpcPermissions.SelectedIndex.ToString();
                string tidpermissions = this.cmbTidPermissions.SelectedIndex.ToString();
                string userpermissions = this.cmbUserPermissions.SelectedIndex.ToString();

                string password = this.txtPasswordLockKill.Text.Trim().ToUpper();

                // TAG LOCK을 수행합니다.
                LockTag(killpermissions, accesspermissions, epcpermissions, tidpermissions, userpermissions, password);
            }
        }

        private void btnKill_Click(object sender, EventArgs e)
        {
            string message = "해당 TAG에 대하여 KILL을 수행하시겠습니까?\r\n\r\nKILL된 TAG는 다시는 사용하실 수 없습니다.";

            if (MessageBox.Show(message, "확인", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                string password = this.txtPasswordLockKill.Text.Trim().ToUpper();

                // TAG KILL을 수행합니다.
                KillTag(password);
            }
        }

        private void btnClearResponseLockKill_Click(object sender, EventArgs e)
        {
            this.lstResponseLockKill.Items.Clear();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(tabControl1.SelectedTab.Text != "Inventory")
            {
                if(reader !=null)
                {
                    // 리더 쓰레드가 동작중이며, 인벤토링중이라면
                    if(reader.IsHandling && reader.IsInventorying)
                    {
                        MessageBox.Show("현재 Inventory중입니다. 먼저 STOP 하여 주십시오.", "확인", MessageBoxButtons.OK);
                        return;
                    }
                }
            }
        }

        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (this.tabControl1.TabPages[e.TabPageIndex].Text != "Inventory")
            {
                if (reader != null)
                {
                    // 리더 쓰레드가 동작중이며, 인벤토링중이라면
                    // 인벤토리 작업 중 다른 동작을 수행할 수 없습니다.
                    if (reader.IsHandling && reader.IsInventorying)
                    {
                        MessageBox.Show("현재 Inventory중입니다. 먼저 STOP 하여 주십시오.", "확인", MessageBoxButtons.OK);
                        // TabPage 이동을 취소합니다.
                        e.Cancel = true;
                    }
                }
            }
        }

        /// <summary>
        /// 리더 연결을 시작합니다.
        /// </summary>
        private void InitReader()
        {
            string ipaddress = "";

            if (this.cmbConnectType.Text == "TCP/IP")
            {
                ipaddress = this.txtIpAddress.Text.Trim();

                // 리더객체 TCP 타입으로 생성한다.
                reader = new Reader(ConnectType.Tcp);
                // 사용될 리더의 모델타입을 지정한다.
                reader.ModelType = ModelType.NL_RF1000;
                // 이벤트 핸들러를 등록한다.
                reader.ReaderEvent += new ReaderEventHandler(OnReaderEvent);
                // IP, 관리포트(5578)로 Socket 연결을 시작한다.
                reader.ConnectSocket(ipaddress, 5578);
            }
            else
            {
                reader = new Reader(ConnectType.Serial);
                // 사용될 리더의 모델타입을 지정한다.
                reader.ModelType = ModelType.NL_RF1000;
                // 이벤트 핸들러를 등록한다.
                reader.ReaderEvent += new ReaderEventHandler(OnReaderEvent);
                // 선택한 COM Port를 이용하여 Serial 연결을 시작한다.
                reader.ConnectSerial(this.cmbSerial.Text, 115200);
            }
        }

        int read_cnt = 0;

        /// <summary>
        /// 리더의 이벤트를 처리합니다.
        /// </summary>
        /// <param name="sender">Reader 객체</param>
        /// <param name="e"></param>
        private void OnReaderEvent(object sender, ReaderEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new ReaderEventHandler(OnReaderEvent), new object[] { sender, e });
                return;
            }

            // sender : Reader 객체
            // e.Kind : 발생된 이벤트 종류
            // e.Message : 이벤트 발생에 대한 설명
            // e.Payload : 리더로부터 수신된 바이트 배열
            // e.CloseType : 닫기 유형
            string payload;

            switch (e.Kind)
            {
                // 연결이 완료되면 발생합니다.
                case ReaderEventKind.Connected:
                    this.tslblStatus.Text = e.Message;
                    this.btnConnect.Text = "Disconnect";

                    // 리더 정보 조회
                    GetConfiguration();

                    this.splitContainer1.Panel2.Enabled = true;

                    btnStartInventory_Click(btnStartInventory, e);

                    break;
                // 연결이 정상적으로 해제되면 발생합니다.
                case ReaderEventKind.Disconnected:
                    this.tslblStatus.Text = e.Message;
                    this.btnConnect.Text = "Connect";
                    this.splitContainer1.Panel2.Enabled = false;
                    //창닫기를 통해서 발생되었으면 창을 닫아 준다.
                    //if (e.CloseType == CloseType.FormClose) Close();
                    break;
                // 일정시간 동안 응답이 없는 경우 발생합니다.
                case ReaderEventKind.timeout:
                    break;
                // 리더의 버전정보 수신시 발생합니다.
                case ReaderEventKind.Version:
                    payload = Encoding.ASCII.GetString(e.Payload);
                    string[] items = payload.Split(' ');

                    switch (items[0])
                    {
                        case "v0": // Reader Version
                            this.lblVersionReader.Text = items[1];
                            break;
                        case "v2": // Root Version
                            this.lblVersionRoot.Text = items[1];
                            break;
                        case "v5": // H/W Version
                            this.lblVersionHardware.Text = items[1];
                            break;
                    }
                    break;
                // 리더의 안테나 정보 수신시 발생합니다.
                case ReaderEventKind.AntennaState:
                // 리더의 안테나 파워값 수신시 발생합니다.
                case ReaderEventKind.Power:
                    payload = Encoding.ASCII.GetString(e.Payload);

                    switch (payload.Substring(0, 1))
                    {
                        case "e": // Antenna State
                            int number = Convert.ToInt32(payload.Substring(1));
                            // Antenna Port는 안테나 번호순서대로 1, 2, 4, 8의 값이 할당됨
                            this.chkAnt1.Checked = (number & 0x0001) > 0;
                            this.chkAnt2.Checked = (number & 0x0002) > 0;
                            this.chkAnt3.Checked = (number & 0x0004) > 0;
                            this.chkAnt4.Checked = (number & 0x0008) > 0;
                            break;
                        case "p": //Power Value
                            this.nudPower.Value = Convert.ToInt32(payload.Substring(1));
                            break;
                    }
                    break;
                // Inventory시 Tag ID 수신시 발생합니다.
                case ReaderEventKind.TagId:
                    // 리더로부터 수신된 데이터는 바이트 배열에 들어 있으며 문자열로 Decode하여 사용합니다.
                    // 한개 이상의 수신 데이터가 들어 있을 수 있습니다.
                    // 하기와 같이 "\r\n>" 기준으로 분리하여 사용합니다.

                    payload = Encoding.ASCII.GetString(e.Payload);
                    string[] tagIds = payload.Split(new string[] { "\r\n>" }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (string tagid in tagIds)
                    {
                        //this.lvwInventory.Items.Add(tagid);
                        // 앞의 2자 (1T)는 응답코드이므로 제거합니다.
                        string epc = tagid.Substring(2);

                        this.lvwInventory.BeginUpdate();
                                                
                        ListViewItem lvi = this.lvwInventory.Items.Insert(0, DateTime.Now.ToString());
                        
                        lvi.SubItems.Add(epc);

                        //HEX 형태를 문자열로 표시해 준다.
                        string txt = string.Empty;
                        if (reader.TagType == TagType.ISO18000_6C_GEN2)
                        {
                            // PC 값 "HHHH" 을 제거하고 처리한다 - PC(HHHH)
                            int p = 4;
                            if (tagid.Length > p)
                            {
                                string hex = epc.Substring(p, epc.Length - p);
                                txt = reader.MakeTextFromHex(hex);
                            }
                        }

                        lvi.SubItems.Add(txt);
                        Insert_Log(epc + '\t' + txt );
                        this.lvwInventory.EndUpdate();
                        //this.lvwInventory.Refresh();

                        if( read_cnt++ >= 2)
                        {
                            Form1.Send_LD_String("say " + txt);
                            Form1.Send_LD_AMC_MSG("SEND", "NONE", "RFID_FIND", "NONE", txt);

                            if (bw_RFID_dealy.IsBusy == false)
                                bw_RFID_dealy.RunWorkerAsync();
                            read_cnt = 0;
                        }
                        
                    }
                    break;
                // Memory Bank 값 Read시 발생
                case ReaderEventKind.GetTagMemory:
                    payload = Encoding.ASCII.GetString(e.Payload);

                    if (payload.Substring(1, 1) == "T" || payload.Substring(1, 1) == "B")
                    {
                        this.mtxtTagHex.Text = payload.Substring(2);
                    }
                    break;
                // Memory Bank 값 Write, Lock, Kill 등 작업에 대한 응답
                case ReaderEventKind.TagResponseCode:
                    // C : Error등 응답코드
                    payload = Encoding.ASCII.GetString(e.Payload);

                    if (payload.Substring(1, 1) == "C")
                    {
                        string code = payload.Substring(2);
                        if (this.tabControl1.SelectedTab.Name == "tpReadWrite")
                            this.lstResponse.Items.Insert(0, code + "-" + Reader.Responses(code));
                        else
                            this.lstResponseLockKill.Items.Insert(0, code + "-" + Reader.Responses(code));
                    }
                    break;
            }
        }

        /// <summary>
        /// 리더의 정보를 조회합니다.
        /// </summary>
        private void GetConfiguration()
        {
            if (reader != null)
            {
                // 리더 쓰레드가 동작중이며, 인벤토링중이 아니라면
                if (reader.IsHandling && !reader.IsInventorying)
                {
                    this.Cursor = Cursors.WaitCursor;

                    this.lblVersionReader.Text = "";
                    this.lblVersionHardware.Text = "";
                    this.lblVersionRoot.Text = "";

                    //// Version - reader
                    //reader.GetVersion(0); Thread.Sleep(300);
                    //// Version - H/W
                    //reader.GetVersion(5); Thread.Sleep(300);
                    //// Version - Root
                    //reader.GetVersion(2); Thread.Sleep(300);

                    //// Version - reader
                    //this.lblVersionReader.Text = reader.GetVersionToSync(0); Thread.Sleep(300);
                    //// Version - H/W
                    //this.lblVersionHardware.Text = reader.GetVersionToSync(5); Thread.Sleep(300);
                    //// Version - Root
                    //this.lblVersionRoot.Text = reader.GetVersionToSync(2); Thread.Sleep(300);

                    // Power
                    reader.GetPower(); Thread.Sleep(300);
                    //Antenna state
                    reader.GetAntennaState(); Thread.Sleep(300);

                    this.Cursor = Cursors.Arrow;
                }
            }
        }


        /// <summary>
        /// 리더의 설정값을 변경합니다.
        /// </summary>
        private void SetConfiguration()
        {
            if (reader != null)
            {
                // 리더 쓰레드가 동작중이며, 인벤토링중이 아니라면
                if (reader.IsHandling && !reader.IsInventorying)
                {
                    this.Cursor = Cursors.WaitCursor;

                    int antstate =
                        ((this.chkAnt1.Checked ? 1 : 0) << 0) +
                        ((this.chkAnt2.Checked ? 1 : 0) << 1) +
                        ((this.chkAnt3.Checked ? 1 : 0) << 2) +
                        ((this.chkAnt4.Checked ? 1 : 0) << 3);

                    int power = (int)this.nudPower.Value;

                    // 안테나 값을 설정합니다.
                    reader.SetAntennaState(antstate); Thread.Sleep(300);
                    // 파워값을 설정합니다.
                    reader.SetPower(power); Thread.Sleep(300);

                    // 리더 설정값 변경에 따른 응답 이벤트는 발생되지 않습니다.

                    this.Cursor = Cursors.Arrow;
                }
            }
        }


        private void Insert_Run_Log(string msg)
        {
            string date = System.DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss:FFF"); ;
            string str_temp = "";
            string log_dir = System.Environment.CurrentDirectory + "\\Log\\RFID\\" + System.DateTime.Now.ToString("yyyy/MM/dd") + "_LOG.txt";

            if (System.IO.File.Exists(log_dir) == false)
            {
                
                string temp;

                temp = "========================================================" + Environment.NewLine;
                temp += "=                                                                                                            =" + Environment.NewLine;
                temp += "=                                      RFID Log File " + System.DateTime.Now.ToString("yyyy/MM/dd") + "                                    =" + Environment.NewLine;
                temp += "=                                                                                                            =" + Environment.NewLine;
                temp += "========================================================" + Environment.NewLine;

                System.IO.File.WriteAllText(log_dir, temp);
            }

            //str_buf = System.IO.File.ReadAllText(log_dir);

            string[] arr_str = msg.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            System.IO.StreamWriter st = System.IO.File.AppendText(log_dir);

            for (int i = 0; i < arr_str.Length; i++)
            {
                if (arr_str[i].Trim('\0') != "")
                {
                    str_temp = date + '\t' + arr_str[i];
                    st.WriteLine(str_temp);
                }
            }

            st.Close();
            st.Dispose();
        }


        private void Insert_Log(string msg)
        {
            string date = System.DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss:FFF"); ;
            string str_temp = "";
            string log_dir = System.Environment.CurrentDirectory + "\\Log\\RFID\\" + System.DateTime.Now.ToString("yyyy/MM/dd") + "_RFID.txt";

            if (System.IO.File.Exists(log_dir) == false)
            {
                string temp;

                temp = "========================================================" + Environment.NewLine;
                temp += "=                                                                                                            =" + Environment.NewLine;
                temp += "=                                      RFID Log File " + System.DateTime.Now.ToString("yyyy/MM/dd") + "                                    =" + Environment.NewLine;
                temp += "=                                                                                                            =" + Environment.NewLine;
                temp += "========================================================" + Environment.NewLine;

                System.IO.File.WriteAllText(log_dir, temp);
            }

            //str_buf = System.IO.File.ReadAllText(log_dir);

            string[] arr_str = msg.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            System.IO.StreamWriter st = System.IO.File.AppendText(log_dir);

            for (int i = 0; i < arr_str.Length; i++)
            {
                if (arr_str[i].Trim('\0') != "")
                {
                    str_temp = date + '\t' + arr_str[i];
                    st.WriteLine(str_temp);
                }
            }

            st.Close();
            st.Dispose();
        }


        /// <summary>
        /// 인벤토리를 시작/종료합니다.
        /// </summary>
        /// <param name="action">Action : Start / Stop</param>
        private void Inventory(Action action)
        {
            if (reader != null)
            {
                // 리더 쓰레드가 동작중인가?
                if (reader.IsHandling)
                {
                    if (action == Action.Start)
                    {
                        // ISO18000_6C_GEN2 타입으로 Multi Inventory를 수행합니다.
                        reader.ReadTagId(TagType.ISO18000_6C_GEN2, ReadType.INTERVAL);
                        // ReaderEventKind.TagID 이벤트 발생
                        this.tslblStatus.Text = "Run Inventory";
                    }
                    else
                    {
                        // Inventory 작업을 중지합니다.
                        //reader.StopOperation();
                        bool returnvalue = reader.StopOperationToSync();
                        if (returnvalue)
                        {
                            // 이벤트 발생없음
                            this.tslblStatus.Text = "Ready";

                            Inventory(Action.Start);
                        }
                        else
                        {
                            this.tslblStatus.Text = "Stop Failed";
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 해당 메모리영역의 값을 조회합니다.
        /// </summary>
        /// <param name="bank">메모리 뱅크(0:Reserved, 1:EPC, 2:TID, 3:USER)</param>
        /// <param name="location">시작위치(단위:Word)</param>
        /// <param name="length">길이(단위:Word)</param>
        /// <param name="password">Password(2Word HEX)</param>
        private void GetTagMemory(uint bank, uint location, uint length, string password)
        {
            if (reader != null)
            {
                // 리더 쓰레드가 동작중인가?
                if (reader.IsHandling)
                {
                    // 해당 메모리 뱅크의 값을 조회합니다.
                    // location 0 은 CRC, 1 은 PC 값의 시작위치를 의미합니다.
                    reader.ReadTagMemory(TagType.ISO18000_6C_GEN2, bank, location, length, password);
                    // ReaderEventKind.GetTagMemory 이벤트 발생
                }
            }
        }

        /// <summary>
        /// 해당 메모리영역에 값을 저장합니다.
        /// </summary>
        /// <param name="bank">메모리 뱅크(0:Reserved, 1:EPC, 2:TID, 3:USER)</param>
        /// <param name="location">시작위치(단위:Word)</param>
        /// <param name="length">길이(단위:Word)</param>
        /// <param name="data">변경할 데이터(HEX)</param>
        /// <param name="password">Password(2Word HEX)</param>
        private void SetTagMemory(uint bank, uint location, uint length, string data, string password)
        {
            if (reader != null)
            {
                // 리더 쓰레드가 동작중인가?
                if (reader.IsHandling)
                {
                    // 해당 메모리 뱅크에 값을 저장합니다.
                    // location 0 은 CRC, 1 은 PC 값의 시작위치를 의미하므로
                    // 임의로 변경하지 마십시오.
                    reader.WriteTagMemory(TagType.ISO18000_6C_GEN2, bank, location, data, password);
                    // ReaderEventKind.SetTagMemory 이벤트 발생
                }
            }
        }

        /// <summary>
        /// LOCK을 수행합니다.
        /// </summary>
        /// <param name="killpermissions">The killpermissions.</param>
        /// <param name="accesspermissions">The accesspermissions.</param>
        /// <param name="epcpermissions">The epcpermissions.</param>
        /// <param name="tidpermissions">The tidpermissions.</param>
        /// <param name="userpermissions">The userpermissions.</param>
        /// <param name="password">The password.</param>
        private void LockTag(string killpermissions, string accesspermissions, string epcpermissions, string tidpermissions, string userpermissions, string password)
        {
            if (reader != null)
            {
                // 리더 쓰레드가 동작중인가?
                if (reader.IsHandling)
                {
                    string mask = "";
                    string action = "";

                    // RF-1000 모델인 경우
                    if (reader.IsRf1000Series)
                    {
                        mask =  killpermissions +
                                accesspermissions +
                                epcpermissions +
                                tidpermissions;

                        action = "000" + userpermissions;
                    }

                    // 태그 Lock을 수행합니다.
                    // ALWAYS ACCESSIBLE, ALWAYS NOT ACCESSIBLE 로 설정하면
                    // 다시 되돌릴 수 없으므로 주의하십시오.
                    reader.LockTag(mask, action, password);
                    // ReaderEventKind.LockTag 이벤트 발생
                }
            }
        }

        /// <summary>
        /// KILL을 수행합니다.
        /// </summary>
        /// <param name="password">The password.</param>
        private void KillTag(string password)
        {
            if (reader != null)
            {
                // 리더 쓰레드가 동작중인가?
                if (reader.IsHandling)
                {
                    // KILL된 TAG는 다시는 사용할 수 없으므로 주의하십시오.
                    reader.KillTag(password);
                    // ReaderEventKind.KillTag 이벤트 발생
                }
            }
        }

        /// <summary>
        /// 선택한 length(word) 만큼만 입력되도록 mask format 적용
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void OnLengthChange(object sender, EventArgs e)
        {
            int length = (int)this.numLength.Value;

            string mask = "";

            for (int i = 0; i < length; i++)
            {
                mask += ">AAAA-";
            }

            this.mtxtTagHex.Mask = mask.Substring(0, mask.Length - 1);
        }

        /// <summary>
        /// IP 정규식 검사
        /// </summary>
        /// <param name="ipaddress">The ipaddress.</param>
        /// <returns></returns>
        private bool CheckIpFormat(string ipaddress)
        {
            string pattern = @"^(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])$";

            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(pattern);

            return regex.IsMatch(ipaddress);
        }

        ManualResetEvent _resetevent = new ManualResetEvent(false);

        Queue<getwork> _work = new Queue<getwork>();

        public delegate void getwork(object value);
        public getwork work;

        private void button1_Click(object sender, EventArgs e)
        {
            work = new getwork(testwork);


            _work.Enqueue(new getwork(testwork));

            _work.Dequeue()(0);
        }

        private void testwork(object value)
        {
            reader.GetVersion((int)value);
        }

        public void Set_IP(string IP)
        {
            txtIpAddress.Text = IP;
        }

        public void Connect()
        {
            EventArgs e = new EventArgs();

            btnConnect_Click("Connect", e);
        }
                       
        int RFID_delay_ms = 3000;
        private void bw_RFID_dealy_DoWork(object sender, DoWorkEventArgs e)
        {
            System.Threading.Thread.Sleep(RFID_delay_ms);
            btnStartInventory_Click(btnStartInventory, e);
        }

       
    }
}
