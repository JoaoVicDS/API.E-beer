namespace APIEbeer.Services.Cache
{
    public interface ICacheService
    {
        public void Set<T>(string key, T value) where T : class;
        public T? Get<T>(string key) where T : class;
    }
}
