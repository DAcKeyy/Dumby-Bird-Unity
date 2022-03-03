using DI.Signals;
using UI.Base;
using UI.Elements;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Canvases
{
    [RequireComponent(typeof(Canvas))]
    public class FlappyBirdGameCanvas : MonoBehaviour, ICanvas
    {
        private SignalBus _signalBus;
        [SerializeField] private UICounter _scoreCounter;
        [SerializeField] private Button _menuButton;

        [Inject]
        public void Init(SignalBus signalBus)
        {
            //TODO Добавить посередник между канвасами
            _signalBus = signalBus;
            _signalBus.Subscribe<GamePointObtainedSignal>(x => _scoreCounter.Increment(x.PointsAmount));
            _signalBus.Subscribe<BirdDiedSignal>(x => gameObject.SetActive(false));
            _signalBus.Subscribe<GamePauseSignal>(x =>
            {
                if (x.Paused)
                {
                    gameObject.SetActive(false);
                }
                else
                {
                    gameObject.SetActive(true);
                }
            });
            
            _menuButton.onClick.AddListener(() => _signalBus.TryFire(new GamePauseSignal(true)));
        }

        public void Update()
        {
            
        }
    }
}