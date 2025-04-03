using System.Collections;
using System.Runtime.InteropServices;
class MyList<T> : IEnumerable<T>
{
    private T[] values;
    private int capacity;

    public MyList(params T[] valuesInit)
    {
        values = new T[valuesInit.Length];
        Array.Copy(valuesInit, values, valuesInit.Length);
        capacity = valuesInit.Length;
    }
    public void Add(T value)
    {
        if (capacity == values.Length)
        {
            int newCapacity = capacity == 0 ? 4 : capacity * 2;
            var newValues = new T[newCapacity];
            Array.Copy(values, newValues, values.Length);
            values = newValues;
        }
        values[capacity] = value;
        capacity++;
    }
    public MyList(IEnumerable<T> collect)
    {
        values = [];
        foreach (var item in collect) {
            Add(item);
        }
    }
    public T this[int index]
    {
        get
        {
            return values[index];
        }
        set
        {
            values[index] = value;
        }
    }
    public IEnumerator<T> GetEnumerator()
    {
        foreach (var item in values) {
            yield return item;
        }
    }
    IEnumerator IEnumerable.GetEnumerator(){
        return GetEnumerator();
    }
    public void Print()
    {
        for (int i = 0;i < capacity; i++)
        {
            Console.Write(values[i] + " ");
        }
        Console.WriteLine();
    }
}
class Program
{
    static void Main(string[] args)
    {
        MyList<int> list = [1, 2, 3, 4, 5, 6];
        list.Print();
        list.Add(13);
        list.Print();
        Console.WriteLine($"\nTotal number of elements: {list.Count()}");

    }
}