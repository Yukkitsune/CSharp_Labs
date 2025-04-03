using Third;

class Program
{
    static void Main()
    {
        Parallel.For(0, 20, i =>
        {
            int randomNumber = SingleRandomizer.Instance.Next(1, 100);
            Console.WriteLine($"Thread: {Thread.CurrentThread.ManagedThreadId}: Random number: {randomNumber}");
        });
        Console.WriteLine("Press Enter to exit...");
        Console.ReadLine();
    }
}