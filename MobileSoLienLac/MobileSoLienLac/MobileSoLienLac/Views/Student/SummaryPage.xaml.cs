using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileSoLienLac.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileSoLienLac.Views.Student
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SummaryPage : ContentPage
    {
        public SummaryViewModel DataSummary = new SummaryViewModel();
        public SummaryPage()
        {
            Title = "Tông kết";
            InitializeComponent();

            if (DataSummary.Error.Length == 0)
            {
                BindingContext = DataSummary;
            }
            else
            {
                DisplayAlert("Thông báo", DataSummary.Error, "OK");
            }

        }


        private void TapHKI_OnTapped(object sender, EventArgs e)
        {
            ListViewHKI.IsVisible = !ListViewHKI.IsVisible;
        }

        private void TapHKII_OnTapped(object sender, EventArgs e)
        {
            ListViewHKII.IsVisible = !ListViewHKII.IsVisible;
        }

        private void TapCN_OnTapped(object sender, EventArgs e)
        {
            ListViewCN.IsVisible = !ListViewCN.IsVisible;
        }
    }
}