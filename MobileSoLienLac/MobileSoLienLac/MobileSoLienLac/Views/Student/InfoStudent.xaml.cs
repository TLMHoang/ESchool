using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileSoLienLac.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InfoStudent : ContentPage
    {
        public InfoStudent()
        {
            InitializeComponent();
            BindingContext = App.StudentSeclect;
        }
    }
}