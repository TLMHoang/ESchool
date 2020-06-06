using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Threading.Tasks;
using MobileSoLienLac.DTO;
using Xamarin.Forms;

using MobileSoLienLac.Models;
using MobileSoLienLac.Models.SQL;
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


        public async Task<int> EditBHYT(int IDStudent, byte BHYT, byte DK)
        {
            return await new Helper().ExecuteNonQuery("M_UpdateBHYT",
                new SqlParameter("@IDHocSinh", SqlDbType.Int) {Value = IDStudent},
                new SqlParameter("@BHYT", SqlDbType.Bit) { Value = BHYT },
                new SqlParameter("@DK", SqlDbType.Bit) {Value = DK});
        }
    }
}