using System;
using Scenes.Actors;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using Object = UnityEngine.Object;

namespace Scenes.Generation
{
    public class PipePareCreator
    {
        private readonly Pipe _pipe;
        private readonly float2 _themselvesDistance;

        public PipePareCreator(PipePareSettings settings) {
            _pipe = settings._prefab;
            _themselvesDistance = settings._themselvesDistance;
        }
        
        [Serializable]
        public struct PipePareSettings {
            public Pipe _prefab;
            public float2 _themselvesDistance;
        }

        public GameObject CreatePipePare(Vector2 position)
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
            return pipePare;
        }

        private void WidePipes(Pipe bottomPipe, Pipe upPipe)
        {
            var pipeTransform = bottomPipe.transform;
            
            pipeTransform.position = new Vector2(
                pipeTransform.position.x,
                -UnityEngine.Random.Range(_themselvesDistance.x, _themselvesDistance.y)); // -y!
            
            
            
            pipeTransform = upPipe.transform;
            
            pipeTransform.position = new Vector2(
                pipeTransform.position.x,
                UnityEngine.Random.Range(_themselvesDistance.x, _themselvesDistance.y));// +y!
            
            //TODO Убрать магическое число
            pipeTransform.eulerAngles = new Vector3(
                pipeTransform.eulerAngles.x,
                pipeTransform.eulerAngles.y,
                -180);
        }

        private void AddPareCollider(PipePare pipePare)
        {
            var collieder = pipePare.gameObject.AddComponent<BoxCollider2D>();
            collieder.size = new Vector2(0, _themselvesDistance.y);
            collieder.isTrigger = true;
        }
    }
}