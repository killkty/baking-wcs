using GlobalsInfo;
using Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Flow
{
    public class FlowMonitoring
    {
        int tim_Monitoring = 30;     //3秒
        public void _tim_Monitoring()  // 
        {
            for (int i = 0; i < tim_Monitoring; i++)
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
                MonitoringCall();
                _tim_Monitoring();
            }
            while (Omron.STOP == false);
        }

        public void MonitoringCall()
        {

            byte[] conbyte = new byte[48];

            if (_Omron_RW("D10000", 100, ref conbyte) == false)
            {
                return;
            }
            _RefBool(conbyte);
            //_RefBool(conbyte5, ref GlobalsInfo.A_AreaA5.low_Alarm, K[4], 9);
            //_RefBool(conbyte5, ref GlobalsInfo.A_AreaA5.difference_Alarm, K[4], 10);
            //_RefBool(conbyte5, ref GlobalsInfo.A_AreaA5.Exception_Alarm, K[4], 11);
        }


        public bool _Omron_RW(string address, int Len, ref byte[] conbyte)
        {
            int Len2 = Len * 2;
            string Length = Len.ToString();     //"400";
            if (OmronHelper._read_Batch3(Omron.ABline, address, Length, ref conbyte) == false)
            {
                return false;
            }

            return true;
        }

        public void _RefBool(byte[] conbyte)
        {
            bool[,] ref_bool = new bool[48,16];
            int length = 16;
            byte[] ref_byte = new byte[2];
            bool[] nb = new bool[16];
            int con = 0;
            for (int i = 0; i < 16; i++)
            {
                ref_byte[0] = conbyte[con ];
                ref_byte[1] = conbyte[con + 1];
                nb = HslCommunication.BasicFramework.SoftBasic.ByteToBoolArray(ref_byte, length);
                for (int n = 0; n < 16; n++)
                {
                    ref_bool[i, n] = nb[n];
                }
                con = con + 2;   //20
            }




        }
    }
}
