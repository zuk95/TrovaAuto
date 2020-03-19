using System;
using System.Collections.Generic;
using System.Text;

namespace TrovaAuto.Dominio
{
    /// <summary>
    /// Classe che implementa il pattern interface segregation
    /// Inoltre, questa classe è utilizzata dal dependecyservice android e viene implementata nativamente (vedere AndroidNotificationManager)
    /// </summary>
    public interface INotificaCreator
    {
        void Inizializza();
        void CreaNotifica(Posizione posizioneAssociata, DateTime dataNotifica);
        void CancellaNotifica(Posizione posizioneAssociata);
    }
}
