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
        

        public async Task<bool> CheckLogin(string NameUser, string PasswordUser)
        {
            DataTable dt = await new ModelLogin().PostUserToDatabase(NameUser, PasswordUser);

            if (dt.Rows.Count == 0)
            {
                return false;
            }
            else
            {
                #region Set data
                int ID = Convert.IsDBNull(dt.Rows[0]["ID"]) ? -1 : Convert.ToInt32(dt.Rows[0]["ID"]);
                App.lstPHvsHs = await new LienKetPHvsHS().GetListLienKet(ID);
                App.IDAccount = ID;
                foreach (LienKetPHvsHS i in App.lstPHvsHs)
                {
                    App.lstStudents.Add((await new ThongTinHS().GetInforStudent(i.IDHocSinh)));
                }

                #endregion
                return true;
            }
        }
    }
}