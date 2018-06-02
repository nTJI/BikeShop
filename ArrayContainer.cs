using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BikeFinal
{
    [Serializable]
    class ArrayContainer<T>  : IContainer<T>, IEnumerable 
    {
        private T[] bikes = new T[0];
        public double Sum { get; set;}
        public event PriceChangingHandler Added;
        public event PriceChangingHandler Deleted;

        public class ArrayEnum : IEnumerator
        {
            public T[] _bike;

            // Enumerators are positioned before the first element 
            // until the first MoveNext() call. 
            int position = -1;

            public ArrayEnum(T[] list)
            {
                _bike = list;
            }

            public bool MoveNext()
            {
                position++;
                return (position < _bike.Length);
            }

            public void Reset()
            {
                position = -1;
            }

            object IEnumerator.Current
            {
                get
                {
                    return Current;
                }
            }

            public T Current
            {
                get
                {
                    try
                    {
                        return _bike[position];
                    }
                    catch (IndexOutOfRangeException)
                    {
                        throw new InvalidOperationException();
                    }
                }
            }
        }

        public ArrayContainer() { }

        public int Length()
        {
            return bikes.Length;
        }
        private T[] DellNulls(T[] arr)
        {
            T[] result = new T[0];
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] != null)
                {
                    // Add to arr
                    T[] temp = new T[result.Length + 1];
                    Array.Copy(result, temp, result.Length);
                    temp[temp.Length - 1] = arr[i];
                    result = temp;
                }
            }
            return result;
        }
        public void Add(T b)
        {
            if(b == null)
            {
                throw new MyOutOfRangeException("Object is null");
            }
            T[] temp = new T[bikes.Length + 1];
            for (int i = 0; i < temp.Length - 1; i++)
            {
                temp[i] = bikes[i];
            }
            temp[temp.Length - 1] = b;
            if (Added != null)
                Added(Sum += (b as Product).Price);
            bikes = temp;
        }
        public void Remove(T b)
        {
            if (!bikes.Contains(b))
                throw new ObjectIsntContains("Object isnot contains");
            T[] temp = new T[bikes.Length - 1];
            
            for (int i = 0, j = 0 ; i < bikes.Length; i++, j++)
            { 
                if (bikes[i].Equals(b))
                {
                    j--;
                    continue;
                }

                temp[j] = bikes[i];
                bikes = temp;
            }
            if (Deleted != null)
                Deleted(Sum -= (b as Product).Price);
        }
        public void Remove(int index)
        {
            if (index < 0 || index >= Length())
                throw new MyOutOfRangeException("Index should be in range 0<x<"+Length());
            if (Deleted != null) Deleted(Sum -= (bikes[index] as Product).Price);
            T[] temp = new T[bikes.Length - 1];

            for (int i = 0, j = 0; i < bikes.Length; i++, j++)
            {
                if (i == index)
                {
                    j--;
                    continue;
                }
                temp[j] = bikes[i];
            }
            bikes = temp;
        }

        // -----------------------                             не не не
        public void Sort()
        {
            T temp;
            for (int i = 0; i<bikes.Length; i++)
            {
                for(int j = 0; j<bikes.Length -1; j++){
                    if ((bikes[j] as Product).Price > (bikes[j + 1] as Product).Price)
                    {
                        //swap
                        temp = bikes[j + 1];
                        bikes[j + 1] = bikes[j];
                        bikes[j] = temp;
                    }
                }
            }
        }

        
        public T this[int index]
        {
            get
            {
                return bikes[index];
            }
            set
            {
                if (Deleted != null) Deleted(Sum -= (bikes[index] as Product).Price);
                bikes[index] = value;
                if (Added != null) Added(Sum += (value as Product).Price);
            }
        }
        public T this[string name]
        {
            get
            {
                if (bikes.Length == 0)
                    return default(T);

                //if (bikes is IName)
                    foreach (IName<string> i in bikes)
                        if (i.Name.Equals(name))
                            return (T)i;

                return default(T);
            }
            set
            {
                
                for (int i = 0; i<bikes.Length; i++)
                    if ((bikes[i] as Product).Name.Equals(name))
                    {
                        if (Deleted != null) Deleted(Sum -= (bikes[i] as Product).Price);
                        bikes[i] = (T)value;
                        if (Added != null) Added(Sum += (bikes[i] as Product).Price);
                        return;
                    }
                        
            }
        }
        public T GetBike(int index)
        {
            if (index >= bikes.Length) return default(T);
            return bikes[index];
        }

        public override string ToString()
        {
            string result = "Array of bikes";
            foreach (T b in bikes)
            {
                result += "\n\t" + b.ToString();
            }

            return result;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }
        public ArrayEnum GetEnumerator()
        {
            return new ArrayEnum(bikes);
        }

        public IEnumerable Reverse()
        {
            for(int i = bikes.Length-1; i>=0; i--)
            {
                yield return bikes[i];
            }
        }

        public IEnumerable Contains(string s)
        {
            for(int i = 0; i<bikes.Length; i++)
            {
                if ((bikes[i] as Product).Name.Contains(s))
                    yield return bikes[i];
            }
        }
        //          ---------------------------------------------------------remake
        public IEnumerable SortedByName()
        {
            T[] res = new T[Length()];

            for (int i = 0; i < Length(); i++)
                res[i] = bikes[i];

            T temp;
            for (int i = 0; i < res.Length; i++)
            {
                for (int j = 0; j < res.Length - 1; j++)
                {
                    if ((res[j] as Product).Name.Length > (res[j + 1] as Product).Name.Length)
                    {
                        //swap
                        temp = bikes[j + 1];
                        res[j + 1] = res[j];
                        res[j] = temp;
                    }
                }
            }

            for(int i = 0; i< Length(); i++)
            {
                yield return res[i];
            }
        }
        // ----------------------------------------------------------------night
        public void Sort(Compare<T> del)
        {
            T temp;
            for (int i = 0; i < bikes.Length; i++)
            {
                for (int j = 0; j < bikes.Length - 1; j++)
                {
                    if (del(bikes[j], bikes[j+1])< 0)
                    {
                        //swap
                        temp = bikes[j + 1];
                        bikes[j + 1] = bikes[j];
                        bikes[j] = temp;
                    }
                }
            }
            //Array.Sort(bikes, (T x, T y) => { return del(x, y); });
        }

        public T Find(Find<T> del)
        {
            foreach (T obj in this)
            {
                if (del(obj))
                {
                    return obj;
                }
            }
            return default(T);
        }

        public List<T> FindAll(Find<T> del)
        {
            List <T> result = new List<T>();
            
            foreach (T obj in this)
            {
                if (del(obj))
                {
                    result.Add(obj);
                }
            }
            return result;
        }

        public T GetCheapest()
        {
            return bikes.Min();
        }

        public T GetMostExpensive()
        {
            return bikes.Max();
        }
        public double GetAvg(Type type)
        {
            
            var c = from b in bikes
                    where b.GetType() == type
                    select b;
            return c.Average(b => (b as Product).Price);     
        }

        public IEnumerable GetLessPriceThan(double value)
        {
            return from b in bikes
                   where (b as Product).Price < value
                   select b;
        }
    }
}
