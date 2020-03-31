using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using MobileSoLienLac.DTO;
using Xamarin.Forms;

using MobileSoLienLac.Models;
using MobileSoLienLac.Views;

namespace MobileSoLienLac.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        public ThongTinHS StudentAccept { get; set; }
        public FormatLayout Format { get; set; }
        public double Height { get; set; }

        public HomeViewModel(double xHeight)
        {
            Title = "Sổ liên lạc điện tử";
            StudentAccept = App.StudentSeclect;

            Height = xHeight / 5;
        }

    }
}