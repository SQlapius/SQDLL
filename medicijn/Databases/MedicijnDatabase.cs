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
                
                 databaseConnection.Table<Medicijn>().DeleteAsync(x => x.Naam == medicijn.Naam).ConfigureAwait(false);

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

                var kip = await databaseConnection.Table<Medicijn>().ToListAsync();

                var test = await AttemptAndRetry(
                            () => databaseConnection.Table<Medicijn>()
                            .Skip(kip.Count - 5)
                            .ToListAsync())
                            .ConfigureAwait(false);

                test.Reverse();

                return new ObservableCollection<Medicijn>(test);
            }
            
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return default;
            }
        }
    }
}
