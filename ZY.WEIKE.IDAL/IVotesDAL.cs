using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZY.WEIKE.IDAL
{
    public interface IVotesDAL : IBaseDAL<MODAL.VotesModel>
    {
        List<int> GetRatingCount(string where, Dictionary<string, object> dic);
    }
}
