using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using MobileSoLienLac.Models.SQL;

namespace MobileSoLienLac.DTO
{
    public class DiemDanh : Helper
    {
        public int ID { get; set; }
        public int IDHocSinh { get; set; }
        public DateTime? NgayNghi { get; set; }
        public byte Phep { get; set; }

        public DiemDanh()
        {
            ID = -1;
            IDHocSinh = -1;
            NgayNghi = DateTime.Now;
            Phep = 1;
        }

        public DiemDanh(int iD, int idHocSinh, DateTime ngayNghi, byte phep)
        {
            ID = -iD;
            IDHocSinh = idHocSinh;
            NgayNghi = ngayNghi;
            Phep = phep;
        }

        public DiemDanh(DataRow dr)
        {
            ID = Convert.IsDBNull(dr["ID"]) ? -1 : Convert.ToInt32(dr["ID"]);
            IDHocSinh = Convert.IsDBNull(dr["IDHocSinh"]) ? -1 : Convert.ToInt32(dr["IDHocSinh"]); ;
            NgayNghi = Convert.ToDateTime(dr["NgayNghi"]);
            Phep = Convert.ToByte(dr["Phep"]);
        }


        public async Task<int> GetAbsent(int IDStudent, int Month)
        {

        }
    }
}
