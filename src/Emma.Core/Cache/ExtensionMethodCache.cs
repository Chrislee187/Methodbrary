using Emma.Core.MethodSources;

namespace Emma.Core.Cache
{
    public interface IExtensionMethodCache
    {
        bool Contains(string cacheId);
        ExtensionMethodsSource Get(string cacheId);
        void Remove(string cacheId);
        void Add(string cacheId, ExtensionMethodsSource extensionMethodsSource);
    }

    public abstract class ExtensionMethodCache : IExtensionMethodCache
    {
        public abstract bool Contains(string cacheId);
        public abstract ExtensionMethodsSource Get(string cacheId);

        public abstract void Remove(string cacheId);

        public abstract void Add(string cacheId, ExtensionMethodsSource extensionMethodsSource);
    }
}