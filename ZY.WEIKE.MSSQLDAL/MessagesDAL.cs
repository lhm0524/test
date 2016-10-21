using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.SqlClient;
namespace ZY.WEIKE.MSSQLDAL
{
    public class MessagesDAL : IDAL.IMessagesDAL
    {
        //[Id]         INT            IDENTITY (1, 1) NOT NULL,
        //[FromUserId] INT            NOT NULL,
        //[ToUserID]   INT            NOT NULL,
        //[Subject]    INT            NOT NULL,
        //[MSGTypeId]  INT            NULL,
        //[MSG]        NVARCHAR (MAX) NOT NULL,
        //[state]      BIT            DEFAULT ((0)) NULL,
        //[Time]       DATETIME       DEFAULT (getdate()) NULL,
        //PRIMARY KEY CLUSTERED ([Id] ASC),
        //FOREIGN KEY ([FromUserId]) REFERENCES [dbo].[Users] ([Id]),
        //FOREIGN KEY ([ToUserID]) REFERENCES [dbo].[Users] ([Id]),
        //FOREIGN KEY ([MSGTypeId]) REFERENCES [dbo].[MessagesType] ([Id])
        public MODEL.MessagesModel GetMsg(string where, MODEL.DoNetParameter dic, string order, bool isAsc)
        {
            string sql = "select Id,MSGTypeId,Subject,MSG,Time,state from MESSAGES";
            if (where.Length != 0)
            {
                sql += " where " + where;
            }
            if (order.Length != 0)
            {
                sql += " order by " + order;
                if (!isAsc)
                {
                    sql += " desc";
                }
            }
            using (SqlDataReader reader = SqlHelper.ExecuteReader(sql, SqlHelper.BuildParameter(dic)))
            {
                reader.Read();
                MODEL.MessagesModel m = new MODEL.MessagesModel();
                m.Id = reader.GetInt32(0);
                m.MSGTypeId = reader.GetInt32(1);
                m.Subject = reader.GetString(2);
                m.MSG = reader.GetString(3);
                m.Time = reader.GetDateTime(4);
                m.State = reader.GetBoolean(5);
                return m;
            }
            throw new NotImplementedException();
        }

        public int DeleteModelByPrimaryKey(int primartyKey)
        {
            throw new NotImplementedException();
        }

        public int CreateEntity(MODEL.MessagesModel t)
        {
            string sql = "insert into messages(FromUserId,ToUserID,Subject,MSGTypeId,MSG) values (@fuid,@tuid,@s,@mtid,@msg)";
            SqlParameter[] ps = new SqlParameter[]
            {
                new SqlParameter() { ParameterName = "@fuid", Value = t.FromUserId, DbType = System.Data.DbType.Int32 },
                new SqlParameter() { ParameterName = "@tuid", Value = t.ToUserID, DbType = System.Data.DbType.Int32 },
                new SqlParameter() { ParameterName = "@s", Value = t.Subject, DbType = System.Data.DbType.String },
                new SqlParameter() { ParameterName = "@mtid", Value = t.MSGTypeId, DbType = System.Data.DbType.Int32 },
                new SqlParameter() { ParameterName = "@msg", Value = t.MSG, DbType = System.Data.DbType.String }
            };
            int res = SqlHelper.ExecuteNonQuery(sql, System.Data.CommandType.Text, ps);
            return res;
        }

        public int EditEntity(MODEL.MessagesModel t)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MODEL.MessagesModel> LoadEntities(string where, Dictionary<string, object> dic, string order, bool isAsc)
        {
            throw new NotImplementedException();
        }

        public MODEL.MessagesModel GetModelByPrimaryKey(int primaryKey)
        {
            throw new NotImplementedException();
        }

        public int GetAllPageCountForWhere(string where, Dictionary<string, object> dic)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MODEL.MessagesModel> LoadPageEntities(int pageIndex, int pageSize, out int totalCount, string whereLambda, Dictionary<string, object> dic, string order, bool isAsc)
        {
            throw new NotImplementedException();
        }

        public MODEL.MessagesModel GetEntity(string where, Dictionary<string, object> dic)
        {
            throw new NotImplementedException();
        }
    }
}
