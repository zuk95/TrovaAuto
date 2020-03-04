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
                throw new Exception($"Acquisizione posizione non riuscita:{ex.Message}", ex);
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

        public async Task<bool> ScattaFoto()
        {

            try
            {
                Posizione posizioneDaAggiornare = await this.GetUltimaPosizioneSalvata();
                Stream fotoStream = await FotoCreator.ScattaFoto();
                if (fotoStream != null)
                {
                    posizioneDaAggiornare.byteImmagine = ConvertStreamtoByte(fotoStream);
                    PosizioneDatabase dbPos = new PosizioneDatabase();
                    await dbPos.SalvaPosizioneAsync(posizioneDaAggiornare);
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception($"Acquisizione foto non riuscita:{ex.Message} ", ex);
            }
            
        }

        public async Task ImpostaTimer(int ore,int minuti)
        {
            try
            {
                Posizione posizioneAssociata = await this.GetUltimaPosizioneSalvata();
                TimerCreator.ImpostaTimer(posizioneAssociata, ore, minuti);
            }
            catch (Exception ex)
            {
                throw new Exception($"Acquisizione timer non riuscita:{ex.Message} ", ex);
            }
        }

        private byte[] ConvertStreamtoByte(Stream input)
        {
            using (var ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }
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
