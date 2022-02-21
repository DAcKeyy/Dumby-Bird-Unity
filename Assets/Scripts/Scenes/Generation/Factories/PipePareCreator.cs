using System;
using Scenes.Actors;
using Unity.Mathematics;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Scenes.Generation.Factories
{
    public class PipePareCreator
    {
        private readonly Pipe _pipe;
        private readonly float2 _themselvesDistance;

        public PipePareCreator(PipePareSettings settings) {
            _pipe = settings._prefab;
            _themselvesDistance = settings._themselvesDistance;
        }

        public PipePare CreatePipePare(Vector2 position)
        {
            var pipePare = new GameObject("Pipe Pare") {
                transform = {
                    parent = null, 
                    position = position
                }
            };
            var upPipe = Object.Instantiate(_pipe, pipePare.transform);
            var bottomPipe = Object.Instantiate(_pipe, pipePare.transform);
            
            WidePipes(bottomPipe, upPipe);
            
            var pare = pipePare.AddComponent<PipePare>();
            
            AddPareCollider(pare);
            
            return pare;
        }

        private void WidePipes(Pipe bottomPipe, Pipe upPipe)
        {
            bottomPipe.transform.position = new Vector2(
                bottomPipe.transform.position.x,
                -UnityEngine.Random.Range(_themselvesDistance.x, _themselvesDistance.y) / 2); // -y / 2
            
            
            upPipe.transform.position = new Vector2(
                upPipe.transform.position.x,
                UnityEngine.Random.Range(_themselvesDistance.x, _themselvesDistance.y) / 2);// +y / 2
            
            //TODO Убрать магическое число
            upPipe.transform.eulerAngles = new Vector3(
                upPipe.transform.eulerAngles.x,
                upPipe.transform.eulerAngles.y,
                -180);
        }

        private void AddPareCollider(PipePare pipePare)
        {
            var collieder = pipePare.gameObject.AddComponent<BoxCollider2D>();
            collieder.size = new Vector2(0, _themselvesDistance.y);
            collieder.isTrigger = true;
        }
        
        [Serializable]
        public struct PipePareSettings {
            public Pipe _prefab;
            public float2 _themselvesDistance;
        }
    }
}