﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FORM.UC
{
    public partial class UC_DWMY : UserControl
    {
        int _btnisDisable = 0;
        public UC_DWMY(int btnisDisable, string _lang)
        {
            InitializeComponent();
            _btnisDisable = btnisDisable;

            //Choose a button to disable
            switch (_btnisDisable)
            { 
                case 1:
                    btnDay.Enabled = false;
                    break;
                case 2:
                    btnWeek.Enabled = false;
                    break;
                case 3:
                    btnDay.Visible = false;
                    btnWeek.Visible = false;
                    btnMonth.Enabled = false;                    
                    btnYear.Enabled = true;
                    break;
                case 4:
                    btnDay.Visible = false;
                    btnWeek.Visible = false;
                    btnMonth.Enabled = true;                    
                    btnYear.Enabled = false;
                    break;
                case 5:
                    btnYear.Visible = false;
                    btnMonth.Visible = false;
                    btnWeek.Visible = false;
                    btnDay.Visible = false;
                    break;
                case 6:
                    btnYear.Visible = true;
                    btnMonth.Visible = true;
                    btnWeek.Visible = false;
                    btnDay.Visible = true;
                    break;
                case 7:
                    btnYear.Visible = true;
                    btnMonth.Visible = true;
                    btnWeek.Visible = false;
                    btnDay.Visible = false;
                    break;
                case 8:
                    btnYear.Visible = false;
                    btnMonth.Visible = true;
                    btnWeek.Visible = false;
                    btnDay.Visible = true;
                    break;
            }
            switch (_lang)
            {
                case "Vi":
                    btnDay.Text = "Ngày";
                    btnMonth.Text = "Tháng";
                    btnWeek.Text = "Tuần";
                    btnYear.Text = "Năm";
                    break;
                default:
                    btnDay.Text = "Day";
                    btnMonth.Text = "Month";
                    btnWeek.Text = "Week";
                    btnYear.Text = "Year";
                    break;
            }
        }
        public delegate void ButtonDWMYHandler(string ButtonCap, string ButtonCD);
        public ButtonDWMYHandler OnDWMYClick = null;

        public void YMD_Change(int btnisDisable, string _lang)
        {
            switch (btnisDisable)
            {
                case 1:
                    btnDay.Enabled = false;
                    break;
                case 2:
                    btnWeek.Enabled = false;
                    break;
                case 3:
                    btnMonth.Enabled = false;
                    break;
                case 4:
                    btnYear.Enabled = false;
                    break;
                case 5:
                    btnYear.Visible = false;
                    btnMonth.Visible = false;
                    btnWeek.Visible = false;
                    btnDay.Visible = false;
                    break;
                case 6:
                    btnYear.Visible = true;
                    btnMonth.Visible = true;
                    btnWeek.Visible = false;
                    btnDay.Visible = true;
                    break;
                case 7:
                    btnYear.Visible = true;
                    btnMonth.Enabled = true;
                    btnWeek.Visible = false;
                    btnDay.Visible = false;
                    break;
                case 8:
                    btnYear.Enabled = false;
                    btnMonth.Visible = true;
                    btnWeek.Visible = false;
                    btnDay.Visible = false;
                    break;
            }
            switch (_lang)
            {
                case "Vi":
                    btnDay.Text = "Ngày";
                    btnMonth.Text = "Tháng";
                    btnWeek.Text = "Tuần";
                    btnYear.Text = "Năm";
                    break;
                default:
                    btnDay.Text = "Day";
                    btnMonth.Text = "Month";
                    btnWeek.Text = "Week";
                    btnYear.Text = "Year";
                    break;
            }
        }

        private void btn_Click(object sender, EventArgs e)
        {
            if (OnDWMYClick != null)
                OnDWMYClick(((DevExpress.XtraEditors.SimpleButton)sender).Name.ToString(), ((DevExpress.XtraEditors.SimpleButton)sender).Tag.ToString());
           
            //btnDay.Enabled = true;
            //btnWeek.Enabled = true;
            //btnMonth.Enabled = true;
            //btnYear.Enabled = true;
            //((DevExpress.XtraEditors.SimpleButton)sender).Enabled = false;

            
            //switch (((DevExpress.XtraEditors.SimpleButton)sender).Tag.ToString())
            //{
            //    case "D":
            //        ((DevExpress.XtraEditors.SimpleButton)sender).Enabled = false;
            //        break;
            //    case "W":
            //        break;
            //    case "M":
            //        break;
            //    case "Y":
            //        break;
            //}
        }
    }
}
