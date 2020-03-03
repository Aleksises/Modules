using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleSampleLibrary
{
    public static class MathHelper
    {
        public static double GetCircleSquare(int radius)
        {
            return 2 * Math.PI * Math.Pow(radius, 2);
        }

        public static double GetAverage(IEnumerable<double> list)
        {
            return list.Average();
        }
    }
}
