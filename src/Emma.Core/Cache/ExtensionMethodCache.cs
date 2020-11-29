using Emma.Core.MethodSources;

namespace Emma.Core.Cache
{
    public abstract class ExtensionMethodCache
    {
        public abstract bool Contains(string cacheId);
        public abstract ExtensionMethodsSource Get(string cacheId);

        public abstract void Remove(string cacheId);

        public abstract void Add(string cacheId, ExtensionMethodsSource extensionMethodsSource);
    }
}