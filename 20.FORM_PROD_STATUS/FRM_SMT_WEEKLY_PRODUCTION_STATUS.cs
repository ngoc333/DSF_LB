using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OracleClient;
using DevExpress.XtraCharts;

namespace FORM
{
    public partial class FRM_SMT_WEEKLY_PRODUCTION_STATUS : Form
    {
        public FRM_SMT_WEEKLY_PRODUCTION_STATUS()
        {
            InitializeComponent();
        }
        int indexScreen;
        string line, mline,Lang;
        Form[] arrForm = new Form[3];
        public FRM_SMT_WEEKLY_PRODUCTION_STATUS(string title, int _indexScreen, string _line, string _mline,string _Lang)
        {
            InitializeComponent();
            lbltitle.Text = title;
            indexScreen = _indexScreen;
            line = _line;
            mline = _mline;
            Lang = _Lang;
            switch (Lang)
            {
                case "Vn":
                    simpleButton1.Text = "Ngày";
                    simpleButton3.Text = "Tháng";
                    simpleButton4.Text = "Tuần";
                    simpleButton2.Text = "Năm";
                    break;
                case "En":
                    simpleButton1.Text = "Day";
                    simpleButton3.Text = "Month";
                    simpleButton4.Text = "Week";
                    simpleButton2.Text = "Year";
                    break;
            }
            //arrForm[0] = new FORM_SMT_PROD_DAILY("Daily Production Status", 1, _line, _mline); //ngoc 
            //arrForm[2] = new FORM_PRODUCTIONTIVITY_WEEKLY("Weekly Productivity Status", 1, _line, _mline); //Lenl
            simpleButton2.Visible = true;
            //arrForm[1] = new FRM_SMT_MON_PROD_STATUS("Monthly Production Status", 1, _line, _mline); //Lenl
            simpleButton2.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblDate.Text = string.Format(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        }

        private void load_data()
        {
            try
            {
                DataTable dt = SEL_SMT_WEEK_PROD_STATUS("Q", line, mline, "");
                DataTable dt1 = SEL_SMT_WEEK_PROD_STATUS("Q1", line, mline, "");

                if (dt != null && dt.Rows.Count > 0)
                {
                    CreateChart(chartControl1, dt, "UPC");
                    CreateChart(chartControl2, dt, "UPS1");
                    CreateChart(chartControl3, dt, "UPS2");
                    CreateChart(chartControl4, dt, "FSS");
                    CreateChart(chartControl5, dt, "FGA");
                    
                }
                if (dt1!= null && dt1.Rows.Count > 0)
                {
                    BindingGauges(circularGauge, Convert.ToDouble(dt1.Rows[0]["RATE"]), Convert.ToInt32(dt1.Rows[0]["V_MIN"]), Convert.ToInt32(dt1.Rows[0]["V_MAX"]), Convert.ToInt32(dt1.Rows[0]["R_MIN"]), Convert.ToInt32(dt1.Rows[0]["R_MAX"]));
                    lblR.Text = "<" + dt1.Rows[0]["R_MIN"].ToString() + "%";
                    lblY.Text = ">=" + dt1.Rows[0]["R_MIN"].ToString() + "% && <=" + dt1.Rows[0]["R_MAX"].ToString() + "%";
                    lblG.Text = ">" + dt1.Rows[0]["R_MAX"].ToString() + "%";
                    lblRPlan.Text = "R.Plan: " + Convert.ToDouble(dt1.Rows[0]["RPLAN"]).ToString("#,#") + "Prs";
                    lblProd.Text = "Prod: " + Convert.ToDouble(dt1.Rows[0]["PROD"]).ToString("#,#") + "Prs";
                    lblRate.Text = "Rate: " + Convert.ToDouble( dt1.Rows[0]["RATE"]).ToString("#,#.#") + "%";
                    labelRate.Text = Convert.ToDouble(dt1.Rows[0]["RATE"]).ToString("#,0.0")+"%";
                }
                else
                {
                    BindingGauges(circularGauge, 0, 0, 100, 95, 98);
                    labelRate.Text = "0%";
                }
            }
            catch
            {
            }
        }

        private void BindingGauges(DevExpress.XtraGauges.Win.Gauges.Circular.CircularGauge circularGauge, double num, int iMin, int iMax, double iRangeMin, double iRangeMax)
        {
            // DataTable dt = SEL_POD("014", "001");
            if (circularGauge.Scales.Count <= 0)
            {
                return;
            }
            circularGauge.Scales[0].EnableAnimation = false;
            //arcScaleGauges.EasingFunction = new BackEase();
            circularGauge.Scales[0].MinValue = iMin;
            circularGauge.Scales[0].MaxValue = iMax;
            //arcScaleGauges.Ranges[0].EndValue = arcScaleGauges.Ranges[1].StartValue = Convert.ToSingle(10);
            //arcScaleGauges.Ranges[1].EndValue = arcScaleGauges.Ranges[2].StartValue = Convert.ToSingle(15);
            //arcScaleGauges.Ranges[2].EndValue = Convert.ToSingle(20);
            circularGauge.Scales[0].Value = 90;

            //circularGauge.Labels[0].Text = "90";
            if (circularGauge.Scales[0].Ranges.Count >= 3)
            {
                circularGauge.Scales[0].Ranges[0].StartValue = (float)(iMin);
                circularGauge.Scales[0].Ranges[0].EndValue = (float)iRangeMin;
                circularGauge.Scales[0].Ranges[1].StartValue = (float)iRangeMin;
                circularGauge.Scales[0].Ranges[1].EndValue = (float)iRangeMax;
                circularGauge.Scales[0].Ranges[2].StartValue = (float)iRangeMax;
                circularGauge.Scales[0].Ranges[2].EndValue = (float)(iMax);

            }
            //labelGauges.Text = "0";
            //if (dt != null && dt.Rows.Count > 0)
            //{
            try
            {

                circularGauge.Scales[0].MinValue = iMin;
                circularGauge.Scales[0].MaxValue = iMax;
                //arcScaleGauges.Ranges[0].EndValue = arcScaleGauges.Ranges[1].StartValue = Convert.ToSingle(Convert.ToSingle(dt.Rows[0][0]) - 2);
                //arcScaleGauges.Ranges[1].EndValue = arcScaleGauges.Ranges[2].StartValue = Convert.ToSingle(Convert.ToSingle(dt.Rows[0][0]) + 2);
                //arcScaleGauges.Ranges[2].EndValue = Convert.ToSingle(Convert.ToSingle(dt.Rows[0][0]) + 5);

                circularGauge.Scales[0].EnableAnimation = true;
                circularGauge.Scales[0].EasingMode = DevExpress.XtraGauges.Core.Model.EasingMode.EaseInOut;
                //arcScaleGauges.EasingFunction = new BackEase();

                circularGauge.Scales[0].Value = (float)num;
                //circularGauge.Labels[0].Text = num.ToString();

            }
            catch (Exception ex)
            { }
        }
        

        private void CreateChart(DevExpress.XtraCharts.ChartControl Chart, DataTable dt, string col)
        {
            try
            {
                Chart.Series.Clear();
                Chart.Titles.Clear();

                

                string name = "";
                switch (col)
                {
                    case "UPC":
                        name = "Cutting";
                        break;
                    case "UPS1":
                        name = "Stitching 1";
                        break;
                    case "UPS2":
                        name = "Stitching 2";
                        break;
                    case "FSS":
                        name = "Stockfit";
                        break;
                    case "FGA":
                        name = "Assembly";
                        break;
                }

                Chart.AppearanceNameSerializable = "Chameleon";
                //chartBTS.AppearanceNameSerializable = "Chameleon";
                Series series1 = new Series(name, ViewType.Bar);
                Chart.AnimationStartMode = DevExpress.XtraCharts.ChartAnimationMode.OnDataChanged;
                DevExpress.XtraCharts.SideBySideBarSeriesView sideBySideBarSeriesView1 = new DevExpress.XtraCharts.SideBySideBarSeriesView();
                DevExpress.XtraCharts.RectangleGradientFillOptions rectangleGradientFillOptions1 = new DevExpress.XtraCharts.RectangleGradientFillOptions();

                Series series2 = new Series("Target", ViewType.Line);
                DevExpress.XtraCharts.LineSeriesView lineSeriesView1 = new DevExpress.XtraCharts.LineSeriesView();

                lineSeriesView1.MarkerVisibility = DevExpress.Utils.DefaultBoolean.True;
                //sideBySideBarSeriesView1.ColorEach = true;
                
                sideBySideBarSeriesView1.FillStyle.FillMode = DevExpress.XtraCharts.FillMode.Solid;
                //switch (col)
                //{
                    //case "UPC":
                    //    sideBySideBarSeriesView1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(112)))), ((int)(((byte)(192)))));
                    //    break;
                    //case "UPS1":
                    //    sideBySideBarSeriesView1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(133)))), ((int)(((byte)(155)))));
                    //    break;
                    //case "UPS2":
                    //    sideBySideBarSeriesView1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(176)))), ((int)(((byte)(240)))));
                    //    break;
                    //case "FSS":
                    //    sideBySideBarSeriesView1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(150)))), ((int)(((byte)(70)))));
                    //    break;
                    //case "FGA":
                    //    sideBySideBarSeriesView1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(208)))), ((int)(((byte)(80)))));
                    //    break;

                    
                //}
                sideBySideBarSeriesView1.Color = System.Drawing.Color.ForestGreen;
                
                series1.View = sideBySideBarSeriesView1;
                series1.Label.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                series1.Label.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
                series1.View = sideBySideBarSeriesView1;
                series1.Label.TextPattern = "{V:#,#}";
                series1.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
                //series2.Label.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                //series2.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
                //series2.Label.TextPattern = "{V:#,#}";
                lineSeriesView1.Color = System.Drawing.Color.LimeGreen;
                lineSeriesView1.LineMarkerOptions.Color = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(208)))), ((int)(((byte)(80)))));
                lineSeriesView1.LineStyle.Thickness = 3;
                series2.View = lineSeriesView1;
                
                string [] value = null;

                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        value = dt.Rows[i][col].ToString().Split('/');
                        series1.Points.Add(new SeriesPoint(dt.Rows[i]["DAY"].ToString(), value[0] ));
                        if (Convert.ToDouble(value[1]) > 0)
                        {
                            series2.Points.Add(new SeriesPoint(dt.Rows[i]["DAY"].ToString(), value[1]));
                        }
                        else
                        {
                            series2.Points.Add(new SeriesPoint(dt.Rows[i]["DAY"].ToString()));
                        }


                        if (Convert.ToDouble(value[0]) < Convert.ToDouble(value[1]))
                        {
                            series1.Points[i].Color = Color.Red;
                        }
                        else
                        {
                            //switch (col)
                            //{
                            //    case "UPC":
                            //        series1.Points[i].Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(112)))), ((int)(((byte)(192)))));
                            //        break;
                            //    case "UPS1":
                            //        series1.Points[i].Color = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(133)))), ((int)(((byte)(155)))));
                            //        break;
                            //    case "UPS2":
                            //        series1.Points[i].Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(176)))), ((int)(((byte)(240)))));
                            //        break;
                            //    case "FSS":
                            //        series1.Points[i].Color = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(150)))), ((int)(((byte)(70)))));
                            //        break;
                            //    case "FGA":
                            //        series1.Points[i].Color = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(208)))), ((int)(((byte)(80)))));
                            //        break;
                            //}
                            series1.Points[i].Color = System.Drawing.Color.ForestGreen;
                        }

                    }

                    Chart.SeriesSerializable = new DevExpress.XtraCharts.Series[] { series1, series2 };
                    Chart.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;

                    ((XYDiagram)Chart.Diagram).AxisX.Label.Font = new System.Drawing.Font("Calibri", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    ((XYDiagram)Chart.Diagram).AxisX.Title.Font = new System.Drawing.Font("Calibri", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    ((XYDiagram)Chart.Diagram).AxisY.Label.TextPattern = "{V:#,#}";
                    ((XYDiagram)Chart.Diagram).AxisY.Label.Font = new System.Drawing.Font("Tahoma", 14F);
                    ((XYDiagram)Chart.Diagram).AxisY.Title.Font = new System.Drawing.Font("Calibri", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    ((XYDiagram)Chart.Diagram).AxisY.Title.Text = "Production (Prs)";

                }                

            }
            catch (Exception EX)
            {
            }
        }

        public DataTable SEL_SMT_WEEK_PROD_STATUS(string ARG_QTYPE, string ARG_LINE, string ARG_MLINE, string ARG_DATE)
        {
            COM.OraDB MyOraDB = new COM.OraDB();
            DataSet ds_ret;
            try
            {
                string process_name = "MES.PKG_SMT_PROD.SP_SMT_WEEKLY_PROD_STATUS_NEW";

                MyOraDB.ReDim_Parameter(5);
                MyOraDB.Process_Name = process_name;

                MyOraDB.Parameter_Name[0] = "V_P_TYPE";
                MyOraDB.Parameter_Name[1] = "V_P_LINE";
                MyOraDB.Parameter_Name[2] = "V_P_MLINE";
                MyOraDB.Parameter_Name[3] = "V_P_DATE";
                MyOraDB.Parameter_Name[4] = "OUT_CURSOR";

                MyOraDB.Parameter_Type[0] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[1] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[2] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[3] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[4] = (int)OracleType.Cursor;

                MyOraDB.Parameter_Values[0] = ARG_QTYPE;
                MyOraDB.Parameter_Values[1] = ARG_LINE;
                MyOraDB.Parameter_Values[2] = ARG_MLINE;
                MyOraDB.Parameter_Values[3] = ARG_DATE;
                MyOraDB.Parameter_Values[4] = "";

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

        private void FRM_SMT_WEEKLY_PRODUCTION_STATUS_Load(object sender, EventArgs e)
        {
            
            
            timer2.Interval = 1000;
            timer2.Start();
            //load_data();
        }
        int int_count = 0;
        private void timer2_Tick(object sender, EventArgs e)
        {
            try
            {
                if (int_count < 20)
                    int_count++;
                else
                {
                    int_count = 0;
                    load_data();
                }
            }
            catch { }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void lbltitle_Click(object sender, EventArgs e)
        {
            load_data();
        }

        private void chartControl1_CustomDrawAxisLabel(object sender, CustomDrawAxisLabelEventArgs e)
        {
            try
            {
                if (e.Item.Axis is AxisX)
                {
                    e.Item.Text = e.Item.Text.Replace("_", "\n");
                }
            }
            catch
            {

            }
        }

        private void chartControl2_CustomDrawAxisLabel(object sender, CustomDrawAxisLabelEventArgs e)
        {
            try
            {
                if (e.Item.Axis is AxisX)
                {
                    e.Item.Text = e.Item.Text.Replace("_", "\n");
                }
            }
            catch
            {

            }
        }

        private void chartControl4_CustomDrawAxisLabel(object sender, CustomDrawAxisLabelEventArgs e)
        {
            try
            {
                if (e.Item.Axis is AxisX)
                {
                    e.Item.Text = e.Item.Text.Replace("_", "\n");
                }
            }
            catch
            {

            }
        }

        private void chartControl3_CustomDrawAxisLabel(object sender, CustomDrawAxisLabelEventArgs e)
        {
            try
            {
                if (e.Item.Axis is AxisX)
                {
                    e.Item.Text = e.Item.Text.Replace("_", "\n");
                }
            }
            catch
            {

            }
        }

        private void FRM_SMT_WEEKLY_PRODUCTION_STATUS_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                lblDate.Text = string.Format(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                int_count = 19;
                timer1.Start();
                timer2.Start();
            }
            else
            {
                timer1.Stop();
                timer2.Stop();
            }
            
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {                 
            string Caption = "Production Status by Month";
            switch (Lang)
            {
                case "Vn":
                    Caption = "Trạng thái sản xuất theo Tháng";
                    break;
                default:
                    Caption = "Production Status by Month";
                    break;
            }
            Form fc = Application.OpenForms["FRM_SMT_MON_PROD_STATUS"];
            if (fc != null)
            {
                //fc.Close();
                fc.Show();
                ////f.TopMost = true;
            }
            else
            {
                FRM_SMT_MON_PROD_STATUS f = new FRM_SMT_MON_PROD_STATUS(Caption, 1, line, mline, Lang);
                f.Show();
                //f.TopMost = true;
            }
            timer1.Stop();
            timer2.Stop();
            this.Hide();
        }

       

        private void chartControl2_CustomDrawAxisLabel_1(object sender, CustomDrawAxisLabelEventArgs e)
        {
            try
            {
                if (e.Item.Axis is AxisX)
                {
                    e.Item.Text = e.Item.Text.Replace("_", "\n");
                }
            }
            catch
            {

            }
        }
    }
}
