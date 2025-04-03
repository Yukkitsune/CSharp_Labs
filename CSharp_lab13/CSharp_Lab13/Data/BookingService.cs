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
    public class BookingService
    {
        private readonly SQLiteAsyncConnection _connection;
        public BookingService(LocalDbService databaseService)
        {
            _connection = databaseService.Connection;
        }
        public async Task AddBookingAsync(Booking booking)
        {
            await _connection.InsertAsync(booking);

        }
        public async Task<List<Booking>> GetBookingsAsync()
        {
            return await _connection.Table<Booking>().ToListAsync();
        }
        public async Task<Booking> GetBookingByIdAsync(int bookingId)
        {
            return await _connection.FindAsync<Booking>(bookingId);
        }
        public async Task UpdateBookingAsync(Booking booking)
        {
            await _connection.UpdateAsync(booking);
        }
        public async Task DeleteBookingAsync(int bookingId)
        {
            var booking = await GetBookingByIdAsync(bookingId);
            if (booking != null)
            {
                await _connection.DeleteAsync(booking);
            }
        }
        public async Task<List<Booking>> GetBookingsByClientIdAsync(int clientId)
        {
            return await _connection.Table<Booking>().Where(x => x.ClientId == clientId).ToListAsync();
        }
        public async Task<List<Booking>> GetBookingsByTourIdAsync(int tourId)
        {
            return await _connection.Table<Booking>().Where(x => x.TourId == tourId).ToListAsync();
        }
        public async Task<List<Booking>> GetBookingsByClientIdAndTourIdAsync(int clientId, int tourId)
        {
            return await _connection.Table<Booking>()
                .Where(x => x.ClientId == clientId && x.TourId == tourId)
                .ToListAsync();
        }

    }
}
