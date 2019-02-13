using Microsoft.Extensions.Caching.Memory;

namespace ToBuy.Middleware
{
    public class BuyMemoryCache
    {
        public MemoryCache Cache { get; set; }
        public BuyMemoryCache()
        {
            Cache = new MemoryCache(new MemoryCacheOptions
            {
                SizeLimit = 12
            });
        }
    }
}
