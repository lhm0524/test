using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;

namespace ZY.WEIKE.MSSQLDAL
{
    public class UserLogDAL : IDAL.IUserLogDAL
    {
        public int DeleteModelByPrimaryKey(int primartyKey)
        {
            throw new NotImplementedException();
        }

        public int CreateEntity(MODEL.UserLogModel t)
        {
            string check = "select count(1) from userlog where weikeid=@wid and userid = @uid";
            string sql = "insert into UserLog(weikeid, userid, iscomplete) values(@wid,@uid,@com)";
            SqlParameter[] ps = new SqlParameter[]
            {//ddddddd
                new SqlParameter() { ParameterName = "@wid", Value = t.Z_WeiKeId, DbType = System.Data.DbType.Int32 },
                new SqlParameter() { ParameterName = "@uid", Value = t.Z_UserId, DbType = System.Data.DbType.Int32 },
                new SqlParameter() {ParameterName = "@com", Value = t.Isfinish, DbType = System.Data.DbType.Boolean }
            };
            if ((int)SqlHelper.ExecuteScalar(check, System.Data.CommandType.Text, ps) > 0)
            {
                sql = "update userlog set isfinish = @is where weikeid=@wid and userid = @uid";
            }
            int result = SqlHelper.ExecuteNonQuery(sql, System.Data.CommandType.Text, ps);
            return result;
        }

        public int EditEntity(MODEL.UserLogModel t)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MODEL.UserLogModel> LoadEntities(string where, Dictionary<string, object> dic, string order, bool isAsc)
        {
            string sql = "select WeikeId, Iscomplete from UserLog";
            List<MODEL.UserLogModel> list = new List<MODEL.UserLogModel>();
            sql = SqlHelper.BuildSql(sql, where, order, isAsc);
            using (SqlDataReader reader = SqlHelper.ExecuteReader(sql, SqlHelper.BuildParameter(dic)))
            {
                if (!reader.HasRows)
                {
                    return list;
                }
                while (reader.Read())
                {
                    MODEL.UserLogModel m = new MODEL.UserLogModel();
                    m.Z_WeiKeId = reader.GetInt32(0);
                    m.Isfinish = reader.GetBoolean(1);
                    list.Add(m);
                }
                return list;
            }
        }

        public MODEL.UserLogModel GetModelByPrimaryKey(int primaryKey)
        {
            throw new NotImplementedException();
        }

        public int GetAllPageCountForWhere(string where, Dictionary<string, object> dic)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MODEL.UserLogModel> LoadPageEntities(int pageIndex, int pageSize, out int totalCount, string whereLambda, Dictionary<string, object> dic, string order, bool isAsc)
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


        public MODEL.UserLogModel GetEntity(string where, Dictionary<string, object> dic)
        {
            throw new NotImplementedException();
        }
    }
}
