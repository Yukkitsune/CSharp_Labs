using CSharp_Lab13.Data;

namespace CSharp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var localDbService = new LocalDbService();

            // Получение списка таблиц
            var tableNames = await localDbService.GetTableNamesAsync();
            Console.WriteLine("Tables in the database:");
            foreach (var tableName in tableNames)
            {
                Console.WriteLine($"- {tableName}");
            }
        }
    }
}