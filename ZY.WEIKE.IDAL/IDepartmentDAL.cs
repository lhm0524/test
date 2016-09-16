using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZY.WEIKE.IDAL
{
    public interface IDepartmentDAL : IDAL.IBaseDAL<MODAL.DepartmentModel>
    {
        IEnumerable<MODAL.DepartmentModel> LoadByParentID(int parentId);
    }
}
