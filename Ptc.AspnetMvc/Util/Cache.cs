using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ptc.AspnetMvc.Util
{

    public static class Cache
    {

        // Gets a reference to the default MemoryCache instance. 
        private static System.Runtime.Caching.ObjectCache cache = System.Runtime.Caching.MemoryCache.Default;

        // private CacheEntryRemovedCallback callback = null;

        public static void AddToCache(String CacheKeyName, Object CacheItem, CachePriorityType MyCacheItemPriority)
        {
            System.Runtime.Caching.CacheItemPolicy policy = new System.Runtime.Caching.CacheItemPolicy();
            policy.Priority = (MyCacheItemPriority == CachePriorityType.Default) ?
                    System.Runtime.Caching.CacheItemPriority.Default : System.Runtime.Caching.CacheItemPriority.NotRemovable;
            policy.AbsoluteExpiration = DateTimeOffset.Now.AddHours(0.5);

            // Add inside cache 
            cache.Set(CacheKeyName, CacheItem, policy);
        }

        public static Object GetItem(String CacheKeyName)
        {
            // 
            return cache[CacheKeyName] as Object;
        }

        public static void RemoveItem(String CacheKeyName)
        {
            // 
            if (cache.Contains(CacheKeyName))
            {
                cache.Remove(CacheKeyName);
            }
        }

        /// <summary>
        /// 清除Cache
        /// </summary>
        public static void Clear()
        {
            var CacheList = cache.DefaultIfEmpty().ToList();
            foreach (var element in CacheList)
            {
                if (element.Key != null)    //身分過期不執行Remove
                    cache.Remove(element.Key);

            }
        }

    }
}
