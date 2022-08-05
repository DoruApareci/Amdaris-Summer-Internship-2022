using System;

namespace SimpleEvent
{
    // Define the delegate type
    delegate void MyEventHandler();

    // Define class-publisher
    class MyEvent
    {
        public event MyEventHandler SomeEvent;

        // This method will generate event
        public void OnSomeEvent()
        {
            if (SomeEvent != null)
                SomeEvent();
        }
    }
    class X
    {
        public void Xhandler()
        {
            Console.WriteLine("Event notification is received by the instance of X.");
        }
    }
    class Y
    {
        public void Yhandler()
        {
            Console.WriteLine("Event notification is received by the instance of Y.");
        }
    }
    class Program
    {
        static void handler()
        {
            Console.WriteLine("Event notification is received by the static class Program.");
        }
        static void Main(string[] args)
        {
            MyEvent evt = new MyEvent();
            X xOb = new X();
            Y yOb = new Y();
            
            // Add references to the event's subscription list (subscribe)
            evt.SomeEvent += new MyEventHandler(handler);
            evt.SomeEvent += new MyEventHandler(xOb.Xhandler);
            evt.SomeEvent += new MyEventHandler(yOb.Yhandler);

            // Invoke generation of the event
            evt.OnSomeEvent();
            Console.WriteLine();

            // Remove one reference from the.subscription list (unsubscribe)
            evt.SomeEvent -= new MyEventHandler(xOb.Xhandler);

            evt.OnSomeEvent();

            Console.ReadLine();
        }
    }
}
