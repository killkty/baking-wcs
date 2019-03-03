using System;

namespace Model
{
    public class Model_Alarm_oven
    {
        private DateTime? _Time_A;
        private string _Type_Code;
        private string _Type_Description;
        private string _data_code;
        private string _data_Description;
        private string _Register;
        private string _Area;

        public DateTime? Time_A
        {
            set { _Time_A = value; }
            get { return _Time_A; }
        }
        public string Type_Code { get => _Type_Code; set => _Type_Code = value; }
        public string Type_Description { get => _Type_Description; set => _Type_Description = value; }
        public string Data_code { get => _data_code; set => _data_code = value; }
        public string Data_Description { get => _data_Description; set => _data_Description = value; }
        public string Register { get => _Register; set => _Register = value; }
        public string Area { get => _Area; set => _Area = value; }
    }
}
