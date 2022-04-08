using System.Collections;
using DI.Signals;
using Scenes.Generation.Base;
using UnityEngine;
using Zenject;

namespace Scenes.Generation
{
    public class LevelGenerator : MonoBehaviour
    {
        [Inject] private ILevelGenerator _levelGeneration;
        [Inject] private SignalBus _signalBus;
        private IEnumerator _updateCoroutine;
        
        private void Start()
        {
            _levelGeneration.Create();
                
            //TODO Убрать костыль
            _updateCoroutine = UpdateLevel();
            StartCoroutine(_updateCoroutine);
            _signalBus.Subscribe<BirdDiedSignal>(x => {
                StopCoroutine(_updateCoroutine);
            });
            _signalBus.Subscribe<GameStarted>(x => {
                StopCoroutine(_updateCoroutine);
            });
        }

        //TODO Убрать костыль
        private IEnumerator UpdateLevel()
        {
            while (true)
            {
                yield return new WaitForSeconds(1f);;
                _levelGeneration.Update();
            }
        }
    }
}