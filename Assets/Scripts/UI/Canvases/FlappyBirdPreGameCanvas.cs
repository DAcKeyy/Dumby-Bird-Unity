using DI.Signals;
using UI.Base;
using UnityEngine;
using Zenject;

namespace UI.Canvases
{
    public class FlappyBirdPreGameCanvas : MonoBehaviour , ICanvas
    {
        [Inject]
        public void Init(SignalBus signalBus)
        {
            //TODO Добавить посередник между канвасами
            signalBus.Subscribe<GameStarted>(x => {
                gameObject.SetActive(false);
            });
        }

        public void Update()
        {
            //throw new System.NotImplementedException();
        }
    }
}