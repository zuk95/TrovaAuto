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
                /*ImpostazioniDatabase impostazionidatabase = new ImpostazioniDatabase();
                Impostazioni impostazioni = impostazionidatabase.GetImpostazioniAsync().Result[0];

                int limiteNumeroPosizioni = impostazioni.NumeroAcquisizioniMassimo;
                int numeroPosizioni = this.GetCountPosizioni().Result[0];
                int scarto = numeroPosizioni - limiteNumeroPosizioni;

                if(scarto > 0)
                {

                }*/

                this.DeleteFirstRowsAsync().Wait();
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

        private Task<int> DeleteFirstRowsAsync()
        {
            ImpostazioniDatabase impostazionidatabase = new ImpostazioniDatabase();
            Impostazioni impostazioni = impostazionidatabase.GetImpostazioniAsync().Result[0];
            return Database.ExecuteAsync($"DELETE FROM [Posizione] where Id NOT IN (select Id FROM [Posizione] ORDER BY Id LIMIT {(impostazioni.NumeroAcquisizioniMassimo - 1)})");
        }

        /*public Task<List<int>> GetCountPosizioni()
        {
            return Database.QueryAsync<int>("SELECT COUNT(*) FROM [Posizione]");
        }*/

    }
}
