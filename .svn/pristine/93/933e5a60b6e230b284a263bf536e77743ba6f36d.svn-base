using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Protocol;

using AMC_Test.ClientSession;
using AMC_Test.CustomEventArgs;
using SocketGlobal;
using SocketGlobal.SendData;
using SocketGlobal.LargeData;


namespace AMC_Test
{
	#region UI로 연결할 델리게이트
	
	/// <summary>
	/// 메시지 요청
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	public delegate void dgMessage(claClientSession session, LocalMessageEventArgs e);
	#endregion

	public class claServer : AppServer<claClientSession, BinaryRequestInfo>
	{
		#region 연결할 이벤트 ♥♥♥♥♥♥♥♥♥♥♥♥
		/// <summary>
		/// 로그에 표시할 내용
		/// </summary>
		public event dgMessage OnLog;

		/// <summary>
		/// UI메시지표시
		/// </summary>
		public event dgMessage OnMessaged;
		
		/// <summary>
		/// 유저의 로그인이 완료 되었다.
		/// </summary>
		public event dgMessage OnLoginUser;
		/// <summary>
		/// 유저가 로그아웃 하였다.
		/// </summary>
		public event dgMessage OnLogoutUser;

		#endregion

		/// <summary>
		/// 명령어 클래스
		/// </summary>
		private claCommand m_insCommand = new claCommand();
		/// <summary>
		/// 큰데이터 관리 메니저
		/// </summary>
		private claLargeData_GetManager m_insLDM = new claLargeData_GetManager();
		

		public claServer()
			: base(new DefaultReceiveFilterFactory<claReceiveFilter, BinaryRequestInfo>())
		{
			
		}

		/// <summary>
		/// 새세션이 접속하면 발생
		/// </summary>
		/// <param name="session"></param>
		protected override void OnNewSessionConnected(claClientSession session)
		{
			//세션으로부터 받을 메시지용 이벤트
			session.OnMessaged += session_OnMessaged;

			base.OnNewSessionConnected(session);
		}

		protected override void OnSessionClosed(claClientSession session, CloseReason reason)
		{
			//로그아웃 처리를 하여 유저가 끊김을 알린다.
			OnLogoutUser(session, null);
			base.OnSessionClosed(session, reason);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="e"></param>
		void session_OnMessaged(claClientSession session,  LocalMessageEventArgs e)
		{
			//OnMessaged(e);	
		}

		/// <summary>
		/// 데이터를 받았다!
		/// </summary>
		/// <param name="session"></param>
		/// <param name="requestInfo"></param>
		protected override void ExecuteCommand(claClientSession session, BinaryRequestInfo requestInfo)
		{
			//UI에 메시지를 표시한다.
			LocalMessageEventArgs e
				= new LocalMessageEventArgs( claGlobal.g_SocketUtile.ByteToString( requestInfo.Body)
											, Type.typeLocal.None);
			OnLog(session, e);

			//오리지널 데이터를 분석합니다.
			claSendData_Analysis insSD_A
				= new claSendData_Analysis(requestInfo.Body);


			//사용자 클래스에서 넘어온 데이터 처리
			MsgAnalysis(session, insSD_A);
		}

		/// <summary>
		/// 데이터를 분석한다.
		/// </summary>
		/// <param name="sMsg"></param>
		private void MsgAnalysis(claClientSession session, claSendData_Analysis insSD_A)
		{
			//메시지 처리
			StringBuilder sbMsg = new StringBuilder();
			sbMsg.Clear();

			switch (insSD_A.DataSend.Command)
			{
				case claCommand.Command.Msg:	//메시지
					insSD_A.CutData1ToDatas();
					sbMsg.Append(session.UserID);
					sbMsg.Append(" : ");
					sbMsg.Append(insSD_A.Data);

					Command_SendMsg(sbMsg.ToString());
					break;
				case claCommand.Command.User_List_Get:	//유저 리스트 갱신 요청
					Command_User_List_Get(session);
					break;

				case claCommand.Command.ID_Check:	//아이디 체크
					insSD_A.CutData1ToDatas();
					Command_IDCheck(session, insSD_A.Data);
					break;
				case claCommand.Command.Login:	//로그인
					Command_Login(session);
					break;
				case claCommand.Command.Logout:	//로그아웃
					Command_Logout(session);
					break;

				case claCommand.Command.LargeData_Start:	//파일 전송 시작
					Command_LargeData_Start(session, insSD_A);
					break;
				case claCommand.Command.LargeData_Receive:	//파일 전송
					Command_LargeData_Receive(session, insSD_A);
					break;
				case claCommand.Command.LargeData_End:		//파일 전송 끝
					insSD_A.CutData1ToDatas();
					Command_LargeData_End(session, insSD_A.Data);
					break;
			}
		}

		/// <summary>
		/// 명령 처리 - 메시지 보내기
		/// </summary>
		/// <param name="sMsg"></param>
		private void Command_SendMsg(string sMsg)
		{
			OnMessaged(null, new LocalMessageEventArgs(sMsg, Type.typeLocal.None));
			//전체 유저에게 메시지 전송
			SendMsg_All(claCommand.Command.Msg, sMsg);
		}

		/// <summary>
		/// 아이디를 체크 합니다.
		/// </summary>
		/// <param name="session"></param>
		/// <param name="sID"></param>
		private void Command_IDCheck(claClientSession session, string sID)
		{
			//사용 가능 여부
			bool bReturn = true;

			//모든 유저의 아이디 체크
			foreach (claClientSession insUserTemp in this.GetAllSessions())
			{
				if (insUserTemp.UserID == sID)
				{
					//같은 유저가 있다!
					//같은 유저가 있으면 그만 검사한다.
					bReturn = false;
					break;
				}
			}

			if (true == bReturn)
			{
				//사용 가능

				//아이디를 지정하고
				session.UserID = sID;

				//유저에게 로그인이 성공했음을 알림
				//접속자에게 먼저 로그인이 성공했음을 알린다.
				session.SendMsg_User(claCommand.Command.ID_Check_Ok, "");

				//유저가 접속 했음을 직접 알리지 말고 'ID_Check_Ok'를 받은
				//클라이언트가 직접 요청한다.
				SendMsg_All(claCommand.Command.Msg, "접속 성공");
			}
			else
			{
				//검사 실패를 알린다.
				session.SendMsg_User(claCommand.Command.ID_Check_Fail, "");
			}

		}

		/// <summary>
		/// 유저가 아이디체크를 끝내고 접속한다!
		/// </summary>
		/// <param name="session"></param>
		private void Command_Login(claClientSession session)
		{			
			//로그인이 완료된 유저에게 유저 리스트를 보낸다.
			Command_User_List_Get(session);

			//전체 유저에게 메시지 전송
			SendMsg_All(claCommand.Command.User_Connect, session.UserID);

			//서버에 로그인 로그를 남긴다.
			OnLoginUser(session, null);

		}

		private void Command_Logout(claClientSession session)
		{
			//전체 유저에게 메시지 전송
			SendMsg_All(claCommand.Command.User_Disonnect, "");

			//서버에 로그아웃 로그를 남긴다.
			OnLogoutUser(session, null);
		}


		/// <summary>
		/// 명령 처리 - 유저 리스트 갱신 요청
		/// </summary>
		/// <param name="insUser"></param>
		private void Command_User_List_Get(claClientSession insUser)
		{
			StringBuilder sbList = new StringBuilder();

			//this.GetAllSessions()의 버그인지 타이밍 문제인지는 모르겠지만
			//this.GetAllSessions()의 리스트에서 방금접속한 유저가 들어있지 않는 경우가 있다.
			//일단 리스트를 만들때 방금접속한 유저는 제외하고 만든 후 다시 추가 해주는 방법을 사용하자.
			//리스트 만들기
			foreach (claClientSession insUser_Temp in this.GetAllSessions())
			{
				//유저아이디가 다를때만 리스트에 추가
				if (insUser_Temp.UserID != insUser.UserID)
				{
					sbList.Append(insUser_Temp.UserID);
					sbList.Append(claGlobal.g_Division1);
				}
			}

			//자기 아이디는 따로 추가
			sbList.Append(insUser.UserID);

			//요청에 응답해준다.
			insUser.SendMsg_User(claCommand.Command.User_List, sbList.ToString());
		}

		private void Command_LargeData_Start(claClientSession insUser, claSendData_Analysis insSD_A)
		{
			insSD_A.CutData1ToDatas();

			//보내는 쪽에서 준 인덱스
			int nIndex = claGlobal.g_Number.StringToInt(insSD_A.Datas[4]);
			//큰데이터 받기 준비
			int nDBIndex
				= this.m_insLDM.Start((typeLargeData)claGlobal.g_Number.StringToInt(insSD_A.Datas[0])
										, insSD_A.Datas[1]
										, claGlobal.g_Number.StringToInt(insSD_A.Datas[2])
										, claGlobal.g_Number.StringToInt(insSD_A.Datas[3])
										, nIndex);

			//큰데이터 받기 준비 끝을 알림
			insUser.SendMsg_User(claCommand.Command.LargeData_Info
									, nDBIndex.ToString()
									, nIndex.ToString());
		}

		private void Command_LargeData_Receive(claClientSession insUser, claSendData_Analysis insSD_A)
		{
			//큰데이터가 오고 있다.
			
			//디비 인덱스
			int nDBIndex = -1;
			//데이터 인덱스(카운트 갯수)
			int nCount = -1;
			typeLargeDataManager typeLDM = typeLargeDataManager.None;
			
			//데이터를 추가 한다.
			typeLDM = this.m_insLDM.AddData(insSD_A.DataSend.Data_Byte, out nDBIndex, out nCount);

			if (typeLargeDataManager.NoItem == typeLDM)
			{	//아이템이 없는 경우
				//아이템이 없으면 재요청을 하는 식으로 처리 한다.
			}

			//데이터 전송 완료 체크
			/*if (false == LargeData_CheckComplete(nDBIndex))
			{	//데이터가 다오지 않았다.
				//이번 데이터는 다 받았다고 클라이언트에게 보내준다.
				insUser.SendMsg_User(claCommand.Command.LargeData_Receive_Complete
										, nDBIndex.ToString()
										, nCount.ToString());
			}*/

			insUser.SendMsg_User(claCommand.Command.LargeData_Receive_Complete
										, nDBIndex.ToString()
										, nCount.ToString());

		}

		public void Command_LargeData_End(claClientSession insUser, string sDBIndex)
		{
			//이것은 클라이언트가 모든 파일을 전송시도 했다는 의미이다.
			//절대 모든 데이터가 도착했다는 의미가 아니다.

			LargeData_CheckComplete(Convert.ToInt32(sDBIndex));
		}

		/// <summary>
		/// 큰데이터가 다 왔는지 확인
		/// </summary>
		/// <param name="nDBIndex"></param>
		private bool LargeData_CheckComplete(int nDBIndex)
		{
			//데이터받기가 완료 되었는지?
			if (true == this.m_insLDM.CheckData(nDBIndex))
			{	//완료됐다.
				//데이터처리가 끝나면 할 작업
				this.m_insLDM.CompleteData(nDBIndex);
				return true;
			}
			else
			{
				return false;
			}
		}


		public void SendMsg_All(claCommand.Command typeCommand, string sData)
		{
			foreach (claClientSession insUserTemp in this.GetAllSessions())
			{
                if (insUserTemp.Connected == true)
                {
                    //insUserTemp.SendMsg_User(typeCommand, sData);
                    
                    insUserTemp.Send(sData);
                }
			}
		}

	}
}
