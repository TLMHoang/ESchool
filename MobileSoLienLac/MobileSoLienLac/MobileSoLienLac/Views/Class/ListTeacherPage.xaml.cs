using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileSoLienLac.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileSoLienLac.Views.Class
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListTeacherPage : ContentPage
    {
        

        public ListTeacherPage()
        {
            InitializeComponent();
        }

        public ListTeacherPage(List<ModelListTeacher> lstTeachers, string title)
        {
            InitializeComponent();

            this.Title = title;

            ListViewListTeacher.ItemsSource = lstTeachers;
        }

    }
}