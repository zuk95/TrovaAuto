using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using TrovaAuto.Database;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TrovaAuto.Dominio
{
    public class UserDevice
    {
        private PosizioneDatabase database;
        private ImpostazioniDatabase impostazioniDatabase;

        public UserDevice()
        {
            database = new PosizioneDatabase();
            impostazioniDatabase = new ImpostazioniDatabase();
        }

        public async Task AcquisisciPosizione()
        {
            try
            {
                Posizione posizioneDaSalvare = await PosizioneCreator.AcquisisciPosizione();
                await database.SalvaPosizioneAsync(posizioneDaSalvare);
            }
            catch (Exception ex)
            {
                throw new Exception("Acquisizione posizione non riuscita", ex);
            }
        }

        public async Task<Posizione> GetUltimaPosizioneSalvata()
        {
            List<Posizione> tmp = await database.GetUltimaPosizioneSalvataAsync();
            if (tmp.Count > 0)
                return tmp[0];
            else
                return null;
        }

        private async Task<Impostazioni> GetImpostazioni()
        {
            List<Impostazioni> tmp = await impostazioniDatabase.GetImpostazioniAsync();
            if (tmp.Count > 0)
                return tmp[0];
            else
                return null;
        }

    }
}
