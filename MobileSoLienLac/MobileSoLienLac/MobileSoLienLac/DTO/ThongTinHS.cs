using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using MobileSoLienLac.Annotations;
using MobileSoLienLac.Models.SQL;

namespace MobileSoLienLac.DTO
{
    public class ThongTinHS : Helper
    {

        public int ID { get; set; }
        public string Ten { get; set; }
        public DateTime NgaySinh { get; set; }
        public byte GioiTinh { get; set; }
        public string NoiSinh { get; set; }
        public string DanToc { get; set; }
        public string TonGiao { get; set; }
        public int IDKhoi { get; set; }
        public int IDLop { get; set; }
        public string TenLop { get; set; }
        public string TenLoai { get; set; }
        public int IDLoaiHocSinh { get; set; }
        public int HKI { get; set; }
        public int HKII { get; set; }
        public int CaNam { get; set; }
        public int Tien { get; set; }
        public byte DangKy { get; set; }
        public byte BHQD { get; set; }

        public ThongTinHS()
        {
            ID = -1;
            Ten = "";
            NgaySinh = DateTime.Now;
            GioiTinh = 0;
            NoiSinh = "";
            DanToc = "";
            TonGiao = "";
            IDKhoi = -1;
            IDLop = -1;
            TenLop = "";
            TenLoai = "";
            IDLoaiHocSinh = -1;
            HKI = -1;
            HKII = -1;
            CaNam = -1;
            Tien = -1;
            DangKy = 0;
            BHQD = 0;
        }

        public ThongTinHS(int iD, string ten, DateTime ngaySinh, byte gioiTinh, string noiSinh, string danToc, string tonGiao, int idKhoi, int iDLop, string tenLop, string tenLoai, int iDLoaiHocSinh, int hKI, int hKII, int caNam, int tien, byte dangKy, byte bhqd)
        {
            ID = iD;
            Ten = ten;
            NgaySinh = ngaySinh;
            GioiTinh = gioiTinh;
            NoiSinh = noiSinh;
            DanToc = danToc;
            TonGiao = tonGiao;
            IDKhoi = idKhoi;
            IDLop = iDLop;
            TenLop = tenLop;
            TenLoai = tenLoai;
            IDLoaiHocSinh = iDLoaiHocSinh;
            HKI = hKI;
            HKII = hKI;
            CaNam = caNam;
            Tien = tien;
            DangKy = dangKy;
            BHQD = bhqd;
        }

        public ThongTinHS(DataRow dr)
        {
            ID = Convert.IsDBNull(dr["ID"]) ? -1 : Convert.ToInt32(dr["ID"]);
            Ten = dr["Ten"].ToString();
            NgaySinh = Convert.ToDateTime(dr["NgaySinh"]);
            GioiTinh = Convert.ToByte(dr["GioiTinh"]);
            NoiSinh = dr["NoiSinh"].ToString();
            DanToc = dr["DanToc"].ToString();
            TonGiao = dr["TonGiao"].ToString();
            IDKhoi = Convert.IsDBNull(dr["IDKhoi"]) ? -1 : Convert.ToInt32(dr["IDKhoi"]);
            IDLop = Convert.IsDBNull(dr["IDLop"]) ? -1 : Convert.ToInt32(dr["IDLop"]);
            TenLop = dr["TenKhoi"].ToString() + dr["TenLop"].ToString();
            TenLoai = dr["TenLoai"].ToString();
            IDLoaiHocSinh = Convert.IsDBNull(dr["IDLoaiHocSinh"]) ? -1 : Convert.ToInt32(dr["IDLoaiHocSinh"]);
            HKI = Convert.IsDBNull(dr["HKI"]) ? -1 : Convert.ToInt32(dr["HKI"]);
            HKII = Convert.IsDBNull(dr["HKII"]) ? -1 : Convert.ToInt32(dr["HKII"]);
            CaNam = Convert.IsDBNull(dr["CaNam"]) ? -1 : Convert.ToInt32(dr["CaNam"]);
            Tien = Convert.IsDBNull(dr["Tien"]) ? -1 : Convert.ToInt32(dr["Tien"]);
            DangKy = Convert.IsDBNull(dr["DangKy"]) ? Convert.ToByte(0) : Convert.ToByte(dr["DangKy"]);
            BHQD = Convert.IsDBNull(dr["BHQD"]) ? Convert.ToByte(0) : Convert.ToByte(dr["BHQD"]);
        }

        #region Handle value


        public override string ToString() => Ten + " - " + TenLop;

        public string FistName()
        {
            return Ten.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Last();
        }

        #endregion


        #region Handle With Database

        public async Task<ValueDTO<ThongTinHS>> GetDaTa(int IDHocSinh)
        {
            ValueDTO<ThongTinHS> val = new ValueDTO<ThongTinHS>();
            DataTableSQL dtSql = await ExecuteQuery("SelectThongTinHSv2",
                new SqlParameter("@ID", SqlDbType.Int) { Value = IDHocSinh });

            if (dtSql.Error == 0)
            {
                foreach (DataRow dr in dtSql.Data.Rows)
                {
                    val.ListT.Add(new ThongTinHS(dr));
                }
            }
            else
            {
                val.Error = dtSql.Error;
            }

            return val;
        }

        #endregion
    }
}
