using System.Collections.Generic;
using System.Data;

namespace BLL
{
    public class BLL_SFC_Water
    {
        DAL.DAL_SFC_Water dal = new DAL.DAL_SFC_Water();
        //插入操作
        public int Insert(Model.Model_SFC_Water model)
        {
            return dal.Insert(model);
        }

        ////删除
        //public int DeleteByContactId(string strWhere)
        //{
        //    return dal.DeleteByContactId(strWhere);
        //}
        ///// <summary>
        ///// 分页获取数据列表
        ///// </summary>
        //public int GetRecordCount(string strWhere)
        //{
        //    return dal.GetRecordCount(strWhere);
        //}
        ////分页查询
        //public DataTable GetListPage(string strWhere, int pageSize, int page)
        //{
        //    return dal.GetListPage(strWhere, pageSize, page);
        //}

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void AddList(List<Model.Model_SFC_Water> model)
        {
            dal.AddList(model);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void UpdateList(List<Model.Model_SFC_Water> model)
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
        public DataTable GetList(Model.Model_SFC_Water model)
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


    }
}
