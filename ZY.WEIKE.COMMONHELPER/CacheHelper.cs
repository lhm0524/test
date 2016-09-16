using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZY.WEIKE.COMMONHELPER
{
    public class CacheHelper
    {
        public static object Get(string key)
        {
            System.Web.Caching.Cache cache = System.Web.HttpRuntime.Cache;
            return cache.Get(key);
        }
        public static void Add(string key, object value)
        {
            System.Web.Caching.Cache cache = System.Web.HttpRuntime.Cache;
            cache.Insert(key, value);
        }
    }
}
