using Enumerator_Usage;

DynamicArray<int> da = new DynamicArray<int>();
da.Add(1);
da.Add(2);


foreach (var sub in da)
{
    Console.WriteLine(sub);
}