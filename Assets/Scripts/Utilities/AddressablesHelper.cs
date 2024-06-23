using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Utilities
{
    /// <summary>
    ///  Класс-помощник для управления адрессаблами
    /// </summary>
    public class AddressablesHelper
    {
        public bool InProgress => _inProgress > 0;
        
        private readonly HandlesDictionary _handles = new();
        private int _inProgress;
        
        public async UniTask<GameObject> GetGameObject(object key)
        {
            _inProgress++;
            var result = await _handles.GetHandleResult(key);
            _inProgress--;
            return result;
        }
        
        public void Dispose(object key)
        {
            _handles.Dispose(key).Forget();
        }

        public void DisposeAll()
        {
            _handles.DisposeAll();
        }

        public void ClearListOfUsedObjects()
        {
            _handles.ClearListOfUsedObjects();
        }

        public void DisposeUnused()
        {
            _handles.DisposeUnused();
        }
    }
    
    /// <summary>
    /// Сервисный класс для управления загруженными адрессаблами в единой библиотеке
    /// </summary>
    public class HandlesDictionary
    {
        private readonly Dictionary<object, AsyncOperationHandle<GameObject>> _handles = new();
        private readonly List<object> _keys = new();
        private readonly List<object> _used = new();

        public async UniTask<GameObject> GetHandleResult(object key)
        {
            if (key == null) return null;
            
            if (!_used.Contains(key))
            {
                _used.Add(key);
            }
            
            if (!_keys.Contains(key))
            {
                if (!_handles.ContainsKey(key))
                {
                    await AddHandle(key);
                }
                else
                {
                    await UniTask.WaitWhile(() => _handles.ContainsKey(key));
                    await AddHandle(key);
                }
            }

            if (!_handles.ContainsKey(key) && _keys.Contains(key))
            {
                await UniTask.WaitWhile(() => !_handles.ContainsKey(key));
            }

            return _handles[key].Result;
        }

        private async UniTask AddHandle(object key)
        {
            _keys.Add(key);
            try
            {
                var handle = Addressables.LoadAssetAsync<GameObject>(key);
                await handle;
                _handles.Add(key, handle);
            }
            catch (Exception e)
            {
                Debug.LogError($"ERROR ADDRESSABLE: {GetAssetName(key)} \n{e}");
            }
        }

        public async UniTask Dispose(object key)
        {
            if (_used.Contains(key))
            {
                _used.Remove(key);
            }
            
            if (!_keys.Contains(key)) return;
            _keys.Remove(key);
            
            if (!_handles.ContainsKey(key))
            {
                await UniTask.WaitWhile(() => !_handles.ContainsKey(key));
            }
            Addressables.ReleaseInstance(_handles[key]);
            _handles.Remove(key);
        }

        private string GetAssetName(object key)
        {
            AssetReference myRef = (AssetReference)key;
            var l = Addressables.LoadResourceLocationsAsync(myRef);
                var loc = l.Result[0];
                string myAssetNaming = loc.InternalId;
                return myAssetNaming;
        }

        public void DisposeAll()
        {
            foreach (var handle in _handles.Values)
            {
                Dispose(handle).Forget();
            }

            _handles.Clear();
        }
        
        public void ClearListOfUsedObjects()
        {
            _used.Clear();
        }

        public void DisposeUnused()
        {
            foreach (var handle in _handles.Values)
            {
                if (!_used.Contains(handle)) continue;
                Dispose(handle).Forget();
            }
        }
    }
}