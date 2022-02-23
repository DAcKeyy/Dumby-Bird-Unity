using UnityEngine;

namespace Scenes.Actors
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class Pipe : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D col)
        {
            Debug.Log($"{col.gameObject.name} touched pipe {gameObject.name}");
        }
    }
}