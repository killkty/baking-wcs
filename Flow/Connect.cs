using Helper;
using System;
using System.Net.NetworkInformation;

namespace Flow
{
    public class Connect
    {
        public bool _connect()
        {
            if (networkIsOk() == false)
            {
                return false;
            }
            //string[] text = new string[2];

            return true;
        }

        #region 判断
        private bool networkIsOk()
        {
            int timeout = 120;
            string mess = "";
            try
            {
                bool online = false;            //是否在线
                Ping ping = new Ping();
                PingReply pingReply;

                if (GlobalsInfo.Omron.Use[0] == "true")
                {
                    pingReply = ping.Send(GlobalsInfo.Omron.IP[0], timeout);
                    if (pingReply.Status != IPStatus.Success)
                    {
                        mess = "【A边调度】-PLC-IP[" + GlobalsInfo.Omron.IP[0] + "]无法连通";
                        GlobalsInfo.Message.Red(mess);
                        online = true;
                    }
                }
                if (GlobalsInfo.Omron.Use[1] == "true")
                {
                    pingReply = ping.Send(GlobalsInfo.Omron.IP[1], timeout);
                    if (pingReply.Status != IPStatus.Success)
                    {
                        mess = "【A边烤箱1】-PLC-IP[" + GlobalsInfo.Omron.IP[1] + "]无法连通";
                        GlobalsInfo.Message.Red(mess);
                        online = true;
                    }
                }
                if (GlobalsInfo.Omron.Use[2] == "true")
                {
                    pingReply = ping.Send(GlobalsInfo.Omron.IP[2], timeout);
                    if (pingReply.Status != IPStatus.Success)
                    {
                        mess = "【A边烤箱2】-PLC-IP[" + GlobalsInfo.Omron.IP[2] + "]无法连通";
                        GlobalsInfo.Message.Red(mess);
                        online = true;
                    }
                }

                if (GlobalsInfo.Omron.Use[3] == "true")
                {
                    pingReply = ping.Send(GlobalsInfo.Omron.IP[3], timeout);
                    if (pingReply.Status != IPStatus.Success)
                    {
                        mess = "【B边调度】-PLC-IP[" + GlobalsInfo.Omron.IP[3] + "]无法连通";
                        GlobalsInfo.Message.Red(mess);
                        online = true;
                    }
                }
                if (GlobalsInfo.Omron.Use[4] == "true")
                {
                    pingReply = ping.Send(GlobalsInfo.Omron.IP[4], timeout);
                    if (pingReply.Status != IPStatus.Success)
                    {
                        mess = "【B边烤箱1】-PLC-IP[" + GlobalsInfo.Omron.IP[4] + "]无法连通";
                        GlobalsInfo.Message.Red(mess);
                        online = true;
                    }
                }
                if (GlobalsInfo.Omron.Use[5] == "true")
                {
                    pingReply = ping.Send(GlobalsInfo.Omron.IP[5], timeout);
                    if (pingReply.Status != IPStatus.Success)
                    {
                        mess = "【B边烤箱2】-PLC-IP[" + GlobalsInfo.Omron.IP[5] + "]无法连通";
                        GlobalsInfo.Message.Red(mess);
                        online = true;
                    }
                }

                if (online == true)
                {
                    return false;
                }
                else
                {
                    return true;
                }

            }
            catch (Exception ex)
            {
                GlobalsInfo.Message.Red(ex.Message);
                return false;
            }
        }
        #endregion

    }
}
