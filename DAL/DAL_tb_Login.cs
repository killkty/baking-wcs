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
    public partial class DAL_tb_Login
    {
        public DAL_tb_Login()
        { }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Model.Model_tb_Login model)//***
        {
            ////级别 Level,账号 Account,密码 password,名称 name,状态 state
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into tb_Login(");
            strSql.Append("Level,Account,password,name,state)");
            strSql.Append(" values (");
            strSql.Append("@Level,@Account,@password,@name,@state)");
            SQLiteParameter[] parameters = {
                    new SQLiteParameter("@Level", DbType.String),
                    new SQLiteParameter("@Account", DbType.String),
                    new SQLiteParameter("@password", DbType.String),
                    new SQLiteParameter("@name", DbType.String),
                    new SQLiteParameter("@state", DbType.String)};
            parameters[0].Value = model.Level;
            parameters[1].Value = model.Account;
            parameters[2].Value = model.password;
            parameters[3].Value = model.name;
            parameters[4].Value = model.state;
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
        public bool UpdatePWD(Model.Model_tb_Login model)
        {
            ////级别 Level,账号 Account,密码 password,名称 name,状态 state
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update tb_Login set ");
            strSql.Append("password=@password");
            strSql.Append(" where name=@name");
            SQLiteParameter[] parameters = {
                    new SQLiteParameter("@password", DbType.String),
                    new SQLiteParameter("@name", DbType.String)};
            parameters[0].Value = model.password;
            parameters[1].Value = model.name;

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
        public bool Update(Model.Model_tb_Login model)
        {
            ////级别 Level,账号 Account,密码 password,名称 name,状态 state
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update tb_Login set ");
            strSql.Append("Level=@Level,");
            strSql.Append("Account=@Account,");
            strSql.Append("password=@password,");
            strSql.Append("name=@name,");
            strSql.Append("state=@state");
            strSql.Append(" where ID=@ID");
            SQLiteParameter[] parameters = {
                    new SQLiteParameter("@Level", DbType.String),
                    new SQLiteParameter("@Account", DbType.String),
                    new SQLiteParameter("@password", DbType.String),
                    new SQLiteParameter("@name", DbType.String),
                    new SQLiteParameter("@state", DbType.String),
                    new SQLiteParameter("@ID", DbType.String)};
            parameters[0].Value = model.Level;
            parameters[1].Value = model.Account;
            parameters[2].Value = model.password;
            parameters[3].Value = model.name;
            parameters[4].Value = model.state;
            parameters[5].Value = model.ID;

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
            strSql.Append("delete from tb_Login ");
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
        public Model.Model_tb_Login GetModel(string name)//***PP
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from tb_Login ");
            strSql.Append(" where name=@name");
            SQLiteParameter[] parameters = {
                    new SQLiteParameter("@name", DbType.String)
            };
            parameters[0].Value = name;

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
        public Model.Model_tb_Login DataRowToModel(DataRow row)//***
        {
            //级别 Level,账号 Account,密码 password,名称 name,状态 state

            Model.Model_tb_Login model = new Model.Model_tb_Login();
            if (row != null)
            {
                if (row["ID"] != null && row["ID"].ToString() != "")
                {
                    model.ID = int.Parse(row["ID"].ToString());
                }
                if (row["Level"] != null && row["Level"].ToString() != "")
                {
                    model.Level = row["Level"].ToString();
                }
                if (row["Account"] != null && row["Account"].ToString() != "")
                {
                    model.Account = row["Account"].ToString();
                }
                if (row["password"] != null && row["password"].ToString() != "")
                {
                    model.password = row["password"].ToString();
                }
                if (row["name"] != null && row["name"].ToString() != "")
                {
                    model.name = row["name"].ToString();
                }
                if (row["state"] != null && row["state"].ToString() != "")
                {
                    model.state = row["state"].ToString();
                }
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataTable GetList()  //***
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM tb_Login ");
            return SQLiteHelper.ExecuteQuery(strSql.ToString());
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataTable GetList(string strWhere)
        {
            //string sql = "select 级别名称 from tb_用户级别";
            //    string sql2 = "select 级别名称,账号,名称,case when z1.状态='true' then '可用' when z1.状态='false' then '停用' end as '当前状态' from tb_用户登陆 z1 inner join tb_用户级别 z2 on z1.级别=z2.编号";
            //级别 Level,账号 Account,密码 password,名称 name,状态 state
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,Account as 所属权限组,password,name as 用户名 ");//Account as 所属权限组,name as 用户名
            strSql.Append(" FROM tb_Login ");
            strSql.Append(" where Account=@Account");
            SQLiteParameter[] parameters = {
                    new SQLiteParameter("@Account", DbType.String)
            };
            parameters[0].Value = strWhere;
            return SQLiteHelper.ExecuteQuery(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM tb_Login ");
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
