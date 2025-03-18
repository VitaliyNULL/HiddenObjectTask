using System.Collections.Generic;
using UnityEngine;

namespace HiddenObjectGame.Runtime.Services
{
    public class ObjectPool<T> where T : MonoBehaviour, new()
    {
        private Queue<T> _pool;
        private readonly int _maxSize;
        private readonly int _initialSize;
        private readonly int _growthRate;

        public ObjectPool(T prefab, int initialSize = 5, int maxSize = 10, int growthRate = 1)
        {
            _initialSize = initialSize;
            _maxSize = maxSize;
            _growthRate = growthRate;
            _pool = new Queue<T>();

            for (int i = 0; i < initialSize; i++)
            {
                var instance = Object.Instantiate(prefab);
                InitializePoolObject(instance);
                instance.gameObject.SetActive(false);
                _pool.Enqueue(instance);
            }
        }

        protected virtual void InitializePoolObject(T instance)
        {
            
        }

        public T Get()
        {
            if (_pool.Count > 0)
            {
                return _pool.Dequeue();
            }
            else
            {
                return ExpandPool();
            }
        }

        public void Return(T item)
        {
            if (_pool.Count < _maxSize)
            {
                _pool.Enqueue(item);
            }
        }

        private T ExpandPool()
        {
            int expandCount = Mathf.Min(_growthRate, _maxSize - _pool.Count);
            for (int i = 0; i < expandCount; i++)
            {
                _pool.Enqueue(new T());
            }

            return _pool.Dequeue();
        }


        public int CurrentSize => _pool.Count;
    }
}