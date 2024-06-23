using UnityEngine;

namespace Utilities
{
    /// <summary>
    /// Реализация паттерна Singleton для классов ScriptableObject
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class StaticScriptableObject<T> : ScriptableObject where T : ScriptableObject
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    var path = $"Settings/{typeof(T).Name}";
                    _instance = Resources.Load(path) as T;
                }

                return _instance;
            }
        }
    }
}