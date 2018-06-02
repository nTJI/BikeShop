using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeFinal
{
    [Serializable]
    class LinkedListContainer<T> : IContainer<T>, IEnumerable where T: Product
    {
        public class Node<Bike>
        {
            public Node(T data)
            {
                Data = data;
            }
            public T Data { get; set; }
            public Node<T> Next { get; set; }
            public override string ToString()
            {
                return Data.ToString();
            }
        }
        public class ListEnum : IEnumerator
        {
            public LinkedListContainer<T> _people;

            // Enumerators are positioned before the first element 
            // until the first MoveNext() call. 
            int position = -1;

            public ListEnum(LinkedListContainer<T> list)
            {
                _people = list;
            }

            public bool MoveNext()
            {
                position++;
                return (position < _people.count);
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
                        return _people[position];
                    }
                    catch (IndexOutOfRangeException)
                    {
                        throw new InvalidOperationException();
                    }
                }
            }
        }

        
        Node<T> head; // головной/первый элемент
        Node<T> tail; // последний/хвостовой элемент
        int count;  // количество элементов в списке

        public double Sum { get; set; }
        public event PriceChangingHandler Added;
        public event PriceChangingHandler Deleted;
        
        public void Add(T data)
        {
            if (data == null)
            {
                throw new MyOutOfRangeException("Object is null");
            }
            Node<T> node = new Node<T>(data);
            if (head == null)
            {
                head = node;
                tail = node;
                tail.Next = head;
            }
            else
            {
                node.Next = head;
                tail.Next = node;
                tail = node;
            }
            if (Added != null) Added(Sum += data.Price);
            count++;
        }
        public void Remove(T data)
        {
            Node<T> current = head;
            Node<T> previous = null;

            if (IsEmpty) return;

            do
            {
                if (current.Data.Equals(data))
                {
                    if (previous != null)
                    {
                        previous.Next = current.Next;
                        if (current == tail)
                            tail = previous;
                    }
                    else 
                    {
                        
                        if (count == 1)
                        {
                            head = tail = null;
                        }
                        else
                        {
                            head = current.Next;
                            tail.Next = current.Next;
                        }
                    }
                    if (Deleted != null) Deleted(Sum -= data.Price);
                    count--;
                }

                previous = current;
                current = current.Next;
            } while (current != head);
        }
        public void Remove(int index)
        {
            if (IsEmpty || index < 0 || index >= count)
                throw new MyOutOfRangeException("Index should be in range 1<x<" + count);

            Node<T> current = head;
            Node<T> previous = null;

            for (int i = 0; i < count; i++)
            {
                current = current.Next;
                previous = current;

                if (i == index)
                {
                    if (previous != null)
                    {
                        previous.Next = current.Next;
                        
                        if (current == tail)
                            tail = previous;
                    }
                    else 
                    {
                        if (count == 1)
                        {
                            head = tail = null;
                        }
                        else
                        {
                            head = current.Next;
                            tail.Next = current.Next;
                        }
                    }
                    if (Deleted != null) Deleted(Sum -= current.Data.Price);
                    count--;
                    return;
                }
            }
        }
        public void Sort()
        {
            ArrayContainer<T> b = new ArrayContainer<T>();
            Node<T> current = head;
            for (int i = 0; i < count; i++)
            {
                b.Add(current.Data);
                current = current.Next;

            }
            b.Sort();
            Clear();
            for (int i = 0; i < b.Length(); i++)
            {
                this.Add(b.GetBike(i));
            }
        }

        public int Length() { return count; }
        public bool IsEmpty { get { return count == 0; } }

        public T this[int index]
        {
            get
            {
                Node<T> current = head;
                for(int i = 0; i<count; i++)
                {
                    if (i == index)
                        return current.Data;
                    current = current.Next;
                }
                return default(T);
            }
            set
            {
                Node<T> current = head;
                for (int i = 0; i < count; i++)
                {
                    if (i == index)
                    {
                        if (Deleted != null) Deleted(Sum -= current.Data.Price);
                        current.Data = value;
                        if (Added != null) Added(Sum += current.Data.Price);
                        return;
                    }
                    current = current.Next;
                    
                }
            }
        }
        public T this[string name]
        {
            get
            {
                Node<T> current = head;
                for (int i = 0; i < count; i++)
                {
                    if ((current.Data as Product).Name.Equals(name))
                        return current.Data;
                    current = current.Next;
                }
                return default(T);
            }
            set
            {
                Node<T> current = head;
                for (int i = 0; i < count; i++)
                {
                    if ((current.Data as Product).Name.Equals(name))
                    {

                        if (Deleted != null) Deleted(Sum -= current.Data.Price);
                        current.Data = value;
                        if (Added != null) Added(Sum += current.Data.Price);
                        return;
                    }
                    current = current.Next;

                }
            }
        }
        
        public void Clear()
        {
            head = null;
            tail = null;
            Sum = 0;
            count = 0;
        }

        public bool Contains(T data)
        {
            Node<T> current = head;
            if (current == null) return false;
            do
            {
                if (current.Data.Equals(data))
                    return true;
                current = current.Next;
            }
            while (current != head);
            return false;
        }

        public override string ToString()
        {
            string result = "LinkedListContainer";
            // если список пуст
            if (head == null)
            {
                return result;
            }
            Node<T> carrent = head;
            for (int i = 0; i < count; i++)
            {
                result += "\n\t" + carrent.Data.ToString();
                carrent = carrent.Next;

            }

            return result;
        }

        public T[] ToArray()
        {
            T[] arr = new T[count];

            Node<T> current = head;
            for(int i = 0; i<count; i++)
            {
                arr[i] = current.Data;
                current = current.Next;
            }
            return arr;
        }

        public IEnumerator GetEnumerator()
        {
            return new ListEnum(this);
        }


        public IEnumerable Reverse()
        {
            ArrayContainer<T> result = new ArrayContainer<T>();
            Node<T> current = head;
            for (int i = Length(); i > 0; --i)
            {
                result.Add(current.Data);
                current = current.Next;
            }

            foreach (Product b in result.Reverse())
            {
                yield return b;
            }
        }

        public IEnumerable Contains(string s)
        {
            Node<T> current = head;
            for (int i = 0; i < Length(); i++)
            {
                if (((Product)current.Data).Name.Contains(s))
                    yield return current.Data;
                current = current.Next;
            }
        }

        public IEnumerable SortedByName()
        {

            T[] result = new T[Length()];
            Node<T> current = head;
            for (int i = 0; i < Length(); i++)
            {
                result[i] = current.Data;
                current = current.Next;
            }

            
            var bikesCopy = result.OrderBy(item => item.Name);
            foreach (Product b in bikesCopy)
            {
                yield return b;
            }
        }

        public void Sort(Compare<T> del)
        {
            List<T> bikes = new List<T>();
            Node<T> current = head;
            for (int i = 0; i < Length(); i++)
            {
                bikes.Add(current.Data);
                current = current.Next;
            }

            bikes.Sort((T x, T y) => { return del(x, y); });

            current = head;
            foreach(T b in bikes)
            {
                current.Data = b;
                current = current.Next;
            }
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
            List<T> result = new List<T>();

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
            T[] bikes = this.ToArray();
            return bikes.Min();
        }

        public T GetMostExpensive()
        {
            T[] bikes = this.ToArray();
            return bikes.Max();
        }
        public double GetAvg(Type type)
        {
            T[] bikes = this.ToArray();
            var c = from b in bikes
                    where b.GetType() == type
                    select b;
            return c.Average(b => b.Price);

        }
    }
}
