using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZY.WEIKE.IDAL
{
    public interface IUserLogDAL : IBaseDAL<MODEL.UserLogModel>
    {
        int AddPlayCount(int userid, int weikeid);
        int AddSurfaceCount(int userid, int weikeid);
    }
}
