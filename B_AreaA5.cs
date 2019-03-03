namespace GlobalsInfo
{
    public class B_AreaA5
    {
        public static short[] vacuo_Alarm1 = new short[16];        //真空异常报警  
        public static short[] vacuo_Alarm2 = new short[16];        //破真空异常报警
        public static double[] vacuo_Alarm3 = new double[16];            //真空报警真空值

        public static bool[,] exceed_Alarm = new bool[16, 12];       //超温报警1-12    bool[12,16]; 
        public static bool[,] low_Alarm = new bool[16, 12];          //低温报警1-12
        public static bool[,] difference_Alarm = new bool[16, 12];   //温差报警1-12
        public static bool[,] Exception_Alarm = new bool[16, 12];    //信号异常报警1-12



    }
}
