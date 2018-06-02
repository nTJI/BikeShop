using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeFinal
{
    public delegate int Compare<T>(T a, T b);
    public delegate bool Find<T>(T a);
    public delegate void PriceChangingHandler(double price);

    interface IContainer<T>
    {
        void Add(T b);
        void Remove(int index);
        void Remove(T b);
        void Sort();
    }
}
