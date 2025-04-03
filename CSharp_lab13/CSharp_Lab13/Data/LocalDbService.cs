using CSharp_Lab13.Models;
using Microsoft.EntityFrameworkCore;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CSharp_Lab13.Data
{
    public class LocalDbService : DbContext
    {

        private const string DB_NAME = "tour_db.db3";
        public SQLiteAsyncConnection Connection { get; }
        public LocalDbService()
        {
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, DB_NAME);
            //if (File.Exists(dbPath))
            //{
            //    File.Delete(dbPath);
            //}
            Connection = new SQLiteAsyncConnection(dbPath);
            Console.WriteLine("Приложение запущено!");

        }
        public async Task InitializeDatabaseAsync()
        {
            try
            {
                await Connection.CreateTableAsync<Client>();
                await Connection.CreateTableAsync<Tour>();
                await Connection.CreateTableAsync<Booking>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Database initialization error: {ex.Message}");
            }
        }
        public async Task<List<string>> GetTableNamesAsync()
        {
            try
            {
                var result = await Connection.QueryAsync<TableInfo>("SELECT name FROM sqlite_master WHERE type='table'");
                return result.Select(t => t.Name).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving table names: {ex.Message}");
                return new List<string>();
            }
        }

    }
    public class TableInfo
    {
        public string Name { get; set; }
    }
}
