using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeFinal
{
    [Serializable]
    class Mountain : Product
    {
        private int gears;
        public int Gears {
            get
            {
                return gears;
            }
            set
            {
                if (value < 1 || value > 30)
                    throw new GearsException("Недопустимое значение количества передач");
                gears = value;
            }

        }
        public Mountain(string name, string producer, double price) : base(name, producer, price) { }
        public Mountain(string name, string producer, double price, int gears) : base(name, producer, price)
        {
            this.Gears = gears;
        }

        public override string ToString()
        {

            return "Mountain " + base.ToString() + " gears=" + Gears;
        }
    }
}
