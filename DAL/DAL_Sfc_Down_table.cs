using Helper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Text;

namespace DAL
{
    public partial class DAL_Sfc_Down_table
    {

        ////插入操作
        //public int Insert(Model.Model_Alarm_oven model)
        //{
        //    string sql = "insert into Alarm_oven (Time_A,Type_Code,Type_Description,Data_code,Data_Description,Register,Area) values(@Time_A,@Type_Code,@Type_Description,@Data_code,@Data_Description,@Register,@Area)";
        //    MySqlParameter[] pms = new MySqlParameter[] {
        //    new MySqlParameter("@Time_A",MySqlDbType.DateTime){ Value=model.Time_A},
        //    new MySqlParameter("@Type_Code",MySqlDbType.VarChar){ Value=model.Type_Code},
        //    new MySqlParameter("@Type_Description",MySqlDbType.VarChar){ Value=model.Type_Description},
        //    new MySqlParameter("@Data_code",MySqlDbType.VarChar){ Value=model.Data_code},
        //    new MySqlParameter("@Data_Description",MySqlDbType.VarChar){ Value=model.Data_Description},
        //    new MySqlParameter("@Register",MySqlDbType.VarChar){ Value=model.Register},
        //    new MySqlParameter("@Area",MySqlDbType.VarChar){ Value=model.Area}
        //    };
        //    return Helper.MySqlHelper.ExecuteNonQuery(sql, System.Data.CommandType.Text, pms);
        //}



        //private int ID;            //1,ID

        //private DateTime? BkTimeEnd;  //2,BK结束时间
        //private string Sfc;        //3,sfc电芯条码
        //private string SfcTest;    //4,测试条码
        //private string Position;   //5,条码 位置

        //private DateTime? MesTime;     //7,上传Mes时间
        //private string MesFlag;     //7,上传Mes标志



         //BkTimeEnd,Sfc,SfcTest,Position,MesTime,MesFlag;     //7,上传Mes标志


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void AddList(List<Model.Model_Sfc_Down_table> model)
        {
            string strSql;
            List<string> _list = new List<string>();
            for (int i = 0; i < model.Count; i++)
            {
                strSql = string.Format("insert into sfc_down_table (BkTimeEnd,Sfc,SfcTest,Position,MesTime,MesFlag) " +
                 "values ('{0}','{1}','{2}','{3}','{4}','{5}')", 
                 model[i].BkTimeEnd, model[i].Sfc, 
                 model[i].SfcTest, model[i].Position, 
                 model[i].MesTime, model[i].MesFlag);
                _list.Add(strSql);
            }
            Helper.MySqlHelper.ExecuteNonQueryBatch(_list);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataTable GetList(Model.Model_Sfc_Down_table model)
        {
            string sql = "select * FROM sfc_down_table where TrayID=@TrayID (BkLine,BkAB,BkNumber,Tray1,Tray2,Tray3,SfcTest,BkTimeStart,BkTimeEnd, BkTime, TAlarm, KPaAlarm,DataTime,CWCValue,CWCResult,DataFlag,MesTime,MesFlag,BkResource) values(@BkLine,@BkAB,@BkNumber,@Tray1,@Tray2,@Tray3,@SfcTest,@BkTimeStart,@BkTimeEnd, @BkTime, @TAlarm, @KPaAlarm,@DataTime,@CWCValue,@CWCResult,@DataFlag,@MesTime,@MesFlag,@BkResource)";
            MySqlParameter[] pms = new MySqlParameter[] {
            //BkLine,BkAB,BkNumber,Tray1;     
            new MySqlParameter("@BkLine",MySqlDbType.DateTime){ Value=model.BkTimeEnd},
            new MySqlParameter("@BkAB",MySqlDbType.VarChar){ Value=model.MesFlag},
            new MySqlParameter("@BkNumber",MySqlDbType.VarChar){ Value=model.Sfc},
             new MySqlParameter("@BkResource",MySqlDbType.VarChar){ Value=model.SfcTest}

            };
            return Helper.MySqlHelper.ExecuteDataTable(sql, System.Data.CommandType.Text, pms);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void UpdateList(List<Model.Model_Sfc_Down_table> model)
        {
            string strSql;
            List<string> _list = new List<string>();
            for (int i = 0; i < model.Count; i++)
            {
                //BkTimeEnd,Sfc,SfcTest,Position,MesTime,MesFlag;     //7,上传Mes标志

                strSql = string.Format("update sfc_down_table set " +
                    " BkTimeEnd='{0}',Sfc='{1}'," +
                    " SfcTest='{2}',Position='{3}', MesTime='{4}'" +
                    " where MesFlag='{5}' ", model[i].BkTimeEnd, model[i].Sfc, model[i].Position, model[i].MesTime, model[i].MesFlag);
                _list.Add(strSql);
            }
            Helper.MySqlHelper.ExecuteNonQueryBatch(_list);
        }



        public DataTable GetListPage(string strWhere, int pageSize, int page)
        {
            DataTable ds = new DataTable();
            string strSql = "";

            //private int ID;            //1,ID

            //private DateTime? BkTimeEnd;  //2,BK结束时间
            //private string Sfc;        //3,sfc电芯条码
            //private string SfcTest;    //4,测试条码
            //private string Position;   //5,条码位置

            //private DateTime? MesTime;     //7,上传Mes时间
            //private string MesFlag;     //7,上传Mes标志

            string str = "ID as 序列号,BkTimeEnd as BK结束时间,Sfc as Sfc电芯条码,SfcTest as 测试条码," +
                "Position as 条码位置,MesTime as 上传Mes时间,MesFlag as 上传Mes标志 ";

            strSql = "select " + str + "  from sfc_down_table " + strWhere + "  limit " + pageSize + "  offset  " + pageSize * (page - 1);
            ds = Helper.MySqlHelper.ExecuteDataTable(strSql, System.Data.CommandType.Text);
            return ds;
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM sfc_down_table " + strWhere);
            object obj = Helper.MySqlHelper.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        //删除
        public int DeleteByContactId(string strWhere)
        {
            string sql = "delete from sfc_down_table  " + strWhere;
            return Helper.MySqlHelper.ExecuteNonQuery(sql, System.Data.CommandType.Text);
        }




    }
}
