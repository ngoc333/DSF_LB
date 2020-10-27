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

namespace FORM
{
    public partial class FRM_SMT_HR_ABSENT_YEARLY_PHUOC : Form
    {
        public FRM_SMT_HR_ABSENT_YEARLY_PHUOC()
        {
            InitializeComponent();
        }
          int indexScreen;
        string line, Mline;
        int cCount = 0;
        Form[] arrForm = new Form[3];
        public FRM_SMT_HR_ABSENT_YEARLY_PHUOC(string Title, int _indexScreen, string _Line, string _Mline)
        {
            InitializeComponent();
            indexScreen = _indexScreen;
            Mline = _Mline;
            line = _Line;
            tmrDate.Stop();
            //lblTitle.Text = Title;
        }

        public DataTable SP_SMT_HR_ABSENT(string ARG_QTYPE, string ARG_LINE_CD, string ARG_MLINE_CD)
        {
            COM.OraDB MyOraDB = new COM.OraDB();
            DataSet ds_ret;
            try
            {
                string process_name = "MES.PKG_SMT_PHUOC.SP_SMT_HR_ABSENT";

                MyOraDB.ReDim_Parameter(4);
                MyOraDB.Process_Name = process_name;

                MyOraDB.Parameter_Name[0] = "ARG_QTYPE";
                MyOraDB.Parameter_Name[1] = "ARG_LINE_CD";
                MyOraDB.Parameter_Name[2] = "ARG_MLINE_CD";
                MyOraDB.Parameter_Name[3] = "OUT_CURSOR";

                MyOraDB.Parameter_Type[0] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[1] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[2] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[3] = (int)OracleType.Cursor;

                MyOraDB.Parameter_Values[0] = ARG_QTYPE;
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

        private void GoFullscreen()
        {
            this.WindowState = FormWindowState.Normal;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Bounds = Screen.PrimaryScreen.Bounds;

        }


        private void ClearGrid(AxFPSpreadADO.AxfpSpread Grid)
        {
            for (int iRow = 0; iRow <= Grid.MaxRows; iRow++)
            {
                for (int iCol = 3; iCol <= Grid.MaxCols; iCol++)
                {

                    Grid.SetText(iCol, iRow, "");
                    axfpAbsent.Row = iRow;
                    axfpAbsent.Col = iCol;
                    
                    switch (iRow)
                    { 
                        case 1:
                            axfpAbsent.Font = new Font("Calibri", 24, FontStyle.Bold);
                            break;
                        default:
                            axfpAbsent.Font = new Font("Calibri", 24, FontStyle.Regular);
                            break;
                    }
                    
                }
            }
            Grid.SetCellBorder(0, 0,axfpAbsent.MaxCols, axfpAbsent.MaxRows, FPSpreadADO.CellBorderIndexConstants.CellBorderIndexTop, 0, FPSpreadADO.CellBorderStyleConstants.CellBorderStyleSolid);
            Grid.SetCellBorder(0, 0, axfpAbsent.MaxCols, axfpAbsent.MaxRows, FPSpreadADO.CellBorderIndexConstants.CellBorderIndexLeft, 0, FPSpreadADO.CellBorderStyleConstants.CellBorderStyleSolid);
            Grid.SetCellBorder(0, 0, axfpAbsent.MaxCols, axfpAbsent.MaxRows, FPSpreadADO.CellBorderIndexConstants.CellBorderIndexRight, 0, FPSpreadADO.CellBorderStyleConstants.CellBorderStyleSolid);
            Grid.SetCellBorder(0, 0, axfpAbsent.MaxCols, axfpAbsent.MaxRows, FPSpreadADO.CellBorderIndexConstants.CellBorderIndexBottom, 0, FPSpreadADO.CellBorderStyleConstants.CellBorderStyleSolid);
            Grid.SetCellBorder(0, 0, axfpAbsent.MaxCols, axfpAbsent.MaxRows, FPSpreadADO.CellBorderIndexConstants.CellBorderIndexLeft, 0, FPSpreadADO.CellBorderStyleConstants.CellBorderStyleSolid);
        }


        private void BindingAbsent()
        {
            DataTable dt = SP_SMT_HR_ABSENT("A", line, Mline);
            double ValueGauges = 0;
            double Planed = 0, Unplaned = 0,TOT_MAN = 0;
            arcScaleComponent1.EnableAnimation = false;
            arcScaleComponent1.EasingMode = DevExpress.XtraGauges.Core.Model.EasingMode.EaseInOut;
            arcScaleComponent1.EasingFunction = new BackEase();
            lblValueG.Text = ValueGauges.ToString();
            arcScaleComponent1.Value = (float)ValueGauges;
            ClearGrid(axfpAbsent);
            if (dt != null && dt.Rows.Count > 0)
            {
               
                axfpAbsent.MaxCols = dt.Rows.Count + 2;
                for (int i = 0; i < dt.Rows.Count; i++)
                { 
                    axfpAbsent.SetText(i+3,1,dt.Rows[i]["DD"].ToString());
                    axfpAbsent.SetText(i + 3, 2, dt.Rows[i]["TOT_MAN"].ToString());
                    axfpAbsent.SetText(i + 3, 3, dt.Rows[i]["UNPLANED"].ToString());
                    axfpAbsent.SetText(i + 3, 4, dt.Rows[i]["PLANED"].ToString());
                    axfpAbsent.SetText(i + 3, 5, dt.Rows[i]["PER"].ToString());
                    axfpAbsent.SetText(i + 3, 6, dt.Rows[i]["RELIEF"].ToString());
                    
                    axfpAbsent.set_ColWidth(i + 3, 190d / (axfpAbsent.MaxCols - 2));
                  
                      
                    if (i == dt.Rows.Count-1)
                    {
                        ValueGauges = Convert.ToDouble(dt.Rows[dt.Rows.Count - 1]["PER"]);
                        Planed = Convert.ToDouble(dt.Rows[dt.Rows.Count - 1]["PLANED"]);
                        Unplaned = Convert.ToDouble(dt.Rows[dt.Rows.Count - 1]["UNPLANED"]);
                        TOT_MAN = Convert.ToDouble(dt.Rows[dt.Rows.Count - 1]["TOT_MAN"]);
                    }

                    axfpAbsent.Row = 1;
                    axfpAbsent.Col = i + 3;
                    if (dt.Rows[i]["IS_TODAY"].ToString().Equals("Y"))
                    {
                        
                        axfpAbsent.BackColor = Color.Salmon;
                        axfpAbsent.ForeColor = Color.White;
                    }
                    else
                    {
                        axfpAbsent.BackColor = Color.FromArgb(1,143,228);
                        axfpAbsent.ForeColor = Color.White;
                    }
                }
               
                axfpAbsent.set_ColWidth(axfpAbsent.MaxCols,14d);

                for (int iRow = 2; iRow <= axfpAbsent.MaxRows; iRow++)
                {
                    axfpAbsent.Row = iRow;
                    axfpAbsent.Col = axfpAbsent.MaxCols;
                    axfpAbsent.TypeNumberDecPlaces = 1;
                    axfpAbsent.BackColor = Color.FromArgb(244, 212, 252);
                }
            }
            arcScaleComponent1.EnableAnimation = true;
            arcScaleComponent1.EasingMode = DevExpress.XtraGauges.Core.Model.EasingMode.EaseInOut;
            arcScaleComponent1.EasingFunction = new BackEase();
            lblValueG.Text = ValueGauges.ToString();
            arcScaleComponent1.Value = (float)ValueGauges;

            DataTable dt_tmp = new DataTable();
            dt_tmp.Columns.Add("CAPTION");
            dt_tmp.Columns.Add("VALUE",typeof(double));

            dt_tmp.Rows.Add();
            dt_tmp.Rows[dt_tmp.Rows.Count - 1]["CAPTION"] = "UNPLANED";
            dt_tmp.Rows[dt_tmp.Rows.Count - 1]["VALUE"] = dt.Rows[dt.Rows.Count - 1]["UNPLANED"].ToString();
            dt_tmp.Rows.Add();
            dt_tmp.Rows[dt_tmp.Rows.Count - 1]["CAPTION"] = "PLANED";
            dt_tmp.Rows[dt_tmp.Rows.Count - 1]["VALUE"] = dt.Rows[dt.Rows.Count - 1]["PLANED"].ToString();

            chartControl2.DataSource = dt_tmp;
            chartControl2.Series[0].ArgumentDataMember = "CAPTION";
            chartControl2.Series[0].ValueDataMembers.AddRange(new string[] { "VALUE" });
            chartControl2.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;

        

        }


        private void BindingTurnOver()
        { 
            DataTable dt = SP_SMT_HR_ABSENT("T", line, Mline);
            ClearGrid(axfpTurnOver);
            if (dt != null && dt.Rows.Count > 0)
            {
                axfpTurnOver.SetText(1, 1, dt.Rows[0]["THEYEAR"].ToString());
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    axfpTurnOver.SetText(i+3,1,dt.Rows[i]["THEDATE"].ToString());
                    axfpTurnOver.SetText(i + 3, 2, dt.Rows[i]["ABS_QTY"].ToString());
                    axfpTurnOver.SetText(i + 3, 3, dt.Rows[i]["PER"].ToString());
                }
               
            }
            //chartTurnOver.DataSource = dt.Select("THEYEAR <> 'AVG (%)'").CopyToDataTable();
            //chartTurnOver.Series[0].ArgumentDataMember = "THEDATE";
            //chartTurnOver.Series[0].ValueDataMembers.AddRange(new string[] { "ABS_QTY" });
            //chartTurnOver.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
        }

        private void FRM_SMT_HR_ABSENT_Load(object sender, EventArgs e)
        {
            lblDate.Text = string.Format(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            GoFullscreen();
            //BindingAbsent();
            //BindingTurnOver();
        }
       
        private void tmrDate_Tick(object sender, EventArgs e)
        {
            cCount++;
            lblDate.Text = string.Format(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            if (cCount >= 30)
            {
                BindingAbsent();
                BindingTurnOver();

                cCount = 0;
            }

        }

        private void FRM_SMT_HR_ABSENT_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                cCount = 30;
                tmrDate.Start();
            }
            else
                tmrDate.Stop();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {

        }
    }
}
