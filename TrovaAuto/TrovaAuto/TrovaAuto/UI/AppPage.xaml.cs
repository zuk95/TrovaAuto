using System;
using System.Collections.Generic;
using TrovaAuto.Dominio;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Permissions.Abstractions;
using System.Threading.Tasks;
using Plugin.Permissions;

namespace TrovaAuto.UI
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppPage : ContentPage
    {
        private UserDevice device;
        private IPermessiManager permessiManager;
        public AppPage()
        {
            InitializeComponent();
            inizializzaElementiPagina();
            device = new UserDevice();
            permessiManager = new PermessiManager();
        }

        private void inizializzaElementiPagina()
        {
            gpsImage.Source = ImageSource.FromResource(CostantiDominio.PATH_POSITION_ICON);
            carImage.Source = ImageSource.FromResource(CostantiDominio.PATH_CAR_ICON);
            settingsImage.Source = ImageSource.FromResource(CostantiDominio.PATH_SETTINGS_ICON);
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
                {
                    dataUltimaAcquisizioneLabel.Text = $"Piu recente: {ultimaPosSalvata.DataAcquisizione.ToString("dd/MM/yyyy HH:mm")}";
                    //localitaUltimaAcquisizioneLabel.Text = $"{ultimaPosSalvata.NomeCitta} {ultimaPosSalvata.Via}";
                }
                else
                {
                    dataUltimaAcquisizioneLabel.Text = "Piu recente: Nessuna acquisizione";
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("ERRORE", $"Si è verificato un problema: {ex.Message}", "OK");
            }
        }

        private async Task<bool> CheckPermessi()
        {
            List<Permission> permessiNonConsentiti = await permessiManager.ChiediPermessi();
            //Se tutti sono consentiti la lista è a 0 e salta il foreach, altrimenti si ferma al primo echiede di settare i permessi dalle impostazioni
            foreach (Permission permission in permessiNonConsentiti)
            {
                bool risposta = await DisplayAlert("Nota", permessiManager.GetMessaggioForPermesso(permission), "Si","No");
                if (risposta)
                    permessiManager.ApriImpostazioni();

                return false;
            }

            return true;
        }

        private async void TapGestureRecognizer_Tapped_acquisiciPosizione(object sender, EventArgs e)
        {

            SettaIndicatore(indicatoreGps, true);
            try
            {
                bool check = await CheckPermessi();
                if (!check)
                    return;

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
                bool check = await CheckPermessi();
                if (!check)
                    return;

                Posizione pos = await device.GetUltimaPosizioneSalvata();
                if(pos == null)
                {
                    await DisplayAlert("Attenzione", $"Nessuna posizione salvata", "OK");
                    return;
                }

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
                bool check = await CheckPermessi();
                if (!check)
                    return;

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
                bool check = await CheckPermessi();
                if (!check)
                    return;

                await Navigation.PushAsync(new CronologiaPage());
            }
            catch (Exception ex)
            {
                await DisplayAlert("ERRORE", $"Si è verificato un problema: {ex.Message}", "OK");
            }
        }
    }
}