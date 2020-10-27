using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using C1.Win.C1FlexGrid;
using System.Data.OracleClient;
using ChartDirector;

namespace FORM
{
    public partial class FRM_MAT_OUT_STATUS : Form
    {
        #region Variable
       
        private FlexSetting flgs = new FlexSetting();
      //  private string digitalText;
        int top_r = 1;

        //private int idx_form;
        #endregion

        #region method


        public FRM_MAT_OUT_STATUS()
        {
            InitializeComponent();
            
        }
  
        private void FlgHead_MAIN()
        {
            // fsp_Main
            flgs.judul_plan = new String[] { "", "", "NO", "", "FACTORY", "LEAN", "RES NO", "REQ DATE", "OUT DATE", "REQUEST", "ACTUAL", "BALANCE", "OVERFLOW", "", "STATUS", "" };

            fsp_Main.Rows.Count = 500;
            fsp_Main.Cols.Count = 16;
            fsp_Main.Rows.Fixed = 1;
            fsp_Main.Cols.Fixed = 1;

            fsp_Main.Font = new Font("Calibri", 22, FontStyle.Bold);
            flgs.FlgHead_MPlan(fsp_Main);

            fsp_Main.Rows[0].Height = 63;
            fsp_Main.Rows[0].StyleNew.Border.Width = 1;
            fsp_Main.Rows[0].StyleNew.Border.Color = Color.Black;
            fsp_Main.Rows[0].StyleNew.BackColor = Color.YellowGreen;
            fsp_Main.GetCellRange(0, 2).StyleNew.BackColor = Color.Gray;
            fsp_Main.GetCellRange(0, 14).StyleNew.BackColor = Color.Blue;
            fsp_Main.Rows[0].StyleNew.ForeColor = Color.White;

            fsp_Main.Cols[0].Width = 0;
            fsp_Main.Cols[1].Width = 0;
            fsp_Main.Cols[2].Width = 70;
            fsp_Main.Cols[3].Width = 0;
            fsp_Main.Cols[4].Width = 190;
            fsp_Main.Cols[5].Width = 160;
            fsp_Main.Cols[6].Width = 190;
            fsp_Main.Cols[7].Width = 200;
            fsp_Main.Cols[8].Width = 200;
            fsp_Main.Cols[9].Width = 180;
            fsp_Main.Cols[10].Width = 160;
            fsp_Main.Cols[11].Width = 160;
            fsp_Main.Cols[12].Width = 190;
            fsp_Main.Cols[13].Width = 0;
            fsp_Main.Cols[14].Width = 140;
            fsp_Main.Cols[15].Width = 0;

            fsp_Main.Cols[14].StyleNew.BackColor = Color.Gray;

            fsp_Main.Rows[0].TextAlign = TextAlignEnum.CenterCenter;
            fsp_Main.Cols[2].TextAlign = TextAlignEnum.CenterCenter;
            fsp_Main.Cols[5].TextAlign = TextAlignEnum.LeftCenter;
            fsp_Main.Cols[6].TextAlign = TextAlignEnum.CenterCenter;
            fsp_Main.Cols[7].TextAlign = TextAlignEnum.CenterCenter;
            fsp_Main.Cols[8].TextAlign = TextAlignEnum.CenterCenter;
            fsp_Main.Cols[9].TextAlign = TextAlignEnum.RightCenter;
            fsp_Main.Cols[10].TextAlign = TextAlignEnum.RightCenter;
            fsp_Main.Cols[11].TextAlign = TextAlignEnum.RightCenter;
            fsp_Main.Cols[14].TextAlign = TextAlignEnum.LeftCenter;
            for (int i = 6; i < 10; i++ )
                fsp_Main.Cols[i].Format = "#,###";

            for (int a = 0; a < fsp_Main.Cols.Count; a++)
            {
                fsp_Main.Cols[a].StyleNew.Border.Width = 1;
                fsp_Main.Cols[a].StyleNew.Border.Color = Color.Black;
            }
        }

      
        #endregion

        private void FRM_MAT_IN_STATUS_Load(object sender, EventArgs e)
        {
            InitForm();
        }
        private void GoFullscreen(bool fullscreen)
        {
            if (fullscreen)
            {
                /*
                this.WindowState = FormWindowState.Normal;
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                this.Bounds = Screen.PrimaryScreen.Bounds;
                */
                Screen[] screens = Screen.AllScreens;
                this.WindowState = FormWindowState.Normal;
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                if (screens.Length == 1)
                    this.Bounds = Screen.PrimaryScreen.Bounds;
                else if (screens.Length == 2)
                    this.Bounds = screens[1].Bounds;
                //else
                //    this.Bounds = screens[2].Bounds;
            }
            else
            {
                this.WindowState = FormWindowState.Maximized;
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            }
        }

        private void InitForm()
        {
            GoFullscreen(true);
          //  Com_Base.Functions.Form_Maximized(this, int.Parse(Com_Base.Variables.Form[idx_form]["monitor"]));

            //lblDate.Text = string.Format(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            tmr_reload.Interval = 300 * 1000;
            tmr_reload.Start();

            txttemp.Focus();           

            //Select data
            FlgHead_MAIN();
            SearchData();

            lblDate.Text = DateTime.Now.ToString("yyyy-MM-dd \n HH:mm:ss");

            timerChangeTime.Start();
        }
       
        private void GridControl()
        {
            fsp_Main.Rows[0].StyleNew.Font = new Font("Calibri", 25, FontStyle.Bold);
            for (int i = 1; i < fsp_Main.Rows.Count; i++)
            {
                fsp_Main.Rows[i].Height = 54;
                //fsp_Main.GetCellRange(i, 7).StyleNew.ForeColor = Color.Yellow;

                if (fsp_Main[i, 14].ToString() == "Complete")
                    fsp_Main.GetCellRange(i, fsp_Main.Cols.Count - 2).StyleNew.ForeColor = Color.Lime;
                else if (fsp_Main[i, fsp_Main.Cols.Count - 2].ToString() == "Processing")
                    fsp_Main.GetCellRange(i, fsp_Main.Cols.Count - 2).StyleNew.ForeColor = Color.Yellow;
                else if (fsp_Main[i, fsp_Main.Cols.Count - 2].ToString() == "Schedule")
                    fsp_Main.GetCellRange(i, fsp_Main.Cols.Count - 2).StyleNew.ForeColor = Color.Black;
                else
                    fsp_Main.GetCellRange(i, fsp_Main.Cols.Count - 2).StyleNew.ForeColor = Color.Red;
            }
          
        }
       
        private void Clear_FlexGrid(COM.FSP arg_Flex)
        {
            if (arg_Flex.Rows.Fixed != arg_Flex.Rows.Count)
            {
                arg_Flex.Clear(ClearFlags.UserData, arg_Flex.Rows.Fixed, 1, arg_Flex.Rows.Count - 1, arg_Flex.Cols.Count - 1);
                arg_Flex.Rows.Count = arg_Flex.Rows.Fixed;
            }
        }

        private void tmr_reload_Tick(object sender, EventArgs e)
        {
            this.UseWaitCursor = true;
            SearchData();
            this.fsp_Main.Hide();

           // MRS.ClassLib.WinAPI.AnimateWindow(this.fsp_Main.Handle, 1000, MRS.ClassLib.Common.getSlidType("4"));
            this.fsp_Main.Show();


            this.UseWaitCursor = false;
        }

        private void SearchData()
        {
            try
            {
                lblCreate.Text = "Create On   " + DateTime.Now.ToString("yyyy-MM-dd 0:0:0");
                timerScroll.Stop();
                fsp_Main.Rows.Count = 23;
                //Search MAIN data

                DataTable vDt = SELECT_DATA("1");
                if (vDt == null || vDt.Rows.Count == 0)
                    return;

                Clear_FlexGrid(fsp_Main);
                if (vDt.Rows.Count > 0)
                {
                    lblCreate.Text = "Create On   " + vDt.Rows[0][0].ToString();
                    lblComp.Text = "Completed   " + vDt.Rows[0][1].ToString();
                    lblPros.Text = "Prosessing   " + vDt.Rows[0][2].ToString();
                    lblSched.Text = "Schedule   " + vDt.Rows[0][3].ToString();
                }

                DataTable vDt1 = SELECT_DATA("2");
                if (vDt1 == null || vDt1.Rows.Count == 0)
                {
                    fsp_Main.Rows.Count = 25;
                    return;
                }

                if (vDt1.Rows.Count > 0)
                {
                    for (int i = 0; i < vDt1.Rows.Count; i++)
                    {
                        fsp_Main.AddItem(vDt1.Rows[i].ItemArray, fsp_Main.Rows.Count, 0);
                    }
                    fsp_Main.ExtendLastCol = true;
                }
                if (vDt1.Rows.Count > 23)
                    fsp_Main.Rows.Count = vDt1.Rows.Count;
                else
                    fsp_Main.Rows.Count = 23;

                vDt1.Dispose();

                GridControl();
                timerScroll.Start();
            }
            catch
            {
            }
        }

        private DataTable SELECT_DATA(string div)
        {
            COM.OraDB MyOraDB = new COM.OraDB();
            System.Data.DataSet ds_ret;
            string process_name = null;
            try
            {
                if(div == "1")
                    process_name = "PKG_MATERIAL_REPORT.SEL_OUT_MONITOR_HEAD";
                else
                    process_name = "PKG_MATERIAL_REPORT.SEL_OUT_MONITOR_LEAN";

                MyOraDB.ReDim_Parameter(2);
                MyOraDB.Process_Name = process_name;

                MyOraDB.Parameter_Name[0] = "ARG_WERKS";
                MyOraDB.Parameter_Name[1] = "OUT_CURSOR";

                MyOraDB.Parameter_Type[0] = (int)OracleType.Char;
                MyOraDB.Parameter_Type[1] = (int)OracleType.Cursor;

                //MyOraDB.Parameter_Values[0] = "2110"; // Vinh Cuu
                MyOraDB.Parameter_Values[0] = "2120"; // Long Thanh
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

        public double[] data2 { get; set; }

        private void FRM_MAT_IN_STATUS_Closing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void tmr_detail_Tick(object sender, EventArgs e)
        {
            //lblDate.Text = string.Format(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        }

        private void timerScroll_Tick(object sender, EventArgs e)
        {
            top_r = top_r + 16;
            if (top_r >= fsp_Main.Rows.Count)
                top_r = 1;
            fsp_Main.TopRow = top_r;
            
        }

        private void timerChangeForm_Tick(object sender, EventArgs e)
        {
        }

        private void timerChangeTime_Tick(object sender, EventArgs e)
        {
            lblDate.Text = DateTime.Now.ToString("yyyy-MM-dd \n HH:mm:ss");
        }

        private void lblDate_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
