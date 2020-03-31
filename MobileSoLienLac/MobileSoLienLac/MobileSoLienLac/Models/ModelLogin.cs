using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using MobileSoLienLac.Models.SQL;

namespace MobileSoLienLac.Models
{
    public class ModelLogin
    {
        public async Task<DataTable> PostUserToDatabase(string NameUser, string PasswordUser)
        {
            return await new Helper().ExecuteQuery("DangNhapPH",
                new SqlParameter("@TaiKhoan", SqlDbType.VarChar) { Value = NameUser },
                new SqlParameter("@MatKhau", SqlDbType.VarChar) { Value = PasswordUser });
        }

        public int CheckEntryNull(string UserAccount, string PassAccount)
        {
            if (UserAccount == "" || UserAccount == null)
            {
                return 1;
            }
            if (PassAccount == "" || PassAccount == null)
            {
                return 2;
            }
            return 0;
        }
    }
}
