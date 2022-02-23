using UnityEngine;

namespace Scenes.Actors
{
    public class PipePare : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log("+1!");
        }
    }
}