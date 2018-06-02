using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeFinal
{
    [Serializable]
    class Crosscountry : Mountain
    {

        private string absorberType;
        public string AbsorberType {
            get
            {
                return absorberType;
            }
            set
            {
                if (value.Length < 3 || value.Length > 30)
                    throw new AbsorberTypeException("Неверное значение типа подвески(3<x<30)");
                absorberType = value;
            }
        }
        public Crosscountry(string name, string producer, double price, int gears, string absorberType)
            : base(name, producer, price, gears)
        {
            this.AbsorberType = absorberType;
        }
        public override string ToString()
        {
            return "Crosscountry-" + base.ToString() + " absorberType=" + AbsorberType;
        }
    }
}
