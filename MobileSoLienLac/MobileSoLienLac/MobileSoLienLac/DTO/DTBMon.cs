using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using MobileSoLienLac.Models.SQL;

namespace MobileSoLienLac.DTO
{
    public class DTBMon : Helper
    {
        public string TenMon { get; set; }
        public Decimal Diem { get; set; }


        public DTBMon()
        {
        }

        public DTBMon(DataRow dr)
        {
            TenMon = dr["TenMon"].ToString();
            Diem = Math.Round(Convert.ToDecimal(dr["Diem"]), 2);
        }

        public async Task<ValueDTO<DTBMon>> GetData(int IDStudent, int IDClass, int HK)
        {
            ValueDTO<DTBMon> val = new ValueDTO<DTBMon>();
            DataTableSQL dtSql = await ExecuteQuery("M_LayDTBMon", 
                new SqlParameter("@IDHocSinh", SqlDbType.Int) { Value = IDStudent },
                new SqlParameter("@IDLop", SqlDbType.Int) { Value = IDClass },
                new SqlParameter("@HocKy", SqlDbType.Int) { Value = HK }
            );

            if (dtSql.Error == 0)
            {
                foreach (DataRow dr in dtSql.Data.Rows)
                {
                    val.ListT.Add(new DTBMon(dr));
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
