namespace CustomInterface
{
    //No data, no ctors!
    public interface IPointy
    {
        // Implicitly public and abstract.
        byte Points { get; }
    }

    // The 3D drawing behavior.
    public interface IDraw3D
    {
        void Draw();
    }

    // Three interfaces each defining identical methods.
    public interface IDraw
    {
        void Draw();
    }

    public interface IDrawToPrinter
    {
        void Draw();
    }
}
_________________________________________________________
using System;

namespace CustomInterface
{
    public abstract class Shape
    {
        // Shapes can be assigned a friendly pet name.
        protected string petName;

        // Constructors.
        public Shape() { petName = "NoName"; }
        public Shape(string s) { petName = s; }

        // Draw() is now completely abstract (note semicolon).
        public abstract void Draw();

        public string PetName
        {
            get { return petName; }
            set { petName = value; }
        }
    }

    // A given class may implement as many interfaces as necessary,
    // but may have exactly 1 base class.
    public class Hexagon : Shape, IPointy, IDraw3D
    {
        public Hexagon() { }
        public Hexagon(string name) : base(name) { }
        public override void Draw()
        {
            // Recall the Shape class defined the PetName property.
            Console.WriteLine("Drawing {0} the Hexagon", PetName);
        }

        #region IPointy Members
        public byte Points
        {
            get { return 6; }
        }
        #endregion

        #region IDraw3D Members
        // Using explicit method implementation we are able
        // to provide distinct Draw() implementations.
        void IDraw3D.Draw()
        {
            { Console.WriteLine("Drawing Hexagon in 3D!"); }
        }

        #endregion
    }

    public class Triangle : Shape, IPointy
    {
        public Triangle() { }
        public Triangle(string name) : base(name) { }
        public override void Draw()
        { Console.WriteLine("Drawing {0} the Triangle", PetName); }

        #region IPointy Members
        public byte Points
        {
            get { return 3; }
        }
        #endregion
    }

    public class Circle : Shape, IDraw3D
    {
        public Circle() { }
        public Circle(string name) : base(name) { }

        public override void Draw()
        {
            Console.WriteLine("Drawing {0} the circle", PetName);
        }

        #region IDraw3D Members
        void IDraw3D.Draw()
        {
            { Console.WriteLine("Drawing Circle in 3D!"); }
        }
        #endregion
    }

 
    

    #region Extra classes for examples
    // Not deriving from Shape, but still injecting a name clash.
    public class SuperImage : IDraw, IDrawToPrinter, IDraw3D
    {
        void IDraw.Draw()
        {  /* Basic drawing logic. */ }
        void IDrawToPrinter.Draw()
        {  /* Printer logic. */}
        void IDraw3D.Draw()
        {  /* 3D support. */}
    }

    class Spear : IPointy
    {
        #region IPointy Members
        byte IPointy.Points
        {
            get { return 1; }
        }
        #endregion
    }

    class Fork : IPointy
    {
        #region IPointy Members
        byte IPointy.Points
        {
            get { return 4; }
        }
        #endregion
    }

    class PitchFork : IPointy
    {
        #region IPointy Members
        byte IPointy.Points
        {
            get { return 3; }
        }
        #endregion

    }
    #endregion
}
______________________________________________________________________
using System;

namespace CustomInterface
{
    class Program
    {
        #region Static helper methods
        // Interface type as method parameter
        // I'll draw anyone supporting IDraw3D!
        public static void DrawIn3D(IDraw3D itf3d)
        {
            Console.WriteLine("-> Drawing IDraw3D compatible type");
            itf3d.Draw();
        }
        // Interface type as return value
        static IPointy ExtractPointyness(object o)
        {
            if (o is IPointy)
                return (IPointy)o;
            else
                return null;
        }
        #endregion

        static void Main(string[] args)
        {
            Shape[] s = { new Hexagon(), new Circle(), new Triangle("Trident"),
              new Circle("Ring")};
            for (int i = 0; i < s.Length; i++)
            {
                // Recall the Shape base class defines an abstract Draw() member,
                // so all shapes know how to draw themselves.
                s[i].Draw();

                // Who's pointy?
                if (s[i] is IPointy)
                    Console.WriteLine("-> Points: {0} ", ((IPointy)s[i]).Points);
                else
                    Console.WriteLine("-> {0}\'s not pointy!", s[i].PetName);

                // Can I draw you in 3D?
                if (s[i] is IDraw3D)
                    DrawIn3D((IDraw3D)s[i]);

                Console.WriteLine("----------------------------");
            }

            #region Interfaces as return values
            // Attempt to get IPointy.
            Circle c = new Circle();
            // IPointy itfPt = c as IPointy;
            IPointy itfPt = ExtractPointyness(c);
            if (itfPt != null)
                Console.WriteLine("Object has {0} points.", itfPt.Points);
            #endregion

            #region Print all the members in IPointy array
            Console.WriteLine("\n***** Printing out members in IPointy array *****");
            IPointy[] myPointyObjects = new IPointy[] {new Hexagon(), new Spear(),
                new Triangle(), new Fork(), new PitchFork()};

            for (int i = 0; i < myPointyObjects.Length; i++)
                Console.WriteLine("Object has {0} points.", myPointyObjects[i].Points);
            #endregion

            Console.ReadLine();
        }
    }
}
