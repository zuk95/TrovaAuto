using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrovaAuto.Dominio;

namespace TrovaAuto.Database
{
    public class ImpostazioniDatabase
    {
        private static SQLiteAsyncConnection Database => lazyInitializer.Value;

        private static readonly Lazy<SQLiteAsyncConnection> lazyInitializer = new Lazy<SQLiteAsyncConnection>(() =>
        {
            return new SQLiteAsyncConnection(CostantiDatabase.DatabasePath, CostantiDatabase.Flags);
        });

        public ImpostazioniDatabase()
        {
            Database.CreateTablesAsync(CreateFlags.None, typeof(Impostazioni)).Wait();
            InizializzaTabella();
        }

        private async void InizializzaTabella()
        {
            List<Impostazioni> tmp = await GetImpostazioniAsync();
            if(tmp.Count == 0)
            {
                Impostazioni impostazioniDefault = new Impostazioni() { IdImpostazione = 1 , NumeroAcquisizioniMassimo = 20 };
                await Database.InsertAsync(impostazioniDefault);
            }
        }


        public Task<List<Impostazioni>> GetImpostazioniAsync()
        {
            return Database.Table<Impostazioni>().ToListAsync();
        }

        public Task<int> SalvaImpostazioniAsync(Impostazioni impostazioni)
        {
            if (impostazioni.IdImpostazione != 0)
            {
                return Database.UpdateAsync(impostazioni);
            }
            else
            {
                return Database.InsertAsync(impostazioni);
            }
        }

        public Task<int> DeleteImpostazioniAsync(Impostazioni impostazioni)
        {
            return Database.DeleteAsync(impostazioni);
        }
    }
}
