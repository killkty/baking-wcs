namespace GlobalsInfo
{
    public class B_Area5
    {
        public static short[,] plc_vacuo_Alarm1 = new short[4, 8];        //真空异常报警  
        public static short[,] plc_vacuo_Alarm2 = new short[4, 8];        //破真空异常报警
        public static double[,] plc_vacuo_Alarm3 = new double[4, 8];       //真空报警真空值

        public static bool[,,] plc_exceed_Alarm = new bool[4, 8, 12];       //超温报警1-12    bool[12,16]; 
        public static bool[,,] plc_low_Alarm = new bool[4, 8, 12];          //低温报警1-12
        public static bool[,,] plc_difference_Alarm = new bool[4, 8, 12];   //温差报警1-12
        public static bool[,,] plc_Exception_Alarm = new bool[4, 8, 12];    //信号异常报警1-12

    }
}
