using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper
{
    public class MySqlCWC
    {
        public static MySql.Data.MySqlClient.MySqlConnection mySQLConn;

        public static bool MySqlBaking_Connect(string strHost, string strPort, string strUser, string strPSW)
        {
            try
            {
                if (mySQLConn != null && mySQLConn.State.ToString() == "Open")
                    return true;

                mySQLConn = null;
                mySQLConn = new MySqlConnection();
                int nTimerout = mySQLConn.ConnectionTimeout;
                //MySqlSslMode.None
                mySQLConn.ConnectionString = string.Format("Data Source={0};Database={1};User ID={2};Password={3};SslMode=None",
                                                      strHost, "baking", strUser, strPSW);
                mySQLConn.Open();

                if (mySQLConn.State.ToString() != "Open")
                {
                    mySQLConn = null;
                    return false;
                }
                else
                {
                    //得到数据数量，如果超过80000行，就清除前面40000行
                    MySqlCommand m_dbCommand = mySQLConn.CreateCommand();

                    //建立SFC绑定数据表
                    string createSfctable = "CREATE TABLE IF NOT EXISTS sfc_table(Number INTEGER,Sfc VARCHAR(32),TrayID VARCHAR(16),Position INTEGER,BindTime VARCHAR(32),UnBindTime VARCHAR(32),PRIMARY KEY(Sfc))";
                    m_dbCommand.CommandText = createSfctable;
                    int Cols = m_dbCommand.ExecuteNonQuery();

                    //建立托盘绑定数据表
                    createSfctable = "CREATE TABLE IF NOT EXISTS TrayID_table(Number INTEGER,Resource VARCHAR(16),SfcSample VARCHAR(32),ProcessLot1 VARCHAR(16),ProcessLot2 VARCHAR(16),ProcessLot3 VARCHAR(16),BindTime VARCHAR(32),UnBindTime VARCHAR(32),IsBind INTEGER,IsUnBind INTEGER,CompletedTime VARCHAR(32),VacuuFlag VARCHAR(5),TempFlag VARCHAR(5),PRIMARY KEY(`Number`, `Resource`))";
                    m_dbCommand.CommandText = createSfctable;
                    Cols = m_dbCommand.ExecuteNonQuery();

                    createSfctable = "ALTER TABLE `baking`.`trayid_table` CHANGE COLUMN `Number` `Number` INT(11) NOT NULL AUTO_INCREMENT";
                    m_dbCommand.CommandText = createSfctable;
                    Cols = m_dbCommand.ExecuteNonQuery();

                    return true;
                }

            }
            catch (Exception )
            {
                //global_data.m_strLastException = string.Format("MySqlBaking_Connect() 异常: {0}{0}{1}", Environment.NewLine, ex.ToString());
                //global_data.AddMessageOutput(global_data.m_strLastException, "", "确认MySQL数据库是否配制正常！");
                return false;
            }
        }

        //设定烘烤开始信息
        public static bool MySqlBaking_AddBakingStartInfo(string strSFC, string strResource, string strProcessLot1, string strProcessLot2, string strProcessLot3,
                                                                                    string strIsUnBind = "0",
                                                                                    string strUnBindTime = "", string strCompletedTime = "",
                                                                                    string strVacuuNGFlag = "0", string strTempNGFlag = "0")
        {
            string strBindTime = "";
            DateTime time = DateTime.Now;
            strBindTime = time.ToString("u").Replace("T", " ").Replace("Z", "");
            int nIsBind = 1;
            MySqlDataReader recode = null;
            MySqlTransaction tran = null;

            try
            {
                //根据Sfc查询对应的托盘A, B查询
                MySqlCommand m_dbCommand = mySQLConn.CreateCommand();
                m_dbCommand.CommandText = string.Format("SELECT * FROM TrayID_table WHERE Resource='{0}'", strResource);
                recode = m_dbCommand.ExecuteReader();

                if (!recode.HasRows)
                {
                    recode.Close();
                    //插入新的数据
                    m_dbCommand.CommandText = "";
                    m_dbCommand.CommandText = string.Format("INSERT INTO TrayID_table({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11}) VALUES('{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}')",
                                                                                         "Resource", "SfcSample", "ProcessLot1", "ProcessLot2", "ProcessLot3", "BindTime", "UnBindTime", "IsBind", "IsUnBind", "CompletedTime", "VacuuFlag", "TempFlag",
                                                                                        strResource, strSFC, strProcessLot1, strProcessLot2, strProcessLot3, strBindTime, strUnBindTime, nIsBind.ToString(), strIsUnBind, strCompletedTime, strVacuuNGFlag, strTempNGFlag);

                    int nRows = m_dbCommand.ExecuteNonQuery();
                    recode.Close();
                }
                else
                {
                    recode.Close();
                    //删除数据（改变数据)
                    tran = mySQLConn.BeginTransaction();

                    m_dbCommand.CommandText = string.Format("UPDATE TrayID_table SET SfcSample = '{0}' WHERE Resource='{1}'", strSFC, strResource);
                    int Cols = m_dbCommand.ExecuteNonQuery();

                    m_dbCommand.CommandText = string.Format("UPDATE TrayID_table SET ProcessLot1 = '{0}' WHERE Resource='{1}'", strProcessLot1, strResource);
                    Cols = m_dbCommand.ExecuteNonQuery();

                    m_dbCommand.CommandText = string.Format("UPDATE TrayID_table SET ProcessLot2 = '{0}' WHERE Resource='{1}'", strProcessLot2, strResource);
                    Cols = m_dbCommand.ExecuteNonQuery();

                    m_dbCommand.CommandText = string.Format("UPDATE TrayID_table SET ProcessLot3 = '{0}' WHERE Resource='{1}'", strProcessLot3, strResource);
                    Cols = m_dbCommand.ExecuteNonQuery();

                    m_dbCommand.CommandText = string.Format("UPDATE TrayID_table SET BindTime = '{0}' WHERE Resource='{1}'", strBindTime, strResource);
                    Cols = m_dbCommand.ExecuteNonQuery();

                    m_dbCommand.CommandText = string.Format("UPDATE TrayID_table SET UnBindTime = '{0}' WHERE Resource='{1}'", strUnBindTime, strResource);
                    Cols = m_dbCommand.ExecuteNonQuery();

                    m_dbCommand.CommandText = string.Format("UPDATE TrayID_table SET IsBind = '{0}' WHERE Resource='{1}'", nIsBind.ToString(), strResource);
                    Cols = m_dbCommand.ExecuteNonQuery();

                    m_dbCommand.CommandText = string.Format("UPDATE TrayID_table SET IsUnBind = '{0}' WHERE Resource='{1}'", strIsUnBind, strResource);
                    Cols = m_dbCommand.ExecuteNonQuery();

                    m_dbCommand.CommandText = string.Format("UPDATE TrayID_table SET CompletedTime = '{0}' WHERE Resource='{1}'", strCompletedTime, strResource);
                    Cols = m_dbCommand.ExecuteNonQuery();

                    m_dbCommand.CommandText = string.Format("UPDATE TrayID_table SET VacuuFlag = '{0}' WHERE Resource='{1}'", strVacuuNGFlag, strResource);
                    Cols = m_dbCommand.ExecuteNonQuery();

                    m_dbCommand.CommandText = string.Format("UPDATE TrayID_table SET TempFlag = '{0}' WHERE Resource='{1}'", strTempNGFlag, strResource);
                    Cols = m_dbCommand.ExecuteNonQuery();

                    tran.Commit();
                    recode.Close();
                    tran.Dispose();
                }


                return true;
            }
            catch (Exception )
            {
                //global_data.m_strLastException = string.Format("MySqlBaking_AddBakingStartInfo() 异常: {0}{0}{1}", Environment.NewLine, ex.ToString());
                //global_data.AddMessageOutput(global_data.m_strLastException, "", "确认MySQL数据库是否配制正常！");
                recode.Close();
                if (tran != null)
                    tran.Dispose();
                return false;
            }
        }

        public static bool MySqlBaking_DeleteSFCInformationAfterWaterTest(string strSFC, string strResource)
        {
            MySqlDataReader recode = null;
            MySqlTransaction tran = null;
            try
            {
                //根据Sfc查询对应的托盘A, B查询
                MySqlCommand m_dbCommand = mySQLConn.CreateCommand();
                m_dbCommand.CommandText = string.Format("SELECT * FROM TrayID_table WHERE Resource='{1}'", strSFC, strResource);//SfcSample='{0}' AND 
                recode = m_dbCommand.ExecuteReader();
                if (!recode.HasRows)
                {
                    recode.Close();
                   // global_data.AddMessageOutput(global_data.m_strLastException, "", "MySqlBaking_DeleteSFCInformationAfterWaterTest() : 在数据库里面没有对应的电苡号，请检查日志文件以确认！");
                    return false;
                }

                recode.Close();
                //删除数据（改变数据)
                tran = mySQLConn.BeginTransaction();

                m_dbCommand.CommandText = string.Format("UPDATE TrayID_table SET SfcSample = '{0}' WHERE Resource='{1}'", "", strResource);
                int Cols = m_dbCommand.ExecuteNonQuery();

                m_dbCommand.CommandText = string.Format("UPDATE TrayID_table SET ProcessLot1 = '{0}' WHERE Resource='{1}'", "", strResource);
                Cols = m_dbCommand.ExecuteNonQuery();

                m_dbCommand.CommandText = string.Format("UPDATE TrayID_table SET ProcessLot2 = '{0}' WHERE Resource='{1}'", "", strResource);
                Cols = m_dbCommand.ExecuteNonQuery();

                m_dbCommand.CommandText = string.Format("UPDATE TrayID_table SET ProcessLot3 = '{0}' WHERE Resource='{1}'", "", strResource);
                Cols = m_dbCommand.ExecuteNonQuery();

                m_dbCommand.CommandText = string.Format("UPDATE TrayID_table SET BindTime = '{0}' WHERE Resource='{1}'", "", strResource);
                Cols = m_dbCommand.ExecuteNonQuery();

                m_dbCommand.CommandText = string.Format("UPDATE TrayID_table SET UnBindTime = '{0}' WHERE Resource='{1}'", "", strResource);
                Cols = m_dbCommand.ExecuteNonQuery();

                m_dbCommand.CommandText = string.Format("UPDATE TrayID_table SET IsBind = '{0}' WHERE Resource='{1}'", "0", strResource);
                Cols = m_dbCommand.ExecuteNonQuery();

                m_dbCommand.CommandText = string.Format("UPDATE TrayID_table SET IsUnBind = '{0}' WHERE Resource='{1}'", "0", strResource);
                Cols = m_dbCommand.ExecuteNonQuery();

                m_dbCommand.CommandText = string.Format("UPDATE TrayID_table SET CompletedTime = '{0}' WHERE Resource='{1}'", "", strResource);
                Cols = m_dbCommand.ExecuteNonQuery();

                m_dbCommand.CommandText = string.Format("UPDATE TrayID_table SET VacuuFlag = '{0}' WHERE Resource='{1}'", "0", strResource);
                Cols = m_dbCommand.ExecuteNonQuery();

                m_dbCommand.CommandText = string.Format("UPDATE TrayID_table SET TempFlag = '{0}' WHERE Resource='{1}'", "0", strResource);
                Cols = m_dbCommand.ExecuteNonQuery();

                tran.Commit();
                recode.Close();
                tran.Dispose();

                return true;

            }
            catch (Exception )
            {
                //global_data.m_strLastException = string.Format("MySqlBaking_DeleteSFCInformationAfterWaterTest() 异常: {0}{0}{1}", Environment.NewLine, ex.ToString());
                //global_data.AddMessageOutput(global_data.m_strLastException, "", "确认MySQL数据库是否配制正常！");
                recode.Close();
                tran.Dispose();
                return false;
            }
        }

        //|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
        //函数名：
        //说明：
        //|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
        public static bool MySqlBaking_GetProcessLotInfoV20(string sfc, ref string outResource, ref string outProcessLot1, ref string outProcessLot2, ref string outProcessLot3,
                                                                                            ref string outBindTime, ref string outUnBindTime, ref string outCompletedTime,
                                                                                            ref bool outIsBind, ref bool outIsUnBind,
                                                                                            ref bool outVacuuNGFlag, ref bool outTempNGFlag)
        {
            MySqlDataReader recode = null;
            try
            {
                //根据Sfc查询对应的托盘A, B查询
                MySqlCommand m_dbCommand = mySQLConn.CreateCommand();
                m_dbCommand.CommandText = string.Format("SELECT * FROM TrayID_table WHERE SfcSample='{0}'", sfc);
                recode = m_dbCommand.ExecuteReader();
                if (!recode.HasRows)
                {
                    recode.Close();
                    //global_data.AddMessageOutput(global_data.m_strLastException, "", "在数据库里面没有对应的电苡号，请检查日志文件以确认！");
                    return false;
                }

                //找到炉子编号
                recode.Read();
                outResource = recode["Resource"] == null ? "" : (string)recode["Resource"];
                outProcessLot1 = recode["ProcessLot1"] == null ? "" : (string)recode["ProcessLot1"];
                outProcessLot2 = recode["ProcessLot2"] == null ? "" : (string)recode["ProcessLot2"];
                outProcessLot3 = recode["ProcessLot3"] == null ? "" : (string)recode["ProcessLot3"];
                outBindTime = recode["BindTime"] == null ? "" : (string)recode["BindTime"];
                outUnBindTime = recode["UnBindTime"] == null ? "" : (string)recode["UnBindTime"];
                outCompletedTime = recode["CompletedTime"] == null ? "" : (string)recode["CompletedTime"];

                if (outResource == null || outResource.Length <= 0)
                {
                    recode.Close();
                    return false;
                }

                recode.Close();
                //global_data.Database_GetVacuuTempFlag(outResource, ref outVacuuNGFlag, ref outTempNGFlag);

                return true;
            }
            catch (Exception )
            {
                //global_data.m_strLastException = string.Format("Database_GetSfcExInfo 失败\r\n{0}", ex.ToString());
                recode.Close();
                return false;
            }
        }





    }
}
