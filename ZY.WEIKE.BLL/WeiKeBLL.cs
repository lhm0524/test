using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZY.WEIKE.BLL
{
    public class WeiKeBLL
    {
        private IDAL.IWeiKeDAL dal;

        public WeiKeBLL()
        {
            dal = DALFACTORY.AbstractFactory.CreateWeiKeDALInstance();
        }

        /// <summary>
        /// 返回一系列的数据
        /// </summary>
        /// <param name="where">条件</param>
        /// <param name="order">按照某个字段排序</param>
        /// <param name="dic">参数集合</param>
        /// <param name="isAsc">是否是升序排序</param>
        /// <param name="type">类型</param>
        /// <returns>IEnumerable枚举</returns>
        public IEnumerable<MODEL.WeiKeModel> LoadEntities(string where, string order, Dictionary<string, object> dic, bool isAsc, int type)
        {
            if (type == 1)
            {
                //加载最新 最热
                return dal.LoadNewEntities(where, dic, order, isAsc);
            }
            return dal.LoadEntities(where, dic, order, isAsc);
        }

        public IEnumerable<MODEL.WeiKeModel> LoadPageEntities(int pageIndex, int pageSize, out int totalCount, string whereLambda, Dictionary<string,object> dic,string order, bool isAsc)
        {
            return dal.LoadPageEntities(pageIndex, pageSize, out totalCount, whereLambda, dic, order, isAsc);
        }

        public int GetAllPageCountForWhere(string where, Dictionary<string, object> dic)
        {
            return dal.GetAllPageCountForWhere(where, dic);
        }

        public MODEL.WeiKeModel GetModelByPrimaryKey(int id)
        {
            return dal.GetModelByPrimaryKey(id);
        }
        public IEnumerable<MODEL.WeiKeModel> Manager_LoadPageEntities(int pageIndex, int pageSize, string where, Dictionary<string, object> dic, string order, bool IsAsc, out int totalCount)
        {
            return dal.Manager_LoadPageEntities(pageIndex, pageSize, where,  dic, order, IsAsc, out totalCount);
        }

        public bool DeleteModelByPrimaryKey(int primarykey)
        {
            return dal.DeleteModelByPrimaryKey(primarykey) > 0;
        }

        public bool CreateEntity(MODEL.WeiKeModel w, MODEL.ResourceModel res)
        {
            return (int)dal.AddWeiKeEntity(w, res) > 1;
        }
    }
}
