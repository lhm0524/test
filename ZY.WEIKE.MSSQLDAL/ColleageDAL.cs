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

        public int CreateEntity(MODAL.ColleageModel t)
        {
            throw new NotImplementedException();
        }

        public int EditEntity(MODAL.ColleageModel t)
        {
            throw new NotImplementedException();
        }

        public MODAL.ColleageModel GetModelByPrimaryKey(int primaryKey)
        {
            throw new NotImplementedException();
        }

        public int GetAllPageCountForWhere(string where, Dictionary<string, object> dic)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MODAL.ColleageModel> GetTopList(int top)
        {
            List<MODAL.ColleageModel> list = new List<MODAL.ColleageModel>();
            string sql = string.Format("select top {0} Id,Name from Colleage", top);
            SqlDataReader reader;
            using (reader = SqlHelper.RunSql(sql, null))
            {
                while (reader.Read())
                {
                    MODAL.ColleageModel m = new MODAL.ColleageModel();
                    m.Id = reader.GetInt32(0);
                    m.Name = reader.GetString(1);
                    list.Add(m);
                }
            }
            return list;
        }


        public IEnumerable<MODAL.ColleageModel> LoadEntities(string where, Dictionary<string, object> dic, string order, bool isAsc)
        {
            //string sql = "select * from Colleage";
            //if (where.Length != 0)
            //{
            //    sql = sql + " where " + where;
            //}
            //SqlParameter[] ps = SqlHelper.BuildParameter(dic);
            //SqlDataReader reader;
            //SqlHelper.RunSql(sql, ps, out reader);

            //while (reader.Read())
            //{

            //}
            //SqlHelper.CloseConnection();
            throw new NotImplementedException();
        }

        public IEnumerable<MODAL.ColleageModel> LoadPageEntities(int pageIndex, int pageSize, out int totalCount, string whereLambda, Dictionary<string, object> dic, string order, bool isAsc)
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
            SqlDataReader reader = SqlHelper.RunSql(sql, ps);
            List<MODAL.ColleageModel> list = new List<MODAL.ColleageModel>();
            while (reader.Read())
            {
                MODAL.ColleageModel m = new MODAL.ColleageModel();
                m.Id = reader.GetInt32(0);
                m.Name = reader.GetString(1);
                list.Add(m);
            }
            reader.Close();
            reader.Dispose();
            return list;
        }
    }
}
