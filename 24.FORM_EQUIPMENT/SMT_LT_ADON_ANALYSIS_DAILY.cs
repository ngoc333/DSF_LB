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
//using Microsoft.VisualBasic.PowerPacks;
//using C1.Win.C1FlexGrid;

namespace FORM
{
    public partial class SMT_LT_ADON_ANALYSIS_DAILY : Form
    {



        public SMT_LT_ADON_ANALYSIS_DAILY()
        {
            InitializeComponent();
        }

        int indexScreen;
        DataTable _dtXML = null;
        #region Variable
        bool _load = true;
        int _icount = 0;
        string _wh_cd,  Lang;


        string _mline_cd = ComVar.Var._strValue1;
        Dictionary<string, string> _dtnInit = new Dictionary<string, string>();


        #endregion
        Form[] arrForm = new Form[3];
        int _iTime;
        public SMT_LT_ADON_ANALYSIS_DAILY(string Title, int _indexScreen, string wh_cd, string mline_cd, string _Lang)
        {

            InitializeComponent();
            indexScreen = _indexScreen;
            _wh_cd = wh_cd;
            _mline_cd = mline_cd;
            Lang = _Lang;
            timer1.Stop();

            lblTitle.Text = Title;
        }
        //public FORM_PRODUCTIONTIVITY_DAILY(string aaa)
        //{
        //    InitializeComponent();
        //}


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

    

        private void CreateChartLine(ChartControl arg_chart, DataTable arg_dt, string arg_name)
        {
            if (arg_dt == null || arg_dt.Rows.Count == 0) return;
            arg_chart.Series.Clear();
            arg_chart.Titles.Clear();

            //----------create--------------------
            Series series2 = new Series("POH", ViewType.Spline);

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
                if (arg_dt.Rows[i]["ACTUAL"] == null || arg_dt.Rows[i]["ACTUAL"].ToString() == "")
                    series2.Points.Add(new SeriesPoint(arg_dt.Rows[i]["NM"].ToString().Replace(" ", "\n")));
                else
                    series2.Points.Add(new SeriesPoint(arg_dt.Rows[i]["NM"].ToString().Replace(" ", "\n"), arg_dt.Rows[i]["ACTUAL"]));
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
            constantLine1.AxisValueSerializable = arg_dt.Rows[0]["TAR"].ToString();
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
            ((XYDiagram)arg_chart.Diagram).AxisY.Title.Text = "POH";
            ((XYDiagram)arg_chart.Diagram).AxisY.Title.TextColor = System.Drawing.Color.Orange;
            ((XYDiagram)arg_chart.Diagram).AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.Default;
            ((XYDiagram)arg_chart.Diagram).AxisX.Title.Text = "Time";
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

        private DataTable select_Data(string arg_expression, DataTable arg_dt)
        {
            string[] str_col = { "NM", "TAR", "ACTUAL" };
            // return arg_dt.Select(arg_expression, arg_sortOrder).CopyToDataTable().DefaultView.ToTable(true, arg_column);
            return arg_dt.Select("OP_CD = '" + arg_expression + "' and NM <> 'total'", "RN").CopyToDataTable().DefaultView.ToTable(true, str_col);

        }

        private void load_data_grid(DataTable arg_dt)
        {
            try
            {
                //string[] arrSel = { "SEQ", "WO_TITLE", "WO_CLASS", "WORK_TYPE" };
                if (arg_dt != null && arg_dt.Rows.Count > 0)
                {
                    timer2.Stop();
                    axGrid.MaxRows = 1;
                    axGrid.MaxRows = 100;
                    for (int i = 0; i < arg_dt.Rows.Count; i++)
                    {
                        
                        axGrid.SetText( 1, i + 2, arg_dt.Rows[i]["SEQ"].ToString());
                        axGrid.SetText( 2, i + 2, arg_dt.Rows[i]["WO_TITLE"].ToString());
                        axGrid.SetText( 3, i + 2, arg_dt.Rows[i]["WO_CLASS"].ToString());
                        axGrid.SetText( 4, i + 2, arg_dt.Rows[i]["WORK_TYPE"].ToString());
                        axGrid.SetText( 5, i + 2, arg_dt.Rows[i]["RP_USER_ID"].ToString());
                        axGrid.SetText( 6, i + 2, arg_dt.Rows[i]["WO_DATE"].ToString());
                        axGrid.SetText( 7, i + 2, arg_dt.Rows[i]["PROBLEM_DATE"].ToString());
                        axGrid.SetText( 8, i + 2, arg_dt.Rows[i]["DEFE_DATE"].ToString());
                        axGrid.SetText( 9, i + 2, arg_dt.Rows[i]["SOLU_DATE"].ToString());
                        axGrid.SetText(10, i + 2, arg_dt.Rows[i]["DEFE_CD"].ToString());
                        axGrid.SetText(12, i + 2, arg_dt.Rows[i]["WO_STATUS"].ToString());

                        axGrid.Col = 5;
                        axGrid.Row = i + 2;
                        axGrid.BackColor = Color.DodgerBlue;
                        axGrid.ForeColor = Color.White;
                        axGrid.Col = 12;
                        if (arg_dt.Rows[i]["STATUS"].ToString().ToUpper() == "RQ")
                        {
                            axGrid.BackColor = Color.Red;
                            axGrid.ForeColor = Color.White;
                        }
                        else if (arg_dt.Rows[i]["STATUS"].ToString().ToUpper() == "RW")
                        {
                            axGrid.BackColor = Color.Yellow;
                            axGrid.ForeColor = Color.Black;
                        }
                        else if (arg_dt.Rows[i]["STATUS"].ToString().ToUpper() == "RC")
                        {
                            axGrid.BackColor = Color.Green;
                            axGrid.ForeColor = Color.White;
                        }                      
                        
                    }

                    axGrid.MaxRows = arg_dt.Rows.Count +1 ;
                    axGrid.SetCellBorder(12, 2, axGrid.MaxCols, axGrid.MaxRows, FPUSpreadADO.CellBorderIndexConstants.CellBorderIndexBottom, 0, FPUSpreadADO.CellBorderStyleConstants.CellBorderStyleSolid);
                    axGrid.SetCellBorder(5, 2, 5, axGrid.MaxRows, FPUSpreadADO.CellBorderIndexConstants.CellBorderIndexBottom, 0, FPUSpreadADO.CellBorderStyleConstants.CellBorderStyleBlank);
                    timer2.Start();
                    //axGrid.MaxCols = iCol - 1;
                    //axGrid.MaxRows = iRow;
                    //axGrid.RowsFrozen = iRow;
                    //axGrid.SetOddEvenRowColor(0xffffff, 0, 0xf7f6e8, 0);
                    //axGrid.SetCellBorder(1, 3, iCol - 1, iRow
                    //            , FPUSpreadADO.CellBorderIndexConstants.CellBorderIndexLeft, 0
                    //            , FPUSpreadADO.CellBorderStyleConstants.CellBorderStyleSolid);
                    //axGrid.SetCellBorder(1, 2, iCol - 1, iRow
                    //            , FPUSpreadADO.CellBorderIndexConstants.CellBorderIndexBottom, 0
                    //            , FPUSpreadADO.CellBorderStyleConstants.CellBorderStyleSolid);

                    //axGrid.Col = 1;
                    //axGrid.ColMerge = FPUSpreadADO.MergeConstants.MergeAlways;
                    //axGrid.Row = 1;
                    //axGrid.RowMerge = FPUSpreadADO.MergeConstants.MergeAlways;
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                //axGrid.Visible = true;
            }

        }

      

        private void load_data()
        {
            DataTable dt = LOAD_DATA();

            if (dt == null || dt.Rows.Count == 0) return;

            //  DataTable dt = select_Data("UPC",  ds.Tables[0]);
          //  axGrid.Visible = true;
            load_data_grid(dt);
            pn_body.Visible = true;
            //switch (Lang)
            //{
            //    case "Vn":
            //        CreateChartLine(Chart1, select_Data("UPC", ds.Tables[0]), "Cắt");
            //        CreateChartLine(Chart2, select_Data("UPS1", ds.Tables[0]), "May 1");
            //        CreateChartLine(Chart3, select_Data("UPS2", ds.Tables[0]), "May 2");
            //        CreateChartLine(Chart4, select_Data("FSS", ds.Tables[0]), "Chuẩn bị");
            //        CreateChartLine(Chart5, select_Data("FGA", ds.Tables[0]), "Lắp rắp");
            //        break;
            //    case "En":
            //        CreateChartLine(Chart1, select_Data("UPC", ds.Tables[0]), "Cutting");
            //        CreateChartLine(Chart2, select_Data("UPS1", ds.Tables[0]), "Stitching 1");
            //        CreateChartLine(Chart3, select_Data("UPS2", ds.Tables[0]), "Stitching 2");
            //        CreateChartLine(Chart4, select_Data("FSS", ds.Tables[0]), "Stockfit");
            //        CreateChartLine(Chart5, select_Data("FGA", ds.Tables[0]), "Assembly");
            //        break;
            //}


           // BindingPOD(ds.Tables[6]);

        }
        #endregion Func

        #region DB
        private DataTable LOAD_DATA()
        {
            try
            {
                COM.OraDB MyOraDB = new COM.OraDB();
                System.Data.DataSet ds_ret;

                string process_name = "MES.PKG_SMT_LT.SEL_CMMS_DAILY";
                //ARGMODE
                MyOraDB.ReDim_Parameter(2);
                MyOraDB.Process_Name = process_name;

                MyOraDB.Parameter_Name[0]  = "ARG_MLINE_CD";
                MyOraDB.Parameter_Name[1] = "OUT_CURSOR";

                MyOraDB.Parameter_Type[0] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[1] = (char)OracleType.Cursor;

                MyOraDB.Parameter_Values[0] = "";
                MyOraDB.Parameter_Values[1] = "";

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


        public System.Data.DataSet LOAD_DATA_v2()
        {
            COM.OraDB MyOraDB = new COM.OraDB();
            System.Data.DataSet ds_ret;

            try
            {
                string process_name = "MES.PKG_SMT_PROD_SHOW.SEL_PRODUCTIVITY_DAILY";

                MyOraDB.ReDim_Parameter(9);
                MyOraDB.Process_Name = process_name;


                MyOraDB.Parameter_Name[0] = "OUT_CURSOR";
                MyOraDB.Parameter_Name[1] = "OUT_CURSOR1";
                MyOraDB.Parameter_Name[2] = "OUT_CURSOR2";
                MyOraDB.Parameter_Name[3] = "OUT_CURSOR3";
                MyOraDB.Parameter_Name[4] = "OUT_CURSOR4";
                MyOraDB.Parameter_Name[5] = "ARG_WH_CD";
                MyOraDB.Parameter_Name[6] = "ARG_MLINE_CD";
                MyOraDB.Parameter_Name[7] = "OUT_CURSOR5";
                MyOraDB.Parameter_Name[8] = "OUT_CURSOR6";

                MyOraDB.Parameter_Type[0] = (int)OracleType.Cursor;
                MyOraDB.Parameter_Type[1] = (int)OracleType.Cursor;
                MyOraDB.Parameter_Type[2] = (int)OracleType.Cursor;
                MyOraDB.Parameter_Type[3] = (int)OracleType.Cursor;
                MyOraDB.Parameter_Type[4] = (int)OracleType.Cursor;
                MyOraDB.Parameter_Type[5] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[6] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[7] = (int)OracleType.Cursor;
                MyOraDB.Parameter_Type[8] = (int)OracleType.Cursor;

                MyOraDB.Parameter_Values[0] = "";
                MyOraDB.Parameter_Values[1] = "";
                MyOraDB.Parameter_Values[2] = "";
                MyOraDB.Parameter_Values[3] = "";
                MyOraDB.Parameter_Values[4] = "";
                MyOraDB.Parameter_Values[5] = _wh_cd;
                MyOraDB.Parameter_Values[6] = _mline_cd;
                MyOraDB.Parameter_Values[7] = "";
                MyOraDB.Parameter_Values[8] = "";

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
                _iTime = DateTime.Now.Hour;
                if (_icount == 20)
                {
                    load_data();
                    _icount = 0;
                }

            }
            catch (Exception)
            { }
        }

        private void SMT_LT_ADON_ANALYSIS_DAILY_VisibleChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.Visible)
                {
                  //  _mline_cd = ComVar.Var._strValue1;
                    //_icount = 0;
                    //_load = true;
                    ////panel2.BringToFront();
                    //load_data();

                    _icount = 19;
                    timer1.Start();
                }
                else
                {
                    timer1.Stop();
                }
            }
            catch (Exception)
            { }

        }



        private void SMT_LT_ADON_ANALYSIS_DAILY_Load(object sender, EventArgs e)
        {
            try
            {
                GoFullscreen(true);
              //  _dtXML = ComVar.Func.ReadXML(Application.StartupPath + "\\InitForm.XML", this.GetType().Name);
                _dtnInit = ComVar.Func.getInitForm(this.GetType().Assembly.GetName().Name, this.GetType().Name);
                pn_body.Visible = false;
                //ClassLib.ComCtl.Form_Maximized(this, indexScreen); //2 man hinh tro len
                lblDate.Text = string.Format(DateTime.Now.ToString("yyyy-MM-dd\nHH:mm:ss"));

                switch (Lang)
                {
                    case "Vn":
                        simpleButton4.Text = "Ngày";
                        simpleButton3.Text = "Tháng";
                        simpleButton2.Text = "Tuần";
                        simpleButton1.Text = "Năm";
                        break;
                    case "En":
                        simpleButton4.Text = "Day";
                        simpleButton3.Text = "Month";
                        simpleButton2.Text = "Week";
                        simpleButton1.Text = "Year";
                        break;
                }


                // CreateChartLine(Chart1, ds.Tables[1], "Cutting", 0,0);

                //createChart1(chart_1, "Cutting");
                //createChart2(chart_2, "Stockfit");
                //createChart3(chart_3, "Stitching");
                //createChart4(chart_4, "Assembly");




                //  load_data();
            }
            catch (Exception)
            { }

        }

        private void lblDate_Click(object sender, EventArgs e)
        {
          

        }

        #endregion event

        private void chart_Click(object sender, EventArgs e)
        {

        }

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    //this.Hide();
        //    ComVar.Var.callForm = _dtXML.Rows[0]["frmHome"].ToString();
        //}

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Hide();
            //arrForm[0].Show();
            Form fc = Application.OpenForms["FORM_PRODUCTIONTIVITY_WEEKLY"];
            if (fc != null)
                fc.Close();


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


            //FORM_PRODUCTIONTIVITY_WEEKLY f = new FORM_PRODUCTIONTIVITY_WEEKLY(Caption, 1, _wh_cd, _mline_cd, Lang);
            //f.Show();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form fc = Application.OpenForms["FORM_PRODUCTIONTIVITY_MONTHLY"];
            if (fc != null)
                fc.Close();

            string Caption = "Productivity Status by Month";
            switch (Lang)
            {
                case "Vn":
                    Caption = "Trạng thái năng suất theo Tháng";
                    break;
                default:
                    Caption = "Productivity Status by Month";
                    break;
            }


           // FORM_PRODUCTIONTIVITY_MONTHLY f = new FORM_PRODUCTIONTIVITY_MONTHLY(Caption, 1, _wh_cd, _mline_cd, Lang);
            //f.Show();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form fc = Application.OpenForms["FORM_PRODUCTIONTIVITY_YEARLY"];
            if (fc != null)
                fc.Close();


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



            //FORM_PRODUCTIONTIVITY_YEARLY f = new FORM_PRODUCTIONTIVITY_YEARLY(Caption, 1, _wh_cd, _mline_cd, Lang);
            //f.Show();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            for (int i = 2; i <= axGrid.MaxRows; i++)
            {
                axGrid.Col = axGrid.MaxCols;
                axGrid.Row = i;
                if (axGrid.BackColor == Color.Red)
                {
                    axGrid.BackColor = Color.White;
                    axGrid.ForeColor = Color.Black;
                }
                else if (axGrid.BackColor == Color.White)
                {
                    axGrid.BackColor = Color.Red;
                    axGrid.ForeColor = Color.White;
                }
            }
        }

        private void lblDate_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                Application.Exit();
            }
            catch (Exception)
            { }
        }

        private void cmdBACK_Click(object sender, EventArgs e)
        {
            //ComVar.Var.callForm = _dtXML.Rows[0]["frmHome"].ToString();
            ComVar.Var.callForm = _dtnInit["frmHome"];
        }



    }
}
