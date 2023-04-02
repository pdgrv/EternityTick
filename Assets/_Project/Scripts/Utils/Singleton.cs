namespace Eternity.Utils
{
    public abstract class Singleton<T> where T : Singleton<T>, new()
    {
        private static readonly object _lock = new();
        static T _instance = null;

        public static T I 
        {
            get
            {
                lock (_lock)
                    return _instance ??= new T();
            }
        }
    }
}