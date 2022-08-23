using System;
using System.Collections;

namespace ComparableCar
{
    class MarkaComparer : IComparer
    {
        int IComparer.Compare(object x, object y)
        {
            Car t1 = (Car)x;
            Car t2 = (Car)y;
            return String.Compare(t1.Marka, t2.Marka);
        }
    }
}
