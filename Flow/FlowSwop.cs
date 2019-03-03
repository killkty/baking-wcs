using GlobalsInfo;
using Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;

namespace Flow
{
    public class FlowSwop
    {
        int tim_Swop = 20;     //3秒
        public void _tim_Swop()  // 
        {
            for (int i = 0; i < tim_Swop; i++)
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
                SwopCall(0);
                _tim_Swop();
            }
            while (Omron.STOP == false);

        }
        bool[] Swop_bool = new bool[4];
        public void SwopCall(int K)
        {
            //0是否已绑定过夹具，是即解绑夹具 信号触发触发  % D22108.00  bool
            string conBool = "";
            if (OmronHelper._Read_bool(Omron.ABline, "D26406.00", ref conBool) == false)
            {
                GlobalsInfo.Message.Red("读PLC异常");
                _tim_Swop();
                return;
            }
            if (conBool == "False")
            {
                Swop_bool[K] = false;
                LoadRw.Keep_State.Swop[K] = "";
                LoadRw.Keep_State._Keep_State();  //保存
                _tim_Swop();
                return;
            }
            if (Swop_bool[K] == true)
            {
                return;
            }

            GlobalsInfo.Message.Green("交换炉区允许");

            //交换炉区
            // 1, 旧炉腔代码赋值 % D22100 -% D22101 int
            // 2, 旧夹具代码赋值 % D22102 int
            // 3, 旧夹具排数赋值 % D22103 int
            // 4, 新炉腔代码赋值 % D22104 -% D22105 int
            //   新夹具代码赋值 % D22106 int
            //   新夹具排数赋值 % D22107 int

            short[] conShort1 = new short[8];
            if (OmronHelper._read_Batch2(Omron.ABline, "D26400", "3", ref conShort1) == false)
            {
                GlobalsInfo.Message.Red("读PLC异常");
                return;
            }
            if (conShort1[0] == 0 || conShort1[0] > 4)
            {
                GlobalsInfo.Message.Red("读炉腔首代码异常");
                return;
            }
            if (conShort1[1] == 0 || conShort1[1] > 8)
            {
                GlobalsInfo.Message.Red("读炉腔次代码异常");
                return;
            }
            if (conShort1[2] == 0 || conShort1[2] > 3)
            {
                GlobalsInfo.Message.Red("读夹具代码异常");
                return;
            }

            //旧托盘
            string Oven1 = GlobalsInfo.BaKing.Oven[(conShort1[0]-1), (conShort1[1]-1)];
            string Tray1 = GlobalsInfo.BaKing.Tray[(conShort1[0]-1), (conShort1[1]-1), (conShort1[2]-1)];


            GlobalsInfo.Message.Green("交换炉区--旧托盘-"+ Tray1);

            short[] conShort2 = new short[8];
            if (OmronHelper._read_Batch2(Omron.ABline, "D26403", "3", ref conShort2) == false)
            {
                GlobalsInfo.Message.Red("读PLC异常");
                return;
            }
            if (conShort2[0] == 0 || conShort2[0] > 4)
            {
                GlobalsInfo.Message.Red("读炉腔首代码异常");
                return;
            }
            if (conShort2[1] == 0 || conShort2[1] > 8)
            {
                GlobalsInfo.Message.Red("读炉腔次代码异常");
                return;
            }
            if (conShort2[2] == 0 || conShort2[2] > 3)
            {
                GlobalsInfo.Message.Red("读夹具代码异常");
                return;
            }
            //新托盘
            string Oven2 = GlobalsInfo.BaKing.Oven[(conShort2[0]-1), (conShort2[1]-1)];
            string Tray2 = GlobalsInfo.BaKing.Tray[(conShort2[0]-1), (conShort2[1]-1), (conShort2[2]-1)];

            GlobalsInfo.Message.Green("交换炉区--新托盘-" + Tray2);

            // 否则不需解绑夹具                    读取旧炉腔代码     int
            //                     读取旧夹具代码     int
            //                     读取旧夹具排数     int
            //                     读取新炉腔代码     int
            //                     读取新夹具代码     int
            //                     读取新夹具排数     int
            //                     是否已绑定过夹具，是即解绑夹具
            //                     否则不需解绑夹具

            //1，读出旧托盘的电芯条码 
            List<string> Sfc = new List<string>();
            Sfc.Clear();
            BLL.BLL_sfc_table cOper = new BLL.BLL_sfc_table();

            //如果有间隔 
            DataTable da = cOper.GetList(Tray1);
            if (da.Rows.Count > 0)
            {
                for (int i = 0; i < da.Rows.Count; i++)
                {
                    Sfc.Add(da.Rows[i]["Sfc"].ToString());
                }
            }
            //放入新托盘中
            List<Model.Model_sfc_table> list1 = new List<Model.Model_sfc_table>();
            list1.Clear();
            for (int i = 0; i < 96; i++)
            {
                Model.Model_sfc_table mOper1 = new Model.Model_sfc_table();
                mOper1.Sfc = Sfc[i];                            //sfc电芯条码
                mOper1.TrayID = Tray2;                      //托盘条码
                mOper1.Position = (i+1).ToString();                       //绑定位置
                list1.Add(mOper1);
            }
            BLL.BLL_sfc_table bOper1 = new BLL.BLL_sfc_table();
            bOper1.UpdateList(list1);

            //2，将旧托盘的电芯条码清空
            List<Model.Model_sfc_table> list = new List<Model.Model_sfc_table>();
            list.Clear();
            for (int i = 0; i < 96; i++)
            {
                Model.Model_sfc_table mOper = new Model.Model_sfc_table();
                mOper.Sfc = "";                         //sfc电芯条码
                mOper.TrayID = Tray1;                   //托盘条码
                mOper.Position = (i + 1).ToString();    //绑定位置
                list.Add(mOper);
            }

            BLL.BLL_sfc_table bOper = new BLL.BLL_sfc_table();
            bOper.UpdateList(list);

            if (LoadRw.Keep_State.Swop[K] == "OK" || LoadRw.Keep_State.Swop[K] == "NG")
            {
                if (LoadRw.Keep_State.Swop[K] == "OK")
                {
                    //发送 PLC  OK
                    SendOK();
                    Swop_bool[K] = true;
                }
                else
                {
                    //发送 PLC  NG
                    SendNG();
                    Swop_bool[K] = true;
                }
                return;
            }

            //                   读取及上传MES
            if (GlobalsInfo.SelectLQ.MesOpen == "open")
            {
                if (GlobalsInfo.Global_Tray.MES == "打开")
                {
                    string processLot = Tray1;
                    string status = "";
                    string message_web = "";
                    WebMesH.WebHelper.WebTray_Verify(processLot, ref status, ref message_web);  //托盘校验

                    if (status == "0")
                    {
                        LoadRw.Keep_State.Swop[K] = "OK";
                        LoadRw.Keep_State._Keep_State();  //保存
                    }
                    else
                    {
                        LoadRw.Keep_State.Swop[K] = "NG";
                        LoadRw.Keep_State._Keep_State();  //保存
                    }
                }
                else
                {
                    LoadRw.Keep_State.Swop[K] = "OK";
                    LoadRw.Keep_State._Keep_State();  //保存
                }
            }
            else
            {
                LoadRw.Keep_State.Swop[K] = "OK";
                LoadRw.Keep_State._Keep_State();  //保存
            }

            if (LoadRw.Keep_State.Swop[K] == "OK")
            {
                //发送 PLC  OK
                SendOK();
            }
            else
            {
                //6 % D20003.01  返回结果 
                //7 % D20003.02  返回处理完成结果  
                //发送 plc  NG
                SendNG();
            }
            Swop_bool[K] = true;
        }


        public void SendOK()
        {
            //发送 PLC  OK
            OmronHelper.writeResult_bool(Omron.ABline, "D26406.01", "true");
            OmronHelper.writeResult_bool(Omron.ABline, "D26406.02", "true");
            GlobalsInfo.Message.Green("交换炉区-- 输出OK");

        }
        public void SendNG()
        {
            //发送 plc  NG
            OmronHelper.writeResult_bool(Omron.ABline, "D26406.01", "false");
            OmronHelper.writeResult_bool(Omron.ABline, "D26406.02", "true");
            GlobalsInfo.Message.Green("交换炉区-- 输出NG");
        }





    }
}
