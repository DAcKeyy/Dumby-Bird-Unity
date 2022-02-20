using UnityEngine;

namespace Scenes.Actors
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class Pipe : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log($"Pipe {gameObject.name} touched {other.name}");
        }
    }
}