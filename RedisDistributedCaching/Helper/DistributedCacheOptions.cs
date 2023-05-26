using Microsoft.Extensions.Caching.Distributed;

namespace RedisDistributedCaching.Helper
{
    public static class DistributedCacheOptions
    {
        public static DistributedCacheEntryOptions GetDistributedCacheEntryOptions()
        {

          return  new DistributedCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600));
        }
    }
}
