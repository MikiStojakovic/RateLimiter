namespace RateLimiter.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class RateLimitAttribute : Attribute
    {
        public RateLimitAttribute(int rateLimitPeriodInMiliseconds, int maxRequestsPerTimePeriod)
        { 
            RateLimitPeriodInMiliseconds = rateLimitPeriodInMiliseconds;
            MaxRequestsPerTimePeriod = maxRequestsPerTimePeriod;
        }
        public int RateLimitPeriodInMiliseconds { get; set; } = 6000;
        public int MaxRequestsPerTimePeriod { get; set; } = 3;
    }
}