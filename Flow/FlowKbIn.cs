using GlobalsInfo;
using Helper;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Flow
{
    public class FlowKbIn
    {

        int tim_KbIn = 30;        //3秒
        public void _tim_KbIn()   // 
        {
            for (int i = 0; i < tim_KbIn; i++)
            {
                if (Omron.STOP == true)
                {
                    return;
                }
                Thread.Sleep(100);
            }
        }

        public void ThreadProcFlow(object str)
        {
            do
            {
                InCall(0);
                _tim_KbIn();
            }
            while (Omron.STOP == false);
        }

        bool[] KbIn_bool = new bool[4];
        public void InCall(int K)
        {
            //炉腔开始烘烤
            DateTime dt_Start = DateTime.Now;
            string str_Start = dt_Start.ToString("u").Replace("T", " ").Replace("Z", "");


            //DateTime dt_Start = DateTime.Now;
            //string str_Start = dt_Start.ToString("u").Replace("T", " ").Replace("Z", "") + ":" + dt_Start.Millisecond.ToString();
            //LoadRw.Keep_State._Sfc[0, 0] = "OK";
            //LoadRw.Keep_State._Keep_State();  //保存
            //DateTime dt_Stop = DateTime.Now;
            //TimeSpan _ts = dt_Stop - dt_Start;
            //string str_ts = _ts.TotalMilliseconds.ToString();

            //string str_Stop = dt_Stop.ToString("u").Replace("T", " ").Replace("Z", "") + ":" + dt_Stop.Millisecond.ToString();


            //1，信号触发触发     % D22082.00        bool
            string conBool = "";
            if (_Read_bool(Omron.ABline, "D26203.00", ref conBool) == false)
            {
                GlobalsInfo.Message.Red("读PLC异常");
                return;
            }
            if (conBool == "False")
            {
                KbIn_bool[K] = false;
                for (int i = 0; i < 3; i++)
                {
                    LoadRw.Keep_State._In[K, i] = "";
                }
                LoadRw.Keep_State._Keep_State();  //保存
                return;
            }
            if (KbIn_bool[K] == true)
            {
                return;
            }
            GlobalsInfo.Message.Green("炉腔开始允许");

            //2，读 炉腔代码赋值  % D22080 -% D22081 int
            short[] conShort = new short[3];
            if (OmronHelper._read_Batch2(Omron.ABline, "D26200", "3", ref conShort) == false)
            {
                GlobalsInfo.Message.Red("读PLC异常");
                KbIn_bool[K] = true;
                return;
            }
            if (conShort[0] == 0 || conShort[0] > 4)
            {
                GlobalsInfo.Message.Red("读炉腔首代码异常");
                KbIn_bool[K] = true;
                return;
            }
            if (conShort[1] == 0 || conShort[1] > 8)
            {
                GlobalsInfo.Message.Red("读炉腔次代码异常");
                KbIn_bool[K] = true;
                return;
            }
            if (conShort[2] == 0 || conShort[2] > 3)
            {
                GlobalsInfo.Message.Red("读托盘个数异常");
                KbIn_bool[K] = true;
                return;
            }

            //3，通过炉腔代码  找出炉腔条码 通过炉腔条码 找3个夹具条码
            string Oven = GlobalsInfo.BaKing.Oven[(conShort[0]-1), (conShort[1]-1)];
            GlobalsInfo.Message.Green("炉腔开始--炉腔条码" + Oven);
            string[] Tray =new string[3];
            Tray[0] = GlobalsInfo.BaKing.Tray[(conShort[0]-1), (conShort[1]-1), 0];
            Tray[1] = GlobalsInfo.BaKing.Tray[(conShort[0]-1), (conShort[1]-1), 1];
            Tray[2] = GlobalsInfo.BaKing.Tray[(conShort[0]-1), (conShort[1]-1), 2];


            //写入数据库
            List<Model.Model_TrayID_table> list = new List<Model.Model_TrayID_table>();
            for (int i = 0; i < conShort[2]; i++)
            {
                Model.Model_TrayID_table mOper = new Model.Model_TrayID_table();
                mOper.TrayID = Tray[i];                      //托盘条码
                mOper.BKBarcode = Oven;                      //炉子编号
                mOper.BKArea = (i+1).ToString();             //炉腔
                mOper.BindTime = str_Start;                  //绑定时间
                mOper.BKPeriod = "";                         //BK时长(工艺为准)
                mOper.BK_Tray_OR = "true";                   //检查烤箱有没有绑定
                list.Add(mOper);
            }
            BLL.BLL_TrayID_table bOper = new BLL.BLL_TrayID_table();
            bOper.UpdateList(list);


            for (int i = 0; i < conShort[2]; i++)
            {
                GlobalsInfo.Message.Green("炉腔开始--托盘条码" + Tray[i]);
                if (LoadRw.Keep_State._In[K, i] == "")
                {
                    //4，上传MES
                    if (GlobalsInfo.SelectLQ.MesOpen == "open")
                    {
                        if (GlobalsInfo.Global_Tray.MES == "打开")
                        {
                            string processLotArray = Tray[i];
                            string status = "";
                            string message_web = "";
                            WebMesH.WebHelper.WebTray_Start(processLotArray, ref status, ref message_web);  //托盘进站
                            if (status == "0")
                            {
                                LoadRw.Keep_State._In[K, i] = "OK";
                                LoadRw.Keep_State._Keep_State();  //保存
                            }
                            else
                            {
                                LoadRw.Keep_State._In[K, i] = "NG";
                                LoadRw.Keep_State._Keep_State();  //保存
                            }
                        }
                        else
                        {
                            LoadRw.Keep_State._In[K, i] = "OK";
                            LoadRw.Keep_State._Keep_State();  //保存
                        }
                    }
                    else
                    {
                        LoadRw.Keep_State._In[K, i] = "OK";
                        LoadRw.Keep_State._Keep_State();  //保存
                    }

                }
            }

            bool bool_NG = false;
            for (int i = 0; i < conShort[2]; i++)
            {
                if (LoadRw.Keep_State._In[K, i] == "NG")
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

            KbIn_bool[K] = true;

            //5，写入数据库

            //5， % D22082.01          返回结果                bool
            //6， % D22082.02          返回处理完成结果        bool
            //OmronHelper.writeResult_bool(Omron.ABline, "D22082.02", "false");
            //OmronHelper.writeResult_bool(Omron.ABline, "D22082.02", "true");



        }

        public void SendOK()
        {
            //发送 PLC  OK
            OmronHelper.writeResult_bool(Omron.ABline, "D26203.01", "true");
            OmronHelper.writeResult_bool(Omron.ABline, "D26203.02", "true");
            GlobalsInfo.Message.Green("炉腔开始--输出OK");
        }
        public void SendNG()
        {
            //发送 plc  NG
            OmronHelper.writeResult_bool(Omron.ABline, "D26203.01", "false");
            OmronHelper.writeResult_bool(Omron.ABline, "D26203.02", "true");
            GlobalsInfo.Message.Red("炉腔开始--输出NG");
        }



        ////修改托盘数据 和 烤箱数据 开始时间 托盘绑定标志
        //private void insertion_TrayID_table()
        //{
        //    List<Model.Model_TrayID_table> list = new List<Model.Model_TrayID_table>();
        //    //int n = 0;
        //    string Tray = "";
        //    string Oven = "";
        //    for (int f = 0; f < 4; f++)
        //    {
        //        for (int j = 0; j < 8; j++)
        //        {
        //            for (int i = 0; i < 3; i++)
        //            {
        //                Tray = GlobalsInfo.BaKing.Tray[f, j, i];
        //                Oven = GlobalsInfo.BaKing.Oven[f, j];
        //                Model.Model_TrayID_table mOper = new Model.Model_TrayID_table();
        //                mOper.TrayID = Tray;                  //托盘条码
        //                mOper.BKBarcode = Oven;               //炉子编号
        //                mOper.BKArea = (i + 1).ToString();    //炉腔
        //                mOper.BindTime = "";                  //绑定时间
        //                mOper.BKPeriod = "";                  //BK时长(工艺为准)
        //                mOper.BK_Tray_OR = "";                //检查烤箱有没有绑定
        //                list.Add(mOper);
        //            }
        //        }
        //    }



        //    BLL.BLL_TrayID_table bOper = new BLL.BLL_TrayID_table();
        //    bOper.AddList(list);
        //}






    }
}
