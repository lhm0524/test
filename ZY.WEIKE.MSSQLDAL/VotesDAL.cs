using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace ZY.WEIKE.MSSQLDAL
{
    public class VotesDAL : IDAL.IVotesDAL
    {

        public int DeleteModelByPrimaryKey(int primartyKey)
        {
            throw new NotImplementedException();
        }

        public int CreateEntity(MODAL.VotesModel t)
        {
            throw new NotImplementedException();
        }

        public int EditEntity(MODAL.VotesModel t)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MODAL.VotesModel> LoadEntities(string where, Dictionary<string, object> dic, string order, bool isAsc)
        {
            throw new NotImplementedException();
        }

        public MODAL.VotesModel GetModelByPrimaryKey(int primaryKey)
        {
            throw new NotImplementedException();
        }

        public int GetAllPageCountForWhere(string where, Dictionary<string, object> dic)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MODAL.VotesModel> LoadPageEntities(int pageIndex, int pageSize, out int totalCount, string whereLambda, Dictionary<string, object> dic, string order, bool isAsc)
        {
            throw new NotImplementedException();
        }


        public MODAL.VotesModel GetEntity(string where, Dictionary<string, object> dic)
        {
            MODAL.VotesModel m = new MODAL.VotesModel();
            string sql = "select Z_Id,Z_WeikeId, Z_star_1,Z_star_2,Z_star_3,Z_star_4,Z_star_5 from votes";
            if (where.Length != 0)
            {
                sql += " where ";
                sql += where;
            }
            SqlParameter[] ps = SqlHelper.BuildParameter(dic);
            using (SqlDataReader reader = SqlHelper.RunSql(sql, ps))
            {
                if (!reader.HasRows)
                {
                    return null;
                }
                reader.Read();
                m.Z_Id = reader.GetInt32(0);
                m.Z_WeiKeId = reader.GetInt32(1);
                m.Z_Star_1 = reader.GetInt32(2);
                m.Z_Star_2 = reader.GetInt32(3);
                m.Z_Star_3 = reader.GetInt32(4);
                m.Z_Star_4 = reader.GetInt32(5);
                m.Z_Star_5 = reader.GetInt32(6);
            }
            return m;
        }

        public List<int> GetRatingCount(string where, Dictionary<string, object> dic)
        {
            throw new NotImplementedException();
        }
    }
}
