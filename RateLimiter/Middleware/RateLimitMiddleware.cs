using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using RateLimiter.Extensions;

namespace RateLimiter.Middleware
{
    public class RateLimitMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IDistributedCache _distributedCache;

        public RateLimitMiddleware(RequestDelegate next, IDistributedCache distributedCache)
        {
            _next = next;
            _distributedCache = distributedCache;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.HasRateLimit())
            {
                await _next(context);
                return;
            }

            var rateLimitDataData = await _distributedCache.GetCustomerRateLimitDataFromContextAsync(context);
            if (rateLimitDataData is not null)
            {
                var rateLimitAttribute = context.GetRateLimitAttributeData();
                 if (rateLimitDataData.IsMaximumRequestTrasholdReached(rateLimitAttribute!.RateLimitPeriodInMiliseconds, rateLimitAttribute!.MaxRequestsPerTimePeriod))
                {
                    context.Response.StatusCode = (int)HttpStatusCode.TooManyRequests;
                    return;
                }
            }

            await _distributedCache.SetCacheValueAsync(context.GetCustomerKey(), rateLimitDataData);

            await _next(context);
        }
    }
}