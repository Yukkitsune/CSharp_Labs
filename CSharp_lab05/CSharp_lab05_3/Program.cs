using System.Collections;
using System.Collections.Generic;
class MyDictionary<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
{
    private List<KeyValuePair<TKey, TValue>> items = [];

    public void Add(TKey key, TValue value)
    {
        items.Add(new KeyValuePair<TKey, TValue>(key, value));
    }

    public TValue this[TKey key]
    {
        get
        {
            var item = items.Find(i => EqualityComparer<TKey>.Default.Equals(i.Key, key));
            if (!item.Equals(default(KeyValuePair<TKey, TValue>)))
            {
                return item.Value;
            }
            throw new KeyNotFoundException($"The key '{key}' was not found in the dictionary.");
        }
    }

    public int Count
    {
        get { return items.Count; }
    }

    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
    {
        return items.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
class Program
{
    static void Main(string[] args)
    {
        MyDictionary<string, int> myDict = new()
        {
            { "one", 1 },
            { "two", 2 },
            { "three", 3 }
        };


        Console.WriteLine("Count: " + myDict.Count);
        Console.WriteLine("\nValue for key 'two': " + myDict["two"]);

        Console.WriteLine("\nIterating through the dictionary:");
        foreach (var item in myDict)
        {
            Console.WriteLine($"{item.Key}: {item.Value}");
        }
    }
}