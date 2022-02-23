using Scenes.Generation.Base;
using UnityEngine;
using Zenject;

namespace Scenes.Generation
{
    public class LevelGenerator : MonoBehaviour
    {
        [Inject] private ILevelGenerator _levelGeneration;
        
        private void OnEnable()
        {
            _levelGeneration.Create();
        }
    }
}