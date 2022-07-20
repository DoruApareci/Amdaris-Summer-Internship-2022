Console.WriteLine("Dati numarul de elemente: ");
int nr = int.Parse(Console.ReadLine());
//int[] arr = new int[nr];
int[] arr = { 3, 8, 7, 3, 3, 5, 3, 2, 1, 3, 3 }; //test case
//Random rd = new Random();
//for (int i = 0; i < nr; i++)
//{
//    //for random elements
//    arr[i] = rd.Next(1, 100);
//    //for consecutive elements
//    //arr[i] = i + 1;
//}

var found = from x in arr where arr.Count(c => c == x) > (arr.GetLength(0) / 2) select x;
if (found.Count()==0)
{
    Console.WriteLine("Nu au fost gasite elementele");
    return;
}
Console.WriteLine("Elementele duplicate care indepolinesc conditia problemei:");
foreach (var item in found.Distinct())
{
    Console.WriteLine($"{item} ");
}