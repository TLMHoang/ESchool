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
        public async Task<string> CheckLogin(string NameUser, string PasswordUser)
        {
            DataTable dt = await new ModelLogin().PostUserToDatabase(NameUser, PasswordUser);

            if (dt.Columns.Count != 1)
            {
                if (dt.Rows.Count != 0)
                {
                    App.IDAccount = Convert.IsDBNull(dt.Rows[0]["ID"]) ? -1 : Convert.ToInt32(dt.Rows[0]["ID"]);
                    return "";
                }
                else
                {
                    
                    return "N";
                }
            }
            else
            {
                return new HandleError().IDErrorToNotify(Convert.ToInt32(dt.Rows[0]["Error"]));
            }
        }
    }
}