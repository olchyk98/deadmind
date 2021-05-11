using System.Linq;

namespace Utils
{
    public static class RandomHelper
    {
        
        /// <summary>
        /// Gives you a seemingly random value that doesn't change if the input and clamping stays the same
        /// </summary>
        public static int getValue(string input, int min, int max)
        {
            var i = input.Aggregate(1, (current, c) => current * c);
            i %= max - min;
            i += min;
            return i;
        }
    }
}