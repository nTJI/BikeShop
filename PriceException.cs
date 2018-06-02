using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeFinal
{
    class PriceException : Exception
    {
        public PriceException(string message) : base(message) { }
    }
}
