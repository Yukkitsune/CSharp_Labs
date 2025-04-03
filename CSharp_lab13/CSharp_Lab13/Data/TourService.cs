using CSharp_Lab13.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_Lab13.Data
{
    public class TourService
    {
        private readonly SQLiteAsyncConnection _connection;
        public TourService(LocalDbService databaseService)
        {
            _connection = databaseService.Connection;
        }
        public async Task<List<Tour>> GetToursAsync()
        {
            return await _connection.Table<Tour>().ToListAsync();
        }
        public async Task<Tour> GetTourByIdAsync(int tourId)
        {
            return await _connection.FindAsync<Tour>(tourId);
        }
        public async Task AddTourAsync(Tour tour)
        {
            await _connection.InsertAsync(tour);
        }
        public async Task UpdateTourAsync(Tour tour)
        {
            await _connection.UpdateAsync(tour);
        }
        public async Task DeleteTourAsync(int tourId)
        {
            var tour = await GetTourByIdAsync(tourId);
            if (tour != null)
            {
                await _connection.DeleteAsync(tour);
            }
        }
    }
}
