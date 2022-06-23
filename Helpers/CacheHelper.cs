using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace redis_explorations.Helpers
{
	public interface ICacheHelper
	{
		public bool TryGetValue<T>(string key, out T? value);
		public void Set<T>(string key, T value);
	}

	public class CacheHelper : ICacheHelper
	{
        private readonly IDistributedCache distributedCache;

        public CacheHelper(IDistributedCache distributedCache)
		{
            this.distributedCache = distributedCache;
        }

        public bool TryGetValue<T>(string key, out T? value)
        {
            var jsonValue = distributedCache.GetString(key);
            if (jsonValue is null)
            {
                value = default;
                return false;
            }
            else
            {
                value = JsonSerializer.Deserialize<T>(jsonValue)!;
                return true;
            }
        }

        public void Set<T>(string key, T value)
        {
            distributedCache.SetString(key, JsonSerializer.Serialize(value));
        }
    }
}

