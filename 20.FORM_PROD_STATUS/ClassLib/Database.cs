using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Collections;
using System.Data.OleDb;
using System.Data;
using System.Data.SqlClient;
using System.Data.OracleClient;

namespace FORM
{
    class Database
    {
		
		#region Connect Database HUBIC
        public String strError;
        private String strConnection, strConnectionSql;


         string strConnectionORA = "Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = 211.54.128.21 "
                                 + ")(PORT = 1521 ))(CONNECT_DATA = (SERVER = DEDICATED)(SERVICE_NAME = HUBICVJ "
                                 + ")));Password= hubicvj; User ID= hubicvj";
        public String settingDBORA()
        {
            XmlDocument xmldoc = new XmlDocument();
            XmlNodeList xmlDBSQL;
            String strFileName = AppDomain.CurrentDomain.BaseDirectory + "DBORA.xml";
            try
            {
                using (FileStream fs = new FileStream(strFileName, FileMode.Open, FileAccess.Read))
                {
                    xmldoc.Load(fs);
                    xmlDBSQL = xmldoc.GetElementsByTagName("DBORA");
                    String DATA_SOURCE = xmlDBSQL[0].ChildNodes.Item(0).InnerText.Trim();
                    String USR = xmlDBSQL[0].ChildNodes.Item(1).InnerText.Trim();
                    String PWD = xmlDBSQL[0].ChildNodes.Item(2).InnerText.Trim();

                    strConnection = "Provider=MSDAORA;Data Source=" + DATA_SOURCE + ";" +
                                    "Persist Security Info=True;" +
                                    "User ID=" + USR + ";" +
                                    "Password=" + PWD;
                    
                    return "";
                }
            }
            catch (Exception e)
            {
                return "Error : " + e.Message;
            }
        }



        public String settingORACLE()
        {
            XmlDocument xmldoc = new XmlDocument();
            XmlNodeList xmlDBSQL;
            String strFileName = AppDomain.CurrentDomain.BaseDirectory + "DBORA.xml";
            try
            {
                using (FileStream fs = new FileStream(strFileName, FileMode.Open, FileAccess.Read))
                {
                    xmldoc.Load(fs);
                    xmlDBSQL = xmldoc.GetElementsByTagName("DBORA");
                    String DATA_SOURCE = xmlDBSQL[0].ChildNodes.Item(0).InnerText.Trim();
                    String USR = xmlDBSQL[0].ChildNodes.Item(1).InnerText.Trim();
                    String PWD = xmlDBSQL[0].ChildNodes.Item(2).InnerText.Trim();

                    strConnectionORA = "Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = 211.54.128.21 "
                                 + ")(PORT = 1521 ))(CONNECT_DATA = (SERVER = DEDICATED)(SERVICE_NAME = HUBICVJ "
                                 + ")));Password=" + PWD + ";User ID=" + USR;
                    
                    return "";
                }

            }
            catch (Exception e)
            {
                return null;

            }
        }

        public  ArrayList getDataORA2(String str)
        {
            try
            {
                using (System.Data.OracleClient.OracleConnection con_ora = new System.Data.OracleClient.OracleConnection(strConnectionORA))
                {

                    con_ora.Open();
                    using (System.Data.OracleClient.OracleCommand command = new System.Data.OracleClient.OracleCommand(str, con_ora))
                    {
                        using (OracleDataReader reader = command.ExecuteReader())
                        {
                            ArrayList list = new ArrayList();
                            while (reader.Read())
                            {
                                object[] values = new object[reader.FieldCount];
                                reader.GetValues(values);
                                list.Add(values);

                            }
                            return list;
                        }
                    }
                }
            }
            catch (Exception e)
            {
               // strError = e.Message.ToString();
               // MessageBox.Show(strError);
                return null;

            }
        }

        public String settingDBSQL()
        {
            XmlDocument xmldoc = new XmlDocument();
            XmlNodeList xmlDBSQL;
            String strFileName = AppDomain.CurrentDomain.BaseDirectory + "DBSQL.xml";
            try
            {
                using (FileStream fs = new FileStream(strFileName, FileMode.Open, FileAccess.Read))
                {
                    xmldoc.Load(fs);
                    xmlDBSQL = xmldoc.GetElementsByTagName("DBSQL");
                    String DATA_SOURCE = xmlDBSQL[0].ChildNodes.Item(0).InnerText.Trim();
                    String DB_NAME = xmlDBSQL[0].ChildNodes.Item(1).InnerText.Trim();
                    String USR = xmlDBSQL[0].ChildNodes.Item(2).InnerText.Trim();
                    String PWD = xmlDBSQL[0].ChildNodes.Item(3).InnerText.Trim();
                    strConnectionSql = "Data Source=" + DATA_SOURCE + ";" +
                                  "Initial Catalog=" + DB_NAME + ";" +
                                  "Persist Security Info=True;" +
                                  "User ID=" + USR + ";" +
                                  "Password=" + PWD;

                    return "";
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public ArrayList getDataORA(String str)
        {
            try
            {
                using (OleDbConnection con_ora = new OleDbConnection(strConnection))
                {
                    con_ora.Open();
                    using (OleDbCommand command = new OleDbCommand(str, con_ora))
                    {
                        using (OleDbDataReader reader = command.ExecuteReader())
                        {
                            ArrayList list = new ArrayList();
                            while (reader.Read())
                            {
                                object[] values = new object[reader.FieldCount];
                                reader.GetValues(values);
                                list.Add(values);

                            }
                            return list;
                        }
                    }
                }
            }
            catch (Exception e)
            {
               // strError = e.Message.ToString();
                return null;
            }
        }

        public DataTable getDataORASource(String str)
        {
            try
            {
                using (OleDbConnection con_ora = new OleDbConnection(strConnection))
                {
                    con_ora.Open();
                    using (OleDbDataAdapter da = new OleDbDataAdapter(str, con_ora))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        return dt;
                    }
                }
            }
            catch (Exception e)
            {
              //  strError = e.Message.ToString();
                return null;
            }
        }

        public ArrayList getDataSQL(String str)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strConnectionSql))
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand(str, con))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            ArrayList list = new ArrayList();
                            while (reader.Read())
                            {
                                object[] values = new object[reader.FieldCount];
                                reader.GetValues(values);
                                list.Add(values);
                            }
                            return list;
                        }
                    }
                }
            }
            catch (Exception e)
            {
               // strError = e.Message.ToString();
                return null;
            }
        }

        public String saveDataORA(String str)
        {
            try
            {
                using (OleDbConnection con_ora = new OleDbConnection(strConnection))
                {
                    using (OleDbCommand command = new OleDbCommand(str, con_ora))
                    {
                        con_ora.Open();
                        command.ExecuteNonQuery();
                        return "";
                    }
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
		
		#endregion
		
		#region Insert Log
        public Boolean INSERT_FRM_LOG(string ARG_PGM_CD, string ARG_PGM_NM, string ARG_FRM_CD, string ARG_FRM_NM, string ARG_REMARK, string ARG_UPD_USER)
        {

            System.Data.DataSet retDS;
            COM.OraDB MyOraDB = new COM.OraDB();

            MyOraDB.ReDim_Parameter(6);
            MyOraDB.Process_Name = "MES.PKG_SMT_B1_PHUOC.SAVE_FRM_LOG";

            MyOraDB.Parameter_Name[0] = "ARG_PGM_CD";
            MyOraDB.Parameter_Name[1] = "ARG_PGM_NM";
            MyOraDB.Parameter_Name[2] = "ARG_FRM_CD";
            MyOraDB.Parameter_Name[3] = "ARG_FRM_NM";
            MyOraDB.Parameter_Name[4] = "ARG_REMARK";
            MyOraDB.Parameter_Name[5] = "ARG_UPD_USER";

            for (int i = 0; i <= 5; i++)
                MyOraDB.Parameter_Type[i] = (char)OracleType.VarChar;

            MyOraDB.Parameter_Values[0] = ARG_PGM_CD;
            MyOraDB.Parameter_Values[1] = ARG_PGM_NM;
            MyOraDB.Parameter_Values[2] = ARG_FRM_CD;
            MyOraDB.Parameter_Values[3] = ARG_FRM_NM;
            MyOraDB.Parameter_Values[4] = ARG_REMARK;
            MyOraDB.Parameter_Values[5] = ARG_UPD_USER;

            MyOraDB.Add_Modify_Parameter(true);

            retDS = MyOraDB.Exe_Modify_Procedure();

            if (retDS == null) return false;

            return true;
        }
        #endregion

        #region UC_OS&D DATA
        public DataTable SEL_OSD_DATA(string ARG_QTYPE)
        {
            COM.OraDB MyOraDB = new COM.OraDB();
            DataSet ds_ret;

            try
            {
                string process_name = "MES.PKG_SMT_B1_INSOLE.M_INTERNAL_OSD_SELECT";

                MyOraDB.ReDim_Parameter(2);
                MyOraDB.Process_Name = process_name;

                MyOraDB.Parameter_Name[0] = "ARG_QTYPE";
                MyOraDB.Parameter_Name[1] = "OUT_CURSOR";

                MyOraDB.Parameter_Type[0] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[1] = (int)OracleType.Cursor;

                MyOraDB.Parameter_Values[0] = ARG_QTYPE;
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
        #endregion

        #region OS PRODUCTION DATA
        public DataTable SEL_OS_PRODUCTION_DATA(string ARG_QTYPE)
        {
            COM.OraDB MyOraDB = new COM.OraDB();
            DataSet ds_ret;

            try
            {
                string process_name = "MES.PKG_SMT_B1_INSOLE.OUTSOLE_PRODUCTION_SELECT";

                MyOraDB.ReDim_Parameter(2);
                MyOraDB.Process_Name = process_name;

                MyOraDB.Parameter_Name[0] = "ARG_QTYPE";
                MyOraDB.Parameter_Name[1] = "OUT_CURSOR";

                MyOraDB.Parameter_Type[0] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[1] = (int)OracleType.Cursor;

                MyOraDB.Parameter_Values[0] = ARG_QTYPE;
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
        #endregion

        #region OS INVENTORY DATA
        public DataTable SEL_OS_INV_DATA(string ARG_QTYPE)
        {
            COM.OraDB MyOraDB = new COM.OraDB();
            DataSet ds_ret;

            try
            {
                string process_name = "MES.PKG_SMT_B1_INSOLE.OUTSOLE_INV_SELECT";

                MyOraDB.ReDim_Parameter(2);
                MyOraDB.Process_Name = process_name;

                MyOraDB.Parameter_Name[0] = "ARG_QTYPE";
                MyOraDB.Parameter_Name[1] = "OUT_CURSOR";

                MyOraDB.Parameter_Type[0] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[1] = (int)OracleType.Cursor;

                MyOraDB.Parameter_Values[0] = ARG_QTYPE;
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
        #endregion

        #region OS POD DATA
        public DataTable SEL_OS_POD_DATA(string ARG_QTYPE)
        {
            COM.OraDB MyOraDB = new COM.OraDB();
            DataSet ds_ret;

            try
            {
                string process_name = "MES.PKG_SMT_B1_INSOLE.OUTSOLE_POD_SELECT";

                MyOraDB.ReDim_Parameter(2);
                MyOraDB.Process_Name = process_name;

                MyOraDB.Parameter_Name[0] = "ARG_QTYPE";
                MyOraDB.Parameter_Name[1] = "OUT_CURSOR";

                MyOraDB.Parameter_Type[0] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[1] = (int)OracleType.Cursor;

                MyOraDB.Parameter_Values[0] = ARG_QTYPE;
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
        #endregion

        #region OS ABSENTEEISM DATA
        public DataTable SEL_OS_ABSENT_DATA(string ARG_QTYPE)
        {
            COM.OraDB MyOraDB = new COM.OraDB();
            DataSet ds_ret;

            try
            {
                string process_name = "MES.PKG_SMT_B1_INSOLE.OUTSOLE_HR_SELECT";

                MyOraDB.ReDim_Parameter(2);
                MyOraDB.Process_Name = process_name;

                MyOraDB.Parameter_Name[0] = "ARG_QTYPE";
                MyOraDB.Parameter_Name[1] = "OUT_CURSOR";

                MyOraDB.Parameter_Type[0] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[1] = (int)OracleType.Cursor;

                MyOraDB.Parameter_Values[0] = ARG_QTYPE;
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
        #endregion




        #region Mold Overhaul
        public static DataTable LOAD_BY_TYPE(string argMode)
        {
            COM.OraDB MyOraDB = new COM.OraDB();
            System.Data.DataSet ds_ret;
            try
            {
                string process_name = "MES.PKG_SMT_B1_DIGITAL.MOLD_REPAIR_BY_TYPE";
                //ARGMODE
                MyOraDB.ReDim_Parameter(2);
                MyOraDB.Process_Name = process_name;
                MyOraDB.Parameter_Name[0] = "ARG_WH_CD";
                MyOraDB.Parameter_Type[0] = (char)OracleType.VarChar;
                MyOraDB.Parameter_Values[0] = argMode;
                MyOraDB.Parameter_Name[1] = "OUT_CURSOR";
                MyOraDB.Parameter_Type[1] = (char)OracleType.Cursor;
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

        public static DataTable LOAD_BY_MODEL(string argMode)
        {
            COM.OraDB MyOraDB = new COM.OraDB();
            System.Data.DataSet ds_ret;
            try
            {
                string process_name = "MES.PKG_SMT_B1_DIGITAL.MOLD_REPAIR_BY_MODEL";
                //ARGMODE
                MyOraDB.ReDim_Parameter(2);
                MyOraDB.Process_Name = process_name;
                MyOraDB.Parameter_Name[0] = "ARG_WH_CD";
                MyOraDB.Parameter_Type[0] = (char)OracleType.VarChar;
                MyOraDB.Parameter_Values[0] = argMode;
                MyOraDB.Parameter_Name[1] = "OUT_CURSOR";
                MyOraDB.Parameter_Type[1] = (char)OracleType.Cursor;
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
        #endregion Mold Overhaul

        #region DATA NGOLADO
        public DataTable SEL_OS_OSD_MONTH(string ARG_QTYPE, string ARG_YMD, string ARG_OP)
        {
            COM.OraDB MyOraDB = new COM.OraDB();
            DataSet ds_ret;

            try
            {
                string process_name = "MES.PKG_SMT_B1.SP_OS_OSD_MONTH";

                MyOraDB.ReDim_Parameter(4);
                MyOraDB.Process_Name = process_name;

                MyOraDB.Parameter_Name[0] = "V_P_TYPE";
                MyOraDB.Parameter_Name[1] = "V_P_YMD";
                MyOraDB.Parameter_Name[2] = "V_P_OP";
                MyOraDB.Parameter_Name[3] = "OUT_CURSOR";

                MyOraDB.Parameter_Type[0] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[1] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[2] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[3] = (int)OracleType.Cursor;

                MyOraDB.Parameter_Values[0] = ARG_QTYPE;
                MyOraDB.Parameter_Values[1] = ARG_YMD;
                MyOraDB.Parameter_Values[2] = ARG_OP;
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

        public DataTable SEL_OS_OSD_MONTH_V2(string ARG_QTYPE, string ARG_YMD, string ARG_OP)
        {
            COM.OraDB MyOraDB = new COM.OraDB();
            DataSet ds_ret;

            try
            {
                string process_name = "MES.PKG_SMT_B1.SP_PH_OSD_MONTH_V2";

                MyOraDB.ReDim_Parameter(4);
                MyOraDB.Process_Name = process_name;

                MyOraDB.Parameter_Name[0] = "V_P_TYPE";
                MyOraDB.Parameter_Name[1] = "V_P_YMD";
                MyOraDB.Parameter_Name[2] = "V_P_OP";
                MyOraDB.Parameter_Name[3] = "OUT_CURSOR";

                MyOraDB.Parameter_Type[0] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[1] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[2] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[3] = (int)OracleType.Cursor;

                MyOraDB.Parameter_Values[0] = ARG_QTYPE;
                MyOraDB.Parameter_Values[1] = ARG_YMD;
                MyOraDB.Parameter_Values[2] = ARG_OP;
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

        public DataTable SEL_OS_OSD_YEAR_V2(string ARG_QTYPE, string ARG_YMD, string ARG_OP)
        {
            COM.OraDB MyOraDB = new COM.OraDB();
            DataSet ds_ret;

            try
            {
                string process_name = "MES.PKG_SMT_B1.SP_PH_OSD_YEAR_V2";

                MyOraDB.ReDim_Parameter(4);
                MyOraDB.Process_Name = process_name;

                MyOraDB.Parameter_Name[0] = "V_P_TYPE";
                MyOraDB.Parameter_Name[1] = "V_P_YMD";
                MyOraDB.Parameter_Name[2] = "V_P_OP";
                MyOraDB.Parameter_Name[3] = "OUT_CURSOR";

                MyOraDB.Parameter_Type[0] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[1] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[2] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[3] = (int)OracleType.Cursor;

                MyOraDB.Parameter_Values[0] = ARG_QTYPE;
                MyOraDB.Parameter_Values[1] = ARG_YMD;
                MyOraDB.Parameter_Values[2] = ARG_OP;
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
        public DataTable SEL_OS_OSD_YEAR(string ARG_QTYPE, string ARG_YMD, string ARG_OP)
        {
            COM.OraDB MyOraDB = new COM.OraDB();
            DataSet ds_ret;

            try
            {
                string process_name = "MES.PKG_SMT_B1.SP_OS_OSD_YEAR";

                MyOraDB.ReDim_Parameter(4);
                MyOraDB.Process_Name = process_name;

                MyOraDB.Parameter_Name[0] = "V_P_TYPE";
                MyOraDB.Parameter_Name[1] = "V_P_YMD";
                MyOraDB.Parameter_Name[2] = "V_P_OP";
                MyOraDB.Parameter_Name[3] = "OUT_CURSOR";

                MyOraDB.Parameter_Type[0] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[1] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[2] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[3] = (int)OracleType.Cursor;

                MyOraDB.Parameter_Values[0] = ARG_QTYPE;
                MyOraDB.Parameter_Values[1] = ARG_YMD;
                MyOraDB.Parameter_Values[2] = ARG_OP;
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

        public DataTable SEL_OS_OSD_EXT(string ARG_QTYPE, string ARG_OP)
        {
            COM.OraDB MyOraDB = new COM.OraDB();
            DataSet ds_ret;

            try
            {
                string process_name = "MES.PKG_SMT_B1.SP_OS_OSD_EXT_MONTH";

                MyOraDB.ReDim_Parameter(3);
                MyOraDB.Process_Name = process_name;

                MyOraDB.Parameter_Name[0] = "V_P_TYPE";
                MyOraDB.Parameter_Name[1] = "V_P_OP";
                MyOraDB.Parameter_Name[2] = "OUT_CURSOR";

                MyOraDB.Parameter_Type[0] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[1] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[2] = (int)OracleType.Cursor;

                MyOraDB.Parameter_Values[0] = ARG_QTYPE;
                MyOraDB.Parameter_Values[1] = ARG_OP;
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

        public DataTable SEL_OS_OSD_EXT_MONTH(string ARG_QTYPE, string ARG_OP)
        {
            COM.OraDB MyOraDB = new COM.OraDB();
            DataSet ds_ret;

            try
            {
                string process_name = "MES.PKG_SMT_B1.SP_PH_OSD_EXT_MONTH";

                MyOraDB.ReDim_Parameter(3);
                MyOraDB.Process_Name = process_name;

                MyOraDB.Parameter_Name[0] = "V_P_TYPE";
                MyOraDB.Parameter_Name[1] = "V_P_OP";
                MyOraDB.Parameter_Name[2] = "OUT_CURSOR";

                MyOraDB.Parameter_Type[0] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[1] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[2] = (int)OracleType.Cursor;

                MyOraDB.Parameter_Values[0] = ARG_QTYPE;
                MyOraDB.Parameter_Values[1] = ARG_OP;
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

        public DataTable SEL_OS_OSD_EXT_YEAR(string ARG_QTYPE, string ARG_OP)
        {
            COM.OraDB MyOraDB = new COM.OraDB();
            DataSet ds_ret;

            try
            {
                string process_name = "MES.PKG_SMT_B1.SP_PH_OSD_EXT_YEAR";

                MyOraDB.ReDim_Parameter(3);
                MyOraDB.Process_Name = process_name;

                MyOraDB.Parameter_Name[0] = "V_P_TYPE";
                MyOraDB.Parameter_Name[1] = "V_P_OP";
                MyOraDB.Parameter_Name[2] = "OUT_CURSOR";

                MyOraDB.Parameter_Type[0] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[1] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[2] = (int)OracleType.Cursor;

                MyOraDB.Parameter_Values[0] = ARG_QTYPE;
                MyOraDB.Parameter_Values[1] = ARG_OP;
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

        public DataTable SEL_OS_OSD_EXT_PARETO(string ARG_QTYPE, string ARG_OP)
        {
            COM.OraDB MyOraDB = new COM.OraDB();
            DataSet ds_ret;

            try
            {
                string process_name = "MES.PKG_SMT_B1.SP_OS_OSD_EXT_PARETO";

                MyOraDB.ReDim_Parameter(3);
                MyOraDB.Process_Name = process_name;

                MyOraDB.Parameter_Name[0] = "V_P_TYPE";
                MyOraDB.Parameter_Name[1] = "V_P_OP";
                MyOraDB.Parameter_Name[2] = "OUT_CURSOR";

                MyOraDB.Parameter_Type[0] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[1] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[2] = (int)OracleType.Cursor;

                MyOraDB.Parameter_Values[0] = ARG_QTYPE;
                MyOraDB.Parameter_Values[1] = ARG_OP;
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

        public DataTable SEL_OS_PROD_DAILY(string ARG_QTYPE, string ARG_OP)
        {
            COM.OraDB MyOraDB = new COM.OraDB();
            DataSet ds_ret;

            try
            {
                string process_name = "MES.PKG_SMT_B_PROD_STATUS.SEL_PRODUCTION_STATUS";

                MyOraDB.ReDim_Parameter(4);
                MyOraDB.Process_Name = process_name;

                MyOraDB.Parameter_Name[0] = "V_P_OP";
                MyOraDB.Parameter_Name[1] = "ARG_FRM_LINE";
                MyOraDB.Parameter_Name[2] = "ARG_TO_LINE";
                MyOraDB.Parameter_Name[3] = "OUT_CURSOR";

                MyOraDB.Parameter_Type[0] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[1] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[2] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[3] = (int)OracleType.Cursor;

                MyOraDB.Parameter_Values[0] = ARG_OP;
                MyOraDB.Parameter_Values[1] = "";
                MyOraDB.Parameter_Values[2] = "";
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

        public DataTable SEL_OS_PROD_MONTH(string ARG_QTYPE, string ARG_YMD, string ARG_OP)
        {
            COM.OraDB MyOraDB = new COM.OraDB();
            DataSet ds_ret;

            try
            {
                string process_name = "MES.PKG_SMT_B1.SP_OS_PROD_MONTH_V2";

                MyOraDB.ReDim_Parameter(4);
                MyOraDB.Process_Name = process_name;

                MyOraDB.Parameter_Name[0] = "V_P_TYPE";
                MyOraDB.Parameter_Name[1] = "V_P_YMD";
                MyOraDB.Parameter_Name[2] = "V_P_OP";
                MyOraDB.Parameter_Name[3] = "OUT_CURSOR";

                MyOraDB.Parameter_Type[0] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[1] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[2] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[3] = (int)OracleType.Cursor;

                MyOraDB.Parameter_Values[0] = ARG_QTYPE;
                MyOraDB.Parameter_Values[1] = ARG_YMD;
                MyOraDB.Parameter_Values[2] = ARG_OP;
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

        //public DataTable SEL_OS_PROD_MONTH(string ARG_QTYPE, string ARG_OP)
        //{
        //    COM.OraDB MyOraDB = new COM.OraDB();
        //    DataSet ds_ret;

        //    try
        //    {
        //        string process_name = "MES.PKG_SMT_B1.SP_OS_PROD_MONTH";

        //        MyOraDB.ReDim_Parameter(3);
        //        MyOraDB.Process_Name = process_name;

        //        MyOraDB.Parameter_Name[0] = "V_P_TYPE";
        //        MyOraDB.Parameter_Name[1] = "V_P_OP";
        //        MyOraDB.Parameter_Name[2] = "OUT_CURSOR";

        //        MyOraDB.Parameter_Type[0] = (int)OracleType.VarChar;
        //        MyOraDB.Parameter_Type[1] = (int)OracleType.VarChar;
        //        MyOraDB.Parameter_Type[2] = (int)OracleType.Cursor;

        //        MyOraDB.Parameter_Values[0] = ARG_QTYPE;
        //        MyOraDB.Parameter_Values[1] = ARG_OP;
        //        MyOraDB.Parameter_Values[2] = "";

        //        MyOraDB.Add_Select_Parameter(true);
        //        ds_ret = MyOraDB.Exe_Select_Procedure();

        //        if (ds_ret == null) return null;
        //        return ds_ret.Tables[process_name];
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}

        public DataTable SEL_OS_PROD_YEAR(string ARG_QTYPE, string ARG_YMD, string ARG_OP)
        {
            COM.OraDB MyOraDB = new COM.OraDB();
            DataSet ds_ret;

            try
            {
                string process_name = "MES.PKG_SMT_B1.SP_OS_PROD_YEAR_V2";

                MyOraDB.ReDim_Parameter(4);
                MyOraDB.Process_Name = process_name;

                MyOraDB.Parameter_Name[0] = "V_P_TYPE";
                MyOraDB.Parameter_Name[1] = "V_P_YMD";
                MyOraDB.Parameter_Name[2] = "V_P_OP";
                MyOraDB.Parameter_Name[3] = "OUT_CURSOR";

                MyOraDB.Parameter_Type[0] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[1] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[2] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[3] = (int)OracleType.Cursor;

                MyOraDB.Parameter_Values[0] = ARG_QTYPE;
                MyOraDB.Parameter_Values[1] = ARG_YMD;
                MyOraDB.Parameter_Values[2] = ARG_OP;
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

        public DataTable SEL_OS_INVENTORY(string ARG_QTYPE, string ARG_OP)
        {
            COM.OraDB MyOraDB = new COM.OraDB();
            DataSet ds_ret;

            try
            {
                string process_name = "MES.PKG_SMT_B1_INSOLE.SP_INS_INVENTORY";

                MyOraDB.ReDim_Parameter(3);
                MyOraDB.Process_Name = process_name;

                MyOraDB.Parameter_Name[0] = "V_P_TYPE";
                MyOraDB.Parameter_Name[1] = "V_P_OP";
                MyOraDB.Parameter_Name[2] = "OUT_CURSOR";

                MyOraDB.Parameter_Type[0] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[1] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[2] = (int)OracleType.Cursor;

                MyOraDB.Parameter_Values[0] = ARG_QTYPE;
                MyOraDB.Parameter_Values[1] = ARG_OP;
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
        #endregion

        #region Machine Layout
        public static DataTable SEL_APS_PLAN_ACTUAL(string arg_wh)
        {
            COM.OraDB MyOraDB = new COM.OraDB();
            System.Data.DataSet ds_ret;

            try
            {
                string process_name = "PKG_DMC.SEL_MOLD_PRODUCTION_LAYOUT";

                MyOraDB.ReDim_Parameter(2);
                MyOraDB.Process_Name = process_name;


                MyOraDB.Parameter_Name[0] = "ARG_WH_CD";
                MyOraDB.Parameter_Name[1] = "OUT_CURSOR";

                MyOraDB.Parameter_Type[0] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[1] = (int)OracleType.Cursor;

                MyOraDB.Parameter_Values[0] = arg_wh;
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

        public DataTable GetMainData(string workType)
        {
            COM.OraDB MyOraDB = new COM.OraDB();
            System.Data.DataSet ds_ret;

            try
            {
                string process_name = "P_OS_MACHINE_LAYOUT_Q";

                MyOraDB.ReDim_Parameter(2);
                MyOraDB.Process_Name = process_name;

                MyOraDB.Parameter_Name[0] = "V_P_WORKTYPE";
                MyOraDB.Parameter_Name[1] = "OUT_CURSOR";
                MyOraDB.Parameter_Type[0] = (char)OracleType.VarChar;
                MyOraDB.Parameter_Type[1] = (char)OracleType.Cursor;

                MyOraDB.Parameter_Values[0] = workType;
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

        #endregion

        #region Mold APS Plan & Actual
        public static DataTable SEL_OS_APS_PLAN_ACTUAL(string arg_wh)
        {
            COM.OraDB MyOraDB = new COM.OraDB();
            System.Data.DataSet ds_ret;

            try
            {
                string process_name = "PKG_SPB_MOLD_WMS_V2.SEL_MOLD_APS_ACTUAL";

                MyOraDB.ReDim_Parameter(2);
                MyOraDB.Process_Name = process_name;


                MyOraDB.Parameter_Name[0] = "ARG_WH_CD";
                MyOraDB.Parameter_Name[1] = "OUT_CURSOR";

                MyOraDB.Parameter_Type[0] = (int)OracleType.VarChar;
                MyOraDB.Parameter_Type[1] = (int)OracleType.Cursor;

                MyOraDB.Parameter_Values[0] = arg_wh;
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
       
        public static DataTable SEL_TOTAL_PLAN_ACTUAL()
        {
            COM.OraDB MyOraDB = new COM.OraDB();
            System.Data.DataSet ds_ret;


            string process_name = "PKG_SPB_MOLD_WMS_V2.SEL_QTY_ACTUAL";

            MyOraDB.ReDim_Parameter(2);
            MyOraDB.Process_Name = process_name;


            MyOraDB.Parameter_Name[0] = "OUT_CURSOR";
            MyOraDB.Parameter_Name[1] = "ARG_WH_CD";

            MyOraDB.Parameter_Type[0] = (int)OracleType.Cursor;
            MyOraDB.Parameter_Type[1] = (int)OracleType.VarChar;

            MyOraDB.Parameter_Values[0] = "";
            MyOraDB.Parameter_Values[1] = "20";

            MyOraDB.Add_Select_Parameter(true);

            ds_ret = MyOraDB.Exe_Select_Procedure();

            if (ds_ret == null) return null;
            return ds_ret.Tables[process_name];

        }

        #endregion Mold APS Plan & Actual
    }
}
