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
    public partial class SuccessoPagina : ContentPage
    {
        private UserDevice device;

        public SuccessoPagina()
        {
            InitializeComponent();
            InizializzaElementiPagina();
        }

        private void InizializzaElementiPagina()
        {
            successIcon.Source = ImageSource.FromResource(CostantiDominio.PATH_SUCCESS_ICON);
            successFotoIcon.Source = ImageSource.FromResource(CostantiDominio.PATH_SUCCESS_ICON);
            successTimerIcon.Source = ImageSource.FromResource(CostantiDominio.PATH_SUCCESS_ICON);
            timer.Time = DateTime.Now.TimeOfDay;
        }

        private async void TapGestureRecognizer_Tapped_faifoto(object sender, EventArgs e)
        {
            indicatoreFoto.IsVisible = true;
            indicatoreFoto.IsRunning = true;

            try
            {
                device = new UserDevice();
                bool esito = await device.ScattaFoto();
                if (esito)
                {
                    successFotoIcon.IsVisible = true;
                    allegaFotoFrame.IsEnabled = false;
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("ERRORE", $"Impossibile scattare la foto: {ex.Message}", "OK");
            }
            finally 
            {
                indicatoreFoto.IsVisible = false;
                indicatoreFoto.IsRunning = false;
            }
        }

        private async void TapGestureRecognizer_Tapped_impostaTimer(object sender, EventArgs e)
        {
            indicatoreTimer.IsVisible = true;
            indicatoreTimer.IsRunning = true;
            try
            {
                device = new UserDevice();
                await device.ImpostaTimer(timer.Time.Hours, timer.Time.Minutes);
                timerFrame.IsVisible = false;
                successTimerIcon.IsVisible = true;
                impostaTimeFrame.IsEnabled = false;
            }
            catch (Exception ex)
            {
                await DisplayAlert("ERRORE", $"Impossibile impostare il timer: {ex.Message}", "OK");
            }
            finally
            {
                indicatoreTimer.IsVisible = false;
                indicatoreTimer.IsRunning = false;
            }
        }
    }
}