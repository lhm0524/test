using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZY.WEIKE.IDAL
{
    public interface IWeiKeDAL : IDAL.IBaseDAL<MODEL.WeiKeModel>
    {
        IEnumerable<MODEL.WeiKeModel> LoadNewEntities(string where, Dictionary<string, object> dic, string order, bool isAsc);
        object AddWeiKeEntity(MODEL.WeiKeModel w, MODEL.ResourceModel res); 

        IEnumerable<MODEL.WeiKeModel> Manager_LoadPageEntities(int pageIndex, int pageSize, string where, Dictionary<string, object> dic, string order, bool IsAsc, out int totalCount);
    }
}
