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
    public class ModelListStudent : Helper
    {
        public string Ten { get; set; }
        public DateTime NgaySinh { get; set; }

        public ModelListStudent() { }

        public ModelListStudent(DataRow dr)
        {
            Ten = dr["Ten"].ToString();
            NgaySinh = Convert.ToDateTime(dr["NgaySinh"]);
        }

        #region Handle with Database

        public async Task<DataTable> GetData(int IDClass)
        {
            return await ExecuteQuery("SelectHocSinhTrongLop",
                new SqlParameter("@IDLop", SqlDbType.Int) { Value = IDClass });
        }

        public List<ModelListStudent> GetData(DataTable dt)
        {
            List<ModelListStudent> lst = new List<ModelListStudent>();
            foreach (DataRow dr in dt.Rows)
            {
                lst.Add(new ModelListStudent(dr));
            }
            return lst;
        }

        #endregion
    }
}
