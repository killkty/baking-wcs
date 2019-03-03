using GlobalsInfo;
using Helper;
using Microsoft.VisualBasic;
using System.Threading;

namespace Flow
{
    public class FlowSfcVerify
    {
        int tim_SfcVerify = 8;     //3秒
        public void _tim_SfcVerify()  // 
        {
            for (int i = 0; i < tim_SfcVerify; i++)
            {
                if (Omron.STOP == true)
                {
                    return;
                }
                Thread.Sleep(100);
            }
        }

        public void ThreadProcFlowA(object str)
        {
            do
            {
                SfcVerifyACall(0);
                _tim_SfcVerify();
            }
            while (Omron.STOP == false);
        }

        bool[] SfcVerify_bool = new bool[3];
        public void SfcVerifyACall(int K)
        {
            //条码枪A
            //1，1#信号触发触发	%D23000.00	bool	
            string conBool = "";
            if (OmronHelper._Read_bool(Omron.ABline, "D27000.00", ref conBool) == false)
            {
                GlobalsInfo.Message.Red("读A条码PLC异常");
                _tim_SfcVerify();
                return;
            }
            if (conBool == "False")
            {
                SfcVerify_bool[K] = false;
                LoadRw.Keep_State.SfcVerify[K] = "";
                LoadRw.Keep_State._Keep_State();  //保存
                _tim_SfcVerify();
                return;
            }
            if (SfcVerify_bool[K] == true)
            {
                return;
            }


            GlobalsInfo.Message.Green("读A条码允许");


            if (LoadRw.Keep_State.SfcVerify[K] == "OK" || LoadRw.Keep_State.SfcVerify[K] == "NG")
            {
                if (LoadRw.Keep_State.SfcVerify[K] == "OK")
                {
                    //发送 PLC  OK
                    SfcAOK();
                    SfcVerify_bool[K] = true;
                }
                else
                {
                    //发送 PLC  NG
                    SfcANG();
                    SfcVerify_bool[K] = true;
                }
                return;
            }

            //2，扫码枪扫码
            string Msg = "";
            OmronHelper._read_string(Omron.ABline, "D27001", "64", ref Msg);
            string receiveMsg = OmronHelper.char_string(Msg);
            GlobalsInfo.Message.Green("读A条码--"+ receiveMsg);
            if (receiveMsg == "")
            {
                SfcANG();
                GlobalsInfo.Message.Red("读A条码-条码为空");
                SfcVerify_bool[K] = true;
                return;
            }
            if (Omron.SfcPrefix == "true")//前三位是否验证
            {
                if (Omron.SNPrefix != receiveMsg.Substring(0, 3))//高位)
                {
                    SfcANG();
                    GlobalsInfo.Message.Red("读A条码-前三位验证失败");
                    SfcVerify_bool[K] = true;
                    return;
                }
            }

            if (Omron.SfcLength == "true")//长度是否验证
            {
                if ((int)Conversion.Val(Omron.SNLength) != receiveMsg.Length)
                {
                    SfcANG();
                    GlobalsInfo.Message.Red("读A条码-长度验证失败");
                    SfcVerify_bool[K] = true;
                    return;
                }
            }

            SfcVerifyMes(K, receiveMsg);
            if (LoadRw.Keep_State.SfcVerify[K] == "OK")
            {
                //发送 PLC  OK
                SfcAOK();
            }
            else
            {
                SfcANG();
            }
            SfcVerify_bool[K] = true;
        }

        public void SfcAOK()
        {
            OmronHelper.writeResult_bool(Omron.ABline, "D27033.00", "true");
            OmronHelper.writeResult_bool(Omron.ABline, "D27033.01", "true");

            //string conBool = "";
            //OmronHelper._Read_bool(Omron.ABline, "D27033.00", ref conBool);
            //GlobalsInfo.Message.Green("D27033.00" + conBool);
            //OmronHelper._Read_bool(Omron.ABline, "D27033.01", ref conBool);
            //GlobalsInfo.Message.Green("D27033.01" + conBool);
            GlobalsInfo.Message.Green("条码验证写入A--OK");
        }
        public void SfcANG()
        {
            OmronHelper.writeResult_bool(Omron.ABline, "D27033.00", "false");
            OmronHelper.writeResult_bool(Omron.ABline, "D27033.01", "true");

            //string conBool = "";
            //OmronHelper._Read_bool(Omron.ABline, "D27033.00", ref conBool);
            //GlobalsInfo.Message.Green("D27033.00" + conBool);
            //OmronHelper._Read_bool(Omron.ABline, "D27033.01", ref conBool);
            //GlobalsInfo.Message.Green("D27033.01" + conBool);

            GlobalsInfo.Message.Red("条码验证写入A--NG");
        }

        public void SfcBOK()
        {
            OmronHelper.writeResult_bool(Omron.ABline, "D27133.00", "true");
            OmronHelper.writeResult_bool(Omron.ABline, "D27133.01", "true");
            GlobalsInfo.Message.Green("条码验证写入B--OK");
        }
        public void SfcBNG()
        {
            OmronHelper.writeResult_bool(Omron.ABline, "D27133.00", "false");
            OmronHelper.writeResult_bool(Omron.ABline, "D27133.01", "true");
            GlobalsInfo.Message.Red("条码验证写入B--NG");
        }

        public void SfcWOK()
        {
            OmronHelper.writeResult_bool(Omron.ABline, "D27233.00", "true");
            OmronHelper.writeResult_bool(Omron.ABline, "D27233.01", "true");
            GlobalsInfo.Message.Green("测试条码验证写入--OK");
        }
        public void SfcWNG()
        {
            OmronHelper.writeResult_bool(Omron.ABline, "D27233.00", "false");
            OmronHelper.writeResult_bool(Omron.ABline, "D27233.01", "true");
            GlobalsInfo.Message.Red("测试条码验证写入--NG");
        }


        public void SfcVerifyMes(int K, string sfc)
        {
            //3，如本地校验通过则上传MES
            if (GlobalsInfo.SelectLQ.MesOpen == "open")
            {
                if (GlobalsInfo.Global_SFC.MES == "打开")
                {
                    string status = "";
                    string message_web = "";
                    WebMesH.WebHelper.WebSFC_Verify(sfc, ref status, ref message_web);//检查电芯状态 OK
                    if (status == "0")
                    {
                        LoadRw.Keep_State.SfcVerify[K] = "OK";
                        LoadRw.Keep_State._Keep_State();  //保存
                    }
                    else
                    {
                        LoadRw.Keep_State.SfcVerify[K] = "NG";
                        LoadRw.Keep_State._Keep_State();  //保存
                    }
                }
                else
                {
                    LoadRw.Keep_State.SfcVerify[K] = "OK";
                    LoadRw.Keep_State._Keep_State();  //保存
                }
            }
            else
            {
                LoadRw.Keep_State.SfcVerify[K] = "OK";
                LoadRw.Keep_State._Keep_State();  //保存
            }
        }


        private void SfcVerifyBCall(int K)
        {
            //条码枪B
            //1，1#信号触发触发	%D23000.00	bool	
            string conBool = "";
            if (OmronHelper._Read_bool(Omron.ABline, "D27100.00", ref conBool) == false)
            {
                GlobalsInfo.Message.Red("读B条码PLC异常");
                _tim_SfcVerify();
                return;
            }
            if (conBool == "False")
            {
                SfcVerify_bool[K] = false;
                LoadRw.Keep_State.SfcVerify[K] = "";
                LoadRw.Keep_State._Keep_State();  //保存
                _tim_SfcVerify();
                return;
            }
            if (SfcVerify_bool[K] == true)
            {
                return;
            }
            GlobalsInfo.Message.Green("读B条码允许");

            if (LoadRw.Keep_State.SfcVerify[K] == "OK" || LoadRw.Keep_State.SfcVerify[K] == "NG")
            {
                if (LoadRw.Keep_State.SfcVerify[K] == "OK")
                {
                    //发送 PLC  OK
                    SfcBOK();
                    SfcVerify_bool[K] = true;
                }
                else
                {
                    //发送 PLC  NG
                    SfcBNG();
                    SfcVerify_bool[K] = true;
                }
                return;
            }

            //2，扫码枪扫码
            string Msg = "";
            OmronHelper._read_string(Omron.ABline, "D27101", "64", ref Msg);
            string receiveMsg = OmronHelper.char_string(Msg);
            GlobalsInfo.Message.Green("读B条码--" + receiveMsg);

            if (receiveMsg == "")
            {
                SfcBNG();
                GlobalsInfo.Message.Red("读B条码-条码为空");
                SfcVerify_bool[K] = true;
                return;
            }
            if (Omron.SfcPrefix == "true")//前三位是否验证
            {
                if (Omron.SNPrefix != receiveMsg.Substring(0, 3))//高位)
                {
                    SfcBNG();
                    GlobalsInfo.Message.Red("读B条码-前三位验证失败");
                    SfcVerify_bool[K] = true;
                    return;
                }
            }

            if (Omron.SfcLength == "true")//长度是否验证
            {
                if ((int)Conversion.Val(Omron.SNLength) != receiveMsg.Length)
                {
                    SfcBNG();
                    GlobalsInfo.Message.Red("读B条码-长度验证失败");
                    SfcVerify_bool[K] = true;
                    return;
                }
            }

            SfcVerifyMes(K, receiveMsg);
            if (LoadRw.Keep_State.SfcVerify[K] == "OK")
            {
                //发送 PLC  OK
                SfcBOK();
            }
            else
            {
                SfcBNG();
            }
            SfcVerify_bool[K] = true;
        }

        public void ThreadProcFlowB(object str)
        {
            do
            {
                //条码枪B SfcVerifyACall(0);
                SfcVerifyBCall(1);
                _tim_SfcVerify();
            }
            while (Omron.STOP == false);
        }

        private void SfcVerifyWCall(int K)
        {
            //条码枪B
            //1，1#信号触发触发	%D23000.00	bool	
            string conBool = "";
            if (OmronHelper._Read_bool(Omron.ABline, "D27200.00", ref conBool) == false)
            {
                GlobalsInfo.Message.Red("读测试条码PLC异常");
                _tim_SfcVerify();
                return;
            }
            if (conBool == "False")
            {
                SfcVerify_bool[K] = false;
                LoadRw.Keep_State.SfcVerify[K] = "";
                LoadRw.Keep_State._Keep_State();  //保存
                _tim_SfcVerify();
                return;
            }
            if (SfcVerify_bool[K] == true)
            {
                return;
            }
            GlobalsInfo.Message.Green("读测试条码允许");

            if (LoadRw.Keep_State.SfcVerify[K] == "OK" || LoadRw.Keep_State.SfcVerify[K] == "NG")
            {
                if (LoadRw.Keep_State.SfcVerify[K] == "OK")
                {
                    //发送 PLC  OK
                    SfcWOK();
                    SfcVerify_bool[K] = true;
                }
                else
                {
                    //发送 PLC  NG
                    SfcWNG();
                    SfcVerify_bool[K] = true;
                }
                return;
            }

            //2，扫码枪扫码
            string Msg = "";
            OmronHelper._read_string(Omron.ABline, "D27201", "64", ref Msg);
            string receiveMsg = OmronHelper.char_string(Msg);
            GlobalsInfo.Message.Green("读B条码--" + receiveMsg);

            if (receiveMsg == "")
            {
                SfcWNG();
                GlobalsInfo.Message.Red("读B条码-条码为空");
                SfcVerify_bool[K] = true;
                return;
            }
            if (Omron.SfcPrefix == "true")//前三位是否验证
            {
                if (Omron.SNPrefix != receiveMsg.Substring(0, 3))//高位)
                {
                    SfcWNG();
                    GlobalsInfo.Message.Red("读B条码-前三位验证失败");
                    SfcVerify_bool[K] = true;
                    return;
                }
            }

            if (Omron.SfcLength == "true")//长度是否验证
            {
                if ((int)Conversion.Val(Omron.SNLength) != receiveMsg.Length)
                {
                    SfcWNG();
                    GlobalsInfo.Message.Red("读B条码-长度验证失败");
                    SfcVerify_bool[K] = true;
                    return;
                }
            }

            SfcVerifyMes(K, receiveMsg);
            if (LoadRw.Keep_State.SfcVerify[K] == "OK")
            {
                //发送 PLC  OK
                SfcWOK();
            }
            else
            {
                SfcWNG();
            }
            SfcVerify_bool[K] = true;
        }


        public void ThreadProcFlowW(object str)
        {
            do
            {
                //条码枪B SfcVerifyACall(0);
                SfcVerifyWCall(2);
                _tim_SfcVerify();
            }
            while (Omron.STOP == false);
        }



    }
}
