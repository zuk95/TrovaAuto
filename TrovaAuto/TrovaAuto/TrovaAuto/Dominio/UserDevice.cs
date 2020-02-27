﻿using System;
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
        private byte[] ConvertStreamtoByte(Stream input)
        {
            using (var ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }
        }

        public async Task AcquisisciPosizione()
        {
            try
            {
                Posizione posizioneDaSalvare = await PosizioneCreator.AcquisisciPosizione();
                Impostazioni impostazioni = await GetImpostazioni();
                if (impostazioni.AcquisizioneConFoto)
                {
                    Stream fotoStream = await FotoCreator.ScattaFoto();
                    if(fotoStream != null)
                        posizioneDaSalvare.byteImmagine = ConvertStreamtoByte(fotoStream);
                }

                await database.SalvaPosizioneAsync(posizioneDaSalvare);
            }
            catch (Exception ex)
            {
                throw ex;
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
