using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bcp.Test.Infrastructure.Repository
{
    public static class CacheExtensions
    {
        public static async Task<T> GetCacheValueAsync<T>(this IDistributedCache cache, string key) where T : class
        {
            string result = await cache.GetStringAsync(key);
            if (String.IsNullOrEmpty(result))
            {
                return null;
            }
            var deserializedObj = JsonConvert.DeserializeObject<T>(result);
            return deserializedObj;
        }

        public static async Task SetCacheValueAsync<T>(this IDistributedCache cache, string key, T value) where T : class
        {
            DistributedCacheEntryOptions cacheEntryOptions = new DistributedCacheEntryOptions();

            // Remove item from cache after duration
            cacheEntryOptions.AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1);

            // Remove item from cache if unsued for the duration
            cacheEntryOptions.SlidingExpiration = TimeSpan.FromDays(1);

            string result = JsonConvert.SerializeObject(value);

            await cache.SetStringAsync(key, result);
        }
    }
}
