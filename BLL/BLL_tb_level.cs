using System.Collections.Generic;
using System.Data;

namespace BLL
{
    /// <summary>
    /// Category
    /// </summary>
    public partial class BLL_tb_Level
    {
        private readonly DAL.DAL_tb_Level dal = new DAL.DAL_tb_Level();
        public BLL_tb_Level()
        { }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Model.Model_tb_Level model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.Model_tb_Level model)
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
        public Model.Model_tb_Level GetModel(string _Account)//***
        {
            return dal.GetModel(_Account);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataTable GetList()
        {
            return dal.GetList();
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataTable Get_Account()
        {
            return dal.Get_Account();
        }
        /// <summary>
        /// 获得数据列表 
        /// </summary>
        public DataTable GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Model.Model_tb_Level> GetModelList(string strWhere)
        {
            DataTable ds = dal.GetList(strWhere);
            return DataTableToList(ds);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Model.Model_tb_Level> DataTableToList(DataTable dt)
        {
            List<Model.Model_tb_Level> modelList = new List<Model.Model_tb_Level>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Model.Model_tb_Level model;
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
            return GetList("");
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
