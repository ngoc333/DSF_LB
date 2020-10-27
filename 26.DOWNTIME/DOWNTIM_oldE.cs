using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OracleClient;
using DevExpress.Utils;
using System.Globalization;
using DevExpress.XtraCharts;
using System.Collections;

namespace FORM
{
    public partial class DOWNTIM_oldE : Form
    {
        public DOWNTIM_oldE()
        {
            InitializeComponent();
            timer1.Stop();


        }
        private DateTime FirstDayOfMonth_AddMethod(DateTime value)
        {
            return value.Date.AddDays(1 - value.Day).AddMonths(1 - value.Month);
        }
        int indexScreen;
        string line, Mline, Lang;
        int cCount = 0;
        Dictionary<string, string> _dtnInit = new Dictionary<string, string>();
        public DOWNTIM_oldE(string Caption, int indexScreen, string Line_cd, string Mline_cd, string Lang)
        {
            InitializeComponent();
            timer1.Stop();
            this.indexScreen = indexScreen;
            this.Mline = Mline_cd;
            this.line = Line_cd;
            //this.Lang = Lang;
            this.lblTitle.Text = Caption;
        }

        #region DB
        private DataTable SEL_DATA_OSD(string V_P_TYPE, string V_LOC_TYPE, string V_LOC_CD, string ARG_SDATE, string ARG_EDATE, string ARG_CULTURE)
        {
            System.Data.DataSet retDS;
            COM.OraDB MyOraDB = new COM.OraDB();
            DataTable data = null;

            MyOraDB.ReDim_Parameter(7);
            MyOraDB.Process_Name = "MES.SP_DOWNTIME";
            //  for (int i = 0; i < intParm; i++)

            MyOraDB.Parameter_Type[0] = (char)OracleType.VarChar;
            MyOraDB.Parameter_Type[1] = (char)OracleType.VarChar;
            MyOraDB.Parameter_Type[2] = (char)OracleType.VarChar;
            MyOraDB.Parameter_Type[3] = (char)OracleType.VarChar;
            MyOraDB.Parameter_Type[4] = (char)OracleType.VarChar;
            MyOraDB.Parameter_Type[5] = (char)OracleType.VarChar;
            MyOraDB.Parameter_Type[6] = (int)OracleType.Cursor;
            //  MyOraDB.Parameter_Type[7] = (int)OracleType.Cursor;  


            //V_P_TYPE,V_P_OPTION
            MyOraDB.Parameter_Name[0] = "V_P_TYPE";
            MyOraDB.Parameter_Name[1] = "V_LOC_TYPE";
            MyOraDB.Parameter_Name[2] = "V_LOC_CD";
            MyOraDB.Parameter_Name[3] = "ARG_SDATE";
            MyOraDB.Parameter_Name[4] = "ARG_EDATE";
            MyOraDB.Parameter_Name[5] = "ARG_CULTURE";
            MyOraDB.Parameter_Name[6] = "CV_1";
            // MyOraDB.Parameter_Name[7] = "CV_2";

            MyOraDB.Parameter_Values[0] = V_P_TYPE;
            MyOraDB.Parameter_Values[1] = V_LOC_TYPE;
            MyOraDB.Parameter_Values[2] = V_LOC_CD;
            MyOraDB.Parameter_Values[3] = ARG_SDATE;
            MyOraDB.Parameter_Values[4] = ARG_EDATE;
            MyOraDB.Parameter_Values[5] = ARG_CULTURE;
            MyOraDB.Parameter_Values[6] = "";
            //   MyOraDB.Parameter_Values[7] = "";

            MyOraDB.Add_Select_Parameter(true);
            retDS = MyOraDB.Exe_Select_Procedure();




            if (retDS == null) return null;

            return retDS.Tables[MyOraDB.Process_Name];
        }

        public void setcombo()
        {
            DataTable data = null;
            data = SEL_DATA_OSD("COM", "", "", "", "", "");

            cbocompany.DataSource = data;
            cbocompany.DisplayMember = "NAME";
            cbocompany.ValueMember = "CODE";

            data = SEL_DATA_OSD("FAC", "", "", "", "", "");
            cbofactory.DataSource = data;
            cbofactory.DisplayMember = "LOC_NM";
            cbofactory.ValueMember = "LOC_CD";

            //   datefrom.DateTime = DateTime.Now;
            dateto.DateTime = DateTime.Now;



        }

        #endregion

        public void setData(string Caption, int indexScreen, string Line_cd, string Mline_cd, string Lang)
        {
            this.indexScreen = indexScreen;
            this.Mline = Mline_cd;
            this.line = Line_cd;
            this.Lang = Lang;
            this.lblTitle.Text = Caption;
        }



        private void btnEx_Click(object sender, EventArgs e)
        {
            //this.Hide();
            //string Caption = "External OS&&D by Day";
            //FRM_SMT_OSD_DAILY_PHUOC fc = (FRM_SMT_OSD_DAILY_PHUOC)Application.OpenForms["FRM_SMT_OSD_DAILY_PHUOC"];
            //if (fc != null)
            //{
            //    fc.SetData(Caption, 1, line, Mline, Lang);
            //    fc.Show();
            //}
            //else
            //{

            //    switch (Lang)
            //    {
            //        case "Vn":
            //            Caption = "External OS&&D by Day";
            //            break;
            //        default:
            //            Caption = "External OS&&D by Day";
            //            break;
            //    }
            //    FRM_SMT_OSD_DAILY_PHUOC f = new FRM_SMT_OSD_DAILY_PHUOC(Caption, 1, line, Mline, Lang);
            //    f.Show();
            //}
        }


        private void GoFullscreen()
        {
            this.WindowState = FormWindowState.Normal;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Bounds = Screen.PrimaryScreen.Bounds;

        }

        private void FRM_SMT_OSD_INTERNAL_PHUOC_Load(object sender, EventArgs e)
        {
            GoFullscreen();
            line = ComVar.Var._strValue1;
            Mline = ComVar.Var._strValue2;
            datefrom.EditValue = FirstDayOfMonth_AddMethod(DateTime.Now).ToString("yyyy-MM-dd");
            Lang = ComVar.Var._strValue3;
            _dtnInit = ComVar.Func.getInitForm(this.GetType().Assembly.GetName().Name, this.GetType().Name);
            //string DATE_FROM = UC_MONTH.GetValue() + "01", DATE_TO =UC_MONTH.GetValue() + DateTime.DaysInMonth(Convert.ToInt32(UC_MONTH.GetValue().Substring(0, 4)), Convert.ToInt32(UC_MONTH.GetValue().Substring(4, 2))).ToString();

            //loadTopLeft(SEL_DATA_OSD("CHART", DATE_FROM, DATE_TO)); bindingDataGrid(DATE_FROM, DATE_TO);
            setcombo();
            sbtnSearch_Click(sender, e);
            ;
        }
        DataTable dtf = null;
        private void bindingDataGrid(string DATE_FROM, string DATE_TO)
        {
            // grdBase.Refresh();
            // gvwBase.Columns.Clear();
            // DataTable dt =  dtf= SEL_DATA_OSD("GRID"+ Mline, DATE_FROM, DATE_TO);
            // grdBase.DataSource = dt;

            // gvwBase.OptionsView.ColumnAutoWidth = false;
            // for (int i = 0; i < gvwBase.Columns.Count; i++)
            // {
            //     gvwBase.Columns[i].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            //     gvwBase.Columns[i].AppearanceHeader.BackColor = System.Drawing.Color.Gray;
            //     gvwBase.Columns[i].AppearanceHeader.BackColor2 = System.Drawing.Color.Gray;
            //     gvwBase.Columns[i].AppearanceHeader.ForeColor = System.Drawing.Color.White;
            //      gvwBase.Columns[i].AppearanceHeader.Font = new System.Drawing.Font("Calibri", 16F, System.Drawing.FontStyle.Bold);
            //     gvwBase.Columns[i].OptionsColumn.ReadOnly = true;
            //     gvwBase.Columns[i].OptionsColumn.AllowEdit = false;
            //     gvwBase.Columns[i].OptionsColumn.ReadOnly = true;
            //     gvwBase.Columns[i].OptionsColumn.AllowEdit = false;
            //     gvwBase.Columns[i].OptionsFilter.AllowFilter = false;
            //     gvwBase.Columns[i].OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            //     if (i < 2)
            //     {
            //         gvwBase.Columns[i].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            //         gvwBase.Columns[i].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            //         //gvwBase.Columns[i].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            //     }
            //     else
            //     {
            //         gvwBase.Columns[i].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            //         gvwBase.Columns[i].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            //         gvwBase.Columns[i].DisplayFormat.FormatString = "#,0.##";
            //     }

            //     gvwBase.Columns[i].Caption = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(gvwBase.Columns[i].GetCaption().Replace("_", " ").Replace("'", " ").ToLower()).Split(',')[0];
            //     gvwBase.Columns[0].Visible = false;
            //     if (gvwBase.Columns[i].FieldName == "TOTAL")
            //     {
            //         gvwBase.Columns[i].VisibleIndex = 999;
            //     }
            //     if (i == 1)
            //         gvwBase.Columns[i].Width = 250;
            //     else
            //         gvwBase.Columns[i].Width = 80;
            // }
            //// gvwBase.BestFitColumns();
            // //gvwBase.TopRowIndex = 0;



        }

        private void BindingChart()
        {
            try
            {
                DataTable data = null;
                data = SEL_DATA_OSD("CH", cbocompany.SelectedValue.ToString(), cbofactory.SelectedValue.ToString(), datefrom.DateTime.ToString("yyyyMMdd"), dateto.DateTime.ToString("yyyyMMdd"), "");

                chartControl1.DataSource = data;
                chartControl1.Series[0].ArgumentDataMember = "COL_NM";
                chartControl1.Series[0].ValueDataMembers.AddRange(new string[] { "TARGET" });

                chartControl1.Series[1].ArgumentDataMember = "COL_NM";
                chartControl1.Series[1].ValueDataMembers.AddRange(new string[] { "AMOUNT" });
            }
            catch { }
        }


        private void InitDataChart(string sChart, DevExpress.XtraCharts.ChartControl ChartControl, DataTable dt)
        {
            try
            {
                DataTable dtChart;

                switch (sChart)
                {
                    case "TOT":
                        dtChart = dt.Select("SUP_CD = 'TOT'").CopyToDataTable();
                        break;
                    default:
                        dtChart = dt.Select("SUP_CD = '" + sChart + "'").CopyToDataTable();
                        break;
                }


                Series series1 = new Series("REPLENISHMENT", ViewType.Bar);
                Series series2 = new Series("C.GRADE RETURN", ViewType.Bar);
                Series series3 = new Series("%", ViewType.Spline);

                for (int i = 0; i < dtChart.Rows.Count; i++)
                {
                    series1.Points.Add(new SeriesPoint(dtChart.Rows[i]["STYLE_NAME"].ToString(), dtChart.Rows[i]["QTY"]));
                    series2.Points.Add(new SeriesPoint(dtChart.Rows[i]["STYLE_NAME"].ToString(), dtChart.Rows[i]["R_QTY"]));
                    series3.Points.Add(new SeriesPoint(dtChart.Rows[i]["STYLE_NAME"].ToString(), dtChart.Rows[i]["RATE"]));
                    if (Convert.ToDouble(dtChart.Rows[i]["QTY"]) == Convert.ToDouble(dtChart.Rows[i]["R_QTY"]))
                    {

                        series1.Points[i].Color = Color.Green;
                        series2.Points[i].Color = Color.Green;
                    }
                    else
                    {
                        series1.Points[i].Color = System.Drawing.Color.FromArgb(255, 128, 0);
                        series2.Points[i].Color = System.Drawing.Color.DodgerBlue;
                    }

                }
                ChartControl.SeriesSerializable = new DevExpress.XtraCharts.Series[] { series1, series2, series3 };


                //ChartControl.DataSource = dtChart;
                //ChartControl.Series[0].ArgumentDataMember = "STYLE_NAME";
                //ChartControl.Series[0].ValueDataMembers.AddRange(new string[] { "QTY" });
                //ChartControl.Series[1].ArgumentDataMember = "STYLE_NAME";
                //ChartControl.Series[1].ValueDataMembers.AddRange(new string[] { "R_QTY" });
                //ChartControl.Series[2].ArgumentDataMember = "STYLE_NAME";
                //ChartControl.Series[2].ValueDataMembers.AddRange(new string[] { "RATE" });


            }
            catch { }
        }


        private bool loadTopLeft(DataTable DTF)
        {
            try
            {
                //chartControl1.DataSource = DTF;
                //chartControl1.Series[0].ArgumentDataMember = "YMD";
                //chartControl1.Series[0].ValueDataMembers.AddRange(new string[] { "OSD_QTY" });
                //chartControl1.Series[1].ArgumentDataMember = "YMD";
                //chartControl1.Series[1].ValueDataMembers.AddRange(new string[] { "RETURN_QTY" });
                //chartControl1.Series[2].ArgumentDataMember = "YMD";
                //chartControl1.Series[2].ValueDataMembers.AddRange(new string[] { "PER" });
                Series series1 = new Series("REPLENISHMENT", ViewType.Bar);
                Series series2 = new Series("C.GRADE RETURN", ViewType.Bar);
                Series series3 = new Series("%", ViewType.Spline);


                for (int i = 0; i < DTF.Rows.Count; i++)
                {
                    series1.Points.Add(new SeriesPoint(DTF.Rows[i]["YMD"].ToString(), DTF.Rows[i]["OSD_QTY"]));
                    series2.Points.Add(new SeriesPoint(DTF.Rows[i]["YMD"].ToString(), DTF.Rows[i]["RETURN_QTY"]));
                    series3.Points.Add(new SeriesPoint(DTF.Rows[i]["YMD"].ToString(), DTF.Rows[i]["PER"]));

                    if (Convert.ToDouble(DTF.Rows[i]["OSD_QTY"]) == Convert.ToDouble(DTF.Rows[i]["RETURN_QTY"]))
                    {

                        series1.Points[i].Color = Color.Green;
                        series2.Points[i].Color = Color.Green;
                    }
                    else
                    {
                        series1.Points[i].Color = System.Drawing.Color.FromArgb(255, 128, 0);
                        series2.Points[i].Color = System.Drawing.Color.DodgerBlue;
                    }

                }
                chartControl1.SeriesSerializable = new DevExpress.XtraCharts.Series[] { series1, series2, series3 };

            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //this.Hide();
            ComVar.Var.callForm = _dtnInit["frmHome"];
        }

        private void UC_MONTH_ValueChangeEvent(object sender, EventArgs e)
        {
            //try
            //{
            //    this.Cursor = Cursors.WaitCursor;
            //    string DATE_FROM = UC_MONTH.GetValue() + "01", DATE_TO = UC_MONTH.GetValue() + DateTime.DaysInMonth(Convert.ToInt32(UC_MONTH.GetValue().Substring(0, 4)), Convert.ToInt32(UC_MONTH.GetValue().Substring(4, 2))).ToString();
            //    loadTopLeft(SEL_DATA_OSD("CHART1", DATE_FROM, DATE_TO)); 
            //    bindingDataGrid(DATE_FROM, DATE_TO);
            //    BindingChart();
            //    this.Cursor = Cursors.Default;
            //}
            //catch { this.Cursor = Cursors.Default; }
        }

        private void gvwBase_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            // return;

            if (e.Column.ColumnHandle > 1)
            {
                if (e.RowHandle == gvwBase.RowCount - 1)
                {
                    if (e.CellValue.ToString() != "")
                    {
                        if (double.Parse(e.CellValue.ToString()) < 80)
                        {
                            e.Appearance.BackColor = Color.Red;
                        }
                        if (double.Parse(e.CellValue.ToString()) > 90)
                        {
                            e.Appearance.BackColor = Color.Green;
                        }
                        if (double.Parse(e.CellValue.ToString()) >= 80 && double.Parse(e.CellValue.ToString()) <= 90)
                        {
                            e.Appearance.BackColor = Color.Yellow;
                        }
                    }
                }

            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //cCount++;
            //lblDate.Text = string.Format(DateTime.Now.ToString("yyyy-MM-dd\nHH:mm:ss"));
            //if (cCount >= 40)
            //{
            //    try
            //    {
            //        this.Cursor = Cursors.WaitCursor;
            //        string DATE_FROM = UC_MONTH.GetValue() + "01", DATE_TO = UC_MONTH.GetValue() + DateTime.DaysInMonth(Convert.ToInt32(UC_MONTH.GetValue().Substring(0, 4)), Convert.ToInt32(UC_MONTH.GetValue().Substring(4, 2))).ToString();
            //        loadTopLeft(SEL_DATA_OSD("CHART1", DATE_FROM, DATE_TO)); 
            //        bindingDataGrid(DATE_FROM, DATE_TO);
            //        BindingChart();
            //        this.Cursor = Cursors.Default;
            //        cCount = 0;
            //    }
            //    catch { this.Cursor = Cursors.Default; cCount = 0; }
            //}
        }

        private void FRM_SMT_OSD_INTERNAL_PHUOC_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                cCount = 39;
                line = ComVar.Var._strValue1;
                Mline = ComVar.Var._strValue2;
                //line = "001";
                //Mline = "000";
                Lang = ComVar.Var._strValue3;
                timer1.Start();
            }
            else
                timer1.Stop();
        }

        private void chartControl1_CustomDrawSeriesPoint(object sender, DevExpress.XtraCharts.CustomDrawSeriesPointEventArgs e)
        {

        }

        private void gvwBase_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            //if (e.Column.ColumnHandle < 2 || e.Clicks <= 1) return;
            //DataTable dtF = new DataTable();
            //if (e.RowHandle == gvwBase.RowCount - 2 && e.Column.ColumnHandle != 2)
            //{
            //    dtF = SEL_DATA_OSD_POP1("Q1"+Mline, dtf.Columns[e.Column.ColumnHandle].ColumnName.ToString());
            //    Popup.FRM_SMT_INTERNAL_POPUP_1 POPUP_1 = new Popup.FRM_SMT_INTERNAL_POPUP_1(dtF);
            //    POPUP_1.ShowDialog();
            //}

            //if (e.RowHandle == gvwBase.RowCount - 1 && e.Column.ColumnHandle != 2)
            //{
            //    dtF = SEL_DATA_OSD_POP1("Q2" + Mline, dtf.Columns[e.Column.ColumnHandle].ColumnName.ToString());
            //    Popup.FRM_SMT_INTERNAL_POPUP_2 POPUP_2 = new Popup.FRM_SMT_INTERNAL_POPUP_2(dtF);
            //    POPUP_2.ShowDialog();
            //}
        }

        private void lblTitle_DoubleClick(object sender, EventArgs e)
        {
            cCount = 39;
        }



        private void Search_Click(object sender, EventArgs e)
        {
            DataTable data = null;
            data = SEL_DATA_OSD("Q", cbocompany.SelectedValue.ToString(), cbofactory.SelectedValue.ToString(), datefrom.DateTime.ToString("yyyyMMdd"), dateto.DateTime.ToString("yyyyMMdd"), "");

            grdBase.DataSource = data;

            gvwBase.Columns["LOCATION"].OptionsColumn.AllowMerge = DefaultBoolean.True;

            BindingChart();

        }

        private void sbtnSearch_Click(object sender, EventArgs e)
        {
            DataTable data = null;
            data = SEL_DATA_OSD("Q", cbocompany.SelectedValue.ToString(), cbofactory.SelectedValue.ToString(), datefrom.DateTime.ToString("yyyyMMdd"), dateto.DateTime.ToString("yyyyMMdd"), "");

            grdBase.DataSource = data;
            for (int i = 2; i < gvwBase.Columns.Count; i++)
            {
                gvwBase.Columns[i].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                gvwBase.Columns[i].DisplayFormat.FormatString = "#,0.##";
            }

            //  gvwBase.Columns["LOCATION"].OptionsColumn.AllowMerge = DefaultBoolean.True;

            BindingChart();
        }









    }
}
