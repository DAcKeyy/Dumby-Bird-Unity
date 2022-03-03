using System;
using DI.Signals;
using UI.Base;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Canvases
{
    public class FlappyBirdPauseCanvas : MonoBehaviour, ICanvas
    {
        [SerializeField] private Button _resumeButton;
        
        [Inject]
        public void Init(SignalBus signalBus)
        {
            _resumeButton.onClick.AddListener(() =>
            {
                signalBus.Fire(new GamePauseSignal(false));
            });
            
            //TODO Добаить посередник между канвасами
            signalBus.Subscribe<GamePauseSignal>(x =>
            {
                if (x.Paused)
                {
                    Time.timeScale = 0;
                    gameObject.SetActive(true);
                }
                else
                {
                    Time.timeScale = 1;
                    gameObject.SetActive(false);
                }
            });
        }
        
        public void Update()
        {
            
        }
    }
}