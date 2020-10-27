using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Data.OracleClient;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;

namespace COM
{

	/// <summary>
	/// FSP : C1FlexGrid를 상속받아 부가기능 추가
	/// </summary>
	public class FSP : C1.Win.C1FlexGrid.C1FlexGrid
	{
		
		#region 변수 정의

		OraDB MyOraDB = new OraDB();

		/// <summary>
		/// Buffer_CellData : 그리드의 특정셀의 이전값 보존
		/// </summary>
		public string Buffer_CellData = "";

		/// <summary>
		/// arr_essential : 필수 컬럼 인덱스 보존
		/// </summary>
		public string[] arr_essential ;

		#endregion

		public FSP()
		{
			//
			// TODO: 여기에 생성자 논리를 추가합니다.
			//
		}


		#region 사용자 메소드 정의
/*
		/// <summary>
		/// Set_Action_Image : FlexGrid에 Set Action Image (I, D, U)
		/// </summary>
		/// <param name="arg_imglist">이미지 리스트</param>
		public  void Set_Action_Image(ImageList arg_imglist)
		{
			Hashtable Imgmap = new Hashtable();
			try
			{
				Imgmap.Clear();

				Imgmap.Add("I", arg_imglist.Images[0]); 
				Imgmap.Add("D", arg_imglist.Images[1]);
				Imgmap.Add("U", arg_imglist.Images[2]);

				this.Cols[0].ImageMap = Imgmap;
				this.Cols[0].ImageAndText = false;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message.ToString(),"Set_Action_Image",MessageBoxButtons.OK,MessageBoxIcon.Error);
			}
		}



		/// <summary>
		/// Set_Action_Image : FlexGrid에 Set Action Image (I, D, U)
		/// </summary>
		/// <param name="arg_imglist">이미지 리스트</param>
		/// <param name="arg_add">이미지 리스트에 이미지 추가 여부</param>
		/// <returns>이미지 리스트 : 이미지 리스트에 추가 리스트 추가하기 위함</returns>
		public Hashtable Set_Action_Image(ImageList arg_imglist, bool arg_add)
		{
			Hashtable Imgmap = new Hashtable();
			try
			{
				Imgmap.Clear();

				Imgmap.Add("I", arg_imglist.Images[0]); 
				Imgmap.Add("D", arg_imglist.Images[1]);
				Imgmap.Add("U", arg_imglist.Images[2]);

				this.Cols[0].ImageMap = Imgmap;
				this.Cols[0].ImageAndText = false;

				return Imgmap;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message.ToString(),"Set_Action_Image",MessageBoxButtons.OK,MessageBoxIcon.Error);
				return null;
			}
		}



		/// <summary>
		/// Mark_Grid_Menu : PopUp Menu (Context Menu) 있는 그리드 표시
		/// </summary>
		public void Mark_Grid_Menu()
		{
			try
			{
				for(int i = 0; i < this.Rows.Fixed; i++) this[i, 0] = COM.ComVar.MarkGrid_Symbol;
				this.GetCellRange(0, 0, this.Rows.Fixed - 1, 0).StyleNew.BackColor = COM.ComVar.MarkGrid_BackColor;
				this.GetCellRange(0, 0, this.Rows.Fixed - 1, 0).StyleNew.ForeColor = COM.ComVar.MarkGrid_ForeColor;
			}
			catch
			{
			} 
		}



		/// <summary>
		/// Add_Row : 행 추가
		/// </summary>
		/// <param name="arg_row">추가하고자 하는 Row</param>
		public  void Add_Row(int arg_row)
		{
 			
			string[] newdata = new string[this.Cols.Count];
			try 
			{
				
				newdata[0] = "I";

				for(int i=1 ;i< newdata.Length; i++)
				{
					newdata[i] = "";
				}
 
				this.AddItem(newdata, arg_row + 1, 0);
				this.Row = arg_row + 1; 
 
			}

			catch (Exception ex)
			{
				MessageBox.Show(ex.Message.ToString(),"Add_Row",MessageBoxButtons.OK,MessageBoxIcon.Error);
			}
 
		}


//		/// <summary>
//		/// Delete_Row : 행 삭제 표시
//		/// </summary>
//		/// <param name="arg_row">삭제하고자 하는 Row</param>
//		public  void Delete_Row(int arg_row)
//		{
//			try
//			{
//				if (this[arg_row, 0].ToString() != "I")
//				{
//					this[arg_row, 0] = "D";
//				}
//
//			}
//
//			catch (Exception ex)
//			{
//				MessageBox.Show(ex.Message.ToString(),"Delete_Row",MessageBoxButtons.OK,MessageBoxIcon.Error);
//			}
//
//		}

		/// <summary>
		/// Delete_Row : 행 삭제 표시
		/// </summary>
		/// <param name="arg_row">삭제하고자 하는 Row</param>
		public  void Delete_Row(int arg_row)
		{
			try
			{
				if (this[arg_row, 0] == null)
				{
					this[arg_row, 0] = "D";
				}
				if (this[arg_row, 0].ToString() != "I")
				{
					this[arg_row, 0] = "D";
				}
			}

			catch (Exception ex)
			{
				MessageBox.Show(ex.Message.ToString(),"Delete_Row",MessageBoxButtons.OK,MessageBoxIcon.Error);
			}

		}

		/// <summary>
		/// Delete_Row : 행 삭제 표시(그리드 선택된 범위내)
		/// </summary> 
		public  void Delete_Row()
		{
					
			int sel_r1 = this.Selection.r1;
			int sel_r2 = this.Selection.r2;
			
			int start_row, end_row;

			try
			{

				start_row = (sel_r1 < sel_r2) ? sel_r1 : sel_r2;
				end_row = (sel_r1 < sel_r2) ? sel_r2 : sel_r1;

				for(int i = start_row; i <= end_row; i++)
				{
					if (this[i, 0] == null)
					{
						this[i, 0] = "D";
					}
					if (this[i, 0].ToString() != "I")
					{
						this[i, 0] = "D";
					}
				} 
 
			}

			catch (Exception ex)
			{
				MessageBox.Show(ex.Message.ToString(),"Delete_Row",MessageBoxButtons.OK,MessageBoxIcon.Error);
			}

		}

        
        //public void Update_Row(int arg_row);
		public  void Update_Row(int arg_row)
		{	
			try
			{
				if (this[arg_row, 0] == null)
				{
					this[arg_row, 0] = "U";
				}

				if (this[arg_row, 0].ToString() != "I")
				{
					this[arg_row, 0] = "U";
				}
 
			}

			catch (Exception ex)
			{
				MessageBox.Show( ex.Message.ToString(),"Update_Row",MessageBoxButtons.OK,MessageBoxIcon.Error);
			}

		}

		/// <summary>
		/// Update_Row : 행 수정 표시(그리드 선택된 행)
		/// </summary> 
        //public void Update_Row();
        public  void Update_Row()
		{

			int sel_row = this.Selection.r1;
			int sel_col = this.Selection.c1;
					
			try
			{
				if(this[sel_row, 0] == null) this[sel_row, 0] = "";
				if(this[sel_row, 0].ToString() == "I") return;

				if (this.Cols[sel_col].DataType != typeof(string))  
				{
					this[sel_row, 0] = "U";
					Buffer_CellData = "";
				}
				else
				{

					//if ((Buffer_CellData.Length != 0) && 

					if (this[sel_row, sel_col].ToString() != Buffer_CellData)  
					{
						this[sel_row, 0] = "U";
						Buffer_CellData = "";
					}
				}
 
			}

			catch (Exception ex)
			{
				MessageBox.Show( ex.Message.ToString(),"Update_Row",MessageBoxButtons.OK,MessageBoxIcon.Error);
			}

		}



	
		/// <summary>
		/// 리스트 항목을 스트링으로 변환
		/// </summary>
		/// <param name="arg_div">리스트추출 유형[0:공통코드,1:쿼리문장, 2:공통코드(코드 : 코드명)]</param>
		/// <param name="arg_dt">리스트 항목</param>
		/// <returns>리스트 문자열</returns>
		public string Make_CmbDataList(ComVar.ComboList_Type arg_div, DataTable arg_dt) 
		{
			string rtn_list;

			int sel_code = 0;
			int sel_name = 0;

			try
			{
				switch(arg_div)
				{
					case ComVar.ComboList_Type.ComCode :   //공통코드에서

						sel_code = (int)TBSCM_CODE.IxCOM_VALUE1;

						break;

					case ComVar.ComboList_Type.Query  :   //쿼리문장에서

						sel_code = 0;

						break;

					case ComVar.ComboList_Type.ComCode_Name : //공통코드에서 코드 : 코드명 

						sel_code = (int)TBSCM_CODE.IxCOM_VALUE1;
						sel_name = (int)TBSCM_CODE.IxCOM_DESC1;

						break;


                    case ComVar.ComboList_Type.Query_Name: //공통코드에서 코드 : 코드명 

                        sel_code = (int)TBSCM_CODE.IxCOM_VALUE1;
                        sel_name = (int)TBSCM_CODE.IxCOM_DESC1;

                        break;



				}

				switch(arg_div)
				{
					case ComVar.ComboList_Type.ComCode_Name:

						//rtn_list = " |" + arg_dt.Rows[0].ItemArray[sel_code].ToString() + " : " + arg_dt.Rows[0].ItemArray[sel_name].ToString();

						rtn_list = arg_dt.Rows[0].ItemArray[sel_code].ToString() + " : " + arg_dt.Rows[0].ItemArray[sel_name].ToString();

						for(int i = 1; i < arg_dt.Rows.Count; i++)
						{
							rtn_list = rtn_list + "|" + arg_dt.Rows[i].ItemArray[sel_code].ToString() + " : " + arg_dt.Rows[i].ItemArray[sel_name].ToString(); 
						}
						

						break;

					default:

						//rtn_list = " |" + arg_dt.Rows[0].ItemArray[sel_code].ToString();

						rtn_list = arg_dt.Rows[0].ItemArray[sel_code].ToString();

						for(int i = 1; i < arg_dt.Rows.Count; i++)
						{
							rtn_list = rtn_list + "|" + arg_dt.Rows[i].ItemArray[sel_code].ToString();
						}

						break;

				} 
	
				return rtn_list;
			}

			catch (Exception ex)
			{
				MessageBox.Show( ex.Message.ToString(),"Make_CmbDataList",MessageBoxButtons.OK,MessageBoxIcon.Error);
				return null;
			}

		}


//		/// <summary>
//		/// 리스트 항목을 스트링으로 변환
//		/// </summary>
//		/// <param name="arg_div">리스트추출 유형[0:공통코드,1:쿼리문장, 2:공통코드(코드 : 코드명)]</param>
//		/// <param name="arg_dt">리스트 항목</param> 
//		/// <param name="arg_col"></param>
//		public void Make_CmbDataList(ComVar.ComboList_Type arg_div, DataTable arg_dt, int arg_col) 
//		{ 
//			int sel_code = 0;
//			int sel_name = 0;
//
//			try
//			{
//				switch(arg_div)
//				{
//					case ComVar.ComboList_Type.ComCode :   //공통코드에서
//
//						sel_code = (int)TBSCM_CODE.IxCOM_VALUE1;
//
//						break;
//
//					case ComVar.ComboList_Type.Query  :   //쿼리문장에서
//
//						sel_code = 0;
//
//						break;
//
//					case ComVar.ComboList_Type.ComCode_Name : //공통코드에서 코드 : 코드명 
//
//						sel_code = (int)TBSCM_CODE.IxCOM_VALUE1;
//						sel_name = (int)TBSCM_CODE.IxCOM_DESC1;
//
//						break;
//
//				}
//
//
//				System.Collections.Specialized.ListDictionary ld = new System.Collections.Specialized.ListDictionary(); 
//
//				switch(arg_div)
//				{
//					case ComVar.ComboList_Type.ComCode :   
//						
//						for(int i = 0; i < arg_dt.Rows.Count; i++)
//						{
//							ld.Add(arg_dt.Rows[i].ItemArray[sel_code].ToString(), arg_dt.Rows[i].ItemArray[sel_code].ToString());
//						} 
//			  
//						break;
//
//					case ComVar.ComboList_Type.Query  :   //쿼리문장에서
//
//						for(int i = 0; i < arg_dt.Rows.Count; i++)
//						{
//							//"code" or "code : desc" 형태일때
//							string[] token = arg_dt.Rows[i].ItemArray[sel_code].ToString().Split(':');
//
//							if(token.Length == 1)
//							{
//								ld.Add(token[0], token[0]);
//							}
//							else
//							{
//								ld.Add(token[0].Trim(), token[1].Trim());
//							}
//
//						}
//
//						break;
//
//					case ComVar.ComboList_Type.ComCode_Name :  
//						 
//						for(int i = 0; i < arg_dt.Rows.Count; i++)
//						{
//							ld.Add(arg_dt.Rows[i].ItemArray[sel_code].ToString(), arg_dt.Rows[i].ItemArray[sel_name].ToString());
//						}
//
//						break;
//
//				}
//
//				
//				this.Cols[arg_col].DataMap = ld; 
//				//this.Cols[arg_col].Caption = "ListDictionary";
//				
//			
//				
//  
//			}
//
//			catch (Exception ex)
//			{
//				MessageBox.Show( ex.Message.ToString(),"Make_CmbDataList",MessageBoxButtons.OK,MessageBoxIcon.Error);
//			}
//
//		}



		/// <summary>
		/// 리스트 항목을 스트링으로 변환
		/// </summary>
		/// <param name="arg_div">리스트추출 유형[0:공통코드,1:쿼리문장, 2:공통코드(코드 : 코드명)]</param>
		/// <param name="arg_dt">리스트 항목</param> 
		/// <param name="arg_col"></param>
		public void Make_CmbDataList(ComVar.ComboList_Type arg_div, DataTable arg_dt, int arg_col) 
		{ 
			int sel_code = 0;
			int sel_name = 0;

			try
			{
				switch(arg_div)
				{
					case ComVar.ComboList_Type.ComCode :   //공통코드에서

						sel_code = (int)TBSCM_CODE.IxCOM_VALUE1;

						break;

						//****************** 박지수 수정분  		
					
					case ComVar.ComboList_Type.Query  :   //쿼리문장에서

						if(arg_dt.Columns.Count > 1)
						{
							sel_name = 1;
						}
						else
						{
							sel_name = 0;
						}

						sel_code = 0;

						break;
						//******************   							
		
					case ComVar.ComboList_Type.ComCode_Name : //공통코드에서 코드 : 코드명 

						sel_code = (int)TBSCM_CODE.IxCOM_VALUE1;
						sel_name = (int)TBSCM_CODE.IxCOM_DESC1;

						break;


                    case ComVar.ComboList_Type.Query_Name: //공통코드에서 코드 : 코드명 

                        sel_code = (int)TBSCM_CODE.IxCOM_VALUE1;
                        sel_name = (int)TBSCM_CODE.IxCOM_DESC1;

                        break;


				}


				System.Collections.Specialized.ListDictionary ld = new System.Collections.Specialized.ListDictionary(); 

				ld.Add("", "");

				switch(arg_div)
				{
					case ComVar.ComboList_Type.ComCode :   
						
						for(int i = 0; i < arg_dt.Rows.Count; i++)
						{
							ld.Add(arg_dt.Rows[i].ItemArray[sel_code].ToString(), arg_dt.Rows[i].ItemArray[sel_code].ToString());
						} 
			  
						break;

					case ComVar.ComboList_Type.Query  :   //쿼리문장에서

						//****************** 박지수 수정분  							

						if (sel_name.Equals(0))
						{
							for(int i = 0; i < arg_dt.Rows.Count; i++)
							{
								//"code" or "code : desc" 형태일때
								string[] token = arg_dt.Rows[i].ItemArray[sel_code].ToString().Split(':');

								if(token.Length == 1)
								{
									ld.Add(token[0], token[0]);
								}
								else
								{
									ld.Add(token[0].Trim(), token[1].Trim());
								}

							}
						}
						else
						{
							//******************
							for(int i = 0; i < arg_dt.Rows.Count; i++)
							{
								ld.Add(arg_dt.Rows[i].ItemArray[sel_code].ToString(), arg_dt.Rows[i].ItemArray[sel_name].ToString());
							}
						}

						break;

					case ComVar.ComboList_Type.ComCode_Name :  
						 
						for(int i = 0; i < arg_dt.Rows.Count; i++)
						{
							ld.Add(arg_dt.Rows[i].ItemArray[sel_code].ToString(), arg_dt.Rows[i].ItemArray[sel_name].ToString());
						}

						break;

                    case ComVar.ComboList_Type.Query_Name:

                        for (int i = 0; i < arg_dt.Rows.Count; i++)
                        {
                            ld.Add(arg_dt.Rows[i].ItemArray[sel_code].ToString(), arg_dt.Rows[i].ItemArray[sel_name].ToString());
                        }

                        break;



				}

				
				this.Cols[arg_col].DataMap = ld; 
				//this.Cols[arg_col].Caption = "ListDictionary";
				
			
				
  
			}

			catch (Exception ex)
			{
				MessageBox.Show( ex.Message.ToString(),"Make_CmbDataList",MessageBoxButtons.OK,MessageBoxIcon.Error);
			}

		}



		/// <summary>
		/// Make_Query : string으로 받은 쿼리문장에서 @값 추출해서 실제 값 적용 -> 쿼리 실행해서 DataTable 로 반환
		/// </summary>
		/// <param name="arg_query">받은 쿼리문장</param>
		/// <returns>DataTable</returns>
		public DataTable Make_Query(string arg_query)
		{
			DataSet DS_Ret ;

			int index = 0; 

			string strDelimiter = " ";
			char[] delimiter = strDelimiter.ToCharArray();
 
			try
			{
				string[] tokenArray = arg_query.Split(delimiter); 
				string[] query_data = new string[tokenArray.Length]; 

				string real_query = "";
				DataTable return_dt;

				//--------------------------------------------------------------------------------
				//1. 공백으로 먼저 자르기
				//-------------------------------------------------------------------------------- 

				foreach( string token in tokenArray )
				{
					if ( !token.Equals("") || !token.Equals(null) ) 
					{
						query_data[index] = token;
						index++;
					} 
				}


				//--------------------------------------------------------------------------------
				//2. @ 들어있는 query_data 추출
				//-------------------------------------------------------------------------------- 

				for(int i = 0; i < query_data.Length; i++)
				{
					if(query_data[i] == null || query_data[i] == "") continue;

					if("@" == query_data[i].Substring(0, 1))
					{
						query_data[i] = Change_RealValue(query_data[i]);
					}
				}


				//--------------------------------------------------------------------------------
				//3. 실 데이터 값 넣어서 쿼리 만들기 -> 실행
				//-------------------------------------------------------------------------------- 

				for(int i = 0; i < query_data.Length; i++)
				{
					if(query_data[i] == null || query_data[i] == "") continue;

					real_query = real_query + query_data[i] + " ";
				}


				DS_Ret = this.MyOraDB.Exe_Select_Query(real_query);
				if(DS_Ret == null) return null;

				return return_dt = DS_Ret.Tables[0];
			}
			catch (Exception ex)
			{
				MessageBox.Show( ex.Message.ToString(),"Make_Query",MessageBoxButtons.OK,MessageBoxIcon.Error);
				return null;
			}

		}



		/// <summary>
		/// Change_RealValue : 구분자 들어있는 데이터를 실 데이터 값으로 치환
		/// </summary>
		/// <param name="arg_data">@포함 문자열</param>
		/// <returns></returns>
		public string Change_RealValue(string arg_data)
		{
			string return_value = "";

			switch(arg_data)
			{
				case "@factory":

					return_value = "'" + ComVar.This_Factory + "'";

					break;

                case "@Area_CD":

                    return_value = "'" + ComVar.This_Area_CD + "'";

                    break;

                case "@gen":

                    return_value = "'" + ComVar.This_Gen + "'";

                    break;
 
			}

			return return_value;
		}


		/// <summary>
		/// Set_Grid : 그리드 설정
		/// </summary>
		/// <param name="arg_pgid">적용시킬 프로그램 아이디</param>
		/// <param name="arg_pgseq">적용시킬 프로그램 순번</param>
		/// <param name="arg_hcount">그리드 헤더 수</param>
		/// <param name="arg_lang">언어코드</param>
		/// <param name="arg_autosize">자동 컬럼 너비 맞추기 여부</param>
		public  void Set_Grid( string arg_pgid, string arg_pgseq, int arg_hcount, string arg_lang, bool arg_autosize)
		{
			
			DataTable dt_list, dt_cmblist; 
			CellStyle cellst; 

			try
			{
				////// DB에서 그리드 정보 추출 
				dt_list =this.MyOraDB.Select_GridHead(arg_pgid,arg_pgseq);
				if (dt_list== null) return ;
	
				if(dt_list.Rows.Count > 0)
				{
					this.Clear(C1.Win.C1FlexGrid.ClearFlags.All); 
					this.Cols.Count = dt_list.Rows.Count + 1; 
					this.Rows.Count = arg_hcount + 1;
					this.Rows.Fixed = arg_hcount + 1;
					this.Rows[0].Visible = false;
				  
					this.Rows[1].TextAlign = TextAlignEnum.CenterCenter;

					if (arg_hcount==2)		// 2번째 Header
					{
						this.Rows[2].TextAlign = TextAlignEnum.CenterCenter;
					}

					if (arg_hcount==3)		// 3번째 Header
					{
						this.Rows[2].TextAlign = TextAlignEnum.CenterCenter;
						this.Rows[3].TextAlign = TextAlignEnum.CenterCenter;
					}

					if (arg_hcount==4)		// 4번째 Header
					{
						this.Rows[2].TextAlign = TextAlignEnum.CenterCenter;
						this.Rows[3].TextAlign = TextAlignEnum.CenterCenter;
						this.Rows[4].TextAlign = TextAlignEnum.CenterCenter;
					}

					//--------------------------------------------------
					//전체 속성 지정
					this.Cols.Fixed = ComVar.GridCol_Fixed ; 
					this.Cols[0].Width = ComVar.GridCol0_Width ;  
					//this.Cols[0].StyleNew.BackColor = ComVar.GridCol0_Color ;  

					this.Cols.Frozen = Convert.ToInt32(dt_list.Rows[0].ItemArray[(int)TBSCM_TABLE.IxFROZENCOL].ToString());	// 칼럼 Frozen
					this.Rows.Frozen = Convert.ToInt32(dt_list.Rows[0].ItemArray[(int)TBSCM_TABLE.IxFROZENROW].ToString());	// 행 Frozen
				
					//-------------------------------------------------
					//Column 속성 설정
					//TEXT
					cellst = this.Styles.Add("TEXT");
					cellst.DataType = typeof(string);		// Type.GetType("System.String");

					//DATE
					cellst = this.Styles.Add("DATE");
					cellst.DataType = typeof(DateTime);		//Type.GetType("System.DateTime");
					cellst.Format = "yyyyMMdd";

					//CHECKBOX
					cellst = this.Styles.Add("CHECKBOX");
					cellst.DataType = typeof(bool);			//Type.GetType("System.Boolean"); 
					//-------------------------------------------------


					arr_essential = new string[dt_list.Rows.Count+1] ;
					
					for(int i = 1; i < dt_list.Rows.Count + 1; i++)
					{
						 
						
						arr_essential[i] = (dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxESSENTIAL_YN] == null) ? "" : dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxESSENTIAL_YN].ToString() ;
				
						//cell type
						switch(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxCELLTYPE].ToString())				// Cell Type
						{
							case "TEXT":
								this.Cols[i].Style = this.Styles["TEXT"];
								break;

							case "DATE":
								this.Cols[i].Style = this.Styles["DATE"];
								break;

							case "CHECKBOX":
								this.Cols[i].Style = this.Styles["CHECKBOX"];
								break;

							case "COMBOBOX":

							switch(Convert.ToInt32(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxDATA_LIST_TYPE].ToString()))	// data_list_type
							{
								case (int)ComVar.ComboList_Type.ComCode :      //공통코드에서 ComboList 추출
										
									if(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxDATA_LIST_CD].ToString() != "")					// Data_LIst_Cd
									{
										//combo_list
										dt_cmblist = this.MyOraDB.Select_ComCode(ComVar.This_Factory, dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxDATA_LIST_CD].ToString());
										if(dt_cmblist.Rows.Count != 0) this.Cols[i].ComboList = this.Make_CmbDataList(ComVar.ComboList_Type.ComCode, dt_cmblist);
									}

									break;

								case (int)ComVar.ComboList_Type.Query :      //쿼리에서 ComboList 추출	
										
									if(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxDATA_LIST_QUERY].ToString() != "")				//Data_List_Query
									{
										//									//combo_list
											
										dt_cmblist = Make_Query(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxDATA_LIST_QUERY].ToString().Trim());
										if(dt_cmblist.Rows.Count != 0) this.Cols[i].ComboList = this.Make_CmbDataList(ComVar.ComboList_Type.Query, dt_cmblist);
									}

									break;

								case (int)ComVar.ComboList_Type.ComCode_Name :
										
									if(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxDATA_LIST_CD].ToString() != "")					// Data_LIst_Cd
									{
										//combo_list
										dt_cmblist = this.MyOraDB.Select_ComCode(ComVar.This_Factory, dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxDATA_LIST_CD].ToString());
										if(dt_cmblist.Rows.Count != 0) this.Cols[i].ComboList = this.Make_CmbDataList(ComVar.ComboList_Type.ComCode_Name, dt_cmblist);
									}

									break;

							}
 
								break;

							default:
								break;
						} //end switch
					
						//-------------------------------------------------------------------------------

						this.Cols[i].Width = Convert.ToInt32(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxWIDTH].ToString());
						this.Cols[i].AllowEditing = Convert.ToBoolean(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxLOCK_YN]);    // 칼럼 에디터 가능 여부
						this.Cols[i].Visible = Convert.ToBoolean(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxVISIBLE_YN]);			// 칼럼 visible 
						this.Cols[i].AllowSorting = Convert.ToBoolean(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxAUTOSORT_YN]);	// 칼럼 별자동 sort

						switch(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxHALIGN].ToString())									// 칼럼정렬
						{
							case "LEFT":
								this.Cols[i].TextAlign = TextAlignEnum.LeftCenter; 
								this.Cols[i].ImageAlign = ImageAlignEnum.LeftCenter;
								break;

							case "CENTER":
								this.Cols[i].TextAlign = TextAlignEnum.CenterCenter;
								this.Cols[i].ImageAlign = ImageAlignEnum.CenterCenter;
								break;

							case "RIGHT":
								this.Cols[i].TextAlign = TextAlignEnum.RightCenter;
								this.Cols[i].ImageAlign = ImageAlignEnum.RightCenter;
								break;

							default:
								break;
						}

					 
					

						//헤더 데이터
						this[0, i] = dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxCOL_NAME].ToString();					// 테이블 칼럼명
 
							
						this[1, i] = dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxHEAD_DESC1].ToString();					// 상단

						if(arg_hcount == 2)	
						{
							this[2, i] = dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxHEAD_DESC2].ToString();				// 하단
						}

						if(arg_hcount == 3)	
						{
							this[2, i] = dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxHEAD_DESC2].ToString();	
							this[3, i] = dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxHEAD_DESC3].ToString();				// 하단
						}

						if(arg_hcount == 4)	
						{
							this[2, i] = dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxHEAD_DESC2].ToString();	
							this[3, i] = dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxHEAD_DESC3].ToString();
							this[4, i] = dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxHEAD_DESC4].ToString();				// 하단
						}
 
					

						//등록된 Title Header에 backcolor,forecolor 설정
						if(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxBACKCOLOR].ToString() != "")							// 배경색
						{
							this.GetCellRange(1, i).StyleNew.BackColor = Color.FromArgb(Convert.ToInt32(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxBACKCOLOR].ToString()));

							if(arg_hcount == 2)
							{
								this.GetCellRange(2, i).StyleNew.BackColor = Color.FromArgb(Convert.ToInt32(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxBACKCOLOR].ToString()));
							}

							if(arg_hcount == 3)
							{
								this.GetCellRange(2, i, 3, i).StyleNew.BackColor = Color.FromArgb(Convert.ToInt32(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxBACKCOLOR].ToString()));
							}

							if(arg_hcount == 4)
							{
								this.GetCellRange(2, i, 4, i).StyleNew.BackColor = Color.FromArgb(Convert.ToInt32(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxBACKCOLOR].ToString()));
							}

						}

						if(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxFORECOLOR].ToString() != "")							// 글자색
						{
							this.GetCellRange(1, i).StyleNew.ForeColor = Color.FromArgb(Convert.ToInt32(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxFORECOLOR].ToString()));

							if(arg_hcount == 2)
							{
								this.GetCellRange(2, i).StyleNew.ForeColor = Color.FromArgb(Convert.ToInt32(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxFORECOLOR].ToString()));
							}

							if(arg_hcount == 3)
							{
								this.GetCellRange(2, i, 3, i).StyleNew.ForeColor = Color.FromArgb(Convert.ToInt32(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxFORECOLOR].ToString()));
							}

							if(arg_hcount == 4)
							{
								this.GetCellRange(2, i, 4, i).StyleNew.ForeColor = Color.FromArgb(Convert.ToInt32(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxFORECOLOR].ToString()));
							}

						 

						}


					} //end for


					if(arg_autosize)
					{
						this.AutoSizeCols();
					} 
				
					this.ExtendLastCol = true;		// 그리드 끝에 빈공간없이 last column에 맞춤
					//this.ExtendLastCol = arg_autosize;

				}
				else 
				{	// 그리드 정보 없슴을 상태 바에 출력

				}//end if

			
			}	
			catch (Exception ex)
			{
				MessageBox.Show( ex.Message.ToString(),"Set_Grid",MessageBoxButtons.OK,MessageBoxIcon.Error);
				
			}
		}





		/// <summary>
		/// Set_Grid : 그리드 설정
		/// </summary>
		/// <param name="arg_pgid">적용시킬 프로그램 아이디</param>
		/// <param name="arg_pgseq">적용시킬 프로그램 순번</param>
		/// <param name="arg_hcount">그리드 헤더 수</param>
		/// <param name="arg_lang">언어코드</param> 
		/// <param name="arg_type">그리드 타입 (Search, Modify)</param> 
		/// <param name="arg_autosize">자동 컬럼 너비 맞추기 여부</param>
		public  void Set_Grid( string arg_pgid, string arg_pgseq, int arg_hcount, string arg_lang, ComVar.Grid_Type arg_type, bool arg_autosize)
		{
			
			DataTable dt_list, dt_cmblist; 
			CellStyle cellst; 

			//신규 스타일 생성시 임의로 일련번호 추가해서 생성
			int cellst_count = 0;


			try
			{
				////// DB에서 그리드 정보 추출 
				dt_list =this.MyOraDB.Select_GridHead(arg_pgid,arg_pgseq);
				if (dt_list== null) return ;
	
				if(dt_list.Rows.Count > 0)
				{
					this.Clear(C1.Win.C1FlexGrid.ClearFlags.All); 
					this.Cols.Count = dt_list.Rows.Count + 1; 
					this.Rows.Count = arg_hcount + 1;
					this.Rows.Fixed = arg_hcount + 1;
					this.Rows[0].Visible = false;
					this.Cols[0].AllowEditing = false;
				 

					#region  그리드 색

					this.Styles.EmptyArea.BackColor = COM.ComVar.GridEmptyColor;
					this.Styles.Alternate.BackColor = COM.ComVar.GridAlternate_Color;
					this.Styles.Highlight.BackColor = COM.ComVar.GridHigh_Color;
					this.Styles.Highlight.ForeColor = COM.ComVar.GridHighFore_Color;
					//this.Styles.Focus.BackColor = COM.ComVar.GridHigh_Color;
					//this.Styles.Focus.ForeColor = COM.ComVar.GridHighFore_Color;
					this.Styles.Fixed.ForeColor = COM.ComVar.GridForeColor;

					switch(arg_type)
					{
						case COM.ComVar.Grid_Type.ForModify:
							this.Styles.Fixed.BackColor = COM.ComVar.GridDarkFixed_Color;
							break;

						case COM.ComVar.Grid_Type.ForSearch:
							this.Styles.Fixed.BackColor = COM.ComVar.GridLightFixed_Color;
							break;
					}


					this.Cols[0].StyleNew.BackColor = COM.ComVar.GridCol0_Color;

 
					#endregion 
					#region 헤더 정렬

					this.Rows[1].TextAlign = TextAlignEnum.CenterCenter;

					if (arg_hcount==2)		// 2번째 Header
					{
						this.Rows[2].TextAlign = TextAlignEnum.CenterCenter;
					}

					if (arg_hcount==3)		// 3번째 Header
					{
						this.Rows[2].TextAlign = TextAlignEnum.CenterCenter;
						this.Rows[3].TextAlign = TextAlignEnum.CenterCenter;
					}

					if (arg_hcount==4)		// 4번째 Header
					{
						this.Rows[2].TextAlign = TextAlignEnum.CenterCenter;
						this.Rows[3].TextAlign = TextAlignEnum.CenterCenter;
						this.Rows[4].TextAlign = TextAlignEnum.CenterCenter;
					}

					#endregion 
					#region 속성 지정

					//--------------------------------------------------
					//전체 속성 지정
					this.Cols.Fixed = ComVar.GridCol_Fixed ; 
					this.Cols[0].Width = ComVar.GridCol0_Width ;  
					//this.Cols[0].StyleNew.BackColor = ComVar.GridCol0_Color ;  

					this.Cols.Frozen = Convert.ToInt32(dt_list.Rows[0].ItemArray[(int)TBSCM_TABLE.IxFROZENCOL].ToString());	// 칼럼 Frozen
					this.Rows.Frozen = Convert.ToInt32(dt_list.Rows[0].ItemArray[(int)TBSCM_TABLE.IxFROZENROW].ToString());	// 행 Frozen
				
					//-------------------------------------------------
					//Column 속성 설정 
					//alingment cellstyle
					//1. left
					cellst = this.Styles.Add("LEFT");
					cellst.TextAlign = TextAlignEnum.LeftCenter; 
					cellst.ImageAlign = ImageAlignEnum.LeftCenter; 

					//2. center
					cellst = this.Styles.Add("CENTER");
					cellst.TextAlign = TextAlignEnum.CenterCenter; 
					cellst.ImageAlign = ImageAlignEnum.CenterCenter; 

					//3. rigth
					cellst = this.Styles.Add("RIGHT");
					cellst.TextAlign = TextAlignEnum.RightCenter; 
					cellst.ImageAlign = ImageAlignEnum.RightCenter; 


					#endregion


					arr_essential = new string[dt_list.Rows.Count+1] ;
					
					for(int i = 1; i < dt_list.Rows.Count + 1; i++)
					{
						 
						
						arr_essential[i] = (dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxESSENTIAL_YN] == null) ? "" : dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxESSENTIAL_YN].ToString() ;
				

						#region 정렬

						switch(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxHALIGN].ToString())									// 칼럼정렬
						{
							case "LEFT":  
								this.Cols[i].Style = this.Styles["LEFT"]; 
								break;

							case "CENTER": 
								this.Cols[i].Style = this.Styles["CENTER"]; 
								break;

							case "RIGHT": 
								this.Cols[i].Style = this.Styles["RIGHT"]; 
								break; 
						} 
					  
						#endregion 


						this.Cols[i].Width = Convert.ToInt32(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxWIDTH].ToString());
						//this.Cols[i].AllowEditing = Convert.ToBoolean(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxLOCK_YN]);    // 칼럼 에디터 가능 여부

						if(Convert.ToBoolean(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxLOCK_YN]) )
						{
							// 컬럼 글자색 파란색으로 처리


							//기존 컬럼 스타일 상속받아서 새로운 스타일 생성, 임의로 일련번호 추가
							cellst = this.Styles.Add("EDIT" + cellst_count.ToString(), this.Cols[i].Style);

							//새로운 스타일의 속성
							cellst.DataType = typeof(string);
							cellst.ForeColor = COM.ComVar.ClrImportant;

							this.Cols[i].Style = this.Styles["EDIT" + cellst_count.ToString()]; 
								 
 
							this.Cols[i].AllowEditing = true; 
						}
						else
						{
							this.Cols[i].AllowEditing = false;
						}

						this.Cols[i].Visible = Convert.ToBoolean(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxVISIBLE_YN]);			// 칼럼 visible 
						this.Cols[i].AllowSorting = Convert.ToBoolean(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxAUTOSORT_YN]);	// 칼럼 별자동 sort

						//헤더 데이터
						this[0, i] = dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxCOL_NAME].ToString();					// 테이블 칼럼명
 

						
						#region cell type
 
						//스타일로 지정되어 정렬되어진 컬럼에 대해서
						//사용자 정의 스타일 동시에 적용시키려 할때
						//이전 스타일 제거되고 신규 스타일만 적용되므로
						//신규 스타일 추가시 이전 스타일 상속받아서 생성

						//신규 스타일로 적용했을때 신규 스타일 이름이 같은 경우
						//기존 정렬이 신규 스타일에 따라서 일괄적으로 변경되기 때문에
						//신규 스타일 생성시 임의로 일련번호 추가해서 생성

						switch(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxCELLTYPE].ToString())				// Cell Type
						{
							case "TEXT":
  
								//기존 컬럼 스타일 상속받아서 새로운 스타일 생성, 임의로 일련번호 추가
								cellst = this.Styles.Add("TEXT" + cellst_count.ToString(), this.Cols[i].Style);

								//새로운 스타일의 속성
								cellst.DataType = typeof(string);

								this.Cols[i].Style = this.Styles["TEXT" + cellst_count.ToString()]; 
								 
								break;

							case "DATE": 

								cellst = this.Styles.Add("DATE" + cellst_count.ToString(), this.Cols[i].Style);
								cellst.DataType = typeof(DateTime);
								cellst.Format = "yyyyMMdd";

								this.Cols[i].Style = this.Styles["DATE" + cellst_count.ToString()]; 
 
								break;

							case "CHECKBOX":
								
								cellst = this.Styles.Add("CHECKBOX" + cellst_count.ToString(), this.Cols[i].Style);
								cellst.DataType = typeof(bool); 

								this.Cols[i].Style = this.Styles["CHECKBOX" + cellst_count.ToString()]; 

								break;

							case "COMBOBOX":
								
								cellst = this.Styles.Add("COMBO_" + cellst_count.ToString(), this.Cols[i].Style);
								cellst.DataType = typeof(string);

								this.Cols[i].Style = this.Styles["COMBO_" + cellst_count.ToString()]; 
 
								
							switch(Convert.ToInt32(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxDATA_LIST_TYPE].ToString()))	// data_list_type
							{
								case (int)ComVar.ComboList_Type.ComCode :      //공통코드에서 ComboList 추출
											
									if(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxDATA_LIST_CD].ToString() != "")					// Data_LIst_Cd
									{
										//combo_list
										dt_cmblist = this.MyOraDB.Select_ComCode(ComVar.This_Factory, dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxDATA_LIST_CD].ToString());
										if(dt_cmblist.Rows.Count != 0) this.Make_CmbDataList(ComVar.ComboList_Type.ComCode, dt_cmblist, i);
									}

									break;

								case (int)ComVar.ComboList_Type.Query :      //쿼리에서 ComboList 추출	
											
									if(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxDATA_LIST_QUERY].ToString() != "")				//Data_List_Query
									{
												 
										dt_cmblist = Make_Query(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxDATA_LIST_QUERY].ToString().Trim());
										if(dt_cmblist.Rows.Count != 0) this.Make_CmbDataList(ComVar.ComboList_Type.Query, dt_cmblist, i);
									}

									break;

								case (int)ComVar.ComboList_Type.ComCode_Name :
											
									if(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxDATA_LIST_CD].ToString() != "")					// Data_LIst_Cd
									{
												 
										dt_cmblist = this.MyOraDB.Select_ComCode(ComVar.This_Factory, dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxDATA_LIST_CD].ToString());
										if(dt_cmblist.Rows.Count != 0) this.Make_CmbDataList(ComVar.ComboList_Type.ComCode_Name, dt_cmblist, i);
									}

									break;

							} 
 
								break;


							case "NUMBER":
								
								cellst = this.Styles.Add("NUMBER" + cellst_count.ToString(), this.Cols[i].Style);
								cellst.DataType = typeof(double);
								cellst.Format = "#,##0.##########"; 

								this.Cols[i].Style = this.Styles["NUMBER" + cellst_count.ToString()]; 

								break;

 
						} //end switch


						cellst_count++;
					  
						#endregion 
						#region 언어
 
						this[1, i] = dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxHEAD_DESC1].ToString();					// 상단

						if(arg_hcount == 2)	
						{
							this[2, i] = dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxHEAD_DESC2].ToString();				// 하단
						}

						if(arg_hcount == 3)	
						{
							this[2, i] = dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxHEAD_DESC2].ToString();	
							this[3, i] = dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxHEAD_DESC3].ToString();				// 하단
						}

						if(arg_hcount == 4)	
						{
							this[2, i] = dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxHEAD_DESC2].ToString();	
							this[3, i] = dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxHEAD_DESC3].ToString();
							this[4, i] = dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxHEAD_DESC4].ToString();				// 하단
						}
 
					
						#endregion 
						#region 타이틀 색깔 설정

						//등록된 Title Header에 backcolor,forecolor 설정
						if(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxBACKCOLOR].ToString() != "")							// 배경색
						{
							this.GetCellRange(1, i).StyleNew.BackColor = Color.FromArgb(Convert.ToInt32(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxBACKCOLOR].ToString()));

							if(arg_hcount == 2)
							{
								this.GetCellRange(2, i).StyleNew.BackColor = Color.FromArgb(Convert.ToInt32(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxBACKCOLOR].ToString()));
							}

							if(arg_hcount == 3)
							{
								this.GetCellRange(2, i, 3, i).StyleNew.BackColor = Color.FromArgb(Convert.ToInt32(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxBACKCOLOR].ToString()));
							}

							if(arg_hcount == 4)
							{
								this.GetCellRange(2, i, 4, i).StyleNew.BackColor = Color.FromArgb(Convert.ToInt32(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxBACKCOLOR].ToString()));
							}

						}

						if(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxFORECOLOR].ToString() != "")							// 글자색
						{
							this.GetCellRange(1, i).StyleNew.ForeColor = Color.FromArgb(Convert.ToInt32(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxFORECOLOR].ToString()));

							if(arg_hcount == 2)
							{
								this.GetCellRange(2, i).StyleNew.ForeColor = Color.FromArgb(Convert.ToInt32(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxFORECOLOR].ToString()));
							}

							if(arg_hcount == 3)
							{
								this.GetCellRange(2, i, 3, i).StyleNew.ForeColor = Color.FromArgb(Convert.ToInt32(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxFORECOLOR].ToString()));
							}

							if(arg_hcount == 4)
							{
								this.GetCellRange(2, i, 4, i).StyleNew.ForeColor = Color.FromArgb(Convert.ToInt32(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxFORECOLOR].ToString()));
							}

						 

						}


						#endregion


					} //end for


					if(arg_autosize)
					{
						this.AutoSizeCols();
					} 
				
					this.ExtendLastCol = true;		// 그리드 끝에 빈공간없이 last column에 맞춤 
					//this.ExtendLastCol = arg_autosize;

					this.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
					this.SelectionMode = SelectionModeEnum.ListBox;
					this.Font = new Font("Verdana", 9);
 
					//-------------------------------------------------------
					// merge
 
                    //this.AllowMerging = AllowMergingEnum.FixedOnly;

                    //for(int i = 0; i < this.Cols.Count; i++)
                    //{
                    //    this.Cols[i].AllowMerging = true;
                    //}

					
                    //for(int i = 0; i < this.Rows.Fixed; i++)
                    //{
                    //    this.Rows[i].AllowMerging = true;
                    //}  
                    
					//-------------------------------------------------------


				}
				else 
				{	// 그리드 정보 없음을 상태 바에 출력

				}//end if

			
			}	
			catch (Exception ex)
			{
				MessageBox.Show( ex.Message.ToString(),"Set_Grid",MessageBoxButtons.OK,MessageBoxIcon.Error);
				
			}
		}	





		/// <summary>
		/// Set_Grid : 공통 그리드 설정 
		/// </summary>
		/// <param name="arg_pgid">적용시킬 프로그램 아이디</param>
		/// <param name="arg_pgseq">적용시킬 프로그램 순번</param>
		/// <param name="arg_hcount">그리드 헤더 수</param>
		/// <param name="arg_lang">언어코드</param> 
		/// <param name="arg_type">그리드 타입 (Search, Modify)</param> 
		/// <param name="arg_autosize">자동 컬럼 너비 맞추기 여부</param>
		public  void Set_Grid_Comm( string arg_pgid, string arg_pgseq, int arg_hcount, string arg_lang, ComVar.Grid_Type arg_type, bool arg_autosize)
		{
			
			DataTable dt_list, dt_cmblist; 
			CellStyle cellst; 

			try
			{
				////// DB에서 그리드 정보 추출 
				dt_list =this.MyOraDB.Select_GridHead(arg_pgid,arg_pgseq);
				if (dt_list== null) return ;
	
				if(dt_list.Rows.Count > 0)
				{
					this.Clear(C1.Win.C1FlexGrid.ClearFlags.All); 
					this.Cols.Count = dt_list.Rows.Count + 1; 
					this.Rows.Count = arg_hcount + 1;
					this.Rows.Fixed = arg_hcount + 1;
					this.Rows[0].Visible = false;
				
 
					this.Styles.EmptyArea.BackColor = Color.White;
					this.Styles.Alternate.BackColor = Color.FromArgb(240, 244, 250);
					this.Styles.Highlight.BackColor = Color.FromArgb(193, 221, 253);
					this.Styles.Focus.BackColor = Color.FromArgb(193, 221, 253);
					this.Styles.Fixed.ForeColor = Color.White; 

					switch(arg_type)
					{
						case COM.ComVar.Grid_Type.ForModify:
							this.Styles.Fixed.BackColor = Color.FromArgb(122, 160, 200); 
							break;

						case COM.ComVar.Grid_Type.ForSearch:
							this.Styles.Fixed.BackColor = Color.FromArgb(135, 179, 234);
							break;
					}
 

					this.Cols[0].StyleNew.BackColor = Color.FromArgb(193, 221, 253);
 


					this.Rows[1].TextAlign = TextAlignEnum.CenterCenter;

					if (arg_hcount==2)		// 2번째 Header
					{
						this.Rows[2].TextAlign = TextAlignEnum.CenterCenter;
					}

					if (arg_hcount==3)		// 3번째 Header
					{
						this.Rows[2].TextAlign = TextAlignEnum.CenterCenter;
						this.Rows[3].TextAlign = TextAlignEnum.CenterCenter;
					}

					if (arg_hcount==4)		// 4번째 Header
					{
						this.Rows[2].TextAlign = TextAlignEnum.CenterCenter;
						this.Rows[3].TextAlign = TextAlignEnum.CenterCenter;
						this.Rows[4].TextAlign = TextAlignEnum.CenterCenter;
					}

					//--------------------------------------------------
					//전체 속성 지정
					this.Cols.Fixed = ComVar.GridCol_Fixed ; 
					this.Cols[0].Width = ComVar.GridCol0_Width ;  
					//this.Cols[0].StyleNew.BackColor = ComVar.GridCol0_Color ;  

					this.Cols.Frozen = Convert.ToInt32(dt_list.Rows[0].ItemArray[(int)TBSCM_TABLE.IxFROZENCOL].ToString());	// 칼럼 Frozen
					this.Rows.Frozen = Convert.ToInt32(dt_list.Rows[0].ItemArray[(int)TBSCM_TABLE.IxFROZENROW].ToString());	// 행 Frozen
				
					//-------------------------------------------------
					//Column 속성 설정
					//TEXT
					cellst = this.Styles.Add("TEXT");
					cellst.DataType = typeof(string);		 

					//DATE
					cellst = this.Styles.Add("DATE");
					cellst.DataType = typeof(DateTime);		 
					cellst.Format = "yyyyMMdd";

					//CHECKBOX
					cellst = this.Styles.Add("CHECKBOX");
					cellst.DataType = typeof(bool);			 
					//-------------------------------------------------


					arr_essential = new string[dt_list.Rows.Count+1] ;
					
					for(int i = 1; i < dt_list.Rows.Count + 1; i++)
					{
						 
						
						arr_essential[i] = (dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxESSENTIAL_YN] == null) ? "" : dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxESSENTIAL_YN].ToString() ;
				
						//cell type
						switch(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxCELLTYPE].ToString())				// Cell Type
						{
							case "TEXT":
								this.Cols[i].Style = this.Styles["TEXT"];
								break;

							case "DATE":
								this.Cols[i].Style = this.Styles["DATE"];
								break;

							case "CHECKBOX":
								this.Cols[i].Style = this.Styles["CHECKBOX"];
								break;

							case "COMBOBOX":

							switch(Convert.ToInt32(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxDATA_LIST_TYPE].ToString()))	// data_list_type
							{
								case (int)ComVar.ComboList_Type.ComCode :      //공통코드에서 ComboList 추출
										
									if(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxDATA_LIST_CD].ToString() != "")					// Data_LIst_Cd
									{
										//combo_list
										dt_cmblist = this.MyOraDB.Select_ComCode(ComVar.This_Factory, dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxDATA_LIST_CD].ToString());
										this.Cols[i].ComboList = this.Make_CmbDataList(ComVar.ComboList_Type.ComCode, dt_cmblist);
									}

									break;

								case (int)ComVar.ComboList_Type.Query :      //쿼리에서 ComboList 추출	
										
									if(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxDATA_LIST_QUERY].ToString() != "")				//Data_List_Query
									{
										//									//combo_list
											
										dt_cmblist = Make_Query(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxDATA_LIST_QUERY].ToString().Trim());
										this.Cols[i].ComboList = this.Make_CmbDataList(ComVar.ComboList_Type.Query, dt_cmblist);
									}

									break;

								case (int)ComVar.ComboList_Type.ComCode_Name :
										
									if(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxDATA_LIST_CD].ToString() != "")					// Data_LIst_Cd
									{
										//combo_list
										dt_cmblist = this.MyOraDB.Select_ComCode(ComVar.This_Factory, dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxDATA_LIST_CD].ToString());
										this.Cols[i].ComboList = this.Make_CmbDataList(ComVar.ComboList_Type.ComCode_Name, dt_cmblist);
									}

									break;

							}
 
								break;

							default:
								break;
						} //end switch
					
						//-------------------------------------------------------------------------------

						this.Cols[i].Width = Convert.ToInt32(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxWIDTH].ToString());
						this.Cols[i].AllowEditing = Convert.ToBoolean(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxLOCK_YN]);    // 칼럼 에디터 가능 여부
						this.Cols[i].Visible = Convert.ToBoolean(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxVISIBLE_YN]);			// 칼럼 visible 
						this.Cols[i].AllowSorting = Convert.ToBoolean(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxAUTOSORT_YN]);	// 칼럼 별자동 sort

						switch(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxHALIGN].ToString())									// 칼럼정렬
						{
							case "LEFT":
								this.Cols[i].TextAlign = TextAlignEnum.LeftCenter; 
								this.Cols[i].ImageAlign = ImageAlignEnum.LeftCenter;
								break;

							case "CENTER":
								this.Cols[i].TextAlign = TextAlignEnum.CenterCenter;
								this.Cols[i].ImageAlign = ImageAlignEnum.CenterCenter;
								break;

							case "RIGHT":
								this.Cols[i].TextAlign = TextAlignEnum.RightCenter;
								this.Cols[i].ImageAlign = ImageAlignEnum.RightCenter;
								break;

							default:
								break;
						}

					 
					

						//헤더 데이터
						this[0, i] = dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxCOL_NAME].ToString();					// 테이블 칼럼명

						//						switch(arg_lang)
						//						{
						//								//한국어 선택
						//							case "KO":
							
						this[1, i] = dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxHEAD_DESC1].ToString();					// 상단

						if(arg_hcount == 2)	
						{
							this[2, i] = dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxHEAD_DESC2].ToString();				// 하단
						}

						if(arg_hcount == 3)	
						{
							this[2, i] = dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxHEAD_DESC2].ToString();	
							this[3, i] = dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxHEAD_DESC3].ToString();				// 하단
						}

						if(arg_hcount == 4)	
						{
							this[2, i] = dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxHEAD_DESC2].ToString();	
							this[3, i] = dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxHEAD_DESC3].ToString();
							this[4, i] = dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxHEAD_DESC4].ToString();				// 하단
						}

						//								break;
						//
						//								//한국어 이외의 언어
						//							default:
						//								this[1, i] = dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxLAN_HEAD_DESC1].ToString();					// 상단
						//
						//								if(arg_hcount == 2)	
						//								{
						//									this[2, i] = dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxLAN_HEAD_DESC2].ToString();				// 하단
						//								}
						//
						//								if(arg_hcount == 3)	
						//								{
						//									this[2, i] = dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxLAN_HEAD_DESC2].ToString();	
						//									this[3, i] = dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxLAN_HEAD_DESC3].ToString();				// 하단
						//								}
						//
						//								if(arg_hcount == 4)	
						//								{
						//									this[2, i] = dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxLAN_HEAD_DESC2].ToString();	
						//									this[3, i] = dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxLAN_HEAD_DESC3].ToString();
						//									this[4, i] = dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxLAN_HEAD_DESC4].ToString();				// 하단
						//								}
						//
						//								break;
						//						}

					
 
					

						//등록된 Title Header에 backcolor,forecolor 설정
						if(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxBACKCOLOR].ToString() != "")							// 배경색
						{
							this.GetCellRange(1, i).StyleNew.BackColor = Color.FromArgb(Convert.ToInt32(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxBACKCOLOR].ToString()));

							if(arg_hcount == 2)
							{
								this.GetCellRange(2, i).StyleNew.BackColor = Color.FromArgb(Convert.ToInt32(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxBACKCOLOR].ToString()));
							}

							if(arg_hcount == 3)
							{
								this.GetCellRange(2, i, 3, i).StyleNew.BackColor = Color.FromArgb(Convert.ToInt32(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxBACKCOLOR].ToString()));
							}

							if(arg_hcount == 4)
							{
								this.GetCellRange(2, i, 4, i).StyleNew.BackColor = Color.FromArgb(Convert.ToInt32(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxBACKCOLOR].ToString()));
							}

						}

						if(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxFORECOLOR].ToString() != "")							// 글자색
						{
							this.GetCellRange(1, i).StyleNew.ForeColor = Color.FromArgb(Convert.ToInt32(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxFORECOLOR].ToString()));

							if(arg_hcount == 2)
							{
								this.GetCellRange(2, i).StyleNew.ForeColor = Color.FromArgb(Convert.ToInt32(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxFORECOLOR].ToString()));
							}

							if(arg_hcount == 3)
							{
								this.GetCellRange(2, i, 3, i).StyleNew.ForeColor = Color.FromArgb(Convert.ToInt32(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxFORECOLOR].ToString()));
							}

							if(arg_hcount == 4)
							{
								this.GetCellRange(2, i, 4, i).StyleNew.ForeColor = Color.FromArgb(Convert.ToInt32(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxFORECOLOR].ToString()));
							}

						 

						}


					} //end for


					if(arg_autosize)
					{
						this.AutoSizeCols();
					} 
				
					this.ExtendLastCol = true;		// 그리드 끝에 빈공간없이 last column에 맞춤
					//this.ExtendLastCol = arg_autosize;

					this.SelectionMode = SelectionModeEnum.ListBox;
 
					this.AllowMerging = AllowMergingEnum.FixedOnly;

					for(int i = 0; i < this.Cols.Count; i++)
					{
						this.Cols[i].AllowMerging = true;
					}


				}
				else 
				{	// 그리드 정보 없슴을 상태 바에 출력

				}//end if

			
			}	
			catch (Exception ex)
			{
				MessageBox.Show( ex.Message.ToString(),"Set_Grid",MessageBoxButtons.OK,MessageBoxIcon.Error);
				
			}
		}
        */

		#endregion

		#region  자재시스템 추가
 /*


		/// <summary>
		/// Display_CrossTab_Head : 크로스탭 헤드 조회
		/// </summary>		
		/// <param name="arg_width">column width</param>		
		/// <param name="arg_startcol">시작 column no</param>
		public  void Display_CrossTab_Head(DataTable dt_col,int arg_width,int arg_startcol)
		{
 									
			try 
			{									
				this.Cols.Count  =  arg_startcol ;
				this.Cols.Count =  this.Cols.Count + dt_col.Rows.Count ;				

				for(int i = 0; i < dt_col.Rows.Count; i++)
				{																			
					switch(this.Rows.Fixed)
					{
						case 3:
							this[this.Rows.Fixed-2,arg_startcol+i] = dt_col.Rows[i].ItemArray[0].ToString() ;
							this[this.Rows.Fixed-1,arg_startcol+i] = dt_col.Rows[i].ItemArray[1].ToString() ;
							break;
						case 4:
							this[this.Rows.Fixed-3,arg_startcol+i] = dt_col.Rows[i].ItemArray[0].ToString() ;
							this[this.Rows.Fixed-2,arg_startcol+i] = dt_col.Rows[i].ItemArray[1].ToString() ;							
							this[this.Rows.Fixed-1,arg_startcol+i] = dt_col.Rows[i].ItemArray[2].ToString() ;
							break;
						case 5:
							this[this.Rows.Fixed-4,arg_startcol+i] = dt_col.Rows[i].ItemArray[0].ToString() ;
							this[this.Rows.Fixed-3,arg_startcol+i] = dt_col.Rows[i].ItemArray[1].ToString() ;
							this[this.Rows.Fixed-2,arg_startcol+i] = dt_col.Rows[i].ItemArray[2].ToString() ;							
							this[this.Rows.Fixed-1,arg_startcol+i] = dt_col.Rows[i].ItemArray[3].ToString() ;
							break;
						default:
							this[this.Rows.Fixed-1,arg_startcol+i] = dt_col.Rows[i].ItemArray[0].ToString() ;
							break;
					}

					this.Cols[arg_startcol+i].Width = arg_width ;										
				}								
			}			
			
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message.ToString(),"Display_CrossTab_Head",MessageBoxButtons.OK,MessageBoxIcon.Error);
			}						
		}		




		
		/// <summary>
		/// Display_CrossTab : CrossTab조회
		/// </summary>
		/// <param name="arg_dt">data table</param>
		/// <param name="arg_key_fr">key field from 칼럼번호</param>		
		/// <param name="arg_key_to">key field to 칼럼번호</param>
		/// <param name="arg_colhead">column head 칼럼번호</param>		
		/// <param name="arg_display">display 칼럼번호</param>							
		public  void Display_CrossTab(DataTable arg_dt,int arg_key_fr,int arg_key_to,int arg_colhead,int arg_display,bool arg_tree)
		{
 									
			string str_newkey = "" ;
			string str_oldkey = "" ;
			
			try 
			{					
				//ROW 초기화
				this.Rows.Count = this.Rows.Fixed ;  				

				//loop - DATA row
				for(int i = 0; i < arg_dt.Rows.Count; i++)
				{		
					str_newkey = "" ;
					
					//key field 생성
					for(int k = arg_key_fr; k <= arg_key_to; k++)
					{
						str_newkey = str_newkey + arg_dt.Rows[i].ItemArray[k].ToString() ;
					}					
															
					//loop -DATA column(마지막ROW는 제외)
					for(int j = 0; j < arg_dt.Columns.Count; j++)
					{							
						if(j <= arg_colhead)
						{
							//key field가 변경시 새로운 row 생성
							if(str_newkey != str_oldkey && j == 0)
							{
								if(arg_tree)
								{	
									this.Rows.InsertNode(this.Rows.Count,int.Parse(arg_dt.Rows[i].ItemArray[j].ToString()));
								}
								else
								{
									this.AddItem("",this.Rows.Count);								
								}
							}
							
							// set division column
							this[this.Rows.Count-1, 0] = "";

							//칼럼이 크로스탭 항목일때:사이즈
							if(j == arg_colhead)
							{
								//칼럼헤드와 비교하여 동일하면 데이타 디스플레이
								//for(int m = arg_colhead; m < this.Cols.Count-1; m++)
								//{
								//	if(arg_dt.Rows[i].ItemArray[j].ToString() == this[this.Rows.Fixed-1,m+1].ToString() )
								//	{
								//		this[this.Rows.Count-1,m+1] = arg_dt.Rows[i].ItemArray[arg_display] ;
								//	}
								//}
								
								//칼럼헤드의 위치를 조회하여 데이타 디스플레이
								try
								{
									if(int.Parse(arg_dt.Rows[i].ItemArray[arg_colhead].ToString()) > 0)
									{
										this[this.Rows.Count-1, arg_colhead + int.Parse(arg_dt.Rows[i].ItemArray[j].ToString())] = arg_dt.Rows[i].ItemArray[arg_display] ;
									}
								}
								catch
								{
								}
									
							}
							else
							{
								this[this.Rows.Count-1,j+1] = arg_dt.Rows[i].ItemArray[j] ;
							}
							//return ;					
						}
					}

					str_oldkey = str_newkey;										
				}			
					
			}			
			
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message.ToString(),"Display_CrossTab",MessageBoxButtons.OK,MessageBoxIcon.Error);
			}
 
		}



		/// <summary>
		/// Display_CrossTab : CrossTab조회
		/// </summary>
		/// <param name="arg_dt">data table</param>
		/// <param name="arg_key_fr">key field from 칼럼번호</param>		
		/// <param name="arg_key_to">key field to 칼럼번호</param>
		/// <param name="arg_colhead">column head 칼럼번호</param>		
		/// <param name="arg_display">display 칼럼번호</param>		
		/// <param name="arg_userdata">cell tag value 칼럼번호</param>					
		/// <param name="arg_tree"></param>
		public  void Display_CrossTab(DataTable arg_dt,int arg_key_fr,int arg_key_to,int arg_colhead,int arg_display, int arg_userdata, bool arg_tree)
		{
 									
			string str_newkey = "" ;
			string str_oldkey = "" ;
			
			try 
			{					
				//ROW 초기화
				this.Rows.Count = this.Rows.Fixed ;  				

				//loop - DATA row
				for(int i = 0; i < arg_dt.Rows.Count; i++)
				{		
					str_newkey = "" ;
					
					//key field 생성
					for(int k = arg_key_fr; k <= arg_key_to; k++)
					{
						str_newkey = str_newkey + arg_dt.Rows[i].ItemArray[k].ToString() ;
					}					
															
					//loop -DATA column(마지막ROW는 제외)
					for(int j = 0; j < arg_dt.Columns.Count; j++)
					{							
						if(j <= arg_colhead)
						{
							//key field가 변경시 새로운 row 생성
							if(str_newkey != str_oldkey && j == 0)
							{
								if(arg_tree)
								{	
									this.Rows.InsertNode(this.Rows.Count,int.Parse(arg_dt.Rows[i].ItemArray[j].ToString()));
								}
								else
								{
									this.AddItem("",this.Rows.Count);								
								}
							}
							
							// set division column
							this[this.Rows.Count-1, 0] = "";

							//칼럼이 크로스탭 항목일때:사이즈
							if(j == arg_colhead)
							{
								 
								//칼럼헤드의 위치를 조회하여 데이타 디스플레이
								try
								{
									if(int.Parse(arg_dt.Rows[i].ItemArray[arg_colhead].ToString()) > 0)
									{
										this[this.Rows.Count-1, arg_colhead + int.Parse(arg_dt.Rows[i].ItemArray[j].ToString())] = arg_dt.Rows[i].ItemArray[arg_display] ;

										CellRange cr = this.GetCellRange(this.Rows.Count-1, arg_colhead + int.Parse(arg_dt.Rows[i].ItemArray[j].ToString()) );
										cr.UserData = arg_dt.Rows[i].ItemArray[arg_userdata].ToString();
 
									}
								}
								catch
								{
								}
									
							}
							else
							{
								this[this.Rows.Count-1,j+1] = arg_dt.Rows[i].ItemArray[j] ;
							}
							//return ;					
						}
					}

					str_oldkey = str_newkey;										
				}			
					
			}			
			
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message.ToString(),"Display_CrossTab",MessageBoxButtons.OK,MessageBoxIcon.Error);
			}
 
		}




		/// <summary>
		/// Display_Size_ColHead : size조회
		/// </summary>
		/// <param name="arg_style">style code</param>		
		/// <param name="arg_width">column width</param>		
		/// <param name="arg_startcol">start column</param>		
		public  void Display_Size_ColHead(string arg_factory,string arg_style,int arg_width,int arg_startcol)
		{
 									
			try 
			{	
				DataSet    ds_size;
				DataTable  dt_size;	

				MyOraDB.ReDim_Parameter(3); 

				//01.PROCEDURE명
				MyOraDB.Process_Name = "PKG_SBC_COMMON.SELECT_SIZE_COLHEAD";
 
				//02.ARGURMENT명
				MyOraDB.Parameter_Name[0] = "ARG_FACTORY";
				MyOraDB.Parameter_Name[1] = "ARG_STYLE";									
				MyOraDB.Parameter_Name[2] = "OUT_CURSOR";

				//03.DATA TYPE
				MyOraDB.Parameter_Type[0] = (int)OracleType.VarChar;
				MyOraDB.Parameter_Type[1] = (int)OracleType.VarChar;									
				MyOraDB.Parameter_Type[2] = (int)OracleType.Cursor;
			 
				//04.DATA 정의  			
				MyOraDB.Parameter_Values[0] = arg_factory; 
				MyOraDB.Parameter_Values[1] = arg_style; 				
				MyOraDB.Parameter_Values[2] = ""; 

				MyOraDB.Add_Select_Parameter(true);
 
				ds_size = MyOraDB.Exe_Select_Procedure();

				if(ds_size == null) return ;
			
				dt_size =  ds_size.Tables[MyOraDB.Process_Name]; 
				
				this.Cols.Count =  arg_startcol ;
				this.Cols.Count =  this.Cols.Count + dt_size.Rows.Count ;

                for (int i = 0; i < dt_size.Rows.Count; i++)
                {
                    this[0, arg_startcol + i] = dt_size.Rows[i].ItemArray[1];	// col_order
                    this[this.Cols.Fixed + 1, arg_startcol + i] = dt_size.Rows[i].ItemArray[0];  // cs_size
                    this.Cols[arg_startcol + i].Width = arg_width;
                }
								
				this.Rows[this.Cols.Fixed+1].TextAlign = TextAlignEnum.CenterCenter;
			}			
			 
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message.ToString(),"Display_Size",MessageBoxButtons.OK,MessageBoxIcon.Error);
			}
 
		}

		/// <summary>
		/// Recover_Row : 행 수정 표시 취소(수정 및 삭제표시 취소)
		/// 수정자 : 우효동
		/// 수정일 : 2005.11.17
		/// </summary> 
		public  void Recover_Row()
		{
		
			try
			{
				int row_count = this.Rows.Count;
				string[] insert_row = null;
				int insert_count = 0;

				for(int i = 0; i < row_count; i++)
				{
					if (this[i, 0] == null)
					{
						continue;
					}
					if (this[i, 0].ToString() == "I")
					{
						insert_count++;
					}
				}
 
				insert_row = new string[insert_count];
				insert_count = 0;

				for(int j = 0; j < row_count; j++)
				{
					if (this[j, 0] == null)
					{
						continue;
					}
					if (this[j, 0].ToString() == "I")
					{
						insert_row[insert_count] = j.ToString();
						insert_count++;
					}
					else
					{
						this[j, 0] = "";
					}
				}
				if(insert_row.Length > 0)
				{
					insert_count = insert_row.Length;

					while(insert_count > 0)
					{
						this.RemoveItem(Int16.Parse(insert_row[insert_count-1].ToString()));
						insert_count--;
					}
				}
			}

			catch (Exception ex)
			{
				MessageBox.Show(ex.Message.ToString(),"Recover_Row",MessageBoxButtons.OK,MessageBoxIcon.Error);
			}


		}



		/// <summary>
		/// ClearAll : 그리드 데이터 초기화
		/// </summary>
		public void ClearAll()
		{
			if (this.Rows.Fixed != this.Rows.Count)
			{
				this.Clear(C1.Win.C1FlexGrid.ClearFlags.UserData, this.Rows.Fixed, 1, this.Rows.Count - 1, this.Cols.Count - 1);
				this.Rows.Count = this.Rows.Fixed;
			}
		}



		/// <summary>
		/// ClearAll : 그리드 플래그 초기화
		/// </summary>
		public void ClearFlags()
		{
			for (int vRow = this.Rows.Fixed ; vRow < this.Rows.Count ; vRow++)
				this[vRow, 0] = "";
		}


		

		/// <summary>
		/// SelectAll : 그리드 전체 선택
		/// </summary>
		public void SelectAll()
		{
			if (this.Rows.Fixed < this.Rows.Count)
			{
				this.Select(this.Rows.Fixed, this.MouseCol, this.Rows.Count - 1, this.MouseCol);
			}
		}


 

		/// <summary>
		/// Set_CellStyle_Number : number 형 셀타입 설정 (예 : 1,234,567.001)
		/// </summary>
		/// <param name="arg_col"></param>
		public void Set_CellStyle_Number(int arg_col)
		{ 
			CellStyle cellst = this.Styles.Add("NUMBER", this.Cols[arg_col].Style);

			cellst.DataType = typeof(double);
			cellst.Format = "#,##0.##########";  

			this.Cols[arg_col].Style = this.Styles["NUMBER"]; 
		}




		/// <summary>
		/// Refresh_Division : 전체 재 조회 하지 않고, division "" 로 세팅
		/// insert, update = "" 로 처리
		/// delete = row 삭제로 처리 
		/// </summary>
		public void Refresh_Division()
		{
			try
			{
				for(int i = this.Rows.Count - 1; i >= this.Rows.Fixed; i--)
				{
					if(this[i, 0] == null || this[i, 0].ToString() == "") continue;

					if(this[i, 0].ToString() == "D")
					{
						this.Rows.Remove(i);
					}
					else
					{
						this[i, 0] = "";
					}

				} // end for i
			}
			catch(Exception ex)
			{
			//	ComFunction.User_Message(ex.Message, "Refresh_Division", MessageBoxButtons.OK, MessageBoxIcon.Error); 
			} 

		}







		/// <summary>
		/// Display_Grid : 
		/// </summary>
		/// <param name="arg_dt"></param>
		public void Display_Grid(DataTable arg_dt, bool arg_autosizecol)
		{
			this.Rows.Count = this.Rows.Fixed; 
  
			for(int i = 0; i < arg_dt.Rows.Count; i++)
			{
				this.AddItem(arg_dt.Rows[i].ItemArray, this.Rows.Count, 1);
				this[this.Rows.Count - 1, 0] = ""; 
			}

			if(arg_autosizecol) 
			{
				this.AutoSizeCols();
			}


			arg_dt.Dispose();
		}




		/// <summary>
		/// Display_Grid : 
		/// </summary>
		/// <param name="arg_dt"></param>
		public void Display_Grid_Add(DataTable arg_dt, bool arg_autosizecol)
		{
			//			this.Rows.Count = this.Rows.Count; 
 
			for(int i = 0; i < arg_dt.Rows.Count; i++)
			{
				this.AddItem(arg_dt.Rows[i].ItemArray, this.Rows.Count, 1);
				this[this.Rows.Count - 1, 0] = ""; 
			}

			if(arg_autosizecol) 
			{
				this.AutoSizeCols();
			}


			arg_dt.Dispose();
		}




		/// <summary>
		/// 선택된 열번호 검색
		/// </summary>
		/// <returns>int[]</returns>
		public int[] Selections
		{
			get
			{
				ArrayList vSelRow = new ArrayList(this.Rows.Count);

				for (int vRow = this.Rows.Fixed ; vRow < this.Rows.Count ; vRow++)
				{
					if (this.Rows[vRow].Selected)
						vSelRow.Add(vRow);
				}

				return (int[])vSelRow.ToArray(System.Type.GetType("System.Int32"));
			}
		}


		public string[][] GetDataSourceWithCode(int arg_col)
		{
			string[][] vData = new string[2][];

			if (this.Cols[arg_col].DataMap != null)
			{
				IDictionary vDic	= null;
				IEnumerator vEnum	= null;
				string[] vCode		= new string[this.Cols[arg_col].DataMap.Count];
				string[] vValue		= new string[this.Cols[arg_col].DataMap.Count];
				int vCnt = 0;

				vData = new string[2][];
				vDic = this.Cols[arg_col].DataMap;
				vEnum = vDic.GetEnumerator();
				while (vEnum.MoveNext())
				{
					DictionaryEntry entry = (DictionaryEntry)vEnum.Current;
					vCode[vCnt] = entry.Key.ToString();
					vValue[vCnt++] = entry.Value.ToString();
				}

				vData[0] = vCode;
				vData[1] = vValue;
			}

			return vData;
		}

*/
	#endregion

        #region CDC 시스템 추가
        /*
        /// <summary>
        /// Set_Grid : 그리드 설정
        /// </summary>
        /// <param name="arg_pgid">적용시킬 프로그램 아이디</param>
        /// <param name="arg_pgseq">적용시킬 프로그램 순번</param>
        /// <param name="arg_hcount">그리드 헤더 수</param>
        /// <param name="arg_lang">언어코드</param> 
        /// <param name="arg_type">그리드 타입 (Search, Modify)</param> 
        /// <param name="arg_autosize">자동 컬럼 너비 맞추기 여부</param>
        public void Set_Grid_CDC(string arg_pgid, string arg_pgseq, int arg_hcount, string arg_lang, ComVar.Grid_Type arg_type, bool arg_autosize)
        {

            DataTable dt_list, dt_cmblist;
            CellStyle cellst;

            //신규 스타일 생성시 임의로 일련번호 추가해서 생성
            int cellst_count = 0;


            try
            {
                ////// DB에서 그리드 정보 추출 
                dt_list = this.MyOraDB.Select_GridHead(arg_pgid, arg_pgseq);
                if (dt_list == null) return;

                if (dt_list.Rows.Count > 0)
                {
                    this.Clear(C1.Win.C1FlexGrid.ClearFlags.All);
                    this.Cols.Count = dt_list.Rows.Count + 1;
                    this.Rows.Count = arg_hcount + 1;
                    this.Rows.Fixed = arg_hcount + 1;
                    this.Rows[0].Visible = false;
                    this.Cols[0].AllowEditing = false;


                    #region  그리드 색

                    this.Styles.EmptyArea.BackColor = COM.ComVar.GridEmptyColor;
                    this.Styles.Alternate.BackColor = COM.ComVar.GridAlternate_Color;
                    this.Styles.Highlight.BackColor = COM.ComVar.GridHigh_Color;
                    this.Styles.Highlight.ForeColor = COM.ComVar.GridHighFore_Color;
                    //this.Styles.Focus.BackColor = COM.ComVar.GridHigh_Color;
                    //this.Styles.Focus.ForeColor = COM.ComVar.GridHighFore_Color;
                    this.Styles.Fixed.ForeColor = COM.ComVar.GridForeColor;

                    switch (arg_type)
                    {
                        case COM.ComVar.Grid_Type.ForModify:
                            this.Styles.Fixed.BackColor = COM.ComVar.GridDarkFixed_Color;
                            break;

                        case COM.ComVar.Grid_Type.ForSearch:
                            this.Styles.Fixed.BackColor = COM.ComVar.GridLightFixed_Color;
                            break;
                    }


                    this.Cols[0].StyleNew.BackColor = COM.ComVar.GridCol0_Color;


                    #endregion
                    #region 헤더 정렬

                    this.Rows[1].TextAlign = TextAlignEnum.CenterCenter;

                    if (arg_hcount == 2)		// 2번째 Header
                    {
                        this.Rows[2].TextAlign = TextAlignEnum.CenterCenter;
                    }

                    if (arg_hcount == 3)		// 3번째 Header
                    {
                        this.Rows[2].TextAlign = TextAlignEnum.CenterCenter;
                        this.Rows[3].TextAlign = TextAlignEnum.CenterCenter;
                    }

                    if (arg_hcount == 4)		// 4번째 Header
                    {
                        this.Rows[2].TextAlign = TextAlignEnum.CenterCenter;
                        this.Rows[3].TextAlign = TextAlignEnum.CenterCenter;
                        this.Rows[4].TextAlign = TextAlignEnum.CenterCenter;
                    }

                    #endregion
                    #region 속성 지정

                    //--------------------------------------------------
                    //전체 속성 지정
                    this.Cols.Fixed = ComVar.GridCol_Fixed;
                    this.Cols[0].Width = ComVar.GridCol0_Width;
                    //this.Cols[0].StyleNew.BackColor = ComVar.GridCol0_Color ;  

                    this.Cols.Frozen = Convert.ToInt32(dt_list.Rows[0].ItemArray[(int)TBSCM_TABLE.IxFROZENCOL].ToString());	// 칼럼 Frozen
                    this.Rows.Frozen = Convert.ToInt32(dt_list.Rows[0].ItemArray[(int)TBSCM_TABLE.IxFROZENROW].ToString());	// 행 Frozen

                    //-------------------------------------------------
                    //Column 속성 설정 
                    //alingment cellstyle
                    //1. left
                    cellst = this.Styles.Add("LEFT");
                    cellst.TextAlign = TextAlignEnum.LeftCenter;
                    cellst.ImageAlign = ImageAlignEnum.LeftCenter;

                    //2. center
                    cellst = this.Styles.Add("CENTER");
                    cellst.TextAlign = TextAlignEnum.CenterCenter;
                    cellst.ImageAlign = ImageAlignEnum.CenterCenter;

                    //3. rigth
                    cellst = this.Styles.Add("RIGHT");
                    cellst.TextAlign = TextAlignEnum.RightCenter;
                    cellst.ImageAlign = ImageAlignEnum.RightCenter;


                    #endregion


                    arr_essential = new string[dt_list.Rows.Count + 1];

                    for (int i = 1; i < dt_list.Rows.Count + 1; i++)
                    {


                        arr_essential[i] = (dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxESSENTIAL_YN] == null) ? "" : dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxESSENTIAL_YN].ToString();


                        #region 정렬

                        switch (dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxHALIGN].ToString())									// 칼럼정렬
                        {
                            case "LEFT":
                                this.Cols[i].Style = this.Styles["LEFT"];
                                break;

                            case "CENTER":
                                this.Cols[i].Style = this.Styles["CENTER"];
                                break;

                            case "RIGHT":
                                this.Cols[i].Style = this.Styles["RIGHT"];
                                break;
                        }

                        #endregion


                        this.Cols[i].Width = Convert.ToInt32(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxWIDTH].ToString());

                        //this.Cols[i].AllowEditing = Convert.ToBoolean(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxLOCK_YN]);    // 칼럼 에디터 가능 여부

                        if (Convert.ToBoolean(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxLOCK_YN]))
                        {
                            // 컬럼 글자색 파란색으로 처리


                            //기존 컬럼 스타일 상속받아서 새로운 스타일 생성, 임의로 일련번호 추가
                            cellst = this.Styles.Add("EDIT" + cellst_count.ToString(), this.Cols[i].Style);

                            //새로운 스타일의 속성
                            cellst.DataType = typeof(string);
                            cellst.ForeColor = COM.ComVar.ClrImportant;

                            this.Cols[i].Style = this.Styles["EDIT" + cellst_count.ToString()];


                            this.Cols[i].AllowEditing = true;
                        }
                        else
                        {
                            this.Cols[i].AllowEditing = false;
                        }

                        this.Cols[i].Visible = Convert.ToBoolean(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxVISIBLE_YN]);			// 칼럼 visible 
                        this.Cols[i].AllowSorting = Convert.ToBoolean(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxAUTOSORT_YN]);	// 칼럼 별자동 sort

                        //헤더 데이터
                        this[0, i] = dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxCOL_NAME].ToString();					// 테이블 칼럼명



                        #region cell type

                        //스타일로 지정되어 정렬되어진 컬럼에 대해서
                        //사용자 정의 스타일 동시에 적용시키려 할때
                        //이전 스타일 제거되고 신규 스타일만 적용되므로
                        //신규 스타일 추가시 이전 스타일 상속받아서 생성

                        //신규 스타일로 적용했을때 신규 스타일 이름이 같은 경우
                        //기존 정렬이 신규 스타일에 따라서 일괄적으로 변경되기 때문에
                        //신규 스타일 생성시 임의로 일련번호 추가해서 생성

                        switch (dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxCELLTYPE].ToString())				// Cell Type
                        {
                            case "TEXT":

                                //기존 컬럼 스타일 상속받아서 새로운 스타일 생성, 임의로 일련번호 추가
                                cellst = this.Styles.Add("TEXT" + cellst_count.ToString(), this.Cols[i].Style);

                                //새로운 스타일의 속성
                                cellst.DataType = typeof(string);

                                this.Cols[i].Style = this.Styles["TEXT" + cellst_count.ToString()];

                                break;

                            case "DATE":

                                cellst = this.Styles.Add("DATE" + cellst_count.ToString(), this.Cols[i].Style);
                                cellst.DataType = typeof(DateTime);
                                cellst.Format = "yyyyMMdd";

                                this.Cols[i].Style = this.Styles["DATE" + cellst_count.ToString()];

                                break;

                            case "CHECKBOX":

                                cellst = this.Styles.Add("CHECKBOX" + cellst_count.ToString(), this.Cols[i].Style);
                                cellst.DataType = typeof(bool);

                                this.Cols[i].Style = this.Styles["CHECKBOX" + cellst_count.ToString()];

                                break;

                            case "COMBOBOX":

                                cellst = this.Styles.Add("COMBO_" + cellst_count.ToString(), this.Cols[i].Style);
                                cellst.DataType = typeof(string);

                                this.Cols[i].Style = this.Styles["COMBO_" + cellst_count.ToString()];


                                switch (Convert.ToInt32(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxDATA_LIST_TYPE].ToString()))	// data_list_type
                                {
                                    case (int)ComVar.ComboList_Type.ComCode:      //공통코드에서 ComboList 추출

                                        if (dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxDATA_LIST_CD].ToString() != "")					// Data_LIst_Cd
                                        {
                                            //combo_list
                                            dt_cmblist = this.MyOraDB.Select_ComCode(ComVar.This_CDC_Factory, dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxDATA_LIST_CD].ToString());
                                            if (dt_cmblist.Rows.Count != 0) this.Make_CmbDataList(ComVar.ComboList_Type.ComCode, dt_cmblist, i);
                                        }

                                        break;

                                    case (int)ComVar.ComboList_Type.Query:      //쿼리에서 ComboList 추출	

                                        if (dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxDATA_LIST_QUERY].ToString() != "")				//Data_List_Query
                                        {

                                            dt_cmblist = Make_Query(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxDATA_LIST_QUERY].ToString().Trim());
                                            if (dt_cmblist.Rows.Count != 0) this.Make_CmbDataList(ComVar.ComboList_Type.Query, dt_cmblist, i);
                                        }

                                        break;

                                    case (int)ComVar.ComboList_Type.ComCode_Name:

                                        if (dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxDATA_LIST_CD].ToString() != "")					// Data_LIst_Cd
                                        {

                                            dt_cmblist = this.MyOraDB.Select_ComCode(ComVar.This_CDC_Factory, dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxDATA_LIST_CD].ToString());
                                            if (dt_cmblist.Rows.Count != 0) this.Make_CmbDataList(ComVar.ComboList_Type.ComCode_Name, dt_cmblist, i);
                                        }

                                        break;
                                    case (int)ComVar.ComboList_Type.Query_Name:

                                        if (dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxDATA_LIST_QUERY].ToString() != "")     //Data_List_Query
                                        {
                                            dt_cmblist = Make_Query(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxDATA_LIST_QUERY].ToString().Trim());
                                            if (dt_cmblist.Rows.Count != 0) this.Make_CmbDataList(ComVar.ComboList_Type.Query_Name, dt_cmblist, i);

                                            //dt_cmblist = this.MyOraDB.Select_ComCode(ComVar.This_CDC_Factory, dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxDATA_LIST_CD].ToString());
                                            //if (dt_cmblist.Rows.Count != 0) this.Make_CmbDataList(ComVar.ComboList_Type.ComCode_Name, dt_cmblist, i);
                                        }

                                        break;

                                }

                                break;


                            case "NUMBER":

                                cellst = this.Styles.Add("NUMBER" + cellst_count.ToString(), this.Cols[i].Style);
                                cellst.DataType = typeof(double);
                                cellst.Format = "#,##0.##########";

                                this.Cols[i].Style = this.Styles["NUMBER" + cellst_count.ToString()];

                                break;


                        } //end switch


                        cellst_count++;

                        #endregion
                        #region 언어

                        this[1, i] = dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxHEAD_DESC1].ToString();					// 상단

                        if (arg_hcount == 2)
                        {
                            this[2, i] = dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxHEAD_DESC2].ToString();				// 하단
                        }

                        if (arg_hcount == 3)
                        {
                            this[2, i] = dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxHEAD_DESC2].ToString();
                            this[3, i] = dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxHEAD_DESC3].ToString();				// 하단
                        }

                        if (arg_hcount == 4)
                        {
                            this[2, i] = dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxHEAD_DESC2].ToString();
                            this[3, i] = dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxHEAD_DESC3].ToString();
                            this[4, i] = dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxHEAD_DESC4].ToString();				// 하단
                        }


                        #endregion
                        #region 타이틀 색깔 설정

                        //등록된 Title Header에 backcolor,forecolor 설정
                        if (dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxBACKCOLOR].ToString().Trim() != "")							// 배경색
                        {
                            this.GetCellRange(1, i).StyleNew.BackColor = Color.FromArgb(Convert.ToInt32(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxBACKCOLOR].ToString()));

                            if (arg_hcount == 2)
                            {
                                this.GetCellRange(2, i).StyleNew.BackColor = Color.FromArgb(Convert.ToInt32(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxBACKCOLOR].ToString()));
                            }

                            if (arg_hcount == 3)
                            {
                                this.GetCellRange(2, i, 3, i).StyleNew.BackColor = Color.FromArgb(Convert.ToInt32(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxBACKCOLOR].ToString()));
                            }

                            if (arg_hcount == 4)
                            {
                                this.GetCellRange(2, i, 4, i).StyleNew.BackColor = Color.FromArgb(Convert.ToInt32(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxBACKCOLOR].ToString()));
                            }

                        }

                        if (dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxFORECOLOR].ToString().Trim() != "")							// 글자색
                        {
                            this.GetCellRange(1, i).StyleNew.ForeColor = Color.FromArgb(Convert.ToInt32(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxFORECOLOR].ToString()));

                            if (arg_hcount == 2)
                            {
                                this.GetCellRange(2, i).StyleNew.ForeColor = Color.FromArgb(Convert.ToInt32(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxFORECOLOR].ToString()));
                            }

                            if (arg_hcount == 3)
                            {
                                this.GetCellRange(2, i, 3, i).StyleNew.ForeColor = Color.FromArgb(Convert.ToInt32(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxFORECOLOR].ToString()));
                            }

                            if (arg_hcount == 4)
                            {
                                this.GetCellRange(2, i, 4, i).StyleNew.ForeColor = Color.FromArgb(Convert.ToInt32(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxFORECOLOR].ToString()));
                            }



                        }


                        #endregion


                    } //end for


                    if (arg_autosize)
                    {
                        this.AutoSizeCols();
                    }

                    this.ExtendLastCol = true;		// 그리드 끝에 빈공간없이 last column에 맞춤 
                    //this.ExtendLastCol = arg_autosize;

                    this.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
                    this.SelectionMode = SelectionModeEnum.ListBox;
                    this.Font = new Font("Verdana", 8);

                    //-------------------------------------------------------
                    // merge
                    this.AllowMerging = AllowMergingEnum.FixedOnly;

                    for (int i = 0; i < this.Cols.Count; i++)
                    {
                        this.Cols[i].AllowMerging = true;
                    }


                    for (int i = 0; i < this.Rows.Fixed; i++)
                    {
                        this.Rows[i].AllowMerging = true;
                    }

                    //-------------------------------------------------------


                }
                else
                {	// 그리드 정보 없음을 상태 바에 출력

                }//end if


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Set_Grid", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        /// <summary>
        /// Set_Grid : 그리드 설정
        /// </summary>
        /// <param name="arg_pgid">적용시킬 프로그램 아이디</param>
        /// <param name="arg_pgseq">적용시킬 프로그램 순번</param>
        /// <param name="arg_hcount">그리드 헤더 수</param>
        /// <param name="arg_lang">언어코드</param>
        /// <param name="arg_autosize">자동 컬럼 너비 맞추기 여부</param>
        public void Set_Grid_CDC(string arg_pgid, string arg_pgseq, int arg_hcount, string arg_lang, bool arg_autosize)
        {

            DataTable dt_list, dt_cmblist;
            CellStyle cellst;

            try
            {
                ////// DB에서 그리드 정보 추출 
                dt_list = this.MyOraDB.Select_GridHead(arg_pgid, arg_pgseq);
                if (dt_list == null) return;

                if (dt_list.Rows.Count > 0)
                {
                    this.Clear(C1.Win.C1FlexGrid.ClearFlags.All);
                    this.Cols.Count = dt_list.Rows.Count + 1;
                    this.Rows.Count = arg_hcount + 1;
                    this.Rows.Fixed = arg_hcount + 1;
                    this.Rows[0].Visible = false;

                    this.Rows[1].TextAlign = TextAlignEnum.CenterCenter;

                    if (arg_hcount == 2)		// 2번째 Header
                    {
                        this.Rows[2].TextAlign = TextAlignEnum.CenterCenter;
                    }

                    if (arg_hcount == 3)		// 3번째 Header
                    {
                        this.Rows[2].TextAlign = TextAlignEnum.CenterCenter;
                        this.Rows[3].TextAlign = TextAlignEnum.CenterCenter;
                    }

                    if (arg_hcount == 4)		// 4번째 Header
                    {
                        this.Rows[2].TextAlign = TextAlignEnum.CenterCenter;
                        this.Rows[3].TextAlign = TextAlignEnum.CenterCenter;
                        this.Rows[4].TextAlign = TextAlignEnum.CenterCenter;
                    }

                    //--------------------------------------------------
                    //전체 속성 지정
                    this.Cols.Fixed = ComVar.GridCol_Fixed;
                    this.Cols[0].Width = ComVar.GridCol0_Width;
                    //this.Cols[0].StyleNew.BackColor = ComVar.GridCol0_Color ;  

                    this.Cols.Frozen = Convert.ToInt32(dt_list.Rows[0].ItemArray[(int)TBSCM_TABLE.IxFROZENCOL].ToString());	// 칼럼 Frozen
                    this.Rows.Frozen = Convert.ToInt32(dt_list.Rows[0].ItemArray[(int)TBSCM_TABLE.IxFROZENROW].ToString());	// 행 Frozen

                    //-------------------------------------------------
                    //Column 속성 설정
                    //TEXT
                    cellst = this.Styles.Add("TEXT");
                    cellst.DataType = typeof(string);		// Type.GetType("System.String");

                    //DATE
                    cellst = this.Styles.Add("DATE");
                    cellst.DataType = typeof(DateTime);		//Type.GetType("System.DateTime");
                    cellst.Format = "yyyyMMdd";

                    //CHECKBOX
                    cellst = this.Styles.Add("CHECKBOX");
                    cellst.DataType = typeof(bool);			//Type.GetType("System.Boolean"); 
                    //-------------------------------------------------


                    arr_essential = new string[dt_list.Rows.Count + 1];

                    for (int i = 1; i < dt_list.Rows.Count + 1; i++)
                    {


                        arr_essential[i] = (dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxESSENTIAL_YN] == null) ? "" : dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxESSENTIAL_YN].ToString();

                        //cell type
                        switch (dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxCELLTYPE].ToString())				// Cell Type
                        {
                            case "TEXT":
                                this.Cols[i].Style = this.Styles["TEXT"];
                                break;

                            case "DATE":
                                this.Cols[i].Style = this.Styles["DATE"];
                                break;

                            case "CHECKBOX":
                                this.Cols[i].Style = this.Styles["CHECKBOX"];
                                break;

                            case "COMBOBOX":

                                switch (Convert.ToInt32(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxDATA_LIST_TYPE].ToString()))	// data_list_type
                                {
                                    case (int)ComVar.ComboList_Type.ComCode:      //공통코드에서 ComboList 추출

                                        if (dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxDATA_LIST_CD].ToString() != "")					// Data_LIst_Cd
                                        {
                                            //combo_list
                                            dt_cmblist = this.MyOraDB.Select_ComCode(ComVar.This_CDC_Factory, dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxDATA_LIST_CD].ToString());
                                            if (dt_cmblist.Rows.Count != 0) this.Cols[i].ComboList = this.Make_CmbDataList(ComVar.ComboList_Type.ComCode, dt_cmblist);
                                        }

                                        break;

                                    case (int)ComVar.ComboList_Type.Query:      //쿼리에서 ComboList 추출	

                                        if (dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxDATA_LIST_QUERY].ToString() != "")				//Data_List_Query
                                        {
                                            //									//combo_list

                                            dt_cmblist = Make_Query(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxDATA_LIST_QUERY].ToString().Trim());
                                            if (dt_cmblist.Rows.Count != 0) this.Cols[i].ComboList = this.Make_CmbDataList(ComVar.ComboList_Type.Query, dt_cmblist);
                                        }

                                        break;

                                    case (int)ComVar.ComboList_Type.ComCode_Name:

                                        if (dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxDATA_LIST_CD].ToString() != "")					// Data_LIst_Cd
                                        {
                                            //combo_list
                                            dt_cmblist = this.MyOraDB.Select_ComCode(ComVar.This_CDC_Factory, dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxDATA_LIST_CD].ToString());
                                            if (dt_cmblist.Rows.Count != 0) this.Cols[i].ComboList = this.Make_CmbDataList(ComVar.ComboList_Type.ComCode_Name, dt_cmblist);
                                        }

                                        break;

                                }

                                break;

                            default:
                                break;
                        } //end switch

                        //-------------------------------------------------------------------------------

                        this.Cols[i].Width = Convert.ToInt32(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxWIDTH].ToString());
                        this.Cols[i].AllowEditing = Convert.ToBoolean(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxLOCK_YN]);    // 칼럼 에디터 가능 여부
                        this.Cols[i].Visible = Convert.ToBoolean(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxVISIBLE_YN]);			// 칼럼 visible 
                        this.Cols[i].AllowSorting = Convert.ToBoolean(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxAUTOSORT_YN]);	// 칼럼 별자동 sort

                        switch (dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxHALIGN].ToString())									// 칼럼정렬
                        {
                            case "LEFT":
                                this.Cols[i].TextAlign = TextAlignEnum.LeftCenter;
                                this.Cols[i].ImageAlign = ImageAlignEnum.LeftCenter;
                                break;

                            case "CENTER":
                                this.Cols[i].TextAlign = TextAlignEnum.CenterCenter;
                                this.Cols[i].ImageAlign = ImageAlignEnum.CenterCenter;
                                break;

                            case "RIGHT":
                                this.Cols[i].TextAlign = TextAlignEnum.RightCenter;
                                this.Cols[i].ImageAlign = ImageAlignEnum.RightCenter;
                                break;

                            default:
                                break;
                        }




                        //헤더 데이터
                        this[0, i] = dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxCOL_NAME].ToString();					// 테이블 칼럼명


                        this[1, i] = dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxHEAD_DESC1].ToString();					// 상단

                        if (arg_hcount == 2)
                        {
                            this[2, i] = dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxHEAD_DESC2].ToString();				// 하단
                        }

                        if (arg_hcount == 3)
                        {
                            this[2, i] = dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxHEAD_DESC2].ToString();
                            this[3, i] = dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxHEAD_DESC3].ToString();				// 하단
                        }

                        if (arg_hcount == 4)
                        {
                            this[2, i] = dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxHEAD_DESC2].ToString();
                            this[3, i] = dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxHEAD_DESC3].ToString();
                            this[4, i] = dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxHEAD_DESC4].ToString();				// 하단
                        }



                        //등록된 Title Header에 backcolor,forecolor 설정
                        if (dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxBACKCOLOR].ToString().Trim() != "")							// 배경색
                        {
                            this.GetCellRange(1, i).StyleNew.BackColor = Color.FromArgb(Convert.ToInt32(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxBACKCOLOR].ToString()));

                            if (arg_hcount == 2)
                            {
                                this.GetCellRange(2, i).StyleNew.BackColor = Color.FromArgb(Convert.ToInt32(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxBACKCOLOR].ToString()));
                            }

                            if (arg_hcount == 3)
                            {
                                this.GetCellRange(2, i, 3, i).StyleNew.BackColor = Color.FromArgb(Convert.ToInt32(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxBACKCOLOR].ToString()));
                            }

                            if (arg_hcount == 4)
                            {
                                this.GetCellRange(2, i, 4, i).StyleNew.BackColor = Color.FromArgb(Convert.ToInt32(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxBACKCOLOR].ToString()));
                            }

                        }

                        if (dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxFORECOLOR].ToString().Trim() != "")							// 글자색
                        {
                            this.GetCellRange(1, i).StyleNew.ForeColor = Color.FromArgb(Convert.ToInt32(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxFORECOLOR].ToString()));

                            if (arg_hcount == 2)
                            {
                                this.GetCellRange(2, i).StyleNew.ForeColor = Color.FromArgb(Convert.ToInt32(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxFORECOLOR].ToString()));
                            }

                            if (arg_hcount == 3)
                            {
                                this.GetCellRange(2, i, 3, i).StyleNew.ForeColor = Color.FromArgb(Convert.ToInt32(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxFORECOLOR].ToString()));
                            }

                            if (arg_hcount == 4)
                            {
                                this.GetCellRange(2, i, 4, i).StyleNew.ForeColor = Color.FromArgb(Convert.ToInt32(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxFORECOLOR].ToString()));
                            }



                        }


                    } //end for


                    if (arg_autosize)
                    {
                        this.AutoSizeCols();
                    }

                    this.ExtendLastCol = true;		// 그리드 끝에 빈공간없이 last column에 맞춤
                    //this.ExtendLastCol = arg_autosize;

                }
                else
                {	// 그리드 정보 없슴을 상태 바에 출력

                }//end if


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Set_Grid", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }





        ///// <summary>
        ///// Set_Grid : 그리드 설정
        ///// </summary>
        ///// <param name="arg_pgid">적용시킬 프로그램 아이디</param>
        ///// <param name="arg_pgseq">적용시킬 프로그램 순번</param>
        ///// <param name="arg_hcount">그리드 헤더 수</param>
        ///// <param name="arg_lang">언어코드</param> 
        ///// <param name="arg_type">그리드 타입 (Search, Modify)</param> 
        ///// <param name="arg_autosize">자동 컬럼 너비 맞추기 여부</param>
        //public  void Set_Grid_CDC( string arg_pgid, string arg_pgseq, int arg_hcount, string arg_lang, ComVar.Grid_Type arg_type, bool arg_autosize)
        //{

        //    DataTable dt_list, dt_cmblist; 
        //    CellStyle cellst; 

        //    //신규 스타일 생성시 임의로 일련번호 추가해서 생성
        //    int cellst_count = 0;


        //    try
        //    {
        //        ////// DB에서 그리드 정보 추출 
        //        dt_list =this.MyOraDB.Select_GridHead(arg_pgid,arg_pgseq);
        //        if (dt_list== null) return ;

        //        if(dt_list.Rows.Count > 0)
        //        {
        //            this.Clear(C1.Win.C1FlexGrid.ClearFlags.All); 
        //            this.Cols.Count = dt_list.Rows.Count + 1; 
        //            this.Rows.Count = arg_hcount + 1;
        //            this.Rows.Fixed = arg_hcount + 1;
        //            this.Rows[0].Visible = false;
        //            this.Cols[0].AllowEditing = false;


        //            #region  그리드 색

        //            this.Styles.EmptyArea.BackColor = COM.ComVar.GridEmptyColor;
        //            this.Styles.Alternate.BackColor = COM.ComVar.GridAlternate_Color;
        //            this.Styles.Highlight.BackColor = COM.ComVar.GridHigh_Color;
        //            this.Styles.Highlight.ForeColor = COM.ComVar.GridHighFore_Color;
        //            //this.Styles.Focus.BackColor = COM.ComVar.GridHigh_Color;
        //            //this.Styles.Focus.ForeColor = COM.ComVar.GridHighFore_Color;
        //            this.Styles.Fixed.ForeColor = COM.ComVar.GridForeColor;

        //            switch(arg_type)
        //            {
        //                case COM.ComVar.Grid_Type.ForModify:
        //                    this.Styles.Fixed.BackColor = COM.ComVar.GridDarkFixed_Color;
        //                    break;

        //                case COM.ComVar.Grid_Type.ForSearch:
        //                    this.Styles.Fixed.BackColor = COM.ComVar.GridLightFixed_Color;
        //                    break;
        //            }


        //            this.Cols[0].StyleNew.BackColor = COM.ComVar.GridCol0_Color;


        //            #endregion 
        //            #region 헤더 정렬

        //            this.Rows[1].TextAlign = TextAlignEnum.CenterCenter;

        //            if (arg_hcount==2)		// 2번째 Header
        //            {
        //                this.Rows[2].TextAlign = TextAlignEnum.CenterCenter;
        //            }

        //            if (arg_hcount==3)		// 3번째 Header
        //            {
        //                this.Rows[2].TextAlign = TextAlignEnum.CenterCenter;
        //                this.Rows[3].TextAlign = TextAlignEnum.CenterCenter;
        //            }

        //            if (arg_hcount==4)		// 4번째 Header
        //            {
        //                this.Rows[2].TextAlign = TextAlignEnum.CenterCenter;
        //                this.Rows[3].TextAlign = TextAlignEnum.CenterCenter;
        //                this.Rows[4].TextAlign = TextAlignEnum.CenterCenter;
        //            }

        //            #endregion 
        //            #region 속성 지정

        //            //--------------------------------------------------
        //            //전체 속성 지정
        //            this.Cols.Fixed = ComVar.GridCol_Fixed ; 
        //            this.Cols[0].Width = ComVar.GridCol0_Width ;  
        //            //this.Cols[0].StyleNew.BackColor = ComVar.GridCol0_Color ;  

        //            this.Cols.Frozen = Convert.ToInt32(dt_list.Rows[0].ItemArray[(int)TBSCM_TABLE.IxFROZENCOL].ToString());	// 칼럼 Frozen
        //            this.Rows.Frozen = Convert.ToInt32(dt_list.Rows[0].ItemArray[(int)TBSCM_TABLE.IxFROZENROW].ToString());	// 행 Frozen

        //            //-------------------------------------------------
        //            //Column 속성 설정 
        //            //alingment cellstyle
        //            //1. left
        //            cellst = this.Styles.Add("LEFT");
        //            cellst.TextAlign = TextAlignEnum.LeftCenter; 
        //            cellst.ImageAlign = ImageAlignEnum.LeftCenter; 

        //            //2. center
        //            cellst = this.Styles.Add("CENTER");
        //            cellst.TextAlign = TextAlignEnum.CenterCenter; 
        //            cellst.ImageAlign = ImageAlignEnum.CenterCenter; 

        //            //3. rigth
        //            cellst = this.Styles.Add("RIGHT");
        //            cellst.TextAlign = TextAlignEnum.RightCenter; 
        //            cellst.ImageAlign = ImageAlignEnum.RightCenter; 


        //            #endregion


        //            arr_essential = new string[dt_list.Rows.Count+1] ;

        //            for(int i = 1; i < dt_list.Rows.Count + 1; i++)
        //            {


        //                arr_essential[i] = (dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxESSENTIAL_YN] == null) ? "" : dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxESSENTIAL_YN].ToString() ;


        //                #region 정렬

        //                switch(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxHALIGN].ToString())									// 칼럼정렬
        //                {
        //                    case "LEFT":  
        //                        this.Cols[i].Style = this.Styles["LEFT"]; 
        //                        break;

        //                    case "CENTER": 
        //                        this.Cols[i].Style = this.Styles["CENTER"]; 
        //                        break;

        //                    case "RIGHT": 
        //                        this.Cols[i].Style = this.Styles["RIGHT"]; 
        //                        break; 
        //                } 

        //                #endregion 


        //                this.Cols[i].Width = Convert.ToInt32(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxWIDTH].ToString());

        //                //this.Cols[i].AllowEditing = Convert.ToBoolean(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxLOCK_YN]);    // 칼럼 에디터 가능 여부

        //                if(Convert.ToBoolean(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxLOCK_YN]) )
        //                {
        //                    // 컬럼 글자색 파란색으로 처리


        //                    //기존 컬럼 스타일 상속받아서 새로운 스타일 생성, 임의로 일련번호 추가
        //                    cellst = this.Styles.Add("EDIT" + cellst_count.ToString(), this.Cols[i].Style);

        //                    //새로운 스타일의 속성
        //                    cellst.DataType = typeof(string);
        //                    cellst.ForeColor = COM.ComVar.ClrImportant;

        //                    this.Cols[i].Style = this.Styles["EDIT" + cellst_count.ToString()]; 


        //                    this.Cols[i].AllowEditing = true; 
        //                }
        //                else
        //                {
        //                    this.Cols[i].AllowEditing = false;
        //                }

        //                this.Cols[i].Visible = Convert.ToBoolean(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxVISIBLE_YN]);			// 칼럼 visible 
        //                this.Cols[i].AllowSorting = Convert.ToBoolean(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxAUTOSORT_YN]);	// 칼럼 별자동 sort

        //                //헤더 데이터
        //                this[0, i] = dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxCOL_NAME].ToString();					// 테이블 칼럼명



        //                #region cell type

        //                //스타일로 지정되어 정렬되어진 컬럼에 대해서
        //                //사용자 정의 스타일 동시에 적용시키려 할때
        //                //이전 스타일 제거되고 신규 스타일만 적용되므로
        //                //신규 스타일 추가시 이전 스타일 상속받아서 생성

        //                //신규 스타일로 적용했을때 신규 스타일 이름이 같은 경우
        //                //기존 정렬이 신규 스타일에 따라서 일괄적으로 변경되기 때문에
        //                //신규 스타일 생성시 임의로 일련번호 추가해서 생성

        //                switch(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxCELLTYPE].ToString())				// Cell Type
        //                {
        //                    case "TEXT":

        //                        //기존 컬럼 스타일 상속받아서 새로운 스타일 생성, 임의로 일련번호 추가
        //                        cellst = this.Styles.Add("TEXT" + cellst_count.ToString(), this.Cols[i].Style);

        //                        //새로운 스타일의 속성
        //                        cellst.DataType = typeof(string);

        //                        this.Cols[i].Style = this.Styles["TEXT" + cellst_count.ToString()]; 

        //                        break;

        //                    case "DATE": 

        //                        cellst = this.Styles.Add("DATE" + cellst_count.ToString(), this.Cols[i].Style);
        //                        cellst.DataType = typeof(DateTime);
        //                        cellst.Format = "yyyyMMdd";

        //                        this.Cols[i].Style = this.Styles["DATE" + cellst_count.ToString()]; 

        //                        break;

        //                    case "CHECKBOX":

        //                        cellst = this.Styles.Add("CHECKBOX" + cellst_count.ToString(), this.Cols[i].Style);
        //                        cellst.DataType = typeof(bool); 

        //                        this.Cols[i].Style = this.Styles["CHECKBOX" + cellst_count.ToString()]; 

        //                        break;

        //                    case "COMBOBOX":

        //                        cellst = this.Styles.Add("COMBO_" + cellst_count.ToString(), this.Cols[i].Style);
        //                        cellst.DataType = typeof(string);

        //                        this.Cols[i].Style = this.Styles["COMBO_" + cellst_count.ToString()]; 


        //                    switch(Convert.ToInt32(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxDATA_LIST_TYPE].ToString()))	// data_list_type
        //                    {
        //                        case (int)ComVar.ComboList_Type.ComCode :      //공통코드에서 ComboList 추출

        //                            if(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxDATA_LIST_CD].ToString() != "")					// Data_LIst_Cd
        //                            {
        //                                //combo_list
        //                                dt_cmblist = this.MyOraDB.Select_ComCode(ComVar.This_CDC_Factory, dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxDATA_LIST_CD].ToString());
        //                                if(dt_cmblist.Rows.Count != 0) this.Make_CmbDataList(ComVar.ComboList_Type.ComCode, dt_cmblist, i);
        //                            }

        //                            break;

        //                        case (int)ComVar.ComboList_Type.Query :      //쿼리에서 ComboList 추출	

        //                            if(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxDATA_LIST_QUERY].ToString() != "")				//Data_List_Query
        //                            {

        //                                dt_cmblist = Make_Query(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxDATA_LIST_QUERY].ToString().Trim());
        //                                if(dt_cmblist.Rows.Count != 0) this.Make_CmbDataList(ComVar.ComboList_Type.Query, dt_cmblist, i);
        //                            }

        //                            break;

        //                        case (int)ComVar.ComboList_Type.ComCode_Name :

        //                            if(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxDATA_LIST_CD].ToString() != "")					// Data_LIst_Cd
        //                            {

        //                                dt_cmblist = this.MyOraDB.Select_ComCode(ComVar.This_CDC_Factory, dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxDATA_LIST_CD].ToString());
        //                                if(dt_cmblist.Rows.Count != 0) this.Make_CmbDataList(ComVar.ComboList_Type.ComCode_Name, dt_cmblist, i);
        //                            }

        //                            break;


        //                        case (int)ComVar.ComboList_Type.Query_Name:

        //                            if (dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxDATA_LIST_QUERY].ToString() != "")     // Data_LIst_Cd
        //                            {
        //                                dt_cmblist = Make_Query(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxDATA_LIST_QUERY].ToString().Trim());
        //                                if (dt_cmblist.Rows.Count != 0) this.Make_CmbDataList(ComVar.ComboList_Type.Query_Name, dt_cmblist, i);

        //                                //dt_cmblist = this.MyOraDB.Select_ComCode(ComVar.This_CDC_Factory, dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxDATA_LIST_CD].ToString());
        //                                //if (dt_cmblist.Rows.Count != 0) this.Make_CmbDataList(ComVar.ComboList_Type.ComCode_Name, dt_cmblist, i);
        //                            }

        //                            break;

        //                    } 

        //                        break;


        //                    case "NUMBER":

        //                        cellst = this.Styles.Add("NUMBER" + cellst_count.ToString(), this.Cols[i].Style);
        //                        cellst.DataType = typeof(double);
        //                        cellst.Format = "#,##0.##########"; 

        //                        this.Cols[i].Style = this.Styles["NUMBER" + cellst_count.ToString()]; 

        //                        break;


        //                } //end switch


        //                cellst_count++;

        //                #endregion 
        //                #region 언어

        //                this[1, i] = dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxHEAD_DESC1].ToString();					// 상단

        //                if(arg_hcount == 2)	
        //                {
        //                    this[2, i] = dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxHEAD_DESC2].ToString();				// 하단
        //                }

        //                if(arg_hcount == 3)	
        //                {
        //                    this[2, i] = dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxHEAD_DESC2].ToString();	
        //                    this[3, i] = dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxHEAD_DESC3].ToString();				// 하단
        //                }

        //                if(arg_hcount == 4)	
        //                {
        //                    this[2, i] = dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxHEAD_DESC2].ToString();	
        //                    this[3, i] = dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxHEAD_DESC3].ToString();
        //                    this[4, i] = dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxHEAD_DESC4].ToString();				// 하단
        //                }


        //                #endregion 
        //                #region 타이틀 색깔 설정

        //                //등록된 Title Header에 backcolor,forecolor 설정
        //                if(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxBACKCOLOR].ToString().Trim() != "")							// 배경색
        //                {
        //                    this.GetCellRange(1, i).StyleNew.BackColor = Color.FromArgb(Convert.ToInt32(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxBACKCOLOR].ToString().Trim()));

        //                    if(arg_hcount == 2)
        //                    {
        //                        this.GetCellRange(2, i).StyleNew.BackColor = Color.FromArgb(Convert.ToInt32(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxBACKCOLOR].ToString().Trim()));
        //                    }

        //                    if(arg_hcount == 3)
        //                    {
        //                        this.GetCellRange(2, i, 3, i).StyleNew.BackColor = Color.FromArgb(Convert.ToInt32(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxBACKCOLOR].ToString().Trim()));
        //                    }

        //                    if(arg_hcount == 4)
        //                    {
        //                        this.GetCellRange(2, i, 4, i).StyleNew.BackColor = Color.FromArgb(Convert.ToInt32(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxBACKCOLOR].ToString().Trim()));
        //                    }

        //                }

        //                if(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxFORECOLOR].ToString().Trim() != "")							// 글자색
        //                {
        //                    this.GetCellRange(1, i).StyleNew.ForeColor = Color.FromArgb(Convert.ToInt32(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxFORECOLOR].ToString().Trim()));

        //                    if(arg_hcount == 2)
        //                    {
        //                        this.GetCellRange(2, i).StyleNew.ForeColor = Color.FromArgb(Convert.ToInt32(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxFORECOLOR].ToString().Trim()));
        //                    }

        //                    if(arg_hcount == 3)
        //                    {
        //                        this.GetCellRange(2, i, 3, i).StyleNew.ForeColor = Color.FromArgb(Convert.ToInt32(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxFORECOLOR].ToString().Trim()));
        //                    }

        //                    if(arg_hcount == 4)
        //                    {
        //                        this.GetCellRange(2, i, 4, i).StyleNew.ForeColor = Color.FromArgb(Convert.ToInt32(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxFORECOLOR].ToString().Trim()));
        //                    }



        //                }


        //                #endregion


        //            } //end for


        //            if(arg_autosize)
        //            {
        //                this.AutoSizeCols();
        //            } 

        //            this.ExtendLastCol = true;		// 그리드 끝에 빈공간없이 last column에 맞춤 
        //            //this.ExtendLastCol = arg_autosize;

        //            this.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
        //            this.SelectionMode = SelectionModeEnum.ListBox;
        //            this.Font = new Font("Verdana", 8);

        //            //-------------------------------------------------------
        //            // merge
        //            this.AllowMerging = AllowMergingEnum.FixedOnly;

        //            for(int i = 0; i < this.Cols.Count; i++)
        //            {
        //                this.Cols[i].AllowMerging = true;
        //            }


        //            for(int i = 0; i < this.Rows.Fixed; i++)
        //            {
        //                this.Rows[i].AllowMerging = true;
        //            }  

        //            //-------------------------------------------------------


        //        }
        //        else 
        //        {	// 그리드 정보 없음을 상태 바에 출력

        //        }//end if


        //    }	
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show( ex.Message.ToString(),"Set_Grid",MessageBoxButtons.OK,MessageBoxIcon.Error);

        //    }
        //}	





        /// <summary>
        /// Set_Grid : 공통 그리드 설정 
        /// </summary>
        /// <param name="arg_pgid">적용시킬 프로그램 아이디</param>
        /// <param name="arg_pgseq">적용시킬 프로그램 순번</param>
        /// <param name="arg_hcount">그리드 헤더 수</param>
        /// <param name="arg_lang">언어코드</param> 
        /// <param name="arg_type">그리드 타입 (Search, Modify)</param> 
        /// <param name="arg_autosize">자동 컬럼 너비 맞추기 여부</param>
        public void Set_Grid_Comm_CDC(string arg_pgid, string arg_pgseq, int arg_hcount, string arg_lang, ComVar.Grid_Type arg_type, bool arg_autosize)
        {

            DataTable dt_list, dt_cmblist;
            CellStyle cellst;

            try
            {
                ////// DB에서 그리드 정보 추출 
                dt_list = this.MyOraDB.Select_GridHead(arg_pgid, arg_pgseq);
                if (dt_list == null) return;

                if (dt_list.Rows.Count > 0)
                {
                    this.Clear(C1.Win.C1FlexGrid.ClearFlags.All);
                    this.Cols.Count = dt_list.Rows.Count + 1;
                    this.Rows.Count = arg_hcount + 1;
                    this.Rows.Fixed = arg_hcount + 1;
                    this.Rows[0].Visible = false;


                    this.Styles.EmptyArea.BackColor = Color.White;
                    this.Styles.Alternate.BackColor = Color.FromArgb(240, 244, 250);
                    this.Styles.Highlight.BackColor = Color.FromArgb(193, 221, 253);
                    this.Styles.Focus.BackColor = Color.FromArgb(193, 221, 253);
                    this.Styles.Fixed.ForeColor = Color.White;

                    switch (arg_type)
                    {
                        case COM.ComVar.Grid_Type.ForModify:
                            this.Styles.Fixed.BackColor = Color.FromArgb(122, 160, 200);
                            break;

                        case COM.ComVar.Grid_Type.ForSearch:
                            this.Styles.Fixed.BackColor = Color.FromArgb(135, 179, 234);
                            break;
                    }


                    this.Cols[0].StyleNew.BackColor = Color.FromArgb(193, 221, 253);



                    this.Rows[1].TextAlign = TextAlignEnum.CenterCenter;

                    if (arg_hcount == 2)		// 2번째 Header
                    {
                        this.Rows[2].TextAlign = TextAlignEnum.CenterCenter;
                    }

                    if (arg_hcount == 3)		// 3번째 Header
                    {
                        this.Rows[2].TextAlign = TextAlignEnum.CenterCenter;
                        this.Rows[3].TextAlign = TextAlignEnum.CenterCenter;
                    }

                    if (arg_hcount == 4)		// 4번째 Header
                    {
                        this.Rows[2].TextAlign = TextAlignEnum.CenterCenter;
                        this.Rows[3].TextAlign = TextAlignEnum.CenterCenter;
                        this.Rows[4].TextAlign = TextAlignEnum.CenterCenter;
                    }

                    //--------------------------------------------------
                    //전체 속성 지정
                    this.Cols.Fixed = ComVar.GridCol_Fixed;
                    this.Cols[0].Width = ComVar.GridCol0_Width;
                    //this.Cols[0].StyleNew.BackColor = ComVar.GridCol0_Color ;  

                    this.Cols.Frozen = Convert.ToInt32(dt_list.Rows[0].ItemArray[(int)TBSCM_TABLE.IxFROZENCOL].ToString());	// 칼럼 Frozen
                    this.Rows.Frozen = Convert.ToInt32(dt_list.Rows[0].ItemArray[(int)TBSCM_TABLE.IxFROZENROW].ToString());	// 행 Frozen

                    //-------------------------------------------------
                    //Column 속성 설정
                    //TEXT
                    cellst = this.Styles.Add("TEXT");
                    cellst.DataType = typeof(string);

                    //DATE
                    cellst = this.Styles.Add("DATE");
                    cellst.DataType = typeof(DateTime);
                    cellst.Format = "yyyyMMdd";

                    //CHECKBOX
                    cellst = this.Styles.Add("CHECKBOX");
                    cellst.DataType = typeof(bool);
                    //-------------------------------------------------


                    arr_essential = new string[dt_list.Rows.Count + 1];

                    for (int i = 1; i < dt_list.Rows.Count + 1; i++)
                    {


                        arr_essential[i] = (dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxESSENTIAL_YN] == null) ? "" : dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxESSENTIAL_YN].ToString();

                        //cell type
                        switch (dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxCELLTYPE].ToString())				// Cell Type
                        {
                            case "TEXT":
                                this.Cols[i].Style = this.Styles["TEXT"];
                                break;

                            case "DATE":
                                this.Cols[i].Style = this.Styles["DATE"];
                                break;

                            case "CHECKBOX":
                                this.Cols[i].Style = this.Styles["CHECKBOX"];
                                break;

                            case "COMBOBOX":

                                switch (Convert.ToInt32(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxDATA_LIST_TYPE].ToString()))	// data_list_type
                                {
                                    case (int)ComVar.ComboList_Type.ComCode:      //공통코드에서 ComboList 추출

                                        if (dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxDATA_LIST_CD].ToString() != "")					// Data_LIst_Cd
                                        {
                                            //combo_list
                                            dt_cmblist = this.MyOraDB.Select_ComCode(ComVar.This_CDC_Factory, dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxDATA_LIST_CD].ToString());
                                            this.Cols[i].ComboList = this.Make_CmbDataList(ComVar.ComboList_Type.ComCode, dt_cmblist);
                                        }

                                        break;

                                    case (int)ComVar.ComboList_Type.Query:      //쿼리에서 ComboList 추출	

                                        if (dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxDATA_LIST_QUERY].ToString() != "")				//Data_List_Query
                                        {
                                            //									//combo_list

                                            dt_cmblist = Make_Query(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxDATA_LIST_QUERY].ToString().Trim());
                                            this.Cols[i].ComboList = this.Make_CmbDataList(ComVar.ComboList_Type.Query, dt_cmblist);
                                        }

                                        break;

                                    case (int)ComVar.ComboList_Type.ComCode_Name:

                                        if (dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxDATA_LIST_CD].ToString() != "")					// Data_LIst_Cd
                                        {
                                            //combo_list
                                            dt_cmblist = this.MyOraDB.Select_ComCode(ComVar.This_CDC_Factory, dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxDATA_LIST_CD].ToString());
                                            this.Cols[i].ComboList = this.Make_CmbDataList(ComVar.ComboList_Type.ComCode_Name, dt_cmblist);
                                        }

                                        break;

                                }

                                break;

                            default:
                                break;
                        } //end switch

                        //-------------------------------------------------------------------------------

                        this.Cols[i].Width = Convert.ToInt32(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxWIDTH].ToString());
                        this.Cols[i].AllowEditing = Convert.ToBoolean(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxLOCK_YN]);    // 칼럼 에디터 가능 여부
                        this.Cols[i].Visible = Convert.ToBoolean(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxVISIBLE_YN]);			// 칼럼 visible 
                        this.Cols[i].AllowSorting = Convert.ToBoolean(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxAUTOSORT_YN]);	// 칼럼 별자동 sort

                        switch (dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxHALIGN].ToString())									// 칼럼정렬
                        {
                            case "LEFT":
                                this.Cols[i].TextAlign = TextAlignEnum.LeftCenter;
                                this.Cols[i].ImageAlign = ImageAlignEnum.LeftCenter;
                                break;

                            case "CENTER":
                                this.Cols[i].TextAlign = TextAlignEnum.CenterCenter;
                                this.Cols[i].ImageAlign = ImageAlignEnum.CenterCenter;
                                break;

                            case "RIGHT":
                                this.Cols[i].TextAlign = TextAlignEnum.RightCenter;
                                this.Cols[i].ImageAlign = ImageAlignEnum.RightCenter;
                                break;

                            default:
                                break;
                        }




                        //헤더 데이터
                        this[0, i] = dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxCOL_NAME].ToString();					// 테이블 칼럼명

                        //						switch(arg_lang)
                        //						{
                        //								//한국어 선택
                        //							case "KO":

                        this[1, i] = dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxHEAD_DESC1].ToString();					// 상단

                        if (arg_hcount == 2)
                        {
                            this[2, i] = dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxHEAD_DESC2].ToString();				// 하단
                        }

                        if (arg_hcount == 3)
                        {
                            this[2, i] = dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxHEAD_DESC2].ToString();
                            this[3, i] = dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxHEAD_DESC3].ToString();				// 하단
                        }

                        if (arg_hcount == 4)
                        {
                            this[2, i] = dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxHEAD_DESC2].ToString();
                            this[3, i] = dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxHEAD_DESC3].ToString();
                            this[4, i] = dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxHEAD_DESC4].ToString();				// 하단
                        }

                        //								break;
                        //
                        //								//한국어 이외의 언어
                        //							default:
                        //								this[1, i] = dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxLAN_HEAD_DESC1].ToString();					// 상단
                        //
                        //								if(arg_hcount == 2)	
                        //								{
                        //									this[2, i] = dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxLAN_HEAD_DESC2].ToString();				// 하단
                        //								}
                        //
                        //								if(arg_hcount == 3)	
                        //								{
                        //									this[2, i] = dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxLAN_HEAD_DESC2].ToString();	
                        //									this[3, i] = dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxLAN_HEAD_DESC3].ToString();				// 하단
                        //								}
                        //
                        //								if(arg_hcount == 4)	
                        //								{
                        //									this[2, i] = dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxLAN_HEAD_DESC2].ToString();	
                        //									this[3, i] = dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxLAN_HEAD_DESC3].ToString();
                        //									this[4, i] = dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxLAN_HEAD_DESC4].ToString();				// 하단
                        //								}
                        //
                        //								break;
                        //						}





                        //등록된 Title Header에 backcolor,forecolor 설정
                        if (dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxBACKCOLOR].ToString() != "")							// 배경색
                        {
                            this.GetCellRange(1, i).StyleNew.BackColor = Color.FromArgb(Convert.ToInt32(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxBACKCOLOR].ToString()));

                            if (arg_hcount == 2)
                            {
                                this.GetCellRange(2, i).StyleNew.BackColor = Color.FromArgb(Convert.ToInt32(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxBACKCOLOR].ToString()));
                            }

                            if (arg_hcount == 3)
                            {
                                this.GetCellRange(2, i, 3, i).StyleNew.BackColor = Color.FromArgb(Convert.ToInt32(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxBACKCOLOR].ToString()));
                            }

                            if (arg_hcount == 4)
                            {
                                this.GetCellRange(2, i, 4, i).StyleNew.BackColor = Color.FromArgb(Convert.ToInt32(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxBACKCOLOR].ToString()));
                            }

                        }

                        if (dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxFORECOLOR].ToString() != "")							// 글자색
                        {
                            this.GetCellRange(1, i).StyleNew.ForeColor = Color.FromArgb(Convert.ToInt32(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxFORECOLOR].ToString()));

                            if (arg_hcount == 2)
                            {
                                this.GetCellRange(2, i).StyleNew.ForeColor = Color.FromArgb(Convert.ToInt32(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxFORECOLOR].ToString()));
                            }

                            if (arg_hcount == 3)
                            {
                                this.GetCellRange(2, i, 3, i).StyleNew.ForeColor = Color.FromArgb(Convert.ToInt32(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxFORECOLOR].ToString()));
                            }

                            if (arg_hcount == 4)
                            {
                                this.GetCellRange(2, i, 4, i).StyleNew.ForeColor = Color.FromArgb(Convert.ToInt32(dt_list.Rows[i - 1].ItemArray[(int)TBSCM_TABLE.IxFORECOLOR].ToString()));
                            }



                        }


                    } //end for


                    if (arg_autosize)
                    {
                        this.AutoSizeCols();
                    }

                    this.ExtendLastCol = true;		// 그리드 끝에 빈공간없이 last column에 맞춤
                    //this.ExtendLastCol = arg_autosize;

                    this.SelectionMode = SelectionModeEnum.ListBox;

                    this.AllowMerging = AllowMergingEnum.FixedOnly;

                    for (int i = 0; i < this.Cols.Count; i++)
                    {
                        this.Cols[i].AllowMerging = true;
                    }


                }
                else
                {	// 그리드 정보 없슴을 상태 바에 출력

                }//end if


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Set_Grid", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        */


        #endregion  


    }
}
