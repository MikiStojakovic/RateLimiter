using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using RateLimiter.Data;

namespace RateLimiter.Extensions
{
    public static class DistributedCacheExtension
    {
        public async static Task<RateLimitData?> GetCustomerRateLimitDataFromContextAsync(
            this IDistributedCache cache,
            HttpContext context,
            CancellationToken cancellation = default)
            {
                var result = await cache.GetStringAsync(context.GetCustomerKey(), cancellation);
                if (result is null)
                    return null;

                return JsonConvert.DeserializeObject<RateLimitData>(result);
            }
    }
}