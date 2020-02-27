using System;
using System.IO;
using TrovaAuto.Database;
using TrovaAuto.Dominio;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace TrovaAuto.UI
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MappaPage : ContentPage
    {
        private Posizione posizione;

        public MappaPage(Posizione _posizione)
        {
            InitializeComponent();

            this.posizione = _posizione;
            InizializzaMappa();
        }

        private void InizializzaMappa()
        {
            Position position = new Position(this.posizione.Latitudine, this.posizione.Longitudine);
            map.Pins.Add(new Pin() { Label = "La tua Auto", Position = position });
            map.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromMiles(1)).WithZoom(20));
        }

        private async void toolbarFoto_Clicked(object sender, EventArgs e)
        {
            if (this.posizione.byteImmagine == null)
            {
                await DisplayAlert("Messaggio", "Nessuna foto in allegato per questa posizione", "OK");
                return;
            }

            try
            {
                var stream = new MemoryStream(this.posizione.byteImmagine);
                await Navigation.PushAsync(new ImmaginePage(stream));
            }
            catch (Exception ex)
            {
                await DisplayAlert("ERRORE", $"Si è verificato un problema nell'elaborare l'immagine: {ex.Message}", "OK");
            }
        }

    }
}