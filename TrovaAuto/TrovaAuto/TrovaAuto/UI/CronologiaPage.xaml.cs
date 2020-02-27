using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class CronologiaPage : ContentPage
    {
        public ObservableCollection<Posizione> Posizioni { get; set; }

        public CronologiaPage()
        {
            InitializeComponent();
            Posizioni = new ObservableCollection<Posizione>();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            PosizioneDatabase db = new PosizioneDatabase();
            List<Posizione> list = await db.GetPosizioniAsync();
            list = list.OrderByDescending(x => x.Id).ToList();
            foreach (Posizione item in list)
            {
                Posizioni.Add(item);
            }

            listaPosizioni.ItemsSource = Posizioni;
        }

        private async void listaPosizioni_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            
            try
            {
                Posizione p = e.SelectedItem as Posizione;
                await Navigation.PushAsync(new MappaPage(p));
            }
            catch (Exception ex)
            {
                await DisplayAlert("Errore", $"Problema apertura mappa: {ex.Message}", "OK");
            }
        }
    }
}