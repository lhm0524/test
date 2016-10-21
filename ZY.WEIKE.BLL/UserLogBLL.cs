using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZY.WEIKE.BLL
{
    public class UserLogBLL
    {
        private IDAL.IUserLogDAL dal;

        public UserLogBLL()
        {
            dal = DALFACTORY.AbstractFactory.CreateUserLogInstance();
        }
        public bool CreateEntity(MODEL.UserLogModel m)
        {
            return dal.CreateEntity(m) > 0;
        }

        public IEnumerable<MODEL.UserLogModel> LoadEntities(string where, Dictionary<string, object> dic, string order, bool isAsc)
        {
            return dal.LoadEntities(where, dic, order, isAsc);
        }
    }
}
