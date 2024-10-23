using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;

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
            await _next(context);
        }
    }
}