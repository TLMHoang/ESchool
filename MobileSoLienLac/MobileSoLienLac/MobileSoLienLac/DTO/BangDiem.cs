using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using MobileSoLienLac.Models.SQL;

namespace MobileSoLienLac.DTO
{
    public class BangDiem : Helper
    {
        public int IDLoaiDiem { get; set; }
        //public string TenLoaiDiem { get; set; }
        public int IDMonHoc { get; set; }
        public double Diem { get; set; }
        public int CotDiem { get; set; }
        public byte HocKyI { get; set; }

        public BangDiem() { }

        public BangDiem(DataRow dr)
        {
            IDLoaiDiem = Convert.IsDBNull(dr["IDLoaiDiem"]) ? -1 : Convert.ToInt32(dr["IDLoaiDiem"]);
            IDMonHoc = Convert.IsDBNull(dr["IDMonHoc"]) ? -1 : Convert.ToInt32(dr["IDMonHoc"]);
            //TenLoaiDiem = dr["TenLoaiDiem"].ToString();
            Diem = Convert.ToDouble(dr["Diem"]);
            CotDiem = Convert.IsDBNull(dr["CotDiem"]) ? -1 : Convert.ToInt32(dr["CotDiem"]);
            HocKyI = Convert.ToByte(dr["HocKyI"]);
        }

        public BangDiem(int iDLoaiDiem, string tenLoaiDiem, byte he, int iDMonHoc, double diem, int cotDiem, byte hocKyI)
        {
            IDLoaiDiem = iDLoaiDiem;
            //TenLoaiDiem = tenLoaiDiem;
            IDMonHoc = iDMonHoc;
            Diem = diem;
            CotDiem = cotDiem;
            HocKyI = hocKyI;
        }

        public async Task<ValueDTO<BangDiem>> GetData(int IDHocSinh)
        {
            ValueDTO<BangDiem> val = new ValueDTO<BangDiem>();
            DataTableSQL dtSql = await ExecuteQuery("SelectBangDiemv2",
                new SqlParameter("@IDHocSinh", SqlDbType.Int) { Value = IDHocSinh });

            if (dtSql.Error == 0)
            {
                foreach (DataRow dr in dtSql.Data.Rows)
                {
                    val.ListT.Add(new BangDiem(dr));
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
