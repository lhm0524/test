using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZY.WEIKE.IDAL
{
    public interface IResourceDAL : IDAL.IBaseDAL<MODEL.ResourceModel>
    {
        string GetImgPath(int weikeid);
    }
}
