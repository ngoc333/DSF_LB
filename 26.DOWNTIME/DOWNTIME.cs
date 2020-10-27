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
    public partial class DOWNTIME : Form
    {
        public DOWNTIME()
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
        public DOWNTIME(string Caption, int indexScreen, string Line_cd, string Mline_cd, string Lang)
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
        private DataTable SEL_DATA_OSD(string V_P_TYPE, string V_P_YMD, string ARG_CULTURE)
        {
            System.Data.DataSet retDS;
            COM.OraDB MyOraDB = new COM.OraDB();
           // DataTable data = null;

            MyOraDB.ReDim_Parameter(4);
            MyOraDB.Process_Name = "MES.SP_DOWNTIME";           

            MyOraDB.Parameter_Type[0] = (char)OracleType.VarChar;
            MyOraDB.Parameter_Type[1] = (char)OracleType.VarChar;
            MyOraDB.Parameter_Type[2] = (char)OracleType.VarChar;
            MyOraDB.Parameter_Type[3] = (int)OracleType.Cursor;
           
            MyOraDB.Parameter_Name[0] = "V_P_TYPE";
            MyOraDB.Parameter_Name[1] = "V_P_YMD";
            MyOraDB.Parameter_Name[2] = "ARG_CULTURE";
            MyOraDB.Parameter_Name[3] = "CV_1";
           
            MyOraDB.Parameter_Values[0] = V_P_TYPE;
            MyOraDB.Parameter_Values[1] = V_P_YMD;
            MyOraDB.Parameter_Values[2] = ARG_CULTURE;
            MyOraDB.Parameter_Values[3] = "";           

            MyOraDB.Add_Select_Parameter(true);
            retDS = MyOraDB.Exe_Select_Procedure();

            if (retDS == null) return null;

            return retDS.Tables[MyOraDB.Process_Name];
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
            Lang = ComVar.Var._strValue3;
            _dtnInit = ComVar.Func.getInitForm(this.GetType().Assembly.GetName().Name, this.GetType().Name);
            bindingDataGrid();

            //sbtnSearch_Click(sender, e);
            
        }
        DataTable dtf = null;
       

        private void BindingChart()
        {
            try
            {
                DataTable data = null;
                data = SEL_DATA_OSD("CH", uc_year.GetValue().ToString(), "");

                chartControl1.DataSource = data;
                chartControl1.Series[0].ArgumentDataMember = "COL_NM";
                chartControl1.Series[0].ValueDataMembers.AddRange(new string[] { "TARGET" });

                chartControl1.Series[1].ArgumentDataMember = "COL_NM";
                chartControl1.Series[1].ValueDataMembers.AddRange(new string[] { "AMOUNT" });

               // chartControl1.Series[2].ArgumentDataMember = "COL_NM";
              //  chartControl1.Series[2].ValueDataMembers.AddRange(new string[] { "RATE" });

            }
            catch { }
        }

     

        private void button1_Click(object sender, EventArgs e)
        {
            //this.Hide();
            ComVar.Var.callForm = _dtnInit["frmHome"];
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
            cCount++;
            lblDate.Text = string.Format(DateTime.Now.ToString("yyyy-MM-dd\nHH:mm:ss"));
            if (cCount >= 40)
            {
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    bindingDataGrid();
                   // sbtnSearch_Click(sender, e);
                    this.Cursor = Cursors.Default;
                    cCount = 0;
                }
                catch { this.Cursor = Cursors.Default; cCount = 0; }
            }
        }

        private void FRM_SMT_OSD_INTERNAL_PHUOC_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                cCount = 39;
                line = ComVar.Var._strValue1;
                Mline = ComVar.Var._strValue2;         
                Lang = ComVar.Var._strValue3;
                timer1.Start();
            }
            else
                timer1.Stop();
        }

        private void sbtnSearch_Click(object sender, EventArgs e)
        {
            DataTable data = null;
            data = SEL_DATA_OSD("Q", uc_year.GetValue().ToString(), "");

            grdBase.DataSource = data;
            for (int i = 2; i < gvwBase.Columns.Count; i++)
            {
                gvwBase.Columns[i].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                gvwBase.Columns[i].DisplayFormat.FormatString = "#,0.##";
            }          

            BindingChart();
        }

        private void bindingDataGrid()
        {
            grdBase.Refresh();
            gvwBase.Columns.Clear();

            DataTable dt = dtf = SEL_DATA_OSD("Q", uc_year.GetValue().ToString(), "");
            grdBase.DataSource = dt;

            gvwBase.OptionsView.ColumnAutoWidth = false;
            for (int i = 0; i < gvwBase.Columns.Count; i++)
            {
                gvwBase.Columns[i].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                gvwBase.Columns[i].AppearanceHeader.BackColor = System.Drawing.Color.Gray;
                gvwBase.Columns[i].AppearanceHeader.BackColor2 = System.Drawing.Color.Gray;
                gvwBase.Columns[i].AppearanceHeader.ForeColor = System.Drawing.Color.White;
                gvwBase.Columns[i].AppearanceHeader.Font = new System.Drawing.Font("Calibri", 14F, System.Drawing.FontStyle.Bold);
                gvwBase.Columns[i].OptionsColumn.ReadOnly = true;
                gvwBase.Columns[i].OptionsColumn.AllowEdit = false;
                gvwBase.Columns[i].OptionsColumn.ReadOnly = true;
                gvwBase.Columns[i].OptionsColumn.AllowEdit = false;
                gvwBase.Columns[i].OptionsFilter.AllowFilter = false;
                gvwBase.Columns[i].OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
                if (i < 2)
                {
                    gvwBase.Columns[i].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
                    gvwBase.Columns[i].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                    //gvwBase.Columns[i].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                }
                else
                {
                    gvwBase.Columns[i].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                    gvwBase.Columns[i].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    gvwBase.Columns[i].DisplayFormat.FormatString = "#,0.##";
                }

                gvwBase.Columns[i].Caption = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(gvwBase.Columns[i].GetCaption().Replace("_", " ").Replace("'", " ").ToLower()).Split(',')[0];
                //gvwBase.Columns[0].Visible = false;
                if (gvwBase.Columns[i].FieldName == "TOTAL")
                {
                    gvwBase.Columns[i].VisibleIndex = 999;
                }
                if (i == 1 || i==0)
                {
                    gvwBase.Columns[i].Width = 150;
                    gvwBase.Columns[i].OptionsColumn.AllowMerge = DefaultBoolean.True;
                     
                     }
                else
                    gvwBase.Columns[i].Width = 130;
            }

            BindingChart();
            // gvwBase.BestFitColumns();
            //gvwBase.TopRowIndex = 0;



        }

        private void uc_year_ValueChangeEvent(object sender, EventArgs e)
        {
            try
            {
                bindingDataGrid();
               // sbtnSearch_Click(sender, e);                
            }
            catch
            {

            }
        }









    }
}
