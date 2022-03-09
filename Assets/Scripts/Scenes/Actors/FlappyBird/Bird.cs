﻿using DI.Signals;
using Scenes.Actors.Movement;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Scenes.Actors.FlappyBird
{
    [RequireComponent(typeof(FlappyMovement))]
    public class Bird : MonoBehaviour
    {
        [SerializeField] private UnityEvent _dieEvent;
        private SignalBus _signalBus;
        
        [Inject]
        public void Init(SignalBus signalBus)
        {
            _signalBus = signalBus;
            
            //TODO: Должна ли птица знать о сигналах?
            _signalBus.Subscribe<PipeTouchedSignal>(x =>
            {
                //TODO Исправить двойную проверку что тут, что в трубе
                if (x.CollisionObj2D.collider.GetComponent<Bird>() != null)
                {
                    Die();
                }
            });
            
            _signalBus.Subscribe<LandTouchedSignal>(x =>
            {
                //TODO Исправить двойную проверку что тут, что на земле
                if (x.CollisionObj2D.collider.GetComponent<Bird>() != null)
                {
                    Die();
                }
            });
        }

        public void Die()
        {
            _dieEvent.Invoke();
            //TODO Анимация смээрти
            GetComponent<FlappyMovement>().enabled = false;
            GetComponent<Collider2D>().isTrigger = true;
            _signalBus.TryFire<BirdDiedSignal>();
        }
    }
}