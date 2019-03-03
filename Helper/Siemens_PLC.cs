using System;
using System.Threading;

namespace Helper
{
    public class Siemens_PLC
    {
        public static bool[] db_td = new bool[64];  //
        private static Object thisLock_Siemens = new Object();

        static int tim_S = 1;    //
        public static void tim_S_ex()  // 
        {
            for (int i = 0; i < tim_S; i++)
            {
                Thread.Sleep(100);
            }
        }


    }
}
