using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrovaAuto.Database;
using TrovaAuto.Dominio;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TrovaAuto.UI
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppPage : ContentPage
    {
        private UserDevice device;
        public AppPage()
        {
            InitializeComponent();
            inizializzaElementiPagina();
            device = new UserDevice();
        }

        private void inizializzaElementiPagina()
        {
            gpsImage.Source = ImageSource.FromResource("TrovaAuto.ImmaginiCondivise.posizioneimage.png");
            carImage.Source = ImageSource.FromResource("TrovaAuto.ImmaginiCondivise.cari.png");
            settingsImage.Source = ImageSource.FromResource("TrovaAuto.ImmaginiCondivise.settings.png");
        }
        private void SettaIndicatore(ActivityIndicator indicatore,bool valore)
        {
            indicatore.IsEnabled = valore;
            indicatore.IsRunning = valore;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            try
            {
                Posizione ultimaPosSalvata = await device.GetUltimaPosizioneSalvata();
                if (ultimaPosSalvata != null)
                    dataUltimaAcquisizioneLabel.Text = "Ultima acquisizione: " + ultimaPosSalvata.DataAcquisizione.ToString();
                else
                    dataUltimaAcquisizioneLabel.Text = "Ultima acquisizione: Nessuna acquisizione";
            }
            catch (Exception ex)
            {
                await DisplayAlert("ERRORE", $"Si è verificato un problema: {ex.Message}", "OK");
            }
        }


        private async void TapGestureRecognizer_Tapped_acquisiciPosizione(object sender, EventArgs e)
        {
            SettaIndicatore(indicatoreGps, true);

            try
            {
                await device.AcquisisciPosizione();
                await Navigation.PushAsync(new SuccessoPagina());
            }
            catch (Exception ex)
            {
                await DisplayAlert("ERRORE", $"Si è verificato un problema: {ex.Message}", "OK");
            }
            finally
            {
                SettaIndicatore(indicatoreGps, false);
            }
        }

        private async void TapGestureRecognizer_Tapped_visualizzaMappa(object sender, EventArgs e)
        {
            SettaIndicatore(indicatoreAuto, true);

            try
            {
                Posizione pos = await device.GetUltimaPosizioneSalvata();
                await Navigation.PushAsync(new MappaPage(pos));
            }
            catch (Exception ex)
            {
                await DisplayAlert("ERRORE", $"Si è verificato un problema: {ex.Message}", "OK");
            }
            finally
            {
                SettaIndicatore(indicatoreAuto, false);
            }
        }

        private async void TapGestureRecognizer_Tapped_impostazioni(object sender, EventArgs e)
        {
            SettaIndicatore(indicatoreImpostazioni, true);

            try
            {
                await Navigation.PushAsync(new ImpostazioniPage());
            }
            catch (Exception ex)
            {
                await DisplayAlert("ERRORE", $"Si è verificato un problema: {ex.Message}", "OK");
            }
            finally
            {
                SettaIndicatore(indicatoreImpostazioni, false);
            }
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            try
            {
                await Navigation.PushAsync(new CronologiaPage());
            }
            catch (Exception ex)
            {
                await DisplayAlert("ERRORE", $"Si è verificato un problema: {ex.Message}", "OK");
            }
        }
    }
}