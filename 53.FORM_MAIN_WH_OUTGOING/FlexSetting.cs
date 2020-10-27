using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using C1.Win.C1FlexGrid;
using System.Windows.Forms;

namespace FORM
{
    class FlexSetting
    {
        #region variable
        public string[] judul;
        public string[] judul_plan;
        public string[] judul_Yplan;
        public string[] DayOfWeek_Detail;
        public string[] DayOfWeek_Detail_Out;
        public string[] judulSum;
        public string[] judulYYYYMMDD_In;
        public string[] judulYYYYMMDD_Out;
        public string[] judul_Detail;
        public string[] judul_Detail_Out;
        public string[] judul_FSP;
        public string[] judul_FSP_Out;
        public string[] judul_FSP_Type;
        public string[] judul_FSP_Type_Out;
        #endregion

        #region function

        public int ToInt(double dbl)
        {
            return Convert.ToInt16(Math.Ceiling(dbl));
        }

        #endregion

        #region procedure

        public void FlgHead(C1FlexGrid flg)
        {
            flg.Clear();
            flg.AllowAddNew = false;
            flg.AllowDelete = false;
            flg.AllowEditing = false;
            flg.AllowFiltering = false;
            flg.AllowDragging = 0;
            flg.AllowFreezing = 0;
            flg.AllowSorting = 0;
            flg.AllowResizing = 0;
            int x = 0;
            if (judul.Length > flg.Cols.Count)
                for (int i = 0; i <= flg.Rows.Fixed - 1; i++)
                    for (int j = 0; j <= flg.Cols.Count - 1; j++)
                    {
                        flg[i, j] = judul[x];
                        x++;
                    }
            else
                for (int i = 0; i <= flg.Cols.Count-1; i++)
                {
                    flg[0, i] = judul[i];
                }
        }
        public void SetColorCondition_Detail(C1FlexGrid flg)
        {
            //string[] s;
            int iColor;
            for (int i = 5; i < 10; i++)
            {
                for (int j = 2; j < flg.Rows.Count; j++)
                {
                    if (flg[j, i].ToString().Trim() != "")
                    {
                        if (flg[j, i].ToString().Split('/').ToString() != "-1")
                        {

                            iColor = isColor(flg[j, i].ToString());
                            if (iColor == 0)
                            {
                                CellStyle newcs = flg.Styles.Add("IsColorGreen");
                               // newcs.BackgroundImage = Properties.Resources.Glass_Green;
                                newcs.BackgroundImageLayout = ImageAlignEnum.TileStretch;
                                newcs.ForeColor = Color.Black;
                                newcs.Font = new Font("Malgun Gothic", 14, FontStyle.Bold);
                                flg.SetCellStyle(j, i, newcs);
                            }
                            else
                            {
                                CellStyle newcs = flg.Styles.Add("IsColorRed");
                                //newcs.BackgroundImage = Properties.Resources.Glass_Red;
                                newcs.BackgroundImageLayout = ImageAlignEnum.TileStretch;
                                newcs.ForeColor = Color.Black;
                                newcs.Font = new Font("Malgun Gothic", 14, FontStyle.Bold);
                                flg.SetCellStyle(j, i, newcs);
                            }
                        }


                    }

                }
            }


            for (int i = 10; i < 16; i++)
            {
                for (int j = 2; j < flg.Rows.Count; j++)
                {
                    if (flg[j, i].ToString().Trim() != "")
                    {
                        if (flg[j, i].ToString().Split('/').ToString() != "-1")
                        {

                            iColor = isColor(flg[j, i].ToString());
                            if (iColor == 0)
                            {
                                CellStyle newcs = flg.Styles.Add("IsColorGreen");
                               // newcs.BackgroundImage = Properties.Resources.Glass_Green;
                                newcs.BackgroundImageLayout = ImageAlignEnum.TileStretch;
                                newcs.ForeColor = Color.Black;
                                newcs.Font = new Font("Malgun Gothic", 14, FontStyle.Bold);
                                flg.SetCellStyle(j, i, newcs);
                            }
                            else
                            {
                                CellStyle newcs = flg.Styles.Add("IsColorYellow");
                              //  newcs.BackgroundImage = Properties.Resources.Glass_Yellow;
                                newcs.BackgroundImageLayout = ImageAlignEnum.TileStretch;
                                newcs.ForeColor = Color.Black;
                                newcs.Font = new Font("Malgun Gothic", 14, FontStyle.Bold);
                                flg.SetCellStyle(j, i, newcs);
                            }
                        }


                    }

                }
            }
        }


        public void FlgSetting_Issue(C1FlexGrid flg)
        {

            CellStyle cs = flg.Styles[CellStyleEnum.Normal];
            //cs.BackgroundImage = Properties.Resources.Glass_Red;
            cs.Margins = new System.Drawing.Printing.Margins(0, 0, 0, 0);
            cs.WordWrap = true;
            cs.TextAlign = TextAlignEnum.CenterCenter;

            cs.ForeColor = Color.Black;
            cs.Border.Style = BorderStyleEnum.Flat;
            cs.Border.Color = Color.Black;
            cs.Border.Width = 1;

            cs = flg.Styles[CellStyleEnum.Alternate];
          //  cs.BackgroundImage = Properties.Resources.Glass_Alternate;
            cs.BackgroundImageLayout = ImageAlignEnum.TileStretch;


            cs = flg.Styles[CellStyleEnum.Fixed];
            cs.BackColor = Color.LightGray;
            cs.ForeColor = Color.Blue;
            cs.Font = new Font("Calibri", 15, FontStyle.Regular);
            cs.Border.Style = BorderStyleEnum.Flat;
            cs.Border.Color = Color.Black;

            cs = flg.Styles[CellStyleEnum.Highlight];
            cs.BackColor = Color.LightSkyBlue;
            cs.ForeColor = Color.White;
            cs.Font = new Font("Calibri", 16, FontStyle.Bold);

        }


        public void FlgSetting_LL(C1FlexGrid flg)
        {
            CellStyle cs = flg.Styles[CellStyleEnum.Normal];
            //cs.BackgroundImage = Properties.Resources.Glass_Normal;
            //cs.BackgroundImageLayout = ImageAlignEnum.TileStretch;            
            cs.Margins = new System.Drawing.Printing.Margins(0, 0, 0, 0);
            cs.WordWrap = true;
            cs.TextAlign = TextAlignEnum.CenterCenter;
            //cs.ForeColor = Color.Black;
            //cs.ForeColor = Color.White;
            cs.ForeColor = Color.Black;
            cs.Border.Style = BorderStyleEnum.Flat;
            //cs.Border.Color = Color.Silver;
            cs.Border.Color = Color.Black;
            cs.Border.Width = 1;

            cs = flg.Styles[CellStyleEnum.Alternate];
          //  cs.BackgroundImage = Properties.Resources.Glass_Alternate;
            cs.BackgroundImageLayout = ImageAlignEnum.TileStretch;


            cs = flg.Styles[CellStyleEnum.Fixed];
            //cs.BackColor = Color.DarkGray;            
            cs.BackColor = Color.LightSkyBlue;
            //cs.ForeColor = Color.White;
            cs.ForeColor = Color.White;
            cs.Font = new Font("Calibri", 15, FontStyle.Regular);
            cs.Border.Style = BorderStyleEnum.Flat;
            cs.Border.Color = Color.Black;

            cs = flg.Styles[CellStyleEnum.Highlight];
            cs.BackColor = Color.LightSkyBlue;//.Transparent;
            cs.ForeColor = Color.White;//flg.Styles[CellStyleEnum.Normal].ForeColor;
            cs.Font = new Font("Calibri", 16, FontStyle.Bold);

        }

        public void FlgSetting_Issue_Out(C1FlexGrid flg)
        {
            CellStyle cs = flg.Styles[CellStyleEnum.Normal];
            //cs.BackgroundImage = Properties.Resources.Glass_Normal;
            //cs.BackgroundImageLayout = ImageAlignEnum.TileStretch;            
            cs.Margins = new System.Drawing.Printing.Margins(0, 0, 0, 0);
            cs.WordWrap = true;
            cs.TextAlign = TextAlignEnum.CenterCenter;
            //cs.ForeColor = Color.Black;
            //cs.ForeColor = Color.White;
            cs.ForeColor = Color.Black;
            cs.Border.Style = BorderStyleEnum.Flat;
            //cs.Border.Color = Color.Silver;
            cs.Border.Color = Color.Black;
            cs.Border.Width = 1;

            cs = flg.Styles[CellStyleEnum.Alternate];
        //    cs.BackgroundImage = Properties.Resources.Glass_Alternate;
            cs.BackgroundImageLayout = ImageAlignEnum.TileStretch;


            cs = flg.Styles[CellStyleEnum.Fixed];
            //cs.BackColor = Color.DarkGray;            
            cs.BackColor = Color.LightGray;
            //cs.ForeColor = Color.White;
            cs.ForeColor = Color.Blue;
            cs.Font = new Font("Calibri", 15, FontStyle.Regular);
            cs.Border.Style = BorderStyleEnum.Flat;
            cs.Border.Color = Color.Black;

            cs = flg.Styles[CellStyleEnum.Highlight];
            cs.BackColor = Color.LightSkyBlue;//.Transparent;
            cs.ForeColor = Color.White;//flg.Styles[CellStyleEnum.Normal].ForeColor;
            cs.Font = new Font("Calibri", 16, FontStyle.Bold);

        }


        public void FlgHeader_Detail(C1FlexGrid flg)
        {
            flg.Clear();
            flg.AllowAddNew = false;
            flg.AllowDelete = false;
            flg.AllowEditing = false;
            flg.AllowFiltering = false;
            flg.AllowDragging = 0;
            flg.AllowFreezing = 0;
            flg.AllowSorting = 0;
            flg.AllowResizing = 0;
            int x = 0;
            if (judul_Detail.Length > flg.Cols.Count)
                for (int i = 0; i <= flg.Rows.Fixed - 1; i++)
                    for (int j = 0; j <= flg.Cols.Count - 1; j++)
                    {
                        flg[i, j] = judul_Detail[x];
                        x++;
                    }
            else
                for (int i = 0; i <= flg.Cols.Count - 1; i++)
                {
                    flg[0, i] = judul_Detail[i];
                }
        }


        public void FlgHeader_FSP(C1FlexGrid flg)
        {
            flg.Clear();
            flg.AllowAddNew = false;
            flg.AllowDelete = false;
            flg.AllowEditing = false;
            flg.AllowFiltering = false;
            flg.AllowMerging = AllowMergingEnum.Free;
            flg.AllowDragging = 0;
            flg.AllowFreezing = 0;
            flg.AllowSorting = 0;
            flg.AllowResizing = 0;
            int x = 0;
            if (judul_FSP.Length > flg.Cols.Count)
                for (int i = 0; i <= flg.Rows.Fixed - 1; i++)
                    for (int j = 0; j <= flg.Cols.Count - 1; j++)
                    {
                        flg[i, j] = judul_FSP[x];
                        x++;
                    }
            else
                for (int i = 0; i <= flg.Cols.Count - 1; i++)
                {
                    flg[0, i] = judul_FSP[i];
                }
        }

        public void FlgHeader_FSP_Out(C1FlexGrid flg)
        {
            flg.Clear();
            flg.AllowAddNew = false;
            flg.AllowDelete = false;
            flg.AllowEditing = false;
            flg.AllowFiltering = false;
            flg.AllowDragging = 0;
            flg.AllowFreezing = 0;
            flg.AllowSorting = 0;
            flg.AllowResizing = 0;
            int x = 0;
            if (judul_FSP_Out.Length > flg.Cols.Count)
                for (int i = 0; i <= flg.Rows.Fixed - 1; i++)
                    for (int j = 0; j <= flg.Cols.Count - 1; j++)
                    {
                        flg[i, j] = judul_FSP_Out[x];
                        x++;
                    }
            else
                for (int i = 0; i <= flg.Cols.Count - 1; i++)
                {
                    flg[0, i] = judul_FSP_Out[i];
                }
        }


        public void FlgHeader_Sum(C1FlexGrid flg)
        {
            flg.Clear();
            flg.AllowAddNew = false;
            flg.AllowDelete = false;
            flg.AllowEditing = false;
            flg.AllowFiltering = false;
            flg.AllowDragging = 0;
            flg.AllowFreezing = 0;
            flg.AllowSorting = 0;
            flg.AllowResizing = 0;

            int x = 0;
            if (judulSum.Length > flg.Cols.Count)
                for (int i = 0; i <= flg.Rows.Fixed - 1; i++)
                    for (int j = 0; j <= flg.Cols.Count - 1; j++)
                    {
                        flg[i, j] = judulSum[x];
                        x++;
                    }
            else
                for (int i = 0; i <= flg.Cols.Count - 1; i++)
                {
                    flg[0, i] = judulSum[i];
                }
        }

        public void FlgHeader_Detail_Out(C1FlexGrid flg)
        {
            flg.Clear();
            flg.AllowAddNew = false;
            flg.AllowDelete = false;
            flg.AllowEditing = false;
            flg.AllowFiltering = false;
            flg.AllowDragging = 0;
            flg.AllowFreezing = 0;
            flg.AllowSorting = 0;
            flg.AllowResizing = 0;
            int x = 0;
            if (judul_Detail_Out.Length > flg.Cols.Count)
                for (int i = 0; i <= flg.Rows.Fixed - 1; i++)
                    for (int j = 0; j <= flg.Cols.Count - 1; j++)
                    {
                        flg[i, j] = judul_Detail_Out[x];
                        x++;
                    }
            else
                for (int i = 0; i <= flg.Cols.Count - 1; i++)
                {
                    flg[0, i] = judul_Detail_Out[i];
                }
        }


        public void FlgHead_MPlan(C1FlexGrid flg)
        {
            flg.Clear();
            flg.AllowAddNew = false;
            flg.AllowDelete = false;
            flg.AllowEditing = false;
            flg.AllowFiltering = false;
            flg.AllowDragging = 0;
            flg.AllowFreezing = 0;
            flg.AllowSorting = 0;
            flg.AllowResizing = 0;
            int x = 0;
            if (judul_plan.Length > flg.Cols.Count)
                for (int i = 0; i <= flg.Rows.Fixed - 1; i++)
                    for (int j = 0; j <= flg.Cols.Count - 1; j++)
                    {
                        flg[i, j] = judul_plan[x];
                        x++;
                    }
            else
                for (int i = 0; i <= flg.Cols.Count - 1; i++)
                {
                    flg[0, i] = judul_plan[i];
                }
        }

        public void FlgHead_YPlan(C1FlexGrid flg)
        {
            flg.Clear();
            flg.AllowAddNew = false;
            flg.AllowDelete = false;
            flg.AllowEditing = false;
            flg.AllowFiltering = false;
            flg.AllowDragging = 0;
            flg.AllowFreezing = 0;
            flg.AllowSorting = 0;
            flg.AllowResizing = 0;
            int x = 0;
            if (judul_Yplan.Length > flg.Cols.Count)
                    for (int j = 0; j <= flg.Cols.Count - 1; j++)
                    {
                        flg[0, j] = judul_Yplan[x];
                        x++;
                    }
            else
                for (int i = 0; i <= flg.Cols.Count - 1; i++)
                {
                    flg[0, i] = judul_Yplan[i];
                }
        }

        public void AutoWidth(C1FlexGrid flg, int startCol, int endCol)
        {
            try
            {
                int widthAmount = 0;
                for (int i = 0; i <= flg.Cols.Count - 1; i++)
                {
                    if ((i < startCol) || (i > endCol))
                        widthAmount += flg.Cols[i].Width;
                }

                int widthTotal = 4;
                for (int i = startCol; i <= endCol; i++)
                {
                    flg.Cols[i].Width = ToInt((flg.Width - widthAmount) / (flg.Cols.Count - startCol));
                    widthTotal += flg.Cols[i].Width;
                }
                flg.Cols[0].Width = ToInt(flg.Cols[0].Width + ((flg.Width - widthAmount)
                    - widthTotal));
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void AutoHeight(C1FlexGrid flg, int startRow, int endRow)
        {
            try
            {
                int heightAmount = 0;
                for (int i = 0; i <= flg.Rows.Count - 1; i++)
                {
                    if ((i < startRow) || (i > endRow))
                        heightAmount += flg.Rows[i].Height;
                }
                int heightTotal = 4;
                for (int i = startRow; i <= endRow; i++)
                {
                    flg.Rows[i].Height = ToInt((flg.Height - heightAmount) / (flg.Rows.Count - startRow));
                    heightTotal += flg.Rows[i].Height;
                }
                flg.Rows[0].Height = ToInt(flg.Rows[0].Height + ((flg.Height - heightAmount)
                    - heightTotal));
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void ColsType(C1FlexGrid flg, int StartCols, int EndCols, String type)
        {
            for (int i = StartCols; i <= EndCols; i++)
                flg.Cols[i].Style = flg.Styles[type];

        }

        public void RowsType(C1FlexGrid flg, int StartRows, int EndRows, String type)
        {
            for (int i = StartRows; i <= EndRows; i++)
                flg.Rows[i].Style = flg.Styles[type];
        }

        public void FlgSetting_L(C1FlexGrid flg)
        {
            CellStyle cs = flg.Styles[CellStyleEnum.Normal];
          //  cs.BackgroundImage = Properties.Resources.Glass_Normal;
            cs.BackgroundImageLayout = ImageAlignEnum.TileStretch;
            cs.Margins = new System.Drawing.Printing.Margins(0, 0, 0, 0);
            cs.Border.Style = BorderStyleEnum.None;
            cs.WordWrap = true;
            //cs.TextAlign = TextAlignEnum.CenterCenter;
            cs.ForeColor = Color.Black;

            cs = flg.Styles[CellStyleEnum.Alternate];
           // cs.BackgroundImage = Properties.Resources.Glass_Alternate;
            cs.BackgroundImageLayout = ImageAlignEnum.TileStretch;


            cs = flg.Styles[CellStyleEnum.Fixed];
            //cs.BackgroundImage = Properties.Resources.Glass_Header;
           // cs.BackgroundImage = Properties.Resources.Total_color;
            
            //cs.BackColor = Color.Gray;

            //cs.BackgroundImage = Properties.Resources.Blue;
            cs.BackgroundImageLayout = ImageAlignEnum.TileStretch;
            cs.ForeColor = Color.White;
            cs.Font = new Font(flg.Font.Name, flg.Font.Size, flg.Font.Style);
            cs.Border.Style = BorderStyleEnum.None;

            cs = flg.Styles[CellStyleEnum.Highlight];
            cs.BackColor = Color.Green;//.Transparent;
            cs.ForeColor = flg.Styles[CellStyleEnum.Normal].ForeColor;
        }


        public void FlgSetting_Today_N(C1FlexGrid flg, String strToday, int n, String []strList)
        {
            for (int i = 0; i < flg.Cols.Count - 1; i++)
            {
                if (flg[0, i].ToString() == strToday)
                {
                    for (int j = 0; j < n; j++)
                    {
                        CellStyle newcs = flg.Styles.Add("TodayN");
                     //   newcs.BackgroundImage = Properties.Resources.Glass_Purple;
                        newcs.BackgroundImageLayout = ImageAlignEnum.TileStretch;
                        if (strList[i + j].ToString() == "Sun")
                            newcs.ForeColor = Color.Red;
                        else
                           newcs.ForeColor = Color.Black;
                        flg.SetCellStyle(0, i+j, newcs);
                    }
                    i = i + n;
                }
                

            }

        }

        public void FlgStyle(C1FlexGrid flg)
        {
            CellStyle cs = flg.Styles.Add("Double");
            cs.Format = "#,##0";
            cs.DataType = Type.GetType("System.Double");

            cs = flg.Styles.Add("Decimal");
            cs.Format = "#,##0.00";
            cs.DataType = Type.GetType("System.Double");

            cs = flg.Styles.Add("String");
            cs.Format = "LLLL";
            cs.DataType = Type.GetType("System.String");

            cs = flg.Styles.Add("BgMerah");
       //     cs.BackgroundImage = Properties.Resources.bg_merah;

            cs = flg.Styles.Add("BgBiru");
       //     cs.BackgroundImage = Properties.Resources.bg_biru;

            cs = flg.Styles.Add("BgKuning");
         //   cs.BackgroundImage = Properties.Resources.bg_kuning;

            cs = flg.Styles.Add("Remark");
            cs.Font = new Font(flg.Font.Name, 24, FontStyle.Bold);

            cs = flg.Styles.Add("Bold");
            cs.Font = new Font(flg.Font.Name, flg.Font.Size, FontStyle.Bold);
        }

        public void SetColorCondition(C1FlexGrid flg, String strToday, int n)
        {
            int iColor;
            for (int i = 5; i < flg.Cols.Count; i++)
            {
                for (int j = 1; j < flg.Rows.Count; j++)
                {
                    if (flg[j,i]!=null)
                    {

                        if (flg[j, i].ToString().IndexOf("/").ToString() != "-1")
                        {
                            iColor = isColor(flg[j, i].ToString());
                            if (iColor == 1)
                            {
                                if (i < 18)
                                {

                                    CellStyle newcs = flg.Styles.Add("IsColorRed");
                                 //   newcs.BackgroundImage = Properties.Resources.Glass_Red;
                                    newcs.BackgroundImageLayout = ImageAlignEnum.TileStretch;
                                    flg.SetCellStyle(j, i, newcs);
                                }
                                else
                                    if (i < 21)
                                    {
                                        CellStyle newcs = flg.Styles.Add("IsColorYellow");
                                    //    newcs.BackgroundImage = Properties.Resources.Glass_Yellow;
                                        newcs.BackgroundImageLayout = ImageAlignEnum.TileStretch;
                                        flg.SetCellStyle(j, i, newcs);
                                    }
                            }
                            else
                                if (i < 21)
                                {
                                    CellStyle newcs = flg.Styles.Add("IsColorGreen");
                                //    newcs.BackgroundImage = Properties.Resources.Glass_Green;
                                    newcs.BackgroundImageLayout = ImageAlignEnum.TileStretch;
                                    flg.SetCellStyle(j, i, newcs);
                                }

                        }

                    }

                }
            }

        }


        public void SetColorCondition_New(C1FlexGrid flg)
        {
            string[] s;
            for (int i = 2; i < flg.Cols.Count; i++)
            {
                for (int j = 1; j < flg.Rows.Count -1 ; j++)
                {
                    if (flg[j, i] != null)
                    {
                        s = flg[j, i].ToString().Split('%');
                        if (s[0].Trim() == "100")
                        {
                            CellStyle newcs = flg.Styles.Add("IsColorGreen");
                         //   newcs.BackgroundImage = Properties.Resources.Glass_Green;
                            newcs.BackgroundImageLayout = ImageAlignEnum.TileStretch;
                            flg.SetCellStyle(j, i, newcs);
                        }
                        else
                        {
                            CellStyle newcs = flg.Styles.Add("IsColorRed");
                        //    newcs.BackgroundImage = Properties.Resources.Glass_Red;
                            newcs.BackgroundImageLayout = ImageAlignEnum.TileStretch;
                            flg.SetCellStyle(j, i, newcs);
                        }

                        
                    }

                }
            }

        }

        public int isColor(String strValue)
        {
            String[] strList;
            String strFirst = "";
            String strSecond = "";

            strList = strValue.Split('/');
            strFirst = strList[0];
            strSecond = strList[1];

            if (Convert.ToInt32(strFirst) < Convert.ToInt32(strSecond))
            {
                return 1;

            }

            return 0;

        }

        public int isColor_New(String strValue)
        {
            String[] strList;
            String strFirst = "";
            String strSecond = "";

            strList = strValue.Split('/');
            strFirst = strList[0];
            strSecond = strList[1];

            if (Convert.ToInt32(strFirst) < Convert.ToInt32(strSecond))
            {
                return 1;

            }

            return 0;

        }


        public void setRowHeight(C1FlexGrid flg, int iStartRow, int iEndRow, int iHeight)
        {
            for (int i = iStartRow; i < iEndRow; i++)
            {
                flg.Rows[i].Height = iHeight;
            }
        }
         
        public void setColSunday(C1FlexGrid flg, String[] strList)
        {
            for (int j = 0; j < flg.Cols.Count; j++)
            {
                if (strList[j].ToString() == "Sun")
                {
                    for (int i = 1; i < flg.Rows.Count; i++)
                    {
                        CellStyle newcs = flg.Styles.Add("IsColorSunday");
                    //    newcs.BackgroundImage = Properties.Resources.Glass_Brown;
                        newcs.BackgroundImageLayout = ImageAlignEnum.TileStretch;
                        flg.SetCellStyle(i, j, newcs);
                    }

                }

            }
        }

        public void setColFontSunday(C1FlexGrid flg, String[] strList)
        {
            for (int j = 0; j < flg.Cols.Count; j++)
            {
                if (strList[j].ToString() == "Sun")
                {                    
                        CellStyle csHeader = flg.Styles.Add("FontRedColor");
                        csHeader.ForeColor = Color.Red;
                        flg.SetCellStyle(0, j, csHeader);        

                }

            }
        }
        public void cutColContent(C1FlexGrid flg, int iCol, int iMaxLength)
        {
            for (int i = 1; i < flg.Rows.Count; i++)
            {
                if (flg[i, iCol] != null)
                {
                    if (flg[i, iCol].ToString().Trim() != "")
                    {
                        String strTemp = "";
                        strTemp = flg[i, iCol].ToString().Substring(0, Math.Min(iMaxLength, flg[i, iCol].ToString().Length));
                        int iLastIndex = 0;
                        iLastIndex = strTemp.LastIndexOf(" ");
                        strTemp = strTemp.Substring(0, iLastIndex);
                        flg[i, iCol] = strTemp;
                    }
                }
            }


        }
        #endregion
    }
}
