using Data.Generators;
using Data.Pools;
using Scenes.Actors.FlappyBird;
using Scenes.Generation.Base;
using Scenes.Generation.Factories;
using UnityEngine;

namespace Scenes.Generation.Contexts.FlappyBird
{
    public class CloudsGenerator : ILevelGenerator
    {
        private readonly FlappyLevelGenerationSettings.BackgroundGenerationSettings _settings;
        private readonly MonoPool<Cloud> _cloudsPool;
        private readonly CloudsFactory _cloudsFactory;

        private uint _cloudsCount;

        public CloudsGenerator(FlappyLevelGenerationSettings.BackgroundGenerationSettings settings, CloudsFactory cloudsFactory)
        {
            _cloudsFactory = cloudsFactory;
            _settings = settings;
            _cloudsPool = new MonoPool<Cloud>();
        }

        public void Create()
        {
            for (var i = 0; i < _settings.CloudsPoolAmount; i++)
            {
                _cloudsPool.AddObject(_cloudsFactory.Create(new Vector2(
                    _settings.CloudsStartPosition.x + _settings.CloudsDistance * i,
                    _settings.CloudsStartPosition.y)));
            }

            _cloudsCount = _settings.CloudsPoolAmount;
        }

        public void Update()
        {
            Object.Destroy(_cloudsPool.PrefabPool[0].gameObject);
            _cloudsPool.PrefabPool.RemoveAt(0);

            _cloudsPool.AddObject(_cloudsFactory.Create(new Vector2(
                _settings.CloudsStartPosition.x + _settings.CloudsDistance * _cloudsCount++,
                _settings.CloudsStartPosition.y)));
        }
    }
}