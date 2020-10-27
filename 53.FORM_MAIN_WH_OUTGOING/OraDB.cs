using System;
using System.Windows.Forms;
using System.Data;
using System.Data.OracleClient;
using C1.Win.C1FlexGrid; 



namespace COM
{
	/// <summary>
	/// OraDB에 대한 요약 설명입니다.
	/// </summary>
	public class OraDB
	{

		#region 변수정의

		private DataSet DS_Select = new DataSet("Parameter DataSet");
		private DataSet DS_Modify = new DataSet("Modify DataSet");
		private DataSet DS_Run = new DataSet("Run DataSet");

		private DataSet DS_Ret = new DataSet("Return DataSet");


		//------- 프로시저 전달용 변수선언
		/// <summary>
		/// SP 프로세스명
		/// </summary>
		public  string Process_Name;
		/// <summary>
		/// SP 파라메터 배열
		/// </summary>
		public  string[] Parameter_Name;
		/// <summary>
		/// SP 파라메터 유형 배열
		/// </summary>
		public  int[] Parameter_Type;
		/// <summary>
		/// SP 파라메터 값 배열
		/// </summary>
		public  string[] Parameter_Values;
		/// <summary>
		/// SP 파라메터 매트릭스 배열
		/// </summary>
		public  string[] Parameter_Matrix;

		#endregion



		public OraDB()
		{
			//
			// TODO: 여기에 생성자 논리를 추가합니다.
			//
		}

		/// <summary>
		/// ReDim_Parameter : 프로시저 기동용 변수 재정의
		/// </summary>
		/// <param name="arg_count">변수 Count</param>
		public void ReDim_Parameter(int arg_count)
		{
			this.Parameter_Name = new string[arg_count]; 
			this.Parameter_Type = new int[arg_count]; 
			this.Parameter_Values = new string[arg_count] ;
		}


		/// <summary>
		/// Clear_Select_DataSet
		/// </summary>
		private void Clear_Select_DataSet()
		{
			DS_Select.Reset();
		}


		/// <summary>
		/// Clear_Run_DataSet
		/// </summary>
		private void Clear_Run_DataSet()
		{
			DS_Run.Reset();
		}


		/// <summary>
		/// Clear_Modify_DataSet
		/// </summary>
		public void Clear_Modify_DataSet()
		{
			DS_Modify.Reset();
		}


		/// <summary>
		/// Add_Select_Parameter :  조회를 위해 미리 Setting 되어진 Parameter정보를 DataSet에 추가
		/// </summary>
		/// <param name="AfterClear">기존의 DataSet을 Clear하고 추가(Cleaer하지 않을 경우는 복수로 추가됨</param>
		/// <returns>정상 : true ,오류 : false</returns>
		public bool Add_Select_Parameter (bool AfterClear) //string Process_Name, string[]  Parameter_Name, int[] Parameter_Type, string[] Parameter_Values)
		{
			DataTable DT_Select = new DataTable(Process_Name);
			DataColumn[] dc= new DataColumn[3];
 
			try
			{
				dc[0] = new DataColumn("Parameter_Name",Type.GetType("System.String"));
				dc[1] = new DataColumn("Parameter_Type",Type.GetType("System.Int32"));
				dc[2] = new DataColumn("Parameter_Value",Type.GetType("System.String"));
				DT_Select.Columns.AddRange(dc);

				for(int i=0; i< Parameter_Name.Length ;i++)
				{
					DataRow newRow = DT_Select.NewRow() ;
				
					newRow["Parameter_Name"] = Parameter_Name[i]; 
					newRow["Parameter_Type"] = (int)Parameter_Type[i];
					newRow["Parameter_Value"] = (Parameter_Values[i]==null) ? "": Parameter_Values[i]  ;
					DT_Select.Rows.Add(newRow);

				}
				if (AfterClear) this.Clear_Select_DataSet();
				DS_Select.Tables.Add(DT_Select);
				return true;
			}
			catch(Exception ex)
			{
				//MessageBox.Show("Error: " + Process_Name + " at Add_Select_Parameter !!"+ "\n" + ex.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
				return false;
			}


		}


		/// <summary>
		/// Add_Run_Parameter : Procedure 실행을 위해 미리 Setting 되어진 Parameter정보를 DataSet에 추가
		/// </summary>
		/// <param name="AfterClear">기존의 DataSet을 Clear하고 추가(Cleaer하지 않을 경우는 복수로 추가됨)</param>
		/// <returns>정상 : true ,오류 : false</returns>
		public bool Add_Run_Parameter (bool AfterClear) //string Process_Name, string[]  Parameter_Name, int[] Parameter_Type, string[] Parameter_Values)
		{
			DataTable DT_Run = new DataTable(Process_Name);
			DataColumn[] dc= new DataColumn[3];

			try
			{
				dc[0] = new DataColumn("Parameter_Name",Type.GetType("System.String"));
				dc[1] = new DataColumn("Parameter_Type",Type.GetType("System.Int32"));
				dc[2] = new DataColumn("Parameter_Value",Type.GetType("System.String"));
				DT_Run.Columns.AddRange(dc);

				for(int i=0; i< Parameter_Name.Length ;i++)
				{
					DataRow newRow = DT_Run.NewRow() ;
				
					newRow["Parameter_Name"] = Parameter_Name[i]; 
					newRow["Parameter_Type"] = (int)Parameter_Type[i];
					newRow["Parameter_Value"] = (Parameter_Values[i]==null) ? "" : Parameter_Values[i] ;
					DT_Run.Rows.Add(newRow);

				}
				if (AfterClear) this.Clear_Run_DataSet();
				DS_Run.Tables.Add(DT_Run);
				return true;
			}
			catch(Exception ex)
			{
				//MessageBox.Show("Error: " + Process_Name + " at Add_Run_Parameter !!" + "\n" + ex.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
				return false;
			}


		}


		/// <summary>
		/// Add_Modify_Parameter : Data 저장을 위해 미리 Setting 되어진 Parameter정보를 DataSet에 추가
		/// </summary>
		/// <param name="AfterClear">기존의 DataSet을 Clear하고 추가(Cleaer하지 않을 경우는 복수로 추가됨)</param>
		/// <returns>정상 : true ,오류 : false</returns>
		public bool Add_Modify_Parameter (bool AfterClear) 
		{
			DataTable DT_Modify = new DataTable(Process_Name);
			DataColumn[] dc= new DataColumn[Parameter_Name.Length];

			int row,col ;

			try
			{
				// DataTable의 Column 정의
				for(int i=0 ;i< Parameter_Name.Length;i++)
				{
					dc[i] = new DataColumn (Parameter_Name[i],Type.GetType("System.String"));
				}
				DT_Modify.Columns.AddRange(dc);

				col=0;
				DataRow newRow = DT_Modify.NewRow() ;

				for(row=0 ;row< Parameter_Values.Length ;row++)
				{
					
					newRow[col] =(Parameter_Values[row]==null) ? "" : Parameter_Values[row].ToString() ;
					col = col +1;
					if(col == Parameter_Name.Length)
					{
						DT_Modify.Rows.Add(newRow);
						col=0;
						
						if (row < (Parameter_Values.Length -1)) newRow = DT_Modify.NewRow() ;
					}
				
				}
				if (AfterClear) this.Clear_Modify_DataSet();
				this.DS_Modify.Tables.Add(DT_Modify);
				return true;
			}
			catch(Exception ex)
			{
				//MessageBox.Show("Error: " + Process_Name + " at Add_Modify_Parameter !!" + "\n" + ex.Message ,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
				return false;
			}


		}


 


		/// <summary>
		/// Exe_Select_Procedure : 복수개의 DataTable 파라미터를 이용하여 조회
		/// </summary>
		/// <returns>정상 : DataSet ,오류 : null</returns>
		public DataSet Exe_Select_Procedure()
		{
			//DataSet DS_Ret = new DataSet();
			string[] RunUser;

			try
			{
				RunUser =ComFunction.Set_UserInfo(ComVar.Log_Type.Write_File_DB);
				DS_Ret=  ComVar._WebSvc.Ora_Select_Procedure(RunUser,this.DS_Select);

				// --------------- DataSet Format----------------
				// DataSet 에는 복수개의 DataTable을 이용하여 호출 할 수 있으면 Return도 복수개임
				// < 호출시 전달 값 >
				// 1. RunUser : Set_UserInfo에서 설정하여 배열로 전달
				// 2. DS_Select : Select 문장이 있는 DataSet(복수개의 Procedure를 호출할수 있슴)
				//		1) DT_Select.TableName : 호출하고자 하는 Oracle Package 및 Procedure 명
				//		2) DT_Select.Column[0] : 칼럼명 -> "Parameter_Name",데이터 Type -> Type.GetType("System.String") , 프로시저 전달인자
				//		3) DT_Select.Column[1] : 칼럼명 -> "Parameter_Type",데이터 Type -> Type.GetType("System.Int32") , OracleType형의 Enum값
				//		4) DT_Select.Column[2] : 칼럼명 -> "Parameter_Value",데이터 Type -> Type.GetType("System.String") , 프로시저 전달값
				// 
				// < 리턴시 전달 값 >
				// 1. 정상 Return 값
				//		1) DataSet.DT.TableName : 호출한 Oracle Package 및 Procedure 명
				//		2) DataSet.DT.Columns	: 결과값의 데이터 필드
				//		3) DataSet.DT.Rows		: 결과값의 레코드
				// 2. 오류시 Return 값
				//		1) DataSet.DataSetName  : "ERROR"
				//		1) DataSet.DT.TableName : 호출한 Oracle Package 및 Procedure 명
				//		2) DataSet.DT.Columns	: 오류내용의 데이터 필드 Column[0].ColumnName = "Method", Column[1].ColumnName = "Error" , Column[2].ColumnName = "Date"
				//		3) DataSet.DT.Rows		: 오류의 내용

				//Return 값 처리
				if(DS_Ret.DataSetName =="ERROR")		// 오류가 Return
				{
					string err_msg = "";
					for(int i=0 ; i< DS_Ret.Tables.Count ;i++)
					{
						DataRow dr = DS_Ret.Tables[i].Rows[0];
						err_msg = err_msg + "Exec. Procedur :" + DS_Ret.Tables[i].TableName + " ,Method :" + dr["Method"].ToString() + "\n" ;
						err_msg = err_msg + "Error Message :" + dr["Error"].ToString() + "\n"  ;    
					}
					//MessageBox.Show( err_msg,"Oracle DataBase Process",MessageBoxButtons.OK,MessageBoxIcon.Error) ;
					return null;
					
				}
				else
				{
					return DS_Ret;
				}

			}
			catch(System.Threading.ThreadAbortException)
			{
				return null;
			}
			catch(Exception ex)
			{
				//MessageBox.Show( ex.Message,"Exe_Select_Procedure",MessageBoxButtons.OK,MessageBoxIcon.Error) ;
				return null;
			}

		}


		/// <summary>
		/// Exe_Run_Procedure : 복수개의 DataTable을 파라미터를 이용하여 프로시저를 실행
		/// </summary>
		/// <returns>정상 : DataSet ,오류 : null</returns>
		public DataSet Exe_Run_Procedure()
		{
			//DataSet DS_Ret = new DataSet();
			string[] RunUser;

			try
			{
				RunUser =ComFunction.Set_UserInfo(ComVar.Log_Type.Write_File_DB);
				DS_Ret=  ComVar._WebSvc.Ora_Run_Procedure(RunUser,this.DS_Run );

				// --------------- DataSet Format----------------
				// DataSet 에는 복수개의 DataTable을 이용하여 호출 할 수 있으면 Return도 복수개임
				// < 호출시 전달 값 >
				// 1. RunUser : Set_UserInfo에서 설정하여 배열로 전달
				// 2. DS_Run : Procedure 실행을 위한 DataSet(복수개의 Procedure를 호출할수 있슴)
				//		1) DT_Run.TableName : 호출하고자 하는 Oracle Package 및 Procedure 명
				//		2) DT_Run.Column[0] : 칼럼명 -> "Parameter_Name",데이터 Type -> Type.GetType("System.String") , 프로시저 전달인자
				//		3) DT_Run.Column[1] : 칼럼명 -> "Parameter_Type",데이터 Type -> Type.GetType("System.Int32") , OracleType형의 Enum값
				//		4) DT_Run.Column[2] : 칼럼명 -> "Parameter_Value",데이터 Type -> Type.GetType("System.String") , 프로시저 전달값
				// 
				// < 리턴시 전달 값 >
				// 1. 정상 Return 값
				//		1) DataSet.DT.TableName : 호출한 Oracle Package 및 Procedure 명
				//		2) DataSet.DT.Columns	: 결과값의 데이터 필드 Column[0].ColumnName = "Result"
				//		3) DataSet.DT.Rows[0]	: 결과값의 레코드  Row[0]= 처리결과값
				// 2. 오류시 Return 값
				//		1) DataSet.DataSetName  : "ERROR"
				//		1) DataSet.DT.TableName : 호출한 Oracle Package 및 Procedure 명
				//		2) DataSet.DT.Columns	: 오류내용의 데이터 필드 Column[0].ColumnName = "Method", Column[1].ColumnName = "Error" , Column[2].ColumnName = "Date"
				//		3) DataSet.DT.Rows		: 오류의 내용

				//Return 값 처리
				if(DS_Ret.DataSetName =="ERROR")		// 오류가 Return
				{
					string err_msg="" ;
					for(int i=0 ; i< DS_Ret.Tables.Count ;i++)
					{
						DataRow dr = DS_Ret.Tables[i].Rows[0];
						err_msg = err_msg + "Exec. Procedur :" + DS_Ret.Tables[i].TableName + " ,Method :" + dr["Method"].ToString() + "\n" ;
						err_msg = err_msg + "Error Message :" + dr["Error"].ToString() + "\n"  ;  
					}
					//MessageBox.Show( err_msg,"Oracle DataBase Process",MessageBoxButtons.OK,MessageBoxIcon.Error) ;
					return null;
						
				}
				else
				{

					return DS_Ret;
				}

			}
			catch(Exception ex)
			{
				//MessageBox.Show( ex.Message,"Exe_Select_Procedure",MessageBoxButtons.OK,MessageBoxIcon.Error) ;
				return null;
			}

		}




 




		/// <summary>
		/// Exe_Modify_Procedure : 복수개의 DataTable을 이용하여 많은 데이터를 저장
		/// </summary>
		/// <returns>정상 : DataSet ,오류 : null</returns>
		public DataSet Exe_Modify_Procedure()
		{
			//DataSet DS_Ret = new DataSet();
			string[] RunUser;

			try
			{
				RunUser =ComFunction.Set_UserInfo(ComVar.Log_Type.Write_File_DB);
				DS_Ret=  ComVar._WebSvc.Ora_Modify_Procedure (RunUser,this.DS_Modify);

				// --------------- DataSet Format----------------
				// DataSet 에는 복수개의 DataTable을 이용하여 호출 할 수 있으면 Return도 복수개임
				// < 호출시 전달 값 >
				// 1. RunUser : Set_UserInfo에서 설정하여 배열로 전달
				// 2. DS_Modify : 배열형태의 데이터를 저장하기 위한 DataSet(복수개의 Procedure를 호출할수 있슴)
				//		1) DT_Modify.TableName : 호출하고자 하는 Oracle Package 및 Procedure 명
				//		2) DT_Modify.Column[0...] : 칼럼명 -> 각 필드의 인자값[0...],데이터 Type -> Type.GetType("System.String") , 프로시저 전달인자
				//		3) DT_Modify.Row[0...] : 값이 있는 레코드
				// 
				// < 리턴시 전달 값 >
				// 1. 정상 Return 값
				//		1) DataSet.DT.TableName : 호출한 Oracle Package 및 Procedure 명
				//		2) DataSet.DT.Columns	: 결과값의 데이터 필드 Column[0].ColumnName = "Result"
				//		3) DataSet.DT.Rows		: 결과값의 레코드  Row[0]= 처리결과값
				// 2. 오류시 Return 값
				//		1) DataSet.DataSetName  : "ERROR"
				//		1) DataSet.DT.TableName : 호출한 Oracle Package 및 Procedure 명
				//		2) DataSet.DT.Columns	: 오류내용의 데이터 필드 Column[0].ColumnName = "Method", Column[1].ColumnName = "Error" , Column[2].ColumnName = "Date"
				//		3) DataSet.DT.Rows		: 오류의 내용

				//Return 값 처리
				if(DS_Ret.DataSetName =="ERROR")		// 오류가 Return
				{
					string err_msg="";
					for(int i=0 ; i< DS_Ret.Tables.Count ;i++)
					{
						DataRow dr = DS_Ret.Tables[i].Rows[0];
						err_msg = err_msg + "Exec. Procedur :" + DS_Ret.Tables[i].TableName + " ,Method :" + dr["Method"].ToString() + "\n" ;
						err_msg = err_msg + "Error Message :" + dr["Error"].ToString() + "\n"  ;  
					}
					//MessageBox.Show( err_msg,"Oracle DataBase Process",MessageBoxButtons.OK,MessageBoxIcon.Error) ;
					return null;
					
				}
				else
				{
					return DS_Ret;
				}

			}
			catch(Exception ex)
			{
				//MessageBox.Show( ex.Message,"Exe_Modify_Procedure",MessageBoxButtons.OK,MessageBoxIcon.Error) ;
				return null;
			}
		}



		/// <summary>
		/// Exe_Modify_Procedure : 복수개의 DataTable을 이용하여 많은 데이터를 저장
		/// </summary>
		/// <returns>정상 : DataSet ,오류 : null</returns>
		public bool Exe_Modify_Procedure_all()
		{
			string[] RunUser;
			
			try
			{
				RunUser =ComFunction.Set_UserInfo(ComVar.Log_Type.Write_File_DB);
				DS_Ret=  ComVar._WebSvc.Ora_Modify_Procedure (RunUser,this.DS_Modify);

				// --------------- DataSet Format----------------
				// DataSet 에는 복수개의 DataTable을 이용하여 호출 할 수 있으면 Return도 복수개임
				// < 호출시 전달 값 >
				// 1. RunUser : Set_UserInfo에서 설정하여 배열로 전달
				// 2. DS_Modify : 배열형태의 데이터를 저장하기 위한 DataSet(복수개의 Procedure를 호출할수 있슴)
				//		1) DT_Modify.TableName : 호출하고자 하는 Oracle Package 및 Procedure 명
				//		2) DT_Modify.Column[0...] : 칼럼명 -> 각 필드의 인자값[0...],데이터 Type -> Type.GetType("System.String") , 프로시저 전달인자
				//		3) DT_Modify.Row[0...] : 값이 있는 레코드
				// 
				// < 리턴시 전달 값 >
				// 1. 정상 Return 값
				//		1) DataSet.DT.TableName : 호출한 Oracle Package 및 Procedure 명
				//		2) DataSet.DT.Columns	: 결과값의 데이터 필드 Column[0].ColumnName = "Result"
				//		3) DataSet.DT.Rows		: 결과값의 레코드  Row[0]= 처리결과값
				// 2. 오류시 Return 값
				//		1) DataSet.DataSetName  : "ERROR"
				//		1) DataSet.DT.TableName : 호출한 Oracle Package 및 Procedure 명
				//		2) DataSet.DT.Columns	: 오류내용의 데이터 필드 Column[0].ColumnName = "Method", Column[1].ColumnName = "Error" , Column[2].ColumnName = "Date"
				//		3) DataSet.DT.Rows		: 오류의 내용

				//Return 값 처리
				if(DS_Ret.DataSetName =="ERROR")		// 오류가 Return
				{
					string err_msg="";
					for(int i=0 ; i< DS_Ret.Tables.Count ;i++)
					{
						DataRow dr = DS_Ret.Tables[i].Rows[0];
						err_msg = err_msg + "Exec. Procedur :" + DS_Ret.Tables[i].TableName + " ,Method :" + dr["Method"].ToString() + "\n" ;
						err_msg = err_msg + "Error Message :" + dr["Error"].ToString() + "\n"  ;  
					}
					//MessageBox.Show( err_msg,"Oracle DataBase Process",MessageBoxButtons.OK,MessageBoxIcon.Error) ;
					return false;
					
				}
				else
				{
					if (DS_Ret == null) return false;
					else return true;
				}

			}
			catch(Exception ex)
			{
				//MessageBox.Show( ex.Message,"Exe_Modify_Procedure",MessageBoxButtons.OK,MessageBoxIcon.Error) ;
				return false;
			}
		}


 
		// 2006 03 13 추가

		/// <summary>
		/// Exe_Modify_Procedure_Blob : Blob 데이터를 저장
		/// </summary>
		/// <returns>정상 : DataSet ,오류 : null</returns>
		public bool Exe_Modify_Procedure_Blob(byte[] BlobData)
		{ 

			try
			{ 
				bool ret =  ComVar._WebSvc.Ora_Run_Procedure_Blob (Process_Name, Parameter_Name, Parameter_Type, Parameter_Values, BlobData);

				 
				// < 리턴시 전달 값 >
				// 1. 정상 Return 값
				//		true
				// 2. 오류시 Return 값
				//		false

				return ret;


				/*
				//Return 값 처리
				if(DS_Ret.DataSetName =="ERROR")		// 오류가 Return
				{
					string err_msg="";
					for(int i=0 ; i< DS_Ret.Tables.Count ;i++)
					{
						DataRow dr = DS_Ret.Tables[i].Rows[0];
						err_msg = err_msg + "Exec. Procedur :" + DS_Ret.Tables[i].TableName + " ,Method :" + dr["Method"].ToString() + "\n" ;
						err_msg = err_msg + "Error Message :" + dr["Error"].ToString() + "\n"  ;  
					}
					MessageBox.Show( err_msg,"Oracle DataBase Process",MessageBoxButtons.OK,MessageBoxIcon.Error) ;
					return null;
					
				}
				else
				{
					return DS_Ret;
				}
				*/


			}
			catch(Exception ex)
			{
				//MessageBox.Show( ex.Message,"Exe_Modify_Procedure_Blob",MessageBoxButtons.OK,MessageBoxIcon.Error) ;
				return false;
			}
		}



		/// <summary>
		/// Exe_Select_Query : 1개의 Query 문장으로 호출
		/// </summary>
		/// <param name="SqlTxt"> Query 문장</param>
		/// <returns>정상 : DataSet ,오류 : null</returns>
		public DataSet Exe_Select_Query(string SqlTxt)
		{
			//DataSet DS_Ret = new DataSet();
			string[] RunUser;

			try
			{
				RunUser =ComFunction.Set_UserInfo(ComVar.Log_Type.Write_File_DB);
				DS_Ret=  ComVar._WebSvc.Ora_Select(RunUser,SqlTxt);

				// --------------- DataSet Format----------------
				// 단일 Sql Query 문장을 전송하여 DataSet의 결과값 Return
				// < 호출시 전달 값 >
				// 1. RunUser : Set_UserInfo에서 설정하여 배열로 전달
				// 2. SqlTxt : 한개의 Select Sql문장
				// 
				// < 리턴시 전달 값 >
				// 1. 정상 Return 값
				//		1) DataSet.DT.TableName : 호출한 Oracle Package 및 Procedure 명
				//		2) DataSet.DT.Columns	: 결과값의 데이터 필드 Column[0].ColumnName = "Result"
				//		3) DataSet.DT.Rows		: 결과값의 레코드  Row[0]= 처리결과값
				// 2. 오류시 Return 값
				//		1) DataSet.DataSetName  : "ERROR"
				//		1) DataSet.DT.TableName : 호출한 Oracle Package 및 Procedure 명
				//		2) DataSet.DT.Columns	: 오류내용의 데이터 필드 Column[0].ColumnName = "Method", Column[1].ColumnName = "Error" , Column[2].ColumnName = "Date"
				//		3) DataSet.DT.Rows		: 오류의 내용

				//Return 값 처리
				if(DS_Ret.DataSetName =="ERROR")		// 오류가 Return
				{
					string err_msg="";
					for(int i=0 ; i< DS_Ret.Tables.Count ;i++)
					{
						DataRow dr = DS_Ret.Tables[i].Rows[0];
						err_msg = err_msg + "Exec. Procedur :" + DS_Ret.Tables[i].TableName + " ,Method :" + dr["Method"].ToString() + "\n" ;
						err_msg = err_msg + "Error Message :" + dr["Error"].ToString() + "\n"  ;  
					}
					//MessageBox.Show( err_msg,"Oracle DataBase Process",MessageBoxButtons.OK,MessageBoxIcon.Error) ;
					return null;
					
				}
				else
				{
					return DS_Ret;
				}

			}
			catch(Exception ex)
			{
				//MessageBox.Show( ex.Message,"Exe_Modify_Procedure",MessageBoxButtons.OK,MessageBoxIcon.Error) ;
				return null;
			}


		}


		/// <summary>
		/// Save_FlexGird : 그리드에 있는 내용을 저장
		/// </summary>
		/// <param name="arg_proc_name">프로세스 이름</param>
		/// <param name="arg_fgrid">대상 그리드</param>
		/// <returns>정상 : true , 오류 : false </returns>
		public bool Save_FlexGird(string arg_proc_name, COM.FSP arg_fgrid)
		{
			int col_ct = arg_fgrid.Cols.Count-1;		// 칼럼의 수
			int row_fixed = arg_fgrid.Rows.Fixed;		// 그리드 고정행 값
			int save_ct =0 ;							// 저장 행 수

			int i;
			int para_ct =0;								// 파라미터 값의 저장 배열의 수
			int row,col;

			try
			{
				this.ReDim_Parameter(col_ct);
				this.Process_Name = arg_proc_name;

				// 파라미터 이름 설정
				this.Parameter_Name[0] = "ARG_DIVISION";
				for(i = 1; i < col_ct; i++)
				{
					this.Parameter_Name[i] = "ARG_" + arg_fgrid[0, i].ToString(); 
				}

				// 파라미터의 데이터 Type
				for(i = 0; i < col_ct ; i++)
				{
					this.Parameter_Type[i] = (int)OracleType.VarChar  ; 
				}
	
				// 저장 행 수 구하기
				for(i = row_fixed ; i < arg_fgrid.Rows.Count; i++)
				{
					if(arg_fgrid[i, 0] == null) continue;

					if(arg_fgrid[i, 0].ToString() != "")
					{
						save_ct += 1;
					}
				}
			
				// 파라미터 값에 저장할 배열
				this.Parameter_Values  = new string[col_ct * save_ct ];


				// 각 행의 변경값 Setting
				for(row = row_fixed; row < arg_fgrid.Rows.Count ; row++)
				{
					if(arg_fgrid[row, 0] == null) continue;

					if(arg_fgrid[row, 0].ToString() != "")
					{ 
						for(col = 0; col < col_ct ; col++)	// 각 열의 값 Setting
						{  

							//데이터 체크
							if(arg_fgrid.arr_essential[col] == "TRUE" && (arg_fgrid[row,col] == null || arg_fgrid[row,col].ToString() == "") )
								//******************  							
							{
								COM.ComFunction.User_Message("Essential Input - " + arg_fgrid[arg_fgrid.Rows.Fixed,col].ToString() );
								arg_fgrid.LeftCol = col;
								return false ;
							}


							// 데이터값 설정 
							if(arg_fgrid.Cols[col].Style.DataType != null
								&& arg_fgrid.Cols[col].DataType.Equals(typeof(bool)) )
							{ 
								arg_fgrid[row, col] = (arg_fgrid[row, col] == null) ? "False" : arg_fgrid[row, col].ToString();
								this.Parameter_Values[para_ct] = (arg_fgrid[row,col].ToString() == "True") ? "Y" : "N"; 

								para_ct ++;
							}
								//콤보리스트 처리 추가
							
							else if(arg_fgrid.Cols[col].ComboList.Length != 0)
							{
								char[] delimiter = ":".ToCharArray();
								string[] token = null; 

								token = arg_fgrid[row,col].ToString().Split(delimiter); 
								this.Parameter_Values[para_ct] = (token[0] == null) ? "" : token[0].Trim();
 
								para_ct ++;
							}
								//추가(사용자업데이트위해서)
							else if(arg_fgrid[0, (col == 0) ? 1 : col].ToString() == "UPD_USER")
							{
								this.Parameter_Values[para_ct] = ComVar.This_User ;
								para_ct ++;
							}
							else
							{ 
								this.Parameter_Values[para_ct] = (arg_fgrid[row, col] == null) ? "" : arg_fgrid[row,col].ToString();
								para_ct ++;
							}			
						} 
					}
				}

				//****************** 박지수 수정분  							
				this.Add_Modify_Parameter(true);						// 파라미터 데이터를 DataSet에 추가
				DataSet ds_Set = this.Exe_Modify_Procedure();			// Modify Procedure 실행
				
				if (ds_Set == null) return false;
				else return true;
				//******************   						
			}
			catch(Exception ex)
			{
				//MessageBox.Show( ex.Message,"Save_FlexGird",MessageBoxButtons.OK,MessageBoxIcon.Error) ;
				return false;
			}
  
		}

		/// <summary>
		/// Save_FlexGird : 그리드에 있는 내용을 저장
		/// </summary>
		/// <param name="arg_proc_name">프로세스 이름</param>
		/// <param name="arg_fgrid">대상 그리드</param>
		/// <returns>정상 : true , 오류 : false </returns>
		public bool Save_FlexGird(string arg_div , string arg_proc_name, COM.FSP arg_fgrid)
		{
			int col_ct = arg_fgrid.Cols.Count;		// 칼럼의 수
			int row_fixed = arg_fgrid.Rows.Fixed;		// 그리드 고정행 값
			int save_ct =0 ;							// 저장 행 수

			int i;
			int para_ct =0;								// 파라미터 값의 저장 배열의 수
			int row,col;

			try
			{
				this.ReDim_Parameter(col_ct);
				this.Process_Name = arg_proc_name;

				// 파라미터 이름 설정
				this.Parameter_Name[0] = "ARG_DIVISION";
				for(i = 1; i < col_ct; i++)
				{
					this.Parameter_Name[i] = "ARG_" + arg_fgrid[0, i].ToString(); 
				}

				// 파라미터의 데이터 Type
				for(i = 0; i < col_ct ; i++)
				{
					this.Parameter_Type[i] = (int)OracleType.VarChar  ; 
				}
	
				// 저장 행 수 구하기
				for(i = row_fixed ; i < arg_fgrid.Rows.Count; i++)
				{
					if(arg_fgrid[i, 0] == null) continue;

					if(arg_fgrid[i, 0].ToString() != "")
					{
						save_ct += 1;
					}
				}
			
				// 파라미터 값에 저장할 배열
				this.Parameter_Values  = new string[col_ct * save_ct ];


				// 각 행의 변경값 Setting
				for(row = row_fixed; row < arg_fgrid.Rows.Count ; row++)
				{
					if(arg_fgrid[row, 0] == null) continue;

					if(arg_fgrid[row, 0].ToString() != "")
					{ 
						for(col = 0; col < col_ct ; col++)	// 각 열의 값 Setting
						{

							//데이터 체크
							if(arg_fgrid.arr_essential[col] == "TRUE" && (arg_fgrid[row,col] == null || arg_fgrid[row,col].ToString() == "") )
								//******************  							
							{
								COM.ComFunction.User_Message("Essential Input - " + arg_fgrid[arg_fgrid.Rows.Fixed,col].ToString() );
								arg_fgrid.LeftCol = col;
								return false ;
							}

						
							// 데이터값 설정
							//if(arg_fgrid.Cols[col].Style.Name == "CHECKBOX")
							if(arg_fgrid.Cols[col].Style.DataType != null
								&& arg_fgrid.Cols[col].DataType.Equals(typeof(bool)) )
							{
								//if(arg_fgrid[row,col] == null) arg_fgrid[row,col] = false ;
								arg_fgrid[row, col] = (arg_fgrid[row, col] == null) ? "False" : arg_fgrid[row, col].ToString();
								this.Parameter_Values[para_ct] = (arg_fgrid[row,col].ToString() == "True") ? "Y" : "N"; 

								para_ct ++;
							}
								//콤보리스트 처리 추가
							else if(arg_fgrid.Cols[col].ComboList.Length != 0)
							{
								char[] delimiter = ":".ToCharArray();
								string[] token = null; 

								arg_fgrid[row,col] = (arg_fgrid[row,col] == null) ? "" : arg_fgrid[row,col].ToString();
								token = arg_fgrid[row,col].ToString().Split(delimiter); 
								this.Parameter_Values[para_ct] = (token[0] == null) ? "" : token[0].Trim();
								//this.Parameter_Values[para_ct] = ComFunction.Empty_String(arg_fgrid[row, col].ToString()," ");
 
								para_ct ++;
							}
							else
							{   
								//사용자/등록일 
								if (col  == col_ct -2) 
								{
									this.Parameter_Values[para_ct] = ComVar.This_User; 
								}
								else if (col  == col_ct -1) 
								{
									this.Parameter_Values[para_ct] = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
								}
								else
								{
									this.Parameter_Values[para_ct] = (arg_fgrid[row, col] == null) ? "" : arg_fgrid[row,col].ToString().ToUpper();
									//this.Parameter_Values[para_ct] = ComFunction.Empty_String(arg_fgrid[row, col].ToString()," ");
								}

								para_ct ++;

							}
							
							
 

						} //end for



					}
				}

				this.Add_Modify_Parameter(true);		// 파라미터 데이터를 DataSet에 추가
				this.Exe_Modify_Procedure();			// Modify Procedure 실행
				
				return true;

			}
			catch(Exception ex)
			{
				//MessageBox.Show( ex.Message,"Save_FlexGird",MessageBoxButtons.OK,MessageBoxIcon.Error) ;
				return false;
			}
  
		}



		/// <summary>
		/// Save_FlexGird : 그리드에 있는 내용을 저장 (2005.11.30 우효동추가)
		/// </summary>
		/// <param name="arg_proc_name">프로세스 이름</param>
		/// <param name="arg_fgrid">대상 그리드</param>
		/// <param name="arg_fgrid">저장 칼럼수</param>
		/// <returns>정상 : true , 오류 : false </returns>
		public bool Save_FlexGird(string arg_proc_name, COM.FSP arg_fgrid,int save_col)
		{
			int col_ct = save_col;//arg_fgrid.Cols.Count-1;		// 칼럼의 수
			int row_fixed = arg_fgrid.Rows.Fixed;		// 그리드 고정행 값
			int save_ct =0 ;							// 저장 행 수

			int i;
			int para_ct =0;								// 파라미터 값의 저장 배열의 수
			int row,col;

			try
			{
				this.ReDim_Parameter(col_ct);
				this.Process_Name = arg_proc_name;

				// 파라미터 이름 설정
				this.Parameter_Name[0] = "ARG_DIVISION";
				for(i = 1; i < col_ct; i++)
				{
					this.Parameter_Name[i] = "ARG_" + arg_fgrid[0, i].ToString(); 
				}

				// 파라미터의 데이터 Type
				for(i = 0; i < col_ct ; i++)
				{
					this.Parameter_Type[i] = (int)OracleType.VarChar  ; 
				}
	
				// 저장 행 수 구하기
				for(i = row_fixed ; i < arg_fgrid.Rows.Count; i++)
				{
					if(arg_fgrid[i, 0] == null) continue;

					if(arg_fgrid[i, 0].ToString() != "")
					{
						save_ct += 1;
					}
				}
			
				// 파라미터 값에 저장할 배열
				this.Parameter_Values  = new string[col_ct * save_ct ];


				// 각 행의 변경값 Setting
				for(row = row_fixed; row < arg_fgrid.Rows.Count ; row++)
				{
					if(arg_fgrid[row, 0] == null) continue;

					if(arg_fgrid[row, 0].ToString() != "")
					{ 
						for(col = 0; col < col_ct ; col++)	// 각 열의 값 Setting
						{  

							//데이터 체크
							if(arg_fgrid.arr_essential[col] == "TRUE" && (arg_fgrid[row,col] == null || arg_fgrid[row,col].ToString() == "") )
								//******************  							
							{
								COM.ComFunction.User_Message("Essential Input - " + arg_fgrid[arg_fgrid.Rows.Fixed,col].ToString() );
								arg_fgrid.LeftCol = col;
								return false ;
							}


							// 데이터값 설정														
							if(arg_fgrid.Cols[col].Style.DataType != null
								&& arg_fgrid.Cols[col].DataType.Equals(typeof(bool)) )
							{
								//if(arg_fgrid[row,col] == null) arg_fgrid[row,col] = false ;
								arg_fgrid[row, col] = (arg_fgrid[row, col] == null) ? "False" : arg_fgrid[row, col].ToString();
								this.Parameter_Values[para_ct] = (arg_fgrid[row,col].ToString() == "True") ? "Y" : "N"; 

								para_ct ++;
							}

								//콤보리스트 처리 추가
							else if(arg_fgrid.Cols[col].ComboList.Length != 0)
							{
								char[] delimiter = ":".ToCharArray();
								string[] token = null; 

								token = arg_fgrid[row,col].ToString().Split(delimiter); 
								this.Parameter_Values[para_ct] = (token[0] == null) ? "" : token[0].Trim();
 
								para_ct ++;
							}
								//추가(사용자업데이트위해서)
							else if(arg_fgrid[0, (col==0)?1:col].ToString() == "UPD_USER")
							{
								this.Parameter_Values[para_ct] = ComVar.This_User ;
								para_ct ++;
							}

							else
							{
								this.Parameter_Values[para_ct] = (arg_fgrid[row, col] == null) ? "" : arg_fgrid[row,col].ToString();
								para_ct ++;
							}			
						} 
					}
				}

				this.Add_Modify_Parameter(true);		// 파라미터 데이터를 DataSet에 추가
				this.Exe_Modify_Procedure();			// Modify Procedure 실행
				
				return true;

			}
			catch(Exception ex)
			{
				//MessageBox.Show( ex.Message,"Save_FlexGird",MessageBoxButtons.OK,MessageBoxIcon.Error) ;
				return false;
			}
  
		}


		/// <summary>
		/// Save_FlexGird : 그리드에 있는 내용을 저장
		/// </summary>
		/// <param name="arg_proc_name">프로세스 이름</param>
		/// <param name="arg_fgrid">대상 그리드</param>
		/// <returns>정상 : true , 오류 : false </returns>
		public bool Save_FlexGird_Ready(string arg_proc_name, COM.FSP arg_fgrid, bool arg_clear)
		{
			int col_ct = arg_fgrid.Cols.Count-1;		// 칼럼의 수
			int row_fixed = arg_fgrid.Rows.Fixed;		// 그리드 고정행 값
			int save_ct =0 ;							// 저장 행 수

			int i;
			int para_ct =0;								// 파라미터 값의 저장 배열의 수
			int row,col;

			try
			{
				this.ReDim_Parameter(col_ct);
				this.Process_Name = arg_proc_name;

				// 파라미터 이름 설정
				this.Parameter_Name[0] = "ARG_DIVISION";
				for(i = 1; i < col_ct; i++)
				{
					this.Parameter_Name[i] = "ARG_" + arg_fgrid[0, i].ToString(); 
				}

				// 파라미터의 데이터 Type
				for(i = 0; i < col_ct ; i++)
				{
					this.Parameter_Type[i] = (int)OracleType.VarChar  ; 
				}
	
				// 저장 행 수 구하기
				for(i = row_fixed ; i < arg_fgrid.Rows.Count; i++)
				{
					if(arg_fgrid[i, 0] == null) continue;

					if(arg_fgrid[i, 0].ToString() != "")
					{
						save_ct += 1;
					}
				}
			
				// 파라미터 값에 저장할 배열
				this.Parameter_Values  = new string[col_ct * save_ct ];


				// 각 행의 변경값 Setting
				for(row = row_fixed; row < arg_fgrid.Rows.Count ; row++)
				{
					if(arg_fgrid[row, 0] == null) continue;

					if(arg_fgrid[row, 0].ToString() != "")
					{ 
						for(col = 0; col < col_ct ; col++)	// 각 열의 값 Setting
						{  

							//데이터 체크
							if(arg_fgrid.arr_essential[col] == "TRUE" && (arg_fgrid[row,col] == null || arg_fgrid[row,col].ToString() == "") )
								//******************  							
							{
								COM.ComFunction.User_Message("Essential Input - " + arg_fgrid[arg_fgrid.Rows.Fixed,col].ToString() );
								return false ;
							}


							// 데이터값 설정 
							if(arg_fgrid.Cols[col].Style.DataType != null
								&& arg_fgrid.Cols[col].DataType.Equals(typeof(bool)) )
							{ 
								arg_fgrid[row, col] = (arg_fgrid[row, col] == null) ? "False" : arg_fgrid[row, col].ToString();
								this.Parameter_Values[para_ct] = (arg_fgrid[row,col].ToString() == "True") ? "Y" : "N"; 

								para_ct ++;
							}
								//콤보리스트 처리 추가
							
							else if(arg_fgrid.Cols[col].ComboList.Length != 0)
							{
								char[] delimiter = ":".ToCharArray();
								string[] token = null; 

								token = arg_fgrid[row,col].ToString().Split(delimiter); 
								this.Parameter_Values[para_ct] = (token[0] == null) ? "" : token[0].Trim();
 
								para_ct ++;
							}
								//추가(사용자업데이트위해서)
							else if(arg_fgrid[0, (col == 0) ? 1 : col].ToString() == "UPD_USER")
							{
								this.Parameter_Values[para_ct] = ComVar.This_User ;
								para_ct ++;
							}
							else
							{ 
								this.Parameter_Values[para_ct] = (arg_fgrid[row, col] == null) ? "" : arg_fgrid[row,col].ToString();
								para_ct ++;
							}			
						} 
					}
				}

				this.Add_Modify_Parameter(arg_clear);						// 파라미터 데이터를 DataSet에 추가
				return true;
			}
			catch(Exception ex)
			{
				//MessageBox.Show( ex.Message,"Save_FlexGird",MessageBoxButtons.OK,MessageBoxIcon.Error) ;
				return false;
			}  
		}



		/// <summary>
		/// Save_FlexGird_CrossTab : 크로스탭 그리드에 있는 내용을 저장
		/// </summary>
		/// <param name="arg_proc_name">프로세스 이름</param>
		/// <param name="arg_fgrid">대상 그리드</param>
		/// <returns>정상 : true , 오류 : false </returns>
		public bool Save_FlexGird_CrossTab(string arg_proc_name, C1FlexGrid arg_fgrid, int arg_crs_start, string arg_col_nm1,string arg_col_nm2)
		{
			int col_ct = arg_fgrid.Cols.Count;		// 칼럼의 수
			int row_fixed = arg_fgrid.Rows.Fixed;		// 그리드 고정행 값
			int save_ct =0 ;							// 저장 행 수

			int i;
			int para_ct =0;								// 파라미터 값의 저장 배열의 수
			int row,col,crs;

			try
			{
				this.ReDim_Parameter(arg_crs_start+2);
				this.Process_Name = arg_proc_name;

				// 파라미터 이름 설정
				this.Parameter_Name[0] = "ARG_DIVISION";
				for(i = 1; i < arg_crs_start; i++)
				{
					this.Parameter_Name[i] = "ARG_" + arg_fgrid[0, i].ToString(); 
				}
				this.Parameter_Name[arg_crs_start]   = arg_col_nm1; 
				this.Parameter_Name[arg_crs_start+1] = arg_col_nm2; 

				// 파라미터의 데이터 Type
				for(i = 0; i < arg_crs_start ; i++)
				{
					this.Parameter_Type[i] = (int)OracleType.VarChar  ; 
				}
				this.Parameter_Type[arg_crs_start]   = (int)OracleType.VarChar  ;
				this.Parameter_Type[arg_crs_start+1] = (int)OracleType.VarChar  ;

	
				// 저장 행 수 구하기
				for(i = row_fixed ; i < arg_fgrid.Rows.Count; i++)
				{
					if(arg_fgrid[i, 0] == null) continue;

					if(arg_fgrid[i, 0].ToString() != "")
					{
						save_ct += 1;
					}
				}
			
				// 파라미터 값에 저장할 배열
				this.Parameter_Values  = new string[(arg_crs_start+2) * save_ct * (col_ct - arg_crs_start) ];


				// 각 행의 변경값 Setting
				for(row = row_fixed; row < arg_fgrid.Rows.Count ; row++)
				{
					if(arg_fgrid[i, 0] == null) continue;

					if(arg_fgrid[row, 0].ToString() != "")
					{ 
						for(crs = arg_crs_start; crs < arg_fgrid.Cols.Count; crs++)
						{
							for(col = 0; col < arg_crs_start ; col++)	// 각 열의 값 Setting
							{  
								// 데이터값 설정														
								if(arg_fgrid.Cols[col].Style.DataType != null
									&& arg_fgrid.Cols[col].DataType.Equals(typeof(bool)) )
								{
									//if(arg_fgrid[row,col] == null) arg_fgrid[row,col] = false ;
									arg_fgrid[row, col] = (arg_fgrid[row, col] == null) ? "False" : arg_fgrid[row, col].ToString();
									this.Parameter_Values[para_ct] = (arg_fgrid[row,col].ToString() == "True") ? "Y" : "N"; 

									para_ct ++;
								}

									//콤보리스트 처리 추가
								else if(arg_fgrid.Cols[col].ComboList.Length != 0)
								{
									char[] delimiter = ":".ToCharArray();
									string[] token = null; 

									token = arg_fgrid[row,col].ToString().Split(delimiter); 
									this.Parameter_Values[para_ct] = (token[0] == null) ? "" : token[0].Trim();
	 
									para_ct ++;
								}
									//추가(사용자업데이트위해서)
								else if(arg_fgrid[0, (col==0)?1:col].ToString() == "UPD_USER")
								{
									this.Parameter_Values[para_ct] = ComVar.This_User ;
									para_ct ++;
								}

								else
								{
									this.Parameter_Values[para_ct] = (arg_fgrid[row, col] == null) ? "" : arg_fgrid[row,col].ToString();
									para_ct ++;
								}			
							}
							
							this.Parameter_Values[para_ct] = arg_fgrid[1, crs].ToString() ;
							para_ct ++;

							this.Parameter_Values[para_ct] = (arg_fgrid[row, crs] == null) ? "" : arg_fgrid[row,crs].ToString(); ;
							para_ct ++;
						}
					}
				}

				this.Add_Modify_Parameter(true);		// 파라미터 데이터를 DataSet에 추가
				this.Exe_Modify_Procedure();			// Modify Procedure 실행
				
				return true;

			}
			catch(Exception ex)
			{
				//MessageBox.Show( ex.Message,"Save_FlexGird",MessageBoxButtons.OK,MessageBoxIcon.Error) ;
				return false;
			}
  
		}


		/// <summary>
		/// Save_FlexGird_CrossTab_Last : 크로스탭 그리드의 ROW의 마지막 칼럼이면 ARG_DIV 수정
		/// </summary>
		/// <param name="arg_proc_name">프로세스 이름</param>
		/// <param name="arg_fgrid">대상 그리드</param>
		/// <returns>정상 : true , 오류 : false </returns>
		public bool Save_FlexGird_CrossTab_Last(string arg_proc_name, C1FlexGrid arg_fgrid, int arg_crs_start, string arg_col_nm1,string arg_col_nm2)
		{
			int col_ct = arg_fgrid.Cols.Count;		// 칼럼의 수
			int row_fixed = arg_fgrid.Rows.Fixed;		// 그리드 고정행 값
			int save_ct =0 ;							// 저장 행 수

			int i;
			int para_ct =0;								// 파라미터 값의 저장 배열의 수
			int row,col,crs;

			try
			{
				this.ReDim_Parameter(arg_crs_start+2);
				this.Process_Name = arg_proc_name;

				// 파라미터 이름 설정
				this.Parameter_Name[0] = "ARG_DIVISION";
				for(i = 1; i < arg_crs_start; i++)
				{
					this.Parameter_Name[i] = "ARG_" + arg_fgrid[0, i].ToString(); 
				}
				this.Parameter_Name[arg_crs_start]   = arg_col_nm1; 
				this.Parameter_Name[arg_crs_start+1] = arg_col_nm2; 

				// 파라미터의 데이터 Type
				for(i = 0; i < arg_crs_start ; i++)
				{
					this.Parameter_Type[i] = (int)OracleType.VarChar  ; 
				}
				this.Parameter_Type[arg_crs_start]   = (int)OracleType.VarChar  ;
				this.Parameter_Type[arg_crs_start+1] = (int)OracleType.VarChar  ;

	
				// 저장 행 수 구하기
				for(i = row_fixed ; i < arg_fgrid.Rows.Count; i++)
				{
					if(arg_fgrid[i, 0] == null) 
						arg_fgrid[i, 0] = "";
					if(arg_fgrid[i, 0].ToString() != "")
						save_ct += 1;
				}
			
				// 파라미터 값에 저장할 배열
				this.Parameter_Values  = new string[(arg_crs_start+2) * save_ct * (col_ct - arg_crs_start) ];


				// 각 행의 변경값 Setting
				for(row = row_fixed; row < arg_fgrid.Rows.Count ; row++)
				{
					if(arg_fgrid[row, 0].ToString() != "")
					{ 
						for(crs = arg_crs_start; crs < arg_fgrid.Cols.Count; crs++)
						{
							for(col = 0; col < arg_crs_start ; col++)	// 각 열의 값 Setting
							{  
								// 데이터값 설정														
								if(arg_fgrid.Cols[col].Style.DataType != null
									&& arg_fgrid.Cols[col].DataType.Equals(typeof(bool)) )
								{
									//if(arg_fgrid[row,col] == null) arg_fgrid[row,col] = false ;
									arg_fgrid[row, col] = (arg_fgrid[row, col] == null) ? "False" : arg_fgrid[row, col].ToString();
									this.Parameter_Values[para_ct] = (arg_fgrid[row,col].ToString() == "True") ? "Y" : "N"; 

									para_ct ++;
								}

									//콤보리스트 처리 추가
								else if(arg_fgrid.Cols[col].ComboList.Length != 0)
								{
									char[] delimiter = ":".ToCharArray();
									string[] token = null; 

									token = arg_fgrid[row,col].ToString().Split(delimiter); 
									this.Parameter_Values[para_ct] = (token[0] == null) ? "" : token[0].Trim();
	 
									para_ct ++;
								}
									//추가(사용자업데이트위해서)
								else if(arg_fgrid[0, (col==0)?1:col].ToString() == "UPD_USER")
								{
									this.Parameter_Values[para_ct] = ComVar.This_User ;
									para_ct ++;
								}

								else
								{
									if(col == 0 && crs == arg_fgrid.Cols.Count-1) //ROW의 마지막 셋팅
									{
										this.Parameter_Values[para_ct] = (arg_fgrid[row, col] == null) ? "X" : arg_fgrid[row,col].ToString()+"X" ;
										para_ct ++;
									}
									else
									{
										this.Parameter_Values[para_ct] = (arg_fgrid[row, col] == null) ? "" : arg_fgrid[row,col].ToString();
										para_ct ++;
									}

								}			
							}
							
							this.Parameter_Values[para_ct] = arg_fgrid[1,crs].ToString() ;
							para_ct ++;

							this.Parameter_Values[para_ct] = (arg_fgrid[row, crs] == null) ? "" : arg_fgrid[row,crs].ToString(); ;
							para_ct ++;
						}
					}
				}

				this.Add_Modify_Parameter(true);		// 파라미터 데이터를 DataSet에 추가
				this.Exe_Modify_Procedure();			// Modify Procedure 실행
				
				return true;

			}
			catch(Exception ex)
			{
				//MessageBox.Show( ex.Message,"Save_FlexGird",MessageBoxButtons.OK,MessageBoxIcon.Error) ;
				return false;
			}
  
		}




		/// <summary>
		/// Save_FlexGird : 그리드에 있는 내용을 저장	//조남숙//hemos
		/// </summary>
		/// <param name="arg_proc_name">프로세스 이름</param>
		/// <param name="arg_fgrid">대상 그리드</param>
		/// <returns>정상 : true , 오류 : false </returns>
		public bool Save_FlexGird_Tree(string arg_proc_name, C1FlexGrid arg_fgrid)
		{
			int col_ct = arg_fgrid.Cols.Count-1;		// 칼럼의 수
			int row_fixed = arg_fgrid.Rows.Fixed+1;		// 그리드 고정행 값
			int save_ct =0 ;							// 저장 행 수
			string s ;

			int i;
			int para_ct =0;								// 파라미터 값의 저장 배열의 수
			int row,col;

			try
			{
				this.ReDim_Parameter(col_ct);
				this.Process_Name = arg_proc_name;

				// 파라미터 이름 설정
				this.Parameter_Name[0] = "ARG_DIVISION";
				for(i = 1; i < col_ct; i++)
				{
					this.Parameter_Name[i] = "ARG_" + arg_fgrid[0, i].ToString(); 
				}

				// 파라미터의 데이터 Type
				for(i = 0; i < col_ct ; i++)
				{
					this.Parameter_Type[i] = (int)OracleType.VarChar  ; 
				}
	
				// 저장 행 수 구하기
				for(i = row_fixed ; i < arg_fgrid.Rows.Count; i++)
				{
					if(arg_fgrid[i, 0] == null) continue;

					if((string)arg_fgrid[i, 0] != "")  //이정한 수정
					{
						save_ct += 1;
					}
				}
			
				// 파라미터 값에 저장할 배열
				this.Parameter_Values  = new string[col_ct * save_ct ];


				// 각 행의 변경값 Setting
				for(row = row_fixed; row < arg_fgrid.Rows.Count ; row++)
				{
					if(arg_fgrid[i, 0] == null) continue;

					if((string)arg_fgrid[row, 0] != "")    //이정한 수정
					{ 
						for(col = 0; col < col_ct ; col++)	// 각 열의 값 Setting
						{  
							// 데이터값 설정														
							if(arg_fgrid.Cols[col].Style.DataType != null
								&& arg_fgrid.Cols[col].DataType.Equals(typeof(bool)) )
							{
								//if(arg_fgrid[row,col] == null) arg_fgrid[row,col] = false ;
								arg_fgrid[row, col] = (arg_fgrid[row, col] == null) ? "False" : arg_fgrid[row, col].ToString();
								this.Parameter_Values[para_ct] = (arg_fgrid[row,col].ToString() == "True") ? "Y" : "N"; 

								para_ct ++;
							}

								//콤보리스트 처리 추가
							else if(arg_fgrid.Cols[col].ComboList.Length != 0)
							{
								char[] delimiter = ":".ToCharArray();
								string[] token = null; 
								
								//이정한 수정
								s = (arg_fgrid[row, col] == null) ? "" : arg_fgrid[row,col].ToString();
								token = s.Split(delimiter); 
								this.Parameter_Values[para_ct] = (token[0] == null) ? "" : token[0].Trim();
 
								para_ct ++;
							}
								//추가(사용자업데이트위해서)
							else if(arg_fgrid[0, (col==0)?1:col].ToString() == "UPD_USER")
							{
								this.Parameter_Values[para_ct] = ComVar.This_User ;
								para_ct ++;
							}

							else
							{
								this.Parameter_Values[para_ct] = (arg_fgrid[row, col] == null) ? "" : arg_fgrid[row,col].ToString();
								para_ct ++;
							}			
						} 
					}
				}

				this.Add_Modify_Parameter(true);		// 파라미터 데이터를 DataSet에 추가
				this.Exe_Modify_Procedure();			// Modify Procedure 실행
				
				return true;

			}
			catch(Exception ex)
			{
				//MessageBox.Show( ex.Message,"Save_FlexGird",MessageBoxButtons.OK,MessageBoxIcon.Error) ;
				return false;
			}
  
		}





//		/// <summary>
//		/// Save_Spread : 스프레드에 있는 내용을 저장
//		/// </summary>
//		/// <param name="arg_proc_name">프로세스 이름</param>
//		/// <param name="arg_fgrid">대상 스프레드</param>
//		/// <returns>정상 : true , 오류 : false </returns>
//		public bool Save_Spread(string arg_proc_name, COM.SSP arg_fgrid)
//		{
//			int col_ct = arg_fgrid.Sheets[0].ColumnCount-1;	           // 칼럼의 수
//			int row_fixed = arg_fgrid.Sheets[0].RowHeader.Rows.Count ; // 그리드 고정행 값
//			int save_ct =0 ;							               // 저장 행 수
//
//			int i;
//			int para_ct =0;								               // 파라미터 값의 저장 배열의 수
//			int row,col;
//			string s;
//
//			try
//			{
//				this.ReDim_Parameter(col_ct);
//				this.Process_Name = arg_proc_name;
//
//				// 파라미터 이름 설정
//				this.Parameter_Name[0] = "ARG_DIVISION";
//				for(i = 1; i < col_ct; i++)
//				{
//					this.Parameter_Name[i] = "ARG_" + arg_fgrid.Sheets[0].ColumnHeader.Cells[0,i].Value.ToString(); 
//				}
//
//				// 파라미터의 데이터 Type
//				for(i = 1; i < col_ct ; i++)
//				{
//					this.Parameter_Type[i] = (int)OracleType.VarChar  ; 
//				}
//	
//				// 저장 행 수 구하기
//				for(i = 0 ; i < arg_fgrid.Sheets[0].Rows.Count; i++)
//				{
//					s = (arg_fgrid.Sheets[0].Cells[i,0].Tag == null) ? "" : arg_fgrid.Sheets[0].Cells[i,0].Tag.ToString();
//					if( s != "")
//					{
//						save_ct += 1;						
//					}
//				}
//			
//				// 파라미터 값에 저장할 배열
//				this.Parameter_Values  = new string[col_ct * save_ct ];
//
//
//				// 각 행의 변경값 Setting
//				for(row = 0; row < arg_fgrid.Sheets[0].Rows.Count ; row++)
//				{
//					s = (arg_fgrid.Sheets[0].Cells[row,0].Tag == null) ? "" : arg_fgrid.Sheets[0].Cells[row,0].Tag.ToString();
//					if(s != "")
//					{ 
//						for(col = 0; col < col_ct ; col++)	// 각 열의 값 Setting
//						{  							
//							
//							//데이터 체크
//							if(arg_fgrid.arr_essential[col] == "TRUE" && arg_fgrid.Sheets[0].Cells[row,col].Value == null)
//							{
//								COM.ComFunction.User_Message("Essential Input - " +arg_fgrid.Sheets[0].ColumnHeader.Cells[arg_fgrid.Sheets[0].ColumnHeader.Rows.Count-1,col].Text) ;
//								return false ;
//							}
//							
//							// 데이터값 설정																				
//							if(arg_fgrid.Sheets[0].GetCellType(0,col).ToString() == "CheckBoxCellType")
//							{
//								if(arg_fgrid.Sheets[0].Cells[row,col].Value == null)
//								{
//									this.Parameter_Values[para_ct] = "N"; 
//								}
//								else
//								{
//									this.Parameter_Values[para_ct] = (arg_fgrid.Sheets[0].Cells[row,col].Value.ToString() == "True") ? "Y" : "N"; 
//								}								
//
//								para_ct ++;
//							}
//
//								//IUD 헤드
//							else if(col == 0)
//							{
//								this.Parameter_Values[para_ct] = (arg_fgrid.Sheets[0].Cells[row,col].Tag == null) ? "" : arg_fgrid.Sheets[0].Cells[row,col].Tag.ToString();
//								para_ct ++;
//							}
//
//
//								//콤보리스트 처리 추가
//							else if(arg_fgrid.Sheets[0].GetCellType(0,col).ToString() == "ComboBoxCellType") 
//							{
//								char[] delimiter = ":".ToCharArray();
//								string[] token = null; 
//								string token_str = "";
//
//								token_str = (arg_fgrid.Sheets[0].Cells[row,col].Value == null) ? "" : arg_fgrid.Sheets[0].Cells[row,col].Value.ToString();
//								token = token_str.Split(delimiter); 
//								this.Parameter_Values[para_ct] = (token[0] == null) ? "" : token[0].Trim();
// 
//								para_ct ++;
//							}
//								//추가(사용자업데이트위해서)
//							else if(arg_fgrid.Sheets[0].ColumnHeader.Cells[0,col].Text == "UPD_USER")
//							{
//								this.Parameter_Values[para_ct] = ComVar.This_User ;
//								para_ct ++;
//							}
//							
//							else
//							{
//								this.Parameter_Values[para_ct] = (arg_fgrid.Sheets[0].Cells[row,col].Value == null) ? "" : arg_fgrid.Sheets[0].Cells[row,col].Value.ToString();
//								para_ct ++;
//							}			
//						} 
//					}
//				}
//
//				this.Add_Modify_Parameter(true);		// 파라미터 데이터를 DataSet에 추가
//				this.Exe_Modify_Procedure();			// Modify Procedure 실행
//				
//				return true;
//
//			}
//			catch(Exception ex)
//			{
//				MessageBox.Show( ex.Message,"Save_Spread",MessageBoxButtons.OK,MessageBoxIcon.Error) ;
//				return false;
//			}
//  
//		}
//
//
//
//
//		/// <summary>
//		/// Save_Spread : 스프레드에 있는 내용을 저장
//		/// </summary>
//		/// <param name="arg_proc_name">프로세스 이름</param>
//		/// <param name="arg_fgrid">대상 스프레드</param>
//		/// <param name="arg_flag">칼럼순서</param>
//		/// <returns>정상 : true , 오류 : false </returns>
//		public bool Save_Spread(string arg_proc_name, COM.SSP arg_fgrid, int arg_flag)
//		{
//			int col_ct = arg_fgrid.Sheets[0].ColumnCount;	           // 칼럼의 수
//			int row_fixed = arg_fgrid.Sheets[0].RowHeader.Rows.Count ; // 그리드 고정행 값
//			int save_ct =0 ;							               // 저장 행 수
//
//			int i;
//			int para_ct =0;								               // 파라미터 값의 저장 배열의 수
//			int row,col;
//			string s;
//
//			try
//			{
//				this.ReDim_Parameter(col_ct);
//				this.Process_Name = arg_proc_name;
//
//				// 파라미터 이름 설정
//				this.Parameter_Name[0] = "ARG_DIVISION";
//				for(i = 1; i < col_ct; i++)
//				{
//					this.Parameter_Name[i] = "ARG_" + arg_fgrid.Sheets[0].ColumnHeader.Cells[0,i].Value.ToString(); 
//				}
//
//				// 파라미터의 데이터 Type
//				for(i = 1; i < col_ct ; i++)
//				{
//					this.Parameter_Type[i] = (int)OracleType.VarChar  ; 
//				}
//	
//				// 저장 행 수 구하기
//				for(i = 0 ; i < arg_fgrid.Sheets[0].Rows.Count; i++)
//				{
//					s = (arg_fgrid.Sheets[0].Cells[i,0].Tag == null) ? "" : arg_fgrid.Sheets[0].Cells[i,0].Tag.ToString();
//					if( s != "")
//					{
//						save_ct += 1;						
//					}
//				}
//			
//				// 파라미터 값에 저장할 배열
//				this.Parameter_Values  = new string[col_ct * save_ct ];
//
//
//				// 각 행의 변경값 Setting
//				for(row = 0; row < arg_fgrid.Sheets[0].Rows.Count ; row++)
//				{
//					s = (arg_fgrid.Sheets[0].Cells[row,0].Tag == null) ? "" : arg_fgrid.Sheets[0].Cells[row,0].Tag.ToString();
//					if(s != "")
//					{ 
//						for(col = 0; col < col_ct ; col++)	// 각 열의 값 Setting
//						{  							
//							
//							//데이터 체크
//							if(arg_fgrid.arr_essential[col] == "TRUE" && arg_fgrid.Sheets[0].Cells[row,col].Value == null)
//							{
//								COM.ComFunction.User_Message("Essential Input - " +arg_fgrid.Sheets[0].ColumnHeader.Cells[arg_fgrid.Sheets[0].ColumnHeader.Rows.Count-1,col].Text) ;
//								return false ;
//							}
//							
//							// 데이터값 설정																				
//							if(arg_fgrid.Sheets[0].GetCellType(0,col).ToString() == "CheckBoxCellType")
//							{
//								if(arg_fgrid.Sheets[0].Cells[row,col].Value == null)
//								{
//									this.Parameter_Values[para_ct] = "N"; 
//								}
//								else
//								{
//									this.Parameter_Values[para_ct] = (arg_fgrid.Sheets[0].Cells[row,col].Value.ToString() == "True") ? "Y" : "N"; 
//								}								
//
//								para_ct ++;
//							}
//
//								//IUD 헤드
//							else if(col == 0)
//							{
//								this.Parameter_Values[para_ct] = (arg_fgrid.Sheets[0].Cells[row,col].Tag == null) ? "" : arg_fgrid.Sheets[0].Cells[row,col].Tag.ToString();
//								para_ct ++;
//							}
//
//
//								//콤보리스트 처리 추가
//							else if(arg_fgrid.Sheets[0].GetCellType(0,col).ToString() == "ComboBoxCellType")
//							{
//								char[] delimiter = ":".ToCharArray();
//								string[] token = null; 
//								string token_str = "";
//
//								token_str = (arg_fgrid.Sheets[0].Cells[row,col].Value == null) ? "" : arg_fgrid.Sheets[0].Cells[row,col].Value.ToString();
//								token = token_str.Split(delimiter); 
//								this.Parameter_Values[para_ct] = (token[0] == null) ? "" : token[0].Trim();
// 
//								para_ct ++;
//							}
//								//추가(사용자업데이트위해서)
//							else if(arg_fgrid.Sheets[0].ColumnHeader.Cells[0,col].Text == "UPD_USER")
//							{
//								this.Parameter_Values[para_ct] = ComVar.This_User ;
//								para_ct ++;
//							}
//
//							else if(arg_fgrid.Sheets[0].ColumnHeader.Cells[0,col].Text == "UPD_YMD")
//							{
//								this.Parameter_Values[para_ct] =System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); ;
//								para_ct ++;
//							}
//							
//							
//							else
//							{
//								this.Parameter_Values[para_ct] = (arg_fgrid.Sheets[0].Cells[row,col].Value == null) ? "" : arg_fgrid.Sheets[0].Cells[row,col].Value.ToString();
//								para_ct ++;
//							}			
//						} 
//					}
//				}
//
//				this.Add_Modify_Parameter(true);		// 파라미터 데이터를 DataSet에 추가
//				this.Exe_Modify_Procedure();			// Modify Procedure 실행
//				
//				return true;
//
//			}
//			catch(Exception ex)
//			{
//				MessageBox.Show( ex.Message,"Save_Spread",MessageBoxButtons.OK,MessageBoxIcon.Error) ;
//				return false;
//			}
//  
//		}




#region No Use
       /* 
		/// <summary>
		/// Save_Spread : 스프레드에 있는 내용을 저장
		/// </summary>
		/// <param name="arg_proc_name">프로세스 이름</param>
		/// <param name="arg_fgrid">대상 스프레드</param>
		/// <returns>정상 : true , 오류 : false </returns>
		public bool Save_Spread(string arg_proc_name, COM.SSP arg_fgrid)
		{
			int col_ct = arg_fgrid.Sheets[0].ColumnCount-1;	           // 칼럼의 수
			int row_fixed = arg_fgrid.Sheets[0].RowHeader.Rows.Count ; // 그리드 고정행 값
			int save_ct =0 ;							               // 저장 행 수

			int i;
			int para_ct =0;								               // 파라미터 값의 저장 배열의 수
			int row,col;
			string s;

			try
			{
				this.ReDim_Parameter(col_ct);
				this.Process_Name = arg_proc_name;

				// 파라미터 이름 설정
				this.Parameter_Name[0] = "ARG_DIVISION";
				for(i = 1; i < col_ct; i++)
				{
					this.Parameter_Name[i] = "ARG_" + arg_fgrid.Sheets[0].ColumnHeader.Cells[0,i].Value.ToString(); 
				}

				// 파라미터의 데이터 Type
				for(i = 1; i < col_ct ; i++)
				{
					this.Parameter_Type[i] = (int)OracleType.VarChar  ; 
				}
	
				// 저장 행 수 구하기
				for(i = 0 ; i < arg_fgrid.Sheets[0].Rows.Count; i++)
				{
					s = (arg_fgrid.Sheets[0].Cells[i,0].Tag == null) ? "" : arg_fgrid.Sheets[0].Cells[i,0].Tag.ToString();
					if( s != "")
					{
						save_ct += 1;						
					}
				}
			
				// 파라미터 값에 저장할 배열
				this.Parameter_Values  = new string[col_ct * save_ct ];


				// 각 행의 변경값 Setting
				for(row = 0; row < arg_fgrid.Sheets[0].Rows.Count ; row++)
				{
					s = (arg_fgrid.Sheets[0].Cells[row,0].Tag == null) ? "" : arg_fgrid.Sheets[0].Cells[row,0].Tag.ToString();
					if(s != "")
					{ 
						for(col = 0; col < col_ct ; col++)	// 각 열의 값 Setting
						{  							
							//****************** 박지수 수정분  							
							//데이터 체크
							if(arg_fgrid.arr_essential[col] == "TRUE" && (arg_fgrid.Sheets[0].Cells[row,col].Value == null || arg_fgrid.Sheets[0].Cells[row,col].Value.ToString() == "") )
								//******************  							
							{
								COM.ComFunction.User_Message("Essential Input - " +arg_fgrid.Sheets[0].ColumnHeader.Cells[arg_fgrid.Sheets[0].ColumnHeader.Rows.Count-1,col].Text) ;
								return false ;
							}
							
							// 데이터값 설정																				
							if(arg_fgrid.Sheets[0].GetCellType(0,col).ToString() == "CheckBoxCellType")
							{
								if(arg_fgrid.Sheets[0].Cells[row,col].Value == null)
								{
									this.Parameter_Values[para_ct] = "N"; 
								}
								else
								{
									this.Parameter_Values[para_ct] = (arg_fgrid.Sheets[0].Cells[row,col].Value.ToString() == "True") ? "Y" : "N"; 
								}								

								para_ct ++;
							}

								//IUD 헤드
							else if(col == 0)
							{
								this.Parameter_Values[para_ct] = (arg_fgrid.Sheets[0].Cells[row,col].Tag == null) ? "" : arg_fgrid.Sheets[0].Cells[row,col].Tag.ToString();
								para_ct ++;
							}


								//콤보리스트 처리 추가
							else if(arg_fgrid.Sheets[0].GetCellType(0,col).ToString() == "ComboBoxCellType") 
							{
								char[] delimiter = ":".ToCharArray();
								string[] token = null; 
								string token_str = "";

								token_str = (arg_fgrid.Sheets[0].Cells[row,col].Value == null) ? "" : arg_fgrid.Sheets[0].Cells[row,col].Value.ToString();
								token = token_str.Split(delimiter); 
								this.Parameter_Values[para_ct] = (token[0] == null) ? "" : token[0].Trim();
 
								para_ct ++;
							}
								//추가(사용자업데이트위해서)
							else if(arg_fgrid.Sheets[0].ColumnHeader.Cells[0,col].Text == "UPD_USER")
							{
								this.Parameter_Values[para_ct] = ComVar.This_User ;
								para_ct ++;
							}
							
							else
							{
								this.Parameter_Values[para_ct] = (arg_fgrid.Sheets[0].Cells[row,col].Value == null) ? "" : arg_fgrid.Sheets[0].Cells[row,col].Value.ToString();
								para_ct ++;
							}			
						} 
					}
				}

				//****************** 박지수 수정분  							
				this.Add_Modify_Parameter(true);						// 파라미터 데이터를 DataSet에 추가
				DataSet ds_Set = this.Exe_Modify_Procedure();			// Modify Procedure 실행
				
				if (ds_Set == null) return false;
				else return true;
				//******************  							
			}
			catch(Exception ex)
			{
				MessageBox.Show( ex.Message,"Save_Spread",MessageBoxButtons.OK,MessageBoxIcon.Error) ;
				return false;
			}
  
		}




		/// <summary>
		/// Save_Spread : 스프레드에 있는 내용을 저장
		/// </summary>
		/// <param name="arg_proc_name">프로세스 이름</param>
		/// <param name="arg_fgrid">대상 스프레드</param>
		/// <param name="arg_flag">칼럼순서</param>
		/// <returns>정상 : true , 오류 : false </returns>
		public bool Save_Spread(string arg_proc_name, COM.SSP arg_fgrid, int arg_flag)
		{
			int col_ct = arg_fgrid.Sheets[0].ColumnCount;	           // 칼럼의 수
			int row_fixed = arg_fgrid.Sheets[0].RowHeader.Rows.Count ; // 그리드 고정행 값
			int save_ct =0 ;							               // 저장 행 수

			int i;
			int para_ct =0;								               // 파라미터 값의 저장 배열의 수
			int row,col;
			string s;

			try
			{
				this.ReDim_Parameter(col_ct);
				this.Process_Name = arg_proc_name;

				// 파라미터 이름 설정
				this.Parameter_Name[0] = "ARG_DIVISION";
				for(i = 1; i < col_ct; i++)
				{
					this.Parameter_Name[i] = "ARG_" + arg_fgrid.Sheets[0].ColumnHeader.Cells[0,i].Value.ToString(); 
				}

				// 파라미터의 데이터 Type
				for(i = 1; i < col_ct ; i++)
				{
					this.Parameter_Type[i] = (int)OracleType.VarChar  ; 
				}
	
				// 저장 행 수 구하기
				for(i = 0 ; i < arg_fgrid.Sheets[0].Rows.Count; i++)
				{
					s = (arg_fgrid.Sheets[0].Cells[i,0].Tag == null) ? "" : arg_fgrid.Sheets[0].Cells[i,0].Tag.ToString();
					if( s != "")
					{
						save_ct += 1;						
					}
				}
			
				// 파라미터 값에 저장할 배열
				this.Parameter_Values  = new string[col_ct * save_ct ];


				// 각 행의 변경값 Setting
				for(row = 0; row < arg_fgrid.Sheets[0].Rows.Count ; row++)
				{
					s = (arg_fgrid.Sheets[0].Cells[row,0].Tag == null) ? "" : arg_fgrid.Sheets[0].Cells[row,0].Tag.ToString();
					if(s != "")
					{ 
						for(col = 0; col < col_ct ; col++)	// 각 열의 값 Setting
						{  							
							
							//데이터 체크
							//****************** 박지수 수정분  							
							if(arg_fgrid.arr_essential[col] == "TRUE" && (arg_fgrid.Sheets[0].Cells[row,col].Value == null || arg_fgrid.Sheets[0].Cells[row,col].Value.ToString() == "") )
								//******************  							
							{
								COM.ComFunction.User_Message("Essential Input - " +arg_fgrid.Sheets[0].ColumnHeader.Cells[arg_fgrid.Sheets[0].ColumnHeader.Rows.Count-1,col].Text) ;
								return false ;
							}
							
							// 데이터값 설정																				
							if(arg_fgrid.Sheets[0].GetCellType(0,col).ToString() == "CheckBoxCellType")
							{
								if(arg_fgrid.Sheets[0].Cells[row,col].Value == null)
								{
									this.Parameter_Values[para_ct] = "N"; 
								}
								else
								{
									this.Parameter_Values[para_ct] = (arg_fgrid.Sheets[0].Cells[row,col].Value.ToString() == "True") ? "Y" : "N"; 
								}								

								para_ct ++;
							}

								//IUD 헤드
							else if(col == 0)
							{
								this.Parameter_Values[para_ct] = (arg_fgrid.Sheets[0].Cells[row,col].Tag == null) ? "" : arg_fgrid.Sheets[0].Cells[row,col].Tag.ToString();
								para_ct ++;
							}


								//콤보리스트 처리 추가
							else if(arg_fgrid.Sheets[0].GetCellType(0,col).ToString() == "ComboBoxCellType")
							{
								char[] delimiter = ":".ToCharArray();
								string[] token = null; 
								string token_str = "";

								token_str = (arg_fgrid.Sheets[0].Cells[row,col].Value == null) ? "" : arg_fgrid.Sheets[0].Cells[row,col].Value.ToString();
								token = token_str.Split(delimiter); 
								this.Parameter_Values[para_ct] = (token[0] == null) ? "" : token[0].Trim();
 
								para_ct ++;
							}
								//추가(사용자업데이트위해서)
							else if(arg_fgrid.Sheets[0].ColumnHeader.Cells[0,col].Text == "UPD_USER")
							{
								this.Parameter_Values[para_ct] = ComVar.This_User ;
								para_ct ++;
							}

							else if(arg_fgrid.Sheets[0].ColumnHeader.Cells[0,col].Text == "UPD_YMD")
							{
								this.Parameter_Values[para_ct] =System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); ;
								para_ct ++;
							}
							
							
							else
							{
								this.Parameter_Values[para_ct] = (arg_fgrid.Sheets[0].Cells[row,col].Value == null) ? "" : arg_fgrid.Sheets[0].Cells[row,col].Value.ToString();
								para_ct ++;
							}			
						} 
					}
				}

				//****************** 박지수 수정분  							
				this.Add_Modify_Parameter(true);						// 파라미터 데이터를 DataSet에 추가
				DataSet ds_Set = this.Exe_Modify_Procedure();			// Modify Procedure 실행
				
				if (ds_Set == null) return false;
				else return true;
				//******************   							
			}
			catch(Exception ex)
			{
				MessageBox.Show( ex.Message,"Save_Spread",MessageBoxButtons.OK,MessageBoxIcon.Error) ;
				return false;
			}
  
		}



		/// <summary>
		/// Save_Spread : 스프레드에 있는 내용을 저장
		/// </summary>
		/// <param name="arg_proc_name">프로시저 이름</param>
		/// <param name="arg_fgrid">대상 스프레드</param>
		/// <param name="arg_flag">데이터셋 클리어 여부</param>
		/// <returns>정상 : true , 오류 : false </returns>
		public bool Save_Spread_Ready(string arg_proc_name, COM.SSP arg_fgrid, bool arg_clear)
		{
			int col_ct = arg_fgrid.Sheets[0].ColumnCount - 1;	           // 칼럼의 수
			int row_fixed = arg_fgrid.Sheets[0].RowHeader.Rows.Count ; // 그리드 고정행 값
			int save_ct =0 ;							               // 저장 행 수

			int i;
			int para_ct =0;								               // 파라미터 값의 저장 배열의 수
			int row,col;
			string s;

			try
			{
				this.ReDim_Parameter(col_ct);
				this.Process_Name = arg_proc_name;

				// 파라미터 이름 설정
				this.Parameter_Name[0] = "ARG_DIVISION";
				for(i = 1; i < col_ct; i++)
				{
					this.Parameter_Name[i] = "ARG_" + arg_fgrid.Sheets[0].ColumnHeader.Cells[0,i].Value.ToString(); 
				}

				// 파라미터의 데이터 Type
				for(i = 1; i < col_ct ; i++)
				{
					this.Parameter_Type[i] = (int)OracleType.VarChar  ; 
				}
	
				// 저장 행 수 구하기
				for(i = 0 ; i < arg_fgrid.Sheets[0].Rows.Count; i++)
				{
					s = (arg_fgrid.Sheets[0].Cells[i,0].Tag == null) ? "" : arg_fgrid.Sheets[0].Cells[i,0].Tag.ToString();
					if( s != "")
					{
						save_ct += 1;						
					}
				}
			
				// 파라미터 값에 저장할 배열
				this.Parameter_Values  = new string[col_ct * save_ct ];


				// 각 행의 변경값 Setting
				for(row = 0; row < arg_fgrid.Sheets[0].Rows.Count ; row++)
				{
					s = (arg_fgrid.Sheets[0].Cells[row,0].Tag == null) ? "" : arg_fgrid.Sheets[0].Cells[row,0].Tag.ToString();
					if(s != "")
					{ 
						for(col = 0; col < col_ct ; col++)	// 각 열의 값 Setting
						{  							
							
							//데이터 체크
							//****************** 박지수 수정분  							
							if(arg_fgrid.arr_essential[col] == "TRUE" && (arg_fgrid.Sheets[0].Cells[row,col].Value == null || arg_fgrid.Sheets[0].Cells[row,col].Value.ToString() == "") )
								//******************  							
							{
								COM.ComFunction.User_Message("Essential Input - " +arg_fgrid.Sheets[0].ColumnHeader.Cells[arg_fgrid.Sheets[0].ColumnHeader.Rows.Count-1,col].Text) ;
								return false ;
							}
							
							// 데이터값 설정																				
							if(arg_fgrid.Sheets[0].GetCellType(0,col).ToString() == "CheckBoxCellType")
							{
								if(arg_fgrid.Sheets[0].Cells[row,col].Value == null)
								{
									this.Parameter_Values[para_ct] = "N"; 
								}
								else
								{
									this.Parameter_Values[para_ct] = (arg_fgrid.Sheets[0].Cells[row,col].Value.ToString() == "True") ? "Y" : "N"; 
								}								

								para_ct ++;
							}

								//IUD 헤드
							else if(col == 0)
							{
								this.Parameter_Values[para_ct] = (arg_fgrid.Sheets[0].Cells[row,col].Tag == null) ? "" : arg_fgrid.Sheets[0].Cells[row,col].Tag.ToString();
								para_ct ++;
							}


								//콤보리스트 처리 추가
							else if(arg_fgrid.Sheets[0].GetCellType(0,col).ToString() == "ComboBoxCellType")
							{
								char[] delimiter = ":".ToCharArray();
								string[] token = null; 
								string token_str = "";

								token_str = (arg_fgrid.Sheets[0].Cells[row,col].Value == null) ? "" : arg_fgrid.Sheets[0].Cells[row,col].Value.ToString();
								token = token_str.Split(delimiter); 
								this.Parameter_Values[para_ct] = (token[0] == null) ? "" : token[0].Trim();
 
								para_ct ++;
							}
								// datetime 컬럼 처리
							else if(arg_fgrid.Sheets[0].GetCellType(0,col).ToString() == "DateTimeCellType")
							{
								if(arg_fgrid.Sheets[0].Cells[row,col].Value == null || arg_fgrid.Sheets[0].Cells[row,col].Value.Equals("") )
								{
									this.Parameter_Values[para_ct] = "";
								}
								else
								{
									this.Parameter_Values[para_ct] = DateTime.Parse(arg_fgrid.Sheets[0].Cells[row,col].Value.ToString() ).ToString("yyyy-MM-dd");
								}

								para_ct ++;

							}
								//추가(사용자업데이트위해서)
							else if(arg_fgrid.Sheets[0].ColumnHeader.Cells[0,col].Text == "UPD_USER")
							{
								this.Parameter_Values[para_ct] = ComVar.This_User ;
								para_ct ++;
							}

							else if(arg_fgrid.Sheets[0].ColumnHeader.Cells[0,col].Text == "UPD_YMD")
							{
								this.Parameter_Values[para_ct] =System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); ;
								para_ct ++;
							}
							
							
							else
							{
								this.Parameter_Values[para_ct] = (arg_fgrid.Sheets[0].Cells[row,col].Value == null) ? "" : arg_fgrid.Sheets[0].Cells[row,col].Value.ToString();
								para_ct ++;
							}			
						} 
					}
				}

				this.Add_Modify_Parameter(arg_clear);						// 파라미터 데이터를 DataSet에 추가
				return true;
			}
			catch(Exception ex)
			{
				MessageBox.Show( ex.Message,"Save_Spread",MessageBoxButtons.OK,MessageBoxIcon.Error) ;
				return false;
			}
  
		}



		/// <summary>
		/// Save_Spread_CrossTab : 스프레드에 있는 내용을 저장
		/// </summary>
		/// <param name="arg_proc_name">프로세스 이름</param>
		/// <param name="arg_fgrid">대상 스프레드</param>
		/// <returns>정상 : true , 오류 : false </returns>
		public bool Save_Spread_CrossTab(string arg_proc_name, COM.SSP arg_fgrid, int arg_crs_start, string arg_col_nm1,string arg_col_nm2)
		{
			int col_ct = arg_fgrid.Sheets[0].ColumnCount;	           // 칼럼의 수
			int row_fixed = arg_fgrid.Sheets[0].RowHeader.Rows.Count ; // 그리드 고정행 값
							
			int save_ct =0 ;							               // 저장 행 수

			int i;
			int para_ct =0;								               // 파라미터 값의 저장 배열의 수
			int row,col,crs;
			string s;

			try
			{
				this.ReDim_Parameter(arg_crs_start+2);
				this.Process_Name = arg_proc_name;

				// 파라미터 이름 설정
				this.Parameter_Name[0] = "ARG_DIVISION";
				for(i = 1; i < arg_crs_start; i++)
				{
					this.Parameter_Name[i] = "ARG_" + arg_fgrid.Sheets[0].ColumnHeader.Cells[0,i].Value.ToString(); 
				}
				this.Parameter_Name[arg_crs_start]   = arg_col_nm1; 
				this.Parameter_Name[arg_crs_start+1] = arg_col_nm2; 

				// 파라미터의 데이터 Type
				for(i = 0; i < arg_crs_start ; i++)
				{
					this.Parameter_Type[i] = (int)OracleType.VarChar  ; 
				}
				this.Parameter_Type[arg_crs_start]   = (int)OracleType.VarChar  ;
				this.Parameter_Type[arg_crs_start+1] = (int)OracleType.VarChar  ;
	
				// 저장 행 수 구하기
				for(i = 0 ; i < arg_fgrid.Sheets[0].Rows.Count; i++)
				{
					s = (arg_fgrid.Sheets[0].Cells[i,0].Tag == null) ? "" : arg_fgrid.Sheets[0].Cells[i,0].Tag.ToString();
					if( s != "")
					{
						save_ct += 1;						
					}
				}
			
				// 파라미터 값에 저장할 배열
				this.Parameter_Values  = new string[(arg_crs_start+2) * save_ct * (col_ct - arg_crs_start) ];


				// 각 행의 변경값 Setting
				for(row = 0; row < arg_fgrid.Sheets[0].Rows.Count ; row++)
				{
					s = (arg_fgrid.Sheets[0].Cells[row,0].Tag == null) ? "" : arg_fgrid.Sheets[0].Cells[row,0].Tag.ToString();
					if(s != "")
					{ 
						for(crs = arg_crs_start; crs < col_ct; crs++)
						{
							for(col = 0; col < arg_crs_start ; col++)	// 각 열의 값 Setting
							{    																					
							
								// 데이터값 설정																				
								if(arg_fgrid.Sheets[0].GetCellType(0,col).ToString() == "CheckBoxCellType")
								{
									if(arg_fgrid.Sheets[0].Cells[row,col].Value == null)
									{
										this.Parameter_Values[para_ct] = "N"; 
									}
									else
									{
										this.Parameter_Values[para_ct] = (arg_fgrid.Sheets[0].Cells[row,col].Value.ToString() == "True") ? "Y" : "N"; 
									}								

									para_ct ++;
								}

									//IUD 헤드
								else if(col == 0)
								{
									this.Parameter_Values[para_ct] = (arg_fgrid.Sheets[0].Cells[row,col].Tag == null) ? "" : arg_fgrid.Sheets[0].Cells[row,col].Tag.ToString();
									para_ct ++;
								}


									//콤보리스트 처리 추가
								else if(arg_fgrid.Sheets[0].GetCellType(0,col).ToString() == "ComboBoxCellType")
								{
									char[] delimiter = ":".ToCharArray();
									string[] token = null; 
									string token_str = "";

									token_str = (arg_fgrid.Sheets[0].Cells[row,col].Value == null) ? "" : arg_fgrid.Sheets[0].Cells[row,col].Value.ToString();
									token = token_str.Split(delimiter); 
									this.Parameter_Values[para_ct] = (token[0] == null) ? "" : token[0].Trim();
	 
									para_ct ++;
								}
									//추가(사용자업데이트위해서)
								else if(arg_fgrid.Sheets[0].ColumnHeader.Cells[0,col].Text == "UPD_USER")
								{
									this.Parameter_Values[para_ct] = ComVar.This_User ;
									para_ct ++;
								}
								
								else
								{
									this.Parameter_Values[para_ct] = (arg_fgrid.Sheets[0].Cells[row,col].Value == null) ? "" : arg_fgrid.Sheets[0].Cells[row,col].Value.ToString();
									para_ct ++;
								}			
							}
							this.Parameter_Values[para_ct] = arg_fgrid.Sheets[0].ColumnHeader.Cells[row_fixed-1,crs].Text ;
							para_ct ++;

							this.Parameter_Values[para_ct] = (arg_fgrid.Sheets[0].Cells[row,crs].Value == null) ? "" : arg_fgrid.Sheets[0].Cells[row,crs].Value.ToString() ;
							para_ct ++;
						} 
					}
				}

				this.Add_Modify_Parameter(true);		// 파라미터 데이터를 DataSet에 추가
				this.Exe_Modify_Procedure();			// Modify Procedure 실행
				
				return true;

			}
			catch(Exception ex)
			{
				MessageBox.Show( ex.Message,"Save_Spread",MessageBoxButtons.OK,MessageBoxIcon.Error) ;
				return false;
			}
  
		}


		/// <summary>
		/// Save_Spread_CrossTab_Request : 스프레드에 있는 내용을 저장
		/// </summary>
		/// <param name="arg_proc_name">프로세스 이름</param>
		/// <param name="arg_fgrid">대상 스프레드</param>
		/// <returns>정상 : true , 오류 : false </returns>
		public bool Save_Spread_CrossTab_Request(string arg_proc_name, COM.SSP arg_fgrid, int arg_crs_start, string arg_col_nm1,string arg_col_nm2, string arg_pk_nm1, string arg_pk_nm2, string arg_pk_val1, string arg_pk_val2)
		{
			int col_ct = arg_fgrid.Sheets[0].ColumnCount;	           // 칼럼의 수
			int row_fixed = arg_fgrid.Sheets[0].RowHeader.Rows.Count ; // 그리드 고정행 값
			
			try
			{
				this.ReDim_Parameter(arg_crs_start+2-1);
				this.Process_Name = arg_proc_name;

				// 파라미터 이름 설정
				this.Parameter_Name[0] = "ARG_DIVISION";
				this.Parameter_Name[1] = arg_col_nm1; 
				this.Parameter_Name[2] = arg_col_nm2; 
				this.Parameter_Name[3] = arg_pk_nm1;
				this.Parameter_Name[4] = arg_pk_nm2;

				// 파라미터의 데이터 Type
				this.Parameter_Type[0] = (int)OracleType.VarChar  ; 
				this.Parameter_Type[1] = (int)OracleType.VarChar  ;
				this.Parameter_Type[2] = (int)OracleType.VarChar  ;
				this.Parameter_Type[3] = (int)OracleType.VarChar  ;
				this.Parameter_Type[4] = (int)OracleType.VarChar  ;
	
				//04.DATA 정의  			

				// 파라미터 값에 저장할 배열
				this.Parameter_Values = new string [(arg_fgrid.Sheets[0].Columns.Count-arg_crs_start)*5];
				
				
				for(int i = 0 ; i < arg_fgrid.Sheets[0].Columns.Count-arg_crs_start ; i++)
				{
					if(arg_fgrid.Sheets[0].Cells[0,i+4].Text != "0")
					{
						this.Parameter_Values[i*5]   = "A"; 
						this.Parameter_Values[i*5+1] = arg_fgrid.ActiveSheet.ColumnHeader.Cells[0,i+4].Text;  //헤더  cs_size 
						this.Parameter_Values[i*5+2] = arg_fgrid.Sheets[0].Cells[0,i+4].Text;                 //값    cs_qty  
						this.Parameter_Values[i*5+3] = arg_pk_val1;
						this.Parameter_Values[i*5+4] = arg_pk_val2;
					}
				}

				this.Add_Modify_Parameter(true);		// 파라미터 데이터를 DataSet에 추가
				this.Exe_Modify_Procedure();			// Modify Procedure 실행
				
				return true;

			}
			catch(Exception ex)
			{
				MessageBox.Show( ex.Message,"Save_Spread",MessageBoxButtons.OK,MessageBoxIcon.Error) ;
				return false;
			}
  
		}




		/// <summary>
		/// Save_Spread_CrossTab_Request : 스프레드에 있는 내용을 저장
		/// </summary>
		/// <param name="arg_proc_name">프로세스 이름</param>
		/// <param name="arg_fgrid">대상 스프레드</param>
		/// <returns>정상 : true , 오류 : false </returns>
		public bool Save_Spread_CrossTab_Request2(string arg_proc_name, COM.SSP arg_fgrid, int arg_crs_start, string arg_col_nm1,string arg_col_nm2, string arg_pk_nm1, string arg_pk_nm2, string arg_pk_nm3, string arg_pk_val1, string arg_pk_val2, string arg_pk_val3)
		{
			int col_ct = arg_fgrid.Sheets[0].ColumnCount;	           // 칼럼의 수
			int row_fixed = arg_fgrid.Sheets[0].RowHeader.Rows.Count ; // 그리드 고정행 값
			
			try
			{
				this.ReDim_Parameter(arg_crs_start+3-1);
				this.Process_Name = arg_proc_name;

				// 파라미터 이름 설정
				this.Parameter_Name[0] = "ARG_DIVISION";
				this.Parameter_Name[1] = arg_col_nm1; 
				this.Parameter_Name[2] = arg_col_nm2; 
				this.Parameter_Name[3] = arg_pk_nm1;
				this.Parameter_Name[4] = arg_pk_nm2;
				this.Parameter_Name[5] = arg_pk_nm3;

				// 파라미터의 데이터 Type
				this.Parameter_Type[0] = (int)OracleType.VarChar; 
				this.Parameter_Type[1] = (int)OracleType.VarChar;
				this.Parameter_Type[2] = (int)OracleType.VarChar;
				this.Parameter_Type[3] = (int)OracleType.VarChar;
				this.Parameter_Type[4] = (int)OracleType.VarChar;
				this.Parameter_Type[5] = (int)OracleType.VarChar;
	
				//04.DATA 정의  			

				// 파라미터 값에 저장할 배열
				this.Parameter_Values = new string [(arg_fgrid.Sheets[0].Columns.Count-arg_crs_start)*6];
				
				
				for(int i = 0 ; i < arg_fgrid.Sheets[0].Columns.Count-arg_crs_start ; i++)
				{
					if(arg_fgrid.Sheets[0].Cells[0,i+4].Text != "0")
					{
						this.Parameter_Values[i*6]   = "A"; 
						this.Parameter_Values[i*6+1] = arg_fgrid.ActiveSheet.ColumnHeader.Cells[0,i+4].Text;  //헤더  cs_size 
						this.Parameter_Values[i*6+2] = arg_fgrid.Sheets[0].Cells[0,i+4].Text;                 //값    cs_qty  
						this.Parameter_Values[i*6+3] = arg_pk_val1;
						this.Parameter_Values[i*6+4] = arg_pk_val2;
						this.Parameter_Values[i*6+5] = arg_pk_val3;											  //값    style_cd  
					}
				}

				this.Add_Modify_Parameter(true);		// 파라미터 데이터를 DataSet에 추가
				this.Exe_Modify_Procedure();			// Modify Procedure 실행
				
				return true;

			}
			catch(Exception ex)
			{
				MessageBox.Show( ex.Message,"Save_Spread",MessageBoxButtons.OK,MessageBoxIcon.Error) ;
				return false;
			}
  
		}
        */

#endregion No Use



		/// <summary>
		/// Select_ComCode : 공통코드 리스트 조회
		/// </summary>
		/// <param name="arg_factory">공장</param>
		/// <param name="arg_code">해당 코드</param>
		/// <returns>정상 : DataTable , 오류 : null </returns>
		public DataTable Select_ComCode(string arg_factory, string arg_code)
		{

			string Proc_Name = "PKG_SCM_CODE.SELECT_COM_CODE";

			this.ReDim_Parameter(3); 
			this.Process_Name = Proc_Name;

			this.Parameter_Name[0] = "ARG_FACTORY";
			this.Parameter_Name[1] = "ARG_COM_CD";
			this.Parameter_Name[2] = "OUT_CURSOR";

			this.Parameter_Type[0] = (int)OracleType.VarChar;
			this.Parameter_Type[1] = (int)OracleType.VarChar;
			this.Parameter_Type[2] = (int)OracleType.Cursor;

			this.Parameter_Values[0] = arg_factory;
			this.Parameter_Values[1] = arg_code;
			this.Parameter_Values[2] = "";

			this.Add_Select_Parameter(true); 
			DS_Ret = Exe_Select_Procedure();

			if(DS_Ret == null) return null ;

			return  DS_Ret.Tables[Proc_Name];
				 
		}


		/// <summary>
		/// Select_GridHead : 그리드 헤드 정보 조회
		/// </summary>
		/// <param name="arg_pgid">그리드사용 프로그램 ID</param>
		/// <param name="arg_pgseq">그리드 Seq</param>
		/// <returns>정상 : DataTable , 오류 : null </returns>
		public DataTable Select_GridHead(string arg_pgid, string arg_pgseq)
		{

			string Proc_Name = "PKG_SCM_TABLE.SELECT_COL_LIST";

			////// DB에서 그리드 Head 추출 
			this.ReDim_Parameter(3);
			this.Process_Name = Proc_Name;

			this.Parameter_Name[0] = "ARG_PG_ID";
			this.Parameter_Name[1] = "ARG_PG_SEQ"; 
			this.Parameter_Name[2] = "OUT_CURSOR"; 
			
			this.Parameter_Type[0] = (int)OracleType.VarChar;
			this.Parameter_Type[1] = (int)OracleType.VarChar;
			this.Parameter_Type[2] = (int)OracleType.Cursor;

			this.Parameter_Values[0] = arg_pgid;
			this.Parameter_Values[1] = arg_pgseq;
			this.Parameter_Values[2] = "";


			this.Add_Select_Parameter(true); 
			DS_Ret = Exe_Select_Procedure();

			if(DS_Ret == null) return null ;
			
			return  DS_Ret.Tables[Proc_Name];

		}

		/// <summary>
		/// Select_Lang : SPC_DATA_DIC테이블에서 데이터를 가져 옵니다.
		/// </summary>
		/// <param name="arg_factory">공장 코드</param>
        /// <param name="arg_lang_cd">언어코드</param>
		/// <param name="arg_pg_id">폼이름</param>
		/// <returns></returns>
		public DataTable Select_LangDic(string arg_factory, string arg_lang_cd, string arg_pg_id)
		{

            string Proc_Name = "PKG_SPC_DATA_DIC.SELECT_SPC_DATA_DIC_REQ";

			//// DB에서 언어 Dictionary 추출
			this.ReDim_Parameter(4);
			this.Process_Name = Proc_Name ;


			this.Parameter_Name[0] = "ARG_FACTORY";
			this.Parameter_Name[1] = "ARG_LANG_CD";
            this.Parameter_Name[2] = "ARG_PG_ID";
            this.Parameter_Name[3] = "OUT_CURSOR";

			this.Parameter_Type[0] = (int)OracleType.VarChar;
			this.Parameter_Type[1] = (int)OracleType.VarChar;
            this.Parameter_Type[2] = (int)OracleType.VarChar;
			this.Parameter_Type[3] = (int)OracleType.Cursor;

			this.Parameter_Values[0] = arg_factory;
			this.Parameter_Values[1] = arg_lang_cd;
            this.Parameter_Values[2] = arg_pg_id;
            this.Parameter_Values[3] = "";

			this.Add_Select_Parameter(true);
			DS_Ret = Exe_Select_Procedure();

			if(DS_Ret == null) return null ;
			
			return  DS_Ret.Tables[Proc_Name];
		}







        ///// <summary>
        ///// Select_Button : 버튼 권한 가져오기
        ///// </summary>
        ///// <param name="arg_factory"></param>
        ///// <param name="arg_menu_pg"></param>
        ///// <returns></returns>
        //public DataTable Select_Button(string arg_factory, string arg_user_id, string arg_menu_pg)
        //{

        //    string Proc_Name = "PKG_SPS_MENU.SELECT_FORM_BTN";

        //    //// DB에서 언어 Dictionary 추출
        //    this.ReDim_Parameter(4);
        //    this.Process_Name = Proc_Name ;


        //    this.Parameter_Name[0] = "ARG_FACTORY";
        //    this.Parameter_Name[1] = "ARG_USER_ID";
        //    this.Parameter_Name[2] = "ARG_MENU_PG";
        //    this.Parameter_Name[3] = "OUT_CURSOR";

        //    this.Parameter_Type[0] = (int)OracleType.VarChar;
        //    this.Parameter_Type[1] = (int)OracleType.VarChar;
        //    this.Parameter_Type[2] = (int)OracleType.VarChar;
        //    this.Parameter_Type[3] = (int)OracleType.Cursor;

        //    this.Parameter_Values[0] = arg_factory;
        //    this.Parameter_Values[1] = COM.ComVar.This_User_AD;  //arg_user_id;
        //    this.Parameter_Values[2] = arg_menu_pg;
        //    this.Parameter_Values[3] = "";

        //    this.Add_Select_Parameter(true);
        //    DS_Ret = Exe_Select_Procedure();

        //    if(DS_Ret == null) return null ;
			
        //    return  DS_Ret.Tables[Proc_Name];
        //}







		/// <summary>
		/// Select_Proc_Error_Check : 프로시져 ERROR를 첵크 합니다.
		/// </summary>
		/// <param name="arg_division">업무 구분</param>
		/// <param name="arg_err_div">에러 타입</param>
		/// <returns></returns>
		public bool Select_Proc_Error_Check(string arg_division, string arg_sp_name, string arg_err_div)
		{
			string Year = DateTime.Now.Year.ToString();
			string Month = DateTime.Now.Month.ToString();
			if(Month.Length == 1)
			{
				Month = "0" + Month; 
			}

			string Day = DateTime.Now.Day.ToString();
			if(Day.Length == 1)
			{
				Day = "0" + Day;
			}

			string upd_date  = Year + Month + Day;

			string Proc_Name = "PKG_SPS_LOG_HIST.SELECT_PROC_ERR";

			//// DB에서 언어 Dictionary 추출
			this.ReDim_Parameter(6);
			this.Process_Name = Proc_Name ;


			this.Parameter_Name[0] = "ARG_DIVISION";
			this.Parameter_Name[1] = "ARG_FACTORY";
			this.Parameter_Name[2] = "ARG_SP_NAME";
			this.Parameter_Name[3] = "ARG_UPD_USER";
			this.Parameter_Name[4] = "ARG_ERR_DIV";
			this.Parameter_Name[5] = "OUT_CURSOR";

			this.Parameter_Type[0] = (int)OracleType.VarChar;
			this.Parameter_Type[1] = (int)OracleType.VarChar;
			this.Parameter_Type[2] = (int)OracleType.VarChar;
			this.Parameter_Type[3] = (int)OracleType.VarChar;
			this.Parameter_Type[4] = (int)OracleType.VarChar;
			this.Parameter_Type[5] = (int)OracleType.Cursor;

			this.Parameter_Values[0] = arg_division;
			this.Parameter_Values[1] = COM.ComVar.This_Factory;
			this.Parameter_Values[2] = arg_sp_name;
			this.Parameter_Values[3] = COM.ComVar.This_User;
			this.Parameter_Values[4] = arg_err_div;
			this.Parameter_Values[5] = "";



			this.Add_Select_Parameter(true);
			DS_Ret = Exe_Select_Procedure();
			
			if(DS_Ret.Tables[Proc_Name].Rows.Count > 0)
				return true;
			else
				return false;
		}

		/// <summary>
		/// Select_Rpm_Error_Check : 프로시져 ERROR를 첵크 합니다.
		/// </summary>
		/// <param name="arg_division">업무 구분</param>
		/// <param name="arg_err_div">에러 타입</param>
		/// <returns></returns>
		public bool Select_Rpm_Error_Check(string arg_division, string arg_sp_name, string arg_err_div)
		{
			string Year = DateTime.Now.Year.ToString();
			string Month = DateTime.Now.Month.ToString();
			
			if(Month.Length == 1)
			{
				Month = "0" + Month; 
			}

			string Day = DateTime.Now.Day.ToString();
			if(Day.Length == 1)
			{
				Day = "0" + Day;
			}

			string upd_date  = Year + Month + Day;

			string Proc_Name = "PKG_SPS_LOG_HIST.SELECT_RPM_ERR";

			//// DB에서 언어 Dictionary 추출
			this.ReDim_Parameter(6);
			this.Process_Name = Proc_Name ;


			this.Parameter_Name[0] = "ARG_DIVISION";
			this.Parameter_Name[1] = "ARG_FACTORY";
			this.Parameter_Name[2] = "ARG_SP_NAME";
			this.Parameter_Name[3] = "ARG_UPD_USER";
			this.Parameter_Name[4] = "ARG_ERR_DIV";
			this.Parameter_Name[5] = "OUT_CURSOR";

			this.Parameter_Type[0] = (int)OracleType.VarChar;
			this.Parameter_Type[1] = (int)OracleType.VarChar;
			this.Parameter_Type[2] = (int)OracleType.VarChar;
			this.Parameter_Type[3] = (int)OracleType.VarChar;
			this.Parameter_Type[4] = (int)OracleType.VarChar;
			this.Parameter_Type[5] = (int)OracleType.Cursor;

			this.Parameter_Values[0] = arg_division;
			this.Parameter_Values[1] = COM.ComVar.This_Factory;
			this.Parameter_Values[2] = arg_sp_name;
			this.Parameter_Values[3] = COM.ComVar.This_User;
			this.Parameter_Values[4] = arg_err_div;
			this.Parameter_Values[5] = "";



			this.Add_Select_Parameter(true);
			DS_Ret = Exe_Select_Procedure();
			
			if(DS_Ret.Tables[Proc_Name].Rows.Count > 0)
				return true;
			else
				return false;
		}
        
	}
}
