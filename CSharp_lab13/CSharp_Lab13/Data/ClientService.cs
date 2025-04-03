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
    public class ClientService
    {
        private readonly SQLiteAsyncConnection _connection;
        public ClientService(LocalDbService databaseService)
        {
            _connection = databaseService.Connection;
        }
        public async Task<List<Client>> GetClientsAsync()
        {
            return await _connection.Table<Client>().ToListAsync();
        }
        public async Task<Client> GetClientByIdAsync(int clientId)
        {
            return await _connection.FindAsync<Client>(clientId);
        }
        public async Task AddClientAsync(Client client)
        {
            await _connection.InsertAsync(client);
        }
        public async Task UpdateClientAsync(Client client)
        {
            await _connection.UpdateAsync(client);
        }
        public async Task DeleteClientAsync(int clientId)
        {
            var client = await GetClientByIdAsync(clientId);
            if (client != null)
            {
                await _connection.DeleteAsync(client);
            }
        }
    }
}
