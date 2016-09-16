using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZY.WEIKE.IDAL
{
    public interface IColleageDAL : IBaseDAL<MODAL.ColleageModel>
    {
        IEnumerable<MODAL.ColleageModel> GetTopList(int top);
    }
}
