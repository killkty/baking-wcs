using System;

namespace Model
{
    public partial class  Model_Sfc_Down_table
    {
        private int _ID;                //1,ID

        private string _BkTimeEnd;   //2,BK结束时间
        private string _Sfc;            //3,sfc电芯条码
        private string _SfcTest;        //4,测试条码
        private string _Position;       //5,条码 位置

        private string _MesTime;     //7,上传Mes时间
        private string _MesFlag;        //7,上传Mes标志

        public int ID
        {
            set { _ID = value; }
            get { return _ID; }
        }

        /// <summary>
        /// BK结束时间
        /// </summary>
        public string BkTimeEnd
        {
            set { _BkTimeEnd = value; }
            get { return _BkTimeEnd; }
        }

        /// <summary>
        /// sfc电芯条码
        /// </summary>
        public string Sfc
        {
            set { _Sfc = value; }
            get { return _Sfc; }
        }

        /// <summary>
        /// 托盘条码
        /// </summary>
        public string SfcTest
        {
            set { _SfcTest = value; }
            get { return _SfcTest; }
        }

        /// <summary>
        /// 条码 位置
        /// </summary>
        public string Position
        {
            set { _Position = value; }
            get { return _Position; }
        }

        /// <summary>
        /// 上传Mes时间
        /// </summary>
        public string MesTime
        {
            set { _MesTime = value; }
            get { return _MesTime; }
        }

        /// <summary>
        /// 上传Mes标志
        /// </summary>
        public string MesFlag
        {
            set { _MesFlag = value; }
            get { return _MesFlag; }
        }

    }
}
