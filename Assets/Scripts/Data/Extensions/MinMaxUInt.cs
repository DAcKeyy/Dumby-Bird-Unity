using System;

namespace Data.Extensions
{
    [Serializable]
    public class MinMaxUInt
    {
        public uint min;
        public uint max;

        public MinMaxUInt(uint min, uint max)
        {
            this.min = min;
            this.max = max;
        }
    }
}