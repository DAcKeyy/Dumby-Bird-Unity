using Data.Generators;
using Data.Pools;
using Scenes.Actors.FlappyBird;
using Scenes.Generation.Base;
using Scenes.Generation.Factories;
using UnityEngine;

namespace Scenes.Generation.Contexts.FlappyBird
{
    public class BushesGenerator : ILevelGenerator
    {
        private readonly FlappyLevelGenerationSettings.BackgroundGenerationSettings _settings;
        private readonly MonoPool<Bush> _bushesPool;
        private readonly BushesFactory _bushesFactory;

        private uint _bushesCount;
        
        public BushesGenerator(FlappyLevelGenerationSettings.BackgroundGenerationSettings settings, BushesFactory bushesFactory)
        {
            _bushesFactory = bushesFactory;
            _settings = settings;
            _bushesPool = new MonoPool<Bush>();
        }

        public void Create()
        {
            for (var i = 0; i < _settings.BushesPoolAmount; i++)
            {
                _bushesPool.AddObject(_bushesFactory.Create(new Vector2(
                    _settings.BushesStartPosition.x + _settings.BushesDistance * i,
                    _settings.BushesStartPosition.y)));
            }

            _bushesCount = _settings.BushesPoolAmount;
        }

        public void Update()
        {
            Object.Destroy(_bushesPool.PrefabPool[0].gameObject);
            _bushesPool.PrefabPool.RemoveAt(0);
            
            _bushesPool.AddObject(_bushesFactory.Create(new Vector2(
                _settings.BushesStartPosition.x + _settings.BushesDistance * _bushesCount++,
                _settings.BushesStartPosition.y)));
        }
    }
}