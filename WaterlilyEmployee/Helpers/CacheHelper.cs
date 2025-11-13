using Microsoft.Extensions.Caching.Memory;

namespace WaterlilyEmployee.Helpers
{
    public class CacheHelper
    {
        private readonly IMemoryCache _cache;
        public CacheHelper(IMemoryCache cache) { _cache = cache; }
    
        public T Cache<T>(string key, Func<T> factory)
        {
            if (!_cache.TryGetValue(key, out T value)) 
            {
                value = factory();
                _cache.Set(key, value, TimeSpan.FromMinutes(5));
            }
            return value;
        }

        public T CachedLong<T>(string key, Func<T> factory)
        {
            if(!_cache.TryGetValue(key, out T value))
            {
                value = factory();
                _cache.Set(key, value);
            }
            return value;
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
        }
    }
}
