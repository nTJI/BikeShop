using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeFinal
{
    class MyOutOfRangeException : Exception
    {
        public MyOutOfRangeException(string message) : base(message){}
    }
}
