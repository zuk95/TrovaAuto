using System;
using System.Collections.Generic;
using System.Text;

namespace TrovaAuto.Dominio
{
    /// <summary>
    /// Classe che implementa il pattern interface segregation
    /// </summary>
    public interface INotificaCreator
    {
        void CreaNotifica(Posizione posizioneAssociata, DateTime dataNotifica);
        void CancellaNotifica(Posizione posizioneAssociata);
    }
}
