namespace Model
{
    public  class Model_Sfc_table  //类别
    {
        private int _ID;         //ID
        private string _Sfc;     //sfc电芯条码
        private string _TrayID;  //托盘条码
        private string _Position;//绑定位置

        //1序列号，2电芯条码，3托盘代码，4托盘排数，5绑定位置，6托盘条码，7绑定时间，8低温标识
        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _ID = value; }
            get { return _ID; }
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
        public string TrayID
        {
            set { _TrayID = value; }
            get { return _TrayID; }
        }

        /// <summary>
        /// 绑定位置
        /// </summary>
        public string Position
        {
            set { _Position = value; }
            get { return _Position; }
        }

    }

}
