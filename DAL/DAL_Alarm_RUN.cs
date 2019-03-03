using System;
using System.Data;
using System.Data.OleDb;
using System.Text;

namespace DAL
{
    public class DAL_Alarm_RUN
    {
        //插入操作
        public int Insert(Model.Model_Alarm_RUN model)
        {
            string sql = "insert into Alarm_RUN (Time_A,Type_Code,Type_Description,Data_code,Data_Description,Register,Area) values(@Time_A,@Type_Code,@Type_Description,@Data_code,@Data_Description,@Register,@Area)";
            OleDbParameter[] pms = new OleDbParameter[] {
            new OleDbParameter("@Time_A",OleDbType.Date){ Value=model.Time_A},
            new OleDbParameter("@Type_Code",OleDbType.VarChar,50){ Value=model.Type_Code},
            new OleDbParameter("@Type_Description",OleDbType.VarChar,50){ Value=model.Type_Description},
            new OleDbParameter("@Data_code",OleDbType.VarChar,50){ Value=model.Data_code},
            new OleDbParameter("@Data_Description",OleDbType.VarChar,50){ Value=model.Data_Description},
            new OleDbParameter("@Register",OleDbType.VarChar,50){ Value=model.Register},
            new OleDbParameter("@Area",OleDbType.VarChar,50){ Value=model.Area}
            };
            return Helper.AccessHelper.ExecuteNonQuery(sql, System.Data.CommandType.Text, pms);
        }

        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            if (strWhere != "")
            {
                strSql.Append("select count(1) FROM Alarm_RUN " + strWhere);
            }
            else
            {
                strSql.Append("select count(1) FROM Alarm_RUN ");
            }

                object obj = Helper.AccessHelper.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }


        public DataTable GetListPage(string strWhere, int pageSize, int page)
        {

            string str = "ID as 索引,Time_A as 时间,Type_Code as 类型代码,Type_Description as 类型描述,Data_code as 数据项代码,Data_Description as 数据项描述,Register as PLC寄存器 ,Area as 区域 ";
            DataTable ds = new DataTable();
            string strSql = "";
            if (strWhere != "")
            {
                strSql = "select top " + pageSize + " " + str  +
                    " from Alarm_RUN " +
                    "where id >=" +
                    "(select top 1 max(id) from " +
                    "(select top " + (((page - 1) * pageSize) + 1).ToString() + " * from Alarm_RUN  " + strWhere + " order by id)" +
                    ") order by id";
            }
            else
            {
                strSql = "select top " + pageSize + " " + str  +
                    " from Alarm_RUN " +
                    "where id >=" +
                    "(select top 1 max(id) from " +
                    "(select top " + (((page - 1) * pageSize) + 1).ToString() + " * from Alarm_RUN order by id)" +
                    ") order by id";

            }
            ds = Helper.AccessHelper.ExecuteDataTable(strSql, System.Data.CommandType.Text);
            return ds;
        }

        //删除
        public int DeleteByContactId(string strWhere)
        {
            string sql = "delete from Alarm_RUN  " + strWhere;
            return Helper.AccessHelper.ExecuteNonQuery(sql, System.Data.CommandType.Text);
        }
    }
}
