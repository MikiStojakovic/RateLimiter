using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RateLimiter.Data
{
    public class RateLimitData
    {
        public RateLimitData(DateTime lastResponseTime, int numberOfRequests)
        {
            LastResponseTime = lastResponseTime;
            NumberOfRequests = numberOfRequests;
        }
        [JsonProperty(PropertyName = "LastResponse")]
        public DateTime LastResponseTime { get; private set; }
        public int NumberOfRequests { get; private set; }
         public bool IsMaximumRequestTrasholdReached(int timeInMiliseconds, int maxRequests)
            => DateTime.UtcNow < LastResponseTime.AddMilliseconds(timeInMiliseconds) && NumberOfRequests == maxRequests;
        public void IncreaseRequests(int maxRequests)
        {
            LastResponseTime = DateTime.UtcNow;

            if (NumberOfRequests == maxRequests)
                NumberOfRequests = 1;
            else
                NumberOfRequests++;
        }
    }
}