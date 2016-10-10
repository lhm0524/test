using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;

namespace ZY.WEIKE.MSSQLDAL
{
    public class WK_UserLogDAL : IDAL.IWK_UserLogDAL
    {
        public int DeleteModelByPrimaryKey(int primartyKey)
        {
            throw new NotImplementedException();
        }

        public int CreateEntity(MODAL.WK_UserLogModel t)
        {
            throw new NotImplementedException();
        }

        public int EditEntity(MODAL.WK_UserLogModel t)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MODAL.WK_UserLogModel> LoadEntities(string where, Dictionary<string, object> dic, string order, bool isAsc)
        {
            throw new NotImplementedException();
        }

        public MODAL.WK_UserLogModel GetModelByPrimaryKey(int primaryKey)
        {
            throw new NotImplementedException();
        }

        public int GetAllPageCountForWhere(string where, Dictionary<string, object> dic)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MODAL.WK_UserLogModel> LoadPageEntities(int pageIndex, int pageSize, out int totalCount, string whereLambda, Dictionary<string, object> dic, string order, bool isAsc)
        {
            throw new NotImplementedException();
        }

        public int AddPlayCount(int userid, int weikeid)
        {
            
            return 0;
        }

        public int AddSurfaceCount(int userid, int weikeid)
        {
            throw new NotImplementedException();
        }


        public MODAL.WK_UserLogModel GetEntity(string where, Dictionary<string, object> dic)
        {
            throw new NotImplementedException();
        }
    }
}
