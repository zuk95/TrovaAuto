using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V4.App;
using Xamarin.Forms;
using AndroidApp = Android.App.Application;
using TrovaAuto.Dominio;

[assembly: Dependency(typeof(TrovaAuto.Droid.AndroidNotificationManager))]
namespace TrovaAuto.Droid
{
    /// <summary>
    /// Implementazione nativa di InotificaCreator del PCL. Verrà utilizzata nel PCL utilizzando il DepencyService di Xamarin
    /// </summary>
    public class AndroidNotificationManager : INotificaCreator
    {
        private const string channelId = "default";
        private const string channelName = "Default";
        private const string channelDescription = "The default channel for notifications.";
        private const int pendingIntentId = 0;
        private bool channelInitialized = false;
        private NotificationManager manager;

        public const string TitleKey = "title";
        public const string MessageKey = "message";
        public const string IdNotifica = "id";

        //public event EventHandler NotificationReceived;

        public void Inizializza()
        {
            CreateNotificationChannel();
        }

        public void CreaNotifica(Posizione posizione,DateTime dateTime)
        {
            if (!channelInitialized)
            {
                CreateNotificationChannel();
            }

            Intent intent = new Intent(AndroidApp.Context, typeof(NotificationReceiver));
            intent.PutExtra(TitleKey, CostantiDominio.TITOLO_NOTIFICA);
            intent.PutExtra(MessageKey, CostantiDominio.MESSAGGIO_NOTIFICA);
            intent.PutExtra(IdNotifica, posizione.Id);

            PendingIntent pendingIntent = PendingIntent.GetBroadcast(AndroidApp.Context, pendingIntentId, intent, PendingIntentFlags.OneShot);
            Java.Util.Calendar calendar = Java.Util.Calendar.Instance;
            calendar.Set(Java.Util.CalendarField.HourOfDay, dateTime.Hour);
            calendar.Set(Java.Util.CalendarField.Minute, dateTime.Minute);
            calendar.Set(Java.Util.CalendarField.Second, 0);

            AlarmManager alarmManager = AndroidApp.Context.GetSystemService(Context.AlarmService) as AlarmManager;
            alarmManager.Set(AlarmType.RtcWakeup, calendar.TimeInMillis, pendingIntent);
        }

        public void CancellaNotifica(Posizione posizione)
        {
            throw new NotImplementedException();
        }

        /*public void ReceiveNotification(string title, string message)
        {
            var args = new NotificationEventArgs()
            {
                Title = title,
                Message = message,
            };
            NotificationReceived?.Invoke(null, args);
        }*/

        private void CreateNotificationChannel()
        {
            manager = (NotificationManager)AndroidApp.Context.GetSystemService(AndroidApp.NotificationService);

            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                var channelNameJava = new Java.Lang.String(channelName);
                var channel = new NotificationChannel(channelId, channelNameJava, NotificationImportance.Default)
                {
                    Description = channelDescription
                };
                manager.CreateNotificationChannel(channel);
            }

            channelInitialized = true;
        }
    }
}