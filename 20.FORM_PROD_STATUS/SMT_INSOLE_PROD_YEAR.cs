using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OracleClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
//using JPlatform.Client.Controls;


namespace FORM
{
    public partial class SMT_INSOLE_PROD_YEAR : Form
    {
        public SMT_INSOLE_PROD_YEAR()
        {
            InitializeComponent();
        }

        int cnt = 0;
        string str_op = "";
        public delegate void MenuHandler();
        public MenuHandler OnClick = null;
        DataTable _dtXML = null;
        #region db
        Database db = new Database();
        #endregion
        #region UC
        UC.UC_DWMY uc = new UC.UC_DWMY(4);
        #endregion

        private void FRM_ROLL_SLABTEST_MON_Load(object sender, EventArgs e)
        {
            _dtXML = ComVar.Func.ReadXML(Application.StartupPath + @"\InitForm.xml", this.GetType().Name);
            timer1.Enabled = true;
            timer1.Start();
            timer1.Interval = 1000;
            pnYMD.Controls.Add(uc);
            uc.OnDWMYClick += DWMYClick;
            uc.YMD_Change(6);
        }

        void DWMYClick(string ButtonCap, string ButtonCD)
        {
            //MessageBox.Show(ButtonCap + "    " + ButtonCD);
            switch (ButtonCD)
            {
                case "C":
                    ComVar.Var.callForm = _dtXML.Rows[0]["frmHome"].ToString();
                    break;
                case "D":
                    ComVar.Var.callForm = _dtXML.Rows[0]["frmDay"].ToString();
                    //this.Close();
                    //Form fc = Application.OpenForms["FRM_SMT_OS_PROD_DAILY"];
                    //if (fc != null)
                    //    fc.Show();
                    //else
                    //{
                    //    SMT_INSOLE_PROD_DAILY f = new SMT_INSOLE_PROD_DAILY();
                    //    f.Show();
                    //}
                    break;
                case "M":

                    //this.Close();
                    //Form fc1 = Application.OpenForms["FRM_SMT_OS_PROD_MONTH"];
                    //if (fc1 != null)
                    //    fc1.Show();
                    //else
                    //{
                    //    SMT_INSOLE_PROD_MONTH f1 = new SMT_INSOLE_PROD_MONTH();
                    //    f1.Show();
                    //}
                    break;
                case "Y":
                    ComVar.Var.callForm = _dtXML.Rows[0]["frmYear"].ToString();
                    //this.Close();
                    //Form fc2 = Application.OpenForms["FRM_SMT_OS_PROD_YEAR"];
                    //if (fc2 != null)
                    //    fc2.Show();
                    //else
                    //{
                    //    SMT_INSOLE_PROD_YEAR f2 = new SMT_INSOLE_PROD_YEAR();
                    //    f2.Show();
                    //}
                    break;
            }
        }

        private void formatband()
        {
            try
            {
                int n;
                DataTable dtsource = null;
                dtsource = db.SEL_OS_PROD_YEAR("H", uc_year.GetValue().ToString(), "");
                if (dtsource != null && dtsource.Rows.Count > 0)
                {
                    string name;
                    //bandYear.Caption = dtsource.Rows[0]["MON"].ToString();
                    if (dtsource.Rows.Count > 0)
                    {
                        foreach (DevExpress.XtraGrid.Views.BandedGrid.GridBand band in gvwView.Bands[1].Children)
                        {
                            double num;
                            if (double.TryParse(band.Caption, out num))
                            {
                                for (int i = 0; i < dtsource.Rows.Count; i++)
                                {
                                    if (band.Name.Contains(dtsource.Rows[i][0].ToString().Substring(dtsource.Rows[i][0].ToString().Length - 2)))
                                    {
                                        band.Visible = true;
                                        break;
                                    }
                                    if (i == dtsource.Rows.Count - 1)
                                    {
                                        band.Visible = false;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
                return;
            }
        }

        private void BindingData(string arg_op)
        {
            grdView.Refresh();
            DataTable dtsource = null;
            dtsource = db.SEL_OS_PROD_YEAR("Q", uc_year.GetValue().ToString(), arg_op);
            //formatband();
            grdView.DataSource = dtsource;
            if (dtsource != null && dtsource.Rows.Count > 0)
            {

                bandYear.Caption = dtsource.Rows[0]["YYYY"].ToString();
                for (int i = 0; i < gvwView.Columns.Count; i++)
                {
                    gvwView.Columns[i].OptionsColumn.ReadOnly = true;
                    gvwView.Columns[i].OptionsColumn.AllowEdit = false;
                    gvwView.Columns[i].OptionsFilter.AllowFilter = false;
                    gvwView.Columns[i].OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
                    gvwView.Columns[i].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    gvwView.Columns[i].AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                    if (i > 0)
                    {
                        gvwView.Columns[i].AppearanceCell.Font = new System.Drawing.Font("Calibri", 14, FontStyle.Bold);
                    }
                }
            }
        }

        private void bindingdatachart(string arg_op)
        {
            DataTable dt = null;
            dt = db.SEL_OS_PROD_YEAR("C", uc_year.GetValue().ToString(), arg_op);
            chartSlabtest.DataSource = dt;
            chartSlabtest.Series[0].ArgumentDataMember = "YMD";
            chartSlabtest.Series[0].ValueDataMembers.AddRange(new string[] { "PLAN_QTY" });
            chartSlabtest.Series[1].ArgumentDataMember = "YMD";
            chartSlabtest.Series[1].ValueDataMembers.AddRange(new string[] { "PROD_QTY" });            
            chartSlabtest.Series[2].ArgumentDataMember = "YMD";
            chartSlabtest.Series[2].ValueDataMembers.AddRange(new string[] { "POD" });
            chartSlabtest.Series[2].Name = "PMD";
            //chartSlabtest.
        }

        private void gvwView_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.Column.ColumnHandle == 1)
            {
                e.Appearance.BackColor = Color.LightGray;//Color.FromArgb(80, 209, 244);
                e.Appearance.ForeColor = Color.Black;
                e.Appearance.Font = new System.Drawing.Font("Calibri", 16, FontStyle.Bold);
            }
            else
            {
                
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblDateTime.Text = string.Format(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            if (cnt < 40)
            {
                cnt++;                
            }
            else
            {
                cnt = 0;
                BindingData("OSP");
                bindingdatachart("OSP");                
            }
        }

        private void FRM_ROLL_SLABTEST_MON_VisibleChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.Visible)
                {                             
                   
                    timer1.Start();
                    cnt = 40;
                }
                else
                    timer1.Stop();
            }
            catch
            {

            }
        }

        private void uC_YEAR_SELECTION1_ValueChangeEvent(object sender, EventArgs e)
        {
            try
            {
                BindingData("OSP");
                bindingdatachart("OSP");       
            }
            catch
            {

            }
        }       
    }
}
