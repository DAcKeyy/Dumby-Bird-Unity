using System.Collections.Generic;
using UnityEngine;

namespace Data.Pools
{
    public class MonoPool<T> where T : MonoBehaviour
    {
        public List<T> PrefabPool { get; }

        public MonoPool() {
            PrefabPool = new List<T>();
        }

        public void AddObject(T prefab)
        {
            PrefabPool.Add(prefab);
        }
    }
}
