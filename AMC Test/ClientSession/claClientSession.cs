using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Protocol;

using SocketGlobal;
using AMC_Test.CustomEventArgs;
using SocketGlobal.SendData;


namespace AMC_Test.ClientSession
{
	public class claClientSession : AppSession<claClientSession, BinaryRequestInfo>
	{
		#region 연결할 이벤트 ♥♥♥♥♥♥♥♥♥♥♥♥
		/// <summary>
		/// UI에 표시할 메시지
		/// </summary>
		public event dgMessage OnMessaged;
		#endregion

		/// <summary>
		/// 이 유저의 아이디
		/// </summary>
		public string UserID { get; set; }

		protected override void OnSessionStarted()
		{
			base.OnSessionStarted();
		}

		protected override void HandleException(Exception e)
		{
			this.Send("오류 : {0}", e.Message);
		}

		protected override void OnSessionClosed(CloseReason reason)
		{
			base.OnSessionClosed(reason);
		}

		/// <summary>
		/// 서버객체로 메시지를 넘길때 사용
		/// </summary>
		/// <param name="sMsg"></param>
		private void SendMsg_ServerLog(string sMsg)
		{
			LocalMessageEventArgs e 
				= new LocalMessageEventArgs(sMsg, Type.typeLocal.None);

			OnMessaged(this, e);
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sMsg"></param>
		public void SendMsg_User(claCommand.Command typeCommand, string sData)
		{
			//보낼 데이터 만들기
			dataSend insSend = new dataSend();
			insSend.Command = typeCommand;
			insSend.Data_String = sData;

			//바이트로 변환
			byte[] byteSend = insSend.CreateDataOriginal().Data;
			//보낸다
			this.Send(byteSend, 0, byteSend.Length);
		}

		public void SendMsg_User(claCommand.Command typeCommand,params string[] sData)
		{
			//보낼 데이터 만들기
			dataSend insSend = new dataSend(typeCommand, sData);

			//바이트로 변환
			byte[] byteSend = insSend.CreateDataOriginal().Data;
			//보낸다
			this.Send(byteSend, 0, byteSend.Length);
		}



	}
}
