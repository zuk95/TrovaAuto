using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TrovaAuto.Database;
using TrovaAuto.Dominio;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TrovaAuto.UI
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CronologiaPage : ContentPage
    {
        public ObservableCollection<Posizione> Posizioni { get; set; }

        public CronologiaPage()
        {
            InitializeComponent();
            Posizioni = new ObservableCollection<Posizione>();

            InizializzaLista();
        }

        private async void InizializzaLista()
        {
            PosizioneDatabase db = new PosizioneDatabase();
            List<Posizione> list = await db.GetPosizioniAsync();

            if(list.Count == 0)
            {
                return;
            }

            list = list.OrderByDescending(x => x.Id).ToList();
            foreach (Posizione item in list)
            {
                Posizioni.Add(item);
            }

            ultimaPosizioneFrame.BindingContext = Posizioni[0];
            Posizioni.RemoveAt(0);
            listaPosizioni.ItemsSource = Posizioni;
        }

        private async void listaPosizioni_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            try
            {
                Posizione p = e.Item as Posizione;
                await Navigation.PushAsync(new MappaPage(p));
                if (sender is ListView listView) listView.SelectedItem = null;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Errore", $"Problema apertura mappa: {ex.Message}", "OK");
            }
        }

        private async void TapGestureRecognizer_Tapped_ultimaPosizioneFrame(object sender, EventArgs e)
        {
            indicatore.IsEnabled = true;
            indicatore.IsRunning = true;

            try
            {
                Posizione p = (sender as Frame).BindingContext as Posizione;
                if (p == null)
                    return;

                await Navigation.PushAsync(new MappaPage(p));
            }
            catch (Exception ex)
            {
                await DisplayAlert("Errore", $"Problema apertura mappa: {ex.Message}", "OK");
            }
            finally
            {
                indicatore.IsEnabled = false;
                indicatore.IsRunning = false;
            }
        }

        
    }
}