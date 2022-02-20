using UnityEngine;

namespace Scenes.Actors
{
    public class PipePare : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("+1!");
        }
    }
}