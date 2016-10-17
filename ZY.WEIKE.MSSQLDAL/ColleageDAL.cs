using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZY.WEIKE.MSSQLDAL
{
    public class ColleageDAL : IDAL.IColleageDAL
    {
        public int DeleteModelByPrimaryKey(int primartyKey)
        {
            throw new NotImplementedException();
        }

        public int CreateEntity(MODEL.ColleageModel t)
        {
            throw new NotImplementedException();
        }

        public int EditEntity(MODEL.ColleageModel t)
        {
            throw new NotImplementedException();
        }

        public MODEL.ColleageModel GetModelByPrimaryKey(int primaryKey)
        {
            throw new NotImplementedException();
        }

        public int GetAllPageCountForWhere(string where, Dictionary<string, object> dic)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MODEL.ColleageModel> GetTopList(int top)
        {
            List<MODEL.ColleageModel> list = new List<MODEL.ColleageModel>();
            string sql = string.Format("select top {0} Id,Name from Colleage", top);
            SqlDataReader reader;
            using (reader = SqlHelper.ExecuteReader(sql, null))
            {
                while (reader.Read())
                {
                    MODEL.ColleageModel m = new MODEL.ColleageModel();
                    m.Id = reader.GetInt32(0);
                    m.Name = reader.GetString(1);
                    list.Add(m);
                }
            }
            return list;
        }


        public IEnumerable<MODEL.ColleageModel> LoadEntities(string where, Dictionary<string, object> dic, string order, bool isAsc)
        {
            string sql = "select Id, Name from Colleage";
            List<MODEL.ColleageModel> list = new List<MODEL.ColleageModel>();
            sql = SqlHelper.BuildSql(sql, where, order, isAsc);
            using (SqlDataReader reader = SqlHelper.ExecuteReader(sql, null))
            {
                if (!reader.HasRows)
                {
                    return list;
                }
                while (reader.Read())
                {
                    MODEL.ColleageModel m = new MODEL.ColleageModel();
                    m.Id = reader.GetInt32(0);
                    m.Name = reader.GetString(1);
                    list.Add(m);
                }
            }
            return list;
        }

        public IEnumerable<MODEL.ColleageModel> LoadPageEntities(int pageIndex, int pageSize, out int totalCount, string whereLambda, Dictionary<string, object> dic, string order, bool isAsc)
        {
            string sql = "select Id,Name from(select row_number() over (order by Id) as num,* from Colleage) AS t where num >= (@PageIndex - 1) * @PageSize + 1 and num <= @PageSize * @PageIndex";

            string calccountsql = "select count(1) from Colleage";
            totalCount = Int32.Parse(SqlHelper.ExecuteScalar(calccountsql, System.Data.CommandType.Text, null).ToString()) / pageSize + 1;
            SqlParameter[] ps = new SqlParameter[]
            {
                new SqlParameter("@PageIndex", pageIndex),
                new SqlParameter("@PageSize", pageSize)
            };
            if (dic != null)
            {
                SqlParameter[] ps1 = SqlHelper.BuildParameter(dic);
                ps.Union(ps1);
            }
            SqlDataReader reader = SqlHelper.ExecuteReader(sql, ps);
            List<MODEL.ColleageModel> list = new List<MODEL.ColleageModel>();
            while (reader.Read())
            {
                MODEL.ColleageModel m = new MODEL.ColleageModel();
                m.Id = reader.GetInt32(0);
                m.Name = reader.GetString(1);
                list.Add(m);
            }
            reader.Close();
            reader.Dispose();
            return list;
        }


        public MODEL.ColleageModel GetEntity(string where, Dictionary<string, object> dic)
        {
            throw new NotImplementedException();
        }
    }
}
