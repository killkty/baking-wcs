using GlobalsInfo;
using Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;

namespace Flow
{
    public class FlowKbOut
    {
        int tim_KbOut = 30;     //3秒
        public void _tim_KbOut()  // 
        {
            for (int i = 0; i < tim_KbOut; i++)
            {
                if (Omron.STOP == true)
                {
                    return;
                }
                Thread.Sleep(100);
            }
        }

        //32个炉腔 开始时间 断电
        public void ThreadProcFlow(object str)
        {
            do
            {
                OutCall(0);
                _tim_KbOut();
            }
            while (Omron.STOP == false);
        }

        bool[] KbOut_bool = new bool[4];
        public void OutCall(int K)
        {
            //炉腔结束烘烤
            //1，信号触发触发
            string conBool = "";
            if (OmronHelper._Read_bool(Omron.ABline, "D26303.00", ref conBool) == false)
            {
                GlobalsInfo.Message.Red("读PLC异常");
                _tim_KbOut();
                return;
            }
            if (conBool == "False")
            {
                KbOut_bool[K] = false;
                for (int i = 0; i < 3; i++)
                {
                    LoadRw.Keep_State._Out1[K, i] = "";
                }
                LoadRw.Keep_State._Keep_State();  //保存
                _tim_KbOut();
                return;
            }
            if (KbOut_bool[K] == true)
            {
                return;
            }
            GlobalsInfo.Message.Green("炉腔结束允许");


            //2，读炉腔代码
            short[] conShort = new short[3];
            if (OmronHelper._read_Batch2(Omron.ABline, "D26300", "3", ref conShort) == false)
            {
                GlobalsInfo.Message.Red("读PLC异常");
                KbOut_bool[K] = true;
                return;
            }
            if (conShort[0] == 0 || conShort[0] > 4)
            {
                GlobalsInfo.Message.Red("读炉腔首代码异常");
                KbOut_bool[K] = true;
                return;
            }
            if (conShort[1] == 0 || conShort[1] > 8)
            {
                GlobalsInfo.Message.Red("读炉腔次代码异常");
                KbOut_bool[K] = true;
                return;
            }
            if (conShort[2] == 0 || conShort[2] > 3)
            {
                GlobalsInfo.Message.Red("读托盘个数代码异常");
                KbOut_bool[K] = true;
                return;
            }
            //3，通过炉腔代码  找出炉腔条码 通过炉腔条码 找3个夹具条码
            string[] Tray = new string[3];

            string Oven = GlobalsInfo.BaKing.Oven[(conShort[0]-1), (conShort[1]-1)];
            GlobalsInfo.Message.Green("炉腔结束--炉腔条码"+ Oven);
            Tray[0] = GlobalsInfo.BaKing.Tray[(conShort[0]-1), (conShort[1]-1), 0];
            Tray[1] = GlobalsInfo.BaKing.Tray[(conShort[0]-1), (conShort[1]-1), 1];
            Tray[2] = GlobalsInfo.BaKing.Tray[(conShort[0]-1), (conShort[1]-1), 2];


            //DateTime dt_Start1 = Convert.ToDateTime("2019-1-10 9:10:10");
            //string str_Start1 = dt_Start1.ToString() + ":" + dt_Start1.Millisecond.ToString();

            //DateTime dt_Stop1 = Convert.ToDateTime("2019-1-12 11:00:00");
            //TimeSpan _ts1 = dt_Stop1 - dt_Start1;
            //string str_ts1 = _ts1.TotalMilliseconds.ToString();
            //double str_ts2 = _ts1.TotalHours;
            //if(str_ts2>24)
            //{


            //}


            //string str_ts3 = _ts1.TotalMilliseconds.ToString();
            //string str_Stop1 = dt_Stop1.ToString("yyyy/mm/dd HH:MM:ss") + ":" + dt_Stop1.Millisecond.ToString();

            DateTime dt_Start;
            //1,读出数据库 查询 BK开始时间   计算BK时长  炉腔编号
            BLL.BLL_TrayID_table cOper = new BLL.BLL_TrayID_table();
            DataTable da = cOper.GetList(Tray[0]);
            if (da.Rows.Count > 0)
            {
                dt_Start = Convert.ToDateTime(da.Rows[0]["BindTime"].ToString());
            }
            else
            {
                GlobalsInfo.Message.Red("读开始时间-异常");
                return;
            }
            DateTime dt_Stop = DateTime.Now;
            TimeSpan _ts = dt_Stop - dt_Start;    //读出开始时间
            string str_ts = _ts.TotalMilliseconds.ToString();

            string str_Start = dt_Start.ToString("u").Replace("T", " ").Replace("Z", "");
            string str_Stop = dt_Stop.ToString("u").Replace("T", " ").Replace("Z", "");

            //4，上传MES
            for (int i = 0; i < conShort[2]; i++)
            {
                GlobalsInfo.Message.Green("炉腔结束--托盘条码" + Tray[i]);
                if (GlobalsInfo.SelectLQ.MesOpen == "open")
                {
                    if (LoadRw.Keep_State._Out1[K, i] == "")
                    {
                        if (GlobalsInfo.Global_TrayData.MES == "打开")//托盘数据收集
                        {
                            string ProcessLot = Tray[i];
                            bool hotP_bool = true;
                            string[] hotP_ss = new string[3];
                            hotP_ss[0] = Oven;   //BK开始时间
                            hotP_ss[1] = str_Start;    //BK结束时间  炉区编号
                            hotP_ss[2] = "1.08";      //BK时长  Baking工序阴极极片水含量数据
                            //hotP_ss[3] = Oven;        //炉子编号  炉号炉腔
                            //hotP_ss[4] = str_ts;        //时长



                            //BKOVENNO      炉区编号
                            //BKSTARTTIME   baking开始时间
                            //BKTIME  时长


                            string status = "";
                            string message_web = "";
                            WebMesH.WebHelper.WebTray_data(ProcessLot, ref status, ref message_web, hotP_bool, hotP_ss);  //托盘数据收集

                            if (status == "0")
                            {
                                LoadRw.Keep_State._Out1[K, i] = "OK";
                                LoadRw.Keep_State._Keep_State();  //保存
                            }
                            else
                            {
                                LoadRw.Keep_State._Out1[K, i] = "NG";
                                LoadRw.Keep_State._Keep_State();  //保存
                            }
                        }
                        else
                        {
                            LoadRw.Keep_State._Out1[K, i] = "OK";
                            LoadRw.Keep_State._Keep_State();  //保存
                        }
                    }
                }
                else
                {
                    LoadRw.Keep_State._Out1[K, i] = "OK";
                    LoadRw.Keep_State._Keep_State();  //保存
                }
            }

            bool bool_NG = false;
            if (conShort[2] == 1)
            {
                 if (LoadRw.Keep_State._Out1[K, 0] == "NG")
                {
                    bool_NG = true;
                }
            }
            if (conShort[2] == 2)
            {
                 if (LoadRw.Keep_State._Out1[K, 0] == "NG" || LoadRw.Keep_State._Out1[K, 1] == "NG" )
                {
                    bool_NG = true;
                }
            }
            if (conShort[2] == 3)
            {
                 if (LoadRw.Keep_State._Out1[K, 0] == "NG"  || LoadRw.Keep_State._Out1[K, 1] == "NG" || LoadRw.Keep_State._Out1[K, 2] == "NG" )
                {
                    bool_NG = true;
                }
            }
            if (bool_NG == true)
            {
                SendNG();
            }
            else
            {
                SendOK();
            }

            //2,先查询有没有该测试条码 通过托盘条码 查询水含量条码 查询3个托盘的条码  将电芯条码和测试条码 搭配写入数据库 便于查询
            List<string> Sfc = new List<string>();
            string SfcTest = "";
            Sfc.Clear();
            if (Sfc_list(Tray, ref Sfc, ref SfcTest) == false)
            {
                return;
            }
            Sfc_Down(Sfc, SfcTest);

            //3,将托盘绑定修改为绑定false
            KbOut_bool[K] = true;
            //5，写入数据库
            //6，写入csv 文件 
        }
        public void Sfc_Down(List<string> Sfc, string SfcTest)
        {
            BLL.BLL_sfc_down_table cOper_down = new BLL.BLL_sfc_down_table();
            List<Model.Model_sfc_down_table> list = new List<Model.Model_sfc_down_table>();
            Model.Model_sfc_down_table mOper = new Model.Model_sfc_down_table();
            list.Clear();
            DataTable da = cOper_down.GetList(SfcTest);
            if (da.Rows.Count == 0)
            {
                for (int i = 0; i < Sfc.Count; i++)
                {
                    if (Sfc[i] != "")
                    {
                        mOper.Sfc = Sfc[i];           //sfc电芯条码
                        mOper.SfcTest = SfcTest;      //测试电芯条码
                        list.Add(mOper);
                    }
                }
                cOper_down.AddList(list);
            }

        }
        public bool Sfc_list(string[] Tray, ref List<string> Sfc, ref string SfcTest)
        {
            BLL.BLL_sfc_table cOper = new BLL.BLL_sfc_table();
            string Position = "0";
            DataTable da = cOper.GetPosition(Tray[0], Position);
            if (da.Rows.Count > 0)
            {
                SfcTest = da.Rows[0]["Sfc"].ToString();
            }
            else
            {
                GlobalsInfo.Message.Red("读测试电芯-异常");
                return false;
            }

            da = cOper.GetList(Tray[0]);
            if (da.Rows.Count > 0)
            {
                for (int i = 0; i < da.Rows.Count; i++)
                {
                    if (da.Rows[i]["Sfc"].ToString() != "")
                    {
                        Sfc.Add(da.Rows[i]["Sfc"].ToString());
                    }
                }
            }
            da = cOper.GetList(Tray[1]);
            if (da.Rows.Count > 0)
            {
                for (int i = 0; i < da.Rows.Count; i++)
                {
                    if (da.Rows[i]["Sfc"].ToString() != "")
                    {
                        Sfc.Add(da.Rows[i]["Sfc"].ToString());
                    }

                }
            }
            da = cOper.GetList(Tray[2]);
            if (da.Rows.Count > 0)
            {
                for (int i = 0; i < da.Rows.Count; i++)
                {
                    if (da.Rows[i]["Sfc"].ToString() != "")
                    {
                        Sfc.Add(da.Rows[i]["Sfc"].ToString());
                    }
                }
            }
            return true;

        }



        public void SendOK()
        {
            //发送 PLC  OK
            OmronHelper.writeResult_bool(Omron.ABline, "D26303.01", "true");
            OmronHelper.writeResult_bool(Omron.ABline, "D26303.02", "true");
            GlobalsInfo.Message.Green("炉腔结束--输出OK");

        }
        public void SendNG()
        {
            //发送 plc  NG
            OmronHelper.writeResult_bool(Omron.ABline, "D26303.01", "false");
            OmronHelper.writeResult_bool(Omron.ABline, "D26303.02", "true");
            GlobalsInfo.Message.Red("炉腔结束--输出NG");
        }


    }
}
