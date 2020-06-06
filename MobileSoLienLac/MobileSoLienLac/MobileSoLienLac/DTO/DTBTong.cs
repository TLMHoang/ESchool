using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using MobileSoLienLac.Models.SQL;

namespace MobileSoLienLac.DTO
{
    public class DTBTong : Helper
    {
        public decimal HKI { get; set; }
        public decimal HKII { get; set; }
        public decimal CaNam { get; set; }


        public DTBTong() {}

        public DTBTong(DataRow dr)
        {
            HKI = Math.Round(Convert.ToDecimal(dr["HKI"]), 2);
            CaNam = Math.Round(Convert.ToDecimal(dr["CaNam"]), 2);
            HKII = Math.Round(Convert.ToDecimal(dr["HKII"]), 2);
        }


        public async Task<ValueDTO<DTBTong>> GetData(int IDStudent, int IDClass)
        {
            ValueDTO<DTBTong> val = new ValueDTO<DTBTong>();
            DataTableSQL dtSql = await ExecuteQuery("M_LayDTB",
                new SqlParameter("@IDHocSinh", SqlDbType.Int) { Value = IDStudent },
                new SqlParameter("@IDLop", SqlDbType.Int) { Value = IDClass });

            if (dtSql.Error == 0)
            {
                foreach (DataRow dr in dtSql.Data.Rows)
                {
                    val.ListT.Add(new DTBTong(dr));
                }
            }
            else
            {
                val.Error = dtSql.Error;
            }


            return val;
        }
    }
}
