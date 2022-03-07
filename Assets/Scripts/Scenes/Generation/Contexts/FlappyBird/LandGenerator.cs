using Data.Generators;
using Data.Pools;
using Scenes.Actors.FlappyBird;
using Scenes.Generation.Base;
using Scenes.Generation.Factories;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

namespace Scenes.Generation.Contexts.FlappyBird
{
    public class LandGenerator : ILevelGenerator
    {
        private readonly LandSpawnFactory _landSpawnFactory;
        private readonly FlappyLevelGenerationSettings.LandGenerationSettings _landSettings;
        private readonly MonoPool<Land> _landPool;
        
        private int _landsCount;///сколько тайлов земли было заспаунено
        
        public LandGenerator(            
            FlappyLevelGenerationSettings.LandGenerationSettings landGenerationSettings,
            LandSpawnFactory landSpawnFactory)
        {
            _landPool = new MonoPool<Land>();
            _landSpawnFactory = landSpawnFactory;
            _landSettings = landGenerationSettings;
        }
        
        public void Create()
        {
            for (var i = 0; i < _landSettings.LandPoolAmount; i++)
            {
                _landPool.AddObject(_landSpawnFactory.Create(new Vector2(
                    _landSettings.LandStartPosition.x + _landSettings.LandsDistance * i,
                    _landSettings.LandStartPosition.y)));
            }
            _landsCount = _landSettings.LandPoolAmount;
        }

        public void Update()
        {
            //ломаем ту что позади
            Object.Destroy(_landPool.PrefabPool[0].gameObject);
            _landPool.PrefabPool.RemoveAt(0);
            
            //делаем новую впереди
            _landPool.AddObject(
                _landSpawnFactory.Create(new Vector2(
                    _landSettings.LandStartPosition.x + _landSettings.LandsDistance * _landsCount++,
                    _landSettings.LandStartPosition.y)));
        }
    }
}