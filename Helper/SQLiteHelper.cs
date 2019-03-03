using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;

namespace Helper
{
    public class SQLiteHelper
    {
        private static Object thisLock_SQLite = new Object();
        private static string connectionString = string.Empty;

        /// <summary>
        /// 根据数据源、密码、版本号设置连接字符串。
        /// </summary>
        /// <param name="datasource">数据源。</param>
        /// <param name="password">密码。</param>
        /// <param name="version">版本号（缺省为3）。</param>
        public static void SetConnectionString(string datasource, int version = 3)
        {
            connectionString = string.Format("Data Source={0};Version={1};", datasource, version);
        }

        /// <summary>
        /// 创建一个数据库文件。如果存在同名数据库文件，则会覆盖。
        /// </summary>
        /// <param name="dbName">数据库文件名。为null或空串时不创建。</param>
        /// <param name="password">（可选）数据库密码，默认为空。</param>
        /// <exception cref="Exception"></exception>
        public static void CreateDB(string dbName)
        {
            if (!string.IsNullOrEmpty(dbName))
            {
                try { SQLiteConnection.CreateFile(dbName); }
                catch (Exception) { throw; }
            }
        }

        /// 执行SQL语句，返回影响的记录行数
        /// <param name="sql">要执行的SQL语句</param>
        public static int RunSql(string sql)//外部调用
        {
            lock (thisLock_SQLite)
            {
                int result = -1;
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {

                    using (SQLiteCommand cmd = new SQLiteCommand(sql, connection))
                    {
                        connection.Open();
                        result = cmd.ExecuteNonQuery();
                        connection.Close();
                        return result;
                    }
                }
            }
        }

        /// 执行SQL语句  ,返回总记录数
        /// <param name="sql">要执行的SQL语句</param>
        public static int GetSingle(string sql)//外部调用
        {
            lock (thisLock_SQLite)
            {
                int recordCount = 0;
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {

                    using (SQLiteCommand cmd = new SQLiteCommand(sql, connection))
                    {
                        connection.Open();
                        recordCount = (int)(Int64)cmd.ExecuteScalar();
                        connection.Close();
                        return recordCount;
                    }
                }
            }
        }
        /// <summary> 
        /// 对SQLite数据库执行增删改操作，返回受影响的行数。 
        /// </summary> 
        /// <param name="sql">要执行的增删改的SQL语句。</param> 
        /// <param name="parameters">执行增删改语句所需要的参数，参数必须以它们在SQL语句中的顺序为准。</param> 
        /// <returns></returns> 
        /// <exception cref="Exception"></exception>
        public static int ExecuteNonQuery(string sql, params SQLiteParameter[] parameters)
        {
            lock (thisLock_SQLite)
            {
                int affectedRows = 0;
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(connection))
                    {
                        try
                        {
                            connection.Open();
                            command.CommandText = sql;
                            if (parameters.Length != 0)
                            {
                                command.Parameters.AddRange(parameters);
                            }
                            affectedRows = command.ExecuteNonQuery();
                            connection.Close();
                        }
                        catch (Exception) { throw; }
                    }
                }
                return affectedRows;
            }
        }



        /// <summary>
        /// 执行查询语句，并返回第一个结果。
        /// </summary>
        /// <param name="sql">查询语句。</param>
        /// <returns>查询结果。</returns>
        /// <exception cref="Exception"></exception>
        public static object ExecuteScalar(string sql, params SQLiteParameter[] parameters)
        {
            lock (thisLock_SQLite)
            {
                using (SQLiteConnection conn = new SQLiteConnection(connectionString))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand(conn))
                    {
                        try
                        {
                            conn.Open();
                            cmd.CommandText = sql;
                            if (parameters.Length != 0)
                            {
                                cmd.Parameters.AddRange(parameters);
                            }
                            return cmd.ExecuteScalar();
                        }
                        catch (Exception) { throw; }
                    }
                }
            }
        }

        /// <summary> 
        /// 执行一个查询语句，返回一个包含查询结果的DataTable。 
        /// </summary> 
        /// <param name="sql">要执行的查询语句。</param> 
        /// <param name="parameters">执行SQL查询语句所需要的参数，参数必须以它们在SQL语句中的顺序为准。</param> 
        /// <returns></returns> 
        /// <exception cref="Exception"></exception>
        public static DataTable ExecuteQuery(string sql, params SQLiteParameter[] parameters)
        {
       //     connectionString = "E:\\NewBakingMES\\database\\database.sqlite";
            lock (thisLock_SQLite)
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString)) //connectionString
                {
                    using (SQLiteCommand command = new SQLiteCommand(sql, connection))
                    {
                        if (parameters.Length != 0)
                        {
                            command.Parameters.AddRange(parameters);
                        }
                        SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
                        DataTable data = new DataTable();
                        try
                        {
                            adapter.Fill(data);
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                        return data;
                    }
                }
            }
        }

        /// <summary> 
        /// 执行一个查询语句，返回一个关联的SQLiteDataReader实例。 
        /// </summary> 
        /// <param name="sql">要执行的查询语句。</param> 
        /// <param name="parameters">执行SQL查询语句所需要的参数，参数必须以它们在SQL语句中的顺序为准。</param> 
        /// <returns></returns> 
        /// <exception cref="Exception"></exception>
        public static SQLiteDataReader ExecuteReader(string sql, params SQLiteParameter[] parameters)
        {
            lock (thisLock_SQLite)
            {
                SQLiteConnection connection = new SQLiteConnection(connectionString);
                SQLiteCommand command = new SQLiteCommand(sql, connection);
                try
                {
                    if (parameters.Length != 0)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    connection.Open();
                    return command.ExecuteReader(CommandBehavior.CloseConnection);
                }
                catch (Exception) { throw; }
            }
        }

        /// <summary> 
        /// 查询数据库中的所有数据类型信息。
        /// </summary> 
        /// <returns></returns> 
        /// <exception cref="Exception"></exception>
        public static DataTable GetSchema()
        {
            lock (thisLock_SQLite)
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    try
                    {
                        connection.Open();
                        return connection.GetSchema("TABLES");
                    }
                    catch (Exception) { throw; }
                }
            }
        }


        /// <summary>
        /// 批量处理数据操作语句。
        /// </summary>
        /// <param name="list">SQL语句集合。</param>
        /// <exception cref="Exception"></exception>
        public static void ExecuteNonQueryBatch(List<KeyValuePair<string, SQLiteParameter[]>> list)
        {
            lock (thisLock_SQLite)
            {
                using (SQLiteConnection conn = new SQLiteConnection(connectionString))
                {
                    try { conn.Open(); }
                    catch { throw; }

                    //DbTransaction

                    using (SQLiteTransaction tran = conn.BeginTransaction())
                    {
                        using (SQLiteCommand cmd = new SQLiteCommand(conn))
                        {
                            try
                            {
                                foreach (var item in list)
                                {
                                    cmd.CommandText = item.Key;
                                    if (item.Value != null)
                                    {
                                        cmd.Parameters.AddRange(item.Value);
                                    }
                                    cmd.ExecuteNonQuery();
                                }
                                tran.Commit();  //   trans.Commit(); // <---------
                            }
                            catch (Exception) { tran.Rollback(); throw; }   //  trans.Rollback(); // <-------------
                        }
                    }
                }

            }
        }



        /// <summary>
        /// 批量处理数据操作语句。
        /// </summary>
        /// <param name="list">SQL语句集合。</param>
        /// <exception cref="Exception"></exception>
        public static void ExecuteNonQueryBatch(List<string> _list)
        {
            lock (thisLock_SQLite)
            {
                using (SQLiteConnection conn = new SQLiteConnection(connectionString))
                {
                    try { conn.Open(); }
                    catch { throw; }
                    using (SQLiteTransaction tran = conn.BeginTransaction())
                    {
                        using (SQLiteCommand cmd = new SQLiteCommand(conn))
                        {
                            try
                            {
                                for (int i = 0; i < _list.Count; i++)
                                {
                                    cmd.CommandText = _list[i];
                                    cmd.ExecuteNonQuery();
                                }
                                tran.Commit();
                            }
                            catch (Exception) { tran.Rollback(); throw; }   // 
                        }
                    }
                }
            }

        }




    }
}
