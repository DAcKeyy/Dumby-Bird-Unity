using System;
using Scenes.Generation.Factories;
using UnityEngine;

namespace Data.Generators
{
    [Serializable]
    public class FlappyLevelGenerationSettings
    {
        public float PipeStartPositionX => _pipeStartPositionX;
        public float PipeDistance => _pipeDistance;
        public int PipesPoolAmount => _pipesPoolAmount;
        public int PipesStartSkipAmount => _pipesStartSkipAmount;
        public PipePareSpawnFactory.PipePareSettings PipeSettings => _pipeSettings;

        [SerializeField] [Range(-10,10f)] private float _pipeStartPositionX = 4f;
        [SerializeField] [Range(1,5f)] private float _pipeDistance = 2f;
        [SerializeField] [Range(5,20)] private int _pipesPoolAmount = 10;
        [SerializeField] [Range(1,5)] private int _pipesStartSkipAmount = 4;
        [SerializeField] private PipePareSpawnFactory.PipePareSettings _pipeSettings;
    }
}
