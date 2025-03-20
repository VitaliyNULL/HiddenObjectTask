using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace HiddenObjectGame.Runtime.VFX
{
    public class ObjectPool<T> where T : MonoBehaviour, new()
    {
        private Queue<T> _pool;
        private readonly int _maxSize;
        private readonly int _initialSize;
        private readonly int _growthRate;
        private AssetReference _assetReference;

        public ObjectPool(AssetReference assetReferenceRef, int initialSize = 5, int maxSize = 10, int growthRate = 1)
        {
            _initialSize = initialSize;
            _maxSize = maxSize;
            _growthRate = growthRate;
            _pool = new Queue<T>();
            _assetReference = assetReferenceRef;
            Initialize().Forget();
        }

        private async UniTaskVoid Initialize()
        {
            Debug.Log(_assetReference);
            var loadAsset = Addressables.LoadAssetAsync<GameObject>(_assetReference);
            await loadAsset;
            Debug.Log($"Loaded asset {loadAsset.IsDone} : {loadAsset.Result}");
            T prefab = loadAsset.Result.GetComponent<T>();
            for (int i = 0; i < _initialSize; i++)
            {
                var instance = Object.Instantiate(prefab);
                InitializePoolObject(instance);
                instance.gameObject.SetActive(false);
                _pool.Enqueue(instance);
            }

            Addressables.Release(loadAsset);
        }

        protected virtual void InitializePoolObject(T instance)
        {
        }

        public async UniTask<T> Get()
        {
            if (_pool.Count > 0)
            {
                return _pool.Dequeue();
            }
            else
            {
                var result = await ExpandPool();
                return result;
            }
        }

        public void Return(T item)
        {
            if (_pool.Count < _maxSize)
            {
                _pool.Enqueue(item);
            }
        }

        private async UniTask<T> ExpandPool()
        {
            var loadAsset = Addressables.LoadAssetAsync<GameObject>(_assetReference);
            await loadAsset;
            T prefab = loadAsset.Result.GetComponent<T>();
            int expandCount = Mathf.Min(_growthRate, _maxSize - _pool.Count);
            for (int i = 0; i < expandCount; i++)
            {
                var instance = Object.Instantiate(prefab);
                InitializePoolObject(instance);
                instance.gameObject.SetActive(false);
                _pool.Enqueue(instance);
            }

            Addressables.Release(loadAsset);

            return _pool.Dequeue();
        }


        public int CurrentSize => _pool.Count;
    }
}