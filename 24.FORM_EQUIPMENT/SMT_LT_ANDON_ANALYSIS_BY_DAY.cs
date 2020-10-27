using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OracleClient;

namespace FORM
{
    public partial class SMT_LT_ANDON_ANALYSIS_BY_DAY : Form
    {
        public SMT_LT_ANDON_ANALYSIS_BY_DAY()
        {
            InitializeComponent();
        }
        #region Ora
        string _Mline; string _Line ;

       // DataTable _dtXML = null;
        Dictionary<string, string> _dtnInit = new Dictionary<string, string>();

        public DataTable SEL_DATA_ANDON(string Qtype, string ARG_LINE_CD, string ARG_MLINE_CD, string ARG_PROCESS_NAME)
        {
            COM.OraDB MyOraDB = new COM.OraDB();
            DataSet ds_ret;

            try
            {
                string process_name = "MES.PKG_SMT_LT.SP_SMT_ANDON_ANALISYS"; //SP_SMT_ANDON_DAILY

                MyOraDB.ReDim_Parameter(5);
                MyOraDB.Process_Name = process_name;

                MyOraDB.Parameter_Name[0] = "ARG_QTYPE";
                MyOraDB.Parameter_Name[1] = "ARG_LINE_CD";
                MyOraDB.Parameter_Name[2] = "ARG_MLINE_CD";
                MyOraDB.Parameter_Name[3] = "ARG_PROCESS";
                MyOraDB.Parameter_Name[4] = "OUT_CURSOR";

                MyOraDB.Parameter_Type[0] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[1] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[2] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[3] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[4] = (int)OracleType.Cursor;

                MyOraDB.Parameter_Values[0] = Qtype;
                MyOraDB.Parameter_Values[1] = ARG_LINE_CD;
                MyOraDB.Parameter_Values[2] = ARG_MLINE_CD;
                MyOraDB.Parameter_Values[3] = ARG_PROCESS_NAME;               
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

        #endregion

        private void GoFullscreen()
        {
            this.WindowState = FormWindowState.Normal;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Bounds = Screen.PrimaryScreen.Bounds;

        }

        int indexScreen;
        string sLine = ComVar.Var._strValue1, sMline = ComVar.Var._strValue2;
        DataTable dtA = null, dtB = null, dtC = null;
        int cMachine = 0, cQual = 0, cProd = 0, cCount = 0;
       // init strinit = new init();
        public SMT_LT_ANDON_ANALYSIS_BY_DAY(string Title, int _indexScreen, string _Line, string _Mline)
        {
            InitializeComponent();
            indexScreen = _indexScreen;          
            sMline = _Mline;
            sLine = _Line;         
            lblTitle.Text = Title;
        }
        private void SMT_LT_ANDON_ANALYSIS_BY_DAY_Load(object sender, EventArgs e)
        {
            GoFullscreen();

        //    _dtXML = ComVar.Func.ReadXML(Application.StartupPath + "\\InitForm.XML", this.GetType().Name);
            _dtnInit = ComVar.Func.getInitForm(this.GetType().Assembly.GetName().Name, this.GetType().Name);


            lblDate.Text = string.Format(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            
        }

        private void BindingChart(DevExpress.XtraCharts.ChartControl sChart, DataTable dt, string Arg_Member, string Arg_ValueData)
        {
            sChart.DataSource = dt;
            sChart.Series[0].ArgumentDataMember = Arg_Member;
            sChart.Series[0].ValueDataMembers.AddRange(new string[] { Arg_ValueData });
            sChart.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
            //DevExpress.XtraCharts.ConstantLine TargetLine = new DevExpress.XtraCharts.ConstantLine();
            //TargetLine.AxisValueSerializable = "1";
            //TargetLine.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            //TargetLine.Name = "Target: " + dt.Rows[0]["TARGET"].ToString();
            //((DevExpress.XtraCharts.XYDiagram)sChart.Diagram).AxisY.ConstantLines.Clear();
            //TargetLine.AxisValue = dt.Rows[0]["TARGET"];

            //((DevExpress.XtraCharts.XYDiagram)sChart.Diagram).AxisY.ConstantLines.AddRange(new DevExpress.XtraCharts.ConstantLine[] {
            //TargetLine});
        }

        private void GetDataMiddle()
        {

        }


        private void tmrDate_Tick(object sender, EventArgs e)
        {
            cCount++;
            lblDate.Text = string.Format(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            if (cCount >= 30)
            {
                try
                {
                    //dtB = SEL_DATA_ANDON("B2", "007", "001", null);
                    dtB = SEL_DATA_ANDON("B2", sLine, sMline, null);
                    if (dtB != null && dtB.Rows.Count > 0)
                    {
                        BindingChart(chartQual, dtB, "STATION", "QUAL");
                        BindingChart(chartMa, dtB, "STATION", "MA");
                        BindingChart(chartProd, dtB, "STATION", "PROD");
                    }

                   // dtA = SEL_DATA_ANDON("A2", "007", "001", null);
                    dtA = SEL_DATA_ANDON("A2", sLine, sMline, null);
                    if (dtA != null && dtA.Rows.Count > 0)
                    {
                       // cProd = 0; cQual = 0; cMachine = 0;
                        tmrCount.Start();
                    }
                    else
                    {
                        tmrCount.Stop();
                        lblQual_DT.Text = "0";
                        lblMa_DT.Text = "0";
                        lblProd_DT.Text = "0";
                    }

                  
                    dtC = SEL_DATA_ANDON("C2", sLine, sMline, "LA");
                    BindingChart(chartSumQual, dtC, "HH", "DT");
                
                    dtC = SEL_DATA_ANDON("C2", sLine, sMline, "LB");
                    BindingChart(chartSumMa, dtC, "HH", "DT");

                    dtC = SEL_DATA_ANDON("C2", sLine, sMline, "LC");
                    BindingChart(chartSumProd, dtC, "HH", "DT");
                }
                catch (Exception ex)
                { }
                
                cCount = 0;
            }
        }

        private void tmrCount_Tick(object sender, EventArgs e)
        {
            //if (cQual <= Convert.ToInt32(dtA.Rows[0]["DT"]) - 1)
            //{
            //    cQual++;
            //    lblQual_DT.Text = cQual.ToString();
            //}
            //if (cMachine <= Convert.ToInt32(dtA.Rows[1]["DT"]) - 1)
            //{
            //    cMachine++; lblMa_DT.Text = cMachine.ToString();
            //}
            //if (cProd <= Convert.ToInt32(dtA.Rows[2]["DT"]) - 1)
            //{
            //    cProd++; lblProd_DT.Text = cProd.ToString();
            //}
            //if (cMachine + cQual + cProd - 3 >= Convert.ToInt32(dtA.Rows[0]["DT"]) - 1 + Convert.ToInt32(dtA.Rows[1]["DT"]) - 1 + Convert.ToInt32(dtA.Rows[2]["DT"]) - 1)
            //{
            //    tmrCount.Stop();
            //}
            lblQual_DT.Text = Convert.ToDouble(dtA.Rows[0]["DT"]).ToString();
            lblMa_DT.Text = Convert.ToDouble(dtA.Rows[1]["DT"]).ToString();
            lblProd_DT.Text = Convert.ToDouble(dtA.Rows[2]["DT"]).ToString();
            tmrCount.Stop();
        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }

        private void SMT_LT_ANDON_ANALYSIS_BY_DAY_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                _Line = ComVar.Var._strValue1;
                _Mline = ComVar.Var._strValue2;
              
                cCount = 30;
                tmrDate.Start();
            }
            else
                tmrDate.Stop();
        }

        private void lblTitle_DoubleClick(object sender, EventArgs e)
        {
            cCount = 29;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnMonth_Click(object sender, EventArgs e)
        {
            this.Hide();
            string Caption = "Andon Data Analysis by Day";
            Form fc = Application.OpenForms["SMT_LT_ANDON_ANALYSIS_BY_DAY"];
            if (fc != null)
                fc.Close();
            //switch (Lang)
            //{
            //    case "Vn":
            //        Caption = "DTD (Dock To Dock) by Year";
            //        break;
            //    default:
            //        Caption = "DTD (Dock To Dock) by Year";
            //        break;
            //}

            SMT_LT_ANDON_ANALYSIS_BY_DAY f = new SMT_LT_ANDON_ANALYSIS_BY_DAY(Caption, 1, sLine, sMline);
            f.Show();
            //f.TopMost = true;
        }

        private void lblDate_DoubleClick(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void cmdBACK_Click(object sender, EventArgs e)
        {
           // ComVar.Var.callForm = _dtXML.Rows[0]["frmHome"].ToString();
            ComVar.Var.callForm = _dtnInit["frmHome"];
        }
    }
}
