using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TrovaAuto.UI
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ImmaginePage : ContentPage
    {
        public ImmaginePage(MemoryStream immagineStream)
        {
            InitializeComponent();
            this.immagine.Source = ImageSource.FromStream(() => immagineStream);
        }

    }
}