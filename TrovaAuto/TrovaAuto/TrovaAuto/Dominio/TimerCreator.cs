using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

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
            //INotificaCreator notificaCreator = new NotificaCreatorNotifierPlugin();
            INotificaCreator notificaCreator = DependencyService.Get<INotificaCreator>();
            notificaCreator.CreaNotifica(posizioneAssociata, dataNotifica);
        }
    }
}
