using System;
using System.Data;
using System.Drawing;
using System.Data.OracleClient;


namespace FORM.ClassLib
{
	/// <summary>
	/// ComVar에 대한 요약 설명입니다.
	/// </summary>
	public class ComVar : COM.ComVar 
	{
		public ComVar()
		{
			//
			// TODO: 여기에 생성자 논리를 추가합니다.
			//
		}
        public static string This_Computer = "";
        public static string This_Action;
        public static string This_Win_ID;
        public static string This_PGM = "MOLD";
        public static string This_Packages;

        public static string This_Title = "";
        public static int This_Screen = 1;
        public static string This_Wh = "";
        public static string This_Mline = "";

        public static bool This_Status = false;

        //public static string This_User = "admin";
        // 공통사용
        public const string Insert = "I";
        public const string Update = "U";
        public const string Delete = "D";

	}
}
