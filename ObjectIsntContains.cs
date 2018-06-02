using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeFinal
{
    class ObjectIsntContains:Exception
    {
        public ObjectIsntContains(string message) : base(message) { }
    }
}
