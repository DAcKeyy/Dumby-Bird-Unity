using System;
using Scenes.Actors.FlappyBird;
using UnityEngine;
using Zenject;

namespace Scenes.Generation.Factories
{
    public class LandSpawnFactory : IFactory<Vector2, Land>
    {
        [Inject] private DiContainer _diContainer;
        private Land _land;
        private GameObject _landsParent;

        public LandSpawnFactory(LandSettings settings) {
            _landsParent = new GameObject("Land") {
                transform = {
                    parent = null, 
                    position = Vector3.zero
                }
            };

            _land = settings._land;
        }
        
        public Land Create(Vector2 position)
        {
            return _diContainer.InstantiatePrefab(
                    _land, position, Quaternion.identity, _landsParent.transform)
                    .GetComponent<Land>();
        }
        
        [Serializable]
        public struct LandSettings
        {
            public Land _land;
        }
    }
    
}