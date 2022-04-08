using System;
using Data.Extensions;
using Scenes.Actors.FlappyBird;
using Unity.Mathematics;
using UnityEngine;
using Zenject;

namespace Scenes.Generation.Factories
{
    public class PipePareSpawnFactory : IFactory<Vector2 , PipePare>
    {
        private readonly Pipe _pipe;
        private readonly float2 _themselvesDistance;
        [Inject] private DiContainer _diContainer;
        private readonly GameObject _pipesParent;

        public PipePareSpawnFactory(PipePareSettings settings) {
            _pipe = settings._prefab;
            _themselvesDistance = new float2(settings._themselvesDistance.min, settings._themselvesDistance.max);
            _pipesParent = new GameObject("Pipes") {
                transform = {
                    position = Vector3.zero,
                }
            };
        }
        
        public PipePare Create(Vector2 position)
        {
            var pipePare = new GameObject("Pipe Pare") {
                transform = {
                    parent = _pipesParent.transform, 
                    position = position
                }
            };
            var upPipe = _diContainer.InstantiatePrefab(_pipe, pipePare.transform);
            var bottomPipe = _diContainer.InstantiatePrefab(_pipe, pipePare.transform);
            
            WidePipes(bottomPipe.GetComponent<Pipe>(), upPipe.GetComponent<Pipe>());
            
            var pare = pipePare.AddComponent<PipePare>();
            
            AddPareCollider(pare);
            _diContainer.Inject(pare);
            return pare;
        }
        
        private void WidePipes(Pipe bottomPipe, Pipe upPipe)
        {
            var yDistance = UnityEngine.Random.Range(_themselvesDistance.x, _themselvesDistance.y);
            
            bottomPipe.transform.position = new Vector2(
                bottomPipe.transform.position.x,
                bottomPipe.transform.position.y + -yDistance / 2); // -y / 2
            
            
            upPipe.transform.position = new Vector2(
                upPipe.transform.position.x,
                upPipe.transform.position.y + yDistance / 2);// +y / 2
            
            //TODO Убрать магическое число
            upPipe.transform.eulerAngles = new Vector3(
                upPipe.transform.eulerAngles.x,
                upPipe.transform.eulerAngles.y,
                -180);
        }

        private void AddPareCollider(PipePare pipePare)
        {
            var collieder = pipePare.gameObject.AddComponent<BoxCollider2D>();
            //TODO Убрать магическое число
            collieder.size = new Vector2(0.1f, _themselvesDistance.y);
            collieder.isTrigger = true;
        }
        
        [Serializable]
        public struct PipePareSettings {
            public Pipe _prefab;
            public MinMaxFloat _themselvesDistance;
        }
    }
}