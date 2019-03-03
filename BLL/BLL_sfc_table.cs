using System.Collections.Generic;
using System.Data;

namespace BLL
{
    /// <summary>
    /// Category
    /// </summary>
    public partial class BLL_Sfc_table
    {
        private readonly DAL.DAL_Sfc_table dal = new DAL.DAL_Sfc_table();
        public BLL_Sfc_table()
        { }
        #region  BasicMethod

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void AddList(List<Model.Model_Sfc_table> model)
        {
             dal.AddList(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void UpdateList(List<Model.Model_Sfc_table> model)
        {
             dal.UpdateList(model);
        }
       

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.Model_Sfc_table GetModel(string ID)
        {
            return dal.GetModel(ID);
        }

        /// <summary>
        /// 
        /// </summary>
        public bool CheckLogin(string ID) //1
        {
            Model.Model_Sfc_table mOper = new Model.Model_Sfc_table();
            mOper = this.GetModel(ID);
            if (mOper == null)
            {
                return false;
            }
            return true;
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
        public DataTable GetPosition(string strWhere,string Position)
        {
            return dal.GetPosition(strWhere, Position);
        }



        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Model.Model_Sfc_table> GetModelList(string strWhere)
        {
            DataTable ds = dal.GetList(strWhere);
            return DataTableToList(ds);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Model.Model_Sfc_table> DataTableToList(DataTable dt)
        {
            List<Model.Model_Sfc_table> modelList = new List<Model.Model_Sfc_table>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Model.Model_Sfc_table model;
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
        #endregion  BasicMethod





    }
}
