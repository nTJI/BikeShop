using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeFinal
{
    class NameException: Exception
    {
        public NameException(string message) : base(message) { }
    }
}
