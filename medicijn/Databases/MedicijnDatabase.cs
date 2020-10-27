using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Collections.Generic;
using SQLite;
using Newtonsoft.Json;
using GZIDAL002.Medicijnen.Models;

namespace medicijn.Databases
{
    public class MedicijnDatabase : BaseDatabase
    {
        SQLiteAsyncConnection _databaseConnection;

        public MedicijnDatabase()
        {
            Init();
        }

        async void Init()
        {
            _databaseConnection = await GetDatabaseConnection<Medicijn>();
            await _databaseConnection.CreateTableAsync<Medicijn>();

        }

        public async Task<int> StoreMedicijn(Medicijn medicijn)
        {
            try
            {
                var databaseConnection = await GetDatabaseConnection<Medicijn>().ConfigureAwait(false);

                await databaseConnection.Table<Medicijn>().DeleteAsync(x => x.Naam == medicijn.Naam).ConfigureAwait(false);

                return await _databaseConnection.InsertAsync(medicijn);
                
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return default;
            }
        }

        public async Task<ObservableCollection<Medicijn>> GetLast5Medicijn()
        {
            try
            {
                var databaseConnection = await GetDatabaseConnection<Medicijn>().ConfigureAwait(false);

                var medicijnen = await databaseConnection.Table<Medicijn>().ToListAsync();

                var latestfivemedicijnen = await AttemptAndRetry(
                            () => databaseConnection.Table<Medicijn>()
                            .Skip(medicijnen.Count - 5)
                            .ToListAsync())
                            .ConfigureAwait(false);

                latestfivemedicijnen.Reverse();

                return new ObservableCollection<Medicijn>(latestfivemedicijnen);
            }
            
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return default;
            }
        }
    }
}
