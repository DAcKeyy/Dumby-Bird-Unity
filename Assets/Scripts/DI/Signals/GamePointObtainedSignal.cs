namespace DI.Signals
{
    public class GamePointObtainedSignal
    {
        public GamePointObtainedSignal(int amount)
        {
            PointsAmount = amount;
        }

        public int PointsAmount
        {
            get;
            private set;
        }
    }
}