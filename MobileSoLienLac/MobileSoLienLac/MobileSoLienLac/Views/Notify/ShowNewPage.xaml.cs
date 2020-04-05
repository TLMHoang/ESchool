using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileSoLienLac.Views.Notify
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShowNewPage : ContentPage
    {
        public ShowNewPage()
        {
            InitializeComponent();
        }

        public ShowNewPage(string Title, string date, string Content)
        {
            InitializeComponent();
            this.Title = Title;

            lblDate.Text = date;
            lblContent.Text = Content;
        }
    }
}