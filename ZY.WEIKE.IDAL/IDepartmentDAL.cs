using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZY.WEIKE.IDAL
{
    public interface IDepartmentDAL : IDAL.IBaseDAL<MODEL.DepartmentModel>
    {
        IEnumerable<MODEL.DepartmentModel> LoadByParentID(int parentId);
    }
}
