using System;
using Scenes.Generation.Factories;
using UnityEngine;

namespace Data.Generators
{
    [Serializable]
    public struct FlappyLevelGenerationSettings
    {
        public PipePareGenerationSettings _pipePareGenerationSettings;
        public LandGenerationSettings _landGenerationSettings;
        public BackgroundGenerationSettings _backgroundGenerationSettings;

        [Serializable]
        public class BackgroundGenerationSettings
        {
            public BushesFactory.BushesSettings BushesSettings => _bushesSettings;
            public float BushesDistance => _bushesDistance;
            public uint BushesPoolAmount => _bushesPoolAmount;
            public Vector2 BushesStartPosition => _bushesStartPosition;
            
            public CloudsFactory.CloudsSettings CloudsSettings => _cloudsSettings;
            public float CloudsDistance => _cloudsDistance;
            public uint CloudsPoolAmount => _cloudsPoolAmount;
            public Vector2 CloudsStartPosition => _cloudsStartPosition;

            public BuildingsFactory.BuildingsSettings BuildingsSettings => _buildingsSettings;
            public float BuildingsDistance => _buildingDistance;
            public uint BuildingsPoolAmount => _buildingsPoolAmount;
            public Vector2 BuildingsStartPosition => _buildingsStartPosition;
            
            [Header("Облака")]
            [SerializeField] private CloudsFactory.CloudsSettings _cloudsSettings;
            [SerializeField] private Vector2 _cloudsStartPosition = new Vector2(-4f, 0);
            [SerializeField] [Range(0,10f)] private float _cloudsDistance = 2f;
            [SerializeField] [Range(0,10)] private uint _cloudsPoolAmount = 6;
            [Header("Постройки")]
            [SerializeField] private BuildingsFactory.BuildingsSettings _buildingsSettings;
            [SerializeField] private Vector2 _buildingsStartPosition = new Vector2(-4f, 0);
            [SerializeField] [Range(0,10)] private float _buildingDistance = 2f;
            [SerializeField] [Range(0,10)] private uint _buildingsPoolAmount = 6;
            [Header("Кусты")]
            [SerializeField] private BushesFactory.BushesSettings _bushesSettings;
            [SerializeField] private Vector2 _bushesStartPosition = new Vector2(-4,0);
            [SerializeField] [Range(0,10f)] private float _bushesDistance = 2f;
            [SerializeField] [Range(0,10)] private uint _bushesPoolAmount = 6;
        }

        [Serializable]
        public class PipePareGenerationSettings
        {
            public PipePareSpawnFactory.PipePareSettings PipeSettings => _pipeSettings;
            public Vector2 PipeStartPosition => _pipeStartPosition;
            public float PipeDistance => _pipeDistance;
            public int PipesPoolAmount => _pipesPoolAmount;
            public int PipesStartSkipAmount => _pipesStartSkipAmount;

            [SerializeField] private PipePareSpawnFactory.PipePareSettings _pipeSettings;
            [SerializeField] private Vector2 _pipeStartPosition = new Vector2(3f, 0);
            [SerializeField] [Range(1,5f)] private float _pipeDistance = 4f;
            [SerializeField] [Range(5,20)] private int _pipesPoolAmount = 6;
            [SerializeField] [Range(1,5)] private int _pipesStartSkipAmount = 3;
        }

        [Serializable]
        public class LandGenerationSettings
        {
            public LandSpawnFactory.LandSettings LandSettings => _landSettings;
            public Vector2 LandStartPosition => _landStartPosition;
            public float LandsDistance => _landsDistance;
            public int LandPoolAmount => _landPoolAmount;

            [SerializeField] private LandSpawnFactory.LandSettings _landSettings;
            [SerializeField] private Vector2 _landStartPosition = new Vector2(-4f, 0);
            [SerializeField] [Range(0,3f)] private float _landsDistance = 1f;
            [SerializeField] [Range(0,30)] private int _landPoolAmount = 21;
        }
    }
}
