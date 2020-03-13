using System;
using System.Collections.Generic;
using TrovaAuto.Database;
using TrovaAuto.Dominio;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TrovaAuto.UI
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ImpostazioniPage : ContentPage
    {
        private ImpostazioniDatabase database;
        private Impostazioni impostazioni;
        private readonly List<int> listaNumeroAcquisizione;

        public ImpostazioniPage()
        {
            InitializeComponent();
            database = new ImpostazioniDatabase();
            listaNumeroAcquisizione = new List<int>();
            InizializzaPickerAcquisizioni();
        }

        private void InizializzaPickerAcquisizioni()
        {
            for (int i = 1; i <= CostantiDominio.MASSIME_ACQUISIZIONI_POSSIBILI; i++)
                listaNumeroAcquisizione.Add(i);

            pickerNumeroAcquisizioniDaTenere.ItemsSource = listaNumeroAcquisizione;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            try
            {
                List<Impostazioni> tmp = await database.GetImpostazioniAsync();
                if(tmp.Count == 0)
                {
                    await DisplayAlert("Errore", "Nessun impostazione salvata", "OK");
                    return;
                }
                impostazioni = tmp[0];
                pickerNumeroAcquisizioniDaTenere.SelectedIndex = (impostazioni.NumeroAcquisizioniMassimo - 1);
                //checkBoxConTimer.IsChecked = impostazioni.AcquisizioneConTimer;
            }
            catch (Exception ex)
            {
                await DisplayAlert("ERRORE", $"Si è verificato un problema: {ex.Message}", "OK");
            }
        }

        protected async override void OnDisappearing()
        {
            base.OnDisappearing();

            try
            {
                impostazioni.NumeroAcquisizioniMassimo = Convert.ToInt32( pickerNumeroAcquisizioniDaTenere.SelectedItem);
                await database.SalvaImpostazioniAsync(impostazioni);
            }
            catch (Exception ex)
            {
                await DisplayAlert("ERRORE", $"Si è verificato un problema: {ex.Message}", "OK");
            }
        }

        private async void PulisciAcquisizioni_Tapped(object sender, EventArgs e)
        {
            indicatorePulisicposizioni.IsEnabled = true;
            indicatorePulisicposizioni.IsRunning = true;
            PosizioneDatabase dbPosizione = new PosizioneDatabase();
            try
            {
                bool risposta = await DisplayAlert("Attenzione", "Sei sicuro di voler eliminare tutte le acquisizioni?", "Si","No");
                if (risposta)
                {
                    await dbPosizione.DeleteAllPosizioniAsync();
                    await DisplayAlert("", "Acquisizioni correttamente eliminate", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("ERRORE", $"Problema nell'eliminazione delle posizioni: {ex.Message}", "OK");
            }
            finally
            {
                indicatorePulisicposizioni.IsEnabled = false;
                indicatorePulisicposizioni.IsRunning = false;
            }
        }

        //private async void TapGestureRecognizer_Tapped_donazioneFrame(object sender, EventArgs e)
        //{
        //    indicatoreDonazione.IsEnabled = true;
        //    indicatoreDonazione.IsRunning = true;

        //    try
        //    {
        //        bool risposta = await DisplayAlert("Nota", "L'indirizzo email a cui dovrai offrire il caffè comparirà nella barra in alto", "OK", "ANNULLA");
        //        if (!risposta)
        //            return;

        //        //await Clipboard.SetTextAsync(CostantiDominio.EMAIL_FATTURAZIONE_DONAZIONE);
        //        await Navigation.PushAsync(new DonazionePage());
        //    }
        //    catch (Exception ex)
        //    {
        //        await DisplayAlert("ERRORE", $"Problema nell'apertura di paypal: {ex.Message}", "OK");
        //    }
        //    finally
        //    {
        //        indicatoreDonazione.IsEnabled = false;
        //        indicatoreDonazione.IsRunning = false;
        //    }
        //}

    }
}