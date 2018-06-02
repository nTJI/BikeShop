using System;

namespace BikeFinal
{
    class CapacityException:Exception
    {
        public CapacityException(string message) : base(message) { }
    }
}
