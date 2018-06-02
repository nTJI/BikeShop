using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeFinal
{
    [Serializable]
    class Product : IName, IName<Product>
    {
        public static event PriceChangingHandler Edited;

        private string name;
        private string producer;
        private double price;

        public string Name {
            get
            {
                return name;
            }

            set
            {
                if (value.Length <= 3)
                {
                    throw new NameException("Слишком коротое название велосипеда. Название должно быть больше 3 символов");
                }

                name = value;
            }
        }
        public string Producer {
            get
            {
                return producer;
            }
            set
            {
                if(value.Length <= 3)
                {
                    throw new ProducerException("Слишком коротое название производителя. Название должно быть больше 3 символов");
                }
                producer = value;
            }
        }
        public double Price
        {
            get { return price; }
            set
            {
                if (value <= 0)
                    throw new PriceException("Цена не может быть отрицательной");
                if (Edited != null) Edited(price-value);
                price = value;
            }
        } 



        public Product() { }
        public Product(string name, string producer, double price)
        {
              
            Price = price;
            Name = name;
            Producer = producer;
        }

        public override string ToString()
        {
            return $"name={Name} producer={Producer} price={Price}";
        }

        public int CompareTo(Product other)
        {
            return Price.CompareTo(other.Price);
        }

        public int CompareTo(object other)
        {          
            return Price.CompareTo((other as Product).Price);
        }
    }
}
