using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;

namespace TrovaAuto.Droid
{
    [BroadcastReceiver]
    public class NotificationReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            string message = intent.GetStringExtra("message");
            string title = intent.GetStringExtra("title");
            int idNotifica = intent.GetIntExtra("id",-1);

            if (idNotifica == -1)
                throw new ArgumentException("Il lancio della notifica non è andato a buon fine", "idNotifica è -1");

            var resultIntent = new Intent(context, typeof(MainActivity));
            resultIntent.SetFlags(ActivityFlags.NewTask | ActivityFlags.ClearTask);

            var pending = PendingIntent.GetActivity(context, 0,
                resultIntent,
                PendingIntentFlags.CancelCurrent);

            var builder =
                new NotificationCompat.Builder(context, "default")
                    .SetContentTitle(title)
                    .SetContentText(message)
                    .SetSmallIcon(Resource.Mipmap.mainIcon)
                    .SetDefaults((int)NotificationDefaults.Sound | (int)NotificationDefaults.Vibrate);

            builder.SetContentIntent(pending);

            var notification = builder.Build();

            var manager = NotificationManager.FromContext(context);
            manager.Notify(idNotifica, notification);
        }
    }
}