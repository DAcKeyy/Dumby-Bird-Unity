using System;
using Scenes.Actors.FlappyBird;
using UnityEngine;
using Zenject;

namespace Scenes.Generation.Factories
{
    public class LandSpawnFactory : IFactory<Vector2, Land>
    {
        [Inject] private DiContainer _diContainer;
        private readonly LandSettings _landSettings;
        private readonly GameObject _landsParent;

        public LandSpawnFactory(LandSettings settings) {
            _landSettings = settings;
            _landsParent = new GameObject("Land") {
                transform = {
                    position = Vector3.zero
                }
            };
        }
        
        public Land Create(Vector2 position)
        {
            return _diContainer.InstantiatePrefab(
                    _landSettings._landPrefab, 
                    position, 
                    Quaternion.identity, 
                    _landsParent.transform)
                    .GetComponent<Land>();
        }
        
        [Serializable]
        public struct LandSettings
        {
            public Land _landPrefab;
        }
    }
    
}