using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }
    }
}