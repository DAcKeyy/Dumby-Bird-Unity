using Data.Generators;
using Pools;
using Scenes.Actors;
using Scenes.Generation.Base;
using Scenes.Generation.Factories;
using UnityEngine;

namespace Scenes.Generation.Contexts
{
    public class FlappyLevelGenerationContext : ILevelGenerator
    {
        private readonly FlappyLevelGenerationSettings _settings;
        private readonly PipePareCreator _pipePareCreator;
        private readonly MonoPool<PipePare> _pipeParePool;

        protected FlappyLevelGenerationContext(FlappyLevelGenerationSettings levelGenerationSettings) {
            _settings = levelGenerationSettings;
            _pipePareCreator = new PipePareCreator(_settings.PipeSettings);
            _pipeParePool = new MonoPool<PipePare>();
        }

        public void Create()
        {
            for (var i = 0; i < _settings.PipesPoolAmount; i++)
            {
                _pipeParePool.AddObject(
                    _pipePareCreator.Create(new Vector2(
                        _settings.PipeStartPositionX + _settings.PipeDistance * i, 
                        0f)));
            }
        }
    }
}