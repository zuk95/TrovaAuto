using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Plugin.LocalNotifications;

namespace TrovaAuto.Droid
{
    [Activity(Icon = "@mipmap/mainIcon", Theme = "@style/MainTheme", MainLauncher = false, LaunchMode = LaunchMode.SingleTop, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            //Inizializzazione servizio delle mappe in android
            global::Xamarin.FormsMaps.Init(this, savedInstanceState);

            //Inizializzazione servizio notifiche
            InizializzaServizioNotifica();

            //Per far funzionare correttamente il plugin dei permessi(vedere pagina github di PluginPermission)
            Plugin.CurrentActivity.CrossCurrentActivity.Current.Init(this, savedInstanceState);

            LoadApplication(new App());
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            //Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        private void InizializzaServizioNotifica()
        {
            LocalNotificationsImplementation.NotificationIconId = Resource.Mipmap.mainIcon;
        }
    }
}