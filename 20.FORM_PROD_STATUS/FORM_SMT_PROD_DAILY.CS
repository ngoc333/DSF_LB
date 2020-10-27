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
using System.Runtime.InteropServices;

namespace FORM
{
    public partial class FORM_SMT_PROD_DAILY : Form
    {
        string sLineCD = "014";
        string sMLineCD = "001";
        string Lang;
        string sTimeTitle = "Time";
        string sCuttingTitle = "Cutting (Start 07:30)";
        string sStitching1Title = "Stitching 1 (Start 07:30)";
        string sStitching2Title = "Stitching 2 (Start 07:30)";
        string sStitching1TitleEn = "Stitching 1 (Start 07:30)";
        string sStitching2TitleEn = "Stitching 2 (Start 07:30)";
        string sStitching1TitleVn = "Stitching 1 (Start 07:30 ~ 08:30)";
        string sStitching2TitleVn = "Stitching 2 (Start 07:30)";
        string sStockfitTitle = "Stockfit (Start 07:30 )";
        string sAssemblyTitle = "Assembly (Start 08:00 )";
        string sTarget = "Target";
        string sActual = "Actual";
        string sProducQty = "Product Qty (Prs)";
        string sHour = "Hour";
        string sChartTarget = "Target Qty";
        string sChartActual = "Actual Qty";
        string sAssemblyProduct = "Assembly Production";
        string sRate = "Rate";
        init strinit = new init();
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FORM_SMT_PROD_DAILY));
            DevExpress.XtraCharts.XYDiagram xyDiagram1 = new DevExpress.XtraCharts.XYDiagram();
            DevExpress.XtraCharts.Series series1 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.PointSeriesLabel pointSeriesLabel1 = new DevExpress.XtraCharts.PointSeriesLabel();
            DevExpress.XtraCharts.SplineSeriesView splineSeriesView1 = new DevExpress.XtraCharts.SplineSeriesView();
            DevExpress.XtraCharts.XYSeriesSlideAnimation xySeriesSlideAnimation1 = new DevExpress.XtraCharts.XYSeriesSlideAnimation();
            DevExpress.XtraCharts.SineEasingFunction sineEasingFunction1 = new DevExpress.XtraCharts.SineEasingFunction();
            DevExpress.XtraCharts.ChartTitle chartTitle1 = new DevExpress.XtraCharts.ChartTitle();
            DevExpress.XtraCharts.XYDiagram xyDiagram2 = new DevExpress.XtraCharts.XYDiagram();
            DevExpress.XtraCharts.Series series2 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.PointSeriesLabel pointSeriesLabel2 = new DevExpress.XtraCharts.PointSeriesLabel();
            DevExpress.XtraCharts.SplineSeriesView splineSeriesView2 = new DevExpress.XtraCharts.SplineSeriesView();
            DevExpress.XtraCharts.XYSeriesSlideAnimation xySeriesSlideAnimation2 = new DevExpress.XtraCharts.XYSeriesSlideAnimation();
            DevExpress.XtraCharts.SineEasingFunction sineEasingFunction2 = new DevExpress.XtraCharts.SineEasingFunction();
            DevExpress.XtraCharts.ChartTitle chartTitle2 = new DevExpress.XtraCharts.ChartTitle();
            DevExpress.XtraGauges.Core.Model.ArcScaleRange arcScaleRange1 = new DevExpress.XtraGauges.Core.Model.ArcScaleRange();
            DevExpress.XtraGauges.Core.Model.ArcScaleRange arcScaleRange2 = new DevExpress.XtraGauges.Core.Model.ArcScaleRange();
            DevExpress.XtraGauges.Core.Model.ArcScaleRange arcScaleRange3 = new DevExpress.XtraGauges.Core.Model.ArcScaleRange();
            DevExpress.XtraCharts.XYDiagram xyDiagram3 = new DevExpress.XtraCharts.XYDiagram();
            DevExpress.XtraCharts.SecondaryAxisY secondaryAxisY1 = new DevExpress.XtraCharts.SecondaryAxisY();
            DevExpress.XtraCharts.Series series3 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.PointSeriesLabel pointSeriesLabel3 = new DevExpress.XtraCharts.PointSeriesLabel();
            DevExpress.XtraCharts.SplineSeriesView splineSeriesView3 = new DevExpress.XtraCharts.SplineSeriesView();
            DevExpress.XtraCharts.XYSeriesSlideAnimation xySeriesSlideAnimation3 = new DevExpress.XtraCharts.XYSeriesSlideAnimation();
            DevExpress.XtraCharts.SineEasingFunction sineEasingFunction3 = new DevExpress.XtraCharts.SineEasingFunction();
            DevExpress.XtraCharts.ChartTitle chartTitle3 = new DevExpress.XtraCharts.ChartTitle();
            DevExpress.XtraCharts.XYDiagram xyDiagram4 = new DevExpress.XtraCharts.XYDiagram();
            DevExpress.XtraCharts.SecondaryAxisY secondaryAxisY2 = new DevExpress.XtraCharts.SecondaryAxisY();
            DevExpress.XtraCharts.Series series4 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.PointSeriesLabel pointSeriesLabel4 = new DevExpress.XtraCharts.PointSeriesLabel();
            DevExpress.XtraCharts.SplineSeriesView splineSeriesView4 = new DevExpress.XtraCharts.SplineSeriesView();
            DevExpress.XtraCharts.XYSeriesSlideAnimation xySeriesSlideAnimation4 = new DevExpress.XtraCharts.XYSeriesSlideAnimation();
            DevExpress.XtraCharts.SineEasingFunction sineEasingFunction4 = new DevExpress.XtraCharts.SineEasingFunction();
            DevExpress.XtraCharts.ChartTitle chartTitle4 = new DevExpress.XtraCharts.ChartTitle();
            DevExpress.XtraCharts.XYDiagram xyDiagram5 = new DevExpress.XtraCharts.XYDiagram();
            DevExpress.XtraCharts.Series series5 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.PointSeriesLabel pointSeriesLabel5 = new DevExpress.XtraCharts.PointSeriesLabel();
            DevExpress.XtraCharts.SplineSeriesView splineSeriesView5 = new DevExpress.XtraCharts.SplineSeriesView();
            DevExpress.XtraCharts.XYSeriesSlideAnimation xySeriesSlideAnimation5 = new DevExpress.XtraCharts.XYSeriesSlideAnimation();
            DevExpress.XtraCharts.SineEasingFunction sineEasingFunction5 = new DevExpress.XtraCharts.SineEasingFunction();
            DevExpress.XtraCharts.ChartTitle chartTitle5 = new DevExpress.XtraCharts.ChartTitle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnYearly = new DevExpress.XtraEditors.SimpleButton();
            this.btnMonthly = new DevExpress.XtraEditors.SimpleButton();
            this.btnWeekly = new DevExpress.XtraEditors.SimpleButton();
            this.btnDaily = new DevExpress.XtraEditors.SimpleButton();
            this.button1 = new System.Windows.Forms.Button();
            this.lblDate = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.splitMain = new System.Windows.Forms.SplitContainer();
            this.axfpView = new AxFPUSpreadADO.AxfpSpread();
            this.chartControl5 = new DevExpress.XtraCharts.ChartControl();
            this.chartControl3 = new DevExpress.XtraCharts.ChartControl();
            this.gaugeControl1 = new DevExpress.XtraGauges.Win.GaugeControl();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblAssemblyProd = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblRate = new System.Windows.Forms.Label();
            this.lblProd = new System.Windows.Forms.Label();
            this.lblRPlan = new System.Windows.Forms.Label();
            this.circularRealTime = new DevExpress.XtraGauges.Win.Gauges.Circular.CircularGauge();
            this.arcScaleBackgroundLayerComponent1 = new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleBackgroundLayerComponent();
            this.arcScaleComponent1 = new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleComponent();
            this.arcScaleNeedleComponent1 = new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleNeedleComponent();
            this.chartControl4 = new DevExpress.XtraCharts.ChartControl();
            this.chartControl2 = new DevExpress.XtraCharts.ChartControl();
            this.chartControl1 = new DevExpress.XtraCharts.ChartControl();
            this.tmr_Date = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).BeginInit();
            this.splitMain.Panel1.SuspendLayout();
            this.splitMain.Panel2.SuspendLayout();
            this.splitMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axfpView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(pointSeriesLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(splineSeriesView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(pointSeriesLabel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(splineSeriesView2)).BeginInit();
            this.gaugeControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.circularRealTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.arcScaleBackgroundLayerComponent1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.arcScaleComponent1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.arcScaleNeedleComponent1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(secondaryAxisY1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(pointSeriesLabel3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(splineSeriesView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(secondaryAxisY2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(pointSeriesLabel4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(splineSeriesView4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(pointSeriesLabel5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(splineSeriesView5)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel1.Controls.Add(this.btnYearly);
            this.panel1.Controls.Add(this.btnMonthly);
            this.panel1.Controls.Add(this.btnWeekly);
            this.panel1.Controls.Add(this.btnDaily);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.lblDate);
            this.panel1.Controls.Add(this.lblTitle);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.MaximumSize = new System.Drawing.Size(1920, 109);
            this.panel1.MinimumSize = new System.Drawing.Size(1920, 109);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1920, 109);
            this.panel1.TabIndex = 1;
            // 
            // btnYearly
            // 
            this.btnYearly.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnYearly.Appearance.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnYearly.Appearance.Font = new System.Drawing.Font("Calibri", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnYearly.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btnYearly.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.btnYearly.Appearance.Options.UseBackColor = true;
            this.btnYearly.Appearance.Options.UseFont = true;
            this.btnYearly.Appearance.Options.UseForeColor = true;
            this.btnYearly.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Flat;
            this.btnYearly.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnYearly.ImageOptions.Image")));
            this.btnYearly.Location = new System.Drawing.Point(1462, 56);
            this.btnYearly.Name = "btnYearly";
            this.btnYearly.Size = new System.Drawing.Size(175, 48);
            this.btnYearly.TabIndex = 61;
            this.btnYearly.Text = "Yearly";
            this.btnYearly.Click += new System.EventHandler(this.btnYearly_Click);
            // 
            // btnMonthly
            // 
            this.btnMonthly.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnMonthly.Appearance.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnMonthly.Appearance.Font = new System.Drawing.Font("Calibri", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMonthly.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btnMonthly.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.btnMonthly.Appearance.Options.UseBackColor = true;
            this.btnMonthly.Appearance.Options.UseFont = true;
            this.btnMonthly.Appearance.Options.UseForeColor = true;
            this.btnMonthly.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Flat;
            this.btnMonthly.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnMonthly.ImageOptions.Image")));
            this.btnMonthly.Location = new System.Drawing.Point(1462, 3);
            this.btnMonthly.Name = "btnMonthly";
            this.btnMonthly.Size = new System.Drawing.Size(175, 48);
            this.btnMonthly.TabIndex = 60;
            this.btnMonthly.Text = "Monthly";
            this.btnMonthly.Click += new System.EventHandler(this.btnMonthly_Click);
            // 
            // btnWeekly
            // 
            this.btnWeekly.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnWeekly.Appearance.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnWeekly.Appearance.Font = new System.Drawing.Font("Calibri", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnWeekly.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btnWeekly.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.btnWeekly.Appearance.Options.UseBackColor = true;
            this.btnWeekly.Appearance.Options.UseFont = true;
            this.btnWeekly.Appearance.Options.UseForeColor = true;
            this.btnWeekly.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Flat;
            this.btnWeekly.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnWeekly.ImageOptions.Image")));
            this.btnWeekly.Location = new System.Drawing.Point(1282, 56);
            this.btnWeekly.Name = "btnWeekly";
            this.btnWeekly.Size = new System.Drawing.Size(175, 48);
            this.btnWeekly.TabIndex = 59;
            this.btnWeekly.Text = "Weekly";
            this.btnWeekly.Click += new System.EventHandler(this.btnWeekly_Click);
            // 
            // btnDaily
            // 
            this.btnDaily.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnDaily.Appearance.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnDaily.Appearance.Font = new System.Drawing.Font("Calibri", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDaily.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btnDaily.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.btnDaily.Appearance.Options.UseBackColor = true;
            this.btnDaily.Appearance.Options.UseFont = true;
            this.btnDaily.Appearance.Options.UseForeColor = true;
            this.btnDaily.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Flat;
            this.btnDaily.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnDaily.ImageOptions.Image")));
            this.btnDaily.Location = new System.Drawing.Point(1282, 4);
            this.btnDaily.Name = "btnDaily";
            this.btnDaily.Size = new System.Drawing.Size(175, 48);
            this.btnDaily.TabIndex = 58;
            this.btnDaily.Text = "Daily";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.DarkTurquoise;
            this.button1.BackgroundImage = global::FORM.Properties.Resources.Back_Icon;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.Yellow;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(1167, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(108, 101);
            this.button1.TabIndex = 52;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // lblDate
            // 
            this.lblDate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.lblDate.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblDate.Font = new System.Drawing.Font("Calibri", 32.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDate.ForeColor = System.Drawing.Color.White;
            this.lblDate.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.lblDate.Location = new System.Drawing.Point(1639, 0);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(281, 109);
            this.lblDate.TabIndex = 46;
            this.lblDate.Text = "lblDate";
            this.lblDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.lblTitle.Font = new System.Drawing.Font("Calibri", 50F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(3, 3);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(1914, 103);
            this.lblTitle.TabIndex = 47;
            this.lblTitle.Text = "Daily Production Status";
            // 
            // splitMain
            // 
            this.splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitMain.Location = new System.Drawing.Point(0, 109);
            this.splitMain.Name = "splitMain";
            this.splitMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitMain.Panel1
            // 
            this.splitMain.Panel1.Controls.Add(this.axfpView);
            // 
            // splitMain.Panel2
            // 

            this.splitMain.Panel2.Controls.Add(this.chartControl5);
            this.splitMain.Panel2.Controls.Add(this.chartControl3);
            this.splitMain.Panel2.Controls.Add(this.gaugeControl1);
            this.splitMain.Panel2.Controls.Add(this.chartControl4);
            this.splitMain.Panel2.Controls.Add(this.chartControl2);
            this.splitMain.Panel2.Controls.Add(this.chartControl1);
            this.splitMain.Size = new System.Drawing.Size(1884, 933);
            this.splitMain.SplitterDistance = 404;
            this.splitMain.TabIndex = 2;
            // 
            // axfpView
            // 
            this.axfpView.DataSource = null;
            this.axfpView.Location = new System.Drawing.Point(3, 6);
            this.axfpView.Name = "axfpView";
            this.axfpView.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axfpView.OcxState")));
            this.axfpView.Size = new System.Drawing.Size(2397, 495);
            this.axfpView.TabIndex = 0;
            // 
            // chartControl5
            // 
            this.chartControl5.AnimationStartMode = DevExpress.XtraCharts.ChartAnimationMode.OnDataChanged;
            this.chartControl5.BorderOptions.Visibility = DevExpress.Utils.DefaultBoolean.True;
            this.chartControl5.DataBindings = null;
            xyDiagram1.AxisX.Title.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            xyDiagram1.AxisX.Title.Text = "Hour";
            xyDiagram1.AxisX.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
            xyDiagram1.AxisX.VisibleInPanesSerializable = "-1";
            xyDiagram1.AxisX.VisualRange.Auto = false;
            xyDiagram1.AxisX.VisualRange.AutoSideMargins = false;
            xyDiagram1.AxisX.VisualRange.MaxValueSerializable = "9";
            xyDiagram1.AxisX.VisualRange.MinValueSerializable = "0";
            xyDiagram1.AxisX.VisualRange.SideMarginsValue = 1D;
            xyDiagram1.AxisY.Title.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            xyDiagram1.AxisY.Title.Text = "Production";
            xyDiagram1.AxisY.Title.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(176)))), ((int)(((byte)(240)))));
            xyDiagram1.AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.Default;
            xyDiagram1.AxisY.VisibleInPanesSerializable = "-1";
            this.chartControl5.Diagram = xyDiagram1;
            this.chartControl5.EmptyChartText.Font = new System.Drawing.Font("Tahoma", 30F);
            this.chartControl5.EmptyChartText.Text = "No Data";
            this.chartControl5.EmptyChartText.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.chartControl5.Legend.Name = "Default Legend";
            this.chartControl5.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;
            this.chartControl5.Location = new System.Drawing.Point(1290, 3);
            this.chartControl5.Name = "chartControl5";
            this.chartControl5.PaletteName = "Flow";
            pointSeriesLabel1.TextPattern = "{V:#,#}";
            series1.Label = pointSeriesLabel1;
            series1.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            series1.Name = "Series 2";
            splineSeriesView1.Color = System.Drawing.Color.Blue;
            splineSeriesView1.LineMarkerOptions.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))));
            splineSeriesView1.LineMarkerOptions.BorderVisible = false;
            splineSeriesView1.LineMarkerOptions.Color = System.Drawing.Color.DodgerBlue;
            splineSeriesView1.LineMarkerOptions.Size = 15;
            splineSeriesView1.LineStyle.Thickness = 3;
            splineSeriesView1.MarkerVisibility = DevExpress.Utils.DefaultBoolean.True;
            xySeriesSlideAnimation1.EasingFunction = sineEasingFunction1;
            splineSeriesView1.SeriesAnimation = xySeriesSlideAnimation1;
            splineSeriesView1.Shadow.Visible = true;
            series1.View = splineSeriesView1;
            this.chartControl5.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series1};
            this.chartControl5.Size = new System.Drawing.Size(593, 255);
            this.chartControl5.TabIndex = 7;
            chartTitle1.Text = "Stitching 2";
            this.chartControl5.Titles.AddRange(new DevExpress.XtraCharts.ChartTitle[] {
            chartTitle1});
            // 
            // chartControl3
            // 
            this.chartControl3.AnimationStartMode = DevExpress.XtraCharts.ChartAnimationMode.OnDataChanged;
            this.chartControl3.BorderOptions.Visibility = DevExpress.Utils.DefaultBoolean.True;
            this.chartControl3.DataBindings = null;
            xyDiagram2.AxisX.Title.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            xyDiagram2.AxisX.Title.Text = "Hour";
            xyDiagram2.AxisX.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
            xyDiagram2.AxisX.VisibleInPanesSerializable = "-1";
            xyDiagram2.AxisX.VisualRange.Auto = false;
            xyDiagram2.AxisX.VisualRange.AutoSideMargins = false;
            xyDiagram2.AxisX.VisualRange.MaxValueSerializable = "9";
            xyDiagram2.AxisX.VisualRange.MinValueSerializable = "0";
            xyDiagram2.AxisX.VisualRange.SideMarginsValue = 1D;
            xyDiagram2.AxisY.Title.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            xyDiagram2.AxisY.Title.Text = "Production";
            xyDiagram2.AxisY.Title.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(176)))), ((int)(((byte)(240)))));
            xyDiagram2.AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.Default;
            xyDiagram2.AxisY.VisibleInPanesSerializable = "-1";
            this.chartControl3.Diagram = xyDiagram2;
            this.chartControl3.EmptyChartText.Font = new System.Drawing.Font("Tahoma", 30F);
            this.chartControl3.EmptyChartText.Text = "No Data";
            this.chartControl3.EmptyChartText.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.chartControl3.Legend.Name = "Default Legend";
            this.chartControl3.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;
            this.chartControl3.Location = new System.Drawing.Point(647, 1);
            this.chartControl3.Name = "chartControl3";
            this.chartControl3.PaletteName = "Flow";
            pointSeriesLabel2.TextPattern = "{V:#,#}";
            series2.Label = pointSeriesLabel2;
            series2.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            series2.Name = "Series 2";
            splineSeriesView2.Color = System.Drawing.Color.Blue;
            splineSeriesView2.LineMarkerOptions.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))));
            splineSeriesView2.LineMarkerOptions.BorderVisible = false;
            splineSeriesView2.LineMarkerOptions.Color = System.Drawing.Color.DodgerBlue;
            splineSeriesView2.LineMarkerOptions.Size = 15;
            splineSeriesView2.LineStyle.Thickness = 3;
            splineSeriesView2.MarkerVisibility = DevExpress.Utils.DefaultBoolean.True;
            xySeriesSlideAnimation2.EasingFunction = sineEasingFunction2;
            splineSeriesView2.SeriesAnimation = xySeriesSlideAnimation2;
            splineSeriesView2.Shadow.Visible = true;
            series2.View = splineSeriesView2;
            this.chartControl3.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series2};
            this.chartControl3.Size = new System.Drawing.Size(593, 255);
            this.chartControl3.TabIndex = 6;
            chartTitle2.Text = "Stitching 1";
            this.chartControl3.Titles.AddRange(new DevExpress.XtraCharts.ChartTitle[] {
            chartTitle2});
            // 
            // gaugeControl1
            // 
            this.gaugeControl1.AutoLayout = false;
            this.gaugeControl1.Controls.Add(this.label5);
            this.gaugeControl1.Controls.Add(this.label4);
            this.gaugeControl1.Controls.Add(this.label3);
            this.gaugeControl1.Controls.Add(this.lblAssemblyProd);
            this.gaugeControl1.Controls.Add(this.label1);
            this.gaugeControl1.Controls.Add(this.lblRate);
            this.gaugeControl1.Controls.Add(this.lblProd);
            this.gaugeControl1.Controls.Add(this.lblRPlan);
            this.gaugeControl1.Gauges.AddRange(new DevExpress.XtraGauges.Base.IGauge[] {
            this.circularRealTime});
            this.gaugeControl1.Location = new System.Drawing.Point(1290, 261);
            this.gaugeControl1.Name = "gaugeControl1";
            this.gaugeControl1.Size = new System.Drawing.Size(593, 255);
            this.gaugeControl1.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Green;
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(357, 6);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 24);
            this.label5.TabIndex = 66;
            this.label5.Text = ">98%";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Yellow;
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(252, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 24);
            this.label4.TabIndex = 65;
            this.label4.Text = ">= 95% && <98%";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Red;
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(188, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 24);
            this.label3.TabIndex = 64;
            this.label3.Text = "<95%";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblAssemblyProd
            // 
            this.lblAssemblyProd.BackColor = System.Drawing.Color.Transparent;
            this.lblAssemblyProd.Font = new System.Drawing.Font("Calibri", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAssemblyProd.ForeColor = System.Drawing.Color.Black;
            this.lblAssemblyProd.Location = new System.Drawing.Point(6, 6);
            this.lblAssemblyProd.Name = "lblAssemblyProd";
            this.lblAssemblyProd.Size = new System.Drawing.Size(188, 39);
            this.lblAssemblyProd.TabIndex = 63;
            this.lblAssemblyProd.Text = "Assemly Production";
            this.lblAssemblyProd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(176, 219);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 39);
            this.label1.TabIndex = 62;
            this.label1.Text = "%";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblRate
            // 
            this.lblRate.BackColor = System.Drawing.Color.DarkTurquoise;
            this.lblRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblRate.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRate.ForeColor = System.Drawing.Color.White;
            this.lblRate.Location = new System.Drawing.Point(421, 92);
            this.lblRate.Name = "lblRate";
            this.lblRate.Size = new System.Drawing.Size(167, 39);
            this.lblRate.TabIndex = 61;
            this.lblRate.Text = "Rate:";
            this.lblRate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblProd
            // 
            this.lblProd.BackColor = System.Drawing.Color.DarkTurquoise;
            this.lblProd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblProd.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProd.ForeColor = System.Drawing.Color.White;
            this.lblProd.Location = new System.Drawing.Point(421, 49);
            this.lblProd.Name = "lblProd";
            this.lblProd.Size = new System.Drawing.Size(167, 39);
            this.lblProd.TabIndex = 60;
            this.lblProd.Text = "Prod:";
            this.lblProd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblRPlan
            // 
            this.lblRPlan.BackColor = System.Drawing.Color.DarkTurquoise;
            this.lblRPlan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblRPlan.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRPlan.ForeColor = System.Drawing.Color.White;
            this.lblRPlan.Location = new System.Drawing.Point(421, 6);
            this.lblRPlan.Name = "lblRPlan";
            this.lblRPlan.Size = new System.Drawing.Size(167, 39);
            this.lblRPlan.TabIndex = 59;
            this.lblRPlan.Text = "R.Plan";
            this.lblRPlan.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // circularRealTime
            // 
            this.circularRealTime.AutoSize = DevExpress.Utils.DefaultBoolean.True;
            this.circularRealTime.BackgroundLayers.AddRange(new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleBackgroundLayerComponent[] {
            this.arcScaleBackgroundLayerComponent1});
            this.circularRealTime.Bounds = new System.Drawing.Rectangle(6, 67, 394, 187);
            this.circularRealTime.Name = "circularRealTime";
            this.circularRealTime.Needles.AddRange(new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleNeedleComponent[] {
            this.arcScaleNeedleComponent1});
            this.circularRealTime.Scales.AddRange(new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleComponent[] {
            this.arcScaleComponent1});
            // 
            // arcScaleBackgroundLayerComponent1
            // 
            this.arcScaleBackgroundLayerComponent1.ArcScale = this.arcScaleComponent1;
            this.arcScaleBackgroundLayerComponent1.Name = "bg";
            this.arcScaleBackgroundLayerComponent1.ScaleCenterPos = new DevExpress.XtraGauges.Core.Base.PointF2D(0.5F, 0.71F);
            this.arcScaleBackgroundLayerComponent1.ShapeType = DevExpress.XtraGauges.Core.Model.BackgroundLayerShapeType.CircularHalf_Style4;
            this.arcScaleBackgroundLayerComponent1.Size = new System.Drawing.SizeF(250F, 176F);
            this.arcScaleBackgroundLayerComponent1.ZOrder = 1000;
            // 
            // arcScaleComponent1
            // 
            this.arcScaleComponent1.AppearanceMajorTickmark.BorderBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:White");
            this.arcScaleComponent1.AppearanceMajorTickmark.ContentBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:White");
            this.arcScaleComponent1.AppearanceMinorTickmark.BorderBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:White");
            this.arcScaleComponent1.AppearanceMinorTickmark.ContentBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:White");
            this.arcScaleComponent1.AppearanceTickmarkText.Font = new System.Drawing.Font("Tahoma", 12F);
            this.arcScaleComponent1.AppearanceTickmarkText.TextBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:Black");
            this.arcScaleComponent1.Center = new DevExpress.XtraGauges.Core.Base.PointF2D(125F, 165F);
            this.arcScaleComponent1.EndAngle = 0F;
            this.arcScaleComponent1.MajorTickCount = 6;
            this.arcScaleComponent1.MajorTickmark.FormatString = "{0:F0}";
            this.arcScaleComponent1.MajorTickmark.ShapeOffset = -14F;
            this.arcScaleComponent1.MajorTickmark.ShapeScale = new DevExpress.XtraGauges.Core.Base.FactorF2D(0.6F, 0.8F);
            this.arcScaleComponent1.MajorTickmark.ShapeType = DevExpress.XtraGauges.Core.Model.TickmarkShapeType.Circular_Style4_2;
            this.arcScaleComponent1.MajorTickmark.TextOffset = -27F;
            this.arcScaleComponent1.MajorTickmark.TextOrientation = DevExpress.XtraGauges.Core.Model.LabelOrientation.LeftToRight;
            this.arcScaleComponent1.MaxValue = 100F;
            this.arcScaleComponent1.MinorTickCount = 4;
            this.arcScaleComponent1.MinorTickmark.ShapeOffset = -7F;
            this.arcScaleComponent1.MinorTickmark.ShapeScale = new DevExpress.XtraGauges.Core.Base.FactorF2D(0.6F, 1F);
            this.arcScaleComponent1.MinorTickmark.ShapeType = DevExpress.XtraGauges.Core.Model.TickmarkShapeType.Circular_Style4_1;
            this.arcScaleComponent1.Name = "scale1";
            this.arcScaleComponent1.RadiusX = 123F;
            this.arcScaleComponent1.RadiusY = 123F;
            arcScaleRange1.AppearanceRange.ContentBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:Red");
            arcScaleRange1.EndThickness = 12F;
            arcScaleRange1.EndValue = 33F;
            arcScaleRange1.Name = "Range0";
            arcScaleRange1.ShapeOffset = -3F;
            arcScaleRange1.StartThickness = 12F;
            arcScaleRange1.StartValue = -0.3F;
            arcScaleRange2.AppearanceRange.ContentBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:Yellow");
            arcScaleRange2.EndThickness = 12F;
            arcScaleRange2.EndValue = 66F;
            arcScaleRange2.Name = "Range1";
            arcScaleRange2.ShapeOffset = -3F;
            arcScaleRange2.StartThickness = 12F;
            arcScaleRange2.StartValue = 33F;
            arcScaleRange3.AppearanceRange.ContentBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:Green");
            arcScaleRange3.EndThickness = 12F;
            arcScaleRange3.EndValue = 100.3F;
            arcScaleRange3.Name = "Range2";
            arcScaleRange3.ShapeOffset = -3F;
            arcScaleRange3.StartThickness = 12F;
            arcScaleRange3.StartValue = 66F;
            this.arcScaleComponent1.Ranges.AddRange(new DevExpress.XtraGauges.Core.Model.IRange[] {
            arcScaleRange1,
            arcScaleRange2,
            arcScaleRange3});
            this.arcScaleComponent1.StartAngle = -180F;
            this.arcScaleComponent1.Value = 80F;
            // 
            // arcScaleNeedleComponent1
            // 
            this.arcScaleNeedleComponent1.ArcScale = this.arcScaleComponent1;
            this.arcScaleNeedleComponent1.EndOffset = 5F;
            this.arcScaleNeedleComponent1.Name = "needle";
            this.arcScaleNeedleComponent1.ShapeType = DevExpress.XtraGauges.Core.Model.NeedleShapeType.CircularFull_Style4;
            this.arcScaleNeedleComponent1.StartOffset = -21F;
            this.arcScaleNeedleComponent1.ZOrder = -50;
            // 
            // chartControl4
            // 
            this.chartControl4.AnimationStartMode = DevExpress.XtraCharts.ChartAnimationMode.OnDataChanged;
            this.chartControl4.DataBindings = null;
            xyDiagram3.AxisX.VisibleInPanesSerializable = "-1";
            xyDiagram3.AxisY.Title.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            xyDiagram3.AxisY.Title.Text = "Production";
            xyDiagram3.AxisY.Title.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(176)))), ((int)(((byte)(240)))));
            xyDiagram3.AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.Default;
            xyDiagram3.AxisY.VisibleInPanesSerializable = "-1";
            secondaryAxisY1.AxisID = 0;
            secondaryAxisY1.Name = "Secondary AxisY 1";
            secondaryAxisY1.Visibility = DevExpress.Utils.DefaultBoolean.False;
            secondaryAxisY1.VisibleInPanesSerializable = "-1";
            xyDiagram3.SecondaryAxesY.AddRange(new DevExpress.XtraCharts.SecondaryAxisY[] {
            secondaryAxisY1});
            this.chartControl4.Diagram = xyDiagram3;
            this.chartControl4.EmptyChartText.Font = new System.Drawing.Font("Tahoma", 30F);
            this.chartControl4.EmptyChartText.Text = "No Data";
            this.chartControl4.EmptyChartText.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.chartControl4.Legend.Name = "Default Legend";
            this.chartControl4.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;
            this.chartControl4.Location = new System.Drawing.Point(647, 261);
            this.chartControl4.Name = "chartControl4";
            this.chartControl4.PaletteName = "Flow";
            pointSeriesLabel3.TextPattern = "{V:#,#}";
            series3.Label = pointSeriesLabel3;
            series3.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            series3.Name = "Series 2";
            splineSeriesView3.Color = System.Drawing.Color.Blue;
            splineSeriesView3.LineMarkerOptions.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))));
            splineSeriesView3.LineMarkerOptions.BorderVisible = false;
            splineSeriesView3.LineMarkerOptions.Color = System.Drawing.Color.DodgerBlue;
            splineSeriesView3.LineMarkerOptions.Size = 15;
            splineSeriesView3.LineStyle.Thickness = 3;
            splineSeriesView3.MarkerVisibility = DevExpress.Utils.DefaultBoolean.True;
            xySeriesSlideAnimation3.EasingFunction = sineEasingFunction3;
            splineSeriesView3.SeriesAnimation = xySeriesSlideAnimation3;
            splineSeriesView3.Shadow.Visible = true;
            series3.View = splineSeriesView3;
            this.chartControl4.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series3};
            this.chartControl4.Size = new System.Drawing.Size(593, 255);
            this.chartControl4.TabIndex = 3;
            chartTitle3.Text = "Assembly";
            this.chartControl4.Titles.AddRange(new DevExpress.XtraCharts.ChartTitle[] {
            chartTitle3});
            // 
            // chartControl2
            // 
            this.chartControl2.AnimationStartMode = DevExpress.XtraCharts.ChartAnimationMode.OnDataChanged;
            this.chartControl2.DataBindings = null;
            xyDiagram4.AxisX.VisibleInPanesSerializable = "-1";
            xyDiagram4.AxisY.Title.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            xyDiagram4.AxisY.Title.Text = "Production";
            xyDiagram4.AxisY.Title.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(176)))), ((int)(((byte)(240)))));
            xyDiagram4.AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.Default;
            xyDiagram4.AxisY.VisibleInPanesSerializable = "-1";
            secondaryAxisY2.AxisID = 0;
            secondaryAxisY2.Name = "Secondary AxisY 1";
            secondaryAxisY2.Visibility = DevExpress.Utils.DefaultBoolean.False;
            secondaryAxisY2.VisibleInPanesSerializable = "-1";
            xyDiagram4.SecondaryAxesY.AddRange(new DevExpress.XtraCharts.SecondaryAxisY[] {
            secondaryAxisY2});
            this.chartControl2.Diagram = xyDiagram4;
            this.chartControl2.EmptyChartText.Font = new System.Drawing.Font("Tahoma", 30F);
            this.chartControl2.EmptyChartText.Text = "No Data";
            this.chartControl2.EmptyChartText.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.chartControl2.Legend.Name = "Default Legend";
            this.chartControl2.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;
            this.chartControl2.Location = new System.Drawing.Point(4, 261);
            this.chartControl2.Name = "chartControl2";
            this.chartControl2.PaletteName = "Flow";
            pointSeriesLabel4.TextPattern = "{V:#,#}";
            series4.Label = pointSeriesLabel4;
            series4.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            series4.Name = "Series 2";
            splineSeriesView4.Color = System.Drawing.Color.Blue;
            splineSeriesView4.LineMarkerOptions.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))));
            splineSeriesView4.LineMarkerOptions.BorderVisible = false;
            splineSeriesView4.LineMarkerOptions.Color = System.Drawing.Color.DodgerBlue;
            splineSeriesView4.LineMarkerOptions.Size = 15;
            splineSeriesView4.LineStyle.Thickness = 3;
            splineSeriesView4.MarkerVisibility = DevExpress.Utils.DefaultBoolean.True;
            xySeriesSlideAnimation4.EasingFunction = sineEasingFunction4;
            splineSeriesView4.SeriesAnimation = xySeriesSlideAnimation4;
            splineSeriesView4.Shadow.Visible = true;
            series4.View = splineSeriesView4;
            this.chartControl2.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series4};
            this.chartControl2.Size = new System.Drawing.Size(593, 255);
            this.chartControl2.TabIndex = 1;
            chartTitle4.Text = "Stockfit";
            this.chartControl2.Titles.AddRange(new DevExpress.XtraCharts.ChartTitle[] {
            chartTitle4});
            // 
            // chartControl1
            // 
            this.chartControl1.AnimationStartMode = DevExpress.XtraCharts.ChartAnimationMode.OnDataChanged;
            this.chartControl1.BorderOptions.Visibility = DevExpress.Utils.DefaultBoolean.True;
            this.chartControl1.DataBindings = null;
            xyDiagram5.AxisX.Title.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            xyDiagram5.AxisX.Title.Text = "Hour";
            xyDiagram5.AxisX.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
            xyDiagram5.AxisX.VisibleInPanesSerializable = "-1";
            xyDiagram5.AxisX.VisualRange.Auto = false;
            xyDiagram5.AxisX.VisualRange.AutoSideMargins = false;
            xyDiagram5.AxisX.VisualRange.MaxValueSerializable = "9";
            xyDiagram5.AxisX.VisualRange.MinValueSerializable = "0";
            xyDiagram5.AxisX.VisualRange.SideMarginsValue = 1D;
            xyDiagram5.AxisY.Title.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            xyDiagram5.AxisY.Title.Text = "Production";
            xyDiagram5.AxisY.Title.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(176)))), ((int)(((byte)(240)))));
            xyDiagram5.AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.Default;
            xyDiagram5.AxisY.VisibleInPanesSerializable = "-1";
            this.chartControl1.Diagram = xyDiagram5;
            this.chartControl1.EmptyChartText.Font = new System.Drawing.Font("Tahoma", 30F);
            this.chartControl1.EmptyChartText.Text = "No Data";
            this.chartControl1.EmptyChartText.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.chartControl1.Legend.Name = "Default Legend";
            this.chartControl1.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;
            this.chartControl1.Location = new System.Drawing.Point(4, 1);
            this.chartControl1.Name = "chartControl1";
            this.chartControl1.PaletteName = "Flow";
            pointSeriesLabel5.TextPattern = "{V:#,#}";
            series5.Label = pointSeriesLabel5;
            series5.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            series5.Name = "Series 2";
            splineSeriesView5.Color = System.Drawing.Color.Blue;
            splineSeriesView5.LineMarkerOptions.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))));
            splineSeriesView5.LineMarkerOptions.BorderVisible = false;
            splineSeriesView5.LineMarkerOptions.Color = System.Drawing.Color.DodgerBlue;
            splineSeriesView5.LineMarkerOptions.Size = 15;
            splineSeriesView5.LineStyle.Thickness = 3;
            splineSeriesView5.MarkerVisibility = DevExpress.Utils.DefaultBoolean.True;
            xySeriesSlideAnimation5.EasingFunction = sineEasingFunction5;
            splineSeriesView5.SeriesAnimation = xySeriesSlideAnimation5;
            splineSeriesView5.Shadow.Visible = true;
            series5.View = splineSeriesView5;
            this.chartControl1.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series5};
            this.chartControl1.Size = new System.Drawing.Size(593, 255);
            this.chartControl1.TabIndex = 0;
            chartTitle5.Text = "Cutting";
            this.chartControl1.Titles.AddRange(new DevExpress.XtraCharts.ChartTitle[] {
            chartTitle5});
            // 
            // tmr_Date
            // 
            this.tmr_Date.Enabled = false;
            this.tmr_Date.Interval = 1000;
            this.tmr_Date.Tick += new System.EventHandler(this.tmr_Date_Tick);
            // 
            // FORM_SMT_PROD_DAILY
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1884, 1042);
            this.Controls.Add(this.splitMain);
            this.Controls.Add(this.panel1);
            this.Name = "FORM_SMT_PROD_DAILY";
            this.Text = "FORM_SMT_PROD_DAILY";
            this.Load += new System.EventHandler(this.FORM_SMT_PROD_DAILY_Load);
            this.VisibleChanged += new System.EventHandler(this.FORM_SMT_PROD_DAILY_VisibleChanged);
            this.panel1.ResumeLayout(false);
            this.splitMain.Panel1.ResumeLayout(false);
            this.splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).EndInit();
            this.splitMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axfpView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(pointSeriesLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(splineSeriesView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(pointSeriesLabel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(splineSeriesView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl3)).EndInit();
            this.gaugeControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.circularRealTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.arcScaleBackgroundLayerComponent1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.arcScaleComponent1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.arcScaleNeedleComponent1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(secondaryAxisY1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(pointSeriesLabel3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(splineSeriesView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(secondaryAxisY2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(pointSeriesLabel4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(splineSeriesView4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(pointSeriesLabel5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(splineSeriesView5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).EndInit();
            this.ResumeLayout(false);

        }

        public FORM_SMT_PROD_DAILY()
        {
            InitializeComponent();
        }
        public FORM_SMT_PROD_DAILY(string title)
        {
            InitializeComponent();          
            lblTitle.Text = title;
           // lblTitle.Text = title + "\n" + "Tình trạng sản xuất hàng ngày";


        }
        int indexScreen;
        public FORM_SMT_PROD_DAILY(string title, int _indexScreen)
        {
            InitializeComponent();
            indexScreen = _indexScreen;
            lblTitle.Text = title;
            //lblTitle.Text = title + "\n" + "Tình trạng sản xuất hàng ngày";
          

        }
        public FORM_SMT_PROD_DAILY(string title, int _indexScreen, string _line_cd)
        {
            InitializeComponent();
            indexScreen = _indexScreen;
            lblTitle.Text = title;
            sLineCD = _line_cd;
          //  lblTitle.Text = title + "\n" + "Tình trạng sản xuất hàng ngày";


        }

        public FORM_SMT_PROD_DAILY(string title, int _indexScreen, string _line_cd, string _mline_cd)
        {
            InitializeComponent();
            indexScreen = _indexScreen;
            lblTitle.Text = title;
            sLineCD = _line_cd;
            sMLineCD = _mline_cd;
          //  lblTitle.Text = title + "\n" + "Tình trạng sản xuất hàng ngày";


        }

        public FORM_SMT_PROD_DAILY(string title, int _indexScreen, string _line_cd, string _mline_cd, string _Lang)
        {
            InitializeComponent();
            indexScreen = _indexScreen;
            lblTitle.Text = title;
            sLineCD = _line_cd;
            sMLineCD = _mline_cd;
            Lang = _Lang;
            lblTitle.Text = title;

            if (this.sMLineCD == "001")
            {
                sStitching1TitleEn = "Stitching 1(Start 07:30)";
                sStitching2TitleEn = "Stitching 2(Start 07:30)";
                sStitching1TitleVn = "May 1(Từ 07:30)";
                sStitching2TitleVn = "May 2(Từ 07:30)";
            }
            if (this.sMLineCD == "002")
            {
                sStitching1TitleEn = "Stitching 3(Start 07:30)";
                sStitching2TitleEn = "Stitching 4(Start 07:30)";
                sStitching1TitleVn = "May 3(Từ 07:30)";
                sStitching2TitleVn = "May 4(Từ 07:30)";
            }
            else if (this.sMLineCD == "003")
            {
                sStitching1TitleEn = "Stitching 5(Start 07:30)";
                sStitching2TitleEn = "Stitching 6(Start 07:30)";
                sStitching1TitleVn = "May 5(Từ 07:30)";
                sStitching2TitleVn = "May 6(Từ 07:30)";
            }
            else if (this.sMLineCD == "004")
            {
                sStitching1TitleEn = "Stitching 7(Start 07:30)";
                sStitching2TitleEn = "Stitching 8(Start 07:30)";
                sStitching1TitleVn = "May 7(Từ 07:30)";
                sStitching2TitleVn = "May 8(Từ 07:30)";
            }

            switch (Lang)
            {
                case "Vn":
                    btnDaily.Text = "Ngày";
                    btnMonthly.Text = "Tháng";
                    btnWeekly.Text = "Tuần";
                    btnYearly.Text = "Năm";
                    sTimeTitle = "Giờ";
                    sCuttingTitle = "Cắt (Start 07:30)";
                    sStitching1Title = sStitching1TitleVn;
                    sStitching2Title = sStitching2TitleVn;
                    sStockfitTitle = "Chuẩn bị(Từ 07:30)";
                    sAssemblyTitle = "Lắp ráp(Từ 08:00)";
                    sTarget = "Mục tiêu";
                    sActual = "Thực tế";
                    sProducQty = "Sản xuất (Prs)";
                    sHour = "Giờ";
                    sChartTarget = "Mục tiêu";
                    sChartActual = "Thực tế";
                    sAssemblyProduct = "Sản xuất lắp ráp";
                    sRate = "Tỷ lệ";
                    break;
                case "En":
                    btnDaily.Text = "Day";
                    btnMonthly.Text = "Month";
                    btnWeekly.Text = "Week";
                    btnYearly.Text = "Year";

                    sTimeTitle = "Time";
                    sCuttingTitle = "Cutting (Start 07:30)";
                    sStitching1Title = sStitching1TitleEn;
                    sStitching2Title = sStitching2TitleEn;
                    sStockfitTitle = "Stockfit (Start 07:30)";
                    sAssemblyTitle = "Assembly (Start 08:00)";
                    sTarget = "Target";
                    sActual = "Actual";
                    sProducQty = "Product Qty(Prs)";
                    sHour = "Hour";
                    sChartTarget = "Target Qty";
                    sChartActual = "Actual Qty";
                    sAssemblyProduct = "Assembly Production";
                    sRate = "Rate";
                    break;

            }


        }
        const int AW_SLIDE = 0X40000;
        const int AW_HOR_POSITIVE = 0X4;
        const int AW_HOR_NEGATIVE = 0X2;
        const int AW_BLEND = 0X80000;
        const int AW_HIDE = 0x00010000;
       
        [DllImport("user32")]
        static extern bool AnimateWindow(IntPtr hwnd, int time, int flags);
        DataTable dt = null;
        int CreLoad = 0;
        private static readonly Random getrandom = new Random();
        private static readonly object syncLock = new object();
        public static int GetRandomNumber(int min, int max)
        {
            lock (syncLock)
            { // synchronize
                return getrandom.Next(min, max);
            }
        }
        // SEL_DMC_SUMPROD_BY_CELL
        public DataTable SP_SMT_PROD_DAILY_NEW(string ARG_DATE, string ARG_LINE_CD, string ARG_MLINE_CD)
        {
            COM.OraDB MyOraDB = new COM.OraDB();
            DataSet ds_ret;

            try
            {
                string process_name = "MES.PKG_SMT_PROD_DAILY.SP_SMT_PROD_DAILY_NEW";

                MyOraDB.ReDim_Parameter(4);
                MyOraDB.Process_Name = process_name;

                MyOraDB.Parameter_Name[0] = "ARG_DATE";
                MyOraDB.Parameter_Name[1] = "ARG_LINE_CD";
                MyOraDB.Parameter_Name[2] = "ARG_MLINE_CD";
                MyOraDB.Parameter_Name[3] = "CV_1";

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

        public DataTable SP_SMT_GET_CURRENT_HH()
        {
            COM.OraDB MyOraDB = new COM.OraDB();
            DataSet ds_ret;

            try
            {
                string process_name = "MES.PKG_SMT_PROD_DAILY.SP_SMT_GET_CURRENT_HH";

                MyOraDB.ReDim_Parameter(1);
                MyOraDB.Process_Name = process_name;
             
                MyOraDB.Parameter_Name[0] = "CV_1";

               
                MyOraDB.Parameter_Type[0] = (int)OracleType.Cursor;

            
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

        //SELECT_DMC_INVENTORY_CHART
        public DataTable SP_SMT_PROD_DAILY_BY_OP_NEW(string ARG_DATE, string ARG_LINE_CD, string ARG_MLINE_CD, string ARG_OP_CD)
        {
            COM.OraDB MyOraDB = new COM.OraDB();
            DataSet ds_ret;

            try
            {
                string process_name = "MES.PKG_SMT_PROD_DAILY.SP_SMT_PROD_DAILY_BY_OP_NEW";

                MyOraDB.ReDim_Parameter(5);
                MyOraDB.Process_Name = process_name;

                MyOraDB.Parameter_Name[0] = "ARG_DATE";
                MyOraDB.Parameter_Name[1] = "ARG_LINE_CD";
                MyOraDB.Parameter_Name[2] = "ARG_MLINE_CD";
                MyOraDB.Parameter_Name[3] = "ARG_OP_CD";
                MyOraDB.Parameter_Name[4] = "CV_1";

                MyOraDB.Parameter_Type[0] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[1] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[2] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[3] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[4] = (int)OracleType.Cursor;

                MyOraDB.Parameter_Values[0] = ARG_DATE;
                MyOraDB.Parameter_Values[1] = ARG_LINE_CD;
                MyOraDB.Parameter_Values[2] = ARG_MLINE_CD;
                MyOraDB.Parameter_Values[3] = ARG_OP_CD;
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

        public DataTable SP_SMT_PROD_DAILY_REALTIME(string ARG_DATE, string ARG_LINE_CD, string ARG_MLINE_CD)
        {
            COM.OraDB MyOraDB = new COM.OraDB();
            DataSet ds_ret;

            try
            {
                string process_name = "MES.PKG_SMT_PROD_DAILY.SP_SMT_PROD_DAILY_REALTIME";

                MyOraDB.ReDim_Parameter(4);
                MyOraDB.Process_Name = process_name;

                MyOraDB.Parameter_Name[0] = "ARG_DATE";
                MyOraDB.Parameter_Name[1] = "ARG_LINE_CD";
                MyOraDB.Parameter_Name[2] = "ARG_MLINE_CD";              
                MyOraDB.Parameter_Name[3] = "CV_1";

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
        //FullScreen
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
            flg.Hide();
            this.Cursor = Cursors.WaitCursor;
            BindingData();
            AnimateWindow(flg.Handle, 300, AW_SLIDE | 0X4); //IPEX_Monitor.ClassLib.WinAPI.getSlidType("2")
            flg.Show();
            this.Cursor = Cursors.Default;
        }

        //Clear Data & set default Color for Grid
        Color BackColor1 = Color.FromArgb(232, 246, 247);
        Color BackColor2 = Color.White;
        private void ClearGrid(AxFPUSpreadADO.AxfpSpread Grid)
        {
            for (int irow = 3; irow <= Grid.MaxRows; irow++)
            {
                Grid.Row = irow;
                for (int icol = 1; icol <= Grid.MaxCols; icol++)
                {
                    Grid.Col = icol;
                    Grid.SetText(icol, irow, "");
                    switch (irow % 2)
                    {
                        case 0:
                            Grid.BackColor = BackColor1;
                            Grid.ForeColor = Color.Black;
                            break;
                        case 1:
                            Grid.BackColor = BackColor2;
                            Grid.ForeColor = Color.Black;
                            break;
                    }
                    Grid.Font = new Font("Calibri", 16, FontStyle.Regular);
                }
               
                axfpView.set_RowHeight(irow, 22);
            }
            axfpView.RowsFrozen = 3;         

           
        }
        private void CreateChart()
        {
            CreateChart(sLineCD, sMLineCD, "UPC", chartControl1, sCuttingTitle);
            CreateChart(sLineCD, sMLineCD, "FSS", chartControl2, sStockfitTitle);
            CreateChart(sLineCD, sMLineCD, "UPS1", chartControl3,sStitching1Title);
            CreateChart(sLineCD, sMLineCD, "FGA", chartControl4, sAssemblyTitle);
            CreateChart(sLineCD, sMLineCD, "UPS2", chartControl5, sStitching2Title);
             string Now = DateTime.Now.ToString("yyyyMMdd");
            DataTable dt = SP_SMT_PROD_DAILY_REALTIME(Now, sLineCD,sMLineCD);
            double num = 0; double iMin = 0; double iMax = 100; double iRangeMin = 95; double iRangeMax = 98;
            if( dt!= null && dt.Rows.Count>0)
            {
                num = Convert.ToDouble(dt.Rows[0]["RATE"].ToString());
                if (num > 3 )
                {
                    if (num < 92)
                    {
                        iMin = Math.Round(num, 0) - 3;
                        //iMax = 98;
                    }
                    else
                    {
                        iMin = 92;
                        //iMax = Math.Round(num, 0) + 3;
                    }
                    if (num > 101)
                    {
                        iMax = Math.Round(num, 0) + 3;

                    }
                    else
                    {
                        iMax = 101;
                    }
                     
                    //iMin = Math.Round(num, 0) - 3;
                    //iMax = Math.Round(num, 0) + 3;
                }
                else
                {
                    iMin = 0;
                    iMax = 100;
                }
                //iMax = Math.Round(num,0) + 3;
                iRangeMin = 95;
                iRangeMax = 98;
                lblAssemblyProd.Text = sAssemblyProduct;
                lblRPlan.Text = sTarget + ": " + Convert.ToDouble(dt.Rows[0]["TARGET"].ToString()).ToString("#,###") + "Prs";
                lblProd.Text = sActual + " : " + Convert.ToDouble(dt.Rows[0]["QTY"].ToString()).ToString("#,###") + "Prs";
                lblRate.Text = sRate + " : " + Convert.ToDouble(dt.Rows[0]["RATE"].ToString()).ToString("#,###.##") + "%";
                if (Convert.ToDouble(dt.Rows[0]["RATE"].ToString()) > 98)
                {
                    lblRate.BackColor = Color.Green;

                }
                else if (Convert.ToDouble(dt.Rows[0]["RATE"].ToString()) > 95)
                {
                    lblRate.BackColor = Color.Yellow;

                }
                else
                {
                    lblRate.BackColor = Color.Red;
                }

            }
            BindingGauges(circularRealTime, num, iMin, iMax, iRangeMin, iRangeMax);
            
        }

        private void CreateChart(string line_cd, string mline_cd, string op_cd, DevExpress.XtraCharts.ChartControl _chartControl, string _title)
        {
            // Create a new chart.
            _chartControl.Series.Clear();
            _chartControl.Titles[0].Text = _title;
            //DataSource
            string Now = DateTime.Now.ToString("yyyyMMdd");
            DataTable dt = SP_SMT_PROD_DAILY_BY_OP_NEW(Now, line_cd, mline_cd, op_cd);

            // Create two series.
            Series series1 = new Series(sChartTarget, ViewType.Line);
            Series series2 = new Series(sChartActual, ViewType.Spline);

            DevExpress.XtraCharts.SplineSeriesView splineSeriesView1 = new DevExpress.XtraCharts.SplineSeriesView();
            DevExpress.XtraCharts.SplineSeriesView splineSeriesView2 = new DevExpress.XtraCharts.SplineSeriesView();
            DevExpress.XtraCharts.SideBySideBarSeriesView sideBySideBarSeriesView1 = new DevExpress.XtraCharts.SideBySideBarSeriesView();
            DevExpress.XtraCharts.PointSeriesLabel pointSeriesLabel1 = new DevExpress.XtraCharts.PointSeriesLabel();
            DevExpress.XtraCharts.BarWidenAnimation barWidenAnimation1 = new DevExpress.XtraCharts.BarWidenAnimation();
            DevExpress.XtraCharts.ElasticEasingFunction elasticEasingFunction1 = new DevExpress.XtraCharts.ElasticEasingFunction();


            DevExpress.XtraCharts.XYSeriesBlowUpAnimation xySeriesBlowUpAnimation2 = new DevExpress.XtraCharts.XYSeriesBlowUpAnimation();
            DevExpress.XtraCharts.XYSeriesUnwindAnimation xySeriesUnwindAnimation2 = new DevExpress.XtraCharts.XYSeriesUnwindAnimation();
            DevExpress.XtraCharts.XYSeriesUnwrapAnimation xySeriesUnwrapAnimation2 = new DevExpress.XtraCharts.XYSeriesUnwrapAnimation();

            DevExpress.XtraCharts.PowerEasingFunction powerEasingFunction2 = new DevExpress.XtraCharts.PowerEasingFunction();
            DevExpress.XtraCharts.SineEasingFunction sineEasingFunction2 = new DevExpress.XtraCharts.SineEasingFunction();
            // Add points to them, with their arguments different.
            if (dt != null && dt.Rows.Count > 0)
            {
                
                for (int i = 0; i <dt.Rows.Count; i++)
                {
                    series1.Points.Add(new SeriesPoint(dt.Rows[i]["HMS"].ToString(), dt.Rows[i]["TARGET"])); //GetRandomNumber(10, 50)
                    series2.Points.Add(new SeriesPoint(dt.Rows[i]["HMS"].ToString(), dt.Rows[i]["QTY"])); //dt.Rows[i]["HMS"]
                }
                //_chartControl1.Series[0].ArgumentScaleType = ScaleType.Qualitative;
            }   
            else
            {
                for (int i = 1; i < 9; i++)
                {
                    //series1.Points.Add(new SeriesPoint(dt.Rows[i]["HMS"].ToString(), dt.Rows[i]["QTY"])); //GetRandomNumber(10, 50)
                    series1.Points.Add(new SeriesPoint(i + "H",0)); 
                    series2.Points.Add(new SeriesPoint(i +"H", 0)); //dt.Rows[i]["HMS"]
                }
            }

            // series2 = splineSeriesView1;
            // Add both series to the chart.
            //chartControl1.Series.AddRange(new Series[] { series1, series2 });


            _chartControl.SeriesSerializable = new DevExpress.XtraCharts.Series[] {series1, series2 };
            ((XYDiagram)_chartControl.Diagram).AxisY.Title.Text = sProducQty ;
            ((XYDiagram)_chartControl.Diagram).AxisY.Title.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(176)))), ((int)(((byte)(240)))));
            ((XYDiagram)_chartControl.Diagram).AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.Default;
            ((XYDiagram)_chartControl.Diagram).AxisX.Title.Text = sHour;
            ((XYDiagram)_chartControl.Diagram).AxisX.Title.Visibility = DevExpress.Utils.DefaultBoolean.Default;
            ((XYDiagram)_chartControl.Diagram).AxisX.Title.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(176)))), ((int)(((byte)(240)))));


            
            // Create two secondary axes, and add them to the chart's Diagram.
        
   

            //Series 1
            //barSlideAnimation1.EasingFunction = bounceEasingFunction1;
            //sideBySideBarSeriesView1.Animation = barWidenAnimation1;
            //series1.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            //sideBySideBarSeriesView1.ColorEach = false;
            //sideBySideBarSeriesView1.Shadow.Visible = false;
            //sideBySideBarSeriesView1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(241)))), ((int)(((byte)(6)))));
            //series1.View = sideBySideBarSeriesView1;

            // Series 2
            //splineSeriesView1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(21)))), ((int)(((byte)(203)))));


            splineSeriesView1.MarkerVisibility = DevExpress.Utils.DefaultBoolean.True;
            splineSeriesView1.Shadow.Visible = false;
            splineSeriesView1.Color = System.Drawing.Color.Green;
            splineSeriesView1.LineMarkerOptions.BorderColor = System.Drawing.Color.DodgerBlue;
            splineSeriesView1.LineMarkerOptions.BorderVisible = false;

            splineSeriesView2.MarkerVisibility = DevExpress.Utils.DefaultBoolean.True;
            splineSeriesView2.Shadow.Visible = false;
            splineSeriesView2.Color = System.Drawing.Color.DodgerBlue;
            splineSeriesView2.LineMarkerOptions.BorderColor = System.Drawing.Color.DodgerBlue;
            splineSeriesView2.LineMarkerOptions.BorderVisible = false;
           
            //splineSeriesView1.LineMarkerOptions.Color = System.Drawing.Color.Red;
            splineSeriesView2.LineMarkerOptions.Kind = DevExpress.XtraCharts.MarkerKind.Circle;
            splineSeriesView2.LineMarkerOptions.Color = System.Drawing.Color.DodgerBlue;
            splineSeriesView2.LineMarkerOptions.Size = 15;
            splineSeriesView2.LineStyle.Thickness = 3;
            series2.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            //pointSeriesLabel1.TextPattern = "{V:#,#}";

            series1.View = splineSeriesView1;

            series2.Label.TextPattern = "{V:#,#}";
            series2.View = splineSeriesView2;
            //((LineSeriesView)series2.View).AxisY = myAxisY;
            xySeriesUnwindAnimation2.EasingFunction = sineEasingFunction2; //powerEasingFunction1;
            splineSeriesView2.SeriesAnimation = xySeriesUnwindAnimation2;//xySeriesBlowUpAnimation1;//xySeriesUnwindAnimation1; // xySeriesUnwrapAnimation1;
            //Legend
            //_chartControl1.Legend.AlignmentHorizontal = DevExpress.XtraCharts.LegendAlignmentHorizontal.Center;
            //_chartControl1.Legend.Direction = DevExpress.XtraCharts.LegendDirection.LeftToRight;
            //_chartControl1.Legend.MarkerMode = DevExpress.XtraCharts.LegendMarkerMode.CheckBoxAndMarker;
            //_chartControl1.Legend.Name = "Phuoc's Legend";
            ((XYDiagram)_chartControl.Diagram).AxisX.VisualRange.Auto = true;
            ((XYDiagram)_chartControl.Diagram).AxisX.VisualRange.AutoSideMargins = false;
            ((XYDiagram)_chartControl.Diagram).AxisX.VisualRange.SideMarginsValue = 2;
            ((XYDiagram)_chartControl.Diagram).AxisX.Label.Angle = 0;
            ((XYDiagram)_chartControl.Diagram).AxisX.Label.Font = new System.Drawing.Font("Calibri", 12, System.Drawing.FontStyle.Bold);
            ((XYDiagram)_chartControl.Diagram).AxisX.NumericScaleOptions.ScaleMode = DevExpress.XtraCharts.ScaleMode.Automatic;
            //((XYDiagram)_chartControl1.Diagram).AxisY.NumericScaleOptions.ScaleMode = DevExpress.XtraCharts.ScaleMode.Automatic;
            ((XYDiagram)_chartControl.Diagram).AxisY.Label.Font = new System.Drawing.Font("Calibri", 12, System.Drawing.FontStyle.Bold);

            ((XYDiagram)_chartControl.Diagram).AxisX.Title.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ((XYDiagram)_chartControl.Diagram).AxisY.Title.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ((XYDiagram)_chartControl.Diagram).AxisY.Title.TextColor = Color.DarkOrange;
            ((XYDiagram)_chartControl.Diagram).AxisX.Title.TextColor = Color.DarkOrange;
           
            //myAxisY.Title.Text = "Inventory (Prs)";
            //myAxisY.Label.TextColor = Color.Blue;
            //myAxisY.Color = Color.Magenta;
            //this.splitMain.Panel2.Controls.Add(_chartControl);
        }

        private void BindingGauges(DevExpress.XtraGauges.Win.Gauges.Circular.CircularGauge circularGauge, double num, double iMin, double iMax, double iRangeMin, double iRangeMax)
        {
            
            if (circularGauge.Scales.Count <= 0)
            {
                return;
            }
            circularGauge.Scales[0].EnableAnimation = false;
          
            circularGauge.Scales[0].MinValue = (int)iMin;
            circularGauge.Scales[0].MaxValue = (int)iMax;
      
            circularGauge.Scales[0].Value = 0;
           
            if (circularGauge.Scales[0].Ranges.Count >= 3)
            {
                circularGauge.Scales[0].Ranges[0].StartValue = (float)iMin;
                circularGauge.Scales[0].Ranges[0].EndValue = (float)iRangeMin;
                circularGauge.Scales[0].Ranges[1].StartValue = (float)iRangeMin;
                circularGauge.Scales[0].Ranges[1].EndValue = (float)iRangeMax;
                circularGauge.Scales[0].Ranges[2].StartValue = (float)iRangeMax;
                circularGauge.Scales[0].Ranges[2].EndValue = (float)iMax;

            }
         
            try
            {

                circularGauge.Scales[0].MinValue = (int)iMin;
                circularGauge.Scales[0].MaxValue = (int)iMax;
           

                circularGauge.Scales[0].EnableAnimation = true;
                circularGauge.Scales[0].EasingMode = DevExpress.XtraGauges.Core.Model.EasingMode.EaseInOut;
              

                
               circularGauge.Scales[0].Value = (float)num;
                 
               
            }
            catch (Exception ex)
            { }
            // }
        }

        private string getCurrentTime()
        {
            string iTime = "0";
            DataTable dt = null;
            dt = SP_SMT_GET_CURRENT_HH();
            if (dt != null && dt.Rows.Count > 0)
                iTime = dt.Rows[0][0].ToString();
            return iTime;
        }

        private void BindingData()
        {
            try
            {
                string iCurent = getCurrentTime();
                string Now = DateTime.Now.ToString("yyyyMMdd");
                dt = SP_SMT_PROD_DAILY_NEW(Now, sLineCD, sMLineCD);
                int irow = 3;

                if (dt != null && dt.Rows.Count > 0)
                {
                    axfpView.MaxRows = dt.Rows.Count + 2;
                    ClearGrid(axfpView);
                    //AxFPUSpreadADO.AxfpSpread
                    //axfpView.SetCellBorder(1, 1, axfpView.MaxCols, axfpView.MaxRows, AxFPUSpreadADO.CellBorderIndexConstants.CellBorderIndexBottom, 0, AxFPUSpreadADO.CellBorderStyleConstants.CellBorderStyleSolid);
                    //axfpView.SetCellBorder(1, 1, axfpView.MaxCols, axfpView.MaxRows, AxFPUSpreadADO.CellBorderIndexConstants.CellBorderIndexTop, 0, AxFPUSpreadADO.CellBorderStyleConstants.CellBorderStyleSolid);
                    //axfpView.SetCellBorder(1, 1, axfpView.MaxCols, axfpView.MaxRows, AxFPUSpreadADO.CellBorderIndexConstants.CellBorderIndexLeft, 0, AxFPUSpreadADO.CellBorderStyleConstants.CellBorderStyleSolid);
                    //axfpView.SetCellBorder(1, 1, axfpView.MaxCols, axfpView.MaxRows, AxFPUSpreadADO.CellBorderIndexConstants.CellBorderIndexRight, 0, AxFPUSpreadADO.CellBorderStyleConstants.CellBorderStyleSolid);

                    foreach (DataRow dr in dt.Rows)
                    {
                        
                        axfpView.SetText(1, irow, dr["HMS"].ToString());
                        if (dr["UPC_TARGET"].ToString() != "0")
                        {
                            axfpView.SetText(2, irow, dr["UPC_TARGET"].ToString());
                        }
                        else
                        {
                            axfpView.SetText(2, irow, "");
                        }
                        if (dr["UPC_ACT"].ToString() != "0")
                        {
                            axfpView.SetText(3, irow, dr["UPC_ACT"].ToString());
                        }
                        else
                        {
                            axfpView.SetText(3, irow, "");
                        }
                        if (dr["UPC_RATE"].ToString() != "0")
                        {
                            axfpView.SetText(4, irow, dr["UPC_RATE"].ToString());
                            if (irow < Convert.ToInt32(iCurent) + 2)
                            {
                                if (Convert.ToDouble(dr["UPC_RATE"].ToString()) > 98)
                                {
                                    axfpView.Row = irow;
                                    axfpView.Col = 4;
                                    axfpView.BackColor = Color.Green;
                                }
                                else if (Convert.ToDouble(dr["UPC_RATE"].ToString()) > 95)
                                {
                                    axfpView.Row = irow;
                                    axfpView.Col = 4;
                                    axfpView.BackColor = Color.Yellow;
                                }
                                else
                                {
                                    axfpView.Row = irow;
                                    axfpView.Col = 4;
                                    axfpView.BackColor = Color.Red;
                                }
                            }
                        }
                        else
                        {
                            axfpView.SetText(4, irow, "");
                            if (irow < Convert.ToInt32(iCurent) + 2 && dr["UPC_TARGET"].ToString() != "0")
                            {
                                axfpView.Row = irow;
                                axfpView.Col = 4;
                                axfpView.BackColor = Color.Red;
                            }

                        }

                        if (dr["UPS_TARGET1"].ToString() != "0")
                        {
                            axfpView.SetText(5, irow, dr["UPS_TARGET1"].ToString());
                        }
                        else
                        {
                            axfpView.SetText(5, irow, "");
                        }
                        if (dr["UPS_ACT1"].ToString() != "0")
                        {
                            axfpView.SetText(6, irow, dr["UPS_ACT1"].ToString());
                        }
                        else
                        {
                            axfpView.SetText(6, irow, "");
                        }
                        if (dr["UPS_RATE1"].ToString() != "0")
                        {
                            axfpView.SetText(7, irow, dr["UPS_RATE1"].ToString());

                            if (irow < Convert.ToInt32(iCurent) + 2)
                            {
                                if (Convert.ToDouble(dr["UPS_RATE1"].ToString()) > 98)
                                {
                                    axfpView.Row = irow;
                                    axfpView.Col = 7;
                                    axfpView.BackColor = Color.Green;
                                }
                                else if (Convert.ToDouble(dr["UPS_RATE1"].ToString()) > 95)
                                {
                                    axfpView.Row = irow;
                                    axfpView.Col = 7;
                                    axfpView.BackColor = Color.Yellow;
                                }
                                else
                                {
                                    axfpView.Row = irow;
                                    axfpView.Col = 7;
                                    axfpView.BackColor = Color.Red;
                                }
                            }
                        }
                        else
                        {
                            axfpView.SetText(7, irow, "");
                            if (irow < Convert.ToInt32(iCurent) + 2 && dr["UPS_TARGET1"].ToString() != "0")
                            {
                                axfpView.Row = irow;
                                axfpView.Col = 7;
                                axfpView.BackColor = Color.Red;
                            }
                        }


                        if (dr["UPS_TARGET2"].ToString() != "0")
                        {
                            axfpView.SetText(8, irow, dr["UPS_TARGET2"].ToString());
                        }
                        else
                        {
                            axfpView.SetText(8, irow, "");
                        }
                        if (dr["UPS_ACT2"].ToString() != "0")
                        {
                            axfpView.SetText(9, irow, dr["UPS_ACT2"].ToString());
                        }
                        else
                        {
                            axfpView.SetText(9, irow, "");
                        }
                        if (dr["UPS_RATE2"].ToString() != "0")
                        {
                            axfpView.SetText(10, irow, dr["UPS_RATE2"].ToString());

                            if (irow < Convert.ToInt32(iCurent) + 2)
                            {
                                if (Convert.ToDouble(dr["UPS_RATE2"].ToString()) > 98)
                                {
                                    axfpView.Row = irow;
                                    axfpView.Col = 10;
                                    axfpView.BackColor = Color.Green;
                                }
                                else if (Convert.ToDouble(dr["UPS_RATE2"].ToString()) > 95)
                                {
                                    axfpView.Row = irow;
                                    axfpView.Col = 10;
                                    axfpView.BackColor = Color.Yellow;
                                }
                                else
                                {
                                    axfpView.Row = irow;
                                    axfpView.Col = 10;
                                    axfpView.BackColor = Color.Red;
                                }
                            }
                        }
                        else
                        {
                            axfpView.SetText(10, irow, "");
                            if (irow < Convert.ToInt32(iCurent) + 2 && dr["UPS_TARGET2"].ToString() != "0")
                            {
                                axfpView.Row = irow;
                                axfpView.Col = 10;
                                axfpView.BackColor = Color.Red;
                            }
                        }



                        if (dr["FSS_TARGET"].ToString() != "0")
                        {
                            axfpView.SetText(11, irow, dr["FSS_TARGET"].ToString());
                        }
                        else
                        {
                            axfpView.SetText(11, irow, "");
                        }
                        if (dr["FSS_ACT"].ToString() != "0")
                        {
                            axfpView.SetText(12, irow, dr["FSS_ACT"].ToString());
                        }
                        else
                        {
                            axfpView.SetText(12, irow, "");
                        }
                        if (dr["FSS_RATE"].ToString() != "0")
                        {
                            axfpView.SetText(13, irow, dr["FSS_RATE"].ToString());
                            if (irow < Convert.ToInt32(iCurent) + 2)
                            {
                                if (Convert.ToDouble(dr["FSS_RATE"].ToString()) > 98)
                                {
                                    axfpView.Row = irow;
                                    axfpView.Col = 13;
                                    axfpView.BackColor = Color.Green;
                                }
                                else if (Convert.ToDouble(dr["FSS_RATE"].ToString()) > 95)
                                {
                                    axfpView.Row = irow;
                                    axfpView.Col = 13;
                                    axfpView.BackColor = Color.Yellow;
                                }
                                else
                                {
                                    axfpView.Row = irow;
                                    axfpView.Col = 13;
                                    axfpView.BackColor = Color.Red;
                                }
                            }
                        }
                        else
                        {
                            axfpView.SetText(13, irow, "");
                            if (irow < Convert.ToInt32(iCurent) + 2 && dr["FSS_TARGET"].ToString() != "0")
                            {
                                axfpView.Row = irow;
                                axfpView.Col = 13;
                                axfpView.BackColor = Color.Red;
                            }
                        }
                        if (dr["FGA_TARGET"].ToString() != "0")
                        {
                            axfpView.SetText(14, irow, dr["FGA_TARGET"].ToString());
                        }
                        else
                        {
                            axfpView.SetText(14, irow, "");
                        }
                        if (dr["FGA_ACT"].ToString() != "0")
                        {
                            axfpView.SetText(15, irow, dr["FGA_ACT"].ToString());
                        }
                        else
                        {
                            axfpView.SetText(15, irow, "");
                        }
                        if (dr["FGA_RATE"].ToString() != "0")
                        {
                            axfpView.SetText(16, irow, dr["FGA_RATE"].ToString());
                            if (irow < Convert.ToInt32(iCurent) + 2)
                            {
                                if (Convert.ToDouble(dr["FGA_RATE"].ToString()) > 98)
                                {
                                    axfpView.Row = irow;
                                    axfpView.Col = 16;
                                    axfpView.BackColor = Color.Green;
                                }
                                else if (Convert.ToDouble(dr["FGA_RATE"].ToString()) > 95)
                                {
                                    axfpView.Row = irow;
                                    axfpView.Col = 16;
                                    axfpView.BackColor = Color.Yellow;
                                } 
                                else
                                {
                                    axfpView.Row = irow;
                                    axfpView.Col = 16;
                                    axfpView.BackColor = Color.Red;
                                }
                            }
                        }
                        else
                        {
                            axfpView.SetText(16, irow, "");
                            if (irow < Convert.ToInt32(iCurent) + 2 && dr["FGA_TARGET"].ToString() != "0")
                            {
                                axfpView.Row = irow;
                                axfpView.Col = 16;
                                axfpView.BackColor = Color.Red;
                            }
                        }


                        if (dr["HMS"].ToString() == "TOTAL")
                        {
                            for (int i = 0; i < dt.Columns.Count-1; i++)
                            {
                                axfpView.Row = irow;
                                axfpView.Col = i + 1;
                                axfpView.BackColor = Color.Orange;

                            }

                        }
                        irow++;
                    }
                    
                    if (iCurent != "0")
                    {
                        
                        for (int i = 0; i < dt.Columns.Count-1; i++)
                        {
                            axfpView.Col = i + 1;
                            axfpView.Row = Convert.ToInt32(iCurent) + 2;
                            axfpView.BackColor = Color.Salmon;
                        }


                    }
                }
                
                
            }
            catch (Exception ex)
            {
               // MessageBox.Show(ex.ToString());
            }
        }


        private void FORM_SMT_PROD_DAILY_Load(object sender, EventArgs e)
        {
            try
            {
                this.Name = "FORM_SMT_PROD_DAILY_" + sMLineCD;
                this.Text = "FORM_SMT_PROD_DAILY_" + sMLineCD;
                //MessageBox.Show(this.Name.ToString());
            }
            catch (Exception ex)
            {
            }
            lblDate.Text = string.Format(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            btnDaily.ForeColor = Color.FromArgb(32, 31, 53);
            btnDaily.Enabled = false;
            tmr_Date.Stop();

            if (this.sMLineCD == "001")
            {
                sStitching1TitleEn = "Stitching 1( Start 07:30)";
                sStitching2TitleEn = "Stitching 2( Start 07:30)";
                sStitching1TitleVn = "May 1";
                sStitching1TitleVn = "May 2";
            }
            if (this.sMLineCD == "002")
            {
                sStitching1TitleEn = "Stitching 3( Start 07:30)";
                sStitching2TitleEn = "Stitching 4( Start 07:30)";
                sStitching1TitleVn = "May 3( Từ 07:30)";
                sStitching2TitleVn = "May 4( Từ 07:30)";
            }
            else if (this.sMLineCD == "003")
            {
                sStitching1TitleEn = "Stitching 5( Start 07:30)";
                sStitching2TitleEn = "Stitching 6( Start 07:30)";
                sStitching1TitleVn = "May 5( Từ 07:30)";
                sStitching1TitleVn = "May 6( Từ 07:30)";
            }
            else if (this.sMLineCD == "004")
            {
                sStitching1TitleEn = "Stitching 7( Start 07:30)";
                sStitching2TitleEn = "Stitching 8( Start 07:30)";
                sStitching1TitleVn = "May 7( Từ 07:30)";
                sStitching1TitleVn = "May 8( Từ 07:30)";
            }


            //ClassLib.ComCtl.Form_Maximized(this, indexScreen); //2 man hinh tro len
            GoFullscreen(true);
            //BindingData();
           // showAnimation(axfpView);
            axfpView.Hide();
           // btnMonthly.Enabled = true;
           // CreateChart();
            //btnDaily.Text = "Daily" + "\n" + "Ngày";
            //btnWeekly.Text = "Weekly" + "\n" + "Tuần";
            //btnMonthly.Text = "Monthly" + "\n" + "Tháng";
            //btnYearly.Text = "Yearly" + "\n" + "Năm";

            Init_Lang();
           
        }
        private void Init_Lang()
        {
            axfpView.SetText(1, 1, sTimeTitle);
            axfpView.SetText(2, 1, sCuttingTitle);
            axfpView.SetText(5, 1, sStitching1Title);
            axfpView.SetText(8, 1, sStitching2Title);
            axfpView.SetText(11, 1, sStockfitTitle);
            axfpView.SetText(14, 1, sAssemblyTitle);
            axfpView.SetText(2, 2, sTarget);
            axfpView.SetText(3, 2, sActual);
            axfpView.SetText(5, 2, sTarget);
            axfpView.SetText(6, 2, sActual);
            axfpView.SetText(8, 2, sTarget);
            axfpView.SetText(9, 2, sActual);
            axfpView.SetText(11, 2, sTarget);
            axfpView.SetText(12, 2, sActual);
            axfpView.SetText(14, 2, sTarget);
            axfpView.SetText(15, 2, sActual);
        }
              

        

        private void tmr_Date_Tick(object sender, EventArgs e)
        {
            lblDate.Text = string.Format(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            CreLoad++;
            if (CreLoad == 20)
            {
                //BindingData();
                
                
                //BindingData();
                showAnimation(axfpView);
                CreateChart();
              
                CreLoad = 0;
            }
        }

      

        private void bgwDoing_DoWork(object sender, DoWorkEventArgs e)
        {
          
           
            if (chartControl1.InvokeRequired)
            {
                chartControl1.BeginInvoke((MethodInvoker)delegate
                {
                    CreateChart();
                    showAnimation(axfpView);
                });

            }
           
          
        }

       

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void FORM_SMT_PROD_DAILY_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                lblDate.Text = string.Format(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                sLineCD = strinit.line;
                sMLineCD = strinit.mline;
                Lang = strinit.lang;
                CreLoad = 19;
                tmr_Date.Start();

                if (this.sMLineCD == "001")
                {
                    sStitching1TitleEn = "Stitching 1(Start 07:30)";
                    sStitching2TitleEn = "Stitching 2(Start 07:30)";
                    sStitching1TitleVn = "May 1(Từ 07:30)";
                    sStitching1TitleVn = "May 2(Từ 07:30)";
                }
                if (this.sMLineCD == "002")
                {
                    sStitching1TitleEn = "Stitching 3(Start 07:30)";
                    sStitching2TitleEn = "Stitching 4(Start 07:30)";
                    sStitching1TitleVn = "May 3(Từ 07:30)";
                    sStitching2TitleVn = "May 4(Từ 07:30)";
                }
                else if (this.sMLineCD == "003")
                {
                    sStitching1TitleEn = "Stitching 5(Start 07:30)";
                    sStitching2TitleEn = "Stitching 6(Start 07:30)";
                    sStitching1TitleVn = "May 5(Từ 07:30)";
                    sStitching1TitleVn = "May 6(Từ 07:30)";
                }
                else if (this.sMLineCD == "004")
                {
                    sStitching1TitleEn = "Stitching 7(Start 07:30)";
                    sStitching2TitleEn = "Stitching 8(Start 07:30)";
                    sStitching1TitleVn = "May 7(Từ 07:30)";
                    sStitching1TitleVn = "May 8(Từ 07:30)";
                }

                switch (Lang)
                {
                    case "Vn":
                        btnDaily.Text = "Ngày";
                        btnMonthly.Text = "Tháng";
                        btnWeekly.Text = "Tuần";
                        btnYearly.Text = "Năm";
                        sTimeTitle = "Giờ";
                        sCuttingTitle = "Cắt(Từ 07:30)";
                        sStitching1Title = sStitching1TitleVn;
                        sStitching2Title = sStitching2TitleVn;
                        sStockfitTitle = "Chuẩn bị(Từ 07:30)";
                        sAssemblyTitle = "Lắp ráp(Từ 08:00)";
                        sTarget = "Mục tiêu";
                        sActual = "Thực tế";
                        sProducQty = "Sản xuất (Prs)";
                        sHour = "Giờ";
                        sChartTarget = "Mục tiêu";
                        sChartActual = "Thực tế";
                        sAssemblyProduct = "Sản xuất lắp ráp";
                        sRate = "Tỷ lệ";
                        break;
                    case "En":
                        btnDaily.Text = "Day";
                        btnMonthly.Text = "Month";
                        btnWeekly.Text = "Week";
                        btnYearly.Text = "Year";

                        sTimeTitle = "Time";
                        sCuttingTitle = "Cutting(Start 07:30)";
                        sStitching1Title = sStitching1TitleEn;
                        sStitching2Title = sStitching2TitleEn;
                        sStockfitTitle = "Stockfit(Start 07:30)";
                        sAssemblyTitle = "Assembly(Start 08:00)";
                        sTarget = "Target";
                        sActual = "Actual";
                        sProducQty = "Product Qty(Prs)";
                        sHour = "Hour";
                        sChartTarget = "Target Qty";
                        sChartActual = "Actual Qty";
                        sAssemblyProduct = "Assembly Production";
                        sRate = "Rate";
                        break;

                }

            }
            else
            {
                tmr_Date.Stop();
            }
        }

        private void btnWeekly_Click(object sender, EventArgs e)
        {


            
            string Caption = "Production Status by Week";
           


            switch (Lang)
            {
                case "Vn":
                    Caption = "Tình trạng sản xuất theo Tuần";
                    break;
                default:
                    Caption = "Production Status by Week";
                    break;
            }
            Form fc = Application.OpenForms["FRM_SMT_WEEKLY_PRODUCTION_STATUS"];
            if (fc != null)
            {
                //fc.Close();
                fc.Show();
                ////f.TopMost = true;
            }
            else
            {
                FRM_SMT_WEEKLY_PRODUCTION_STATUS f = new FRM_SMT_WEEKLY_PRODUCTION_STATUS(Caption, 1, sLineCD, sMLineCD, Lang);
                f.Show();
                //f.TopMost = true;
            }
            tmr_Date.Stop();
            this.Hide();
        }

        private void btnMonthly_Click(object sender, EventArgs e)
        {
            
            string Caption = "Production Status by Month";
            

            switch (Lang)
            {
                case "Vn":
                    Caption = "Tình trạng sản xuất theo Tháng";
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
                FRM_SMT_MON_PROD_STATUS f = new FRM_SMT_MON_PROD_STATUS(Caption, 1, sLineCD, sMLineCD, Lang);
                f.Show();
                //f.TopMost = true;
            }
            tmr_Date.Stop();
            this.Hide();
        }

        private void btnYearly_Click(object sender, EventArgs e)
        {
            
            string Caption = "Production Status by Year";
            

            switch (Lang)
            {
                case "Vn":
                    Caption = "Tình trạng sản xuất theo Năm";
                    break;
                default:
                    Caption = "Production Status by Year";
                    break;
            }
            Form fc = Application.OpenForms["FRM_SMT_YEAR_PROD_STATUS"];
            if (fc != null)
            {
                //fc.Close();
                fc.Show();
                ////f.TopMost = true;
            }
            else
            {
                FRM_SMT_YEAR_PROD_STATUS f = new FRM_SMT_YEAR_PROD_STATUS(Caption, 1, sLineCD, sMLineCD, Lang);
                f.Show();
                //f.TopMost = true;
            }
            tmr_Date.Stop();
            this.Hide();

         
        }




    }
}
