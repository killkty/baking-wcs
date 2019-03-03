using GlobalsInfo;
using Helper;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Flow
{
    public class FlowSfc
    {
        int tim_Sfc = 30;     //3秒
        public void _tim_Sfc()  // 
        {
            for (int i = 0; i < tim_Sfc; i++)
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
                SfcCall(0);
                _tim_Sfc();
            }
            while (Omron.STOP == false);
        }


        bool[] Sfc_bool = new bool[4];
        public void SfcCall(int K)
        {
            //电池绑定夹具
            //0读 信号触发触发 % D22069.00  bool
            string conBool = "";
            if (OmronHelper._Read_bool(Omron.ABline, "D23087.00", ref conBool) == false)
            {
                GlobalsInfo.Message.Red("读PLC异常");
                _tim_Sfc();
                return;
            }
            if (conBool == "False")
            {
                Sfc_bool[K] = false;
                for (int i = 0; i < 96; i++)
                {
                    LoadRw.Keep_State._Sfc[K, i] = "";
                }
                LoadRw.Keep_State._Keep_State();  //保存
                _tim_Sfc();
                return;
            }
            if (Sfc_bool[K] == true)
            {
                return;
            }
            GlobalsInfo.Message.Green("电池绑定托盘允许");

            //1读 炉腔代码赋值 % D20010 -% D20011 int
            //2读 夹具代码赋值 % D20012 int
            //3读 夹具排数赋值 % D20013 int
            //4读 电池数组有效数量赋值 % D20014 int

            short[] conShort = new short[5];
            if (OmronHelper._read_Batch2(Omron.ABline, "D20010", "5", ref conShort) == false)
            {
                GlobalsInfo.Message.Red("读PLC异常");
                Sfc_bool[K] = true;
                return;
            }
            if (conShort[0] == 0 || conShort[0] > 4)
            {
                GlobalsInfo.Message.Red("读炉腔首代码异常");
                Sfc_bool[K] = true;
                return;
            }
            if (conShort[1] == 0 || conShort[1] > 8)
            {
                GlobalsInfo.Message.Red("读炉腔次代码异常");
                Sfc_bool[K] = true;
                return;
            }
            if (conShort[2] == 0 || conShort[2] > 3)
            {
                GlobalsInfo.Message.Red("读托盘代码异常");
                Sfc_bool[K] = true;
                return;
            }
            //if (conShort[3] == 0 || conShort[3] > 10)
            //{
            //    GlobalsInfo.Message.Red("读夹具排数代码异常");
            //    return;
            //}
            if (conShort[4] == 0 || conShort[4] > 96)
            {
                GlobalsInfo.Message.Red("读电池数组代码异常");
                Sfc_bool[K] = true;
                return;
            }

            string Oven = GlobalsInfo.BaKing.Oven[(conShort[0]-1), (conShort[1]-1)];
            string Tray = GlobalsInfo.BaKing.Tray[(conShort[0]-1), (conShort[1]-1), (conShort[2]-1)];

            //5读 电池二维码数组赋值 % D20020 -% D22068 ARRAY[0..31] OF string[64]
            string conString = "";
            string receiveMsg = "";
            string[] SFC = new string[96];
            int M = 0;
            for (int i = 0; i < 96; i++)
            {
                SFC[i] = "";
            }
            for (int i = 0; i < conShort[4]; i++)
            {
                M = i * 32;
                OmronHelper._read_string(Omron.ABline, "D" + (20015 + M), "32", ref receiveMsg);
                conString = OmronHelper.char_string(receiveMsg);
                GlobalsInfo.Message.Green("电池绑定托盘--电芯编号:"+(i+1)+ "--电芯条码:" + conString);
                SFC[i] = conString;
            }

            //第一个夹具时将 数据库托盘条码清空

            List<Model.Model_sfc_table> list1 = new List<Model.Model_sfc_table>();
            list1.Clear();
            //if (conShort[2] == 1)
            //{
                for (int i = 0; i < 96; i++)
                {
                    DateTime dt_Start = DateTime.Now;
                    string str_Start = dt_Start.ToString("u").Replace("T", " ").Replace("Z", "");
                    Model.Model_sfc_table mOper1 = new Model.Model_sfc_table();
                    mOper1.Sfc = "";                         //sfc电芯条码
                    mOper1.TrayID = Tray;                    //托盘条码
                    mOper1.Position =i.ToString();       //绑定位置
                    list1.Add(mOper1);
                }
                BLL.BLL_sfc_table bOper1 = new BLL.BLL_sfc_table();
                bOper1.UpdateList(list1);
            //}




            int position = 0;
            //7 通过炉腔代码 + 夹具代码 找出炉腔代码  和夹具代码  通过夹具排数 计算上传的电芯位置
            //int pos = 3 * conShort[4];

            List<Model.Model_sfc_table> list = new List<Model.Model_sfc_table>();
            list.Clear();
            //Tray = "";
            for (int i = 0; i < conShort[4]; i++)
            {
                //if (i > 0)
                //{
                //    position =   //((conShort[3] - 1) * conShort[4]) + i;
                //}
                //else
                //{
                position = i;
                //}

                DateTime dt_Start = DateTime.Now;
                string str_Start = dt_Start.ToString("u").Replace("T", " ").Replace("Z", "");

                Model.Model_sfc_table mOper = new Model.Model_sfc_table();
                mOper.Sfc = SFC[i];                    //sfc电芯条码
                mOper.TrayID = Tray;                   //托盘条码
                mOper.Position = position.ToString();  //绑定位置
                list.Add(mOper);
            }

            BLL.BLL_sfc_table bOper = new BLL.BLL_sfc_table();
            bOper.UpdateList(list);


            for (int i = 0; i < conShort[4]; i++) //32
            {
                if (LoadRw.Keep_State._Sfc[K, i] == "")
                {
                    //8读取及上传MES
                    if (GlobalsInfo.SelectLQ.MesOpen == "open")
                    {
                        if (GlobalsInfo.Global_SFCTray.MES == "打开" && SFC[i] != "")
                        {
                            if (SFC[i] == "")
                            {
                                GlobalsInfo.Message.Green("电池绑定托盘--空电芯编号:" + (i+1));
                            }

                            string status = "";
                            string message_web = "";
                            //if (i > 0)
                            //{
                            //    position = ((conShort[3] - 1) * conShort[4]) + i;
                            //}
                            //else
                            //{
                            position = i;
                            //}
                            WebMesH.WebHelper.WebSFCTray(Tray, SFC[i], ref status, ref message_web, position);
                            if (status == "0")
                            {
                                LoadRw.Keep_State._Sfc[K, i] = "OK";
                                LoadRw.Keep_State._Keep_State();  //保存
                            }
                            else
                            {
                                LoadRw.Keep_State._Sfc[K, i] = "NG";
                                LoadRw.Keep_State._Keep_State();  //保存
                            }
                        }
                        else
                        {
                            LoadRw.Keep_State._Sfc[K, i] = "OK";
                            LoadRw.Keep_State._Keep_State();  //保存
                        }
                    }
                    else
                    {
                        LoadRw.Keep_State._Sfc[K, i] = "OK";
                        LoadRw.Keep_State._Keep_State();  //保存
                    }
                }
            }
            bool conBool_plc = false;

            for (int i = 0; i < conShort[4]; i++) //32
            {

                if (LoadRw.Keep_State._Sfc[K, i] == "NG")
                {
                    GlobalsInfo.Message.Red("电池绑定托盘--NG电芯编号:" + (i + 1)+ "--NG电芯条码:" + SFC[i]);
                    conBool_plc = true;
                }
            }

            if (conBool_plc == false)
            {
                SendOK();
            }
            else
            {
                SendNG();
            }
            Sfc_bool[K] = true;
        }




        public void SendOK()
        {
            OmronHelper.writeResult_bool(Omron.ABline, "D23087.01", "true");
            OmronHelper.writeResult_bool(Omron.ABline, "D23087.02", "true");
            GlobalsInfo.Message.Green("电池绑定托盘--输出OK");
        }
        public void SendNG()
        {
            OmronHelper.writeResult_bool(Omron.ABline, "D23087.01", "false");
            OmronHelper.writeResult_bool(Omron.ABline, "D23087.02", "true");
            GlobalsInfo.Message.Red("电池绑定托盘--输出NG");
        }






    }
}
