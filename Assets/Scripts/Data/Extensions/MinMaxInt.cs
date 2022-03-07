using System;

namespace Data.Extensions
{
    [Serializable]
    public class MinMaxInt
    {
        public int min;
        public int max;

        public MinMaxInt(int min, int max)
        {
            this.min = min;
            this.max = max;
        }
    }
}