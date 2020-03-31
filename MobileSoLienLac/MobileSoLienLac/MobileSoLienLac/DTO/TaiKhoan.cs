using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using MobileSoLienLac.Models.SQL;

namespace MobileSoLienLac.DTO
{
    public class TaiKhoan
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string NameMom { get; set; }
        public string PhoneMom { get; set; }
        public string NameDad { get; set; }
        public string PhoneDad { get; set; }

        public TaiKhoan()
        {
            ID = -1;
            UserName = "";
            Password = "";
            NameMom = "";
            PhoneMom = "";
            NameDad = "";
            PhoneDad = "";
        }
        public TaiKhoan(int iD, string userName, string password, string nameMom, string phoneMom, string nameDad, string phoneDad)
        {
            ID = iD;
            UserName = userName;
            Password = password;
            NameMom = nameMom;
            PhoneMom = phoneMom;
            NameDad = nameDad;
            PhoneDad = phoneDad;
        }
        public TaiKhoan(DataRow dr)
        {
            ID = Convert.IsDBNull(dr["ID"]) ? -1 : Convert.ToInt32(dr["ID"]);
            UserName = dr["TaiKhoan"].ToString();
            Password = dr["MatKhau"].ToString();
            NameMom = dr["TenMe"].ToString();
            PhoneMom = dr["SDTMe"].ToString();
            NameDad = dr["TenBo"].ToString();
            PhoneDad = dr["SDTBo"].ToString();
        }

        public async Task<int> ChangePassword(int ID, string OldPassword, string NewPassword)
        {
            return await new Helper().ExecuteNonQuery("DoiMatKhauPH",
                new SqlParameter("@ID", SqlDbType.Int) { Value = ID},
                new SqlParameter("@MatKhauCu", SqlDbType.VarChar) { Value = OldPassword},
                new SqlParameter("@MatKhauMoi", SqlDbType.VarChar) { Value = NewPassword}
                );
        }
    }
}
