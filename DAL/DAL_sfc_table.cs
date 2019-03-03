using Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Text;

namespace DAL
{
    /// <summary>
    /// 数据访问类:Category
    /// </summary>
    public partial class DAL_Sfc_table
    {
        public DAL_Sfc_table()
        { }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void AddList(List<Model.Model_Sfc_table> model)
        {
            string strSql ;
            List<string> _list = new List<string>();
            for (int i = 0; i < model.Count; i++)
            {
               strSql = string.Format("insert into sfc_table(Sfc,TrayID,Position) " +
                "values ('{0}','{1}','{2}')", model[i].Sfc, model[i].TrayID, model[i].Position);
                _list.Add(strSql);
            }  
            SQLiteHelper.ExecuteNonQueryBatch(_list);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void UpdateList(List<Model.Model_Sfc_table> model)
        {
            string strSql;
            List<string> _list = new List<string>();
            for (int i = 0; i < model.Count; i++)
            {
                strSql = string.Format("update sfc_table set " +
                    " Sfc='{0}'" +
                    " where TrayID='{1}'  AND Position='{2}'", model[i].Sfc, model[i].TrayID, model[i].Position);
                _list.Add(strSql);
            }
            SQLiteHelper.ExecuteNonQueryBatch(_list);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.Model_Sfc_table GetModel(string ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from sfc_table ");
            strSql.Append(" where ID=@ID");
            SQLiteParameter[] parameters = {
                    new SQLiteParameter("@ID", DbType.String)
            };
            parameters[0].Value = ID;

            Model.Model_Sfc_table model = new Model.Model_Sfc_table();
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
        public Model.Model_Sfc_table DataRowToModel(DataRow row)
        {
            Model.Model_Sfc_table model = new Model.Model_Sfc_table();
            if (row != null)
            {
                if (row["Sfc"] != null && row["Sfc"].ToString() != "")
                {
                    model.Sfc = row["Sfc"].ToString();
                }
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataTable GetList(string TrayID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");//
            strSql.Append(" FROM sfc_table ");
            strSql.Append(" where TrayID=@TrayID");
            SQLiteParameter[] parameters = {
                    new SQLiteParameter("@TrayID", DbType.String)
            };
            parameters[0].Value = TrayID;
            return SQLiteHelper.ExecuteQuery(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataTable GetPosition(string TrayID,string Position)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");//
            strSql.Append(" FROM sfc_table ");
            strSql.Append(" where TrayID=@TrayID AND Position=@Position");
            SQLiteParameter[] parameters = {
                    new SQLiteParameter("@TrayID", DbType.String),
                    new SQLiteParameter("@Position", DbType.String)
            };
            parameters[0].Value = TrayID;
            parameters[1].Value = Position;
            return SQLiteHelper.ExecuteQuery(strSql.ToString(), parameters);
        }
        

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            if (strWhere.Trim() != "")
            {
                strSql.Append("select count(1) FROM sfc_table " + strWhere);
            }
            else
            {
                strSql.Append("select count(1) FROM sfc_table ");
            }
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

        public bool IfGetSfc(string TrayID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from sfc_table ");
            strSql.Append(" where TrayID=@TrayID");
            SQLiteParameter[] parameters = {
                    new SQLiteParameter("@TrayID", DbType.String)
            };
            parameters[0].Value = TrayID;
            DataTable ds = SQLiteHelper.ExecuteQuery(strSql.ToString(), parameters);
            if (ds.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public bool IfGetSfc(string TrayID,string Position)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from sfc_table ");
            strSql.Append(" where TrayID=@TrayID  AND  Position=@Position  ");
            SQLiteParameter[] parameters = {
                    new SQLiteParameter("@TrayID", DbType.String),
                    new SQLiteParameter("@Position", DbType.String)
            };
            parameters[0].Value = TrayID;
            parameters[1].Value = Position;
            DataTable ds = SQLiteHelper.ExecuteQuery(strSql.ToString(), parameters);
            if (ds.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public DataTable GetListPage(string strWhere, int pageSize, int page)
        {
            DataTable ds = new DataTable();
            string strSql = "";
            if (strWhere != "")
            {
                /*在C#这样创建SQL语言更简洁 size:每页显示条数，index页码  asc desc倒序 也可以在GuestInfo 加where条件 */
                //string.Format("select * from GuestInfo order by GuestId desc limit {0} offset {0}*{1}", size, index - 1);
                strSql = "select ID as 索引,Sfc as 电芯条码,TrayID as 托盘条码,Position as 绑定位置 from sfc_table " + strWhere + " order by ID  limit " + pageSize + "  offset  "+ pageSize * (page-1);  
            }
            else
            {
                strSql = "select ID as 索引,Sfc as 电芯条码,TrayID as 托盘条码,Position as 绑定位置 from sfc_table order by ID limit " + pageSize + "  offset  " + pageSize * (page - 1);
            }
            ds = SQLiteHelper.ExecuteQuery(strSql);
            return ds;
        }

    }
}
