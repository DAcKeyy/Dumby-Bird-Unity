using Data.Generators;
using Data.Pools;
using Scenes.Actors.FlappyBird;
using Scenes.Generation.Base;
using Scenes.Generation.Factories;
using UnityEngine;

namespace Scenes.Generation.Contexts.FlappyBird
{
    public class BuildingsGenerator : ILevelGenerator
    {
        private readonly FlappyLevelGenerationSettings.BackgroundGenerationSettings _settings;
        private readonly MonoPool<Building> _buildingsPool;
        private readonly BuildingsFactory _buildingsFactory;

        private uint _buildingsCount;

        public BuildingsGenerator(
            FlappyLevelGenerationSettings.BackgroundGenerationSettings settings, 
            BuildingsFactory buildingsFactory)
        {
            _settings = settings;
            _buildingsFactory = buildingsFactory;
            _buildingsPool = new MonoPool<Building>();
        }
        
        public void Create()
        {
            for (var i = 0; i < _settings.BuildingsPoolAmount; i++)
            {
                _buildingsPool.AddObject(_buildingsFactory.Create(new Vector2(
                    _settings.BuildingsStartPosition.x + _settings.BuildingsDistance * i,
                    _settings.BuildingsStartPosition.y)));
            }

            _buildingsCount = _settings.BuildingsPoolAmount;
        }
        
        public void Update()
        {
            Object.Destroy(_buildingsPool.PrefabPool[0].gameObject);
            _buildingsPool.PrefabPool.RemoveAt(0);
            
            _buildingsPool.AddObject(_buildingsFactory.Create(new Vector2(
                _settings.BuildingsStartPosition.x + _settings.BuildingsDistance * _buildingsCount++,
                _settings.BuildingsStartPosition.y)));
        }
    }
}