using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;

namespace Helper
{
    public class MySqlHelper
    {  //localhost     192.168.250.20
        private static Object thisLock_MySql = new Object();
        private static readonly string conStrD = "Data Source=localhost;Persist Security Info=yes;UserId=root; PWD=123;pooling=true;charset=utf8;";
        private static readonly string conStr = "Data Source=localhost;Database = DataBaKing; UserID = root; Password = 123;pooling=true;charset=utf8;";

        public static int ExecuteNonQueryD(string sql)
        {
            using (MySqlConnection con = new MySqlConnection(conStrD))
            {
                using (MySqlCommand cmd = new MySqlCommand(sql, con))
                {
                    con.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        /// 执行SQL语句  ,返回总记录数
        /// <param name="sql">要执行的SQL语句</param>
        public static int GetSingle(string sql)//外部调用
        {
            lock (thisLock_MySql)
            {
                int recordCount = 0;
                using (MySqlConnection con = new MySqlConnection(conStr))
                {

                    using (MySqlCommand cmd = new MySqlCommand(sql, con))
                    {
                        con.Open();
                        recordCount = (int)(Int64)cmd.ExecuteScalar();
                        con.Close();
                        return recordCount;
                    }
                }
            }
        }


        //insert delete update
        public static int ExecuteNonQuery(string sql, CommandType cmdType, params MySqlParameter[] pms)
        {
            lock (thisLock_MySql)
            {
                using (MySqlConnection con = new MySqlConnection(conStr))
                {
                    using (MySqlCommand cmd = new MySqlCommand(sql, con))
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
        public static object ExecuteScalar(string sql, CommandType cmdType, params MySqlParameter[] pms)
        {
            lock (thisLock_MySql)
            {
                using (MySqlConnection con = new MySqlConnection(conStr))
                {
                    using (MySqlCommand cmd = new MySqlCommand(sql, con))
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
        public static MySqlDataReader ExecuteReader(string sql, CommandType cmdType, params MySqlParameter[] pms)
        {
            lock (thisLock_MySql)
            {
                MySqlConnection con = new MySqlConnection(conStr);
                using (MySqlCommand cmd = new MySqlCommand(sql, con))
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


        public static DataTable ExecuteDataTable(string sql, CommandType cmdType, params MySqlParameter[] pms)
        {
            lock (thisLock_MySql)
            {
                DataTable dt = new DataTable();
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(sql, conStr))
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

        /// <summary>  
        /// 执行多条SQL语句，实现数据库事务。  
        /// </summary>mysql数据库  
        /// <param name="SQLStringList">多条SQL语句</param>  
        public static void ExecuteNonQueryBatch(List<string> SQLStringList)  //ExecuteSqlTran
        {
            lock (thisLock_MySql)
            {
                using (MySqlConnection conn = new MySqlConnection(conStr))
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = conn;
                    MySqlTransaction tx = conn.BeginTransaction();
                    cmd.Transaction = tx;
                    try
                    {
                        for (int n = 0; n < SQLStringList.Count; n++)
                        {
                            string strsql = SQLStringList[n].ToString();
                            if (strsql.Trim().Length > 1)
                            {
                                cmd.CommandText = strsql;
                                cmd.ExecuteNonQuery();
                            }
                            //后来加上的
                            if (n > 0 && (n % 500 == 0 || n == SQLStringList.Count - 1))
                            {
                                tx.Commit();
                                tx = conn.BeginTransaction();
                            }
                        }
                        //tx.Commit();//原来一次性提交
                    }
                    catch (System.Data.SqlClient.SqlException E)
                    {
                        tx.Rollback();
                        throw new Exception(E.Message);
                    }
                }
            }
        }


    }
}
