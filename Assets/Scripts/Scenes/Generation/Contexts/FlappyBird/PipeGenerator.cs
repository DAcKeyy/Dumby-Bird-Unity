using Data.Generators;
using Data.Pools;
using Scenes.Actors.FlappyBird;
using Scenes.Generation.Base;
using Scenes.Generation.Factories;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

namespace Scenes.Generation.Contexts.FlappyBird
{
    public class PipeGenerator : ILevelGenerator
    {
        private readonly PipePareSpawnFactory _pipePareSpawnFactory;
        private readonly FlappyLevelGenerationSettings.PipePareGenerationSettings _pipeParesSettings;
        private readonly MonoPool<PipePare> _pipeParePool;
        private readonly Vector2 _startPosition;
        
        private int _startPipesSkipCount;//сколько пар было пропущено в начале (чтобы последняя не исчезала сразу)
        private int _pipesCount;//сколько пар труб было заспаунено

        public PipeGenerator( 
            FlappyLevelGenerationSettings.PipePareGenerationSettings pipeSettings, 
            PipePareSpawnFactory pipePareSpawnFactory,
            Vector2 startPosition)
        {
            _pipeParesSettings = pipeSettings;
            _pipePareSpawnFactory = pipePareSpawnFactory;
            _startPosition = startPosition;
            _pipeParePool = new MonoPool<PipePare>();
            _startPipesSkipCount = pipeSettings.PipesStartSkipAmount;
        }

        public void Create()
        {
            for (var i = 0; i < _pipeParesSettings.PipesPoolAmount; i++)
            {
                float yOffset = 0;
                if (i != 0) yOffset = Random.Range(-_pipeParesSettings.PipeYOffset, _pipeParesSettings.PipeYOffset);
                _pipeParePool.AddObject(
                    _pipePareSpawnFactory.Create(new Vector2(
                        _pipeParesSettings.PipeStartPosition.x + _startPosition.x + _pipeParesSettings.PipeDistance * i, 
                        _pipeParesSettings.PipeStartPosition.y + _startPosition.y + yOffset)));
            }
            _pipesCount = _pipeParesSettings.PipesPoolAmount;
        }

        public void Update()
        {
            if (_startPipesSkipCount > 0)
            {
                _startPipesSkipCount -= 1;
                return;
            }
            
            //ломаем ту что позади
            Object.Destroy(_pipeParePool.PrefabPool[0].gameObject);
            _pipeParePool.PrefabPool.RemoveAt(0);

            //делаем новую впереди
            var yOffset = Random.Range(-_pipeParesSettings.PipeYOffset, _pipeParesSettings.PipeYOffset);
            _pipeParePool.AddObject(
                _pipePareSpawnFactory.Create(new Vector2(
                    _pipeParesSettings.PipeStartPosition.x + _startPosition.x + _pipeParesSettings.PipeDistance * _pipesCount++,
                    _pipeParesSettings.PipeStartPosition.y + _startPosition.y + yOffset)));
        }
    }
}