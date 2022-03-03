using Data.Generators;
using Pools;
using Scenes.Actors.FlappyBird;
using Scenes.Generation.Base;
using Scenes.Generation.Factories;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

namespace Scenes.Generation.Contexts
{
    public class PipeGenerator : ILevelGenerator
    {
        private readonly PipePareSpawnFactory _pipePareSpawnFactory;
        private readonly FlappyLevelGenerationSettings.PipePareGenerationSettings _pipeParesSettings;
        private readonly MonoPool<PipePare> _pipeParePool;
        
        private int _startPipesSkipCount;//сколько пар было пропущено в начале (чтобы последняя не исчезала сразу)
        private int _pipesCount;//сколько пар труб было заспаунено
        
        public PipeGenerator( 
            FlappyLevelGenerationSettings.PipePareGenerationSettings pipeSettings, 
            PipePareSpawnFactory pipePareSpawnFactory)
        {
            _pipeParesSettings = pipeSettings;
            _pipePareSpawnFactory = pipePareSpawnFactory;
            _pipeParePool = new MonoPool<PipePare>();
            _startPipesSkipCount = pipeSettings.PipesStartSkipAmount;
        }
        
        public void Create()
        {
            for (var i = 0; i < _pipeParesSettings.PipesPoolAmount; i++)
            {
                _pipeParePool.AddObject(
                    _pipePareSpawnFactory.Create(new Vector2(
                        _pipeParesSettings.PipeStartPositionX + _pipeParesSettings.PipeDistance * i, 
                        0f)));
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
            _pipeParePool.AddObject(
                _pipePareSpawnFactory.Create(new Vector2(
                    _pipeParesSettings.PipeStartPositionX + _pipeParesSettings.PipeDistance * _pipesCount++,
                    0f)));
        }
    }
}