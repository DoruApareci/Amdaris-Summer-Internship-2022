using System;
using System.Collections;


namespace ComparableCar
{
    class Car : IComparable
    {
        public int ID { get; set; }
        public int Speed { get; set; }
        public string Marka { get; set; }
        public override string ToString()
        {
            return String.Format("ID: {0,3} Marka: {1,-7} Speed: {2,3}", ID, Marka, Speed);
        }
        public static IComparer SortByMarka
        { get { return (IComparer)new MarkaComparer(); } }
        public int CompareTo(object obj)
        {
            Car temp = (Car)obj;
            if (this.ID > temp.ID)
                return 1;
            if (this.ID < temp.ID)
                return -1;
            else
                return 0;
        }
    }
}
