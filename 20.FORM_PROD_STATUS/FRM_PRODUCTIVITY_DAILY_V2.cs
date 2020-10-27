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
using DevExpress.XtraCharts;
using DevExpress.XtraGauges.Core.Model;

namespace FORM
{
    public partial class FRM_PRODUCTIVITY_DAILY_V2 : Form
    {
        public FRM_PRODUCTIVITY_DAILY_V2()
        {
            InitializeComponent();
        }
        string line, mline, lang;
        #region db
        Database db = new Database();
        DataTable _dtXML = null;
        DataTable dt = null;
        #endregion
        #region UC
        UC.UC_DWMY uc = new UC.UC_DWMY(1,""); //Hiển thị 4 Button, Button Day thì Disable
        #endregion
        Dictionary<string, string> _dtnInit = new Dictionary<string, string>();
        int cCount = 0;

        public DataTable SP_SMT_PROD_DAILY_NEW(string ARG_DATE, string ARG_LINE_CD, string ARG_MLINE_CD)
        {
            COM.OraDB MyOraDB = new COM.OraDB();
            DataSet ds_ret;
            try
            {
                string process_name = "MES.PKG_SMT_LONGTHANH.SMT_PRODUCTIVITY_DAILY_SELECT";
                MyOraDB.ReDim_Parameter(4);
                MyOraDB.Process_Name = process_name;
                MyOraDB.Parameter_Name[0] = "ARG_DATE";
                MyOraDB.Parameter_Name[1] = "ARG_LINE_CD";
                MyOraDB.Parameter_Name[2] = "ARG_MLINE_CD";
                MyOraDB.Parameter_Name[3] = "OUT_CURSOR";
                MyOraDB.Parameter_Type[0] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[1] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[2] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[3] = (int)OracleType.Cursor;
                MyOraDB.Parameter_Values[0] = ARG_DATE;
                MyOraDB.Parameter_Values[1] = ARG_LINE_CD;
                MyOraDB.Parameter_Values[2] = ARG_MLINE_CD;
                MyOraDB.Parameter_Values[3] = "";


                MyOraDB.Add_Select_Parameter(true);
                ds_ret = MyOraDB.Exe_Select_Procedure();

                if (ds_ret == null) return null;
                return ds_ret.Tables[process_name];
            }
            catch
            {
                return null;
            }
        }

        private void FRM_PROD_STATUS_DAILY_Load(object sender, EventArgs e)
        {
            lblDate.Text = string.Format(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            _dtnInit = ComVar.Func.getInitForm(this.GetType().Assembly.GetName().Name, this.GetType().Name);
            pnYMD.Controls.Add(uc);
            uc.OnDWMYClick += DWMYClick;
            line = ComVar.Var._strValue1; mline = ComVar.Var._strValue2; lang = ComVar.Var._strValue3;

        }

        void DWMYClick(string ButtonCap, string ButtonCD)
        {
            switch (ButtonCD)
            {
                case "C":
                    ComVar.Var.callForm = _dtnInit["frmHome"];
                    break;
                case "D":
                    ComVar.Var.callForm = _dtnInit["frmDay"];
                    break;
                case "W":
                    ComVar.Var.callForm = _dtnInit["frmWeek"];
                    break;
                case "M":
                    ComVar.Var.callForm = _dtnInit["frmMonth"];
                    break;
                case "Y":
                    ComVar.Var.callForm = _dtnInit["frmYear"];
                    break;
            }
        }

        private void BindingData()
        {
            try
            {
                var sTitle = new List<string>();
                int mLineStart = Convert.ToInt32(line.Equals("099") ? mline : line);
                if (mLineStart == 3)
                    mLineStart = 2;
                else if (mLineStart == 2)
                    mLineStart = 3;
                switch (line)
                {

                    case "001":
                    case "002":
                    case "003":
                    case "004":
                    case "005":
                    case "006":
                        for (int i = 1; i <= 3; i++)
                            sTitle.Add("Stitching " + ((mLineStart - 1) * 3 + i).ToString());
                        break;
                    default:
                        for (int i = 1; i <= 2; i++)
                            sTitle.Add("Stitching " + ((mLineStart - 1) * 2 + i).ToString());
                        break;
                }
                dt = SP_SMT_PROD_DAILY_NEW("", line, mline); ;
                grdBase.DataSource = dt;
                for (int i = 0; i < bdgrdView.Columns.Count; i++)
                {
                    bdgrdView.Columns[i].OptionsColumn.ReadOnly = true;
                    bdgrdView.Columns[i].OptionsColumn.AllowEdit = false;
                    bdgrdView.Columns[i].AppearanceCell.Font = new System.Drawing.Font("Calibri", 20f);
                    bdgrdView.Columns[i].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                }
                grdBandStit1.Caption = sTitle[0];
                grdBandStit2.Caption = sTitle[1];
                grdBandStit3.Caption = string.IsNullOrEmpty(sTitle[2]) ? "STITCHING 3" : sTitle[2];
            }
            catch { }
        }

        private void BindingChart()
        {
            InitChart("UPC", dt, chartUPC); InitChart("UPS1", dt, chartUPS1); InitChart("UPS2", dt, chartUPS2); InitChart("UPS3", dt, chartUPS3);
        }

        private void InitChart(string sChart, DataTable dt, DevExpress.XtraCharts.ChartControl chartControl)
        {
            try
            {
                var sTitle = new List<string>();
                int mLineStart = Convert.ToInt32(line.Equals("099") ? mline : line);
                if (mLineStart == 3)
                    mLineStart = 2;
                else if (mLineStart == 2)
                    mLineStart = 3;
                if (!sChart.Equals("UPC"))
                {
                    switch (line)
                    {

                        case "001":
                        case "002":
                        case "003":
                        case "004":
                        case "005":
                        case "006":
                            for (int i = 1; i <= 3; i++)
                                sTitle.Add("Stitching " + ((mLineStart - 1) * 3 + i).ToString());
                            break;
                        default:
                            for (int i = 1; i <= 2; i++)
                                sTitle.Add("Stitching " + ((mLineStart - 1) * 2 + i).ToString());
                            break;
                    }
                }
                DevExpress.XtraCharts.ChartTitle chartTitle = new DevExpress.XtraCharts.ChartTitle();
                chartControl.DataSource = dt.Select("HMS <>'TOTAL'").CopyToDataTable();
                chartControl.Series[0].ArgumentDataMember = "HMS";
                chartControl.Series[0].ValueDataMembers.AddRange(new string[] { sChart + "_RATE1" });
                ((DevExpress.XtraCharts.XYDiagram)chartControl.Diagram).AxisX.QualitativeScaleOptions.AutoGrid = false;
                if (!sChart.Equals("UPC"))
                {
                    chartTitle.Text = sTitle[Convert.ToInt32(sChart.Substring(3, 1)) - 1];
                    chartControl.Titles.Clear();
                    chartControl.Titles.AddRange(new DevExpress.XtraCharts.ChartTitle[] { chartTitle });
                }
            }
            catch { }
            finally {
                BindingGauge(dt);
            }
        }

        private void BindingGauge(DataTable arg_dt)
        {
            lblTitleProd.Text = "POD: 0";
            arcScale.EnableAnimation = false;
            arcScale.EasingMode = DevExpress.XtraGauges.Core.Model.EasingMode.EaseIn;
            arcScale.EasingFunction = new BackEase();
            arcScale.Value = 0;
            if (arg_dt != null && arg_dt.Rows.Count > 0)
            {
                try
                {

                    //arcScale.MinValue = Convert.ToSingle(arg_dt.Rows[0]["MIN_VALUE"]);
                    //arcScale.MaxValue = Convert.ToSingle(arg_dt.Rows[0]["MAX_VALUE"]);
                    arcScale.Ranges[0].StartValue = 6;
                    arcScale.Ranges[0].EndValue = arcScale.Ranges[1].StartValue = Convert.ToSingle(arg_dt.Rows[0]["R_MIN"]); ;
                    arcScale.Ranges[1].EndValue = arcScale.Ranges[2].StartValue = Convert.ToSingle(arg_dt.Rows[0]["R_MAX"]); ;
                    arcScale.Ranges[2].EndValue = 12;

                    DataTable dt = arg_dt.Select("HMS = 'TOTAL'").CopyToDataTable();
                    arcScale.EnableAnimation = true;
                    arcScale.EasingMode = DevExpress.XtraGauges.Core.Model.EasingMode.EaseInOut;
                    arcScale.EasingFunction = new BackEase();
                    double num = Convert.ToDouble(dt.Rows[0]["POD"]);
                    arcScale.Value = (float)num;

                    lblRed.Text = "<" + arg_dt.Rows[0]["R_MIN"].ToString();
                    lblYellow.Text = arg_dt.Rows[0]["R_MIN"].ToString() + " ~ " + arg_dt.Rows[0]["R_MAX"].ToString();
                    lblGreen.Text = ">" + arg_dt.Rows[0]["R_MAX"].ToString();
                    lblTitleProd.Text = "POD: " + num.ToString();
                }
                catch (Exception ex)
                { }
            }
        }

        private void tmr_Tick(object sender, EventArgs e)
        {
            lblDate.Text = string.Format(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            cCount++;
            if (cCount >= 30)
            {
                BindingData();
                BindingChart();
                cCount = 0;
            }
        }

        private void FRM_PROD_STATUS_DAILY_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                lang = ComVar.Var._strValue3;
                uc.YMD_Change(1,lang);
                cCount = 30 - 1;
                line = ComVar.Var._strValue1; mline = ComVar.Var._strValue2; 
                //line = "099"; mline = "001";
                switch (line)
                {
                    case "099":
                        grdBandStit3.Visible = false;
                        //chartUPS3.Visible = false;
                        tblMain.ColumnStyles[0].Width = 38.00F;
                        tblMain.ColumnStyles[0].SizeType = SizeType.Percent;
                        tblMain.ColumnStyles[1].Width = 38.00F;
                        tblMain.ColumnStyles[1].SizeType = SizeType.Percent;
                        tblMain.ColumnStyles[2].Width = 0;
                        tblMain.ColumnStyles[2].SizeType = SizeType.Percent;
                        break;
                    default:
                        grdBandStit3.Visible = true;
                        //chartUPS3.Visible = true;
                        tblMain.ColumnStyles[0].Width = 25.00F;
                        tblMain.ColumnStyles[0].SizeType = SizeType.Percent;
                        tblMain.ColumnStyles[1].Width = 25.00F;
                        tblMain.ColumnStyles[1].SizeType = SizeType.Percent;
                        tblMain.ColumnStyles[2].Width = 25.00F;
                        tblMain.ColumnStyles[2].SizeType = SizeType.Percent;
                        break;
                }
                switch (lang)
                {
                    case "Vi":
                        lang = "Vi";
                        lbltitle.Text = "Tình trạng năng suất theo ngày";
                        break;
                    default:
                        lbltitle.Text = "Productivity Status by Day";
                        break;

                }
                tmr.Start();
            }
            else
                tmr.Stop();

        }

        private void bdgrdView_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            try
            {


                if (bdgrdView.GetRowCellValue(e.RowHandle, "HMS").ToString().Contains("CURR"))
                {
                    e.Appearance.BackColor = Color.Salmon;
                    e.Appearance.ForeColor = Color.White;
                }

                if (e.Column.FieldName.Contains("RATE"))
                {
                    if (e.CellValue.ToString().Contains("G"))
                    {
                        e.Appearance.BackColor = Color.LimeGreen;
                        e.Appearance.ForeColor = Color.Black;
                    }
                    else if (e.CellValue.ToString().Contains("R"))
                    {
                        e.Appearance.BackColor = Color.Red;
                        e.Appearance.ForeColor = Color.White;
                    }
                    else if (e.CellValue.ToString().Contains("Y"))
                    {
                        e.Appearance.BackColor = Color.Yellow;
                        e.Appearance.ForeColor = Color.Black;
                    }
                }
                if (bdgrdView.GetRowCellValue(e.RowHandle, "HMS").ToString().Contains("TOTAL"))
                {
                    e.Appearance.BackColor = Color.FromArgb(254, 255, 198);
                    e.Appearance.ForeColor = Color.Black;
                }
            }
            catch (Exception ex)
            { }
        }

        private void chart_CustomDrawAxisLabel(object sender, DevExpress.XtraCharts.CustomDrawAxisLabelEventArgs e)
        {
            try
            {
                AxisBase axis = e.Item.Axis;
                if (axis is AxisX)
                {
                    e.Item.Text = e.Item.Text.Substring(0, 1);
                    e.Item.TextColor = Color.Green;
                    e.Item.EnableAntialiasing = DefaultBoolean.True;
                }
            }
            catch { }
        }
    }
}
