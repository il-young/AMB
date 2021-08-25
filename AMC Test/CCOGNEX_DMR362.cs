using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Cognex.DataMan.SDK;
using Cognex.DataMan.SDK.Discovery;
using Cognex.DataMan.SDK.Utils;
using System.Xml;





namespace AMC_Test
{

    class CCOGNEX_DMR362
    {
        /***********************************************************************************************************/
        private ResultCollector _results;

        private SynchronizationContext _syncContext = null;
        private EthSystemDiscoverer _ethSystemDiscoverer = null;
        private SerSystemDiscoverer _serSystemDiscoverer = null;
        private ISystemConnector _connector = null;
        private DataManSystem _system = null;
        private object _currentResultInfoSyncLock = new object();
        private bool _closing = false;
        private bool _autoconnect = false;
        private object _listAddItemLock = new object();
        private GuiLogger _logger;

        /***********************************************************************************************************/

        private System.ComponentModel.BackgroundWorker bw_DMR_DATA = new System.ComponentModel.BackgroundWorker();
        private Socket DMR_CLIENT = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private IPAddress DMR_IP;
        private int DMR_PORT;
        private bool bDATA_T;

        private AMC_Test.Form1.stDMR_POSITION RES_POSITION;

        private const double DMR362_150_WFOV = 106;
        private const double DMR362_150_HFOV = 91;
        private const double DMR362_250_WFOV = 171;
        private const double DMR362_250_HFOV = 106;

        private const double DMR362_150_WPIXEL = 0.082;
        private const double DMR362_150_HPIXEL = 0.088;
        private const double DMR362_250_WPIXEL = 0.133;
        private const double DMR362_250_HPIXEL = 0.103;

        private const double DMR362_WPIXEL_INC = 0.00051;   // 1mm 당 Pixel 넓이 증가량.
        private const double DMR362_HPIXEL_INC = 0.00015;   // 1mm 당 Pixel 높이 증가량.

        //public event EventHandler Parsing_COMP;

        Form1.stDMR_POSITION QR_POS;

        public CCOGNEX_DMR362()
        {
            bw_DMR_DATA.DoWork += Bw_DMR_DATA_DoWork;
            bDATA_T = false;

            _syncContext = WindowsFormsSynchronizationContext.Current;
            //_logger = new GuiLogger(tbLog, cbLoggingEnabled.Checked, ref _closing);

            // Create discoverers to discover ethernet and serial port systems.
            _ethSystemDiscoverer = new EthSystemDiscoverer();
            _serSystemDiscoverer = new SerSystemDiscoverer();

            // Subscribe to the system discoved event.
            //_ethSystemDiscoverer.SystemDiscovered += new EthSystemDiscoverer.SystemDiscoveredHandler(OnEthSystemDiscovered);
            //_serSystemDiscoverer.SystemDiscovered += new SerSystemDiscoverer.SystemDiscoveredHandler(OnSerSystemDiscovered);

            // Ask the discoverers to start discovering systems.
            _ethSystemDiscoverer.Discover();
            _serSystemDiscoverer.Discover();
        }

        public Form1.stDMR_POSITION Get_LOC()
        {
            return QR_POS;
        }

        private void Bw_DMR_DATA_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            byte[] _data = new byte[10240];
            string _buf = "";
            IPEndPoint IP = new IPEndPoint(DMR_IP, DMR_PORT);

            DMR_CLIENT.Connect(IP);

            while (bDATA_T)
            {
                for (int i = 0; i < 5; i++)
                {
                    DMR_Trigger_();
                    System.Threading.Thread.Sleep(100);

                    DMR_CLIENT.Receive(_data);
                    _buf = Encoding.Default.GetString(_data);

                    DMR_PARSING(_buf);

                    _data = new byte[10240];
                    _buf = "";

                    System.Threading.Thread.Sleep(500);
                }
            }
            throw new NotImplementedException();
        }

        private void DMR_PARSING(string msg)
        {
            string[] msg_temp = msg.Split(';');
            string[] msg_buf = new string[2];

            for (int i = 0; i < (msg_temp[msg_temp.Length - 1] == "" ? msg_temp.Length - 1 : msg_temp.Length); i++)
            {
                msg_buf = msg_temp[i].Split(',');
            }

            RES_POSITION.X = int.Parse(msg_buf[0]);
            RES_POSITION.Y = int.Parse(msg_buf[1]);

        }

        public void Set_IP(string IP)
        {
            DMR_IP = IPAddress.Parse(IP);
        }
        
        public void Set_PORT(int port)
        {
            DMR_PORT = port;
        }

        private void DMR_Trigger_()
        {

        }

        public AMC_Test.Form1.stDMR_POSITION Get_Position(double Range)
        {
            AMC_Test.Form1.stDMR_POSITION aa = new Form1.stDMR_POSITION();
            AMC_Test.Form1.stDMR_POSITION Pixel_size = new AMC_Test.Form1.stDMR_POSITION();
            double val = Range - 150;


            Pixel_size.X = (int)(DMR362_150_WPIXEL + (val * DMR362_WPIXEL_INC));
            Pixel_size.Y = (int)(DMR362_150_HPIXEL + (val * DMR362_HPIXEL_INC));

            aa.X = Pixel_size.X * RES_POSITION.X;
            aa.Y = Pixel_size.Y * RES_POSITION.Y;

            RES_POSITION = new Form1.stDMR_POSITION();

            return aa;

        }
        
        private void btnTrigger_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                _system.SendCommand("TRIGGER ON");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to send TRIGGER ON command: " + ex.ToString());
            }
        }

        private void btnTrigger_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                _system.SendCommand("TRIGGER OFF");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to send TRIGGER OFF command: " + ex.ToString());
            }
        }

        private void OnLiveImageArrived(IAsyncResult result)
        {
            try
            {
                Image image = _system.EndGetLiveImage(result);

                _syncContext.Post(
                    delegate
                    {
                        Size image_size = new Size();
                        Image fitted_image = Gui.ResizeImageToBitmap(image, image_size);
                        //picResultImage.Image = fitted_image;
                        //picResultImage.Invalidate();

                        //if (cbLiveDisplay.Checked)
                        //{
                        //    _system.BeginGetLiveImage(
                        //        ImageFormat.jpeg,
                        //        ImageSize.Sixteenth,
                        //        ImageQuality.Medium,
                        //        OnLiveImageArrived,
                        //        null);
                        //}
                    },
                null);
            }
            catch
            {
            }
        }

    }
}
