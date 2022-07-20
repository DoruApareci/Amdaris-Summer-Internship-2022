Console.WriteLine("Dati numarul de elemente: ");
int nr = int.Parse(Console.ReadLine());
int[] arr = new int[nr];
Random rd = new Random();
for (int i = 0; i < nr; i++)
{
    //for random elements
    arr[i] = rd.Next(1, 100);
    //for consecutive elements
    //arr[i] = i + 1;
}
DisplayArr(arr);
if (isConsecutive(arr))
    Console.WriteLine("Array-ul este consecuriv");
else
    Console.WriteLine("Array-ul nu este consecuriv");



bool isConsecutive(int [] arr)
{
    return isAscending(arr) | isDescending(arr);
}

bool isDescending(int[] arr)
{
    bool good = true;
    for (int i = 0; i < arr.GetLength(0)-1; i++)
    {
        if (arr[i] > arr[i+1])
        {
            good = false;
            break;
        }
    }
    return good;
}

bool isAscending(int[] arr)
{
    bool good = true;
    for (int i = 0; i < arr.GetLength(0)-1; i++)
    {
        if (arr[i] < arr[i + 1])
        {
            good = false;
            break;
        }
    }
    return good;
}

void DisplayArr(int[]arr)
{
    Console.WriteLine("Array-ul generat: ");
    for (int i = 0; i < arr.GetLength(0); i++)
    {
        Console.Write($"{arr[i]} ");
    }
    Console.WriteLine();
}