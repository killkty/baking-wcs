using System.Collections.Generic;
using System.Data;

namespace BLL
{
    /// <summary>
    /// Category
    /// </summary>
    public partial class BLL_tb_Login
    {
        private readonly DAL.DAL_tb_Login dal = new DAL.DAL_tb_Login();
        public BLL_tb_Login()
        { }
        /// <summary>
        /// 增加一条数据 
        /// </summary>
        public bool Add(Model.Model_tb_Login model)//***
        {
            return dal.Add(model);
        }
        /// <summary>
        /// 更新一条数据 UpdatePWD
        /// </summary>
        public bool UpdatePWD(Model.Model_tb_Login model)
        {
            return dal.UpdatePWD(model);
        }
        /// <summary>
        /// 更新一条数据 
        /// </summary>
        public bool Update(Model.Model_tb_Login model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int Id)
        {

            return dal.Delete(Id);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string Idlist)
        {
            return dal.DeleteList(Idlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.Model_tb_Login GetModel(string name)//***
        {
            return dal.GetModel(name);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataTable GetList()//***
        {
            return dal.GetList();
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataTable GetList(string strWhere)//***
        {
            return dal.GetList(strWhere);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Model.Model_tb_Login> GetModelList(string strWhere)
        {
            DataTable ds = dal.GetList();
            return DataTableToList(ds);
        }

        /// <summary>
        /// 验证用户登录
        /// </summary>
        /// <param name="Userid"></param>
        /// <param name="Pwd"></param>
        /// <returns></returns>
        public bool CheckUser(string name, string password) //***
        {
            Model.Model_tb_Login mOper = new Model.Model_tb_Login();
            mOper = this.GetModel(name);
            if (mOper == null)
            {
                return false;
            }
            if (password != mOper.password)
            {
                return false;
            }
            GlobalsInfo.tb_Login.Level = mOper.Level.ToString();
            GlobalsInfo.tb_Login.Account = mOper.Account.ToString();
            GlobalsInfo.tb_Login.name = mOper.name.ToString();
            return true;
        }

        /// <summary>
        /// 修改验证用户登录
        /// </summary>
        /// <param name="Userid"></param>
        /// <param name="Pwd"></param>
        /// <returns></returns>
        public bool EditUser(string name, string password) //***
        {
            Model.Model_tb_Login mOper = new Model.Model_tb_Login();
            mOper = this.GetModel(name);
            if (mOper == null)
            {
                return false;
            }
            if (password != mOper.password)
            {
                return false;
            }
            //GlobalsInfo.tb_Login.ID = mOper.ID;
            return true;
        }




        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Model.Model_tb_Login> DataTableToList(DataTable dt)
        {
            List<Model.Model_tb_Login> modelList = new List<Model.Model_tb_Login>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Model.Model_tb_Login model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = dal.DataRowToModel(dt.Rows[n]);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataTable GetAllList()
        {
            return GetList();
        }

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            return dal.GetRecordCount(strWhere);
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataTable GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }



    }
}
