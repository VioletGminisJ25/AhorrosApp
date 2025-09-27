using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using AhorrosApp.Models;

namespace AhorrosApp.Services.Data
{
    public class DatabaseService
    {
        private SQLiteAsyncConnection _connection;

        private string DatabasePath =>
            Path.Combine(FileSystem.AppDataDirectory, "AhorrosApp.db3");

        public DatabaseService()
        {
            InitializeAsync();
        }

        async void InitializeAsync()
        {
            if (_connection != null)
                return;
            _connection = new SQLiteAsyncConnection(DatabasePath);
            await _connection.CreateTableAsync<Categoria>();
            await _connection.CreateTableAsync<Gasto>();

            await SeedCategoriesAsync();
        }

        public Task<int> SaveGastoAsync(Gasto gasto)
        {
            return _connection.InsertAsync(gasto);
        }

        public Task<List<Gasto>> GetGastosAsync()
        {
            return _connection.Table<Gasto>().ToListAsync();
        }


        async Task SeedCategoriesAsync()
        {
            if (await _connection.Table<Categoria>().CountAsync() == 0)
            {
                var categorias = new List<Categoria>
                {
                    new Categoria { Nombre = "Alimentos" },
                    new Categoria { Nombre = "Transporte" },
                    new Categoria { Nombre = "Entretenimiento" },
                    new Categoria { Nombre = "Salud" },
                    new Categoria { Nombre = "Educación" },
                    new Categoria { Nombre = "Otros" } 
                };
                await _connection.InsertAllAsync(categorias);
            }
        }
    }
}
