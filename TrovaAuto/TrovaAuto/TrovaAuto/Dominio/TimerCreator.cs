using System;
using System.Collections.Generic;
using System.Text;

namespace TrovaAuto.Dominio
{
    public static class TimerCreator
    {
        public static void ImpostaTimer(Posizione posizioneAssociata,int ore,int minuti)
        {
            DateTime ora = DateTime.Now;
            DateTime dataNotifica = new DateTime(
                ora.Year, ora.Month, ora.Day, ore, minuti, 0
                );
            INotificaCreator notificaCreator = new NotificaCreatorNotifierPlugin();
            notificaCreator.CreaNotifica(posizioneAssociata, dataNotifica);
        }
    }
}
