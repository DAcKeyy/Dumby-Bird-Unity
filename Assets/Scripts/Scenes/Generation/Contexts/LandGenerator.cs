using Data.Generators;
using Pools;
using Scenes.Actors.FlappyBird;
using Scenes.Generation.Base;
using Scenes.Generation.Factories;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

namespace Scenes.Generation.Contexts
{
    public class LandGenerator : ILevelGenerator
    {
        private readonly LandSpawnFactory _landSpawnFactory;
        private readonly FlappyLevelGenerationSettings.LandGenerationSettings _landSettings;
        private readonly MonoPool<Land> _landPool;
        
        private int _startLandsSkipCount;//сколько тайлов земли пропущено в начале (чтобы последий не исчезал сразу)
        private int _landsCount;///сколько тайлов земли было заспаунено
        
        public LandGenerator(            
            FlappyLevelGenerationSettings.LandGenerationSettings landGenerationSettings,
            LandSpawnFactory landSpawnFactory)
        {
            _landPool = new MonoPool<Land>();
            _landSpawnFactory = landSpawnFactory;
            _landSettings = landGenerationSettings;
            _startLandsSkipCount = landGenerationSettings.LandStartSkipAmount;
        }
        
        public void Create()
        {
            for (var i = 0; i < _landSettings.LandPoolAmount; i++)
            {
                //TODO Remove Magic number
                _landPool.AddObject(_landSpawnFactory.Create(new Vector2(
                    _landSettings.LandStartPositionX + _landSettings.LandsDistance * i,
                    -4)));
            }
            _landsCount = _landSettings.LandPoolAmount;
        }

        public void Update()
        {
            if (_startLandsSkipCount > 0)
            {
                _startLandsSkipCount--;
                return;
            }
            
            //ломаем ту что позади
            Object.Destroy(_landPool.PrefabPool[0].gameObject);
            _landPool.PrefabPool.RemoveAt(0);
            
            //TODO Remove Magic number
            //делаем новую впереди
            _landPool.AddObject(
                _landSpawnFactory.Create(new Vector2(
                    _landSettings.LandStartPositionX + _landSettings.LandsDistance * _landsCount++,
                    -4f)));
        }
    }
}