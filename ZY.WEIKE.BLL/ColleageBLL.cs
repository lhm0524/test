using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZY.WEIKE.BLL
{
    public class ColleageBLL
    {
        private IDAL.IColleageDAL dal;
        public ColleageBLL()
        {
            dal = DALFACTORY.AbstractFactory.CreateColleageDALInstance();
        }
        public IEnumerable<MODEL.ColleageModel> GetTopList(int top)
        {
            return dal.GetTopList(top);
        }
        public IEnumerable<MODEL.ColleageModel> LoadEntites(string where, Dictionary<string, object> dic, string order, bool isAsc)
        {
            return dal.LoadEntities(where, dic, order, isAsc);
        }
    }
}
