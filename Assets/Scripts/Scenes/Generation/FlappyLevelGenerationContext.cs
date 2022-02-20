using Pools;
using Scenes.Actors;
using UnityEngine;

namespace Scenes.Generation
{
    public class FlappyLevelGenerationContext : MonoBehaviour, ILevelGenerator
    {
        [SerializeField] private PipePareCreator.PipePareSettings _pipePareSettings;
        private PipePareCreator _pipePareCreator;
        private MonoPool<PipePare> _pipeParePool;

        public void OnEnable()
        {
            Init();
        }

        public void Init()
        {
            _pipePareCreator = new PipePareCreator(_pipePareSettings);
        }

        public void Create()
        {
            //_pipeParePool = new MonoPool<PipePare>(_pipePareCreator.CreatePipePare())
        }
    }
}