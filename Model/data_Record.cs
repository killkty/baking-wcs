using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Serializable]
    public class data_Record
    {
        public string UP { get; set; }
        public string Down { get; set; }

        public string UP_NG { get; set; }
        public string Down_NG { get; set; }
    }
}
