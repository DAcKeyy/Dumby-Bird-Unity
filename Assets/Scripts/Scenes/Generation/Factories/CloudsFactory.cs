using Scenes.Actors.FlappyBird;
using UnityEngine;
using Zenject;

namespace Scenes.Generation.Factories
{
    public class CloudsFactory : IFactory<Vector2 ,Cloud>
    {
        [Inject] private DiContainer _diContainer;
        private readonly CloudsSettings _settings;
        private readonly GameObject _cloudsParent;

        public CloudsFactory(CloudsSettings settings)
        {
            _settings = settings;
            _cloudsParent = new GameObject("Clouds") {
                transform = {
                    position = Vector3.zero
                }
            };
        }
        
        public Cloud Create(Vector2 position)
        {
            var cloudObj = _diContainer.InstantiatePrefab(
                    _settings._cloudPrefab,
                    position, 
                    Quaternion.identity, 
                    _cloudsParent.transform)
                .GetComponent<Cloud>();
            
            return cloudObj;
        }
        
        [System.Serializable]
        public struct CloudsSettings
        {
            public Cloud _cloudPrefab;
            public Sprite _cloudSprite;
        } 
    }
}