namespace Codetecuico.Byns.Common.Core
{
    public class CacheHelper
    {
        private readonly int cacheExpiration = 60;

        //public T GetOrSet<T>(string cacheKey, Func<T> getItemCallback) where T : class
        //{
        //    var item = MemoryCache.Default.Get(cacheKey) as T;
        //    if (item == null)
        //    {
        //        item = getItemCallback();
        //        MemoryCache.Default.Add(cacheKey, item, DateTime.Now.AddMinutes(cacheExpiration));
        //    }
        //    return item;
        //}
    }
}