using UnityEngine;
using UnityEngine.Pool;

namespace Utilities
{
    /// <summary>
    /// Стандартная реализация для пула объектов Unity
    /// </summary>
    /// <typeparam name="T">Тип объектов, из которых состоит пул</typeparam>
    public class DefaultObjectPool<T> where T : MonoBehaviour
    {
        public IObjectPool<T> Value => _pool;
        
        private readonly IObjectPool<T> _pool;
        private readonly T _prefab;
        private readonly Transform _parent;
        
        public DefaultObjectPool(T prefab, Transform parent = null, int startingSize = 1, int maxSize = 10000)
        {
            _prefab = prefab;
            _parent = parent;
            _pool = new ObjectPool<T>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, true,
                startingSize, maxSize);
        }
        
        private T CreatePooledItem()
        {
            var result = Object.Instantiate(_prefab, _parent);
            result.gameObject.SetActive(false);
            return result;
        }

        private void OnReturnedToPool(T element)
        {
            element.gameObject.SetActive(false);
        }

        private void OnTakeFromPool(T element)
        {
            element.gameObject.SetActive(true);
        }

        private void OnDestroyPoolObject(T element)
        {
            Object.Destroy(element.gameObject);
        }
    }
}