using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeFinal
{
    class ProducerException: Exception
    {
        public ProducerException(string message) : base(message) { }
    }
}
