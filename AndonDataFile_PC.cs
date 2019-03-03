namespace GlobalsInfo
{
    public static class AndonDataFile_PC
    {
        public static string Status { get; set; }  // addressPort  MES交互信息路径    1 dbPath  + "Port" +"接口" 作为 Port下共9个文件  接口下每天一个txt文件 本机写入的 txt数据  2 选择的路径 +"接口名称" 文件夹作为 客户观看的csv文件
        public static string Alarm { get; set; }   //   
        public static string Output { get; set; }   //
        public static string Light { get; set; }   //

        public static string Heart { get; set; }   //
        public static string Control { get; set; }    //
        public static string PPM { get; set; }

    }
}
