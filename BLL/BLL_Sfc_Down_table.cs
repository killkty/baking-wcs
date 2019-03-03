using System.Collections.Generic;
using System.Data;

namespace BLL
{
    public partial class BLL_Sfc_Down_table
    {
        DAL.DAL_Sfc_Down_table dal = new DAL.DAL_Sfc_Down_table();
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void AddList(List<Model.Model_Sfc_Down_table> model)
        {
            dal.AddList(model);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void UpdateList(List<Model.Model_Sfc_Down_table> model)
        {
            dal.UpdateList(model);
        }


        //删除
        public int DeleteByContactId(string strWhere)
        {
            return dal.DeleteByContactId(strWhere);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataTable GetList(Model.Model_Sfc_Down_table model)
        {
            return dal.GetList(model);
        }

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            return dal.GetRecordCount(strWhere);
        }
        //分页查询
        public DataTable GetListPage(string strWhere, int pageSize, int page)
        {
            return dal.GetListPage(strWhere, pageSize, page);
        }



        //private readonly DAL.DAL_Sfc_Down_table dal = new DAL.DAL_Sfc_Down_table();
        //public BLL_Sfc_Down_table()
        //{ }
        //#region  BasicMethod

        ///// <summary>
        ///// 增加一条数据
        ///// </summary>
        //public void AddList(List<Model.Model_Sfc_Down_table> model)
        //{
        //    dal.AddList(model);
        //}

        ///// <summary>
        ///// 删除一条数据
        ///// </summary>
        //public bool Delete(string SfcTest)
        //{
        //    return dal.Delete(SfcTest);
        //}

        ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        //public DataTable GetList(string strWhere)
        //{
        //    return dal.GetList(strWhere);
        //}

        ///// <summary>
        ///// 得到一个对象实体
        ///// </summary>
        //public Model.Model_Sfc_Down_table GetModel(string ID)
        //{
        //    return dal.GetModel(ID);
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        //public bool CheckLogin(string ID) //1
        //{
        //    Model.Model_Sfc_Down_table mOper = new Model.Model_Sfc_Down_table();
        //    mOper = this.GetModel(ID);
        //    if (mOper == null)
        //    {
        //        return false;
        //    }
        //    return true;
        //}


        ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        //public List<Model.Model_Sfc_Down_table> DataTableToList(DataTable dt)
        //{
        //    List<Model.Model_Sfc_Down_table> modelList = new List<Model.Model_Sfc_Down_table>();
        //    int rowsCount = dt.Rows.Count;
        //    if (rowsCount > 0)
        //    {
        //        Model.Model_Sfc_Down_table model;
        //        for (int n = 0; n < rowsCount; n++)
        //        {
        //            model = dal.DataRowToModel(dt.Rows[n]);
        //            if (model != null)
        //            {
        //                modelList.Add(model);
        //            }
        //        }
        //    }
        //    return modelList;
        //}

        ///// <summary>
        ///// 分页获取数据列表
        ///// </summary>
        //public int GetRecordCount(string strWhere)
        //{
        //    return dal.GetRecordCount(strWhere);
        //}
        //#endregion  BasicMethod
    }
}
