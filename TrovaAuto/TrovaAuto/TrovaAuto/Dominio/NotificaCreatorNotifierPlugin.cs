using Plugin.LocalNotifications;

namespace TrovaAuto.Dominio
{
    public class NotificaCreatorNotifierPlugin : NotificaCreator
    {

        public override void SchedulaNotifica(Notifica notifica)
        {
            CrossLocalNotifications.Current.Show(notifica.Titolo, notifica.Messaggio, notifica.IdNotifica, notifica.DataNotifica);
        }

        public override void CancellaNotifica(Posizione posizioneAssociata)
        {
            CrossLocalNotifications.Current.Cancel(posizioneAssociata.Id);
        }
    }
}