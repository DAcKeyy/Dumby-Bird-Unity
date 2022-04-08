using UnityEngine;

namespace Scenes.Actors.FlappyBird
{
	[RequireComponent(typeof(BoxCollider2D))]
	public class InvisibleBorder2D : MonoBehaviour
	{
		private BoxCollider2D _borderCollider;
		
		public void Awake()
		{
			_borderCollider = GetComponent<BoxCollider2D>();
		}

		public void ChangePosition(Vector2 newPos)
		{
			transform.position = newPos;
		}

		public void ChangeColliderSize(Vector2 wightAndHeight)
		{
			_borderCollider.size = wightAndHeight;
		}
	}
}