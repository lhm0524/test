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
    }
}
