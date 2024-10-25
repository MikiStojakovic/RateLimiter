using Microsoft.AspNetCore.Http;
using RateLimiter.Attributes;

namespace RateLimiter.Extensions
{
    public static class HttpContextExtension
    {
        public static bool HasRateLimit(this HttpContext context)
        {
            var rateLimitAttribute = context.GetRateLimitAttributeData();
            return rateLimitAttribute is not null;
        }
        public static RateLimitAttribute? GetRateLimitAttributeData(this HttpContext context)
        {
            return context.GetEndpoint()?.Metadata.GetMetadata<RateLimitAttribute>();
        }
        public static string GetCustomerKey(this HttpContext context)
        => $"{context.Request.Path}_{context.Connection.RemoteIpAddress}";
    }
}