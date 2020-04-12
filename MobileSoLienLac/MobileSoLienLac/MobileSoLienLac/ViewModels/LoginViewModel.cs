using MobileSoLienLac.Models.SQL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using MobileSoLienLac.DTO;
using MobileSoLienLac.Models;


namespace MobileSoLienLac.ViewModels
{
    public class LoginViewModel
    {
        public async Task<ValueDTO<TaiKhoan>> CheckLogin(string NameUser, string PasswordUser)
        {
            return await new ModelLogin().PostUserToDatabase(NameUser, PasswordUser);
        }
    }
}