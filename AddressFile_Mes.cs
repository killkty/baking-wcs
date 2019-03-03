namespace GlobalsInfo
{
    public static class AddressFile_Mes
    {
        public static string Port { get; set; }  // addressPort  MES交互信息路径    1 dbPath  + "Port" +"接口" 作为 Port下共9个文件  接口下每天一个txt文件 本机写入的 txt数据  2 选择的路径 +"接口名称" 文件夹作为 客户观看的csv文件
        public static string Sfc { get; set; }   //   
        public static string Tray { get; set; }   //
        public static string Temp { get; set; }   //


        public static string Run { get; set; }   //
        public static string MesCsv { get; set; }    //
        public static string Water { get; set; }

        public static string dbPath { get; set; } //数据
        public static string database { get; set; } //数据

        public static string Alarm_String { get; set; }
        public static string BK_String { get; set; }

    }
}
