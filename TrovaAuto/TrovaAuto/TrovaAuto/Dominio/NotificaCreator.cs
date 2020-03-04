using System;
using System.Collections.Generic;
using System.Text;

namespace TrovaAuto.Dominio
{
    public abstract class NotificaCreator : INotificaCreator
    {

        public void CreaNotifica(Posizione posizioneAssociata, DateTime dataNotifica)
        {
            try
            {
                Notifica notifica = new Notifica(posizioneAssociata);
                notifica.DataNotifica = dataNotifica;
                SchedulaNotifica(notifica);
            }
            catch (Exception ex)
            {
                throw new Exception($"Problema con gestione notifica:{ex.Message}",ex);
            }
        }

        public abstract void SchedulaNotifica(Notifica notifica);
        public abstract void CancellaNotifica(Posizione posizioneAssociata);

    }
}
