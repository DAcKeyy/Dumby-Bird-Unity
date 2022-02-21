using System.Collections.Generic;
using Scenes.Generation.Base;
using Scenes.Generation.Contexts;
using UnityEngine;

namespace Scenes.Generation
{
    public class LevelGenerator : MonoBehaviour
    {
        [SerializeField] private MyEnum generatorsContexts;
        private enum MyEnum {
            FlappyBird = 1
        }
        


        private Dictionary<int, ILevelGenerator> _generatorsContexts = new Dictionary<int, ILevelGenerator>() {
            //{1, new FlappyLevelGenerationContext()}
        };
        
        private void OnEnable()
        {

        }
    }
}