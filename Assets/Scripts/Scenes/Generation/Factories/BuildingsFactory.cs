using System.Collections.Generic;
using Data.Extensions;
using Scenes.Actors.FlappyBird;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Scenes.Generation.Factories
{
    public class BuildingsFactory : IFactory<Vector2 ,Building>
    {
        [Inject] private DiContainer _diContainer;
        private readonly BuildingsSettings _settings;
        private readonly GameObject _buildingParent;
        

        public BuildingsFactory(BuildingsSettings settings)
        {
            _settings = settings;
            _buildingParent = new GameObject("Buildings") {
                transform = {
                    position = Vector3.zero,
                }
            };
        }
        
        public Building Create(Vector2 position)
        {
            var buildObj = _diContainer.InstantiatePrefab(
                _settings._buildingPrefab,
                position, 
                Quaternion.identity, 
                _buildingParent.transform)
                .GetComponent<Building>();
            
            buildObj.Init(CreateBuildingsCluster(_settings));
            
            return buildObj;
        }

        private static (Sprite sprite, Vector2 position)[] CreateBuildingsCluster(BuildingsSettings buildingsSettings)
        {
            var buildingsAmount = Random.Range((int) buildingsSettings._buildingsInCluster.min, (int) buildingsSettings._buildingsInCluster.max);
            
            (Sprite sprite, Vector2 position)[] cluster = new (Sprite sprite, Vector2 position)[buildingsAmount];
            
            for (var i = 0; i < buildingsAmount; i++)
            {
                cluster[i] = (buildingsSettings._buildingsSprites[
                        Random.Range(0, buildingsSettings._buildingsSprites.Count - 1)],
                    new Vector2(
                        i * buildingsSettings._distanceInCluster, 
                        Random.Range(buildingsSettings._buildHeight.min, buildingsSettings._buildHeight.max)));           
            }

            return cluster;
        }

        [System.Serializable]
        public struct BuildingsSettings
        {
            public float _distanceInCluster;
            public MinMaxUInt _buildingsInCluster;
            public MinMaxFloat _buildHeight;
            public List<Sprite> _buildingsSprites;
            public Building _buildingPrefab;
        }
    }
}