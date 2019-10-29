using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Ptc.AspnetMvc.Util
{
    /// <summary>
    /// Cache Extensions
    /// </summary>
    public static class CacheExtensions
    {
        private static object sync = new object();

        private class Container<T>
        {
            public T Value;
        }

        /// <summary>
        /// 更新Cache
        /// </summary>
        /// <param name="cache"></param>
        /// <param name="key"></param>
        public static bool UpdateStore<TValue>(this System.Runtime.Caching.ObjectCache cache,
            string key,
            TValue item)
        {
            bool result = false;

            if (string.IsNullOrEmpty(key))
                return result;

            if (cache.Contains(key))//檢查cache 是否有對應的資料
            {
                var instance = cache[key];//取得該筆資料

                lock (instance)
                {
                    var instanceValue = ((Util.CacheExtensions.Container<TValue>)instance).Value;//取的該筆資料的值

                    foreach (var property in instanceValue.GetType().GetProperties())//取得該筆資料的型別與內容物件
                    {
                        if (item.GetType().GetProperty(property.Name) != null)
                            property.SetValue(instanceValue, item.GetType().GetProperty(property.Name).GetValue(item));//更新內容
                    }
                    result = true;
                }
            }

            return result;
        }

        /// <summary>
        /// 移除Cache
        /// </summary>
        /// <param name="cache"></param>
        /// <param name="key"></param>
        public static void RemoveStore(this System.Runtime.Caching.ObjectCache cache,
          string key)
        {
            if (string.IsNullOrEmpty(key))
                return;

            lock (cache)
            {
                if (cache.Contains(key))
                    cache.Remove(key);
            }
        }

        #region GetOrStore 多載方法

        public static TValue GetOrStoreAndNotRemove<TValue>(this System.Runtime.Caching.ObjectCache cache,
            string key,
            Func<TValue> create)
        {
            return cache.GetOrStore(key, create, 
                System.Runtime.Caching.MemoryCache.InfiniteAbsoluteExpiration,
                System.Runtime.Caching.MemoryCache.NoSlidingExpiration);
        }
        
        public static TValue GetOrStore<TValue>(this System.Runtime.Caching.ObjectCache cache,
          string key,
          Func<TValue> create)
        {
            return cache.GetOrStore(key, create, DateTime.Now.AddHours(2), new TimeSpan(1, 0, 0));
        }


        public static TValue GetOrStore<TValue>(this System.Runtime.Caching.ObjectCache cache,
            string key,
            Func<TValue> create,
            TimeSpan slidingExpiration)
        {
            return cache.GetOrStore(key, create, System.Runtime.Caching.ObjectCache.InfiniteAbsoluteExpiration, slidingExpiration);
        }

        public static TValue GetOrStore<TValue>(this System.Runtime.Caching.ObjectCache cache,
            string key,
            Func<TValue> create,
            DateTime absoluteExpiration)
        {
            return cache.GetOrStore(key, create, absoluteExpiration, System.Runtime.Caching.ObjectCache.NoSlidingExpiration);
        }

        #endregion

        #region GetOrStore

        /// <summary>
        /// 檢查快取位址是否有值，無則使用create查詢1
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="cache"></param>
        /// <param name="key"></param>
        /// <param name="create"></param>
        /// <param name="absoluteExpiration"></param>
        /// <param name="slidingExpiration"></param>
        /// <returns></returns>
        public static TValue GetOrStore<TValue>(this System.Runtime.Caching.ObjectCache cache,
            string key,
            Func<TValue> create,
            DateTimeOffset absoluteExpiration, TimeSpan slidingExpiration)
        {
            var instance = cache.GetOrStoreContainer<TValue>(key, absoluteExpiration, slidingExpiration);
            if (instance.Value == null)
                lock (instance)
                    if (instance.Value == null)
                        instance.Value = create();

            return instance.Value;
        }

        /// <summary>
        /// 檢查快取位址是否有值，無則使用create查詢2
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="cache"></param>
        /// <param name="key"></param>
        /// <param name="create"></param>
        /// <param name="absoluteExpiration"></param>
        /// <param name="slidingExpiration"></param>
        /// <returns></returns>
        public static TValue GetOrStore<TValue>(this System.Runtime.Caching.ObjectCache cache,
            string key,
            Func<string, TValue> create,
            DateTimeOffset absoluteExpiration, TimeSpan slidingExpiration)
        {
            var instance = cache.GetOrStoreContainer<TValue>(key, absoluteExpiration, slidingExpiration);
            if (instance.Value == null)
                lock (instance)
                    if (instance.Value == null)
                        instance.Value = create(key);

            return instance.Value;
        }

        #endregion

        /// <summary>
        /// 檢查是否存在快取位址，無則創建
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="cache"></param>
        /// <param name="key"></param>
        /// <param name="absoluteExpiration"></param>
        /// <param name="slidingExpiration"></param>
        /// <returns></returns>
        private static Container<TValue> GetOrStoreContainer<TValue>(this System.Runtime.Caching.ObjectCache cache, string key, DateTimeOffset absoluteExpiration, TimeSpan slidingExpiration)
        {
            var instance = cache[key];
            if (instance == null)
                lock (cache)
                {
                    instance = cache[key];
                    if (instance == null)
                    {
                        instance = new Container<TValue>();
                        System.Runtime.Caching.CacheItemPolicy policy = new System.Runtime.Caching.CacheItemPolicy();
                        policy.Priority = System.Runtime.Caching.CacheItemPriority.Default;
                        policy.AbsoluteExpiration = absoluteExpiration;
                        // policy.SlidingExpiration = slidingExpiration;
                        policy.UpdateCallback = CacheItemRemoved;
                        cache.Set(key, instance, policy);

                    }
                }

            return (Container<TValue>)instance;
        }

        /// <summary>
        /// 檢查Cache是否移除
        /// </summary>
        /// <param name="args"></param>
        private static void CacheItemRemoved(System.Runtime.Caching.CacheEntryUpdateArguments args)
        {
            if (args.RemovedReason == System.Runtime.Caching.CacheEntryRemovedReason.Expired || args.RemovedReason == System.Runtime.Caching.CacheEntryRemovedReason.Removed)
            {
                //var updatedEntity = _baseRepository.GetById(id);
                //args.UpdatedCacheItem = new CacheItem(id, updatedEntity);
                //args.UpdatedCacheItemPolicy = GetPolicy();
            }
        }
    }
}
