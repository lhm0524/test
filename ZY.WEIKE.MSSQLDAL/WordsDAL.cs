using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;

namespace ZY.WEIKE.MSSQLDAL
{
    public class WordsDAL : IDAL.IWordsDAL
    {

        public int DeleteModelByPrimaryKey(int primartyKey)
        {
            throw new NotImplementedException();
        }

        public int CreateEntity(MODAL.WordsModel t)
        {
            string sql = "insert into Words (WeiKeId,UserID,WordsTitle,WordsBody,WordsTime) values(@wid, @uid,@wt,@wb,@time)";
            object o;
            if (t.WordsTitle == null)
            {
                o = DBNull.Value;
            }
            else
            {
                o = t.WordsTitle;
            }
            SqlParameter[] ps = new SqlParameter[]
            {
                new SqlParameter("@wid", t.WeiKeId),
                new SqlParameter("@uid", t.UserId),
                new SqlParameter("@wb", t.WordsBody),
                new SqlParameter("@wt", o),
                new SqlParameter("@time", t.WordsTime)
            };
            int result = SqlHelper.ExecuteNonQuery(sql, System.Data.CommandType.Text, ps);

            return result;
        }

        public int EditEntity(MODAL.WordsModel t)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MODAL.WordsModel> LoadEntities(string where, Dictionary<string, object> dic, string order, bool isAsc)
        {
            throw new NotImplementedException();
        }

        public MODAL.WordsModel GetModelByPrimaryKey(int primaryKey)
        {
            throw new NotImplementedException();
        }

        public int GetAllPageCountForWhere(string where, Dictionary<string, object> dic)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MODAL.WordsModel> LoadPageEntities(int pageIndex, int pageSize, out int totalCount, string whereLambda, Dictionary<string, object> dic, string order, bool isAsc)
        {
            string sql = "select Id,UserID,WordsTitle,WordsBody,WordsTime from(select row_number() over (order by Id) as num,Id,UserID,WordsTitle,WordsBody,WordsTime from words where WeiKeId = @id) AS t where num >= (@PageIndex - 1) * @PageSize + 1 and num <= @PageSize * @PageIndex";
            List<MODAL.WordsModel> list = new List<MODAL.WordsModel>();
            string sqlcount = "select count(1) from words where weikeid=@id";
            totalCount = int.Parse(SqlHelper.ExecuteScalar(sqlcount, System.Data.CommandType.Text, SqlHelper.BuildParameter(dic)).ToString()) / pageSize + 1;
            SqlParameter[] ps = new SqlParameter[]
            {
                new SqlParameter("@pageIndex", pageIndex),
                new SqlParameter("@pageSize", pageSize),
                SqlHelper.BuildParameter(dic)[0]
            };
            if (order.Length != 0)
            {
                sql += " order by " + order;
                if (isAsc)
                {
                    sql += " asc";
                }
            }
            using (SqlDataReader reader = SqlHelper.RunSql(sql, ps))
            {
                if (!reader.HasRows)
                {
                    return list;
                }
                while (reader.Read())
                {
                    MODAL.WordsModel m = new MODAL.WordsModel();
                    m.Id = reader.GetInt32(0);
                    m.UserId = reader.GetInt32(1);
                    m.WordsTitle = reader.IsDBNull(2) ? null : reader.GetString(2);
                    m.WordsBody = reader.GetString(3);
                    m.WordsTime = reader.GetDateTime(4);
                    list.Add(m);
                }
            }

            return list;
        }


        public MODAL.WordsModel GetEntity(string where, Dictionary<string, object> dic)
        {
            throw new NotImplementedException();
        }
    }
}
