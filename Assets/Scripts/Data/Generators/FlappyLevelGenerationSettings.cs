using System;
using Scenes.Generation.Factories;
using UnityEngine;

namespace Data.Generators
{
    [Serializable]
    public class FlappyLevelGenerationSettings
    {
        public PipePareGenerationSettings _pipePareGenerationSettings;
        public LandGenerationSettings _landGenerationSettings;
        
        [Serializable]
        public class PipePareGenerationSettings
        {
            public PipePareSpawnFactory.PipePareSettings PipeSettings => _pipeSettings;
            public float PipeStartPositionX => _pipeStartPositionX;
            public float PipeDistance => _pipeDistance;
            public int PipesPoolAmount => _pipesPoolAmount;
            public int PipesStartSkipAmount => _pipesStartSkipAmount;
            
            [SerializeField] private PipePareSpawnFactory.PipePareSettings _pipeSettings;
            [SerializeField] [Range(-10,10f)] private float _pipeStartPositionX = 3f;
            [SerializeField] [Range(1,5f)] private float _pipeDistance = 4f;
            [SerializeField] [Range(5,20)] private int _pipesPoolAmount = 6;
            [SerializeField] [Range(1,5)] private int _pipesStartSkipAmount = 3;
        }
        
        [Serializable]
        public class LandGenerationSettings
        {
            public LandSpawnFactory.LandSettings LandSettings => _landSettings;
            public float LandStartPositionX => _landStartPositionX;
            public float LandsDistance => _landsDistance;
            public int LandPoolAmount => _landPoolAmount;
            public int LandStartSkipAmount => _landStartSkipAmount;
            
            [SerializeField] private LandSpawnFactory.LandSettings _landSettings;
            [SerializeField] private float _landStartPositionX = -4f;
            [SerializeField] private float _landsDistance = 1.5f;
            [SerializeField] private int _landPoolAmount = 5;
            [SerializeField] private int _landStartSkipAmount = 3;
        }
    }
}
