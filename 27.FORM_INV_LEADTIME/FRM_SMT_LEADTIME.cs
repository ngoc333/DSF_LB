using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic.PowerPacks;
using System.Drawing.Drawing2D;
using System.Data.OracleClient;
using DevExpress.XtraCharts;
using DevExpress.XtraGauges.Core.Model;
using System.Reflection;


namespace FORM
{
    public partial class FRM_SMT_LEADTIME : Form
    {
        public FRM_SMT_LEADTIME()
        {
            InitializeComponent();
        }

          init strinit = new init();
          bool _load = false;

          string _Line, _Mline , _Lang  ;
         // Dictionary<string, string> _dtnInit = new Dictionary<string, string>();
          Dictionary<string, Tuple<string, string, string, string, string, string>> _dtnInit = new Dictionary<string, Tuple<string, string, string, string, string, string>>();
        int _reload_data = 0;

        #region Load/Visible
        private void FRM_SMT_LEADTIME_Load(object sender, EventArgs e)
        {
            lblDate.Text = string.Format(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            
            
        }

        private void FRM_SMT_LEADTIME_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                _Line = ComVar.Var._strValue1;
                _Mline = ComVar.Var._strValue2;
                _Lang = ComVar.Var._strValue3;
                initForm();
                if (ComVar.Var._strValue1 == null)
                {
                    _Line = "001";
                    _Mline = "";
                    _Lang = ComVar.Var._strValue3;
                }

                if (_Line == "099")
                {
                    cntStitching2.Visible = false;
                    cntStitching2_H.Visible = false;
                    pnStitching2.Visible = false;
                    cntStitching2_Qty.Visible = false;
                    cntStitching1.Text = "Stitching 1";
                    cntStitching3.Text = "Stitching 2";
                }
                else
                {
                    cntStitching2.Visible = true;
                    cntStitching2_H.Visible = true;
                    pnStitching2.Visible = true;
                    cntStitching2_Qty.Visible = true;
                    cntStitching1.Text = "Stitching 1";
                    cntStitching2.Text = "Stitching 2";
                    cntStitching3.Text = "Stitching 3";
                }
                _reload_data = 39;

                _load = true;
                tmr_Time.Enabled = true;
            }
            else
            {
                tmr_Time.Enabled = false;
                // HidePic();
            }
        }
        #endregion

        #region initForm
        /// <summary>
        /// Set giá trị của 1 cotrol bất kì trong form
        /// </summary>
        /// <param name="obj">tên và property của label, button,...  cần set</param>
        /// <param name="obj_value">giá trị cần set</param>
        private void setComValue(string obj, string obj_value)
        {
            try
            {
                if (obj.Contains('.'))
                {
                    string[] strSplit = obj.Split('.');
                    Control cnt = this.Controls.Find(strSplit[0], true).FirstOrDefault();

                    PropertyInfo propertyInfo = cnt.GetType().GetProperty(strSplit[1]);
                    propertyInfo.SetValue(cnt, Convert.ChangeType(obj_value, propertyInfo.PropertyType), null);
                }
            }
            catch (Exception ex)
            {
                ComVar.Var.writeToLog(this.GetType().Name + " ---> Void SetValue(string obj_name, string obj_value):    " + ex.ToString());
            }

        }

        private string getValueByLang(string arg_en, string arg_vi)
        {
            //if (arg_vi == "") return arg_en;
            //if (_Lang.ToUpper() == "EN")
            //{
            //    return arg_en;
            //}
            //else
            //{
                return arg_vi;
            //}
        }


        private void initForm()
        {
            _dtnInit = ComVar.Func.getInitForm2(this.GetType().Assembly.GetName().Name, this.GetType().Name);
           // _dtnInt2 = ComVar.Func.getInitForm2(this.GetType().Assembly.GetName().Name, this.GetType().Name);

        //    string str = _dtnInt2.ElementAt(0).Key;
          //  string str2 = _dtnInt2.ElementAt(0).Value.Item1;
            switch (_Lang)
            {
                case "Vi":
                    btnDay.Text = "Ngày";
                    btnMonth.Text = "Tháng";
                    btnWeek.Text = "Tuần";
                    btnYear.Text = "Năm";
                    break;
                default:
                    btnDay.Text = "Day";
                    btnMonth.Text = "Month";
                    btnWeek.Text = "Week";
                    btnYear.Text = "Year";
                    break;
            }

            for (int i = 0; i < _dtnInit.Count; i++)
            {
                if (_Lang == "Vi")
                {
                    if (_dtnInit.ElementAt(i).Value.Item6 != null)
                        setComValue(_dtnInit.ElementAt(i).Key, _dtnInit.ElementAt(i).Value.Item6);
                    else
                        setComValue(_dtnInit.ElementAt(i).Key, _dtnInit.ElementAt(i).Value.Item1);
                }
                else
                {
                    setComValue(_dtnInit.ElementAt(i).Key, _dtnInit.ElementAt(i).Value.Item1);
                }
            }

            
        }

        #endregion
        
        #region Method
        private void load_Data()
        {
            try
            {
                System.Data.DataSet ds = LOAD_DATA();
                if (ds == null || ds.Tables.Count == 0) return;
                add_Data_Control(ds.Tables[0]);
                Create_chart(ds.Tables[1], "");
                load_data_grid(ds.Tables[2]);
            }
            catch 
            {}

            
        }
        private string FormatData(object arg_obj)
        {
            try
            {
                if (arg_obj != null && arg_obj.ToString() != "0")
                {
                    return Convert.ToDouble(arg_obj).ToString("#,###,##0.##");
                }
                else
                {
                    return "";
                }

            }
            catch (Exception)
            {
                return "";
            }

        }

        private string addBlank(int arg_i)
        {
            string str="";
            for (int i = 0; i < arg_i; i++)
            {
                str += " ";
            }
            return str;
        }

        #region add Data
        private void add_Data_Control(DataTable arg_dt)
        {
            try
            {
                if (arg_dt == null || arg_dt.Rows.Count == 0) return;
                for (int i = 0; i < arg_dt.Rows.Count; i++)
                {
                    setComValue(arg_dt.Rows[i]["COM_NM"].ToString(), arg_dt.Rows[i]["TXT"].ToString());
                }
            }
            catch 
            {}
            
            
        }
        private void Create_chart( DataTable arg_dt, string arg_name)
        {
            if (arg_dt == null || arg_dt.Rows.Count == 0) return;
            //dt = db.SEL_OS_PROD_MONTH("C", uc_month.GetValue().ToString(), arg_op);
            chartSlabtest.DataSource = arg_dt;
            chartSlabtest.Series[0].ArgumentDataMember = "label_text";
            chartSlabtest.Series[0].ValueDataMembers.AddRange(new string[] { "leadtime" });

            chartSlabtest.Series[1].ArgumentDataMember = "label_text";
            chartSlabtest.Series[1].ValueDataMembers.AddRange(new string[] { "inventory" });
            chartSlabtest.Titles[0].Text = arg_name;
            
        }
        private void load_data_grid(DataTable arg_dt)
        {
            try
            {
                if (arg_dt != null && arg_dt.Rows.Count > 0)
                {
                    int iCount = arg_dt.Rows.Count;
                    axGrid.MaxRows = 2;
                    axGrid.MaxRows = 50;
                    //axGrid.SetText(1, 1, arg_dt.Rows[0]["MON"].ToString());
                    //double dColWidth = Convert.ToDouble(arg_dt.Rows[0]["col_width"]);
                    int iCol = 1, iRow = 3;
                  //  double dColWidth = Convert.ToDouble(arg_dt.Rows[0]["col_width"]);
                    //axGrid.Col = -1;
                    //axGrid.Row = 1;
                    //axGrid.BackColor = Color.FromArgb(71, 143, 143);
                    //axGrid.ForeColor = Color.White;
                    //axGrid.Row = 2;
                    //axGrid.BackColor = Color.FromArgb(71, 143, 143);
                    //axGrid.ForeColor = Color.White;

                    

                    for (int i = 0; i < iCount; i++)
                    {
                        //iCol = i + 3;
                        int.TryParse(arg_dt.Rows[i]["ORD"].ToString().Substring(arg_dt.Rows[i]["ORD"].ToString().Length-1, 1), out iCol);
                        
                        axGrid.SetText(1, iRow, arg_dt.Rows[i]["MODEL_NM"].ToString());
                        axGrid.SetText(2, iRow, arg_dt.Rows[i]["STYLE_CD"].ToString());

                        axGrid.SetText(3 + ((iCol -1) * 2), iRow, arg_dt.Rows[i]["INVENTORY"].ToString());
                        axGrid.SetText(3 + ((iCol -1) * 2) + 1, iRow, arg_dt.Rows[i]["LEADTIME"].ToString());

                        if (i+1 <iCount && arg_dt.Rows[i]["STYLE_CD"].ToString() != arg_dt.Rows[i + 1]["STYLE_CD"].ToString())
                        {
                            iRow++;
                        }
                        //axGrid.SetText(iCol, iRow, arg_dt.Rows[i]["INV"].ToString());
                        //axGrid.SetText(iCol + 1, iRow, FormatData(arg_dt.Rows[i]["LT"].ToString()));

                        if (arg_dt.Rows[i]["MODEL_NM"].ToString() == arg_dt.Rows[i]["STYLE_CD"].ToString())
                        {
                            axGrid.AddCellSpan(1, iRow, 2, 1);
                            axGrid.Col = -1;
                            axGrid.Row = iRow;
                            axGrid.FontSize = 20;
                            axGrid.BackColor = Color.PeachPuff;
                            axGrid.Col = 1;
                            axGrid.TypeHAlign = FPUSpreadADO.TypeHAlignConstants.TypeHAlignCenter;
                        }
                        //if (i + 1 < iCount && arg_dt.Rows[i]["STYLE_CD"].ToString() != arg_dt.Rows[i + 1]["STYLE_CD"].ToString())
                        //{
                        //    iRow++;
                        //    iCol = 3;
                        //}
                        //else iCol += 2;
                    }

                    //for (int i = 3; i < iCol; i++)
                    //{
                    //    axGrid.set_ColWidth(i, dColWidth);
                    //}


                    axGrid.MaxCols = 8;
                    axGrid.MaxRows = iRow;
                    axGrid.SetOddEvenRowColor(0xffffff, 0, 0xf7f6e8, 0);
                    axGrid.SetCellBorder(1, 3, axGrid.MaxCols, axGrid.MaxRows
                                , FPUSpreadADO.CellBorderIndexConstants.CellBorderIndexLeft, 0
                                , FPUSpreadADO.CellBorderStyleConstants.CellBorderStyleSolid);
                    axGrid.SetCellBorder(1, 2, axGrid.MaxCols, axGrid.MaxRows
                                , FPUSpreadADO.CellBorderIndexConstants.CellBorderIndexBottom, 0
                                , FPUSpreadADO.CellBorderStyleConstants.CellBorderStyleSolid);

                    axGrid.Col = 1;
                    axGrid.ColMerge = FPUSpreadADO.MergeConstants.MergeAlways;
                 //   axGrid.Row = 1;
                 //   axGrid.RowMerge = FPUSpreadADO.MergeConstants.MergeAlways;


                }
                axGrid.TopRow = 1;

            }
            catch (Exception)
            {
            }

        }
        #endregion 
       

        #endregion Method

        #region DB
        private System.Data.DataSet LOAD_DATA()
        {
            try
            {
                COM.OraDB MyOraDB = new COM.OraDB();
                System.Data.DataSet ds_ret;

                string process_name = "MES.PKG_SMT_LT.SMT_LONGTHANH_LT_V2";
                //ARGMODE
                MyOraDB.ReDim_Parameter(8);
                MyOraDB.Process_Name = process_name;
                MyOraDB.Parameter_Name[0] = "ARG_TYPE";
                MyOraDB.Parameter_Name[1] = "ARG_LINE";
                MyOraDB.Parameter_Name[2] = "ARG_MLINE";
                MyOraDB.Parameter_Name[3] = "ARG_DATE";
                MyOraDB.Parameter_Name[4] = "ARG_LANG";
                MyOraDB.Parameter_Name[5] = "OUT_CURSOR_Q";
                MyOraDB.Parameter_Name[6] = "OUT_CURSOR_C";
                MyOraDB.Parameter_Name[7] = "OUT_CURSOR_G";
                

                MyOraDB.Parameter_Type[0] = (char)OracleType.VarChar;
                MyOraDB.Parameter_Type[1] = (char)OracleType.VarChar;
                MyOraDB.Parameter_Type[2] = (char)OracleType.VarChar;
                MyOraDB.Parameter_Type[3] = (char)OracleType.VarChar;
                MyOraDB.Parameter_Type[4] = (char)OracleType.VarChar;
                MyOraDB.Parameter_Type[5] = (char)OracleType.Cursor;
                MyOraDB.Parameter_Type[6] = (char)OracleType.Cursor;
                MyOraDB.Parameter_Type[7] = (char)OracleType.Cursor;

                MyOraDB.Parameter_Values[0] = "";
                MyOraDB.Parameter_Values[1] = _Line;
                MyOraDB.Parameter_Values[2] = _Mline;
                MyOraDB.Parameter_Values[3] = "";
                MyOraDB.Parameter_Values[4] = _Lang;
                MyOraDB.Parameter_Values[5] = "";
                MyOraDB.Parameter_Values[6] = "";
                MyOraDB.Parameter_Values[7] = "";

                MyOraDB.Add_Select_Parameter(true);
                ds_ret = MyOraDB.Exe_Select_Procedure();
                if (ds_ret == null) return null;
                return ds_ret;
            }
            catch
            {
                return null;
            }
        }


        #endregion DB

        #region Event

        

        private void tmr_Time_Tick(object sender, EventArgs e)
        {
            
            _reload_data++;
            if (_reload_data>=40)
            {
                load_Data();
                _reload_data = 0;
            }
            lblDate.Text = string.Format(DateTime.Now.ToString("yyyy-MM-dd\nHH:mm:ss"));
            //if ( _loadpic == true && _reload_data == 2)
            //{
            //    load_Data();
                
            //    _loadpic = false;
            //}
        }

        private void lineArrow_Paint(object sender, PaintEventArgs e)
        {
            LineShape line = (LineShape)sender;
            Pen pen = new Pen(Color.FromArgb(66, 134, 244), 5);
            pen.StartCap = LineCap.ArrowAnchor;
            pen.EndCap = LineCap.ArrowAnchor;
            e.Graphics.DrawLine(pen, line.EndPoint, line.StartPoint);
        }

        private void lblDate_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        #endregion Event
        void mgs_conf_OnConfirm()
        {
            _reload_data = 39;
        }
        private void cmdL_Click(object sender, EventArgs e)
        {
            string MLINE_CD = "";

            try
            {
                //MessageBox.Show(((Button)sender).Name.ToString());
                if (_Line.Contains("FTY"))
                {
                    string ARG_CON_GPR = ((Button)sender).Name.ToString().Split('_')[0].Replace("cmdL", "");

                    switch (ARG_CON_GPR)
                    {
                        case "1":
                            MLINE_CD = "001";
                            break;
                        case "2":
                            MLINE_CD = "002";
                            break;
                        case "3":
                            MLINE_CD = "003";
                            break;
                        case "4":
                            MLINE_CD = "004";
                            break;
                    }
                }
                else
                    MLINE_CD = _Mline;
                string ARG_CON_CD = ((Button)sender).Name.ToString().Split('_')[1].Replace("Val", "");
              //  FRM_LEADTIME_TARGET LT_TAR = new FRM_LEADTIME_TARGET(_wh_cd,MLINE_CD, ARG_CON_CD);
                //LT_TAR.OnConfirm += mgs_conf_OnConfirm;
             //   LT_TAR.ShowDialog();

            }
            catch (Exception Ex)
            { }
        }

        private void callForm_Click(object sender, EventArgs e)
        {

            ComVar.Var.callForm = "23";
        }

        private void cntCutting_H_MouseHover(object sender, EventArgs e)
        {
            cntCutting_Text.Visible = true;
        }

        private void cntCutting_H_MouseLeave(object sender, EventArgs e)
        {
            cntCutting_Text.Visible = false;
        }

        private void cntStitching1_H_MouseHover(object sender, EventArgs e)
        {
            cntStitching_Text.Visible = true;
        }

        private void cntNoSew_H_MouseHover(object sender, EventArgs e)
        {
            cntNoSew_Text.Visible = true;
        }

        private void picZ1_MouseHover(object sender, EventArgs e)
        {
            cntOutgoing.Visible = true;
        }

        private void cntNoSew_H_MouseLeave(object sender, EventArgs e)
        {
            cntNoSew_Text.Visible = false;
        }

        private void picZ1_MouseLeave(object sender, EventArgs e)
        {
            cntOutgoing.Visible = false;
        }

        private void cntStitching1_H_MouseLeave(object sender, EventArgs e)
        {
            cntStitching_Text.Visible = false;
        }

        private void pn_top_Paint(object sender, PaintEventArgs e)
        {

        }


    }
}
