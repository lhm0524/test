using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZY.WEIKE.IDAL
{
    public interface IWeiKeDAL : IDAL.IBaseDAL<MODAL.WeiKeModel>
    {
        IEnumerable<MODAL.WeiKeModel> LoadNewEntities(string where, Dictionary<string, object> dic, string order, bool isAsc);
        object AddWeiKeEntity(MODAL.WeiKeModel w, MODAL.ResourceModel res); 

        IEnumerable<MODAL.WeiKeModel> Manager_LoadPageEntities(int pageIndex, int pageSize, string where, Dictionary<string, object> dic, string order, bool IsAsc, out int totalCount);
    }
}
