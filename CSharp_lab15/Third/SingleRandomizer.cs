using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Third
{
    public class SingleRandomizer
    {
        private static readonly Lazy<SingleRandomizer> _instance =
            new Lazy<SingleRandomizer>(() => new SingleRandomizer());
        private readonly Random _random;
        private readonly object _lock = new object();
        private SingleRandomizer()
        {
            _random = new Random();
        }
        public static SingleRandomizer Instance => _instance.Value;
        public int Next(int minValue, int maxValue)
        {
            lock (_lock)
            {
                return _random.Next(minValue, maxValue);
            }
        }
    }
}
