using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Pools
{
    public class MonoPool<T> where T : MonoBehaviour
    {
        public Action<T> PrefabCreated = delegate(T behaviour) {  };
        public List<T> PrefabPool => _prefabPool;

        private readonly T _prefab;
        private readonly Transform _parent;
        private readonly List<T> _prefabPool;
        
        public MonoPool(T prefab, int initAmount, Transform parent)
        {
            _prefab = prefab;
            _parent = parent;
            _prefabPool = new List<T>();

            for (var i = 0; i < initAmount; i++)
            {
                CreatePrefab();
            }
        }

        public void Reenqueue()
        {
            var queue = new Queue<T>(_prefabPool);
            
            queue.Enqueue(queue.Dequeue());

            var enqueuedList = queue.ToList();
            
            for (var i = 0; i < _prefabPool.Count; i++)
            {
                _prefabPool[i] = enqueuedList[i];
            }
        }


        private void CreatePrefab()
        {
            var prefab = Object.Instantiate(_prefab, _parent);
            
            _prefabPool.Add(prefab);
            
            PrefabCreated(prefab);
        }
    }
}
