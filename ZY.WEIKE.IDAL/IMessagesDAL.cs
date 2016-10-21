using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZY.WEIKE.IDAL
{
    public interface IMessagesDAL : IBaseDAL<MODEL.MessagesModel>
    {
        MODEL.MessagesModel GetMsg(string where, MODEL.DoNetParameter dic, string order, bool isAsc);
    }
}
