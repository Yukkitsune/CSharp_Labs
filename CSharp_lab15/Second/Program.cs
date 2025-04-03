using Second;

class Program
{
    static void Main()
    {
        ILoggerRepository textLogger = new TextFileLoggerRepository("log.txt");
        MyLogger logger1 = new MyLogger(textLogger);
        logger1.Log("This is the message for text file.");

        ILoggerRepository jsonLogger = new JsonFileLoggerRepository("log.json");
        MyLogger logger2 = new MyLogger(jsonLogger);
        logger2.Log("This is the message for JSON file.");

        Console.WriteLine("Logs are written. Press Enter to exit...");
        Console.ReadLine();
    }
}