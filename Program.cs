using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace BikeFinal
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //<------------ Lab10 -------------->
                ArrayContainer<Product> bikes = new ArrayContainer<Product>();

                Console.ForegroundColor = ConsoleColor.Green;

                //bikes.Added += ((x) => { Console.WriteLine("(Added)Sum of products: " + x); });
                //bikes.Deleted += (x => Console.WriteLine("(Deleted)Sum of products: " + x));
                //Product.Edited += ((x) => { bikes.Sum -= x; Console.WriteLine("(Edited)Sum of products: " + bikes.Sum); });

                bikes.Add(new Urban("xf11", "Cannondale", 14.0d, 2));
                bikes.Add(new Crosscountry("gf41", "Cannondale", 17.7d, 6, "двухтрубный"));
                bikes.Add(new Mountain("vsd51", "Cannondale", 13.4d, 5));
                bikes.Add(new Mountain("vsd52", "Another", 4, 5));

                //bikes.Remove(1);
                //bikes[3].Price = 6;
                
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();
                Console.WriteLine(bikes.ToString());

                bikes.Sort((x,y) => x.Price.CompareTo(y.Price));
                Console.WriteLine(bikes.ToString());










                //<------------ Lab11 -------------->
                Console.WriteLine();
                //Console.WriteLine(bikes.GetCheapest().ToString());
                //Console.WriteLine(bikes.GetMostExpensive().ToString());
                //Console.WriteLine("AVG = "+bikes.GetAvg(typeof(Crosscountry)).ToString());

                //foreach(Product b in bikes.GetLessPriceThan(10))
                //{
                //    Console.WriteLine(b.ToString());
                //}

                //<------------ RGZ -------------->

                //Console.WriteLine();
                //BinaryFormatter formatter = new BinaryFormatter();
                ////получаем поток, куда будем записывать сериализованный объект
                //using (FileStream fs = new FileStream("bike.dat", FileMode.OpenOrCreate))
                //{
                //    formatter.Serialize(fs, bikes);

                //    Console.WriteLine("Объект сериализован");
                //}

                ////десериализация из файла bike.dat
                //using (FileStream fs = new FileStream("bike.dat", FileMode.OpenOrCreate))
                //{
                //    ArrayContainer<Product> bike = (ArrayContainer<Product>)formatter.Deserialize(fs);
                //    Console.WriteLine("Объект десериализован");

                //    Console.WriteLine(bike.ToString());
                //}


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            Console.ReadLine();
        }
    }
}
