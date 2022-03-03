using UnityEngine;

namespace Movement
{
    [RequireComponent(typeof(CircleCollider2D), typeof(Rigidbody2D))]
    public class FlappyMovement : MonoBehaviour
    {
        [SerializeField] [Range(0, 20f)] private float _jumpForce;
        [SerializeField] [Range(-5, 5f)] private float _xPositionMovement;
        [SerializeField] [Range(0, 100f)] private float _rotationMultiplier = 20;
        private Rigidbody2D _rigidbody2D;

        private void OnEnable()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            MoveToward(new Vector2(_xPositionMovement, _rigidbody2D.velocity.y));
            RotateDown();
        }

        public void Jump()
        {
            //  TODO Синхронизировать работу из Update в FixedUpdate чтобы даблклика не было
            if(this.enabled == false) return;//UnityEventы могут вызывать методы в выключеных компонентах kekw0_0
            
            _rigidbody2D.velocity = Vector2.zero;

            _rigidbody2D.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        }

        private void MoveToward(Vector2 direction)
        {
            _rigidbody2D.velocity = direction;
        }

        private void RotateDown()
        {

            transform.eulerAngles = new Vector3(0, 0, Mathf.Clamp(_rigidbody2D.velocity.y * _rotationMultiplier, -80, 50));
        }
    }
}
