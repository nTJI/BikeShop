using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeFinal
{
    class SeatsException:Exception
    {
        public SeatsException(string message) : base(message) { }
    }
}
