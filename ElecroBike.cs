using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeFinal
{
    [Serializable]
    class ElectroBike : Product
    {
        private double capacity;
        public double Capacity
        {
            get
            {
                return capacity;
            }
            set
            {
                if (value < 30)
                    throw new CapacityException("Неверное значение объема двигателя");
                capacity = value;
            }
        }
        public ElectroBike(string name, string producer, double price) : base(name, producer, price) { }
        public ElectroBike(string name, string producer, double price, double capacity) : base(name, producer, price)
        {
            this.capacity = capacity;
        }
        public override string ToString()
        {
            return "Electrobike " + base.ToString() + " capacity=" + capacity;
        }
    }
}
