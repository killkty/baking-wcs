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
    public class FlowOMR
    {
        //int tim_Omron = 30;     //3秒
        //public void tim_Omron_PLC()  // 
        //{
        //    for (int i = 0; i < tim_Omron; i++)
        //    {
        //        if (Omron.STOP == true)
        //        {
        //            return;
        //        }
        //        Thread.Sleep(100);
        //    }
        //}

        //public void ThreadProcFlow(object str)
        //{
        //    Thread.Sleep(1000);
        //    do
        //    {
        //        if (GlobalsInfo.Omron.Use[0] == "true" && GlobalsInfo.Omron.Connect[0] == "连接")
        //        {
        //            if (Omron_BKing_bool(0) == false)
        //            {
        //                GlobalsInfo.Omron.Use[0] = "false";
        //            }
        //        }
        //        if (Omron.STOP == true) { return; }
        //        if (GlobalsInfo.Omron.Use[3] == "true" && GlobalsInfo.Omron.Connect[3] == "连接")
        //        {
        //            if (Omron_BKing_bool(3) == false)
        //            {
        //                GlobalsInfo.Omron.Use[3] = "false";
        //            }
        //        }
        //        tim_Omron_PLC();
        //    }
        //    while (Omron.STOP == false);
        //}

        //public bool Omron_BKing_bool(int p)  //
        //{
        //    byte[] conbyte = new byte[94];  //800
        //    if (OmronHelper._read_Batch3(p, "D10000", "47", ref conbyte) == false)
        //    {
        //        return false;
        //    }
        //    if (p == 0)
        //    {
        //        OmronHelper._RefA_Bool(conbyte);
        //    }
        //    else if (p == 3)
        //    {
        //        OmronHelper._RefB_Bool(conbyte);
        //    }
        //    return true;
        //}






    }
}
