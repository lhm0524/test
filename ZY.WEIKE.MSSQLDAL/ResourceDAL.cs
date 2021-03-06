﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;

namespace ZY.WEIKE.MSSQLDAL
{
    public class ResourceDAL : IDAL.IResourceDAL
    {

        public string GetImgPath(int weikeid)
        {
            string sql = "select VideoImgPath from Resource where WeikeId=@id";
            string result = SqlHelper.ExecuteScalar(sql, System.Data.CommandType.Text, new SqlParameter[] { new SqlParameter("@id", weikeid) }).ToString();
            return result;
        }

        public int DeleteModelByPrimaryKey(int primartyKey)
        {
            throw new NotImplementedException();
        }

        public int CreateEntity(MODEL.ResourceModel t)
        {
            throw new NotImplementedException();
        }

        public int EditEntity(MODEL.ResourceModel t)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MODEL.ResourceModel> LoadEntities(string where, Dictionary<string, object> dic, string order, bool isAsc)
        {
            throw new NotImplementedException();
        }

        public MODEL.ResourceModel GetModelByPrimaryKey(int primaryKey)
        {
            MODEL.ResourceModel res = new MODEL.ResourceModel();
            string sql = "select AttachmentPath,VideoPath,VideoImgPath,TotalProgress from resource where weikeid=@id";
            using (SqlDataReader reader = SqlHelper.ExecuteReader(sql, new SqlParameter[] { new SqlParameter("@id", primaryKey) }))
            {
                reader.Read();
                res.AttachmentPath = reader.IsDBNull(0) ? null : reader.GetString(0);
                res.VideoImgPath = reader.GetString(2);
                res.VideoPath = reader.GetString(1);
                res.TotalProgress = reader.GetDouble(3);
            }
            return res;
        }

        public int GetAllPageCountForWhere(string where, Dictionary<string, object> dic)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MODEL.ResourceModel> LoadPageEntities(int pageIndex, int pageSize, out int totalCount, string whereLambda, Dictionary<string, object> dic,  string order, bool isAsc)
        {
            throw new NotImplementedException();
        }


        public MODEL.ResourceModel GetEntity(string where, Dictionary<string, object> dic)
        {
            throw new NotImplementedException();
        }
    }
}
