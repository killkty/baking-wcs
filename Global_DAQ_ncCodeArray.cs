using Model;
using System.Collections.Generic;

namespace GlobalsInfo
{
    public static class Global_DAQ_ncCodeArray
    {
        public static  int Id { get; set; }

        public static string ncCode { get; set; }

        public static string trayHasNc { get; set; }

        public static List<Model_DAQ_ncCodeArray> list { get; set; }
    }
}
