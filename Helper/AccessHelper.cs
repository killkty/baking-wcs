using System;
using System.Data.OleDb;
using System.Data;
using ADOX;

namespace Helper
{
    public class AccessHelper
    {
        private static Object thisLock_Access = new Object();
        public static string filePath = GlobalsInfo.AddressFile_Mes.dbPath + "\\Alarm" + ".mdb";
        public static string conStr = "Provider=Microsoft.Jet.Oledb.4.0;Data Source=" + filePath + ";Jet OLEDB:Database Password=123;Jet OLEDB:Engine Type=5";
        /// <summary>
        /// 创建access数据库
        /// </summary>
        /// <param name="filePath">数据库文件的全路径，如 D:\\NewDb.mdb</param>
        public static bool CreateAccessDb()
        {
            ADOX.Catalog catalog = new Catalog();
                try
                {
                    catalog.Create(conStr);
                    System.Runtime.InteropServices.Marshal.FinalReleaseComObject(catalog.ActiveConnection);
                    System.Runtime.InteropServices.Marshal.FinalReleaseComObject(catalog);

                    //string str = "";
                    //str = string.Format("create table Alarm_RUN(id AUTOINCREMENT PRIMARY KEY, s varchar(50))");  //
                    //ExecuteNonQuery(str, System.Data.CommandType.Text);

                    //str = string.Format("create table Alarm_RUN(id AUTOINCREMENT PRIMARY KEY, s varchar(50))");  //
                    //ExecuteNonQuery(str, System.Data.CommandType.Text);

                }
                catch (System.Exception)
                {
                    return false;
                }
            return true;
        }




        public static int ExecuteNonQuery(string sql, CommandType cmdType, params OleDbParameter[] pms)
        {
            lock (thisLock_Access)
            {
                using (OleDbConnection con = new OleDbConnection(conStr))
                {
                    using (OleDbCommand cmd = new OleDbCommand(sql, con))
                    {
                        cmd.CommandType = cmdType;
                        if (pms != null)
                        {
                            cmd.Parameters.AddRange(pms);
                        }
                        con.Open();
                        return cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        //返回单个值
        public static object ExecuteScalar(string sql, CommandType cmdType, params OleDbParameter[] pms)
        {
            lock (thisLock_Access)
            {
                using (OleDbConnection con = new OleDbConnection(conStr))
                {
                    using (OleDbCommand cmd = new OleDbCommand(sql, con))
                    {
                        cmd.CommandType = cmdType;
                        if (pms != null)
                        {
                            cmd.Parameters.AddRange(pms);
                        }
                        con.Open();
                        return cmd.ExecuteScalar();
                    }
                }
            }
        }

        //执行返回DataReader
        public static OleDbDataReader ExecuteReader(string sql, CommandType cmdType, params OleDbParameter[] pms)
        {
            lock (thisLock_Access)
            {
                OleDbConnection con = new OleDbConnection(conStr);
                using (OleDbCommand cmd = new OleDbCommand(sql, con))
                {
                    cmd.CommandType = cmdType;
                    if (pms != null)
                    {
                        cmd.Parameters.AddRange(pms);
                    }
                    //con.Open();
                    try
                    {
                        if (con.State == ConnectionState.Closed)
                        {
                            con.Open();
                        }
                        return cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    }
                    catch
                    {
                        con.Close();
                        con.Dispose();
                        throw;
                    }
                }
            }
        }

        /// 执行SQL语句  ,返回总记录数
        /// <param name="sql">要执行的SQL语句</param>
        public static int GetSingle(string sql)//外部调用
        {
            lock (thisLock_Access)
            {
                int recordCount = 0;
                using (OleDbConnection con = new OleDbConnection(conStr))
                {

                    using (OleDbCommand cmd = new OleDbCommand(sql, con))
                    {
                        con.Open();
                        recordCount = (int)(Int32)cmd.ExecuteScalar();
                        con.Close();
                        return recordCount;
                    }
                }
            }
        }

        public static DataTable ExecuteDataTable(string sql, CommandType cmdType, params OleDbParameter[] pms)
        {
            lock (thisLock_Access)
            {
                DataTable dt = new DataTable();
                using (OleDbDataAdapter adapter = new OleDbDataAdapter(sql, conStr))
                {
                    adapter.SelectCommand.CommandType = cmdType;
                    if (pms != null)
                    {
                        adapter.SelectCommand.Parameters.AddRange(pms);
                    }
                    adapter.Fill(dt);
                }
                return dt;
            }
        }

    }
}
