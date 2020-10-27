using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OracleClient;
using System.Runtime.InteropServices;
using DevExpress.XtraCharts;

namespace FORM
{
    public partial class FRM_SMT_OSD_DAILY_PHUOC : Form
    {
        public FRM_SMT_OSD_DAILY_PHUOC()
        {
            InitializeComponent();
            tmrDate.Stop();
        }

        const int AW_SLIDE = 0X40000;
        const int AW_HOR_POSITIVE = 0X4;
        const int AW_HOR_NEGATIVE = 0X2;
        const int AW_BLEND = 0X80000;
        const int AW_HIDE = 0x00010000;
        init strinit = new init();
        [DllImport("user32")]
        static extern bool AnimateWindow(IntPtr hwnd, int time, int flags);

        int indexScreen;
        string line, Mline,Lang;
        public FRM_SMT_OSD_DAILY_PHUOC(string Title, int _indexScreen, string _Line, string _Mline,string _Lang)
        {
            InitializeComponent();
            indexScreen = _indexScreen;
            Mline = _Mline;
            line = _Line;
            Lang = _Lang;
            //  arrForm[0] = new FORM_PRODUCTIONTIVITY_DAILY("Daily Productivity Status", 1, _Line, _Mline); //ngoc 
            //  arrForm[1] = new FORM_PRODUCTIONTIVITY_WEEKLY("Weekly Productivity Status", 1, _Line, _Mline); //Lenl
            simpleButton3.Visible = false;
            tmrDate.Stop();
            //arrForm[2] = new FRM_SMT_MON_PROD_STATS("Monthly Productivity Status", 1, _Line, _Mline); //Lenl
            lblTitle.Text = Title;
        }


        public DataTable SP_SMT_OSD_DAILY(string ARG_COMP_NAME, string ARG_LINE_CD, string ARG_MLINE_CD, string ARG_MONTH)
        {
            COM.OraDB MyOraDB = new COM.OraDB();
            DataSet ds_ret;
            try
            {
                string process_name = "MES.PKG_SMT_PHUOC.SP_SMT_OSD_DAILY_DIV_V2";

                MyOraDB.ReDim_Parameter(5);
                MyOraDB.Process_Name = process_name;

                MyOraDB.Parameter_Name[0] = "ARG_COMP_NAME";
                MyOraDB.Parameter_Name[1] = "ARG_LINE_CD";
                MyOraDB.Parameter_Name[2] = "ARG_MLINE_CD";
                MyOraDB.Parameter_Name[3] = "ARG_MONTH";
                MyOraDB.Parameter_Name[4] = "OUT_CURSOR";

                MyOraDB.Parameter_Type[0] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[1] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[2] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[3] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[4] = (int)OracleType.Cursor;

                MyOraDB.Parameter_Values[0] = ARG_COMP_NAME;
                MyOraDB.Parameter_Values[1] = ARG_LINE_CD;
                MyOraDB.Parameter_Values[2] = ARG_MLINE_CD;
                MyOraDB.Parameter_Values[3] = ARG_MONTH;
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

        private void GoFullscreen()
        {
            this.WindowState = FormWindowState.Normal;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Bounds = Screen.PrimaryScreen.Bounds;

        }

        private void FRM_SMT_OSD_DAILY_PHUOC_Load(object sender, EventArgs e)
        {
            lblDate.Text = string.Format(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            GoFullscreen();
            this.Cursor = Cursors.WaitCursor;
            //BindingOSDDaily();
            if (!bgw.IsBusy)
                bgw.RunWorkerAsync();
            //BindingOSDWeekly("IPPH");
            //BindingOSDWeekly("IN");
            //BindingOSDWeekly("DMPPU");
            //BindingOSDWeekly("OS");
            //load_data();
            this.Cursor = Cursors.Default;
        }

        private string GetText(AxFPSpreadADO.AxfpSpread spread, int col, int row)
        {
            try
            {
                object data = null;
                spread.GetText(col, row, ref data);
                return data.ToString();
            }
            catch (Exception ex)
            {
                //return "";
                //log.Error(ex);
                return null;
            }

        }


        private void CLearGrid()
        {
            for (int iRow = 2;iRow<= axfpOSD.MaxRows;iRow++)
            {
             for (int iCol = 2;iCol <= axfpOSD.MaxCols;iCol++)
             {
                axfpOSD.SetText(iCol,iRow,"");
                axfpOSD.Row = iRow;
                axfpOSD.Col = iCol;
                if (iRow < axfpOSD.MaxRows)
                {
                   
                    axfpOSD.BackColor = Color.White;
                    axfpOSD.ForeColor = Color.Black;
                }
                else

                {
                    axfpOSD.BackColor = Color.FromArgb(251, 255, 209);
                    axfpOSD.ForeColor = Color.Black;
                }
             }
            }
        }

        private void Animation(AxFPSpreadADO.AxfpSpread Grid,DataTable dt)
        {
            Grid.Hide();
            this.Cursor = Cursors.WaitCursor;
            BindingGrid(dt);
            AnimateWindow(Grid.Handle, 500, AW_SLIDE | 0X4); //IPEX_Monitor.ClassLib.WinAPI.getSlidType("2")
            Grid.Show();
            this.Cursor = Cursors.Default;

        }

        private void BindingGrid(DataTable dt)
        {
            
            if (dt !=null && dt.Rows.Count >0)
            {
                try
                {
                   
                    axfpOSD.MaxCols = dt.Rows.Count+ 3;
                    CLearGrid();
                   
                    axfpOSD.set_ColWidth(1, 10d);
                   

                    axfpOSD.SetText(1, 1, dt.Rows[0]["OSD_YMD"].ToString().Substring(0,3));

                    for (int iCol = 2; iCol <= axfpOSD.MaxCols; iCol++)
                    {
                        axfpOSD.set_ColWidth(iCol,126.2d/(axfpOSD.MaxCols - 1));
                        axfpOSD.Row = 1;
                        axfpOSD.Col = iCol;
                            axfpOSD.BackColor = Color.FromArgb(192,192,192);
                        axfpOSD.ForeColor = Color.Black;
                        axfpOSD.Font = new System.Drawing.Font("Calibri", 12, FontStyle.Bold);
                    }
                    axfpOSD.SetText(axfpOSD.MaxCols - 1,1, "AVG");
                    axfpOSD.SetText(axfpOSD.MaxCols, 1, "TOT");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                       // for (int iCol = 1; iCol < axfpOSD.MaxCols; iCol++)
                       // {
                            //if (dt.Rows[i]["OSD_YMD"].ToString().Substring(4, 2).Equals(GetText(this.axfpOSD,iCol,1)))
                          //  {

                                axfpOSD.SetText(i+2, 1, dt.Rows[i]["OSD_YMD"].ToString().Substring(4,2));
                                axfpOSD.SetText(i + 2, 2, dt.Rows[i]["OS"].ToString());
                                axfpOSD.SetText(i + 2, 3, dt.Rows[i]["TOT"].ToString());
                                

                                if (dt.Rows[i]["TODAY"].ToString().Equals(GetText(this.axfpOSD, i + 2, 1)))
                                {
                                    axfpOSD.Col = i + 2;

                                    for (int iRow = 2;iRow<= axfpOSD.MaxRows;iRow++)
                                    {
                                        axfpOSD.Row = iRow;
                                        axfpOSD.BackColor = Color.Orange;
                                    }

                                }

                                for (int iRow = 1; iRow <= 3; iRow++)
                                {
                                    axfpOSD.Row = iRow+1;
                                    axfpOSD.Col = i+2;
                                    axfpOSD.CellType = FPSpreadADO.CellTypeConstants.CellTypeNumber;
                                    axfpOSD.TypeNumberDecPlaces = 1;
                                    axfpOSD.TypeNumberShowSep = true;
                                    axfpOSD.Font = new System.Drawing.Font("Calibri", 11, FontStyle.Bold);
                                    axfpOSD.SetCellBorder(1, 1, axfpOSD.MaxCols, axfpOSD.MaxRows, FPSpreadADO.CellBorderIndexConstants.CellBorderIndexTop, 1, FPSpreadADO.CellBorderStyleConstants.CellBorderStyleSolid);
                                    axfpOSD.SetCellBorder(1, 1, axfpOSD.MaxCols, axfpOSD.MaxRows, FPSpreadADO.CellBorderIndexConstants.CellBorderIndexRight, 1, FPSpreadADO.CellBorderStyleConstants.CellBorderStyleSolid);
                                    axfpOSD.SetCellBorder(1, 1, axfpOSD.MaxCols, axfpOSD.MaxRows, FPSpreadADO.CellBorderIndexConstants.CellBorderIndexLeft, 1, FPSpreadADO.CellBorderStyleConstants.CellBorderStyleSolid);
                                    axfpOSD.SetCellBorder(1, 1, axfpOSD.MaxCols, axfpOSD.MaxRows, FPSpreadADO.CellBorderIndexConstants.CellBorderIndexBottom, 1, FPSpreadADO.CellBorderStyleConstants.CellBorderStyleSolid);


                                }


                                axfpOSD.Col = axfpOSD.MaxCols;
                                for (int iRow = 2; iRow <= axfpOSD.MaxRows; iRow++)
                                {
                                    axfpOSD.Row = iRow;
                                    axfpOSD.BackColor = Color.FromArgb(255,255,202);
                                    axfpOSD.Font = new System.Drawing.Font("Calibri", 11, FontStyle.Bold);
                                }

                                axfpOSD.Col = axfpOSD.MaxCols - 1;
                                for (int iRow = 2; iRow <= axfpOSD.MaxRows; iRow++)
                                {
                                    axfpOSD.Row = iRow;
                                    axfpOSD.BackColor = Color.FromArgb(244, 212, 252);
                                    axfpOSD.Font = new System.Drawing.Font("Calibri", 11, FontStyle.Bold);
                                    
                                }
                            //}
                       // }
                    }
                    //AVG
                    axfpOSD.SetText(axfpOSD.MaxCols - 1, 2, dt.Rows[0]["AVG_OS"].ToString());
                    axfpOSD.SetText(axfpOSD.MaxCols - 1, 3, dt.Rows[0]["AVG_TOT"].ToString());
                    //TOT
                    axfpOSD.SetText(axfpOSD.MaxCols , 2, dt.Rows[0]["TOT_OS"].ToString());
                    axfpOSD.SetText(axfpOSD.MaxCols, 3, dt.Rows[0]["TOT_TOT"].ToString());

                }
                catch(Exception ex)
                {}
            }
        }

        private void BindingOSDDaily()
        {
            try
            {
                DataTable dt = SP_SMT_OSD_DAILY("DAILY", line, Mline,UC_MONTH.GetValue());
                chartOSDDaily.DataSource = dt;

                Animation(axfpOSD, dt);
                //BindingGrid(dt);

                chartOSDDaily.Series[0].ArgumentDataMember = "OSD_YMD";
                chartOSDDaily.Series[0].ValueDataMembers.AddRange(new string[] { "OS" });
                chartOSDDaily.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
            }
            catch (Exception ex)
            { 
                
            }
        }

        private void BindingOSDWeekly(string Comp_Name)
        {
            try
            {
                switch (Comp_Name)
                {
                    case "IN":
                        chartIN.DataSource = SP_SMT_OSD_DAILY(Comp_Name, line, Mline, UC_MONTH.GetValue());
                        chartIN.Series[0].ArgumentDataMember = "STYLE_NAME";
                        chartIN.Series[0].ValueDataMembers.AddRange(new string[] { "QTY" });
                        chartIN.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;

                        chartIN.Series[1].ArgumentDataMember = "STYLE_NAME";
                        chartIN.Series[1].ValueDataMembers.AddRange(new string[] { "PER" });
                        chartIN.Series[1].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                        ((XYDiagram)chartIN.Diagram).AxisX.Label.Angle = -35;

                        break;
                    case "IP":
                        chartIPPH.DataSource = SP_SMT_OSD_DAILY(Comp_Name, line, Mline, UC_MONTH.GetValue());
                        chartIPPH.Series[0].ArgumentDataMember = "STYLE_NAME";
                        chartIPPH.Series[0].ValueDataMembers.AddRange(new string[] { "QTY" });
                        chartIPPH.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;

                        chartIPPH.Series[1].ArgumentDataMember = "STYLE_NAME";
                        chartIPPH.Series[1].ValueDataMembers.AddRange(new string[] { "PER" });
                        chartIPPH.Series[1].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;

                        ((XYDiagram)chartIPPH.Diagram).AxisX.Label.Angle = -35;
                        break;
                    case "PH":
                        chartPH.DataSource = SP_SMT_OSD_DAILY(Comp_Name, line, Mline, UC_MONTH.GetValue());
                        chartPH.Series[0].ArgumentDataMember = "STYLE_NAME";
                        chartPH.Series[0].ValueDataMembers.AddRange(new string[] { "QTY" });
                        chartPH.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;

                        chartPH.Series[1].ArgumentDataMember = "STYLE_NAME";
                        chartPH.Series[1].ValueDataMembers.AddRange(new string[] { "PER" });
                        chartPH.Series[1].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;

                        ((XYDiagram)chartPH.Diagram).AxisX.Label.Angle = -35;
                        break;
                    case "DMP":
                        chartDMPPU.DataSource = SP_SMT_OSD_DAILY(Comp_Name, line, Mline, UC_MONTH.GetValue());
                        chartDMPPU.Series[0].ArgumentDataMember = "STYLE_NAME";
                        chartDMPPU.Series[0].ValueDataMembers.AddRange(new string[] { "QTY" });
                        chartDMPPU.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;

                        chartDMPPU.Series[1].ArgumentDataMember = "STYLE_NAME";
                        chartDMPPU.Series[1].ValueDataMembers.AddRange(new string[] { "PER" });
                        chartDMPPU.Series[1].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;

                        ((XYDiagram)chartDMPPU.Diagram).AxisX.Label.Angle = -35;
                        break;
                    case "PU":
                        chartPU.DataSource = SP_SMT_OSD_DAILY(Comp_Name, line, Mline, UC_MONTH.GetValue());
                        chartPU.Series[0].ArgumentDataMember = "STYLE_NAME";
                        chartPU.Series[0].ValueDataMembers.AddRange(new string[] { "QTY" });
                        chartPU.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;

                        chartPU.Series[1].ArgumentDataMember = "STYLE_NAME";
                        chartPU.Series[1].ValueDataMembers.AddRange(new string[] { "PER" });
                        chartPU.Series[1].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                        ((XYDiagram)chartPU.Diagram).AxisX.Label.Angle = -35;
                        break;
                    case "OS":
                        chartOS.DataSource = SP_SMT_OSD_DAILY(Comp_Name, line, Mline, UC_MONTH.GetValue());
                        chartOS.Series[0].ArgumentDataMember = "STYLE_NAME";
                        chartOS.Series[0].ValueDataMembers.AddRange(new string[] { "QTY" });
                        chartOS.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;

                        chartOS.Series[1].ArgumentDataMember = "STYLE_NAME";
                        chartOS.Series[1].ValueDataMembers.AddRange(new string[] { "PER" });
                        chartOS.Series[1].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                        ((XYDiagram)chartOS.Diagram).AxisX.Label.Angle = -35;
                        break;

                }
            }
            catch (Exception ex)
            { }
        }

        private void load_data()
        {
            try
            {
                splitMain.Visible = false;
                BindingOSDDaily();
                BindingOSDWeekly("IP");
                BindingOSDWeekly("PH");
                BindingOSDWeekly("OS");
                BindingOSDWeekly("DMP");
                BindingOSDWeekly("PU");
                BindingOSDWeekly("IN");
            }
            catch
            {
            }
            finally
            {
                splitMain.Visible = true;
            }
        }

        private void bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
               // return;
                if (this.chartOSDDaily.InvokeRequired)
                    this.chartOSDDaily.Invoke((MethodInvoker)delegate
                    {
                        BindingOSDDaily();
                    });
                else
                    BindingOSDDaily();

                if (this.chartIPPH.InvokeRequired)
                    this.chartIPPH.Invoke((MethodInvoker)delegate
                    {
                        BindingOSDWeekly("IP");
                        BindingOSDWeekly("PH");
                    });
                else
                {
                    BindingOSDWeekly("IP");
                    BindingOSDWeekly("PH");
                }

                if (this.chartOS.InvokeRequired)
                    this.chartOS.Invoke((MethodInvoker)delegate
                    {
                        BindingOSDWeekly("OS");
                    });
                else
                    BindingOSDWeekly("OS");


                if (this.chartDMPPU.InvokeRequired)
                    this.chartDMPPU.Invoke((MethodInvoker)delegate
                    {
                        BindingOSDWeekly("DMP");
                        BindingOSDWeekly("PU");
                    });
                else
                {
                    BindingOSDWeekly("PU");
                    BindingOSDWeekly("DMP");
                }
                if (this.chartIN.InvokeRequired)
                    this.chartIN.Invoke((MethodInvoker)delegate
                    {
                        BindingOSDWeekly("IN");
                    });
                else
                    BindingOSDWeekly("IN");
            }
            catch (Exception ex)
            { }
        }

        private void lblTitle_DoubleClick(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            //BindingOSDDaily();
            if (!bgw.IsBusy)
                bgw.RunWorkerAsync();
            
            this.Cursor = Cursors.Default;
        }
        int cCount = 0;
        private void tmrDate_Tick(object sender, EventArgs e)
        {
            cCount++;
            lblDate.Text = string.Format(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            if (cCount >= 30)
            {
                this.Cursor = Cursors.WaitCursor;
            //   // BindingOSDDaily();
                if (!bgw.IsBusy)
                    bgw.RunWorkerAsync();
               // load_data();
                cCount = 0;
                this.Cursor = Cursors.Default;
            }
        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }

        private void lblTitle_VisibleChanged(object sender, EventArgs e)
        {
           
        }

        private void FRM_SMT_OSD_DAILY_PHUOC_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                cCount = 25;
                tmrDate.Start();
                line = strinit.line;
                Mline = strinit.mline;
                Lang = strinit.lang;
            }
            else
                tmrDate.Stop();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void UC_MONTH_ValueChangeEvent(object sender, EventArgs e)
        {

            cCount = 25;
            tmrDate.Start();
            line = strinit.line;
            Mline = strinit.mline;
            Lang = strinit.lang;
        }

        private void axfpOSD_ClickEvent(object sender, AxFPSpreadADO._DSpreadEvents_ClickEvent e)
        {
            string sCellValue = "";
            axfpOSD.Row = 1;
            axfpOSD.Col = e.col;
            sCellValue = axfpOSD.Value.ToString();

            string date = UC_MONTH.GetValue() + sCellValue;
           //MessageBox.Show(date);
            this.TopMost = false;
            FRM_EXTERNAL_OSND_POP frm_pop = new FRM_EXTERNAL_OSND_POP(line, Mline, date, date);
            frm_pop.ShowDialog();
            frm_pop.TopMost = true;
        }

        private void UC_MONTH_Load(object sender, EventArgs e)
        {

        }
        
    }
}
