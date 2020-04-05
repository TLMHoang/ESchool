using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using MobileSoLienLac.Models.SQL;

namespace MobileSoLienLac.Models
{
    public class ModelListTeacher : Helper
    {
        public string TenGV { get; set; }
        public string TenMon { get; set; }
        public string SDT { get; set; }
        public int IDLop { get; set; }

        public ModelListTeacher()
        {
            TenGV = "";
            TenMon = "";
            SDT = "";
            IDLop = -1;
        }

        public ModelListTeacher(string tenGV, string tenMon, string sDT, int iDLop)
        {
            TenGV = tenGV;
            TenMon = tenMon;
            SDT = sDT;
            IDLop = iDLop;
        }

        public ModelListTeacher(DataRow dr)
        {
            TenGV = dr["TenGV"].ToString();
            TenMon = dr["TenMon"].ToString();
            SDT = dr["SDT"].ToString();
            IDLop = Convert.IsDBNull(dr["IDLop"])? -1: Convert.ToInt32(dr["IDLop"]);
        }

        #region Handle wwith database

        public async Task<DataTable> GetData(int IDLop)
        {
            return await ExecuteQuery("SelectThongTinGV",
                new SqlParameter("@IDLop", SqlDbType.Int) {Value = IDLop}
            );
        }

        public List<ModelListTeacher> GetData(DataTable dt)
        {
            List<ModelListTeacher> lst = new List<ModelListTeacher>();
            foreach (DataRow dr in dt.Rows)
            {
                lst.Add(new ModelListTeacher(dr));
            }

            return lst;
        }

        #endregion
    }
}
