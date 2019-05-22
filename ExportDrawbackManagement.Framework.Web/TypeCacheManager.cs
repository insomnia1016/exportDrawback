using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Caching;

namespace ExportDrawbackManagement.Framework.Web
{
    /// <summary>
    /// 类型化缓存管理器
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class TypeCacheManager<T>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TypeCacheManager()
        {
        }
        /// <summary>
        /// 向缓存添加数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        /// <param name="timeSpan"></param>
        public void Add(string key,T obj,TimeSpan timeSpan)
        {
            HttpContext.Current.Cache.Add(key, obj, null, DateTime.MaxValue, timeSpan, CacheItemPriority.Default, null);
        }
        /// <summary>
        /// 向缓存添加数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        public void Add(string key, T obj)
        {
            Add(key,obj,TimeSpan.FromMinutes(10));
        }
        /// <summary>
        /// 从缓存中获取数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public T Get(string key)
        {
            object obj = HttpContext.Current.Cache[key];
            if (obj != null && obj is T)
            {
                return (T)obj;
            }
            return default(T);
        }
        /// <summary>
        /// 从缓存中获取数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool TryGet(string key, out T obj)
        {
            try
            {
                obj = Get(key);
                return obj != null;
            }
            catch 
            {
                obj = default(T);
                return false;
            }
            
        }
    }
}
