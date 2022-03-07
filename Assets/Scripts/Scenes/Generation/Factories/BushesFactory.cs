using Scenes.Actors.FlappyBird;
using UnityEngine;
using Zenject;

namespace Scenes.Generation.Factories
{
    public class BushesFactory: IFactory<Vector2 ,Bush>
    {
        [Inject] private DiContainer _diContainer;
        private readonly BushesSettings _settings;
        private readonly GameObject _bushParent;

        public BushesFactory(BushesSettings settings)
        {
            _settings = settings;
            _bushParent = new GameObject("Bushes") {
                transform = {
                    position = Vector3.zero
                }
            };
        }

        public Bush Create(Vector2 position)
        {
            var bushObj = _diContainer.InstantiatePrefab(
                    _settings._bushPrefab,
                    position, 
                    Quaternion.identity, 
                    _bushParent.transform)
                .GetComponent<Bush>();
            
            return bushObj;
        }
        
        [System.Serializable]
        public struct BushesSettings
        {
            public Bush _bushPrefab;
            public Sprite _bushSprite;
        }
    }
}