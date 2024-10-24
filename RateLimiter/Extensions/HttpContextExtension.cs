using Microsoft.AspNetCore.Http;
using RateLimiter.Attributes;

namespace RateLimiter.Extensions
{
    public static class HttpContextExtension
    {
        public static bool HasRateLimit(this HttpContext context, out RateLimitAttribute? rateLimitAttribute)
        {
            rateLimitAttribute = context.GetEndpoint()?.Metadata.GetMetadata<RateLimitAttribute>();
            return rateLimitAttribute is not null;
        }
    }
}