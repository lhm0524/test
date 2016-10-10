using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
namespace ZY.WEIKE.DALFACTORY
{
    public class AbstractFactory
    {
        private static readonly string NameSpace = System.Configuration.ConfigurationManager.AppSettings["namespace"];
        private static readonly string AssemblyPath = System.Configuration.ConfigurationManager.AppSettings["AssemblyPath"];

        public static IDAL.IColleageDAL CreateColleageDALInstance()
        {
            IDAL.IColleageDAL dal;
            dal = COMMONHELPER.CacheHelper.Get("IDAL_ColleageDAL") as IDAL.IColleageDAL;
            if (dal == null)
            {
                dal = CreateInstance(NameSpace + ".ColleageDAL") as IDAL.IColleageDAL;
                COMMONHELPER.CacheHelper.Add("IDAL_ColleageDAL", dal);
            }
            return dal;
        }

        public static IDAL.IDepartmentDAL CreateDepartmentDALInstance()
        {
            IDAL.IDepartmentDAL dal;
            dal = COMMONHELPER.CacheHelper.Get("IDAL_DepartmentDAL") as IDAL.IDepartmentDAL;
            if (dal == null)
            {
                dal = CreateInstance(NameSpace + ".DeparmentDAL") as IDAL.IDepartmentDAL;
                COMMONHELPER.CacheHelper.Add("IDAL_DepartmentDAL", dal);
            }
            return dal;
            //return null;
        }

        public static IDAL.IUserDAL CreateUserDALInstance()
        {
            IDAL.IUserDAL dal;
            dal = COMMONHELPER.CacheHelper.Get("IDAL_UserDAL") as IDAL.IUserDAL;
            if (dal == null)
            {
                dal = CreateInstance(NameSpace + ".UsersDAL") as IDAL.IUserDAL;
                COMMONHELPER.CacheHelper.Add("IDAL_UserDAL", dal);
            }
            return dal;
        }

        public static IDAL.IWeiKeDAL CreateWeiKeDALInstance()
        {
            //return new MSSQLDAL.WeiKeDAL();
            IDAL.IWeiKeDAL dal;
            dal = COMMONHELPER.CacheHelper.Get("IDAL_WeikeDAL") as IDAL.IWeiKeDAL;
            if (dal == null)
            {
                dal = CreateInstance(NameSpace + ".WeiKeDAL") as IDAL.IWeiKeDAL;
                COMMONHELPER.CacheHelper.Add("IDAL_WeikeDAL", dal);
            }
            return dal;
        }
        public static IDAL.IResourceDAL CreateResourceDALInstance()
        {
            IDAL.IResourceDAL dal;
            dal = COMMONHELPER.CacheHelper.Get("IDAL_ResourceDAL") as IDAL.IResourceDAL;
            if (dal == null)
            {
                dal = CreateInstance(NameSpace + ".ResourceDAL") as IDAL.IResourceDAL;
                COMMONHELPER.CacheHelper.Add("IDAL_ResourceDAL", dal);
            }

            return dal;
        }


        //public static T GetInstance<T>(string fullname, string key) where T : IDAL.IBaseDAL<T>
        //{
        //    T t;
        //    t = COMMONHELPER.CacheHelper.Get(key) as T;
        //    if (t == null)
        //    {
        //        t = CreateInstance(fullname) as T;
        //        COMMONHELPER.CacheHelper.Add(key, t);
        //    }
        //    return t;
        //}

        public static IDAL.IWordsDAL CreateWordsDALInstance()
        {
            IDAL.IWordsDAL dal;
            dal = COMMONHELPER.CacheHelper.Get("IDAL_IWordsDAL") as IDAL.IWordsDAL;
            if (dal == null)
            {
                dal = CreateInstance(NameSpace + ".WordsDAL") as IDAL.IWordsDAL;
                COMMONHELPER.CacheHelper.Add("IDAL_IWordsDAL", dal);
            }

            return dal;
        }

        public static IDAL.IVotesDAL CreateVotesDALInstance()
        {
            IDAL.IVotesDAL dal;
            dal = COMMONHELPER.CacheHelper.Get("IDAL_IVotesDAL") as IDAL.IVotesDAL;
            if (dal == null)
            {
                dal = CreateInstance(NameSpace + ".VotesDAL") as IDAL.IVotesDAL;
                COMMONHELPER.CacheHelper.Add("IDAL_IVotesDAL", dal);
            }
            return dal;
        }

        private static object CreateInstance(string fullname)
        {
            Assembly assembly = Assembly.Load(AssemblyPath);
            return assembly.CreateInstance(fullname);
        }
    }
}
