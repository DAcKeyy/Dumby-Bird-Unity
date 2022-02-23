using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Pools
{
    public class MonoPool<T> where T : MonoBehaviour
    {
        public Action<T> PrefabCreated = delegate(T behaviour) {  };
        public List<T> PrefabPool => _prefabPool;

        private readonly T _prefab;
        private readonly Transform _parent;
        private readonly List<T> _prefabPool;

        public MonoPool() {
            _prefabPool = new List<T>();
        }

        public MonoPool(T prefab) {
            _prefab = prefab;
            _prefabPool = new List<T>();
        }

        public MonoPool(T prefab, int initAmount, Transform parent) {
            _prefab = prefab;
            _parent = parent;
            _prefabPool = new List<T>();

            for (var i = 0; i < initAmount; i++) {
                CreatePrefab();
            }
        }
        
        public void AddObject(T prefab)
        {
            _prefabPool.Add(prefab);
        }
        
        public List<T> ShiftPool(MoveDirection direction)
        {
            var queue = new Queue<T>(_prefabPool);
            List<T> enqueuedList;
            
            //TODO чОТО не то
            switch (direction) {
                case MoveDirection.Left:
                    queue.Enqueue(queue.Dequeue());
                    enqueuedList = queue.ToList();
                    break;
                case MoveDirection.Right:
                    enqueuedList = _prefabPool;
                    (enqueuedList[_prefabPool.Count - 1], enqueuedList[0]) = (enqueuedList[0], enqueuedList[_prefabPool.Count - 1]);
                    break;
                default:
                    Debug.LogWarning($"{direction} for PoolShift in MonoPool not implemented yet.");
                    return null;
            }

            
            for (var i = 0; i < _prefabPool.Count; i++)
            {
                _prefabPool[i] = enqueuedList[i];
            }

            return _prefabPool;
        }
        
        private void CreatePrefab()
        {
            var prefab = UnityEngine.Object.Instantiate(_prefab, _parent);
            
            _prefabPool.Add(prefab);
            
            PrefabCreated(prefab);
        }
    }
}
