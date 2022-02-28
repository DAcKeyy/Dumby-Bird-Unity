using DI.Signals;
using UnityEngine;
using Zenject;

namespace Scenes.Actors
{
    public class PipePare : MonoBehaviour
    {
        [Inject] private SignalBus _signalBus;
        
        private void OnTriggerEnter2D(Collider2D col)
        {
            //TODO Подумать как это скейлить
            if (col.GetComponent<Bird>() != null)
            {
                //TODO Magic number
                _signalBus.TryFire(new GamePointObtainedSignal(1));
            }
        }
    }
}