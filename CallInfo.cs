namespace GlobalsInfo
{
    public class CallInfo  //发送  接收
    {
        public static string _file = "";          //
        public static int ID { get; set; }   

        //异常报警 事件
        public static void Connect(string Str)  //
        {
            string[] Info = new string[2];
            Info[0] = Str;
            Info[1] = "true";
            Fault(Info);
        }
        public static void Green(string Str, string info,string FilePath_A,string FilePath_B, string dt)  //
        {
            string[] Info = new string[6];
            Info[0] = Str;
            Info[1] = info;
            Info[2] = "false";
            Info[3] = FilePath_A;
            Info[4] = FilePath_B;
            Info[5] = dt;

            Fault(Info);
        }
        public static void Red(string Str, string info,string FilePath_A, string FilePath_B, string dt)  //
        {
            string[] Info = new string[6];
            Info[0] = Str;
            Info[1] = info;
            Info[2] = "true";
            Info[3] = FilePath_A;
            Info[4] = FilePath_B;
            Info[5] = dt;
            Fault(Info);
        }
        public delegate void FFrmMainHandle(string[] str);
        public static event FFrmMainHandle Fault_;

        public static void Fault(string[] Str)
        {
            Fault_(Str);
        }


    }
}
