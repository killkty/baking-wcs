namespace Model
{
    public  class Model_tb_Level  //类别
    {
        //级别名称 _Account,设置A LevelA,设置B,设置C,设置D,设置E,设置F
        //public Model_tb_level()
        //{ }

        #region Model
        private int _id;
        private string _Account_;
        private int _LevelA;
        private int _LevelB;
        private int _LevelC;
        private int _LevelD;
        private int _LevelE;
        private int _LevelF;
        /// <summary>
        /// ID
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 级别名称
        /// </summary>
        public string _Account
        {
            set { _Account_ = value; }
            get { return _Account_; }
        }

        /// <summary>
        /// 设置
        /// </summary>
        public int LevelA
        {
            set { _LevelA = value; }
            get { return _LevelA; }
        }

        /// <summary>
        /// 设置
        /// </summary>
        public int LevelB
        {
            set { _LevelB = value; }
            get { return _LevelB; }
        }

        /// <summary>
        /// 设置
        /// </summary>
        public int LevelC
        {
            set { _LevelC = value; }
            get { return _LevelC; }
        }
        /// <summary>
        /// 设置
        /// </summary>
        public int LevelD
        {
            set { _LevelD = value; }
            get { return _LevelD; }
        }
        /// <summary>
        /// 设置
        /// </summary>
        public int LevelE
        {
            set { _LevelE = value; }
            get { return _LevelE; }
        }
        /// <summary>
        /// 设置
        /// </summary>
        public int LevelF
        {
            set { _LevelF = value; }
            get { return _LevelF; }
        }
        #endregion Model
    }
}
