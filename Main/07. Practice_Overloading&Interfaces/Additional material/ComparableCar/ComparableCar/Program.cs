using System;

namespace ComparableCar
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***** Sorting Array of cars *****\n");
            // Make an array of Car types.
            Car[] myAutos = new Car[5]
            {
                new Car {ID=1, Marka="Dacia", Speed=150 },
                new Car {ID=234, Marka="Ford", Speed=170 },
                new Car {ID=34, Marka="BMW", Speed=220 },
                new Car {ID=45, Marka="Audi", Speed=200 },
                new Car {ID=5, Marka="Mazda", Speed=180 }
            };

            // Dump current array.
            Console.WriteLine("Here is the unordered set of cars:");
            foreach (Car c in myAutos)
                Console.WriteLine(c);

            // Now, sort them using IComparable!
            Array.Sort(myAutos);

            // Dump sorted array.
            Console.WriteLine("\nHere is the ordered set of cars:");
            foreach (Car c in myAutos)
                Console.WriteLine(c);

            // Now sort by marka.
            Array.Sort(myAutos, Car.SortByMarka);

            // Dump sorted array.
            Console.WriteLine("\nOrdering by marka:");
            foreach (Car c in myAutos)
                Console.WriteLine(c);

            Console.ReadLine();
        }
    }
}
