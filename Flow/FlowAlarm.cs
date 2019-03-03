using GlobalsInfo;
using System;
using System.Threading;

namespace Flow
{
    public class FlowAlarm
    {
        int tim_Alarm = 50;        //3秒
        public void _tim_Alarm()   // 
        {
            for (int i = 0; i < tim_Alarm; i++)
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
                AlarmCall(0);
                _tim_Alarm();
            }
            while (Omron.STOP == false);
        }

        bool[,,] plcA_exceed_Alarm = new bool[4, 8, 12];
        bool[,,] plcA_low_Alarm = new bool[4, 8, 12];
        bool[,,] plcA_difference_Alarm = new bool[4, 8, 12];
        bool[,,] plcA_Exception_Alarm = new bool[4, 8, 12];

        bool[,,] plcB_exceed_Alarm = new bool[4, 8, 12];
        bool[,,] plcB_low_Alarm = new bool[4, 8, 12];
        bool[,,] plcB_difference_Alarm = new bool[4, 8, 12];
        bool[,,] plcB_Exception_Alarm = new bool[4, 8, 12];


        bool[,] plcA_vacuo_Alarm1 = new bool[4, 8];
        bool[,] plcA_vacuo_Alarm2 = new bool[4, 8];

        bool[,] plcB_vacuo_Alarm1 = new bool[4, 8];
        bool[,] plcB_vacuo_Alarm2 = new bool[4, 8];

        //容置腔A1-1(下层)
        public string WriteA_name(int j, int i)    //写入
        {
            string name = "";
            if (j == 0)
            {
                name = "容置腔A1-" + (i + 1).ToString() + "(下层)";

            }
            else if (j == 1)
            {
                name = "容置腔A1-" + (i + 5).ToString() + "(上层)";

            }
            else if (j == 2)
            {
                name = "容置腔A2-" + (i + 1).ToString() + "(下层)";
            }
            else if (j == 3)
            {
                name = "容置腔A2-" + (i + 5).ToString() + "(上层)";
            }
            return name;
        }
        public string WriteB_name(int j, int i)    //写入
        {
            string name = "";
            if (j == 0)
            {
                name = "容置腔B1-" + (i + 1).ToString() + "(下层)";

            }
            else if (j == 1)
            {
                name = "容置腔B1-" + (i + 5).ToString() + "(上层)";

            }
            else if (j == 2)
            {
                name = "容置腔B2-" + (i + 1).ToString() + "(下层)";
            }
            else if (j == 3)
            {
                name = "容置腔B2-" + (i + 5).ToString() + "(上层)";
            }
            return name;
        }
        private void insertion_Alarm_Oven(string Data_code, string Data_Description, string Area)
        {
            Model.Model_Alarm_oven model = new Model.Model_Alarm_oven();
            model.Time_A = DateTime.Now.ToLocalTime(); //DateTime.Now.ToLocalTime();
            model.Type_Code = "C03";
            model.Type_Description = "容置腔";
            model.Data_code = Data_code;
            model.Data_Description = Data_Description;
            model.Register = "PLC";
            model.Area = Area;

            DAL.DAL_Alarm_oven bll = new DAL.DAL_Alarm_oven();
            bll.Insert(model);
        }

        public void AlarmCall(int K)
        {
            for (var j = 0; j < 4; j++)
            {
                for (var i = 0; i < 4; i++)
                {
                    if (GlobalsInfo.A_AreaA5.plc_vacuo_Alarm1[j, i] == 1 && plcA_vacuo_Alarm1[j, i] == false)
                    {
                        //真空异常报警
                        plcA_vacuo_Alarm1[j, i] = true;
                        string name = WriteA_name(j,i);
                        insertion_Alarm_Oven("BK01", "真空异常报警", name);
                        GlobalsInfo.Message.Red(name + "--BK01--" + "真空异常报警");
                    }
                    else if (GlobalsInfo.A_AreaA5.plc_vacuo_Alarm1[j, i] != 1 && plcA_vacuo_Alarm1[j, i] == true)
                    {
                        plcA_vacuo_Alarm1[j, i] = false;
                    }

                    if (GlobalsInfo.A_AreaA5.plc_vacuo_Alarm2[j, i] == 1 && plcA_vacuo_Alarm2[j, i] == false)
                    {
                        //破真空异常报警
                        plcA_vacuo_Alarm2[j, i] = true;
                        string name = WriteA_name(j, i);
                        insertion_Alarm_Oven("BK02", "破真空异常报警", name);
                        GlobalsInfo.Message.Red(name + "--BK02--" + "破真空异常报警");
                    }
                    else if (GlobalsInfo.A_AreaA5.plc_vacuo_Alarm2[j, i] != 1 && plcA_vacuo_Alarm2[j, i] == true)
                    {
                        plcA_vacuo_Alarm2[j, i] = false;
                    }

                    for (var s = 0; s < 12; s++)
                    {
                        if (GlobalsInfo.A_AreaA5.plc_exceed_Alarm[j, i, s] == true && plcA_exceed_Alarm[j, i, s] == false)
                        {
                            //超温报警1
                            plcA_exceed_Alarm[j, i, s] = true;
                            string name = WriteA_name(j, i);
                            insertion_Alarm_Oven("BK03", "超温报警", name);
                            GlobalsInfo.Message.Red(name + "--BK03--" + "超温报警");
                        }
                        else if (GlobalsInfo.A_AreaA5.plc_exceed_Alarm[j, i, s] == false && plcA_exceed_Alarm[j, i, s] == true)
                        {
                            plcA_exceed_Alarm[j, i, s] = false;
                        }

                        if (GlobalsInfo.A_AreaA5.plc_low_Alarm[j, i, s] == true && plcA_low_Alarm[j, i, s] == false)
                        {
                            //低温报警1
                            plcA_low_Alarm[j, i, s] = true;
                            string name = WriteA_name(j, i);
                            insertion_Alarm_Oven("BK04", "低温报警", name);
                            GlobalsInfo.Message.Red(name + "--BK04--" + "低温报警");

                        }
                        else if (GlobalsInfo.A_AreaA5.plc_low_Alarm[j, i, s] == false && plcA_low_Alarm[j, i, s] == true)
                        {
                            plcA_low_Alarm[j, i, s] = false;
                        }

                        if (GlobalsInfo.A_AreaA5.plc_difference_Alarm[j, i, s] == true && plcA_difference_Alarm[j, i, s] == false)
                        {
                            //温差报警1
                            plcA_difference_Alarm[j, i, s] = true;
                            string name = WriteA_name(j, i);
                            insertion_Alarm_Oven("BK05", "温差报警", name);
                            GlobalsInfo.Message.Red(name + "--BK05--" + "温差报警");
                        }
                        else if (GlobalsInfo.A_AreaA5.plc_difference_Alarm[j, i, s] == false && plcA_difference_Alarm[j, i, s] == true)
                        {
                            plcA_difference_Alarm[j, i, s] = false;
                        }

                        if (GlobalsInfo.A_AreaA5.plc_Exception_Alarm[j, i, s] == true && plcA_Exception_Alarm[j, i, s] == false)
                        {
                            //信号异常报警1
                            plcA_Exception_Alarm[j, i, s] = true;
                            string name = WriteA_name(j, i);
                            insertion_Alarm_Oven("BK06", "信号异常报警", name);
                            GlobalsInfo.Message.Red(name + "--BK06--" + "信号异常报警");
                        }
                        else if (GlobalsInfo.A_AreaA5.plc_Exception_Alarm[j, i, s] == false && plcA_Exception_Alarm[j, i, s] == true)
                        {
                            plcA_Exception_Alarm[j, i, s] = false;
                        }
                    }

                    //******
                    if (GlobalsInfo.B_AreaA5.plc_vacuo_Alarm1[j, i] == 1 && plcB_vacuo_Alarm1[j, i] == false)
                    {
                        //真空异常报警
                        plcB_vacuo_Alarm1[j, i] = true;
                        string name = WriteB_name(j, i);
                        insertion_Alarm_Oven("BK01", "真空异常报警", name);
                        GlobalsInfo.Message.Red(name + "--BK01--" + "真空异常报警");
                    }
                    else if (GlobalsInfo.B_AreaA5.plc_vacuo_Alarm1[j, i] != 1 && plcB_vacuo_Alarm1[j, i] == true)
                    {
                        plcB_vacuo_Alarm1[j, i] = false;
                    }

                    if (GlobalsInfo.B_AreaA5.plc_vacuo_Alarm2[j, i] == 1 && plcB_vacuo_Alarm2[j, i] == false)
                    {
                        //破真空异常报警
                        plcB_vacuo_Alarm2[j, i] = true;
                        string name = WriteB_name(j, i);
                        insertion_Alarm_Oven("BK02", "破真空异常报警", name);
                        GlobalsInfo.Message.Red(name + "--BK02--" + "破真空异常报警");
                    }
                    else if (GlobalsInfo.B_AreaA5.plc_vacuo_Alarm2[j, i] != 1 && plcB_vacuo_Alarm2[j, i] == true)
                    {
                        plcB_vacuo_Alarm2[j, i] = false;
                    }

                    for (var s = 0; s < 12; s++)
                    {
                        if (GlobalsInfo.B_AreaA5.plc_exceed_Alarm[j, i, s] == true && plcB_exceed_Alarm[j, i, s] == false)
                        {
                            //超温报警1
                            plcB_exceed_Alarm[j, i, s] = true;
                            string name = WriteB_name(j, i);
                            insertion_Alarm_Oven("BK03", "超温报警", name);
                            GlobalsInfo.Message.Red(name + "--BK03--" + "超温报警");

                        }
                        else if (GlobalsInfo.B_AreaA5.plc_exceed_Alarm[j, i, s] == false && plcB_exceed_Alarm[j, i, s] == true)
                        {
                            plcB_exceed_Alarm[j, i, s] = false;
                        }

                        if (GlobalsInfo.B_AreaA5.plc_low_Alarm[j, i, s] == true && plcB_low_Alarm[j, i, s] == false)
                        {
                            //低温报警1
                            plcB_low_Alarm[j, i, s] = true;
                            string name = WriteB_name(j, i);
                            insertion_Alarm_Oven("BK04", "低温报警", name);
                            GlobalsInfo.Message.Red(name + "--BK04--" + "低温报警");

                        }
                        else if (GlobalsInfo.B_AreaA5.plc_low_Alarm[j, i, s] == false && plcB_low_Alarm[j, i, s] == true)
                        {
                            plcB_low_Alarm[j, i, s] = false;
                        }

                        if (GlobalsInfo.B_AreaA5.plc_difference_Alarm[j, i, s] == true && plcB_difference_Alarm[j, i, s] == false)
                        {
                            //温差报警1
                            plcB_difference_Alarm[j, i, s] = true;
                            string name = WriteB_name(j, i);
                            insertion_Alarm_Oven("BK05", "温差报警", name);
                            GlobalsInfo.Message.Red(name + "--BK05--" + "温差报警");

                        }
                        else if (GlobalsInfo.B_AreaA5.plc_difference_Alarm[j, i, s] == false && plcB_difference_Alarm[j, i, s] == true)
                        {
                            plcB_difference_Alarm[j, i, s] = false;
                        }

                        if (GlobalsInfo.B_AreaA5.plc_Exception_Alarm[j, i, s] == true && plcB_Exception_Alarm[j, i, s] == false)
                        {
                            //信号异常报警1
                            plcB_Exception_Alarm[j, i, s] = true;
                            string name = WriteB_name(j, i);
                            insertion_Alarm_Oven("BK06", "信号异常报警", name);
                            GlobalsInfo.Message.Red(name + "--BK06--" + "信号异常报警");
                        }
                        else if (GlobalsInfo.B_AreaA5.plc_Exception_Alarm[j, i, s] == false && plcB_Exception_Alarm[j, i, s] == true)
                        {
                            plcB_Exception_Alarm[j, i, s] = false;
                        }
                    }
                }
            }


        }
    }
}
