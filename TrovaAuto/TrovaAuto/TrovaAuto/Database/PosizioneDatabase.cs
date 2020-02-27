using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrovaAuto.Dominio;

namespace TrovaAuto.Database
{
    public class PosizioneDatabase
    {
        private static SQLiteAsyncConnection Database => lazyInitializer.Value;

        private static readonly Lazy<SQLiteAsyncConnection> lazyInitializer = new Lazy<SQLiteAsyncConnection>(() =>
        {
            return new SQLiteAsyncConnection(CostantiDatabase.DatabasePath, CostantiDatabase.Flags);
        });

        public PosizioneDatabase()
        {
             Database.CreateTablesAsync(CreateFlags.None, typeof(Posizione)).Wait();
        }

        public Task<List<Posizione>> GetPosizioniAsync()
        {
            return Database.Table<Posizione>().ToListAsync();
        }

        public Task<List<Posizione>> GetUltimaPosizioneSalvataAsync()
        {
            return Database.QueryAsync<Posizione>("SELECT * FROM [Posizione] ORDER BY Id desc LIMIT 1");
        }

        public Task<Posizione> GetPosizioneAsync(int id)
        {
            return Database.Table<Posizione>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<int> SalvaPosizioneAsync(Posizione posizione)
        {
            if (posizione.Id != 0)
            {
                return Database.UpdateAsync(posizione);
            }
            else
            {
                return Database.InsertAsync(posizione);
            }
        }

        public Task<int> DeletePosizioneAsync(Posizione item)
        {
            return Database.DeleteAsync(item);
        }

        public Task DeleteAllPosizioniAsync()
        {
            return Database.DeleteAllAsync<Posizione>();
        }

    }
}
