using GlobalsInfo;
using Helper;
using System;
using System.Threading;

namespace Flow
{
    public class FlowOmron
    {
        int tim_Omron = 30;     //3秒
        public void tim_Omron_PLC()  // 
        {
            for (int i = 0; i < tim_Omron; i++)
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
            Thread.Sleep(1000);
            int p1;
            int p2;
            string[] address2A = new string[7] { "D0", "D400", "无", "无", "D2060", "D2400", "D2600" };
            string[] address2B = new string[7] { "D4000", "D4400", "无", "无", "D6060", "D6400", "D6600" };

            do
            {
                if (GlobalsInfo.Omron.Use[0] == "true" && GlobalsInfo.Omron.Connect[0] == "连接")
                {
                    if (Omron_BKing_bool(0) == false)
                    {
                        GlobalsInfo.Omron.Use[0] = "false";
                    }
                }
                if (Omron.STOP == true) { return; }
                if (GlobalsInfo.Omron.Use[3] == "true" && GlobalsInfo.Omron.Connect[3] == "连接")
                {
                    if (Omron_BKing_bool(3) == false)
                    {
                        GlobalsInfo.Omron.Use[3] = "false";
                    }
                }

                if (GlobalsInfo.Omron.Use[1] == "true" && GlobalsInfo.Omron.Connect[1] == "连接")
                {
                    p2 = 0;
                    p1 = 1;

                    if (Omron_BKing(p1, p2, address2A) == false)
                    {
                        GlobalsInfo.Omron.Use[1] = "false";
                    }
                }

                if (Omron.STOP == true) { return; }
                if (GlobalsInfo.Omron.Use[2] == "true" && GlobalsInfo.Omron.Connect[2] == "连接")
                {
                    p2 = 0;
                    p1 = 2;
                    if (Omron_BKing(p1, p2, address2B) == false)
                    {
                        GlobalsInfo.Omron.Use[2] = "false";
                    }
                }

                if (Omron.STOP == true) { return; }
                if (GlobalsInfo.Omron.Use[4] == "true" && GlobalsInfo.Omron.Connect[4] == "连接")
                {
                    p2 = 3;
                    p1 = 4;
                    if (Omron_BKing(p1, p2, address2A) == false)
                    {
                        GlobalsInfo.Omron.Use[4] = "false";
                    }
                }


                if (Omron.STOP == true) { return; }
                if (GlobalsInfo.Omron.Use[5] == "true" && GlobalsInfo.Omron.Connect[5] == "连接")
                {
                    p2 = 3;
                    p1 = 5;
                    if (Omron_BKing(p1, p2, address2B) == false)
                    {
                        GlobalsInfo.Omron.Use[5] = "false";
                    }
                }
                plc_OmronA_BKing();  //
                plc_OmronB_BKing();  //

                tim_Omron_PLC();
            }
            while (Omron.STOP == false);


        }

        public bool Omron_BKing_bool(int p)  //
        {
            byte[] conbyte = new byte[94];     //800
            if (OmronHelper._read_Batch3(p, "D10000", "47", ref conbyte) == false)
            {
                return false;
            }
            if (p == 0)
            {
                OmronHelper._RefA_Dispatch_Bool(conbyte);
            }
            else if (p == 3)
            {
                OmronHelper._RefB_Dispatch_Bool(conbyte);
            }
            byte[] conbyte1 = new byte[46];     //800
            if (OmronHelper._read_Batch3(p, "D11000", "23", ref conbyte1) == false)
            {
                return false;
            }
            if (p == 0)
            {
                OmronHelper._RefA_Loading_Bool(conbyte);
            }
            else if (p == 3)
            {
                OmronHelper._RefB_Loading_Bool(conbyte);
            }
            byte[] conbyte2 = new byte[34];      //800
            if (OmronHelper._read_Batch3(p, "D12000", "17", ref conbyte2) == false)
            {
                return false;
            }
            if (p == 0)
            {
                OmronHelper._RefA_layingOff_Bool(conbyte);
            }
            else if (p == 3)
            {
                OmronHelper._RefB_layingOff_Bool(conbyte);
            }
            return true;
        }


        public bool Omron_BKing(int p1, int p2, string[] address2)  //
        {
            double[] TestT1 = new double[16];
            double[] TestT2 = new double[16];
            double[] TestT3 = new double[16];
            double[] TestT4 = new double[16];

            double[] TestT5 = new double[16];
            double[] TestT6 = new double[16];
            double[] TestT7 = new double[16];
            double[] TestT8 = new double[16];

            double[] TestT9 = new double[16];
            double[] TestT10 = new double[16];
            double[] TestT11 = new double[16];
            double[] TestT12 = new double[16];



            double[] AlarmT1 = new double[16];
            double[] AlarmT2 = new double[16];
            double[] AlarmT3 = new double[16];
            double[] AlarmT4 = new double[16];

            double[] AlarmT5 = new double[16];
            double[] AlarmT6 = new double[16];
            double[] AlarmT7 = new double[16];
            double[] AlarmT8 = new double[16];

            double[] AlarmT9 = new double[16];
            double[] AlarmT10 = new double[16];
            double[] AlarmT11 = new double[16];
            double[] AlarmT12 = new double[16];


            int[] Len = new int[7] { 400, 480, 480, 480, 340, 200, 480 };
            int[] K = new int[7] { 20, 30, 30, 30, 20, 10, 30 };
            string[] address1 = new string[7] { "D0", "D400", "D900", "D1480", "D2060", "D2400", "D2600" };
            short[] content1 = new short[Len[0]];
            short[] content2 = new short[Len[1]];
            short[] content3 = new short[Len[2]];
            short[] content4 = new short[Len[3]];
            short[] content5 = new short[Len[4]];
            short[] content6 = new short[Len[5]];
            short[] content7 = new short[Len[6]];

            byte[] conbyte1 = new byte[Len[0] * 2];  //800
            byte[] conbyte2 = new byte[Len[1] * 2];
            byte[] conbyte3 = new byte[Len[2] * 2];
            byte[] conbyte4 = new byte[Len[3] * 2];
            byte[] conbyte5 = new byte[Len[4] * 2];
            byte[] conbyte6 = new byte[Len[5] * 2];
            byte[] conbyte7 = new byte[Len[6] * 2];

            //400
            if (_Omron_RW(address1[0], address2[0], p1, p2, Len[0], ref content1, ref conbyte1) == false)   //读地址1 写地址2 读P1  写P2 长度  返回数据\
            {
                return false;
            }
            if (p1 == 1)  //PLC 1   A边
            {
                _RefShort(content1, ref GlobalsInfo.A_AreaA1.Time, K[0], 0);     //1                //传入   返回  间隔  序号
                _RefShort(content1, ref GlobalsInfo.A_AreaA1.state, K[0], 1);
                RefString(content1, ref GlobalsInfo.A_AreaA1.oml, K[0], 2);
            }
            else if (p1 == 2) //PLC 2 A边
            {
                _RefShort(content1, ref GlobalsInfo.A_AreaB1.Time, K[0], 0);                     //传入   返回  间隔  序号
                _RefShort(content1, ref GlobalsInfo.A_AreaB1.state, K[0], 1);
                RefString(content1, ref GlobalsInfo.A_AreaB1.oml, K[0], 2);
            }
            else if (p1 == 4)// PLC 3 B边
            {
                _RefShort(content1, ref GlobalsInfo.B_AreaA1.Time, K[0], 0);                     //传入   返回  间隔  序号
                _RefShort(content1, ref GlobalsInfo.B_AreaA1.state, K[0], 1);
                RefString(content1, ref GlobalsInfo.B_AreaA1.oml, K[0], 2);
            }
            else if (p1 == 5) //PLC 4 B 边
            {
                _RefShort(content1, ref GlobalsInfo.B_AreaB1.Time, K[0], 0);                     //传入   返回  间隔  序号
                _RefShort(content1, ref GlobalsInfo.B_AreaB1.state, K[0], 1);
                RefString(content1, ref GlobalsInfo.B_AreaB1.oml, K[0], 2);
            }

            if (Omron.STOP == true) { return true; }
            if (_Omron_RW(address1[1], address2[1], p1, p2, Len[1], ref content2, ref conbyte2) == false)
            {
                return false;
            }
            if (p1 == 1)
            {
                _RefShort(content2, ref GlobalsInfo.A_AreaA2.T_set, K[1], 0);
                _RefShort(content2, ref GlobalsInfo.A_AreaA2.T_Alarm, K[1], 1);
            }
            else if (p1 == 2)
            {
                _RefShort(content2, ref GlobalsInfo.A_AreaB2.T_set, K[1], 0);
                _RefShort(content2, ref GlobalsInfo.A_AreaB2.T_Alarm, K[1], 1);
            }
            if (p1 == 4)
            {
                _RefShort(content2, ref GlobalsInfo.B_AreaA2.T_set, K[1], 0);
                _RefShort(content2, ref GlobalsInfo.B_AreaA2.T_Alarm, K[1], 1);
            }
            else if (p1 == 5)
            {
                _RefShort(content2, ref GlobalsInfo.B_AreaB2.T_set, K[1], 0);
                _RefShort(content2, ref GlobalsInfo.B_AreaB2.T_Alarm, K[1], 1);
            }


            if (Omron.STOP == true) { return true; }
            if (_Omron_RW(address1[2], address2[2], p1, p2, Len[2], ref content3, ref conbyte3) == false)
            {
                return false;
            }

            RefString(content3, ref TestT1, K[2], 0);
            RefString(content3, ref TestT2, K[2], 2);
            RefString(content3, ref TestT3, K[2], 4);
            RefString(content3, ref TestT4, K[2], 6);
            RefString(content3, ref TestT5, K[2], 8);
            RefString(content3, ref TestT6, K[2], 10);

            RefString(content3, ref TestT7, K[2], 12);
            RefString(content3, ref TestT8, K[2], 14);
            RefString(content3, ref TestT9, K[2], 16);
            RefString(content3, ref TestT10, K[2], 18);
            RefString(content3, ref TestT11, K[2], 20);
            RefString(content3, ref TestT12, K[2], 22);

            if (p1 == 1)
            {
                for (int n = 0; n < 16; n++)
                {
                    GlobalsInfo.A_AreaA3.TestT[n,0] = TestT1[n];
                    GlobalsInfo.A_AreaA3.TestT[n,1] = TestT2[n];
                    GlobalsInfo.A_AreaA3.TestT[n,2] = TestT3[n];
                    GlobalsInfo.A_AreaA3.TestT[n,3] = TestT4[n];
                    GlobalsInfo.A_AreaA3.TestT[n,4] = TestT5[n];
                    GlobalsInfo.A_AreaA3.TestT[n,5] = TestT6[n];
                    GlobalsInfo.A_AreaA3.TestT[n,6] = TestT7[n];
                    GlobalsInfo.A_AreaA3.TestT[n,7] = TestT8[n];
                    GlobalsInfo.A_AreaA3.TestT[n,8] = TestT9[n];
                    GlobalsInfo.A_AreaA3.TestT[n,9] = TestT10[n];
                    GlobalsInfo.A_AreaA3.TestT[n,10] = TestT11[n];
                    GlobalsInfo.A_AreaA3.TestT[n,11] = TestT12[n];
                }
            }
            else if (p1 == 2)
            {
                for (int n = 0; n < 16; n++)
                {
                    GlobalsInfo.A_AreaB3.TestT[n, 0] = TestT1[n];
                    GlobalsInfo.A_AreaB3.TestT[n, 1] = TestT2[n];
                    GlobalsInfo.A_AreaB3.TestT[n, 2] = TestT3[n];
                    GlobalsInfo.A_AreaB3.TestT[n, 3] = TestT4[n];
                    GlobalsInfo.A_AreaB3.TestT[n, 4] = TestT5[n];
                    GlobalsInfo.A_AreaB3.TestT[n, 5] = TestT6[n];
                    GlobalsInfo.A_AreaB3.TestT[n, 6] = TestT7[n];
                    GlobalsInfo.A_AreaB3.TestT[n, 7] = TestT8[n];
                    GlobalsInfo.A_AreaB3.TestT[n, 8] = TestT9[n];
                    GlobalsInfo.A_AreaB3.TestT[n, 9] = TestT10[n];
                    GlobalsInfo.A_AreaB3.TestT[n, 10] = TestT11[n];
                    GlobalsInfo.A_AreaB3.TestT[n, 11] = TestT12[n];
                }
            }

            if (p1 == 4)
            {
                for (int n = 0; n < 16; n++)
                {
                    GlobalsInfo.B_AreaA3.TestT[n, 0] = TestT1[n];
                    GlobalsInfo.B_AreaA3.TestT[n, 1] = TestT2[n];
                    GlobalsInfo.B_AreaA3.TestT[n, 2] = TestT3[n];
                    GlobalsInfo.B_AreaA3.TestT[n, 3] = TestT4[n];
                    GlobalsInfo.B_AreaA3.TestT[n, 4] = TestT5[n];
                    GlobalsInfo.B_AreaA3.TestT[n, 5] = TestT6[n];
                    GlobalsInfo.B_AreaA3.TestT[n, 6] = TestT7[n];
                    GlobalsInfo.B_AreaA3.TestT[n, 7] = TestT8[n];
                    GlobalsInfo.B_AreaA3.TestT[n, 8] = TestT9[n];
                    GlobalsInfo.B_AreaA3.TestT[n, 9] = TestT10[n];
                    GlobalsInfo.B_AreaA3.TestT[n, 10] = TestT11[n];
                    GlobalsInfo.B_AreaA3.TestT[n, 11] = TestT12[n];

                }
            }
            else if (p1 == 5)
            {
                for (int n = 0; n < 16; n++)
                {
                    GlobalsInfo.B_AreaB3.TestT[n, 0] = TestT1[n];
                    GlobalsInfo.B_AreaB3.TestT[n, 1] = TestT2[n];
                    GlobalsInfo.B_AreaB3.TestT[n, 2] = TestT3[n];
                    GlobalsInfo.B_AreaB3.TestT[n, 3] = TestT4[n];
                    GlobalsInfo.B_AreaB3.TestT[n, 4] = TestT5[n];
                    GlobalsInfo.B_AreaB3.TestT[n, 5] = TestT6[n];
                    GlobalsInfo.B_AreaB3.TestT[n, 6] = TestT7[n];
                    GlobalsInfo.B_AreaB3.TestT[n, 7] = TestT8[n];
                    GlobalsInfo.B_AreaB3.TestT[n, 8] = TestT9[n];
                    GlobalsInfo.B_AreaB3.TestT[n, 9] = TestT10[n];
                    GlobalsInfo.B_AreaB3.TestT[n, 10] = TestT11[n];
                    GlobalsInfo.B_AreaB3.TestT[n, 11] = TestT12[n];

                }
            }

            if (Omron.STOP == true) { return true; }
            if (_Omron_RW(address1[3], address2[3], p1, p2, Len[3], ref content4, ref conbyte4) == false)
            {
                return false;
            }

            RefString(content4, ref AlarmT1, K[3], 0);
            RefString(content4, ref AlarmT2, K[3], 2);
            RefString(content4, ref AlarmT3, K[3], 4);
            RefString(content4, ref AlarmT4, K[3], 6);
            RefString(content4, ref AlarmT5, K[3], 8);
            RefString(content4, ref AlarmT6, K[3], 10);

            RefString(content4, ref AlarmT7, K[3], 12);
            RefString(content4, ref AlarmT8, K[3], 14);
            RefString(content4, ref AlarmT9, K[3], 16);
            RefString(content4, ref AlarmT10, K[3], 18);
            RefString(content4, ref AlarmT11, K[3], 20);
            RefString(content4, ref AlarmT12, K[3], 22);

            if (p1 == 1)
            {
                for (int n = 0; n < 16; n++)
                {
                    GlobalsInfo.A_AreaA4.AlarmT[ n,0] = AlarmT1[n];
                    GlobalsInfo.A_AreaA4.AlarmT[ n,1] = AlarmT2[n];
                    GlobalsInfo.A_AreaA4.AlarmT[ n,2] = AlarmT3[n];
                    GlobalsInfo.A_AreaA4.AlarmT[ n,3] = AlarmT4[n];
                    GlobalsInfo.A_AreaA4.AlarmT[ n,4] = AlarmT5[n];
                    GlobalsInfo.A_AreaA4.AlarmT[ n,5] = AlarmT6[n];
                    GlobalsInfo.A_AreaA4.AlarmT[ n,6] = AlarmT7[n];
                    GlobalsInfo.A_AreaA4.AlarmT[ n,7] = AlarmT8[n];
                    GlobalsInfo.A_AreaA4.AlarmT[ n,8] = AlarmT9[n];
                    GlobalsInfo.A_AreaA4.AlarmT[ n,9] = AlarmT10[n];
                    GlobalsInfo.A_AreaA4.AlarmT[ n,10] = AlarmT11[n];
                    GlobalsInfo.A_AreaA4.AlarmT[ n,11] = AlarmT12[n];
                }
            }
            else if (p1 == 2)
            {
                for (int n = 0; n < 16; n++)
                {
                    GlobalsInfo.A_AreaB4.AlarmT[n, 0] = AlarmT1[n];
                    GlobalsInfo.A_AreaB4.AlarmT[n, 1] = AlarmT2[n];
                    GlobalsInfo.A_AreaB4.AlarmT[n, 2] = AlarmT3[n];
                    GlobalsInfo.A_AreaB4.AlarmT[n, 3] = AlarmT4[n];
                    GlobalsInfo.A_AreaB4.AlarmT[n, 4] = AlarmT5[n];
                    GlobalsInfo.A_AreaB4.AlarmT[n, 5] = AlarmT6[n];
                    GlobalsInfo.A_AreaB4.AlarmT[n, 6] = AlarmT7[n];
                    GlobalsInfo.A_AreaB4.AlarmT[n, 7] = AlarmT8[n];
                    GlobalsInfo.A_AreaB4.AlarmT[n, 8] = AlarmT9[n];
                    GlobalsInfo.A_AreaB4.AlarmT[n, 9] = AlarmT10[n];
                    GlobalsInfo.A_AreaB4.AlarmT[n, 10] = AlarmT11[n];
                    GlobalsInfo.A_AreaB4.AlarmT[n, 11] = AlarmT12[n];

                }
            }

            if (p1 == 4)
            {
                for (int n = 0; n < 16; n++)
                {
                    GlobalsInfo.B_AreaA4.AlarmT[n, 0] = AlarmT1[n];
                    GlobalsInfo.B_AreaA4.AlarmT[n, 1] = AlarmT2[n];
                    GlobalsInfo.B_AreaA4.AlarmT[n, 2] = AlarmT3[n];
                    GlobalsInfo.B_AreaA4.AlarmT[n, 3] = AlarmT4[n];
                    GlobalsInfo.B_AreaA4.AlarmT[n, 4] = AlarmT5[n];
                    GlobalsInfo.B_AreaA4.AlarmT[n, 5] = AlarmT6[n];
                    GlobalsInfo.B_AreaA4.AlarmT[n, 6] = AlarmT7[n];
                    GlobalsInfo.B_AreaA4.AlarmT[n, 7] = AlarmT8[n];
                    GlobalsInfo.B_AreaA4.AlarmT[n, 8] = AlarmT9[n];
                    GlobalsInfo.B_AreaA4.AlarmT[n, 9] = AlarmT10[n];
                    GlobalsInfo.B_AreaA4.AlarmT[n, 10] = AlarmT11[n];
                    GlobalsInfo.B_AreaA4.AlarmT[n, 11] = AlarmT12[n];

                }
            }
            else if (p1 == 5)
            {
                for (int n = 0; n < 16; n++)
                {
                    GlobalsInfo.B_AreaB4.AlarmT[n, 0] = AlarmT1[n];
                    GlobalsInfo.B_AreaB4.AlarmT[n, 1] = AlarmT2[n];
                    GlobalsInfo.B_AreaB4.AlarmT[n, 2] = AlarmT3[n];
                    GlobalsInfo.B_AreaB4.AlarmT[n, 3] = AlarmT4[n];
                    GlobalsInfo.B_AreaB4.AlarmT[n, 4] = AlarmT5[n];
                    GlobalsInfo.B_AreaB4.AlarmT[n, 5] = AlarmT6[n];
                    GlobalsInfo.B_AreaB4.AlarmT[n, 6] = AlarmT7[n];
                    GlobalsInfo.B_AreaB4.AlarmT[n, 7] = AlarmT8[n];
                    GlobalsInfo.B_AreaB4.AlarmT[n, 8] = AlarmT9[n];
                    GlobalsInfo.B_AreaB4.AlarmT[n, 9] = AlarmT10[n];
                    GlobalsInfo.B_AreaB4.AlarmT[n, 10] = AlarmT11[n];
                    GlobalsInfo.B_AreaB4.AlarmT[n, 11] = AlarmT12[n];
                }
            }



            if (Omron.STOP == true) { return true; }
            if (_Omron_RW(address1[4], address2[4], p1, p2, Len[4], ref content5, ref conbyte5) == false)
            {
                return false;
            }
            if (p1 == 1)
            {
                _RefShort(content5, ref GlobalsInfo.A_AreaA5.vacuo_Alarm1, K[4], 0);
                _RefShort(content5, ref GlobalsInfo.A_AreaA5.vacuo_Alarm2, K[4], 1);
                RefString(content5, ref GlobalsInfo.A_AreaA5.vacuo_Alarm3, K[4], 4);
                OmronHelper._RefBool(conbyte5, ref GlobalsInfo.A_AreaA5.exceed_Alarm, K[4], 8);
                OmronHelper._RefBool(conbyte5, ref GlobalsInfo.A_AreaA5.low_Alarm, K[4], 9);
                OmronHelper._RefBool(conbyte5, ref GlobalsInfo.A_AreaA5.difference_Alarm, K[4], 10);
                OmronHelper._RefBool(conbyte5, ref GlobalsInfo.A_AreaA5.Exception_Alarm, K[4], 11);
            }
            else if (p1 == 2)
            {
                _RefShort(content5, ref GlobalsInfo.A_AreaB5.vacuo_Alarm1, K[4], 0);
                _RefShort(content5, ref GlobalsInfo.A_AreaB5.vacuo_Alarm2, K[4], 1);
                RefString(content5, ref GlobalsInfo.A_AreaB5.vacuo_Alarm3, K[4], 4);
                OmronHelper._RefBool(conbyte5, ref GlobalsInfo.A_AreaB5.exceed_Alarm, K[4], 8);
                OmronHelper._RefBool(conbyte5, ref GlobalsInfo.A_AreaB5.low_Alarm, K[4], 9);
                OmronHelper._RefBool(conbyte5, ref GlobalsInfo.A_AreaB5.difference_Alarm, K[4], 10);
                OmronHelper._RefBool(conbyte5, ref GlobalsInfo.A_AreaB5.Exception_Alarm, K[4], 11);
            }

            if (p1 == 4)
            {
                _RefShort(content5, ref GlobalsInfo.B_AreaA5.vacuo_Alarm1, K[4], 0);
                _RefShort(content5, ref GlobalsInfo.B_AreaA5.vacuo_Alarm2, K[4], 1);
                RefString(content5, ref GlobalsInfo.B_AreaA5.vacuo_Alarm3, K[4], 4);
                OmronHelper._RefBool(conbyte5, ref GlobalsInfo.B_AreaA5.exceed_Alarm, K[4], 8);
                OmronHelper._RefBool(conbyte5, ref GlobalsInfo.B_AreaA5.low_Alarm, K[4], 9);
                OmronHelper._RefBool(conbyte5, ref GlobalsInfo.B_AreaA5.difference_Alarm, K[4], 10);
                OmronHelper._RefBool(conbyte5, ref GlobalsInfo.B_AreaA5.Exception_Alarm, K[4], 11);
            }
            else if (p1 == 5)
            {
                _RefShort(content5, ref GlobalsInfo.B_AreaB5.vacuo_Alarm1, K[4], 0);
                _RefShort(content5, ref GlobalsInfo.B_AreaB5.vacuo_Alarm2, K[4], 1);
                RefString(content5, ref GlobalsInfo.B_AreaB5.vacuo_Alarm3, K[4], 4);
                OmronHelper._RefBool(conbyte5, ref GlobalsInfo.B_AreaB5.exceed_Alarm, K[4], 8);
                OmronHelper._RefBool(conbyte5, ref GlobalsInfo.B_AreaB5.low_Alarm, K[4], 9);
                OmronHelper._RefBool(conbyte5, ref GlobalsInfo.B_AreaB5.difference_Alarm, K[4], 10);
                OmronHelper._RefBool(conbyte5, ref GlobalsInfo.B_AreaB5.Exception_Alarm, K[4], 11);
            }



            if (Omron.STOP == true) { return true; }


            if (_Omron_RW(address2[5], address1[5], p2, p1, Len[5], ref content6, ref conbyte6) == false)
            {
                return false;
            }
            if (p1 == 1)
            {
                _RefShortQD(content6, ref GlobalsInfo.A_AreaA6.Start_stop);
            }
            else if (p1 == 2)
            {
                _RefShortQD(content6, ref GlobalsInfo.A_AreaB6.Start_stop);
            }

            if (p1 == 4)
            {
                _RefShortQD(content6, ref GlobalsInfo.B_AreaA6.Start_stop);
            }
            else if (p1 == 5)
            {
                _RefShortQD(content6, ref GlobalsInfo.B_AreaB6.Start_stop);
            }

            //if (Omron.STOP == true) { return true; }

            //if (_Omron_RW(address2[6], address1[6], p2, p1, Len[6], ref content7, ref conbyte7) == false)
            //{
            //    return false;
            //}
            return true;
        }
        public void plc_OmronA_BKing()  //
        {

            if (GlobalsInfo.SelectLQ.SLine == "4列")  //  如果选择4  A  如果现在5   如果选择6   如果选择7   如果选择8
            {
                for (int i = 0; i < 4; i++)
                {
                    GlobalsInfo.A_AreaA1.plc_Time[0, i] = GlobalsInfo.A_AreaA1.Time[i];
                    GlobalsInfo.A_AreaA1.plc_state[0, i] = GlobalsInfo.A_AreaA1.state[i];
                    GlobalsInfo.A_AreaA1.plc_oml[0, i] = GlobalsInfo.A_AreaA1.oml[i];

                    GlobalsInfo.A_AreaA2.plc_T_set[0, i] = GlobalsInfo.A_AreaA2.T_set[i];
                    GlobalsInfo.A_AreaA2.plc_T_Alarm[0, i] = GlobalsInfo.A_AreaA2.T_Alarm[i];
                    for (int j = 0; j < 12; j++)
                    {
                        GlobalsInfo.A_AreaA3.plc_TestT[0,  i,j] = GlobalsInfo.A_AreaA3.TestT[i, j];
                        GlobalsInfo.A_AreaA4.plc_AlarmT[0, i, j] = GlobalsInfo.A_AreaA4.AlarmT[i, j];
                    }
                    GlobalsInfo.A_AreaA5.plc_vacuo_Alarm1[0, i] = GlobalsInfo.A_AreaA5.vacuo_Alarm1[i];
                    GlobalsInfo.A_AreaA5.plc_vacuo_Alarm2[0, i] = GlobalsInfo.A_AreaA5.vacuo_Alarm2[i];
                    GlobalsInfo.A_AreaA5.plc_vacuo_Alarm3[0, i] = GlobalsInfo.A_AreaA5.vacuo_Alarm3[i];

                    for (int j = 0; j < 12; j++)
                    {
                        GlobalsInfo.A_AreaA5.plc_exceed_Alarm[0, i, j] = GlobalsInfo.A_AreaA5.exceed_Alarm[i, j];
                        GlobalsInfo.A_AreaA5.plc_low_Alarm[0, i, j] = GlobalsInfo.A_AreaA5.low_Alarm[i, j];
                        GlobalsInfo.A_AreaA5.plc_difference_Alarm[0, i, j] = GlobalsInfo.A_AreaA5.difference_Alarm[i, j];
                        GlobalsInfo.A_AreaA5.plc_Exception_Alarm[0, i, j] = GlobalsInfo.A_AreaA5.Exception_Alarm[i, j];
                    }
                    GlobalsInfo.A_AreaA6.Sstop[0, i] = GlobalsInfo.A_AreaA6.Start_stop[i];


                    //***
                    GlobalsInfo.A_AreaA1.plc_Time[1, i] = GlobalsInfo.A_AreaB1.Time[i];
                    GlobalsInfo.A_AreaA1.plc_state[1, i] = GlobalsInfo.A_AreaB1.state[i];
                    GlobalsInfo.A_AreaA1.plc_oml[1, i] = GlobalsInfo.A_AreaB1.oml[i];

                    GlobalsInfo.A_AreaA2.plc_T_set[1, i] = GlobalsInfo.A_AreaB2.T_set[i];
                    GlobalsInfo.A_AreaA2.plc_T_Alarm[1, i] = GlobalsInfo.A_AreaB2.T_Alarm[i];
                    for (int j = 0; j < 12; j++)
                    {
                        GlobalsInfo.A_AreaA3.plc_TestT[1, i, j] = GlobalsInfo.A_AreaB3.TestT[i, j];
                        GlobalsInfo.A_AreaA4.plc_AlarmT[1, i, j] = GlobalsInfo.A_AreaB4.AlarmT[i, j];
                    }
                    GlobalsInfo.A_AreaA5.plc_vacuo_Alarm1[1, i] = GlobalsInfo.A_AreaB5.vacuo_Alarm1[i];
                    GlobalsInfo.A_AreaA5.plc_vacuo_Alarm2[1, i] = GlobalsInfo.A_AreaB5.vacuo_Alarm2[i];
                    GlobalsInfo.A_AreaA5.plc_vacuo_Alarm3[1, i] = GlobalsInfo.A_AreaB5.vacuo_Alarm3[i];

                    for (int j = 0; j < 12; j++)
                    {
                        GlobalsInfo.A_AreaA5.plc_exceed_Alarm[1, i, j] = GlobalsInfo.A_AreaB5.exceed_Alarm[i, j];
                        GlobalsInfo.A_AreaA5.plc_low_Alarm[1, i, j] = GlobalsInfo.A_AreaB5.low_Alarm[i, j];
                        GlobalsInfo.A_AreaA5.plc_difference_Alarm[1, i, j] = GlobalsInfo.A_AreaB5.difference_Alarm[i, j];
                        GlobalsInfo.A_AreaA5.plc_Exception_Alarm[1, i, j] = GlobalsInfo.A_AreaB5.Exception_Alarm[i, j];
                    }
                    GlobalsInfo.A_AreaA6.Sstop[1, i] = GlobalsInfo.A_AreaB6.Start_stop[i];


                }
                int n = 0;
                for (int i = 4; i < 8; i++)
                {
                    GlobalsInfo.A_AreaA1.plc_Time[2, n] = GlobalsInfo.A_AreaA1.Time[i];
                    GlobalsInfo.A_AreaA1.plc_state[2, n] = GlobalsInfo.A_AreaA1.state[i];
                    GlobalsInfo.A_AreaA1.plc_oml[2, n] = GlobalsInfo.A_AreaA1.oml[i];

                    GlobalsInfo.A_AreaA2.plc_T_set[2, n] = GlobalsInfo.A_AreaA2.T_set[i];
                    GlobalsInfo.A_AreaA2.plc_T_Alarm[2, n] = GlobalsInfo.A_AreaA2.T_Alarm[i];
                    for (int j = 0; j < 12; j++)
                    {
                        GlobalsInfo.A_AreaA3.plc_TestT[2, n, j] = GlobalsInfo.A_AreaA3.TestT[i, j];
                        GlobalsInfo.A_AreaA4.plc_AlarmT[2, n, j] = GlobalsInfo.A_AreaA4.AlarmT[i, j];
                    }
                    GlobalsInfo.A_AreaA5.plc_vacuo_Alarm1[2, n] = GlobalsInfo.A_AreaA5.vacuo_Alarm1[i];
                    GlobalsInfo.A_AreaA5.plc_vacuo_Alarm2[2, n] = GlobalsInfo.A_AreaA5.vacuo_Alarm2[i];
                    GlobalsInfo.A_AreaA5.plc_vacuo_Alarm3[2, n] = GlobalsInfo.A_AreaA5.vacuo_Alarm3[i];

                    for (int j = 0; j < 12; j++)
                    {
                        GlobalsInfo.A_AreaA5.plc_exceed_Alarm[2, n, j] = GlobalsInfo.A_AreaA5.exceed_Alarm[i, j];
                        GlobalsInfo.A_AreaA5.plc_low_Alarm[2, n, j] = GlobalsInfo.A_AreaA5.low_Alarm[i, j];
                        GlobalsInfo.A_AreaA5.plc_difference_Alarm[2, n, j] = GlobalsInfo.A_AreaA5.difference_Alarm[i, j];
                        GlobalsInfo.A_AreaA5.plc_Exception_Alarm[2, n, j] = GlobalsInfo.A_AreaA5.Exception_Alarm[i, j];
                    }
                    GlobalsInfo.A_AreaA6.Sstop[2, n] = GlobalsInfo.A_AreaA6.Start_stop[i];

                    //***
                    GlobalsInfo.A_AreaA1.plc_Time[3, n] = GlobalsInfo.A_AreaB1.Time[i];
                    GlobalsInfo.A_AreaA1.plc_state[3, n] = GlobalsInfo.A_AreaB1.state[i];
                    GlobalsInfo.A_AreaA1.plc_oml[3, n] = GlobalsInfo.A_AreaB1.oml[i];

                    GlobalsInfo.A_AreaA2.plc_T_set[3, n] = GlobalsInfo.A_AreaB2.T_set[i];
                    GlobalsInfo.A_AreaA2.plc_T_Alarm[3, n] = GlobalsInfo.A_AreaB2.T_Alarm[i];
                    for (int j = 0; j < 12; j++)
                    {
                        GlobalsInfo.A_AreaA3.plc_TestT[3, n, j] = GlobalsInfo.A_AreaB3.TestT[i, j];
                        GlobalsInfo.A_AreaA4.plc_AlarmT[3, n, j] = GlobalsInfo.A_AreaB4.AlarmT[i, j];
                    }
                    GlobalsInfo.A_AreaA5.plc_vacuo_Alarm1[3, n] = GlobalsInfo.A_AreaB5.vacuo_Alarm1[i];
                    GlobalsInfo.A_AreaA5.plc_vacuo_Alarm2[3, n] = GlobalsInfo.A_AreaB5.vacuo_Alarm2[i];
                    GlobalsInfo.A_AreaA5.plc_vacuo_Alarm3[3, n] = GlobalsInfo.A_AreaB5.vacuo_Alarm3[i];

                    for (int j = 0; j < 12; j++)
                    {
                        GlobalsInfo.A_AreaA5.plc_exceed_Alarm[3, n, j] = GlobalsInfo.A_AreaB5.exceed_Alarm[i, j];
                        GlobalsInfo.A_AreaA5.plc_low_Alarm[3, n, j] = GlobalsInfo.A_AreaB5.low_Alarm[i, j];
                        GlobalsInfo.A_AreaA5.plc_difference_Alarm[3, n, j] = GlobalsInfo.A_AreaB5.difference_Alarm[i, j];
                        GlobalsInfo.A_AreaA5.plc_Exception_Alarm[3, n, j] = GlobalsInfo.A_AreaB5.Exception_Alarm[i, j];
                    }
                    GlobalsInfo.A_AreaA6.Sstop[3, n] = GlobalsInfo.A_AreaB6.Start_stop[i];
                    n++;
                }
            }

            else if (GlobalsInfo.SelectLQ.SLine == "5列")
            {
                //for (int i = 0; i < 5; i++)
                //{
                //    GlobalsInfo.A_AreaA1.plc_Time[2, i] = GlobalsInfo.A_AreaA1.Time[i];
                //    GlobalsInfo.A_AreaA1.plc_state[2, i] = GlobalsInfo.A_AreaA1.state[i];
                //    GlobalsInfo.A_AreaA1.plc_oml[2, i] = GlobalsInfo.A_AreaA1.oml[i];

                //    GlobalsInfo.A_AreaA2.plc_T_set[2, i] = GlobalsInfo.A_AreaA2.T_set[i];
                //    GlobalsInfo.A_AreaA2.plc_T_Alarm[2, i] = GlobalsInfo.A_AreaA2.T_Alarm[i];
                //    for (int j = 0; j < 12; j++)
                //    {
                //        GlobalsInfo.A_AreaA3.plc_TestT[2, i, j] = GlobalsInfo.A_AreaA3.TestT[i, j];
                //        GlobalsInfo.A_AreaA4.plc_AlarmT[2, i, j] = GlobalsInfo.A_AreaA4.AlarmT[i, j];
                //    }
                //    GlobalsInfo.A_AreaA5.plc_vacuo_Alarm1[2, i] = GlobalsInfo.A_AreaA5.vacuo_Alarm1[i];
                //    GlobalsInfo.A_AreaA5.plc_vacuo_Alarm2[2, i] = GlobalsInfo.A_AreaA5.vacuo_Alarm2[i];
                //    GlobalsInfo.A_AreaA5.plc_vacuo_Alarm3[2, i] = GlobalsInfo.A_AreaA5.vacuo_Alarm3[i];

                //    for (int j = 0; j < 12; j++)
                //    {
                //        GlobalsInfo.A_AreaA5.plc_exceed_Alarm[2, i, j] = GlobalsInfo.A_AreaA5.exceed_Alarm[i, j];
                //        GlobalsInfo.A_AreaA5.plc_low_Alarm[2, i, j] = GlobalsInfo.A_AreaA5.low_Alarm[i, j];
                //        GlobalsInfo.A_AreaA5.plc_difference_Alarm[2, i, j] = GlobalsInfo.A_AreaA5.difference_Alarm[i, j];
                //        GlobalsInfo.A_AreaA5.plc_Exception_Alarm[2, i, j] = GlobalsInfo.A_AreaA5.Exception_Alarm[i, j];
                //    }
                //    //GlobalsInfo.A_AreaA6.Sstop[2, n] = GlobalsInfo.A_AreaA6.Start_stop[i];


                //    GlobalsInfo.A_AreaA6.Sstop[0, i] = GlobalsInfo.A_AreaA6.Start_stop[i];
                //}
                //int n = 0;
                //for (int i = 5; i < 10; i++)
                //{
                //    GlobalsInfo.A_AreaA6.Sstop[2, n] = GlobalsInfo.A_AreaA6.Start_stop[i];
                //    n++;
                //}
            }
            else if (GlobalsInfo.SelectLQ.SLine == "6列")
            {
                //for (int i = 0; i < 6; i++)
                //{
                //    GlobalsInfo.A_AreaA6.Sstop[0, i] = GlobalsInfo.A_AreaA6.Start_stop[i];
                //}
                //int n = 0;
                //for (int i = 6; i < 12; i++)
                //{
                //    GlobalsInfo.A_AreaA6.Sstop[2, n] = GlobalsInfo.A_AreaA6.Start_stop[i];
                //    n++;
                //}
            }
            else if (GlobalsInfo.SelectLQ.SLine == "7列")
            {
                //for (int i = 0; i < 7; i++)
                //{
                //    GlobalsInfo.A_AreaA6.Sstop[0, i] = GlobalsInfo.A_AreaA6.Start_stop[i];
                //}
                //int n = 0;
                //for (int i = 7; i < 14; i++)
                //{
                //    GlobalsInfo.A_AreaA6.Sstop[2, n] = GlobalsInfo.A_AreaA6.Start_stop[i];
                //    n++;
                //}
            }
            else if (GlobalsInfo.SelectLQ.SLine == "8列")
            {
                //for (int i = 0; i < 8; i++)
                //{
                //    GlobalsInfo.A_AreaA6.Sstop[0, i] = GlobalsInfo.A_AreaA6.Start_stop[i];
                //}
                //int n = 0;
                //for (int i = 8; i < 16; i++)
                //{
                //    GlobalsInfo.A_AreaA6.Sstop[2, n] = GlobalsInfo.A_AreaA6.Start_stop[i];
                //    n++;
                //}
            }


        }


        public void plc_OmronB_BKing()  //
        {

            if (GlobalsInfo.SelectLQ.SLine == "4列")  //  如果选择4  A  如果现在5   如果选择6   如果选择7   如果选择8
            {
                for (int i = 0; i < 4; i++)
                {
                    GlobalsInfo.B_AreaA1.plc_Time[0, i] = GlobalsInfo.B_AreaA1.Time[i];
                    GlobalsInfo.B_AreaA1.plc_state[0, i] = GlobalsInfo.B_AreaA1.state[i];
                    GlobalsInfo.B_AreaA1.plc_oml[0, i] = GlobalsInfo.B_AreaA1.oml[i];

                    GlobalsInfo.B_AreaA2.plc_T_set[0, i] = GlobalsInfo.B_AreaA2.T_set[i];
                    GlobalsInfo.B_AreaA2.plc_T_Alarm[0, i] = GlobalsInfo.B_AreaA2.T_Alarm[i];
                    for (int j = 0; j < 12; j++)
                    {
                        GlobalsInfo.B_AreaA3.plc_TestT[0, i, j] = GlobalsInfo.B_AreaA3.TestT[i, j];
                        GlobalsInfo.B_AreaA4.plc_AlarmT[0, i, j] = GlobalsInfo.B_AreaA4.AlarmT[i, j];
                    }
                    GlobalsInfo.B_AreaA5.plc_vacuo_Alarm1[0, i] = GlobalsInfo.B_AreaA5.vacuo_Alarm1[i];
                    GlobalsInfo.B_AreaA5.plc_vacuo_Alarm2[0, i] = GlobalsInfo.B_AreaA5.vacuo_Alarm2[i];
                    GlobalsInfo.B_AreaA5.plc_vacuo_Alarm3[0, i] = GlobalsInfo.B_AreaA5.vacuo_Alarm3[i];

                    for (int j = 0; j < 12; j++)
                    {
                        GlobalsInfo.B_AreaA5.plc_exceed_Alarm[0, i, j] = GlobalsInfo.B_AreaA5.exceed_Alarm[i, j];
                        GlobalsInfo.B_AreaA5.plc_low_Alarm[0, i, j] = GlobalsInfo.B_AreaA5.low_Alarm[i, j];
                        GlobalsInfo.B_AreaA5.plc_difference_Alarm[0, i, j] = GlobalsInfo.B_AreaA5.difference_Alarm[i, j];
                        GlobalsInfo.B_AreaA5.plc_Exception_Alarm[0, i, j] = GlobalsInfo.B_AreaA5.Exception_Alarm[i, j];
                    }
                    GlobalsInfo.B_AreaA6.Sstop[0, i] = GlobalsInfo.B_AreaA6.Start_stop[i];


                    //***
                    GlobalsInfo.B_AreaA1.plc_Time[1, i] = GlobalsInfo.B_AreaB1.Time[i];
                    GlobalsInfo.B_AreaA1.plc_state[1, i] = GlobalsInfo.B_AreaB1.state[i];
                    GlobalsInfo.B_AreaA1.plc_oml[1, i] = GlobalsInfo.B_AreaB1.oml[i];

                    GlobalsInfo.B_AreaA2.plc_T_set[1, i] = GlobalsInfo.B_AreaB2.T_set[i];
                    GlobalsInfo.B_AreaA2.plc_T_Alarm[1, i] = GlobalsInfo.B_AreaB2.T_Alarm[i];
                    for (int j = 0; j < 12; j++)
                    {
                        GlobalsInfo.B_AreaA3.plc_TestT[1, i, j] = GlobalsInfo.B_AreaB3.TestT[i, j];
                        GlobalsInfo.B_AreaA4.plc_AlarmT[1, i, j] = GlobalsInfo.B_AreaB4.AlarmT[i, j];
                    }
                    GlobalsInfo.B_AreaA5.plc_vacuo_Alarm1[1, i] = GlobalsInfo.B_AreaB5.vacuo_Alarm1[i];
                    GlobalsInfo.B_AreaA5.plc_vacuo_Alarm2[1, i] = GlobalsInfo.B_AreaB5.vacuo_Alarm2[i];
                    GlobalsInfo.B_AreaA5.plc_vacuo_Alarm3[1, i] = GlobalsInfo.B_AreaB5.vacuo_Alarm3[i];

                    for (int j = 0; j < 12; j++)
                    {
                        GlobalsInfo.B_AreaA5.plc_exceed_Alarm[1, i, j] = GlobalsInfo.B_AreaB5.exceed_Alarm[i, j];
                        GlobalsInfo.B_AreaA5.plc_low_Alarm[1, i, j] = GlobalsInfo.B_AreaB5.low_Alarm[i, j];
                        GlobalsInfo.B_AreaA5.plc_difference_Alarm[1, i, j] = GlobalsInfo.B_AreaB5.difference_Alarm[i, j];
                        GlobalsInfo.B_AreaA5.plc_Exception_Alarm[1, i, j] = GlobalsInfo.B_AreaB5.Exception_Alarm[i, j];
                    }
                    GlobalsInfo.B_AreaA6.Sstop[1, i] = GlobalsInfo.B_AreaB6.Start_stop[i];


                }
                int n = 0;
                for (int i = 4; i < 8; i++)
                {
                    GlobalsInfo.B_AreaA1.plc_Time[2, i] = GlobalsInfo.B_AreaA1.Time[i];
                    GlobalsInfo.B_AreaA1.plc_state[2, i] = GlobalsInfo.B_AreaA1.state[i];
                    GlobalsInfo.B_AreaA1.plc_oml[2, i] = GlobalsInfo.B_AreaA1.oml[i];

                    GlobalsInfo.B_AreaA2.plc_T_set[2, i] = GlobalsInfo.B_AreaA2.T_set[i];
                    GlobalsInfo.B_AreaA2.plc_T_Alarm[2, i] = GlobalsInfo.B_AreaA2.T_Alarm[i];
                    for (int j = 0; j < 12; j++)
                    {
                        GlobalsInfo.B_AreaA3.plc_TestT[2, i, j] = GlobalsInfo.B_AreaA3.TestT[i, j];
                        GlobalsInfo.B_AreaA4.plc_AlarmT[2, i, j] = GlobalsInfo.B_AreaA4.AlarmT[i, j];
                    }
                    GlobalsInfo.B_AreaA5.plc_vacuo_Alarm1[2, i] = GlobalsInfo.B_AreaA5.vacuo_Alarm1[i];
                    GlobalsInfo.B_AreaA5.plc_vacuo_Alarm2[2, i] = GlobalsInfo.B_AreaA5.vacuo_Alarm2[i];
                    GlobalsInfo.B_AreaA5.plc_vacuo_Alarm3[2, i] = GlobalsInfo.B_AreaA5.vacuo_Alarm3[i];

                    for (int j = 0; j < 12; j++)
                    {
                        GlobalsInfo.B_AreaA5.plc_exceed_Alarm[2, i, j] = GlobalsInfo.B_AreaA5.exceed_Alarm[i, j];
                        GlobalsInfo.B_AreaA5.plc_low_Alarm[2, i, j] = GlobalsInfo.B_AreaA5.low_Alarm[i, j];
                        GlobalsInfo.B_AreaA5.plc_difference_Alarm[2, i, j] = GlobalsInfo.B_AreaA5.difference_Alarm[i, j];
                        GlobalsInfo.B_AreaA5.plc_Exception_Alarm[2, i, j] = GlobalsInfo.B_AreaA5.Exception_Alarm[i, j];
                    }
                    GlobalsInfo.B_AreaA6.Sstop[2, n] = GlobalsInfo.B_AreaA6.Start_stop[i];

                    //***
                    GlobalsInfo.B_AreaA1.plc_Time[3, i] = GlobalsInfo.B_AreaB1.Time[i];
                    GlobalsInfo.B_AreaA1.plc_state[3, i] = GlobalsInfo.B_AreaB1.state[i];
                    GlobalsInfo.B_AreaA1.plc_oml[3, i] = GlobalsInfo.B_AreaB1.oml[i];

                    GlobalsInfo.B_AreaA2.plc_T_set[3, i] = GlobalsInfo.B_AreaB2.T_set[i];
                    GlobalsInfo.B_AreaA2.plc_T_Alarm[3, i] = GlobalsInfo.B_AreaB2.T_Alarm[i];
                    for (int j = 0; j < 12; j++)
                    {
                        GlobalsInfo.B_AreaA3.plc_TestT[3, i, j] = GlobalsInfo.B_AreaB3.TestT[i, j];
                        GlobalsInfo.B_AreaA4.plc_AlarmT[3, i, j] = GlobalsInfo.B_AreaB4.AlarmT[i, j];
                    }
                    GlobalsInfo.B_AreaA5.plc_vacuo_Alarm1[3, i] = GlobalsInfo.B_AreaB5.vacuo_Alarm1[i];
                    GlobalsInfo.B_AreaA5.plc_vacuo_Alarm2[3, i] = GlobalsInfo.B_AreaB5.vacuo_Alarm2[i];
                    GlobalsInfo.B_AreaA5.plc_vacuo_Alarm3[3, i] = GlobalsInfo.B_AreaB5.vacuo_Alarm3[i];

                    for (int j = 0; j < 12; j++)
                    {
                        GlobalsInfo.B_AreaA5.plc_exceed_Alarm[3, i, j] = GlobalsInfo.B_AreaB5.exceed_Alarm[i, j];
                        GlobalsInfo.B_AreaA5.plc_low_Alarm[3, i, j] = GlobalsInfo.B_AreaB5.low_Alarm[i, j];
                        GlobalsInfo.B_AreaA5.plc_difference_Alarm[3, i, j] = GlobalsInfo.B_AreaB5.difference_Alarm[i, j];
                        GlobalsInfo.B_AreaA5.plc_Exception_Alarm[3, i, j] = GlobalsInfo.B_AreaB5.Exception_Alarm[i, j];
                    }
                    GlobalsInfo.B_AreaA6.Sstop[3, n] = GlobalsInfo.B_AreaB6.Start_stop[i];
                    n++;
                }
            }

            else if (GlobalsInfo.SelectLQ.SLine == "5列")
            {
                //for (int i = 0; i < 5; i++)
                //{
                //    GlobalsInfo.A_AreaA1.plc_Time[2, i] = GlobalsInfo.A_AreaA1.Time[i];
                //    GlobalsInfo.A_AreaA1.plc_state[2, i] = GlobalsInfo.A_AreaA1.state[i];
                //    GlobalsInfo.A_AreaA1.plc_oml[2, i] = GlobalsInfo.A_AreaA1.oml[i];

                //    GlobalsInfo.A_AreaA2.plc_T_set[2, i] = GlobalsInfo.A_AreaA2.T_set[i];
                //    GlobalsInfo.A_AreaA2.plc_T_Alarm[2, i] = GlobalsInfo.A_AreaA2.T_Alarm[i];
                //    for (int j = 0; j < 12; j++)
                //    {
                //        GlobalsInfo.A_AreaA3.plc_TestT[2, i, j] = GlobalsInfo.A_AreaA3.TestT[i, j];
                //        GlobalsInfo.A_AreaA4.plc_AlarmT[2, i, j] = GlobalsInfo.A_AreaA4.AlarmT[i, j];
                //    }
                //    GlobalsInfo.A_AreaA5.plc_vacuo_Alarm1[2, i] = GlobalsInfo.A_AreaA5.vacuo_Alarm1[i];
                //    GlobalsInfo.A_AreaA5.plc_vacuo_Alarm2[2, i] = GlobalsInfo.A_AreaA5.vacuo_Alarm2[i];
                //    GlobalsInfo.A_AreaA5.plc_vacuo_Alarm3[2, i] = GlobalsInfo.A_AreaA5.vacuo_Alarm3[i];

                //    for (int j = 0; j < 12; j++)
                //    {
                //        GlobalsInfo.A_AreaA5.plc_exceed_Alarm[2, i, j] = GlobalsInfo.A_AreaA5.exceed_Alarm[i, j];
                //        GlobalsInfo.A_AreaA5.plc_low_Alarm[2, i, j] = GlobalsInfo.A_AreaA5.low_Alarm[i, j];
                //        GlobalsInfo.A_AreaA5.plc_difference_Alarm[2, i, j] = GlobalsInfo.A_AreaA5.difference_Alarm[i, j];
                //        GlobalsInfo.A_AreaA5.plc_Exception_Alarm[2, i, j] = GlobalsInfo.A_AreaA5.Exception_Alarm[i, j];
                //    }
                //    //GlobalsInfo.A_AreaA6.Sstop[2, n] = GlobalsInfo.A_AreaA6.Start_stop[i];


                //    GlobalsInfo.A_AreaA6.Sstop[0, i] = GlobalsInfo.A_AreaA6.Start_stop[i];
                //}
                //int n = 0;
                //for (int i = 5; i < 10; i++)
                //{
                //    GlobalsInfo.A_AreaA6.Sstop[2, n] = GlobalsInfo.A_AreaA6.Start_stop[i];
                //    n++;
                //}
            }
            else if (GlobalsInfo.SelectLQ.SLine == "6列")
            {
                //for (int i = 0; i < 6; i++)
                //{
                //    GlobalsInfo.A_AreaA6.Sstop[0, i] = GlobalsInfo.A_AreaA6.Start_stop[i];
                //}
                //int n = 0;
                //for (int i = 6; i < 12; i++)
                //{
                //    GlobalsInfo.A_AreaA6.Sstop[2, n] = GlobalsInfo.A_AreaA6.Start_stop[i];
                //    n++;
                //}
            }
            else if (GlobalsInfo.SelectLQ.SLine == "7列")
            {
                //for (int i = 0; i < 7; i++)
                //{
                //    GlobalsInfo.A_AreaA6.Sstop[0, i] = GlobalsInfo.A_AreaA6.Start_stop[i];
                //}
                //int n = 0;
                //for (int i = 7; i < 14; i++)
                //{
                //    GlobalsInfo.A_AreaA6.Sstop[2, n] = GlobalsInfo.A_AreaA6.Start_stop[i];
                //    n++;
                //}
            }
            else if (GlobalsInfo.SelectLQ.SLine == "8列")
            {
                //for (int i = 0; i < 8; i++)
                //{
                //    GlobalsInfo.A_AreaA6.Sstop[0, i] = GlobalsInfo.A_AreaA6.Start_stop[i];
                //}
                //int n = 0;
                //for (int i = 8; i < 16; i++)
                //{
                //    GlobalsInfo.A_AreaA6.Sstop[2, n] = GlobalsInfo.A_AreaA6.Start_stop[i];
                //    n++;
                //}
            }


        }




        public bool _Omron_RW(string address1, string address2, int P1, int P2, int Len, ref short[] content, ref byte[] conbyte)
        {
            int Len2 = Len * 2;
            string Length = Len.ToString();     //"400";
            if (OmronHelper._read_Batch(P1, address1, Length, ref content, ref conbyte) == false)
            {
                return false;
            }
            if (address2 != "无")
            {
                //p2 = 0;
                //p1 = 1;

                //p2 = 0;
                //p1 = 2;

                //p2 = 3;
                //p1 = 4;

                //p2 = 3;
                //p1 = 5;

                //if (GlobalsInfo.SelectLQ.ABLine == "A线")
                //{
                //    Omron.ABline = 0;  //A线 0   B线3
                //}
                //else if (GlobalsInfo.SelectLQ.ABLine == "B线")
                //{
                //    Omron.ABline = 3;  //A线 0   B线3
                //}

                if ((GlobalsInfo.SelectLQ.ABLine == "A线" && P2 == 0) || GlobalsInfo.SelectLQ.ABLine == "B线" && P2 == 3)
                {
                    if (OmronHelper._write_Batch(P2, address2, conbyte) == false)
                    {
                        return false;
                    }
                }
            }
            return true;
        }


        public bool _Omron_RW2(string address1, string address2, int P1, int P2, int Len, ref short[] content, ref byte[] conbyte)
        {
            int Len2 = Len * 2;
            string Length = Len.ToString();     //"400";
            if (OmronHelper._read_Batch(P1, address1, Length, ref content, ref conbyte) == false)
            {
                return false;
            }
            if (address2 != "无")
            {
                //p2 = 0;
                //p1 = 1;

                //p2 = 0;
                //p1 = 2;

                //p2 = 3;
                //p1 = 4;

                //p2 = 3;
                //p1 = 5;

                //if (GlobalsInfo.SelectLQ.ABLine == "A线")
                //{
                //    Omron.ABline = 0;  //A线 0   B线3
                //}
                //else if (GlobalsInfo.SelectLQ.ABLine == "B线")
                //{
                //    Omron.ABline = 3;  //A线 0   B线3
                //}

                if ((GlobalsInfo.SelectLQ.ABLine == "A线" && P2 == 0) || GlobalsInfo.SelectLQ.ABLine == "B线" && P2 == 3)
                {
                    if (OmronHelper._write_Batch(P2, address2, conbyte) == false)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public void _RefShortQD(short[] content, ref short[] ref_short)
        {
            int con = 0;
            for (int i = 0; i < 16; i++)
            {
                ref_short[i] = content[con];
                con = con + 10; //20
            }
        }
        public void _RefShort(short[] content, ref short[] ref_short, int K, int f)
        {
            int con = 0;
            for (int i = 0; i < 16; i++)
            {
                ref_short[i] = content[con + f];
                con = con + K; //20
            }
        }

        public void RefString(short[] content, ref double[] ref_Str, int K, int f)
        {
            string str1 = "";
            string str2 = "";  //20  2
            int con = 0;
            for (int i = 0; i < 16; i++)
            {
                str1 = content[con + f].ToString("X4");
                str2 = content[con + f + 1].ToString("X4");

                double dou1 = Convert.ToDouble(Convert.ToInt32((str2 + str1), 16));
                ref_Str[i] = dou1 / 100;
                con = con + K; //20
            }
        }


    }
}
