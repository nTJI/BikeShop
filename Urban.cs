using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeFinal
{
    [Serializable]
    class Urban : Product
    {
        private int seats;
        public int Seats
        {
            get
            {
                return seats;
            }
            set
            {
                if (value < 1 || value > 4)
                    throw new SeatsException("Неверное количество сидений");
                seats = value;
            }
        }

        public Urban(string name, string producer, double price) : base(name, producer, price) { }
        public Urban(string name, string producer, double price, int seats) : base(name, producer, price)
        {
            this.seats = seats;
        }

        public override string ToString()
        {
            return "Urban " + base.ToString() + " seats=" + seats;
        }
    }
}
