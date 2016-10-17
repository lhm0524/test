using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ZY.WEIKE.MSSQLDAL
{
    public class WeiKeDAL : IDAL.IWeiKeDAL
    {
        public int DeleteModelByPrimaryKey(int primartyKey)
        {
            string sql = "delete Weike where id=@id";
            SqlParameter[] ps = new SqlParameter[] 
            {
                new SqlParameter(){ ParameterName = "@id", Value = primartyKey}
            };
            return (int)SqlHelper.ExecuteNonQuery(sql, System.Data.CommandType.Text, ps);
        }

        public int CreateEntity(MODEL.WeiKeModel t)
        {
            throw new NotImplementedException();
        }

        public int EditEntity(MODEL.WeiKeModel t)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MODEL.WeiKeModel> LoadEntities(string where, Dictionary<string, object> dic, string order, bool isAsc)
        {
            string sql = "select Id, Name, Description from weike";
            List<MODEL.WeiKeModel> list = new List<MODEL.WeiKeModel>();
            if (where.Length != 0)
            {
                sql = sql + " where " + where;
            }
            if (order.Length != 0)
            {
                sql = sql + " order by " + order;
                if (isAsc)
                {
                    sql += " asc";
                }
                else
                {
                    sql += " desc";
                }
            }
            SqlDataReader reader;
            SqlParameter[] ps = SqlHelper.BuildParameter(dic);


            using (reader = SqlHelper.ExecuteReader(sql, ps))
            {
                while (reader.Read())
                {
                    MODEL.WeiKeModel m = new MODEL.WeiKeModel();
                    m.Id = reader.GetInt32(0);
                    m.Name = reader.GetString(1);
                    m.Description = reader.GetString(2);
                    list.Add(m);
                }
            }
            return list;
        }

        public MODEL.WeiKeModel GetModelByPrimaryKey(int primaryKey)
        {
            string sql = "select TeacherId,typeid,CreateTime,Name,Detail,Description from Weike where Id=@id";
            using (SqlDataReader reader = SqlHelper.ExecuteReader(sql, new SqlParameter[] { new SqlParameter("@id", primaryKey) }))
            {
                reader.Read();

                MODEL.WeiKeModel m = new MODEL.WeiKeModel();
                m.TeacherId = reader.GetInt32(0);
                m.TypeId = reader.GetInt32(1);
                m.CreateTime = reader.GetDateTime(2);
                m.Name = reader.GetString(3);
                m.Detail = reader.GetString(4);
                m.Description = reader.GetString(5);
                return m;
            }
        }

        public int GetAllPageCountForWhere(string where, Dictionary<string, object> dic)
        {
            int pagecount = 0;

            string sql = "select count(1) from Weike";

            sql = SqlHelper.BuildSql(sql, where, "", true);
            Int32.TryParse(SqlHelper.ExecuteScalar(sql, System.Data.CommandType.Text, SqlHelper.BuildParameter(dic)).ToString(), out pagecount);

            return pagecount;
        }

        public IEnumerable<MODEL.WeiKeModel> LoadPageEntities(int pageIndex, int pageSize, out int totalCount, string whereLambda, Dictionary<string, object> dic, string order, bool isAsc)
        {
            List<MODEL.WeiKeModel> list = new List<MODEL.WeiKeModel>();

            string sqlcount = "select count(1) from Weike where typeid=@id";
            totalCount = int.Parse(SqlHelper.ExecuteScalar(sqlcount, System.Data.CommandType.Text, SqlHelper.BuildParameter(dic)).ToString()) / pageSize + 1;
            string sql = "select Id,Name,Description from(select row_number() over (order by Id) as num,Id,Name,Description from WeiKe where typeid = @id) AS t where num >= (@PageIndex - 1) * @PageSize + 1 and num <= @PageSize * @PageIndex";
            SqlParameter[] ps = new SqlParameter[]
            {
                new SqlParameter("@PageIndex", pageIndex),
                new SqlParameter("@PageSize", pageSize),
                SqlHelper.BuildParameter(dic)[0]
            };
            using (SqlDataReader reader = SqlHelper.ExecuteReader(sql, ps))
            {
                while (reader.Read())
                {
                    MODEL.WeiKeModel w = new MODEL.WeiKeModel();
                    w.Id = reader.GetInt32(0);
                    w.Name = reader.GetString(1);
                    w.Description = reader.GetString(2);
                    list.Add(w);
                }
            }
            return list;
        }

        public IEnumerable<MODEL.WeiKeModel> LoadNewEntities(string where, Dictionary<string, object> dic, string order, bool isAsc)
        {
            string sql = "select top 3 Id, Name, Description from weike";
            List<MODEL.WeiKeModel> list = new List<MODEL.WeiKeModel>();
            if (where.Length != 0)
            {
                sql = sql + " where " + where;
            }
            if (order.Length != 0)
            {
                sql = sql + " order by " + order;
                if (isAsc)
                {
                    sql += " asc";
                }
                else
                {
                    sql += " desc";
                }
            }
            SqlDataReader reader;
            SqlParameter[] ps = SqlHelper.BuildParameter(dic);

            reader = SqlHelper.ExecuteReader(sql, ps);

            while (reader.Read())
            {
                MODEL.WeiKeModel m = new MODEL.WeiKeModel();
                m.Id = reader.GetInt32(0);
                m.Name = reader.GetString(1);
                m.Description = reader.GetString(2);
                list.Add(m);
            }
            reader.Close();
            reader.Dispose();
            return list;
        }


        public MODEL.WeiKeModel GetEntity(string where, Dictionary<string, object> dic)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<MODEL.WeiKeModel> Manager_LoadPageEntities(int pageIndex, int pageSize, string where, Dictionary<string, object> dic, string order, bool IsAsc, out int totalCount)
        {
            List<MODEL.WeiKeModel> list = new List<MODEL.WeiKeModel>();

            string sqlcount = "select count(1) from Weike where " + where;
            totalCount = int.Parse(SqlHelper.ExecuteScalar(sqlcount, System.Data.CommandType.Text, SqlHelper.BuildParameter(dic)).ToString()) / pageSize + 1;
            string sql = "select Id,Name,createtime,typeid from(select row_number() over (order by {0} Id) as num,Id,Name,createtime,typeid from WeiKe where " + where + ") AS t where num >= (@PageIndex - 1) * @PageSize + 1 and num <= @PageSize * @PageIndex";
            if (order.Length != 0)
            {
                if (!IsAsc)
                {
                    sql = string.Format(sql, order  + " desc,");
                }
                else
                {
                    sql = string.Format(sql, order + " asc,");
                }
            }
            else
            {
                sql = string.Format(sql, "");
            }
            SqlParameter[] ps = new SqlParameter[]
            {
                new SqlParameter("@PageIndex", pageIndex),
                new SqlParameter("@PageSize", pageSize)
            };
            List<SqlParameter> listps = ps.ToList();
            listps.AddRange(SqlHelper.BuildParameter(dic));
            using (SqlDataReader reader = SqlHelper.ExecuteReader(sql, listps.ToArray()))
            {
                
                while (reader.Read())
                {
                    MODEL.WeiKeModel w = new MODEL.WeiKeModel();
                    w.Id = reader.GetInt32(0);
                    w.Name = reader.GetString(1);
                    w.CreateTime = reader.GetDateTime(2);
                    w.TypeId = reader.GetInt32(3);
                    list.Add(w);
                }
            }
            return list;
            throw new NotImplementedException();
        }


        public object AddWeiKeEntity(MODEL.WeiKeModel w, MODEL.ResourceModel res)
        {
            List<SqlParameter> list = new List<SqlParameter>();
            SqlParameter attach = new SqlParameter() { ParameterName = "@attachmentpath", DbType = System.Data.DbType.String, Value = res.AttachmentPath };
            if (res.AttachmentPath == null)
            {
                attach.Value = DBNull.Value;
            }
            list.Add(new SqlParameter("@name", w.Name));
            list.Add(new SqlParameter("@Detail", w.Detail));
            list.Add(new SqlParameter("@teacherid", w.TeacherId));
            list.Add(new SqlParameter("@WeikeType", w.TypeId));
            list.Add(new SqlParameter("@createtime", DateTime.Now));
            list.Add(new SqlParameter("@description", w.Description));

            list.Add(attach);
            list.Add(new SqlParameter("@videopath", res.VideoPath));
            list.Add(new SqlParameter("@imagepath", res.VideoImgPath));
            list.Add(new SqlParameter("@totalprogress", res.TotalProgress));
            SqlParameter[] ps = list.ToArray();
            object result = SqlHelper.ExecuteScalar("p_InsertWeiKe", System.Data.CommandType.StoredProcedure, ps);

            return result;
        }
    }
}
