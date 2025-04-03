using CSharp_Lab15;

public class FileObserver : IFileSystemObserver
{
    public void OnFileCreated(string path)
    {
        Console.WriteLine($"File created: {path}");
    }

    public void OnFileDeleted(string path)
    {
        Console.WriteLine($"File deleted: {path}");
    }
}
class Program
{
    static void Main()
    {
        var watcher = new SimpleFileSystemWatcher("C:\\Users\\akito", 1000);
        var observer = new FileObserver();
        watcher.Subscribe( observer );
        Console.WriteLine("Press Enter to exit...");
        Console.ReadLine();
        watcher.Unsubscribe(observer);
    }
}