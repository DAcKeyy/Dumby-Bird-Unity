using DI.Signals;
using UnityEngine;
using Zenject;

namespace Scenes.Actors.FlappyBird
{
    public class Land : MonoBehaviour
    {
        [Inject] private SignalBus _signalBus;
        
        private void OnCollisionEnter2D(Collision2D col)
        {
            //TODO Подумать как это скейлить
            if (col.gameObject.GetComponent<Bird>() != null)
            {
                //TODO Magic number
                _signalBus.TryFire(new LandTouchedSignal(col));
            }
        }
    }
}
