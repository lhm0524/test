using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZY.WEIKE.IDAL
{
    public interface IColleageDAL : IBaseDAL<MODEL.ColleageModel>
    {
        IEnumerable<MODEL.ColleageModel> GetTopList(int top);
    }
}
