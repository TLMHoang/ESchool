using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileSoLienLac.Models;
using MobileSoLienLac.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.Xaml;

namespace MobileSoLienLac.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListStudentInClassPage : ContentPage
    {
        public List<ModelListStudent> Lst { get; set; }

        public ListStudentInClassPage()
        {
            InitializeComponent();
        }

        public ListStudentInClassPage(ListStudentInClassViewModel _value)
        {
            InitializeComponent();
            this.Title = _value.Message;
            _value.ListStudents.Sort(new SortListStudent());

            ListViewListStudent.ItemsSource = _value.ListStudents;
        }
    }
}