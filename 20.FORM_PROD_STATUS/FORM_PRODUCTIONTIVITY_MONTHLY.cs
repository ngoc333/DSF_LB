using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OracleClient;
using System.Drawing.Drawing2D;
using DevExpress.XtraCharts;
using DevExpress.XtraGauges.Core.Model;
using System.Runtime.InteropServices;
//using Microsoft.VisualBasic.PowerPacks;
//using C1.Win.C1FlexGrid;

namespace FORM
{
    public partial class FORM_PRODUCTIONTIVITY_MONTHLY : Form
    {
        
    

        public FORM_PRODUCTIONTIVITY_MONTHLY()
        {
            InitializeComponent();
            _wh_cd = ComVar.Var._bValue1.ToString();
            _mline_cd = ComVar.Var._bValue2.ToString();
        }
        [DllImport("user32")]
        static extern bool AnimateWindow(IntPtr hwnd, int time, int flags);
        const int AW_SLIDE = 0X40000;
        const int AW_HOR_POSITIVE = 0X4;
        const int AW_HOR_NEGATIVE = 0X2;
        const int AW_BLEND = 0X80000;
        const int AW_HIDE = 0x00010000;

       int indexScreen;
       #region Variable
       bool _load = true;
       int _icount = 0;
       string _wh_cd, _mline_cd,Lang;
       //init strline = new init();
       #endregion
       #region db
       Database db = new Database();
       DataTable _dtXML = null;
       Dictionary<string, string> _dtnInit = new Dictionary<string, string>();
       #endregion
       #region UC
       UC.UC_DWMY uc = new UC.UC_DWMY(3);
       #endregion
       Form[] arrForm = new Form[3];
       public FORM_PRODUCTIONTIVITY_MONTHLY(string Title, int _indexScreen, string line_cd, string mline_cd,string _Lang)
        {
           
           InitializeComponent();
           indexScreen = _indexScreen;
           //_wh_cd = line_cd;
           //_mline_cd = mline_cd;
           _wh_cd = ComVar.Var._bValue1.ToString();
           _mline_cd = ComVar.Var._bValue2.ToString();
           Lang = _Lang;
      
            lblTitle.Text = Title;
        }
     
       
     
        #region Func

        private void GoFullscreen(bool fullscreen)
        {
            if (fullscreen)
            {
                this.WindowState = FormWindowState.Normal;
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                this.Bounds = Screen.PrimaryScreen.Bounds;
            }
            else
            {
                this.WindowState = FormWindowState.Maximized;
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            }
        }

        private void showAnimation(Control flg)
        {
            //flg.Hide();
            this.Cursor = Cursors.WaitCursor;
            load_data();
            //AnimateWindow(flg.Handle, 300, AW_SLIDE | 0X4); //IPEX_Monitor.ClassLib.WinAPI.getSlidType("2")
           // flg.Show();
            this.Cursor = Cursors.Default;
        }


        private void BindingPOD(DataTable arg_dt)
        {
            
            arcScaleTrucks.EnableAnimation = false;
            arcScaleTrucks.EasingMode = DevExpress.XtraGauges.Core.Model.EasingMode.EaseIn;
            arcScaleTrucks.EasingFunction = new BackEase();
            //  arcScaleTrucks.MinValue = 0;
            // arcScaleTrucks.MaxValue = 20;
            //arcScaleTrucks.Ranges[0].EndValue = arcScaleTrucks.Ranges[1].StartValue = Convert.ToSingle(10);
            //arcScaleTrucks.Ranges[1].EndValue = arcScaleTrucks.Ranges[2].StartValue = Convert.ToSingle(15);
            //arcScaleTrucks.Ranges[2].EndValue = Convert.ToSingle(20);
            arcScaleTrucks.Value = 0;
           // lblGaugesValue.Text = "0";
            if (arg_dt != null && arg_dt.Rows.Count > 0)
            {
                try
                {

                    arcScaleTrucks.MinValue = Convert.ToSingle(arg_dt.Rows[0]["MIN_VALUE"]);
                    arcScaleTrucks.MaxValue = Convert.ToSingle(arg_dt.Rows[0]["MAX_VALUE"]);
                    arcScaleTrucks.Ranges[0].StartValue = Convert.ToSingle(arg_dt.Rows[0]["MIN_VALUE"]);
                    arcScaleTrucks.Ranges[0].EndValue = arcScaleTrucks.Ranges[1].StartValue = Convert.ToSingle(arg_dt.Rows[0]["YELLOW_VALUE"]); ;
                    arcScaleTrucks.Ranges[1].EndValue = arcScaleTrucks.Ranges[2].StartValue = Convert.ToSingle(arg_dt.Rows[0]["GREEN_VALUE"]); ;
                    arcScaleTrucks.Ranges[2].EndValue = Convert.ToSingle(arg_dt.Rows[0]["MAX_VALUE"]); 


                    arcScaleTrucks.EnableAnimation = true;
                    arcScaleTrucks.EasingMode = DevExpress.XtraGauges.Core.Model.EasingMode.EaseInOut;
                    arcScaleTrucks.EasingFunction = new BackEase();
                    double num = Convert.ToDouble(arg_dt.Rows[0]["POD"]); //GetRandomNumber(20, 90);
                    arcScaleTrucks.Value = (float)num;

                    lblRed.Text = "<" + arg_dt.Rows[0]["YELLOW_VALUE"].ToString();
                    lblYellow.Text = arg_dt.Rows[0]["YELLOW_VALUE"].ToString() + " ~ " + arg_dt.Rows[0]["GREEN_VALUE"].ToString();
                    lblGreen.Text = ">" +arg_dt.Rows[0]["GREEN_VALUE"].ToString();

                    //if (num < Convert.ToDouble(arg_dt.Rows[0]["YELLOW_VALUE"]))
                    //{
                    //    lblGaugesValue.ForeColor = Color.Red;
                    //}
                    //else if (num >= Convert.ToDouble(arg_dt.Rows[0]["YELLOW_VALUE"]) && num < Convert.ToDouble(arg_dt.Rows[0]["GREEN_VALUE"]))
                    //{
                    //    lblGaugesValue.ForeColor = Color.Yellow;
                    //}
                    //else if (num >= Convert.ToDouble(arg_dt.Rows[0]["GREEN_VALUE"]))
                    //{
                    //    lblGaugesValue.ForeColor = Color.Green;
                    //}
                    lblGaugesValue.Text = num.ToString();
                }
                catch
                { }
            }
        }

        private void CreateChartLine(ChartControl arg_chart, DataTable arg_dt, string arg_name)
        {
            if (arg_dt == null || arg_dt.Rows.Count == 0) return;
            arg_chart.Series.Clear();
            arg_chart.Titles.Clear();
            
            //----------create--------------------
            Series series2 = new Series("POD", ViewType.Spline);

            DevExpress.XtraCharts.SplineSeriesView splineSeriesView1 = new DevExpress.XtraCharts.SplineSeriesView();
            //DevExpress.XtraCharts.SideBySideBarSeriesView sideBySideBarSeriesView1 = new DevExpress.XtraCharts.SideBySideBarSeriesView();
            //DevExpress.XtraCharts.PointSeriesLabel pointSeriesLabel1 = new DevExpress.XtraCharts.PointSeriesLabel();
            //DevExpress.XtraCharts.BarWidenAnimation barWidenAnimation1 = new DevExpress.XtraCharts.BarWidenAnimation();
            //DevExpress.XtraCharts.ElasticEasingFunction elasticEasingFunction1 = new DevExpress.XtraCharts.ElasticEasingFunction();
            //DevExpress.XtraCharts.XYSeriesBlowUpAnimation xySeriesBlowUpAnimation1 = new DevExpress.XtraCharts.XYSeriesBlowUpAnimation();
            DevExpress.XtraCharts.XYSeriesUnwindAnimation xySeriesUnwindAnimation1 = new DevExpress.XtraCharts.XYSeriesUnwindAnimation();
            //DevExpress.XtraCharts.XYSeriesUnwrapAnimation xySeriesUnwrapAnimation1 = new DevExpress.XtraCharts.XYSeriesUnwrapAnimation();
            //DevExpress.XtraCharts.PowerEasingFunction powerEasingFunction1 = new DevExpress.XtraCharts.PowerEasingFunction();
            DevExpress.XtraCharts.SineEasingFunction sineEasingFunction1 = new DevExpress.XtraCharts.SineEasingFunction();
            DevExpress.XtraCharts.ConstantLine constantLine1 = new DevExpress.XtraCharts.ConstantLine();

            //--------- Add data Point------------
            for (int i = 0; i < arg_dt.Rows.Count; i++)
            {
                if (arg_dt.Rows[i]["POD"] == null || arg_dt.Rows[i]["POD"].ToString() == "")
                    series2.Points.Add(new SeriesPoint(arg_dt.Rows[i]["LB"].ToString().Replace("_","\n") ));
                else
                    series2.Points.Add(new SeriesPoint(arg_dt.Rows[i]["LB"].ToString().Replace("_", "\n"), arg_dt.Rows[i]["POD"]));
            }
            
            arg_chart.SeriesSerializable = new DevExpress.XtraCharts.Series[] { series2 };

            

            //title
            DevExpress.XtraCharts.ChartTitle chartTitle2 = new DevExpress.XtraCharts.ChartTitle();
            chartTitle2.Alignment = System.Drawing.StringAlignment.Near;
            chartTitle2.Font = new System.Drawing.Font("Calibri", 24F, System.Drawing.FontStyle.Bold);
            chartTitle2.Text = arg_name;
            chartTitle2.TextColor = System.Drawing.Color.Black;
            arg_chart.Titles.AddRange(new DevExpress.XtraCharts.ChartTitle[] { chartTitle2 });


            // format Series 
            splineSeriesView1.MarkerVisibility = DevExpress.Utils.DefaultBoolean.True;
            splineSeriesView1.Color = System.Drawing.Color.DodgerBlue;
            splineSeriesView1.LineMarkerOptions.BorderColor = System.Drawing.Color.DodgerBlue;
            splineSeriesView1.LineMarkerOptions.BorderVisible = false;
            splineSeriesView1.LineMarkerOptions.Kind = DevExpress.XtraCharts.MarkerKind.Circle;
            splineSeriesView1.LineMarkerOptions.Color = System.Drawing.Color.DodgerBlue;
            splineSeriesView1.LineMarkerOptions.Size = 10;
            
            splineSeriesView1.LineStyle.Thickness = 3;
            series2.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            series2.Label.ResolveOverlappingMode = ResolveOverlappingMode.JustifyAllAroundPoint;
            //series2.Label.TextPattern = "{V:#,0}";
            series2.View = splineSeriesView1;

            xySeriesUnwindAnimation1.EasingFunction = sineEasingFunction1;
            splineSeriesView1.SeriesAnimation = xySeriesUnwindAnimation1;

            arg_chart.Legend.Direction = LegendDirection.LeftToRight;

            //Constant line
            //constantLine1.ShowInLegend = false;
            constantLine1.AxisValueSerializable = arg_dt.Rows[0]["TARGET"].ToString();
            constantLine1.Color = System.Drawing.Color.Green;
            constantLine1.Name = "Target";
           // constantLine1.ShowBehind = false;
            constantLine1.Title.Visible = false;
            constantLine1.Title.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            //constantLine1.Title.Text = "Target";
            constantLine1.LineStyle.Thickness = 2;
           // constantLine1.Title.Alignment = DevExpress.XtraCharts.ConstantLineTitleAlignment.Far;
            ((XYDiagram)arg_chart.Diagram).AxisY.ConstantLines.Clear();
            ((XYDiagram)arg_chart.Diagram).AxisY.ConstantLines.AddRange(new DevExpress.XtraCharts.ConstantLine[] { constantLine1 });


            //((XYDiagram)arg_chart.Diagram).AxisX.Tickmarks.MinorVisible = false;
            ((XYDiagram)arg_chart.Diagram).AxisX.VisualRange.Auto = false;
            ((XYDiagram)arg_chart.Diagram).AxisX.VisualRange.AutoSideMargins = false;
            ((XYDiagram)arg_chart.Diagram).AxisX.VisualRange.SideMarginsValue = 2;
            ((XYDiagram)arg_chart.Diagram).AxisX.Label.Angle = 0;
            ((XYDiagram)arg_chart.Diagram).AxisX.Label.Font = new System.Drawing.Font("Tahoma", 10, System.Drawing.FontStyle.Bold);
            ((XYDiagram)arg_chart.Diagram).AxisX.NumericScaleOptions.ScaleMode = DevExpress.XtraCharts.ScaleMode.Continuous;
           ((XYDiagram)arg_chart.Diagram).AxisY.Label.Font = new System.Drawing.Font("Tahoma", 10, System.Drawing.FontStyle.Bold);
            ((XYDiagram)arg_chart.Diagram).AxisX.Title.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ((XYDiagram)arg_chart.Diagram).AxisY.Title.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            //--------Text AxisX/ AxisY
            ((XYDiagram)arg_chart.Diagram).AxisY.Title.Text = "POD";
            ((XYDiagram)arg_chart.Diagram).AxisY.Title.TextColor = System.Drawing.Color.Orange;
            ((XYDiagram)arg_chart.Diagram).AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.Default;
            ((XYDiagram)arg_chart.Diagram).AxisX.Title.Text = "Date";
            ((XYDiagram)arg_chart.Diagram).AxisX.Title.Visibility = DevExpress.Utils.DefaultBoolean.Default;
            ((XYDiagram)arg_chart.Diagram).AxisX.Title.TextColor = System.Drawing.Color.Orange;




            
            //---------------add chart in panel
            pn_body.Controls.Add(arg_chart);
        }

        private void CreateChartBar(ChartControl arg_chart, DataTable arg_dt, string arg_name)
        {
            // Create a new chart.
            arg_chart.Series.Clear();
            arg_chart.Titles.Clear();
          //  ((XYDiagram)arg_chart.Diagram).AxisX.CustomLabels.Clear();
            //DataSource
            string Now = DateTime.Now.ToString("yyyyMMdd");


            // Create two series.
            //Series series1 = new Series("Production Qty", ViewType.Bar);
            Series series2 = new Series("POD", ViewType.Bar);

           // DevExpress.XtraCharts.SplineSeriesView splineSeriesView1 = new DevExpress.XtraCharts.SplineSeriesView();
            DevExpress.XtraCharts.SideBySideBarSeriesView sideBySideBarSeriesView1 = new DevExpress.XtraCharts.SideBySideBarSeriesView();
            DevExpress.XtraCharts.PointSeriesLabel pointSeriesLabel1 = new DevExpress.XtraCharts.PointSeriesLabel();
            //DevExpress.XtraCharts.BarWidenAnimation barWidenAnimation1 = new DevExpress.XtraCharts.BarWidenAnimation();
            //DevExpress.XtraCharts.ElasticEasingFunction elasticEasingFunction1 = new DevExpress.XtraCharts.ElasticEasingFunction();


           // DevExpress.XtraCharts.XYSeriesBlowUpAnimation xySeriesBlowUpAnimation1 = new DevExpress.XtraCharts.XYSeriesBlowUpAnimation();
            DevExpress.XtraCharts.XYSeriesUnwindAnimation xySeriesUnwindAnimation1 = new DevExpress.XtraCharts.XYSeriesUnwindAnimation();
           // DevExpress.XtraCharts.XYSeriesUnwrapAnimation xySeriesUnwrapAnimation1 = new DevExpress.XtraCharts.XYSeriesUnwrapAnimation();

            DevExpress.XtraCharts.PowerEasingFunction powerEasingFunction1 = new DevExpress.XtraCharts.PowerEasingFunction();
            DevExpress.XtraCharts.SineEasingFunction sineEasingFunction1 = new DevExpress.XtraCharts.SineEasingFunction();

            DevExpress.XtraCharts.ConstantLine constantLine1 = new DevExpress.XtraCharts.ConstantLine();

            // Add points to them, with their arguments different.

            for (int i = 0; i < arg_dt.Rows.Count; i++)
            {
                //series1.Points.Add(new SeriesPoint(dt.Rows[i]["HMS"].ToString(), dt.Rows[i]["QTY"])); //GetRandomNumber(10, 50)
                series2.Points.Add(new SeriesPoint(arg_dt.Rows[i]["LB"].ToString().Replace("_", "\n"),
                                arg_dt.Rows[i]["POD"] == null || arg_dt.Rows[i]["POD"].ToString() == "" ? 0 : arg_dt.Rows[i]["POD"]));
                if ((arg_dt.Rows[i]["POD"] == null || arg_dt.Rows[i]["POD"].ToString() == "" ? 0 : Convert.ToDouble(arg_dt.Rows[i]["POD"])) > Convert.ToDouble(arg_dt.Rows[0]["TARGET"]))
                    series2.Points[i].Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(176)))), ((int)(((byte)(240)))));
                else
                    series2.Points[i].Color = Color.Red;
            }

            (series2.Label as SideBySideBarSeriesLabel).Position = DevExpress.XtraCharts.BarSeriesLabelPosition.Top;

            // series2 = splineSeriesView1;
            // Add both series to the chart.
            //chartControl1.Series.AddRange(new Series[] { series1, series2 });
            

            arg_chart.SeriesSerializable = new DevExpress.XtraCharts.Series[] { series2 };
            ((XYDiagram)arg_chart.Diagram).AxisY.Title.Text = "POD";
            ((XYDiagram)arg_chart.Diagram).AxisY.Title.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(176)))), ((int)(((byte)(240)))));
            ((XYDiagram)arg_chart.Diagram).AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.Default;
            ((XYDiagram)arg_chart.Diagram).AxisX.Title.Text = "Date";
            ((XYDiagram)arg_chart.Diagram).AxisX.Title.Visibility = DevExpress.Utils.DefaultBoolean.Default;
            ((XYDiagram)arg_chart.Diagram).AxisX.Title.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(176)))), ((int)(((byte)(240)))));

            ((XYDiagram)arg_chart.Diagram).AxisX.Tickmarks.MinorVisible = true;
  

            sideBySideBarSeriesView1.ColorEach = false;
            series2.View = sideBySideBarSeriesView1;

            //title
            DevExpress.XtraCharts.ChartTitle chartTitle2 = new DevExpress.XtraCharts.ChartTitle();
            chartTitle2.Alignment = System.Drawing.StringAlignment.Near;
            chartTitle2.Font = new System.Drawing.Font("Tahoma", 24F, System.Drawing.FontStyle.Bold);
            chartTitle2.Text = arg_name;
            chartTitle2.TextColor = System.Drawing.Color.Blue;
            arg_chart.Titles.AddRange(new DevExpress.XtraCharts.ChartTitle[] { chartTitle2 });

   
            series2.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            xySeriesUnwindAnimation1.EasingFunction = sineEasingFunction1; //powerEasingFunction1;
            //splineSeriesView1.SeriesAnimation = xySeriesUnwindAnimation1;//xySeriesBlowUpAnimation1;//xySeriesUnwindAnimation1; // xySeriesUnwrapAnimation1;

            arg_chart.Legend.Direction = LegendDirection.LeftToRight;

            //Constant line
            //constantLine1.ShowInLegend = false;
            constantLine1.AxisValueSerializable = arg_dt.Rows[0]["TARGET"].ToString();
            constantLine1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(176)))), ((int)(((byte)(80)))));
            constantLine1.Name = "Target";
            constantLine1.ShowBehind = false;
            constantLine1.Title.Visible = false;
            //constantLine1.Title.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            //constantLine1.Title.Text = "Target";
            constantLine1.LineStyle.Thickness = 2;
            constantLine1.Title.Alignment = DevExpress.XtraCharts.ConstantLineTitleAlignment.Far;
            ((XYDiagram)arg_chart.Diagram).AxisY.ConstantLines.Clear();
            ((XYDiagram)arg_chart.Diagram).AxisY.ConstantLines.AddRange(new DevExpress.XtraCharts.ConstantLine[] { constantLine1 });


            

            //((XYDiagram)arg_chart.Diagram).AxisX.NumericScaleOptions.AutoGrid = false;
            //((XYDiagram)arg_chart.Diagram).AxisX.VisualRange.Auto = false;
            //((XYDiagram)arg_chart.Diagram).AxisX.VisualRange.AutoSideMargins = false;
            //((XYDiagram)arg_chart.Diagram).AxisX.Label.Angle = 90;
            //((XYDiagram)arg_chart.Diagram).AxisX.Label.ResolveOverlappingOptions.AllowHide = false;
            //((XYDiagram)arg_chart.Diagram).AxisX.Label.ResolveOverlappingOptions.AllowStagger = true;
            ((XYDiagram)arg_chart.Diagram).AxisX.Tickmarks.MinorVisible = false;
            ((XYDiagram)arg_chart.Diagram).AxisX.GridLines.Visible = false;
            
            ((XYDiagram)arg_chart.Diagram).AxisX.Label.Font = new System.Drawing.Font("Tahoma", 10, System.Drawing.FontStyle.Bold);
            //((XYDiagram)arg_chart.Diagram).AxisY.NumericScaleOptions.ScaleMode = DevExpress.XtraCharts.ScaleMode.Continuous;
            //((XYDiagram)_chartControl1.Diagram).AxisY.NumericScaleOptions.ScaleMode = DevExpress.XtraCharts.ScaleMode.Automatic;
            //((XYDiagram)arg_chart.Diagram).AxisX.
            ((XYDiagram)arg_chart.Diagram).AxisY.Label.Font = new System.Drawing.Font("Tahoma", 10, System.Drawing.FontStyle.Bold);

            ((XYDiagram)arg_chart.Diagram).AxisX.Title.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ((XYDiagram)arg_chart.Diagram).AxisY.Title.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));



            pn_body.Controls.Add(arg_chart);
        }

        private void load_data_grid(DataTable arg_dt)
        {
            try
            {
                if (arg_dt != null && arg_dt.Rows.Count > 0)
                {
                    axGrid.MaxCols = arg_dt.Rows.Count + 2;
                    axGrid.SetText(1, 1, arg_dt.Rows[0]["MON"].ToString());
                    double dColWidth = Convert.ToDouble(arg_dt.Rows[0]["col_width"]);
                    int icol;

                    axGrid.Col = -1;
                    axGrid.Row = 1;
                    axGrid.BackColor = Color.FromArgb(71, 143, 143);
                    axGrid.ForeColor = Color.White;
                    axGrid.Row = 2;
                    axGrid.BackColor = Color.FromArgb(71, 143, 143);
                    axGrid.ForeColor = Color.White;

                    for (int i = 0; i < arg_dt.Rows.Count; i++)
                    {
                        icol = i+3;
                        //axGrid.SetText(icol, 1, arg_dt.Rows[i]["DD"].ToString());
                        //axGrid.SetText(icol, 2, arg_dt.Rows[i]["DY"].ToString());
                        //axGrid.SetText(icol, 3, arg_dt.Rows[i]["ACT_QTY"].ToString());
                        //axGrid.SetText(icol, 4, arg_dt.Rows[i]["TOT_MAN"].ToString());
                        //axGrid.SetText(icol, 5, arg_dt.Rows[i]["TOT_DIRECT"].ToString());
                        //axGrid.SetText(icol, 6, arg_dt.Rows[i]["TOT_INDIRECT"].ToString());
                        //axGrid.SetText(icol, 7, arg_dt.Rows[i]["TOT_RELIEF"].ToString());
                        //axGrid.SetText(icol, 8, arg_dt.Rows[i]["POD"].ToString());

                        axGrid.SetText(icol, 1, arg_dt.Rows[i]["DD"].ToString());
                        axGrid.SetText(icol, 2, arg_dt.Rows[i]["DY"].ToString());
                        axGrid.SetText(icol, 3, arg_dt.Rows[i]["ACT_QTY"].ToString());
                        axGrid.SetText(icol, 4, arg_dt.Rows[i]["TOT_MAN"].ToString());
                        axGrid.SetText(icol, 5, arg_dt.Rows[i]["TOT_ATTN"].ToString());
                        
                        axGrid.SetText(icol, 6, arg_dt.Rows[i]["TOT_DIRECT"].ToString());
                        axGrid.SetText(icol, 7, arg_dt.Rows[i]["TOT_INDIRECT"].ToString());
                        axGrid.SetText(icol, 8, arg_dt.Rows[i]["TOT_RELIEF"].ToString());
                        axGrid.SetText(icol, 9, arg_dt.Rows[i]["TOT_ABSENT"].ToString());
                        axGrid.SetText(icol, 10, arg_dt.Rows[i]["POH"].ToString());
                        axGrid.SetText(icol, 11, arg_dt.Rows[i]["POD"].ToString());
                        

                        axGrid.set_ColWidth(icol, dColWidth);

                        if (arg_dt.Rows[i]["TODAY"].ToString() == "1")
                        {
                            axGrid.Col = icol;
                            axGrid.Row = 1;
                            axGrid.BackColor = Color.Salmon;
                            axGrid.Row =2;
                            axGrid.BackColor = Color.Salmon;
                        }

                        if (arg_dt.Rows[i]["DD"].ToString() == arg_dt.Rows[i]["DY"].ToString())
                        {
                            axGrid.AddCellSpan(icol, 1, 1, 2);
                        }
                    }


                    axGrid.SetOddEvenRowColor(0xffffff, 0, 0xf7f6e8, 0);
                    axGrid.SetCellBorder(1,3,axGrid.MaxCols, axGrid.MaxRows
                                , FPUSpreadADO.CellBorderIndexConstants.CellBorderIndexLeft, 0
                                , FPUSpreadADO.CellBorderStyleConstants.CellBorderStyleSolid);
                    axGrid.SetCellBorder(1, 2, axGrid.MaxCols, axGrid.MaxRows
                                , FPUSpreadADO.CellBorderIndexConstants.CellBorderIndexBottom, 0
                                , FPUSpreadADO.CellBorderStyleConstants.CellBorderStyleSolid);
                    axGrid.SetCellBorder(1, 3, 2, axGrid.MaxRows
                                , FPUSpreadADO.CellBorderIndexConstants.CellBorderIndexLeft, 0
                                , FPUSpreadADO.CellBorderStyleConstants.CellBorderStyleBlank);
                    axGrid.Col = -1;
                    axGrid.Row = axGrid.MaxRows-1;
                    axGrid.BackColor = Color.Orange;
                    axGrid.ForeColor = Color.White;

                    axGrid.Col = axGrid.MaxCols;
                    

                    //double  iYellow, iGreen, iQty;
                    //double.TryParse(arg_dt.Rows[0]["YELLOW_VALUE"].ToString(), out iYellow);
                    //double.TryParse(arg_dt.Rows[0]["GREEN_VALUE"].ToString(), out iGreen);
                    for (int i = 1; i <= axGrid.MaxRows; i++)
                    {

                        axGrid.Row = i;
                        axGrid.BackColor = Color.PeachPuff;
                        axGrid.ForeColor = Color.Black;
                        //
                        //double.TryParse(axGrid.Text.Replace("%",""), out iQty);
                        //if (iQty < 95)
                        //{
                        //    axGrid.BackColor = Color.Red;
                        //    axGrid.ForeColor = Color.White;
                        //}
                        //else if (iQty >= 95 && iQty <98)
                        //{
                        //    axGrid.BackColor = Color.Yellow;
                        //    axGrid.ForeColor = Color.Black;
                        //}
                        //else if (iQty > 98)
                        //{
                        //    axGrid.BackColor = Color.Green;
                        //    axGrid.ForeColor = Color.White;
                        //}
                    }

                    
                    
                }


            }
            catch (Exception)
            {
            }
           
        }

        private void load_data()
        {
            System.Data.DataSet ds = LOAD_DATA_v2(_wh_cd, _mline_cd, UC_MONTH.GetValue());
            axGrid.Hide();
            load_data_grid(ds.Tables[0]);
            AnimateWindow(axGrid.Handle, 300, AW_SLIDE | 0X4);
            axGrid.Show();
            //pn_body.Visible = true;

            switch (Lang)
            {
                case "Vn":
                      CreateChartLine(Chart1, ds.Tables[1], "Cắt");
            CreateChartLine(Chart2, ds.Tables[3], "May 1");
            CreateChartLine(Chart3, ds.Tables[5], "May 2");
            CreateChartLine(Chart4, ds.Tables[2], "Chuẩn bị");           
            CreateChartLine(Chart5, ds.Tables[4], "Lắp ráp");
               break;
                case "En":
                      CreateChartLine(Chart1, ds.Tables[1], "Cutting");
            CreateChartLine(Chart2, ds.Tables[3], "Stitching 1");
            CreateChartLine(Chart3, ds.Tables[5], "Stitching 2");
            CreateChartLine(Chart4, ds.Tables[2], "Stockfit");           
            CreateChartLine(Chart5, ds.Tables[4], "Assembly");
                    break;
            }


         
            BindingPOD(ds.Tables[6]);
            
        }
        #endregion Func

        #region DB
        private DataTable LOAD_DATA()
        {           
            try
            {
                COM.OraDB MyOraDB = new COM.OraDB();
                System.Data.DataSet ds_ret;

                string process_name = "SEPHIROTH.PKG_DMC.SEL_PROD_SET_STATUS";
                //ARGMODE
                MyOraDB.ReDim_Parameter(1);
                MyOraDB.Process_Name = process_name;
                MyOraDB.Parameter_Name[0] = "OUT_CURSOR";
                MyOraDB.Parameter_Type[0] = (char)OracleType.Cursor;
                MyOraDB.Parameter_Values[0] = "";
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


        public System.Data.DataSet LOAD_DATA_v2(string sLine_cd, string sMline_cd, string smonth)
        {
            COM.OraDB MyOraDB = new COM.OraDB();
            System.Data.DataSet ds_ret;

            try
            {
                string process_name = "MES.PKG_SMT_PROD_SHOW.SEL_PRODUCTIVITY_MONTH_V2";

                MyOraDB.ReDim_Parameter(10);
                MyOraDB.Process_Name = process_name;

                MyOraDB.Parameter_Name[0] = "ARG_LINE_CD";
                MyOraDB.Parameter_Name[1] = "ARG_MLINE_CD";
                MyOraDB.Parameter_Name[2] = "ARG_MONTH";
                MyOraDB.Parameter_Name[3] = "OUT_CURSOR";
                MyOraDB.Parameter_Name[4] = "OUT_CURSOR1";
                MyOraDB.Parameter_Name[5] = "OUT_CURSOR2";
                MyOraDB.Parameter_Name[6] = "OUT_CURSOR3";
                MyOraDB.Parameter_Name[7] = "OUT_CURSOR4";              
                MyOraDB.Parameter_Name[8] = "OUT_CURSOR5";
                MyOraDB.Parameter_Name[9] = "OUT_CURSOR6";

                MyOraDB.Parameter_Type[0] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[1] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[2] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[3] = (int)OracleType.Cursor;
                MyOraDB.Parameter_Type[4] = (int)OracleType.Cursor;
                MyOraDB.Parameter_Type[5] = (int)OracleType.Cursor;
                MyOraDB.Parameter_Type[6] = (int)OracleType.Cursor;
                MyOraDB.Parameter_Type[7] = (int)OracleType.Cursor;              
                MyOraDB.Parameter_Type[8] = (int)OracleType.Cursor;
                MyOraDB.Parameter_Type[9] = (int)OracleType.Cursor;

                MyOraDB.Parameter_Values[0] = sLine_cd;
                MyOraDB.Parameter_Values[1] = sMline_cd;
                MyOraDB.Parameter_Values[2] = smonth;
                MyOraDB.Parameter_Values[3] = "";
                MyOraDB.Parameter_Values[4] = "";
                MyOraDB.Parameter_Values[5] = "";
                MyOraDB.Parameter_Values[6] = "";
                MyOraDB.Parameter_Values[7] = "";              
                MyOraDB.Parameter_Values[8] = "";
                MyOraDB.Parameter_Values[9] = "";

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

        #region event
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                _icount++;
                lblDate.Text = string.Format(DateTime.Now.ToString("yyyy-MM-dd\nHH:mm:ss"));
                if (_icount >= 20)
                {
                    showAnimation(axGrid);
                    _icount = 0;
                }
               
            }
            catch (Exception)
            {} 
        }

        

      


        private void FORM_IPEX3_LOGISTIC_VisibleChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.Visible)
                {
                    //_icount = 0;
                    //_load = true;
                    uc.YMD_Change(3);
                    ////panel2.BringToFront();
                    //load_data();
                    lblDate.Text = string.Format(DateTime.Now.ToString("yyyy-MM-dd\nHH:mm:ss"));
                    _icount = 19;
                    timer1.Start();
                }
                else
                {
                    timer1.Stop();
                }
            }
            catch (Exception)
            {}
                
        }

       

        private void FORM_IPEX3_LOGISTIC_Load(object sender, EventArgs e)
        {
            try
            {
                GoFullscreen(true);
                //pn_body.Visible = true;
                //ClassLib.ComCtl.Form_Maximized(this, indexScreen); //2 man hinh tro len
                lblDate.Text = string.Format(DateTime.Now.ToString("yyyy-MM-dd\nHH:mm:ss"));

                _dtnInit = ComVar.Func.getInitForm(this.GetType().Assembly.GetName().Name, this.GetType().Name);
                pnYMD.Controls.Add(uc);
                uc.OnDWMYClick += DWMYClick;
                timer1.Start();
               
               // CreateChartLine(Chart1, ds.Tables[1], "Cutting", 0,0);
                
                //createChart1(chart_1, "Cutting");
                //createChart2(chart_2, "Stockfit");
                //createChart3(chart_3, "Stitching");
                //createChart4(chart_4, "Assembly");




              //  load_data();
            }
            catch (Exception)
            {}
            
        }
        void DWMYClick(string ButtonCap, string ButtonCD)
        {
            //MessageBox.Show(ButtonCap + "    " + ButtonCD);
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
        private void lblDate_Click(object sender, EventArgs e)
        {
            try
            {
                Application.Exit();
            }
            catch (Exception)
            {}

        }

        #endregion event

        private void chart_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
                    


            string Caption = "Productivity Status by Week";
            switch (Lang)
            {
                case "Vn":
                    Caption = "Trạng thái năng suất theo Tuần";
                    break;
                default:
                    Caption = "Productivity Status by Week";
                    break;
            }

            Form fc = Application.OpenForms["FORM_PRODUCTIONTIVITY_WEEKLY"];
            if (fc != null)
            {
                //fc.Close();
                fc.Show();
                ////f.TopMost = true;
            }
            else
            {
                FORM_PRODUCTIONTIVITY_WEEKLY f = new FORM_PRODUCTIONTIVITY_WEEKLY(Caption, 1, _wh_cd, _mline_cd, Lang);
                f.Show();
                //f.TopMost = true;
            }
            this.timer1.Stop();
            this.Hide();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
          
            


            string Caption = "Productivity Status by Year";
            switch (Lang)
            {
                case "Vn":
                    Caption = "Trạng thái năng suất theo Năm";
                    break;
                default:
                    Caption = "Productivity Status by Year";
                    break;
            }

            Form fc = Application.OpenForms["FORM_PRODUCTIONTIVITY_YEARLY"];
            if (fc != null)
            {
                //fc.Close();
                fc.Show();
                ////f.TopMost = true;
            }
            else
            {
                FORM_PRODUCTIONTIVITY_YEARLY f = new FORM_PRODUCTIONTIVITY_YEARLY(Caption, 1, _wh_cd, _mline_cd, Lang);
                f.Show();
                //f.TopMost = true;
            }
            this.timer1.Stop();
            this.Hide();
        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            
            //arrForm[0].Show();
            


            string Caption = "Productivity Status by Day";
            switch (Lang)
            {
                case "Vn":
                    Caption = "Trạng thái năng suất theo Ngày";
                    break;
                default:
                    Caption = "Productivity Status by Day";
                    break;
            }
            Form fc = Application.OpenForms["FORM_PRODUCTIONTIVITY_DAILY"];
            if (fc != null)
            {
                //fc.Close();
                fc.Show();
                ////f.TopMost = true;
            }
            else
            {
                FORM_PRODUCTIONTIVITY_DAILY f = new FORM_PRODUCTIONTIVITY_DAILY(Caption, 1, _wh_cd, _mline_cd, Lang);
                f.Show();
                //f.TopMost = true;
            }
            this.timer1.Stop();
            this.Hide();
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {

        }

        private void UC_MONTH_ValueChangeEvent(object sender, EventArgs e)
        {
            load_data();
        }



    }
}
