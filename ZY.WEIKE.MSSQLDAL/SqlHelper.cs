using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZY.WEIKE.MSSQLDAL
{
    public class SqlHelper
    {

        /// <summary>
        /// 获得数据连接对象
        /// </summary>
        /// <returns>sqlconnection</returns>
        public static SqlConnection GetConnection()
        {
            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            return con;
        }

        /// <summary>
        /// reader对象
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="ps">sql语句中的参数</param>
        /// <param name="reader">sqldatareader对象</param>
        public static SqlDataReader ExecuteReader(string sql, SqlParameter[] ps)
        {
            SqlConnection con = GetConnection();
            SqlDataReader reader;
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                if (ps != null)
                {
                    cmd.Parameters.AddRange(ps);
                }
                con.Open();
                //System.Diagnostics.Debug.WriteLine(cmd.Parameters);
                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                return reader;
            }
        }

        /// <summary>
        /// 返回第一行第一列
        /// </summary>
        /// <param name="sql">执行sql语句或者是存储过程的名称</param>
        /// <param name="type">类型是存储过程还是sql文本</param>
        /// <param name="ps">参数</param>
        /// <returns></returns>
        public static object ExecuteScalar(string sql, CommandType type, SqlParameter[] ps)
        {
            using (SqlConnection con = GetConnection())
            {
                //MyConnection.Open();//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! 出现BUG的位置
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! 放在这里会间歇性出现
                cmd.CommandType = type;
                if (ps != null)
                {
                    cmd.Parameters.AddRange(ps);
                }
                object o = cmd.ExecuteScalar();
                return o;
            }
        }

        /// <summary>
        /// 对连接执行 Transact-SQL 语句并返回受影响的行数。
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="type">类型</param>
        /// <param name="ps">参数</param>
        /// <returns>返回影响的行数</returns>
        public static int ExecuteNonQuery(string sql, CommandType type, SqlParameter[] ps)
        {
            int result = -1;
            using (SqlConnection con = GetConnection())
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(sql, con);
                if (ps != null)
                {
                    cmd.Parameters.AddRange(ps);
                }
                cmd.CommandType = type;
                result = cmd.ExecuteNonQuery();
            }
            return result;
        }

        /// <summary>
        /// 返回一个datatable数据表
        /// </summary>
        /// <param name="sql">sql语句或函数名或存储过程名</param>
        /// <param name="type">类型</param>
        /// <param name="ps">参数</param>
        /// <returns>返回datable</returns>
        public static DataTable ExecuteDataTable(string sql, CommandType type, SqlParameter[] ps)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = GetConnection())
            {
                using (SqlDataAdapter sda = new SqlDataAdapter(sql, con))
                {
                    if (ps != null)
                    {
                        sda.SelectCommand.Parameters.AddRange(ps);
                    }
                    sda.SelectCommand.CommandType = type;
                    sda.Fill(dt);
                }
            }
            return dt;
        }

        /// <summary>
        /// 构建参数
        /// </summary>
        /// <param name="dic">字典集合</param>
        /// <returns>sqlparameter数组</returns>
        public static SqlParameter[] BuildParameter(Dictionary<string, object> dic)
        {
            if (dic == null)
            {
                return null;
            }
            List<SqlParameter> list = new List<SqlParameter>();
            foreach (var item in dic)
            {
                list.Add(new SqlParameter(item.Key, item.Value));
            }
            return list.ToArray();
        }

        public static string BuildSql(string sql, string where, string order, bool isAsc)
        {
            if (sql == null || sql.Length == 0)
            {
                return null;
            }
            if (where.Length != 0)
            {
                sql = sql + " where " + where;
            }
            if (order.Length != 0)
	        {
                sql += " order by " + order;
                if (isAsc)
                {
                    sql += " asc";
                }
                else
                {
                    sql += " desc";
                }
	        }
            return sql;
        }
    }
}
