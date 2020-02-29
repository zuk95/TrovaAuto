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
        public SuccessoPagina()
        {
            InitializeComponent();
            InizializzaElementiPagina();
        }

        private void InizializzaElementiPagina()
        {
            successIcon.Source = ImageSource.FromResource("TrovaAuto.ImmaginiCondivise.succesimage.png");
            successFotoIcon.Source = ImageSource.FromResource("TrovaAuto.ImmaginiCondivise.succesimage.png");
        }

        private async void TapGestureRecognizer_Tapped_faifoto(object sender, EventArgs e)
        {
            indicatore.IsVisible = true;
            indicatore.IsRunning = true;

            try
            {
                UserDevice device = new UserDevice();
                Posizione posizioneDaAggiornare = await device.GetUltimaPosizioneSalvata();
                Stream fotoStream = await FotoCreator.ScattaFoto();
                if (fotoStream != null)
                {
                    posizioneDaAggiornare.byteImmagine = ConvertStreamtoByte(fotoStream);
                    PosizioneDatabase dbPos = new PosizioneDatabase();
                    await dbPos.SalvaPosizioneAsync(posizioneDaAggiornare);
                    successFotoIcon.IsVisible = true;
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("ERRORE", $"Impossibile scattare la foto: {ex.Message}", "OK");
            }
            finally 
            {
                indicatore.IsVisible = false;
                indicatore.IsRunning = false;
            }
        }

        private byte[] ConvertStreamtoByte(Stream input)
        {
            using (var ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }
        }
    }
}