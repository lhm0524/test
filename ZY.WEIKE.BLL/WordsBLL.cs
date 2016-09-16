using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZY.WEIKE.BLL
{
    public class WordsBLL
    {
        private IDAL.IWordsDAL dal;
        public WordsBLL()
        {
            dal = DALFACTORY.AbstractFactory.CreateWordsDALInstance();
        }
        public IEnumerable<MODAL.WordsModel> LoadPageEntities(int pageIndex, int pageSize, out int totalCount, string whereLambda, Dictionary<string, object> dic, string order, bool isAsc)
        {
            return dal.LoadPageEntities(pageIndex, pageSize, out totalCount, whereLambda, dic, order, isAsc);
        }
        public bool CreateEntity(MODAL.WordsModel m)
        {
            return dal.CreateEntity(m) > 0;
        }
    }
}
