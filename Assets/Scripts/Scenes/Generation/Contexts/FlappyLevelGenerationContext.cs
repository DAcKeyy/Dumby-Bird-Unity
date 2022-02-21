using System;
using Pools;
using Scenes.Actors;
using Scenes.Generation.Base;
using Scenes.Generation.Factories;
using UnityEngine;

namespace Scenes.Generation.Contexts
{
    [Serializable]
    public class FlappyLevelGenerationContext : ILevelGenerator
    {
        private const float PIPE_START_POSITION_X = 4f;
        private const float PIPE_DISTANCE = 2f;
        private const int PIPES_POOL_AMOUNT = 10;

        private PipePareCreator _pipePareCreator;
        private MonoPool<PipePare> _pipeParePool;

        public FlappyLevelGenerationContext(PipePareCreator.PipePareSettings pipePareSettings) {
            _pipePareCreator = new PipePareCreator(pipePareSettings);
        }

        public void Create()
        {
            _pipeParePool = new MonoPool<PipePare>();
            
            for (var i = 0; i < PIPES_POOL_AMOUNT; i++)
            {
                _pipeParePool.AddObject(
                    _pipePareCreator.CreatePipePare(new Vector2(
                        PIPE_START_POSITION_X + PIPE_DISTANCE * i, 
                        0f)));
            }
        }
    }
}