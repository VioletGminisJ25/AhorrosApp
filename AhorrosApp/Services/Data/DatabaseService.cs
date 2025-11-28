using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using AhorrosApp.Models;
using System.Diagnostics;

namespace AhorrosApp.Services.Data
{
    public class DatabaseService
    {
        private SQLiteAsyncConnection _database;

        private string DatabasePath =>
            Path.Combine(FileSystem.AppDataDirectory, "AhorrosApp.db3");


        public DatabaseService()
        {
            if (_database is not null)
                return;
            _database = new SQLiteAsyncConnection(DatabasePath);
            InitializeAsync();
            Debug.WriteLine($"Database path: {DatabasePath}");
        }

        async void InitializeAsync()
        {
            try
            {

                Debug.WriteLine("DatabasePath: "+ DatabasePath);
                await _database.CreateTableAsync<Categoria>();
                await _database.CreateTableAsync<Gasto>();
                await _database.CreateTableAsync<ObjetivoAhorro>();

                await SeedCategoriesAsync();
                Debug.WriteLine("Database initialized!");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception DatabaseService -> InitializeAsync(): {ex.Message}");
            }
        }

        public Task<int> SaveGastoAsync(Gasto gasto)
        {
            try
            {

                return _database.InsertAsync(gasto);
            }
            catch (SQLiteException ex)
            {
                Debug.WriteLine($"SQLiteException DatabaseService -> SaveGastoAsync(): {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception DatabaseService -> SaveGastoAsync(): {ex.Message}");
                throw;
            }
        }

        public Task<List<Gasto>> GetGastosAsync()
        {
            try
            {
                return _database.Table<Gasto>().ToListAsync();

            }
            catch (SQLiteException ex)
            {
                Debug.WriteLine($"SQLiteException DatabaseService -> GetGastosAsync(): {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception DatabaseService -> GetGastosAsync(): {ex.Message}");
                throw;
            }
        }


        async Task SeedCategoriesAsync()
        {
            try
            {

                if (await _database.Table<Categoria>().CountAsync() == 0)
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
                    await _database.InsertAllAsync(categorias);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception DatabaseService -> SeedCategoriesAsync(): {ex.Message}");
            }
        }
    }
}
