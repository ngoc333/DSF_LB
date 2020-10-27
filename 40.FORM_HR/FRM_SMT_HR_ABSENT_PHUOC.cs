using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OracleClient;
using DevExpress.XtraGauges.Core.Model;
using System.Threading;

namespace FORM
{
    public partial class FRM_SMT_HR_ABSENT_PHUOC : Form
    {
        public FRM_SMT_HR_ABSENT_PHUOC()
        {
            InitializeComponent();
            
            _lang = "en";
        }
          int indexScreen;
        string _line, _mLine, _lang;
        int cCount = 0;
        Dictionary<string, string> _dtnInit = new Dictionary<string, string>();
        Form[] arrForm = new Form[3];
        //init strinit = new init();
        public FRM_SMT_HR_ABSENT_PHUOC(string Title, int _indexScreen, string Line, string Mline,string Lang)
        {
            InitializeComponent();
            indexScreen = _indexScreen;
            _mLine = Mline;
            _line = Line; 
            _lang = Lang;
            tmrDate.Stop();
            lblTitle.Text = Title;
        }

        

        private void GoFullscreen()
        {
            this.WindowState = FormWindowState.Normal;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Bounds = Screen.PrimaryScreen.Bounds;

        }


        #region Absent
        private void loadChartAbsent(DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleComponent arcScaleComponent
                                    , DevExpress.XtraCharts.ChartControl argChart
                                    , DevExpress.XtraGauges.Win.Base.LabelComponent arglbl
                                    , string argPer, string argPlan, string argNoPlan)
        {
            try
            {
                float value = 0;
                //Chart Per
                arcScaleComponent.EnableAnimation = false;
                arcScaleComponent.EasingMode = DevExpress.XtraGauges.Core.Model.EasingMode.EaseInOut;
                arcScaleComponent.EasingFunction = new BackEase();
                arglbl.Text = "0";
                arcScaleComponent.Value = 0;

                arcScaleComponent.EnableAnimation = true;
                arcScaleComponent.EasingMode = DevExpress.XtraGauges.Core.Model.EasingMode.EaseInOut;
                arcScaleComponent.EasingFunction = new BackEase();
                arglbl.Text = argPer;
                float.TryParse(argPer, out value);
                arglbl.Text = value.ToString("##0.#");
                arcScaleComponent.Value = value;

                arcScaleComponent.MinValue = 0f;
                arcScaleComponent.MaxValue = 20f;
                //arcScaleComponent.Ranges[0].StartValue = 0;
                //arcScaleComponent.Ranges[0].EndValue = arcScaleComponent.Ranges[1].StartValue = (float)9; ;
                //arcScaleComponent.Ranges[1].EndValue = arcScaleComponent.Ranges[2].StartValue = (float)10;
                //arcScaleComponent.Ranges[2].EndValue = (float)10;

                //Chart Absent
                /*DataTable dt_tmp = new DataTable();
                dt_tmp.Columns.Add("CAPTION");
                dt_tmp.Columns.Add("VALUE", typeof(double));

                dt_tmp.Rows.Add();
                dt_tmp.Rows[0]["CAPTION"] = "NO PLAN";
                dt_tmp.Rows[0]["VALUE"] = argNoPlan == "" ? "0" : argNoPlan;
                dt_tmp.Rows.Add();
                dt_tmp.Rows[1]["CAPTION"] = "PLAN";
                dt_tmp.Rows[1]["VALUE"] = argPlan;

                argChart.DataSource = dt_tmp;
                argChart.Series[0].ArgumentDataMember = "CAPTION";
                argChart.Series[0].ValueDataMembers.AddRange(new string[] { "VALUE" });
                argChart.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;

                */
            }
            catch
            { }
        }

        private void loadDataGridAbsent(DataTable argDt)
        {
            try
            {
                if (argDt == null || argDt.Rows.Count == 0) return;
                string[] arr = {"MON", "THEDATE" 
                         //   ,"TOT_MAN", "TOT_NO_PLAN", "TOT_PLAN", "TOT_PER", "TOT_TUNOVER",  "TOT_TUNOVER_PER"
                            ,"MAN", "NO_PLAN", "PLAN", "PER", "TUNOVER",  "TUNOVER_PER"
                          //  ,"MAN2", "NO_PLAN2", "PLAN2", "PER2", "TUNOVER2",  "TUNOVER_PER2"                            
                           };
                axfpAbsent.Visible = false;
                int iNumRow = argDt.Rows.Count;
                axfpAbsent.MaxRows = 8;
                axfpAbsent.MaxCols = 5;
                axfpAbsent.MaxCols = 50;
                axfpAbsent.SetText(1, 3, argDt.Rows[0]["LOC"].ToString());
                for (int i = 0; i < argDt.Rows.Count; i++)
                {
                    axfpAbsent.set_ColWidth(i + 4, Convert.ToDouble(argDt.Rows[0]["COL_W"].ToString()));
                    for (int j = 0; j < arr.Length; j++)
                    {
                        axfpAbsent.Col = i + 4;
                        axfpAbsent.Row = j + 1;
                        axfpAbsent.Text = argDt.Rows[i][arr[j]].ToString();
                        if (j + 1 > 2)
                        {
                            axfpAbsent.BackColor = Color.White;
                            axfpAbsent.ForeColor = Color.Black;
                            axfpAbsent.TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignRight;
                            axfpAbsent.TypeVAlign = FPSpreadADO.TypeVAlignConstants.TypeVAlignCenter;
                        }
                        else if (j + 1 == 1)
                        {
                            axfpAbsent.BackColor = Color.Gray;
                            axfpAbsent.ForeColor = Color.White;
                            axfpAbsent.TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter;
                            axfpAbsent.TypeVAlign = FPSpreadADO.TypeVAlignConstants.TypeVAlignCenter;
                        }
                        else if (j + 1 == 2)
                        {
                            axfpAbsent.BackColor = Color.Silver;
                            axfpAbsent.ForeColor = Color.White;
                            axfpAbsent.TypeHAlign = FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter;
                            axfpAbsent.TypeVAlign = FPSpreadADO.TypeVAlignConstants.TypeVAlignCenter;
                        }


                        // axfpAbsent.SetText(i + 4, j + 1, argDt.Rows[i][arr[j]].ToString());
                    }

                    if (argDt.Rows[i]["TODAY"].ToString() == argDt.Rows[i]["THEDATE"].ToString())
                    {
                        loadChartAbsent(arcScaleComponentRub, chartHrCmp, lblRubValueG, argDt.Rows[i]["PER"].ToString(), argDt.Rows[i]["PLAN"].ToString(), argDt.Rows[i]["NO_PLAN"].ToString());

                    }

                }
                axfpAbsent.Row = -1;
                axfpAbsent.Col = iNumRow + 3;
                axfpAbsent.BackColor = Color.Orange;
                axfpAbsent.ForeColor = Color.White;

                axfpAbsent.SetCellBorder(iNumRow + 3, 1, iNumRow + 3, axfpAbsent.MaxRows, FPSpreadADO.CellBorderIndexConstants.CellBorderIndexBottom, 0, FPSpreadADO.CellBorderStyleConstants.CellBorderStyleBlank);


                axfpAbsent.AddCellSpan(4, 1, iNumRow - 1, 1);
                axfpAbsent.AddCellSpan(iNumRow + 3, 1, 1, 2);
                axfpAbsent.set_ColWidth(iNumRow + 3, 8);
                axfpAbsent.MaxCols = iNumRow + 3;
            }
            catch
            { }
            finally { axfpAbsent.Visible = true; }


        }

       
        private void chartHr(DataTable argDt, DevExpress.XtraCharts.ChartControl argChart)
        {
            try
            {
                if (argDt == null || argDt.Rows.Count == 0) return;
                string strTotal = "";
                double totalMain = 0;
                DataTable dt = argDt.Clone();
                dt.Columns["VALUE_DATA"].DataType = typeof(double);
                foreach (DataRow row in argDt.Rows)
                    dt.ImportRow(row);

                argChart.DataSource = dt;
                argChart.Series[0].ArgumentDataMember = "CAPTION";
                argChart.Series[0].ValueDataMembers.AddRange(new string[] { "VALUE_DATA" });

                double iAbsent, iAttend;
                double.TryParse(dt.Rows[0][1].ToString(), out iAbsent);
                double.TryParse(dt.Rows[1][1].ToString(), out iAttend);

                // return;
                totalMain = iAbsent + iAttend;

                strTotal = "Total Absent\n"
                       + totalMain.ToString() + " Person(s)\n"
                       + (Math.Round(totalMain * 100 / (totalMain + double.Parse(dt.Rows[2][1].ToString())), 1)).ToString() + " %";

                if (argChart.Name == "chartHrCmp")
                {
                    lblTotAbsent.Text = strTotal;
                }
                else
                {
                    lblTotAbsent.Text = strTotal;   //PHP
                }


                //if (iAbsent / (iAbsent + iAttend) * 100 >= 5)
                //    argChart.PaletteName = "Absent_Red";
                //else
                //    argChart.PaletteName = "Absent_Blue";
            }
            catch
            {
            }

            //argChart.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
            //DevExpress.XtraCharts.ChartTitle chartTitle1 = new DevExpress.XtraCharts.ChartTitle();
            // chartTitle1.Font = new System.Drawing.Font("Tahoma", 20F);
            //this.argChart.Titles.AddRange(new DevExpress.XtraCharts.ChartTitle[] { chartTitle1 });
        }


        #endregion Absent




        private void loadData()
        {
            try
            {

                System.Data.DataSet ds = null ;//=  ComVar.Func.ReadXMLToDs("FRM_SMT_HR_ABSENT_MONTH_"+_mLine ,true);
                if (btnYM == "A")
                   ds = ComVar.Func.ReadXMLToDs("FRM_SMT_HR_ABSENT_MONTH_" + uc_month.GetValue().ToString() + _line + _mLine, true);
                else
                    ds = ComVar.Func.ReadXMLToDs("FRM_SMT_HR_ABSENT_YEAR_" + uc_year.GetValue().ToString() + _line + _mLine, true);

                if (ds == null)
                    ds = GET_DATA();

                if (ds == null) return;
                loadDataGridAbsent(ds.Tables[0]);
                // loadDataGridTunover(ds.Tables[1]);
                chartHr(ds.Tables[1], chartHrCmp);
            }
            catch 
            {}
            
            // chartHr(ds.Tables[2], chartHrPhy);
        }

        private void getDataByThread()
        {
            DataSet ds = GET_DATA();

        }

        #region DB
        private System.Data.DataSet GET_DATA()
        {
            COM.OraDB MyOraDB = new COM.OraDB();
            DataSet ds_ret;
            try
            {
                string process_name = "MES.PKG_SMT_B_HR_STATUS.SEL_FTY_LT_HR_STATUS";

                MyOraDB.ReDim_Parameter(6);
                MyOraDB.Process_Name = process_name;



                MyOraDB.Parameter_Name[0] = "ARG_QTYPE";
                MyOraDB.Parameter_Name[1] = "ARG_LINE_CD";
                MyOraDB.Parameter_Name[2] = "ARG_MLINE_CD";
                MyOraDB.Parameter_Name[3] = "ARG_YM";
                MyOraDB.Parameter_Name[4] = "OUT_CURSOR";
                MyOraDB.Parameter_Name[5] = "OUT_CURSOR1";

                MyOraDB.Parameter_Type[0] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[1] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[2] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[3] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[4] = (int)OracleType.Cursor;
                MyOraDB.Parameter_Type[5] = (int)OracleType.Cursor;

                MyOraDB.Parameter_Values[0] = btnYM;
                MyOraDB.Parameter_Values[1] = _line;
                MyOraDB.Parameter_Values[2] = _mLine;
                if (btnYM == "A")
                    MyOraDB.Parameter_Values[3] = uc_month.GetValue().ToString();
                else
                    MyOraDB.Parameter_Values[3] = uc_year.GetValue().ToString();
                MyOraDB.Parameter_Values[4] = "";
                MyOraDB.Parameter_Values[5] = "";

                MyOraDB.Add_Select_Parameter(true);
                ds_ret = MyOraDB.Exe_Select_Procedure();

                if (ds_ret == null) return null;
                if (btnYM == "A")
                    ComVar.Func.WriteXML("FRM_SMT_HR_ABSENT_MONTH_" + uc_month.GetValue().ToString() + _line + _mLine, ds_ret);
                else
                    ComVar.Func.WriteXML("FRM_SMT_HR_ABSENT_YEAR_" + uc_year.GetValue().ToString() + _line + _mLine, ds_ret);
                return ds_ret;
            }
            catch
            {
                return null;
            }
        }

        #endregion DB

        private void FRM_SMT_HR_ABSENT_Load(object sender, EventArgs e)
        {
            //this.Name = "FRM_SMT_HR_ABSENT_PHUOC_" + _mLine;
            //this.Text = "FRM_SMT_HR_ABSENT_PHUOC_" + _mLine;
            lblDate.Text = string.Format(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            GoFullscreen();
            btnYM = "A";

            switch (_lang)
            { 
                case "Vi":
                    lblTitleGauges.Text = "Tỉ lệ vắng mặt trung bình (%)";
                    lblTitle.Text = "Nhân lực vắng mặt theo tháng";   
                    break;

                case "En":
                    lblTitleGauges.Text = "Absenteeism AVG (%)";
                    lblTitle.Text = "Human Absenteeism by Month";
                    break;
            }

            _dtnInit = ComVar.Func.getInitForm(this.GetType().Assembly.GetName().Name, this.GetType().Name);

            //BindingAbsent();
            //BindingTurnOver();
        }
       
        private void tmrDate_Tick(object sender, EventArgs e)
        {
            cCount++;
            lblDate.Text = string.Format(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            if (cCount >= 30)
            {

                loadData();
                Thread t = new Thread(new ThreadStart(getDataByThread));
                t.Start();
                
                cCount = 0;
            }

        }

        private void FRM_SMT_HR_ABSENT_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                _lang = ComVar.Var._strValue3;
                _line = ComVar.Var._strValue1;
                _mLine = ComVar.Var._strValue2;
                cCount = 30;
                tmrDate.Start();
                switch (_lang)
                {
                    case "Vi":
                        lblTitle.Text = "Nhân lực vắng mặt theo tháng";
                        simpleButton1.Text = "Ngày";
                        simpleButton3.Text = "Tháng";
                        simpleButton2.Text = "Tuần";
                        simpleButton4.Text = "Năm";
                        break;

                    case "En":
                        lblTitle.Text = "Human Absenteeism by Month";
                        simpleButton1.Text = "Day";
                        simpleButton3.Text = "Month";
                        simpleButton2.Text = "Week";
                        simpleButton4.Text = "Year";
                        break;
                }
               // lblTitle.Text = "Human Absenteeism by Month";
                btnYM = "A";
                //  axfpAbsent.SetText(1, 1, "Month");
                // loadData();
                simpleButton3.Enabled = false;
                simpleButton4.Enabled = true;
                uc_month.Visible = true;
                uc_year.Visible = false;
            }
            else
                tmrDate.Stop();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //this.Hide();
            ComVar.Var.callForm = _dtnInit["frmHome"];
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {

        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                switch (_lang)
                {
                    case "Vi":
                        lblTitle.Text = "Nhân lực vắng mặt theo năm";
                        simpleButton1.Text = "Ngày";
                        simpleButton3.Text = "Tháng";
                        simpleButton2.Text = "Tuần";
                        simpleButton4.Text = "Năm";
                        break;

                    case "En":
                        lblTitle.Text = "Human Absenteeism by Year";
                        simpleButton1.Text = "Day";
                        simpleButton3.Text = "Month";
                        simpleButton2.Text = "Week";
                        simpleButton4.Text = "Year";
                        break;
                }
                btnYM = "Y";
                axfpAbsent.SetText(1, 1, "Year");
                loadData();
                simpleButton3.Enabled = true;
                simpleButton4.Enabled = false;
                uc_month.Visible = false;
                uc_year.Visible = true;

            }
            catch { }
            finally { this.Cursor = Cursors.Default; }
            //  lblTitle.Text = "Human Absenteeism by Year";

        }
        string btnYM;
        private void simpleButton3_Click(object sender, EventArgs e)
        {
        //    lblTitle.Text = "Human Absenteeism by Month";

            try
            {
                this.Cursor = Cursors.WaitCursor;
                switch (_lang)
                {
                    case "Vi":
                        lblTitle.Text = "Nhân lực vắng mặt theo tháng";
                        simpleButton1.Text = "Ngày";
                        simpleButton3.Text = "Tháng";
                        simpleButton2.Text = "Tuần";
                        simpleButton4.Text = "Năm";
                        break;
                    case "En":
                        lblTitle.Text = "Human Absenteeism by Month";
                        simpleButton1.Text = "Day";
                        simpleButton3.Text = "Month";
                        simpleButton2.Text = "Week";
                        simpleButton4.Text = "Year";
                        break;
                }

                btnYM = "A";
                axfpAbsent.SetText(1, 1, "Month");
                loadData();
                simpleButton3.Enabled = false;
                simpleButton4.Enabled = true;
                uc_month.Visible = true;
                uc_year.Visible = false;
            }
            catch { }
            finally { this.Cursor = Cursors.Default; }
            
           
        }

        private void uc_month_ValueChangeEvent(object sender, EventArgs e)
        {
            loadData();
        }
    }
}
