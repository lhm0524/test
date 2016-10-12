using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;

namespace ZY.WEIKE.MSSQLDAL
{
    public class DeparmentDAL : IDAL.IDepartmentDAL
    {
        public IEnumerable<MODAL.DepartmentModel> LoadByParentID(int parentId)
        {
            List<MODAL.DepartmentModel> list = new List<MODAL.DepartmentModel>();

            string sql = "select Id,Name from Department where parentId=@id";

            SqlDataReader reader;
            using (reader = SqlHelper.ExecuteReader(sql, new SqlParameter[] { new SqlParameter("@id", parentId) }))
            {
                while (reader.Read())
                {
                    MODAL.DepartmentModel m = new MODAL.DepartmentModel();
                    m.Id = reader.GetInt32(0);
                    m.Name = reader.GetString(1);
                    list.Add(m);
                }
            }
            return list;
        }

        public int DeleteModelByPrimaryKey(int primartyKey)
        {
            throw new NotImplementedException();
        }

        public int CreateEntity(MODAL.DepartmentModel t)
        {
            throw new NotImplementedException();
        }

        public int EditEntity(MODAL.DepartmentModel t)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MODAL.DepartmentModel> LoadEntities(string where, Dictionary<string, object> dic, string order, bool isAsc)
        {
            throw new NotImplementedException();
        }

        public MODAL.DepartmentModel GetModelByPrimaryKey(int primaryKey)
        {
            string sql = "select Id, ParentId, Name from department where Id=@id";
            SqlParameter[] ps = new SqlParameter[]
            {
                new SqlParameter(){ ParameterName = "@id", Value = primaryKey }
            };
            using(SqlDataReader reader = SqlHelper.ExecuteReader(sql, ps))
	        {
                if (!reader.HasRows)
                {
                    return null;
                }
                reader.Read();
                MODAL.DepartmentModel m = new MODAL.DepartmentModel();
                m.Id = reader.GetInt32(0);
                m.Parentid = reader.GetInt32(1);
                m.Name = reader.GetString(2);
                return m;
	        }
        }

        public int GetAllPageCountForWhere(string where, Dictionary<string, object> dic)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MODAL.DepartmentModel> LoadPageEntities(int pageIndex, int pageSize, out int totalCount, string whereLambda, Dictionary<string, object> dic, string order, bool isAsc)
        {
            throw new NotImplementedException();
        }


        public MODAL.DepartmentModel GetEntity(string where, Dictionary<string, object> dic)
        {
            throw new NotImplementedException();
        }
    }
}
