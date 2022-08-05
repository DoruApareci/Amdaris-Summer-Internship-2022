using System;

namespace Compose
{
    delegate void MyDelegate(string s);
    class Program
    {
        public static void Hello(string s)
        {
            Console.WriteLine("  Hello, {0}!", s);
        }
        public static void Goodbye(string s)
        {
            Console.WriteLine("  Goodbye, {0}!", s);
        }

        static void Main(string[] args)
        {
            MyDelegate a, b, c, d;
            // Create the delegate object a that references  the method Hello:
            a = new MyDelegate(Hello);
            // Create the delegate object b that references  the method Goodbye:
            b = new MyDelegate(Goodbye);
            // The two delegates, a and b, are composed to form c, which calls both methods in order:
            c = a + b;
            //Delegate[] ar = c.GetInvocationList();
            // Remove a from the composed delegate, leaving d, which calls only the method Goodbye:
            d = c - a;
            Console.WriteLine("Invoking delegate a:");
            a("A");
            Console.WriteLine("Invoking delegate b:");
            b("B");
            Console.WriteLine("Invoking delegate c:");
            c("C");
            Console.WriteLine("Invoking delegate d:");
            d("D");
            Console.ReadLine();

        }
    }
}
