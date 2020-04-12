using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using MobileSoLienLac.DTO;
using MobileSoLienLac.Models.SQL;

namespace MobileSoLienLac.Models
{
    public class ModelLogin :Helper
    {
        public async Task<ValueDTO<TaiKhoan>> PostUserToDatabase(string NameUser, string PasswordUser)
        {
            ValueDTO<TaiKhoan> val = new ValueDTO<TaiKhoan>();
            DataTableSQL dtSql = await ExecuteQuery("DangNhapPH",
                new SqlParameter("@TaiKhoan", SqlDbType.VarChar) { Value = NameUser },
                new SqlParameter("@MatKhau", SqlDbType.VarChar) { Value = PasswordUser });
            if (dtSql.Error == 0)
            {
                foreach (DataRow dr in dtSql.Data.Rows)
                {
                    val.ListT.Add(new TaiKhoan(dr));
                }
            }
            else
            {
                val.Error = dtSql.Error;
            }

            return val;
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
