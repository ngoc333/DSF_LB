using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OracleClient;
using DevExpress.XtraGrid;
using DevExpress.XtraEditors;
using DevExpress.XtraTab;

namespace FORM
{
    public partial class FRM_MUILTI_SKILL_V2 : Form
    {
        public FRM_MUILTI_SKILL_V2()
        {
            InitializeComponent();
        }
          int indexScreen;
        string line, Mline;
        int cCount = 0;
        public FRM_MUILTI_SKILL_V2(string Title, int _indexScreen, string _Line, string _Mline)
        {
            InitializeComponent();
            indexScreen = _indexScreen;
            Mline = _Mline;
            line = _Line;
           // tmrDate.Stop();
            lbltitle.Text = Title;
        }

        public DataTable SEL_MULTI_SKILL_V2(string Qtype, string ARG_PROCESS,string ARG_LINE,string ARG_MLINE)
        {
            COM.OraDB MyOraDB = new COM.OraDB();
            DataSet ds_ret;

            try
            {
                string process_name = "MES.PKG_SMT_PHUOC.SP_SMT_MULTI_SKILL_V2";

                MyOraDB.ReDim_Parameter(5);
                MyOraDB.Process_Name = process_name;

                MyOraDB.Parameter_Name[0] = "ARG_QTYPE";
                MyOraDB.Parameter_Name[1] = "ARG_PROCESS_CD";
                MyOraDB.Parameter_Name[2] = "ARG_LINE";
                MyOraDB.Parameter_Name[3] = "ARG_MLINE";
                MyOraDB.Parameter_Name[4] = "OUT_CURSOR";

                MyOraDB.Parameter_Type[0] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[1] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[2] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[3] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[4] = (int)OracleType.Cursor;

                MyOraDB.Parameter_Values[0] = Qtype;
                MyOraDB.Parameter_Values[1] = ARG_PROCESS;
                MyOraDB.Parameter_Values[2] = ARG_LINE;
                MyOraDB.Parameter_Values[3] = ARG_MLINE;
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
        private void Tab_Click(object sender, EventArgs e)
        {
            //DevExpress.XtraTab.XtraTabPage tab = sender as DevExpress.XtraTab.XtraTabPage;
            //if (tab != null)
            //{
                MessageBox.Show(  ((DevExpress.XtraTab.XtraTabPage)sender).Name.ToString());
            //}

          //  ((DevExpress.XtraTab.XtraTabPage)sender).Name.ToString();
        }


        private void BindingData(string ARG_PROCESS,string ARG_LINE,string ARG_MLINE)
        {

            DataTable dt = SEL_MULTI_SKILL_V2("Q", ARG_PROCESS, ARG_LINE, ARG_MLINE);
            gridView1.Columns.Clear();
            if (dt != null && dt.Rows.Count > 0)
            {
               
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    dt.Columns[i].ColumnName = dt.Columns[i].ColumnName.Replace("'", "").Replace("_", " ");

                }

                gridControl1.DataSource = dt;
               

                //format Condition
                for (int i = 2; i < gridView1.Columns.Count; i++)
                {

                    
                    GridFormatRule gridFormatRule = new GridFormatRule();
                    FormatConditionRuleIconSet formatConditionRuleIconSet = new FormatConditionRuleIconSet();
                    FormatConditionIconSet iconSet = formatConditionRuleIconSet.IconSet = new FormatConditionIconSet();
                  
                    FormatConditionIconSetIcon icon1 = new FormatConditionIconSetIcon();
                    FormatConditionIconSetIcon icon2 = new FormatConditionIconSetIcon();
                    FormatConditionIconSetIcon icon3 = new FormatConditionIconSetIcon();
                    FormatConditionIconSetIcon icon4 = new FormatConditionIconSetIcon();
                    FormatConditionIconSetIcon icon5 = new FormatConditionIconSetIcon();

                    //Choose predefined icons.
                    icon1.PredefinedName = "Quarters5_1.png";
                    icon2.PredefinedName = "Quarters5_2.png";
                    icon3.PredefinedName = "Quarters5_3.png";
                    icon4.PredefinedName = "Quarters5_4.png";
                    icon5.PredefinedName = "Quarters5_5.png";

                    //Specify the type of threshold values.
                    iconSet.ValueType = FormatConditionValueType.Automatic;
                    
                    //Define ranges to which icons are applied by setting threshold values.
                    icon1.Value = 100; // target range: 100% <= value
                    icon1.ValueComparison = FormatConditionComparisonType.GreaterOrEqual;
                    
                    icon2.Value = 75; // target range: 50% <= value < 75%
                    icon2.ValueComparison = FormatConditionComparisonType.GreaterOrEqual;
                    icon3.Value = 50; // target range: 25% <= value < 50%
                    icon3.ValueComparison = FormatConditionComparisonType.GreaterOrEqual;
                    icon4.Value = 25; // target range: 0% <= value < 25%
                    icon4.ValueComparison = FormatConditionComparisonType.GreaterOrEqual;
                    icon5.Value = 0; // target range: 0% <= value < 33%
                    icon5.ValueComparison = FormatConditionComparisonType.GreaterOrEqual;

                    //Add icons to the icon set.
                    iconSet.Icons.Add(icon1);
                    iconSet.Icons.Add(icon2);
                    iconSet.Icons.Add(icon3);
                    iconSet.Icons.Add(icon4);
                    iconSet.Icons.Add(icon5);
                    
                    //Specify the rule type.
                    gridFormatRule.Rule = formatConditionRuleIconSet;
                   
                    //Specify the column to which formatting is applied.

                    gridFormatRule.Column = gridView1.Columns[i];
                    //Add the formatting rule to the GridView.
                    gridView1.FormatRules.Add(gridFormatRule);

                    gridView1.Columns[i].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    gridView1.Columns[i].AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                    gridView1.Columns[i].OptionsColumn.AllowEdit = false;
                }
                gridView1.BestFitColumns();
                gridView1.OptionsView.ColumnAutoWidth = false;
               
            }
            else
            {
                gridControl1.Hide();
            }
          //  gridControl1.EndUpdate();
        }
        private void GoFullscreen()
        {
            this.WindowState = FormWindowState.Normal;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Bounds = Screen.PrimaryScreen.Bounds;
        }

        private void FRM_MUILTI_SKILL_V2_Load(object sender, EventArgs e)
        {
            GoFullscreen();
            lblDate.Text = string.Format(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            BindingData("001",line,Mline);
        }

        private void Cutting_Click(object sender, EventArgs e)
        {
           
        }

        private void Cutting_MouseClick(object sender, MouseEventArgs e)
        {
            
        }

        private void Cutting_MouseUp(object sender, MouseEventArgs e)
        {
           
        }

        private void xtraTabControl1_MouseUp(object sender, MouseEventArgs e)
        {
            try
            
            {
                progressPanel1.Visible = true;
                this.Cursor = Cursors.WaitCursor;

                XtraTabControl xtc = sender as XtraTabControl;

                Point pos = new Point(e.X, e.Y);

                DevExpress.XtraTab.ViewInfo.XtraTabHitInfo xthi = xtc.CalcHitInfo(pos);

                string tp = xthi.Page.Name;

                //  MessageBox.Show(tp + " is clicked!", "xtraTabControl1_MouseUp");
                switch (tp)
                {
                    case "Cutting":
                        BindingData("001", line, Mline);
                        break;
                    case "Nosew":
                        BindingData("002", line, Mline);
                        break;
                    case "HF":
                        BindingData("003", line, Mline);
                        break;
                    case "Stitching":
                        BindingData("004", line, Mline);
                        break;
                    case "Stockfit":
                        BindingData("005", line, Mline);
                        break;
                    case "Assembly":
                        BindingData("006", line, Mline);
                        break;
                    default:
                        break;
                }
               
                gridControl1.Show();
                xthi.Page.Controls.Add(gridControl1);
                gridControl1.Dock = DockStyle.Fill;
                this.Cursor = Cursors.Default;
                progressPanel1.Visible = false;
               
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default;
            progressPanel1.Visible = false;
            }
        }

        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.AbsoluteIndex >= 2)
                e.DisplayText = "";
        }

        private void tmrDate_Tick(object sender, EventArgs e)
        {
            lblDate.Text = string.Format(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        }

        private void FRM_MUILTI_SKILL_V2_VisibleChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
