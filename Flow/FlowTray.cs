using GlobalsInfo;
using Helper;
using System.Threading;

namespace Flow
{
    public class FlowTray
    {
        int tim_Tray = 30;     //3秒
        public void _tim_Tray()  // 
        {
            for (int i = 0; i < tim_Tray; i++)
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
                TrayCall(0);
                _tim_Tray();
            }
            while (Omron.STOP == false);
        }

        bool[] Tray_bool = new bool[4];
        public void TrayCall(int K)
        {
            //夹具校验1
            //0读 信号触发触发 % D20003.00  bool
            string conBool = "";
            if (OmronHelper._Read_bool(Omron.ABline, "D20003.00", ref conBool) == false)
            {
                GlobalsInfo.Message.Red("读PLC异常");
                _tim_Tray();
                return;
            }
            if (conBool == "False")
            {
                Tray_bool[K] = false;
                LoadRw.Keep_State.Tray[K] = "";
                LoadRw.Keep_State._Keep_State();  //保存
                _tim_Tray();
                return;
            }
            if (Tray_bool[K] == true)
            {
                return;
            }
            GlobalsInfo.Message.Green("托盘校验允许");


            //1读 炉腔代码赋值 % D20000 -% D20001 int
            //2读 夹具代码赋值 % D20002 int
            short[] conShort = new short[3];
            if (OmronHelper._read_Batch2(Omron.ABline, "D20000", "3", ref conShort) == false)
            {
                GlobalsInfo.Message.Red("读PLC异常");
                Tray_bool[K] = true;
                return;
            }
            if (conShort[0] == 0 || conShort[0] > 4)
            {
                GlobalsInfo.Message.Red("读炉腔首代码异常");
                Tray_bool[K] = true;
                return;
            }
            if (conShort[1] == 0 || conShort[1] > 8)
            {
                GlobalsInfo.Message.Red("读炉腔次代码异常");
                Tray_bool[K] = true;
                return;
            }
            if (conShort[2] == 0 || conShort[2] > 3)
            {
                GlobalsInfo.Message.Red("读夹具代码异常");
                Tray_bool[K] = true;
                return;
            }

            //3 通过炉腔代码 + 夹具代码 找出炉腔代码  和夹具代码
            string Oven = GlobalsInfo.BaKing.Oven[(conShort[0]-1), (conShort[1]-1)];
            string Tray = GlobalsInfo.BaKing.Tray[(conShort[0]-1), (conShort[1]-1), (conShort[2]-1)];
            GlobalsInfo.Message.Green("托盘校验--托盘条码" + Tray);

            if (LoadRw.Keep_State.Tray[K] == "OK" || LoadRw.Keep_State.Tray[K] == "NG")
            {
                if (LoadRw.Keep_State.Tray[K] == "OK")
                {
                    //发送 PLC  OK
                    SendOK();
                    Tray_bool[K] = true;
                }
                else
                {
                    //发送 PLC  NG
                    SendNG();
                    Tray_bool[K] = true;
                }
                return;
            }

            //5 读取及上传MES
            if (GlobalsInfo.SelectLQ.MesOpen == "open")
            {
                if (GlobalsInfo.Global_Tray.MES == "打开")
                {
                    string processLot = Tray;
                    string status = "";
                    string message_web = "";
                    WebMesH.WebHelper.WebTray_Verify(processLot, ref status, ref message_web);  //托盘校验
                    if (status == "0")
                    {
                        LoadRw.Keep_State.Tray[K] = "OK";
                        LoadRw.Keep_State._Keep_State();  //保存
                    }
                    else
                    {
                        LoadRw.Keep_State.Tray[K] = "NG";
                        LoadRw.Keep_State._Keep_State();  //保存
                    }
                }
                else
                {
                    LoadRw.Keep_State.Tray[K] = "OK";
                    LoadRw.Keep_State._Keep_State();  //保存
                }
            }
            else
            {
                LoadRw.Keep_State.Tray[K] = "OK";
                LoadRw.Keep_State._Keep_State();  //保存
            }

            if (LoadRw.Keep_State.Tray[K] == "OK")
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
            Tray_bool[K] = true;

        }

        public void SendOK()
        {
            //发送 PLC  OK
            OmronHelper.writeResult_bool(Omron.ABline, "D20003.01", "true");
            OmronHelper.writeResult_bool(Omron.ABline, "D20003.02", "true");
            GlobalsInfo.Message.Green("托盘校验--输出OK");
        }
        public void SendNG()
        {
            //发送 plc  NG
            OmronHelper.writeResult_bool(Omron.ABline, "D20003.01", "false");
            OmronHelper.writeResult_bool(Omron.ABline, "D20003.02", "true");
            GlobalsInfo.Message.Red("托盘校验--输出NG");
        }




    }
}
