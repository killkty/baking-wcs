using System.Data;

namespace BLL
{
    public class BLL_Alarm_oven
    {
        DAL.DAL_Alarm_oven dal = new DAL.DAL_Alarm_oven();
        //插入操作
        public int Insert(Model.Model_Alarm_oven model)
        {
            return dal.Insert(model);
        }

        //删除
        public int DeleteByContactId(string strWhere)
        {
            return dal.DeleteByContactId(strWhere);
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
