namespace Model
{
    public  class Model_tb_Login  //类别
    {
        //级别 Level,账号 Account,密码 password,名称 name,状态 state
        //public Model_tb_Login()
        //{ }
        private int _id;
        private  string _Level;
        private string _Account;
        private string _password;
        private string _name;
        private string _state;
        /// <summary>
        /// ID
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 级别
        /// </summary>
        public string Level
        {
            set { _Level = value; }
            get { return _Level; }
        }
        /// <summary>
        /// 账号
        /// </summary>
        public string Account
        {
            set { _Account = value; }
            get { return _Account; }
        }

        /// <summary>
        /// 密码
        /// </summary>
        public string password
        {
            set { _password = value; }
            get { return _password; }
        }

        /// <summary>
        /// 名称
        /// </summary>
        public string name
        {
            set { _name = value; }
            get { return _name; }
        }

        /// <summary>
        /// 状态
        /// </summary>
        public string state
        {
            set { _state = value; }
            get { return _state; }
        }

    }

}
