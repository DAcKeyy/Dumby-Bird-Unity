using UnityEngine;

namespace DI.Signals
{
    public class GameStarted
    {
        public GameStarted(Vector2 startPosition)
        {
            StartPosition = startPosition;
        }

        public Vector2 StartPosition
        {
            get;
            private set;
        }
    }
}