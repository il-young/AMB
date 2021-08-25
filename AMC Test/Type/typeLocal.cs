using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMC_Test.Type
{
	/// <summary>
	/// 서버 내부 메시지 전달용
	/// </summary>
	public enum typeLocal
	{
		//http://msdn.microsoft.com/ko-kr/library/system.windows.forms.messageboxicon(v=vs.110).aspx

		None = 0,
		Asterisk,

		/// <summary>
		/// 메시지
		/// </summary>
		Msg,
	}
}
