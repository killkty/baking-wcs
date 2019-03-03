using Model;
using System.Collections.Generic;

namespace GlobalsInfo
{
    public static class Global_DAQ_processLotArray
    {
        public static int Id { get; set; }

        public static string name { get; set; }

        public static string dataType { get; set; }

        public static string trayValue { get; set; }

        public static List<Model_DAQ_processLotArray> list { get; set; }
    }
}
