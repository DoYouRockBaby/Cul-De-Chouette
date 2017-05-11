using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class Cube
    {
        public virtual int Value { get; private set; }
        private static Random Random = new Random();
        public int Throw()
        {
            Value = Random.Next(1, 7);
            return Value;
        }
    }
}
