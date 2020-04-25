using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MobileSoLienLac.DTO
{
    public class BHYT
    {
        public int IDHocSinh { get; set; }
        public byte DangKy { get; set; }
        public byte BHQD { get; set; }

        public BHYT(DataRow dr)
        {
            IDHocSinh = Convert.IsDBNull(dr["IDHocSinh"]) ? -1 : Convert.ToInt32(dr["IDHocSinh"]);
            DangKy = Convert.ToByte("DangKy");
            BHQD = Convert.ToByte("BHQD");
        }
    }
}
