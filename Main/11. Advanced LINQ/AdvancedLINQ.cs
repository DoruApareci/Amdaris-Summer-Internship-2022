using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp1
{
    public class AdvanceLink
    {
        int[] simpleArray = new int[] { 11, 21, 31, 15, 16, 17, 150, 19, 15, 12, 1, 5, 12, 3, 8, 11, 123 };

        public AdvanceLink()
        {
            //EvenNumbers();
            //TakeSkipOperations();
            //SelectOperations();
            //SelectManyOperations();
            //OtherOperations();
            //Zip();
            //Join();
            //GroupJoin();
            Closure();
        }

        void EvenNumbers()
        {
            var result = simpleArray.Where(x => x % 2 == 0);
            foreach (var item in result)
            {
                Console.Write(item+" ");
            }
        }

        void TakeSkipOperations()
        {
            var take = simpleArray.Take(5).ToArray();
            PrintArray(take, "Take");
            Console.WriteLine();

            var skip = simpleArray.Skip(6).ToArray();
            PrintArray(skip, "Skip");
            Console.WriteLine();

            var takeWhile = simpleArray.TakeWhile(x => x < 150).ToArray();
            PrintArray(takeWhile, "Take While x<150");
            Console.WriteLine();

            var skipWhile = simpleArray.SkipWhile(x => x < 150).ToArray();
            PrintArray(skipWhile, "Skip While x>=150");
            Console.WriteLine();

            var dist = simpleArray.Distinct().ToArray();
            PrintArray(dist, "Distinct");
        }

        void Zip()
        {
            int[] numbers = { 1, 2, 3, 4 };
            string[] words = { "one", "two", "three" };

            var nrW = numbers.Zip(words, (f, s) => f + " " + s).ToArray();
            PrintArray(nrW, "Zp");
        }

        List<Owner> initUserList()
        {
            return new List<Owner>
            {
                new Owner { Name="Jora", Age=15},
                new Owner { Name="Ghita", Age=25},
                new Owner { Name="Thral", Age=19},
                new Owner { Name="Thuuu", Age=19},
                new Owner { Name="Thssl", Age=19}
            };
        }
        void SelectOperations()
        {
            var userList = initUserList();

            Console.WriteLine("=== Select ===");
            var names = userList.Select(x => x.Name).ToList();
            foreach (var item in names)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("=== Projection ===");
            var newObj = userList.Select(x => new { x.Name, x.Age }).ToList();
            foreach (var item in newObj)
            {
                Console.WriteLine(item);
                Console.WriteLine(item.Age);
            }
        }

        private void SelectManyOperations()
        {
            var userList = new List<Owner>
            {
                new Owner {Name="Jora",Age=15 , PetName= new List<Pet>{
                                                new Pet{ PetName="Kesha",Legs=2 },
                                                new Pet{ PetName="Sharic",Legs=3},
                                                new Pet{ PetName="Gaaa",Legs=5}}
                },
                new Owner {Name="Ghita",Age=25 ,PetName= new List<Pet>{new Pet{ PetName="Purr",Legs=4 } }},
                new Owner {Name="Thral",Age=19, PetName= new List<Pet>{
                                                new Pet{PetName="Orro",Legs=0 },
                                                }                   }
            };

            var result = userList.SelectMany(x => x.PetName);
            Console.WriteLine("=== pets ==");
            foreach (var item in result)
            {
                Console.WriteLine($"{item.PetName} {item.Legs}");
            }

            Console.WriteLine("=== owner with pets ==");
            var result1 = userList.SelectMany(
                x => x.PetName,
                (parent, child) => new
                {
                    parent.Name,
                    child.PetName,
                    child.Legs
                });

            foreach (var item in result1)
            {
                Console.WriteLine($"{item.Name} - {item.PetName} - {item.Legs}");
            }
        }
        void OtherOperations()
        {
            var userList = initUserList();

            var repeat = Enumerable.Repeat(new Owner(), 15);

            var firstElement = userList.FirstOrDefault(x => x.Age == 50);

            var sumList = userList.Sum(x => x.Age);
            userList.Count();

            Console.WriteLine("=== Revers ===");

            var nnn = Enumerable.Repeat(new Owner(), 5);

            userList.Reverse();

            foreach (var item in userList)
            {
                Console.WriteLine($"{item.Age} - {item.Name} - {item.PetName} ");
            }

            var orderedList = userList.OrderByDescending(x => x.Age).ThenBy(x => x.Name);
            foreach (var item in orderedList)
            {
                Console.WriteLine($"{item.Age} - {item.Name} - {item.PetName} ");
            }
        }

        void Join()
        {
            var jora = new Owner { Name = "Jora", Age = 15 };
            var ghita = new Owner { Name = "Ghita", Age = 25 };
            var thral = new Owner { Name = "Thral", Age = 19 };

            var car1 = new Car { CarName = "toyota", Owner = jora };
            var car2 = new Car { CarName = "vw", Owner = ghita };
            var car3 = new Car { CarName = "fiat", Owner = thral };
            var car4 = new Car { CarName = "opel", Owner = jora };

            var personList = new List<Owner> { jora, ghita, thral };
            //var personList_1 = new List<Owner> { thral, jora, ghita };
            var carList = new List<Car> { car1, car2, car3, car4 };

            Console.WriteLine("=== Join ===");
            var result = personList.Join(carList,
                owner => owner,
                car => car.Owner,
                (owner, car) =>
                     new
                     {
                         OwnerName = owner.Name,
                         OwnerAge = owner.Age,
                         Car = car.CarName
                     });

            foreach (var item in result)
            {
                Console.WriteLine($"{item.OwnerName} - {item.OwnerAge} - {item.Car}");
            }
        }

        void GroupJoin()
        {
            var jora = new Owner { Name = "Jora", Age = 15 };
            var ghita = new Owner { Name = "Ghita", Age = 25 };
            var thral = new Owner { Name = "Thral", Age = 19 };

            var car1 = new Car { CarName = "toyota", Owner = jora };
            var car2 = new Car { CarName = "vw", Owner = ghita };
            var car3 = new Car { CarName = "fiat", Owner = thral };
            var car4 = new Car { CarName = "opel", Owner = jora };

            var personList = new List<Owner> { jora, ghita, thral };
            //var personList_1 = new List<Owner> { thral, jora, ghita };
            var carList = new List<Car> { car1, car2, car3, car4 };

            Console.WriteLine("=== Join ===");
            var result = personList.GroupJoin(carList,
                owner => owner,
                car => car.Owner,
                (owner, car) =>
                     new
                     {
                         OwnerName = owner.Name,
                         OwnerAge = owner.Age,
                         car
                     });

            foreach (var item in result)
            {
                Console.WriteLine($"{item.OwnerName} - {item.OwnerAge} - {item.car}");
            }
        }
       
        void PrintArray(int[] array, string message)
        {
            Console.WriteLine($"============= {message} ==============");
            foreach (var item in array)
            {
                Console.Write(item+" ");
            }
        }
        void PrintArray(string[] array, string message)
        {
            Console.WriteLine($"============= {message} ==============");
            foreach (var item in array)
            {
                Console.Write(item+" ");
            }
        }

        private void Closure()
        {
            Action a = SomeAction("action a");
            Action b = SomeAction("action b");
            a();
            b();
            a();
            a();
            b();
        }

        Action SomeAction(string action)
        {
            int i = 0;
            return () =>
            {
                Console.WriteLine($"{action}=> {i}");
                i++;
            };
        }
    }

    class Owner
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public List<Pet> PetName { get; set; }
    }

    class Pet
    {
        public string PetName { get; set; }
        public int Legs { get; set; }
    }

    class Car
    {
        public string CarName { get; set; }
        public Owner Owner { get; set; }
    }
}
