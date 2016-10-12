using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZY.WEIKE.MSSQLDAL
{
    public class UsersDAL : IDAL.IUserDAL
    {
        //[Id] [UserRole] [Email]  [LastLoginTime] [Birthday] [RegeistTime] [Sex] [UserPwd] [UserName]  [Answer] [RequireInfomation] [UserImagePath]  
        public int DeleteModelByPrimaryKey(int primartyKey)
        {
            throw new NotImplementedException();
        }

        public int CreateEntity(MODAL.UsersModel t)
        {
            SqlParameter[] ps = new SqlParameter[] 
            { 
                new SqlParameter() { ParameterName = "@username", Value = t.UserName, SqlDbType = SqlDbType.NVarChar, Size = 16 },
                new SqlParameter() { ParameterName = "@pwd", Value = t.UserPwd, DbType = DbType.String },
                new SqlParameter() { ParameterName = "@answer", Value = t.Answer, DbType = DbType.String },
                new SqlParameter() { ParameterName = "@email", Value = t.Email, DbType = DbType.String },
                new SqlParameter() { ParameterName = "@time", Value = DateTime.Now, DbType = DbType.DateTime }
            };
            string sql = "insert into Users (UserName,UserPwd,Answer,Email,RegeistTime) values(@username,@pwd,@answer,@email, @time)";
            int insertcount = SqlHelper.ExecuteNonQuery(sql, CommandType.Text, ps);
            return insertcount;
        }

        public int EditEntity(MODAL.UsersModel t)
        {
            string sql = "update Users set Email=@email,Birthday=@birthday,Sex=@sex,UserName=@username,Answer=@answer,UserPwd=@userpwd,UserImagePath=@imgpath where Id=@id";

            SqlParameter[] ps = new SqlParameter[]
            {
                new SqlParameter { ParameterName = "@email", Value = t.Email, SqlDbType = SqlDbType.NVarChar, Size = 35 },
                new SqlParameter { ParameterName = "@birthday", Value = t.Birthday, SqlDbType = SqlDbType.DateTime },
                new SqlParameter { ParameterName = "@sex", Value = t.Sex, SqlDbType = SqlDbType.NChar, Size = 1 },
                new SqlParameter { ParameterName = "@username", Value = t.UserName, SqlDbType = SqlDbType.NVarChar, Size = 16 },
                new SqlParameter { ParameterName = "@answer", Value = t.Answer, SqlDbType = SqlDbType.NVarChar, Size = 12 },
                new SqlParameter { ParameterName = "@userpwd", Value = t.UserPwd, SqlDbType = SqlDbType.NVarChar, Size = 32 },
                new SqlParameter { ParameterName = "@imgpath", Value = t.UserImagePath, DbType = DbType.String },
                new SqlParameter { ParameterName = "@id", Value = t.Id, DbType = DbType.Int32 }
            };

            int result = SqlHelper.ExecuteNonQuery(sql, CommandType.Text, ps);

            return result;
        }

        public IEnumerable<MODAL.UsersModel> LoadEntities(string where, Dictionary<string, object> dic, string order, bool isAsc)
        {
            throw new NotImplementedException();
        }

        public MODAL.UsersModel GetModelByPrimaryKey(int primaryKey)
        {
            string sql = "select Email,Birthday,Sex,UserName,Answer,UserImagePath,LastLoginTime,UserPwd,Id from Users where Id=@id";
            using (SqlDataReader reader = SqlHelper.ExecuteReader(sql, new SqlParameter[] { new SqlParameter("@id", primaryKey) }))
            {
                MODAL.UsersModel m = new MODAL.UsersModel();
                reader.Read();
                m.Email = reader.GetString(0);
                m.Birthday = reader.GetDateTime(1);
                m.Sex = reader.GetString(2);
                m.UserName = reader.GetString(3);
                m.Answer = reader.GetString(4);
                m.UserImagePath = reader.GetString(5);
                m.LastLoginTime = reader.GetDateTime(6);
                m.UserPwd = reader.GetString(7);
                m.Id = reader.GetInt32(8);
                return m;
            }
        }

        public int GetAllPageCountForWhere(string where, Dictionary<string, object> dic)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MODAL.UsersModel> LoadPageEntities(int pageIndex, int pageSize, out int totalCount, string whereLambda, Dictionary<string, object> dic, string order, bool isAsc)
        {
            throw new NotImplementedException();
        }

        public int Login(int logintype, string name, string pwd, out object role)
        {
            using (SqlConnection con = SqlHelper.GetConnection())
            {
                con.Open();
                SqlCommand cmd = null;
                SqlParameter username;
                if (logintype == 1)
                {
                    cmd = new SqlCommand("p_LoginByEmail", con);
                    username = new SqlParameter("@username", SqlDbType.NVarChar, 35);
                }
                else
                {
                    cmd = new SqlCommand("p_LoginByUserName", con);
                    username = new SqlParameter("@username", SqlDbType.NVarChar, 16);
                }
                cmd.CommandType = CommandType.StoredProcedure;
                username.Value = name;
                SqlParameter output_role = new SqlParameter("@role", SqlDbType.NVarChar, 5);
                output_role.Direction = System.Data.ParameterDirection.Output;//输出类型

                SqlParameter result_id = new SqlParameter("@reuslt_id", SqlDbType.Int);
                result_id.Direction = ParameterDirection.ReturnValue;//返回类型



                SqlParameter password = new SqlParameter("@password", SqlDbType.NVarChar, 32);
                password.Value = pwd;


                cmd.Parameters.Add(username);
                cmd.Parameters.Add(password);
                cmd.Parameters.Add(output_role);
                cmd.Parameters.Add(result_id);
                cmd.ExecuteNonQuery();


                cmd.ExecuteScalar();
                cmd.Dispose();
                role = output_role.Value;
                return (int)result_id.Value;
            }
        }

        public MODAL.UsersModel GetImageAndName(int primaryKey)
        {
            MODAL.UsersModel u = new MODAL.UsersModel();
            string sql = "select UserImagePath,UserName from Users where Id=@id";
            using (SqlDataReader reader = SqlHelper.ExecuteReader(sql, new SqlParameter[] { new SqlParameter("@id", primaryKey) }))
            {
                reader.Read();
                u.UserImagePath = reader[0] is DBNull ? null : reader.GetString(0);
                u.UserName = reader.GetString(1);
            }
            return u;
        }

        public int IsExists(MODAL.UsersModel usermodel)
        {
            string sql = "select count(1) from users where UserName=@username or Email=@email";
            SqlParameter[] ps = new SqlParameter[]
            {
                new SqlParameter() { ParameterName = "@username", Value = usermodel.UserName, SqlDbType = SqlDbType.NVarChar, Size = 16 },
                new SqlParameter() { ParameterName = "@email", Value = usermodel.Email, SqlDbType = SqlDbType.NVarChar, Size = 16 }
            };
            int result = (int)SqlHelper.ExecuteScalar(sql, CommandType.Text, ps);
            return result;
        }

        public int EditImage(int primarykey, string imagepath)
        {
            string sql = "update users set userimagepath=@path where Id=@id";
            SqlParameter[] ps = new SqlParameter[]
            {
                new SqlParameter() { ParameterName = "@path", Value = imagepath, DbType = DbType.String },
                new SqlParameter() { ParameterName = "@id", DbType = DbType.Int32, Value = primarykey }
            };
            int count = SqlHelper.ExecuteNonQuery(sql, CommandType.Text, ps);
            return count;
        }


        public MODAL.UsersModel GetEntity(string where, Dictionary<string, object> dic)
        {
            throw new NotImplementedException();
        }
    }
}
