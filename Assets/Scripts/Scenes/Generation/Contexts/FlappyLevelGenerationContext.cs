using Data.Generators;
using DI.Signals;
using Pools;
using Scenes.Actors;
using Scenes.Generation.Base;
using Scenes.Generation.Factories;
using UnityEngine;
using Zenject;

namespace Scenes.Generation.Contexts
{
    public class FlappyLevelGenerationContext : ILevelGenerator
    {
        private readonly PipePareSpawnFactory _pipePareSpawnFactory;
        private readonly FlappyLevelGenerationSettings _settings;
        private readonly MonoPool<PipePare> _pipeParePool;
        
        private int _startSkipCount;//сколько пар было пропущено в начале (чтобы последняя не исчезала сразу)
        private int _pipesCount;//сколько пар труб было заспаунено

        
        
        
        protected FlappyLevelGenerationContext(
            FlappyLevelGenerationSettings levelGenerationSettings, 
            PipePareSpawnFactory pipePareSpawnFactory,
            SignalBus signalBus) 
        {
            signalBus.Subscribe<GamePointObtainedSignal>(x => Update());
            _pipeParePool = new MonoPool<PipePare>();
            _pipePareSpawnFactory = pipePareSpawnFactory;
            _settings = levelGenerationSettings;
            _startSkipCount = _settings.PipesStartSkipAmount;
        }

        
        
        public void Create()
        {
            for (var i = 0; i < _settings.PipesPoolAmount; i++)
            {
                _pipeParePool.AddObject(
                    _pipePareSpawnFactory.Create(new Vector2(
                        _settings.PipeStartPositionX + _settings.PipeDistance * i, 
                        0f)));
            }

            _pipesCount = _settings.PipesPoolAmount;

            //TODO Задний фон
        }

        
        
        public void Update()
        {
            if (_startSkipCount > 0)
            {
                _startSkipCount -= 1;
                return;
            }

            //ломаем ту что позади
            Object.Destroy(_pipeParePool.PrefabPool[0].gameObject);
            _pipeParePool.PrefabPool.RemoveAt(0);

            //делаем новую впереди
            _pipeParePool.AddObject(
                _pipePareSpawnFactory.Create(new Vector2(
                    _settings.PipeStartPositionX + _settings.PipeDistance * _pipesCount++, 
                    0f)));
        }
    }
}