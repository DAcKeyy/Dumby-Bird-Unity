using System;
using Unity.Mathematics;

namespace Data.Extensions
{
    [Serializable]
    public class MinMaxFloat
    {
        public float min;
        public float max;

        public MinMaxFloat(float min, float max)
        {
            this.min = min;
            this.max = max;
        }
    }
}