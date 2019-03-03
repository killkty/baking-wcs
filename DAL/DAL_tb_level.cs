using Helper;
using System;
using System.Data;
using System.Data.SQLite;
using System.Text;

namespace DAL
{
    /// <summary>
    /// 数据访问类:Category
    /// </summary>
    public partial class DAL_tb_Level
    {
        public DAL_tb_Level()
        { }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Model.Model_tb_Level model)
        {
            //级别名称 _Account,设置A LevelA,设置B,设置C,设置D,设置E,设置F
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into tb_level(");
            strSql.Append("_Account,LevelA,LevelB,LevelC,LevelD,LevelE,LevelF)");
            strSql.Append(" values (");
            strSql.Append("@_Account,@LevelA,@LevelB,@LevelC,@LevelD,@LevelE,@LevelF)");
            SQLiteParameter[] parameters = {
                    new SQLiteParameter("@_Account", DbType.String),
                    new SQLiteParameter("@LevelA", DbType.Int32,4),
                    new SQLiteParameter("@LevelB", DbType.Int32,4),
                    new SQLiteParameter("@LevelC", DbType.Int32,4),
                    new SQLiteParameter("@LevelD", DbType.Int32,4),
                    new SQLiteParameter("@LevelE", DbType.Int32,4),
                    new SQLiteParameter("@LevelF", DbType.Int32,4)};
            parameters[0].Value = model._Account;
            parameters[1].Value = model.LevelA;
            parameters[2].Value = model.LevelB;
            parameters[3].Value = model.LevelC;
            parameters[4].Value = model.LevelD;
            parameters[5].Value = model.LevelE;
            parameters[6].Value = model.LevelF;
            int rows = SQLiteHelper.ExecuteNonQuery(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.Model_tb_Level model)
        {
            //级别名称 _Account,设置A LevelA,设置B,设置C,设置D,设置E,设置F
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update tb_level set ");
            strSql.Append("LevelA=@LevelA,");
            strSql.Append("LevelB=@LevelB,");
            strSql.Append("LevelC=@LevelC,");
            strSql.Append("LevelD=@LevelD,");
            strSql.Append("LevelE=@LevelE,");
            strSql.Append("LevelF=@LevelF");
            strSql.Append(" where _Account=@_Account");
            SQLiteParameter[] parameters = {
                    new SQLiteParameter("@LevelA", DbType.Int32,4),
                    new SQLiteParameter("@LevelB", DbType.Int32,4),
                    new SQLiteParameter("@LevelC", DbType.Int32,4),
                    new SQLiteParameter("@LevelD", DbType.Int32,4),
                    new SQLiteParameter("@LevelE", DbType.Int32,4),
                    new SQLiteParameter("@LevelF", DbType.Int32,4),
                    new SQLiteParameter("@_Account", DbType.String)};
            parameters[0].Value = model.LevelA;
            parameters[1].Value = model.LevelB;
            parameters[2].Value = model.LevelC;
            parameters[3].Value = model.LevelD;
            parameters[4].Value = model.LevelE;
            parameters[5].Value = model.LevelF;
            parameters[6].Value = model._Account;
            int rows = SQLiteHelper.ExecuteNonQuery(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Category ");
            strSql.Append(" where Id=@Id");
            SQLiteParameter[] parameters = {
                    new SQLiteParameter("@Id", DbType.Int32,4)
            };
            parameters[0].Value = Id;


            int rows = SQLiteHelper.ExecuteNonQuery(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string Idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Category ");
            strSql.Append(" where Id in (" + Idlist + ")  ");
            int rows = SQLiteHelper.ExecuteNonQuery(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.Model_tb_Level GetModel(string _Account)//***
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from tb_level ");
            strSql.Append(" where _Account=@_Account");
            SQLiteParameter[] parameters = {
                    new SQLiteParameter("@_Account", DbType.String)
            };
            parameters[0].Value = _Account;
            DataTable ds = SQLiteHelper.ExecuteQuery(strSql.ToString(), parameters);
            if (ds.Rows.Count > 0)
            {
                return DataRowToModel(ds.Rows[0]);
            }
            else
            {
                return null;
            }

        }

        /// <summary>
        /// 得到一个对象实体  
        /// </summary>
        public Model.Model_tb_Level DataRowToModel(DataRow row)//***
        {
            //级别名称 _Account,设置A LevelA,设置B,设置C,设置D,设置E,设置F
            Model.Model_tb_Level model = new Model.Model_tb_Level();
            if (row != null)
            {
                if (row["_Account"] != null && row["_Account"].ToString() != "")
                {
                    model._Account = row["_Account"].ToString();
                }
                if (row["LevelA"] != null && row["LevelA"].ToString() != "")
                {
                    model.LevelA = int.Parse(row["LevelA"].ToString());
                }
                if (row["LevelB"] != null && row["LevelB"].ToString() != "")
                {
                    model.LevelB = int.Parse(row["LevelB"].ToString());
                }
                if (row["LevelC"] != null && row["LevelC"].ToString() != "")
                {
                    model.LevelC = int.Parse(row["LevelC"].ToString());
                }
                if (row["LevelD"] != null && row["LevelD"].ToString() != "")
                {
                    model.LevelD = int.Parse(row["LevelD"].ToString());
                }
                if (row["LevelE"] != null && row["LevelE"].ToString() != "")
                {
                    model.LevelE = int.Parse(row["LevelE"].ToString());
                }
                if (row["LevelF"] != null && row["LevelF"].ToString() != "")
                {
                    model.LevelF = int.Parse(row["LevelF"].ToString());
                }
            }
            return model;
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataTable GetList()
        {
            //string sql = "select 级别名称 from tb_用户级别";
            //级别名称 _Account,设置A LevelA,设置B,设置C,设置D,设置E,设置F
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select _Account as 权限组名称 ");
            strSql.Append(" FROM tb_level ");
            return SQLiteHelper.ExecuteQuery(strSql.ToString());
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataTable Get_Account()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM tb_level ");
            return SQLiteHelper.ExecuteQuery(strSql.ToString());
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataTable GetList(string strWhere)
        {
            //string sql = "select 级别名称 from tb_用户级别";

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,Pid,Name ");
            strSql.Append(" FROM tb_level ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return SQLiteHelper.ExecuteQuery(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM tb_level ");
            //if (strWhere.Trim() != "")
            //{
            //    strSql.Append(" where " + strWhere);
            //}
            object obj = SQLiteHelper.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataTable GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.Id desc");
            }
            strSql.Append(")AS Row, T.*  from Category T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return SQLiteHelper.ExecuteQuery(strSql.ToString());
        }

    }
}
