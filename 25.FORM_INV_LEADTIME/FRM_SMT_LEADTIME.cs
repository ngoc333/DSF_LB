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


namespace FORM
{
    public partial class FRM_SMT_LEADTIME : Form
    {
        public FRM_SMT_LEADTIME()
        {
            InitializeComponent();
        }
          int indexScreen;
       //   string _wh_cd="014", _mline_cd,Lang;
          init strinit = new init();
          bool _load = false;
          string Lang;

          string _Line = ComVar.Var._strValue1, _Mline = ComVar.Var._strValue2;
          Dictionary<string, string> _dtnInit = new Dictionary<string, string>();


          public FRM_SMT_LEADTIME(string Title, int _indexScreen, string wh_cd, string mline_cd,string _Lang)
        {
            InitializeComponent();
            indexScreen = _indexScreen;
            _Line = wh_cd;
            _Mline = mline_cd;
            Lang = _Lang;
            load_Data();
            tmr_Time.Stop();
            lblTitle.Text = Title;
            this.Text = "FRM_SMT_LEADTIME";


        }
        int _reload_data = 0;
        bool _loadpic = false;


        private void GoFullscreen()
        {
            this.WindowState = FormWindowState.Normal;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Bounds = Screen.PrimaryScreen.Bounds;

        }

        public void setData(string Title, int _indexScreen, string wh_cd, string mline_cd, string _Lang)
        {
            indexScreen = _indexScreen;
            _Line = wh_cd;
            _Mline = mline_cd;
            lblTitle.Text = Title;

            switch (Lang)
            {
                case "Vn":
                    btnDay.Text = "Ngày";
                    btnMonth.Text = "Tháng";
                    btnWeek.Text = "Tuần";
                    btnYear.Text = "Năm";
                    break;
                case "En":
                    btnDay.Text = "Day";
                    btnMonth.Text = "Month";
                    btnWeek.Text = "Week";
                    btnYear.Text = "Year";
                    break;
            }
        }

        #region Method
        private void load_Data()
        {
            try
            {
                DataTable dt = LOAD_DATA();
                if (dt == null || dt.Rows.Count == 0) return;
                Control cntrl;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    cntrl = this.Controls.Find(dt.Rows[i]["CON_NM"].ToString(), true).FirstOrDefault();
                    
                    if (cntrl != null)
                    {
                        cntrl.Text = dt.Rows[i]["CON_VAL"].ToString();
                        switch (dt.Rows[i]["COLOR"].ToString())
                        {
                            case "RED":
                                cntrl.ForeColor = Color.Red;
                                break;
                            case "BLACK":
                                cntrl.ForeColor = Color.Black;
                                break;
                            case "YELLOW":
                                cntrl.ForeColor = Color.Yellow;
                                break;
                            default:
                                cntrl.ForeColor = Color.Black;
                                break;
                        } 
                    }
                }
            }
            catch 
            {}

            try
            {
                DataTable dt = LOAD_DATA("");
                pnMain.Visible = true;
                if (dt == null || dt.Rows.Count == 0) return;
                Create_chart(Dailychart, dt, "");
                load_data_grid(dt);
            }
            catch
            { }
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

        private void Create_chart(ChartControl arg_chart, DataTable arg_dt, string arg_name)
        {
            //Reset Chart beforce biding Data
            arg_chart.Series.Clear();
            arg_chart.Titles.Clear();
            // Create an empty chart. (No need).
            arg_chart.AppearanceNameSerializable = "Slipstream";
            //create New object
            DevExpress.XtraCharts.LineSeriesView lineSeriesView1 = new DevExpress.XtraCharts.LineSeriesView();
            DevExpress.XtraCharts.XYSeriesUnwindAnimation xySeriesUnwindAnimation1 = new DevExpress.XtraCharts.XYSeriesUnwindAnimation();
            DevExpress.XtraCharts.PowerEasingFunction powerEasingFunction1 = new DevExpress.XtraCharts.PowerEasingFunction();

            // Create the first side-by-side bar series and add points to it.
            Series series1 = new Series("Lead Time", ViewType.Bar);
            // Create the second side-by-side bar series and add points to it.
            Series series2 = new Series("Inventory", ViewType.Line);
            int iCount = 0, iShow = 0; ;
            for (int i = 0; i < arg_dt.Rows.Count; i++)
            {
                if (arg_dt.Rows[i]["STYLE_CD"].ToString().ToUpper() != "TOTAL")
                {
                    iShow++;
                    if (iShow == 3)
                    {
                        series1.Points.Add(new SeriesPoint(arg_dt.Rows[i]["SUM_GRP_DETAIL"].ToString() + "\n" + arg_dt.Rows[i]["STYLE_CD"].ToString()
                            , arg_dt.Rows[i]["LT"] == null || arg_dt.Rows[i]["LT"].ToString() == "" ? 0 : arg_dt.Rows[i]["LT"]));

                        series2.Points.Add(new SeriesPoint(arg_dt.Rows[i]["SUM_GRP_DETAIL"].ToString() + "\n" + arg_dt.Rows[i]["STYLE_CD"].ToString()
                            , arg_dt.Rows[i]["INV"] == null || arg_dt.Rows[i]["INV"].ToString() == "" ? 0 : arg_dt.Rows[i]["INV"]));
                    }
                    else
                    {
                        series1.Points.Add(new SeriesPoint(arg_dt.Rows[i]["SUM_GRP_DETAIL"].ToString() + "\n" + addBlank(iCount)
                            , arg_dt.Rows[i]["LT"] == null || arg_dt.Rows[i]["LT"].ToString() == "" ? 0 : arg_dt.Rows[i]["LT"]));

                        series2.Points.Add(new SeriesPoint(arg_dt.Rows[i]["SUM_GRP_DETAIL"].ToString() + "\n" + addBlank(iCount)
                            , arg_dt.Rows[i]["INV"] == null || arg_dt.Rows[i]["INV"].ToString() == "" ? 0 : arg_dt.Rows[i]["INV"]));
                    }


                    if (i + 1 < arg_dt.Rows.Count && 
                        arg_dt.Rows[i + 1]["STYLE_CD"].ToString().ToUpper() != "TOTAL" &&
                        arg_dt.Rows[i]["STYLE_CD"].ToString() != arg_dt.Rows[i + 1]["STYLE_CD"].ToString())
                    {
                        iCount++;
                        iShow = 0;
                        series1.Points.Add(new SeriesPoint(addBlank(iCount)));

                        series2.Points.Add(new SeriesPoint(addBlank(iCount)));
                    }
                }
           }
            
            //marker
            lineSeriesView1.LineMarkerOptions.Color = System.Drawing.Color.Gold;
            
            lineSeriesView1.MarkerVisibility = DevExpress.Utils.DefaultBoolean.True;

            
            series2.View = lineSeriesView1;
            // Add the series to the chart.
            series1.ArgumentScaleType = ScaleType.Qualitative;
            series2.ArgumentScaleType = ScaleType.Qualitative;

            
            
            arg_chart.SeriesSerializable = new Series[] { series1, series2 };


            //arg_chart.Series.Add(series1);
            //arg_chart.Series.Add(series2);
            // Create two secondary axes, and add them to the chart's Diagram.
            SecondaryAxisY axisYSecond = new SecondaryAxisY("my Y-Axis");
            ((XYDiagram)arg_chart.Diagram).SecondaryAxesY.Clear();
            ((XYDiagram)arg_chart.Diagram).SecondaryAxesY.Add(axisYSecond);
            axisYSecond.Label.TextPattern = "{V:#,#}";
            axisYSecond.Title.TextColor = Color.Orange;
            axisYSecond.Title.Font = new Font("Tahoma", 16F, FontStyle.Bold);
            axisYSecond.Title.Text = "Inventory";
            axisYSecond.Title.Visibility = DevExpress.Utils.DefaultBoolean.Default;
            axisYSecond.Label.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);

            ((LineSeriesView)series2.View).AxisY = axisYSecond;

            // Hide the legend (if necessary).
            arg_chart.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;
            arg_chart.Legend.AlignmentVertical = LegendAlignmentVertical.TopOutside;
            arg_chart.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Center; 
            arg_chart.Legend.Direction = LegendDirection.LeftToRight;

            // Rotate the diagram (if necessary).
            ((XYDiagram)arg_chart.Diagram).Rotated = false;

            //ScaleBreak NUmber
            ((XYDiagram)arg_chart.Diagram).AxisX.NumericScaleOptions.AutoGrid = false;

            //Title
            ((XYDiagram)arg_chart.Diagram).AxisX.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
            ((XYDiagram)arg_chart.Diagram).AxisX.Title.Text = "";
            if (iCount>2)
                ((XYDiagram)arg_chart.Diagram).AxisX.Label.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            else
                ((XYDiagram)arg_chart.Diagram).AxisX.Label.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);

            ((XYDiagram)arg_chart.Diagram).AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
            ((XYDiagram)arg_chart.Diagram).AxisY.Title.TextColor = Color.Orange;
            ((XYDiagram)arg_chart.Diagram).AxisY.Title.Text = "Lead Time";
            ((XYDiagram)arg_chart.Diagram).AxisY.Title.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            ((XYDiagram)arg_chart.Diagram).AxisY.Label.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);


            //((XYDiagram)arg_chart.Diagram).AxisY.WholeRange.Auto = true;
            // ((XYDiagram)arg_chart.Diagram).AxisY.WholeRange.SetMinMaxValues(0, 2);


            //Animation Series
            lineSeriesView1.Color = System.Drawing.Color.DarkOrange;
            xySeriesUnwindAnimation1.EasingFunction = powerEasingFunction1;
            lineSeriesView1.SeriesAnimation = xySeriesUnwindAnimation1;



            ((XYDiagram)arg_chart.Diagram).AxisY.Label.TextPattern = "{V:#,0.##}";
            //Label
            series1.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;
            series1.Label.TextPattern = "{V:#,#}";
            (series1.Label as SideBySideBarSeriesLabel).Position = DevExpress.XtraCharts.BarSeriesLabelPosition.Top;
            series2.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;
            series2.Label.TextPattern = "{V:#,#}";
            // Add a title to the chart (if necessary).
            ChartTitle chartTitle1 = new ChartTitle();
            chartTitle1.Font = new System.Drawing.Font("Calibri", 20F, System.Drawing.FontStyle.Bold);
            chartTitle1.Text = arg_dt.Rows[0]["CHART_TITLE"].ToString();
            arg_chart.Titles.Add(chartTitle1);

            // Add the chart to the form.


            pnMain.Controls.Add(arg_chart);
        }

        private void load_data_grid(DataTable arg_dt)
        {
            try
            {
                if (arg_dt != null && arg_dt.Rows.Count > 0)
                {
                    int iCount = arg_dt.Rows.Count;
                    //axGrid.MaxCols = iCount + 2;
                    //axGrid.SetText(1, 1, arg_dt.Rows[0]["MON"].ToString());
                    //double dColWidth = Convert.ToDouble(arg_dt.Rows[0]["col_width"]);
                    int iCol = 3, iRow = 3;
                    double dColWidth = Convert.ToDouble(arg_dt.Rows[0]["col_width"]);
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

                        axGrid.SetText(1, iRow, arg_dt.Rows[i]["MODEL_NAME"].ToString());
                        axGrid.SetText(2, iRow, arg_dt.Rows[i]["STYLE_CD"].ToString());

                        axGrid.SetText(iCol, 1, arg_dt.Rows[i]["HEADER_1"].ToString());
                        axGrid.SetText(iCol + 1, 1, arg_dt.Rows[i]["HEADER_1"].ToString());
                        axGrid.SetText(iCol, 2, arg_dt.Rows[i]["HEADER_21"].ToString());
                        axGrid.SetText(iCol + 1, 2, arg_dt.Rows[i]["HEADER_22"].ToString());

                        axGrid.SetText(iCol, iRow, arg_dt.Rows[i]["INV"].ToString());
                        axGrid.SetText(iCol + 1, iRow, FormatData(arg_dt.Rows[i]["LT"].ToString()));

                        if (arg_dt.Rows[i]["MODEL_NAME"].ToString() == arg_dt.Rows[i]["STYLE_CD"].ToString())
                        {
                            axGrid.AddCellSpan(1, iRow, 2, 1);
                            axGrid.Col = -1;
                            axGrid.Row = iRow;
                            axGrid.FontSize = 20;
                            axGrid.BackColor = Color.PeachPuff;
                            axGrid.Col = 1;
                            axGrid.TypeHAlign = FPUSpreadADO.TypeHAlignConstants.TypeHAlignCenter;
                        }
                        if (i + 1 < iCount && arg_dt.Rows[i]["STYLE_CD"].ToString() != arg_dt.Rows[i + 1]["STYLE_CD"].ToString())
                        {
                            iRow++;
                            iCol = 3;
                        }
                        else iCol += 2;
                    }

                    for (int i = 3; i < iCol; i++)
                    {
                        axGrid.set_ColWidth(i, dColWidth);
                    }


                    axGrid.MaxCols = iCol - 1;
                    axGrid.MaxRows = iRow;
                    axGrid.SetOddEvenRowColor(0xffffff, 0, 0xf7f6e8, 0);
                    axGrid.SetCellBorder(1, 3, iCol - 1, iRow
                                , FPUSpreadADO.CellBorderIndexConstants.CellBorderIndexLeft, 0
                                , FPUSpreadADO.CellBorderStyleConstants.CellBorderStyleSolid);
                    axGrid.SetCellBorder(1, 2, iCol - 1, iRow
                                , FPUSpreadADO.CellBorderIndexConstants.CellBorderIndexBottom, 0
                                , FPUSpreadADO.CellBorderStyleConstants.CellBorderStyleSolid);

                    axGrid.Col = 1;
                    axGrid.ColMerge = FPUSpreadADO.MergeConstants.MergeAlways;
                    axGrid.Row = 1;
                    axGrid.RowMerge = FPUSpreadADO.MergeConstants.MergeAlways;


                }
                axGrid.TopRow = 1;

            }
            catch (Exception)
            {
            }

        }

        private void HidePic()
        {
            PicL1_1.Image = null;
            PicL2_1.Image = null;
            PicL3_1.Image = null;
            PicL4_1.Image = null;
            //this.Controls.f
        }

        //private void initForm()
        //{
        //    switch (_Mline)
        //    {
        //        case "001":
        //            ShowGp(gpL1);
        //            ShowPic(PicL1_1);
        //            break;
        //        case "002":
        //            ShowGp(gpL2);
        //            ShowPic(PicL2_1);
        //            break;
        //        case "003":
        //            ShowGp(gpL3);
        //            ShowPic(PicL3_1);
        //            break;
        //        case "004":
        //            ShowGp(gpL4);
        //            ShowPic(PicL4_1);
        //            break;
        //    }
        //}

        private void ShowGp(GroupBox arg_gp)
        {
            gpL1.Visible = false;
            gpL2.Visible = false;
            gpL3.Visible = false;
            gpL4.Visible = false;

            arg_gp.Location = new Point(135, -2);
            arg_gp.Visible = false;
            
        }

        private void ShowPic(PictureBox arg_pic)
        {
            //PicL1_1.Image = null;
            //PicL2_1.Image = null;
            //PicL3_1.Image = null;
            //PicL4_1.Image = null;

            //arg_pic.Image = Smart_FTY.Properties.Resources.c49a207e0f89c9290d98fd43a87a8cb0;
       //     arg_pic.Image = FORM.Properties.Resources.truck;
            //this.Controls.f
        }

        //private void Line1_Click(
        #endregion Method

        #region DB
        private DataTable LOAD_DATA()
        {
            try
            {
                COM.OraDB MyOraDB = new COM.OraDB();
                System.Data.DataSet ds_ret;

                string process_name = "MES.PKG_SMT_PROD_SHOW.SEL_LEAD_TIME";
                //ARGMODE
                MyOraDB.ReDim_Parameter(3);
                MyOraDB.Process_Name = process_name;
                MyOraDB.Parameter_Name[0] = "ARG_WH_CD";
                MyOraDB.Parameter_Name[1] = "ARG_MLINE_CD";
                MyOraDB.Parameter_Name[2] = "OUT_CURSOR";

                MyOraDB.Parameter_Type[0] = (char)OracleType.VarChar;
                MyOraDB.Parameter_Type[1] = (char)OracleType.VarChar;
                MyOraDB.Parameter_Type[2] = (char)OracleType.Cursor;

                MyOraDB.Parameter_Values[0] = _Line;
                MyOraDB.Parameter_Values[1] = _Mline;
                MyOraDB.Parameter_Values[2] = "";

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


        private DataTable LOAD_DATA(string arg_cmd)
        {
            try
            {
                COM.OraDB MyOraDB = new COM.OraDB();
                System.Data.DataSet ds_ret;

                string process_name = "MES.PKG_SMT_PROD_SHOW.SEL_LEAD_TIME_DETAIL";
                //ARGMODE
                MyOraDB.ReDim_Parameter(4);
                MyOraDB.Process_Name = process_name;
                MyOraDB.Parameter_Name[0] = "ARG_WH_CD";
                MyOraDB.Parameter_Name[1] = "ARG_MLINE_CD";
                MyOraDB.Parameter_Name[2] = "OUT_CURSOR";
                MyOraDB.Parameter_Name[3] = "ARG_CMD";

                MyOraDB.Parameter_Type[0] = (char)OracleType.VarChar;
                MyOraDB.Parameter_Type[1] = (char)OracleType.VarChar;
                MyOraDB.Parameter_Type[2] = (char)OracleType.Cursor;
                MyOraDB.Parameter_Type[3] = (char)OracleType.VarChar;

                MyOraDB.Parameter_Values[0] = _Line;
                MyOraDB.Parameter_Values[1] = _Mline;
                MyOraDB.Parameter_Values[2] = "";
                MyOraDB.Parameter_Values[3] = arg_cmd;


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
        #endregion DB

        #region Event

        private void FRM_SMT_LEADTIME_Load(object sender, EventArgs e)
        {
            GoFullscreen();
            lblDate.Text = string.Format(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            _dtnInit = ComVar.Func.getInitForm(this.GetType().Assembly.GetName().Name, this.GetType().Name);

            //Setup Lang
            //switch (Lang)
            //{
            //    case "Vn":
            //        btnDay.Text = "Ngày";
            //        btnMonth.Text = "Tháng";
            //        btnWeek.Text = "Tuần";
            //        btnYear.Text = "Năm";
            //        break;
            //    case "En":
            //        btnDay.Text = "Day";
            //        btnMonth.Text = "Month";
            //        btnWeek.Text = "Week";
            //        btnYear.Text = "Year";
            //        break;
            //}
            //  pnBot.Location = new Point(138, 881);
           // ClassLib.ComCtl.Form_Maximized(this, indexScreen); //2 man hinh tro len
           // var dateStr = "20110321";
           // DateTime dateTime = DateTime.ParseExact("20110321", "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
           // groupBox1.Click += new EventHandler(Line1_Click);
        }

        //private void Line1_Click(object sendder, EventArgs e)
        //{
        //   FRM_SMT_LEADTIME_DETAIL frm = new  FRM_SMT_LEADTIME_DETAIL("L/T Inventory Tracking", 1, "014", "001");
        //   frm.ShowDialog();
            
        //}

        private void FRM_SMT_LEADTIME_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                _Line = ComVar.Var._strValue1;
                _Mline = ComVar.Var._strValue2;
                Lang = strinit.lang;
               // lblMline.Text = _mline_cd.Substring(2,1);
               // initForm();
                if (_load)
                    _reload_data = 39;
                else
                    _reload_data = 0;
                _load = true;
                tmr_Time.Enabled = true;
            }
            else
            {
                tmr_Time.Enabled = false;
               // HidePic();
            }
        }

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
            Pen pen = new Pen(Color.FromArgb(255, 0, 0, 0), 5);
            pen.StartCap = LineCap.ArrowAnchor;
            pen.EndCap = LineCap.NoAnchor;
            e.Graphics.DrawLine(pen, line.EndPoint, line.StartPoint);
        }

        private void lblDate_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        #endregion Event

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void gpL1_Enter(object sender, EventArgs e)
        {

        }

        //private void btnMonth_Click(object sender, EventArgs e)
        //{
        //    this.Hide();
        //    string Caption = "DTD (Dock To Dock) by Month";
        //    FRM_SMT_DTD_MONTH fc = (FRM_SMT_DTD_MONTH)Application.OpenForms["FRM_SMT_DTD_MONTH"];
        //    if (fc != null)
        //    {
        //        fc.setData(Caption, 1, _wh_cd, _mline_cd, Lang);
        //        fc.Show();
        //        ////f.TopMost = true;

        //    }
        //    else
        //    {
        //        switch (Lang)
        //        {
        //            case "Vn":
        //                Caption = "DTD (Dock To Dock) by Month";
        //                break;
        //            default:
        //                Caption = "DTD (Dock To Dock) by Month";
        //                break;
        //        }
        //        FRM_SMT_DTD_MONTH f = new FRM_SMT_DTD_MONTH(Caption, 1, _wh_cd, _mline_cd, Lang);

        //        f.Show();
        //        //f.TopMost = true;
        //    }
        //}

        //private void btnYear_Click(object sender, EventArgs e)
        //{
        //    this.Hide();
        //    string Caption = "DTD (Dock To Dock) by Year";
        //    FRM_SMT_DTD_YEAR fc = (FRM_SMT_DTD_YEAR)Application.OpenForms["FRM_SMT_DTD_YEAR"];
        //    if (fc != null)
        //    {
        //        fc.setData(Caption, 1, _wh_cd, _mline_cd, Lang);
        //        fc.Show();
        //        ////f.TopMost = true;
        //    }
        //    else
        //    {

        //        switch (Lang)
        //        {
        //            case "Vn":
        //                Caption = "DTD (Dock To Dock) by Year";
        //                break;
        //            default:
        //                Caption = "DTD (Dock To Dock) by Year";
        //                break;
        //        }
        //        FRM_SMT_DTD_YEAR f = new FRM_SMT_DTD_YEAR(Caption, 1, _wh_cd, _mline_cd, Lang);
                
        //        f.Show();
        //        //f.TopMost = true;
        //    }
        //}

        private void axGrid_Advance(object sender, AxFPUSpreadADO._DSpreadEvents_AdvanceEvent e)
        {

        }
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

        private void cmdBack_Click(object sender, EventArgs e)
        {
            ComVar.Var.callForm = _dtnInit["frmHome"];

        }
    }
}
