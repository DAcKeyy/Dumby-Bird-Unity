namespace DI.Signals
{
    public class GamePauseSignal
    {
        public GamePauseSignal(bool paused)
        {
            Paused = paused;
        }

        public bool Paused { get; private set; }
    }
}