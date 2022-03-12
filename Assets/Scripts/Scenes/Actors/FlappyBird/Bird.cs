using DI.Signals;
using Scenes.Actors.Movement;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using Zenject;

namespace Scenes.Actors.FlappyBird
{
    [RequireComponent(typeof(FlappyMovement))]
    public class Bird : MonoBehaviour
    {
        public bool IsAlive => !_isDied;
        [SerializeField] private UnityEvent _dieEvent;
        private SignalBus _signalBus;
        private bool _isDied;
        
        [Inject]
        public void Init(SignalBus signalBus)
        {
            _signalBus = signalBus;
            
            //TODO: Должна ли птица знать о сигналах?
            GetComponent<PlayerInput>().onControlsChanged += context =>
            {
                //print(context.);
            };
            

            _signalBus.Subscribe<PipeTouchedSignal>(x => {
                //TODO Исправить двойную проверку что тут, что в трубе
                if (x.CollisionObj2D.collider.GetComponent<Bird>() != null)
                {
                    Die();
                }
            });
            
            _signalBus.Subscribe<LandTouchedSignal>(x => {
                //TODO Исправить двойную проверку что тут, что на земле
                if (x.CollisionObj2D.collider.GetComponent<Bird>() != null)
                {
                    Die();
                }
            });
        }

        public void Die()
        {
            if(_isDied) return;
            _dieEvent.Invoke();
            _isDied = true;
            //TODO Анимация смээрти
            GetComponent<FlappyMovement>().enabled = false;
            //GetComponent<Collider2D>().isTrigger = true;
            _signalBus.TryFire<BirdDiedSignal>();
            
        }
    }
}