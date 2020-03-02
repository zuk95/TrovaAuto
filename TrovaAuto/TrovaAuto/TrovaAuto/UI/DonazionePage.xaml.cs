using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrovaAuto.Dominio;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TrovaAuto.UI
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DonazionePage : ContentPage
    {
        public DonazionePage()
        {
            InitializeComponent();
            this.Title = CostantiDominio.EMAIL_FATTURAZIONE_DONAZIONE;
        }
    }
}