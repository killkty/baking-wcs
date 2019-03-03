using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DAL
{
    public class DAL_SFC_Water
    {
        //  1 ID, 2 拉线号, 3 A/B线,4 炉腔编号 ,5 托盘1编号,6 托盘2编号
        // ,7 托盘3编号,8 测试条码,9 BK开始时间,10 BK结束时间,11 BK时长,
        //  12 BK温度报警,13 BK真空报警,14 上传数据库时间,15 水含量值,16 水含量结果,17 上传数据库标志,18 上传Mes时间,19 上传Mes标志,


        //private int _ID;             //1,ID
        //private string BkLine;      //2,拉线号
        //private string BkAB;        //3,A/B线
        //private string BkNumber;    //4,炉腔编号
        //private string Tray1;       //5,托盘1编号

        //private string Tray2;       //6,托盘2编号
        //private string Tray3;       //7,托盘3编号
        //private string SfcTest;     //8,测试条码
        //private string BkTimeStart; //9,BK开始时间

        //private string BkTimeEnd;   //10,BK结束时间   
        //private string BkTime;      //11,BK时长
        //private string TAlarm;      //12,BK温度报警
        //private string KPaAlarm;    //13,BK真空报警

        //private string DataTime;    //14,上传数据库时间
        //private string CWCValue;    //15,水含量值
        //private string CWCResult;   //16,水含量结果
        //private string DataFlag;    //17,上传数据库标志

        //private string MesTime;     //18,上传Mes时间
        //private string MesFlag;     //19,上传Mes标志




        //BkLine,BkAB,BkNumber,Tray1;       //5,托盘1编号

        //Tray2,Tray3,SfcTest,BkTimeStart

        // BkTimeEnd,BkTime,TAlarm, KPaAlarm;    //13,BK真空报警

        //DataTime,CWCValue,CWCResult,DataFlag;    //17,上传数据库标志

        //MesTime,MesFlag;     //19,上传Mes标志



        //BkLine,BkAB,BkNumber,Tray1,Tray2,Tray3,SfcTest,BkTimeStart,BkTimeEnd, BkTime, TAlarm, KPaAlarm,DataTime,CWCValue,CWCResult,DataFlag,MesTime,MesFlag    //19,上传Mes标志



        //插入操作
        public int Insert(Model.Model_SFC_Water model)
        {
            string sql = "insert into SFC_Water (BkLine,BkAB,BkNumber,Tray1,Tray2,Tray3,SfcTest,BkTimeStart,BkTimeEnd, BkTime, TAlarm, KPaAlarm,DataTime,CWCValue,CWCResult,DataFlag,MesTime,MesFlag,BkResource) values(@BkLine,@BkAB,@BkNumber,@Tray1,@Tray2,@Tray3,@SfcTest,@BkTimeStart,@BkTimeEnd, @BkTime, @TAlarm, @KPaAlarm,@DataTime,@CWCValue,@CWCResult,@DataFlag,@MesTime,@MesFlag,@BkResource)";
            MySqlParameter[] pms = new MySqlParameter[] {
            //BkLine,BkAB,BkNumber,Tray1;     
            new MySqlParameter("@BkLine",MySqlDbType.DateTime){ Value=model.BkLine},
            new MySqlParameter("@BkAB",MySqlDbType.VarChar){ Value=model.BkAB},
            new MySqlParameter("@BkNumber",MySqlDbType.VarChar){ Value=model.BkNumber},
            new MySqlParameter("@Tray1",MySqlDbType.VarChar){ Value=model.Tray1},
            
            //Tray2,Tray3,SfcTest,BkTimeStart
            new MySqlParameter("@Tray2",MySqlDbType.VarChar){ Value=model.Tray2},
            new MySqlParameter("@Tray3",MySqlDbType.VarChar){ Value=model.Tray3},
            new MySqlParameter("@SfcTest",MySqlDbType.VarChar){ Value=model.SfcTest},
            new MySqlParameter("@BkTimeStart",MySqlDbType.VarChar){ Value=model.BkTimeStart},
            
           // BkTimeEnd,BkTime,TAlarm, KPaAlarm;   
            new MySqlParameter("@BkTimeEnd",MySqlDbType.VarChar){ Value=model.BkTimeEnd},
            new MySqlParameter("@BkTime",MySqlDbType.VarChar){ Value=model.BkTime},
            new MySqlParameter("@TAlarm",MySqlDbType.VarChar){ Value=model.TAlarm},
            new MySqlParameter("@KPaAlarm",MySqlDbType.VarChar){ Value=model.KPaAlarm},
            
            //DataTime,CWCValue,CWCResult,DataFlag;    
            new MySqlParameter("@DataTime",MySqlDbType.VarChar){ Value=model.DataTime},
            new MySqlParameter("@CWCValue",MySqlDbType.VarChar){ Value=model.CWCValue},
            new MySqlParameter("@CWCResult",MySqlDbType.VarChar){ Value=model.CWCResult},
            new MySqlParameter("@DataFlag",MySqlDbType.VarChar){ Value=model.DataFlag},
            
            //MesTime,MesFlag;     
            new MySqlParameter("@MesTime",MySqlDbType.VarChar){ Value=model.MesTime},
            new MySqlParameter("@MesFlag",MySqlDbType.VarChar){ Value=model.MesFlag},
            //,@BkResource
            new MySqlParameter("@BkResource",MySqlDbType.VarChar){ Value=model.BkResource}
            };
            return Helper.MySqlHelper.ExecuteNonQuery(sql, System.Data.CommandType.Text, pms);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataTable GetList(Model.Model_SFC_Water model)
        {
            //以 测试电芯条码 为条件
            string sql = "select * FROM SFC_Water where  DataFlag=@DataFlag AND MesFlag=@MesFlag ";
            MySqlParameter[] pms = new MySqlParameter[] {
            new MySqlParameter("@DataFlag",MySqlDbType.VarChar){ Value=model.DataFlag},
            new MySqlParameter("@MesFlag",MySqlDbType.VarChar){ Value=model.MesFlag}
            };
            return Helper.MySqlHelper.ExecuteDataTable(sql, System.Data.CommandType.Text, pms);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataTable GetList2(Model.Model_SFC_Water model)
        {
            //StringBuilder strSql = new StringBuilder();
            //strSql.Append("select * ");//
            //strSql.Append(" FROM sfc_table ");
            //strSql.Append(" where TrayID=@TrayID");
            //MySqlParameter[] pms = {
            //        new MySqlParameter("@TrayID", DbType.String)
            //};
            //pms[0].Value = TrayID;

            //以 测试电芯条码 为条件  strSql.Append("select count(1) FROM Alarm_RUN " + strWhere);

            string sql = "select * FROM SFC_Water where SfcTest=@SfcTest";
            MySqlParameter[] pms = new MySqlParameter[] {
            new MySqlParameter("@SfcTest",MySqlDbType.VarChar){ Value=model.SfcTest},
            new MySqlParameter("@MesFlag",MySqlDbType.VarChar){ Value=model.MesFlag},
            //,@BkResource
             new MySqlParameter("@SfcTest",MySqlDbType.VarChar){ Value=model.SfcTest}

            };
            //return Helper.MySqlHelper.ExecuteNonQuery(sql, System.Data.CommandType.Text, pms);



            return Helper.MySqlHelper.ExecuteDataTable(sql, System.Data.CommandType.Text, pms);
        }



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

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void AddList(List<Model.Model_SFC_Water> model)
        {
            string strSql;
            List<string> _list = new List<string>();
            for (int i = 0; i < model.Count; i++)
            {
                strSql = string.Format("insert into SFC_Water (BkLine,BkAB,BkNumber,Tray1,Tray2,Tray3,SfcTest,BkTimeStart,BkTimeEnd, BkTime, TAlarm, KPaAlarm,DataTime,CWCValue,CWCResult,DataFlag,MesTime,MesFlag,BkResource) " +
                    "values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}')",
                    model[i].BkLine,
                    model[i].BkAB,
                    model[i].BkNumber,
                    model[i].Tray1,

                    model[i].Tray2,
                    model[i].Tray3,
                    model[i].SfcTest,
                    model[i].BkTimeStart,
 
                    model[i].BkTimeEnd,
                    model[i].BkTime,
                    model[i].TAlarm,
                    model[i].KPaAlarm,

                    model[i].DataTime,
                    model[i].CWCValue,
                    model[i].CWCResult,
                    model[i].DataFlag,
    
                    model[i].MesTime,
                    model[i].MesFlag,
                    model[i].BkResource);
            _list.Add(strSql);
            }
            Helper.MySqlHelper.ExecuteNonQueryBatch(_list);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void UpdateList(List<Model.Model_SFC_Water> model)
        {
            string strSql;
            List<string> _list = new List<string>();
            for (int i = 0; i < model.Count; i++)
            {
                //BkLine,BkAB,BkNumber,Tray1;       //5,托盘1编号

                //Tray2,Tray3,SfcTest,BkTimeStart

                // BkTimeEnd,BkTime,TAlarm, KPaAlarm;    //13,BK真空报警

                //DataTime,CWCValue,CWCResult,DataFlag;    //17,上传数据库标志

                //MesTime,MesFlag;     //19,上传Mes标志

                strSql = string.Format("update SFC_Water set " +
                    //BkLine,BkAB,BkNumber,Tray1; 
                    " BkLine='{0}',BkAB='{1}'," +
                    " BkNumber='{2}',Tray1='{3}'," +

                    //Tray2,Tray3,SfcTest,BkTimeStart
                    " Tray2='{4}',Tray3='{5}'," +
                    " SfcTest='{6}',BkTimeStart='{7}'," +

                    // BkTimeEnd,BkTime,TAlarm, KPaAlarm; 
                    " BkTimeEnd='{8}',BkTime='{9}'," +
                    " TAlarm='{10}',KPaAlarm='{11}'," +

                    //DataTime,CWCValue,CWCResult,DataFlag; 
                    " DataTime='{12}',CWCValue='{13}'," +
                    " CWCResult='{14}',DataFlag='{15}'," +

                    //MesTime,MesFlag;
                    " Tray1='{16}' " +

                    " where Area='{17}' ",
                    //BkLine,BkAB,BkNumber,Tray1; 
                    model[i].BkLine, 
                    model[i].BkAB, 
                    model[i].BkNumber, 
                    model[i].Tray1,

                    //Tray2,Tray3,SfcTest,BkTimeStart
                    model[i].Tray2,
                    model[i].Tray3,
                    model[i].SfcTest,
                    model[i].BkTimeStart,

                    // BkTimeEnd,BkTime,TAlarm, KPaAlarm; 
                    model[i].BkTimeEnd,
                    model[i].BkTime,
                    model[i].TAlarm,
                    model[i].KPaAlarm,

                    //DataTime,CWCValue,CWCResult,DataFlag; 
                    model[i].DataTime,
                    model[i].CWCValue,
                    model[i].CWCResult,
                    model[i].DataFlag,

                    //MesTime,MesFlag;
                    model[i].MesTime,
                    model[i].MesFlag,
                    model[i].BkResource);
            _list.Add(strSql);
            }
            Helper.MySqlHelper.ExecuteNonQueryBatch(_list);
        }



        public DataTable GetListPage(string strWhere, int pageSize, int page)
        {
            DataTable ds = new DataTable();
            string strSql = "";

            //private int ID;             //1,ID
            //private string BkLine;      //2,拉线号
            //private string BkAB;        //3,A/B线
            //private string BkNumber;    //4,炉腔编号
            //private string Tray1;       //5,托盘1编号

            string str = "ID as 序列号,BkLine as 拉线号,BkAB as AB线,BkNumber as 炉腔编号,Tray1 as 托盘1编号," +

                //private string Tray2;       //6,托盘2编号
                //private string Tray3;       //7,托盘3编号
                //private string SfcTest;     //8,测试条码
                //private string BkTimeStart; //9,BK开始时间

                "Tray2 as 托盘2编号,Tray3 as 托盘3编号,SfcTest as 测试条码,BkTimeStart as BK开始时间," +

                //private string BkTimeEnd;   //10,BK结束时间   
                //private string BkTime;      //11,BK时长
                //private string TAlarm;      //12,BK温度报警
                //private string KPaAlarm;    //13,BK真空报警

                "BkTimeEnd as BK结束时间,BkTime as BK时长,TAlarm as BK温度报警,KPaAlarm as BK真空报警," +

                //private string DataTime;    //14,上传数据库时间
                //private string CWCValue;    //15,水含量值
                //private string CWCResult;   //16,水含量结果
                //private string DataFlag;    //17,上传数据库标志

                "DataTime as 上传数据库时间,CWCValue as 水含量值,CWCResult as 水含量结果,DataFlag as 上传数据库标志," +
                //private string MesTime;     //18,上传Mes时间
                //private string MesFlag;     //19,上传Mes标志

                "MesTime as 上传Mes时间,MesFlag as 上传Mes标志, BkResource as 设备资源号 ";
            //"ID 时间  类型代码 类型描述  数据项代码 数据项描述 PLC寄存器 区域""  BkResource
            //strSql = "select * from Alarm_oven where  order by ID limit " + pageSize + "  offset  " + pageSize * (page - 1);

            strSql = "select " + str + "  from SFC_Water " + strWhere + "  limit " + pageSize + "  offset  " + pageSize * (page - 1);
            ds = Helper.MySqlHelper.ExecuteDataTable(strSql, System.Data.CommandType.Text);
            return ds;
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM SFC_Water " + strWhere);
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
            string sql = "delete from SFC_Water  " + strWhere;
            return Helper.MySqlHelper.ExecuteNonQuery(sql, System.Data.CommandType.Text);
        }



















        //public DataTable GetListPage(string strWhere, int pageSize, int page)
        //{
        //    DataTable ds = new DataTable();
        //    string strSql = "";

        //    string str = "ID as 索引,Time_A as 时间,Type_Code as 类型代码,Type_Description as 类型描述,Data_code as 数据项代码,Data_Description as 数据项描述,Register as PLC寄存器 ,Area as 区域 ";
        //    "ID 时间  类型代码 类型描述  数据项代码 数据项描述 PLC寄存器 区域""
        //    strSql = "select * from SFC_Water where  order by ID limit " + pageSize + "  offset  " + pageSize * (page - 1);

        //    strSql = "select " + str + "  from SFC_Water " + strWhere + "  limit " + pageSize + "  offset  " + pageSize * (page - 1);
        //    ds = Helper.MySqlHelper.ExecuteDataTable(strSql, System.Data.CommandType.Text);
        //    return ds;
        //}

        /// <summary>
        /// 获取记录总数
        /// </summary>
        //public int GetRecordCount(string strWhere)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("select count(1) FROM SFC_Water " + strWhere);
        //    object obj = Helper.MySqlHelper.GetSingle(strSql.ToString());
        //    if (obj == null)
        //    {
        //        return 0;
        //    }
        //    else
        //    {
        //        return Convert.ToInt32(obj);
        //    }
        //}

        //删除
        //public int DeleteByContactId(string strWhere)
        //{
        //    string sql = "delete from SFC_Water  " + strWhere;
        //    return Helper.MySqlHelper.ExecuteNonQuery(sql, System.Data.CommandType.Text);
        //}
    }
}
