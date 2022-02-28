using DI.Signals;
using UnityEngine;
using Zenject;

namespace Scenes.Actors
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class Pipe : MonoBehaviour
    {
        [Inject] private SignalBus _signalBus;

        private void OnCollisionEnter2D(Collision2D col)
        {
            //TODO Подумать как это скейлить
            if (col.collider.GetComponent<Bird>() != null)
            {
                _signalBus.TryFire(new PipeTouchedSignal(col));
            }
        }
    }
}