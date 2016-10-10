using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZY.WEIKE.IDAL
{
    public interface IBaseDAL<T> where T : class, new()
    {
        /// <summary>
        /// 从数据源中删除某条数据
        /// </summary>
        /// <param name="primartyKey">主键值</param>
        /// <returns>返回影响条数</returns>
        int DeleteModelByPrimaryKey(int primartyKey);

        /// <summary>
        /// 向数据源中新增实例
        /// </summary>
        /// <param name="t">实例</param>
        /// <returns>返回影响条数</returns>
        int CreateEntity(T t);

        /// <summary>
        /// 修改某条实例
        /// </summary>
        /// <param name="t">实例</param>
        /// <returns>影响条数</returns>
        int EditEntity(T t);

        /// <summary>
        /// 得到实例列表
        /// </summary>
        /// <param name="where">条件</param>
        /// <param name="dic">sql参数</param>
        /// <returns>集合</returns>
        IEnumerable<T> LoadEntities(string where, Dictionary<string, object> dic, string order, bool isAsc);

        /// <summary>
        /// 由主键值获得实例
        /// </summary>
        /// <param name="primaryKey">主键值</param>
        /// <returns>实例</returns>
        T GetModelByPrimaryKey(int primaryKey);

        /// <summary>
        /// 获得分页的总页数
        /// </summary>
        /// <param name="where">条件</param>
        /// <param name="dic">参数</param>
        /// <returns>总页数</returns>
        int GetAllPageCountForWhere(string where, Dictionary<string, object> dic);

        IEnumerable<T> LoadPageEntities(int pageIndex, int pageSize, out int totalCount, string whereLambda, Dictionary<string, object> dic, string order, bool isAsc);

        T GetEntity(string where, Dictionary<string, object> dic);
    }
}
