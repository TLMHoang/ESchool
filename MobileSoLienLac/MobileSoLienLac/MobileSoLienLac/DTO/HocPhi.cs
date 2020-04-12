using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using MobileSoLienLac.Models.SQL;

namespace MobileSoLienLac.DTO
{
    public class HocPhi : Helper
    {
        public int SoNgayHoc { get; set; }
        public int TienAn { get; set; }
        public int TongAn { get; set; }
        public int TienDien { get; set; }
        public int TienHoc { get; set; }
        public int TienNuoc { get; set; }
        public int TienTaiLieu { get; set; }
        public int TienTrangThietBi { get; set; }
        public int TienVeSinh { get; set; }
        public int TongTien { get; set; }

        public HocPhi()
        {
            SoNgayHoc = -1;
            TienAn = -1;
            TongAn = -1;
            TienDien = -1;
            TienHoc = -1;
            TienNuoc = -1;
            TienTaiLieu = -1;
            TienTrangThietBi = -1;
            TienVeSinh = -1;
            TongTien = -1;
        }

        public HocPhi(DataRow dr, int NgayNghi)
        {
            SoNgayHoc = Convert.IsDBNull(dr["SoNgayHoc"]) ? -1 : Convert.ToInt32(dr["SoNgayHoc"]);
            TienAn = Convert.IsDBNull(dr["TienAn"]) ? -1 : Convert.ToInt32(dr["TienAn"]);
            TongAn = TienAn * (SoNgayHoc - NgayNghi);
            TienDien = Convert.IsDBNull(dr["TienDien"]) ? -1 : Convert.ToInt32(dr["TienDien"]);
            TienHoc = Convert.IsDBNull(dr["TienHoc"]) ? -1 : Convert.ToInt32(dr["TienHoc"]);
            TienNuoc = Convert.IsDBNull(dr["TienNuoc"]) ? -1 : Convert.ToInt32(dr["TienNuoc"]);
            TienTaiLieu = Convert.IsDBNull(dr["TienTaiLieu"]) ? -1 : Convert.ToInt32(dr["TienTaiLieu"]);
            TienTrangThietBi = Convert.IsDBNull(dr["TienTrangThietBi"]) ? -1 : Convert.ToInt32(dr["TienTrangThietBi"]);
            TienVeSinh = Convert.IsDBNull(dr["TienVeSinh"]) ? -1 : Convert.ToInt32(dr["TienVeSinh"]);
            TongTien = TongAn + TienDien + TienHoc + TienNuoc + TienTaiLieu + TienTrangThietBi + TienVeSinh;
        }

        public HocPhi(int soNgayHoc, int tienAn, int tongAn, int tienDien, int tienHoc, int tienNuoc, int tienTaiLieu, int tienTrangThietBi, int tienVeSinh, int tongTien)
        {
            SoNgayHoc = soNgayHoc;
            TienAn = tienAn;
            TongAn = tongAn;
            TienDien = tienDien;
            TienHoc = tienHoc;
            TienNuoc = tienNuoc;
            TienTaiLieu = tienTaiLieu;
            TienTrangThietBi = tienTrangThietBi;
            TienVeSinh = tienVeSinh;
            TongTien = tongTien;
        }

        #region Handle with Database

        public async Task<ValueDTO<HocPhi>> GetData(int Thang, int IDLoaiHocSinh, int IDKhoi, int NgayNghi)
        {
            ValueDTO<HocPhi> val = new ValueDTO<HocPhi>();
            DataTableSQL dtSql = await ExecuteQuery("SelectChiTietTienHoc",
                new SqlParameter("@Thang", SqlDbType.Int) {Value = Thang},
                new SqlParameter("@IDLoaiHocSinh", SqlDbType.Int) {Value = IDLoaiHocSinh},
                new SqlParameter("@IDKhoi", SqlDbType.Int) {Value = IDKhoi});

            if (dtSql.Error == 0)
            {
                foreach (DataRow dr in dtSql.Data.Rows)
                {
                    val.ListT.Add(new HocPhi(dr, NgayNghi));
                }
            }

            return val;
        }

        #endregion
    }
}
