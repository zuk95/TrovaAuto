using System;
using System.Collections.Generic;
using System.Text;

namespace TrovaAuto.Dominio
{
    /// <summary>
    /// Questa classe rappresenta l'astrazione di oggetti che creano notifiche attraverso i plugin che offre xamarin; Se
    /// non si utilizzano plugin, e la gestione delle notifiche viene implementata nativamente, richiamando il dependecyService, allora 
    /// risulta inutile l'utilizzo di questa classe
    /// </summary>
    public abstract class NotificaCreator : INotificaCreator
    {
        public void Inizializza()
        {
            throw new NotImplementedException();
        }

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
