using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using MobileSoLienLac.Models.SQL;

namespace MobileSoLienLac.DTO
{
    public class XinPhep : Helper
    {
        public int ID { get; set; }
        public int IDHocSinh { get; set; }
        public DateTime NghiTu { get; set; }
        public DateTime NghiDen { get; set; }
        public byte TrangThai { get; set; }
        public byte ChoHuy { get; set; }
        public string LyDo { get; set; }

        public XinPhep()
        {
            ID = -1;
            IDHocSinh = -1;
            NghiTu = DateTime.Now;
            NghiDen = DateTime.Now;
            TrangThai = 0;
            ChoHuy = 0;
            LyDo = "";
        }

        public XinPhep(int iD, int iDHocSinh, DateTime nghiTu, DateTime nghiDen, byte trangThai, byte choHuy, string lyDo)
        {
            ID = iD;
            IDHocSinh = iDHocSinh;
            NghiTu = nghiTu;
            NghiDen = nghiDen;
            TrangThai = trangThai;
            ChoHuy = choHuy;
            LyDo = lyDo;
        }

        public XinPhep(DataRow dr)
        {
            ID = Convert.IsDBNull(dr["ID"]) ? -1 : Convert.ToInt32(dr["ID"]);
            IDHocSinh = Convert.IsDBNull(dr["IDHocSinh"]) ? -1 : Convert.ToInt32(dr["IDHocSinh"]);
            NghiTu = Convert.ToDateTime(dr["NghiTu"]);
            NghiDen = Convert.ToDateTime(dr["NghiDen"]);
            TrangThai = Convert.ToByte(dr["TrangThai"]);
            ChoHuy = Convert.ToByte(dr["ChoHuy"]);
            LyDo = dr["LyDo"].ToString();
        }

        #region Handle with Database

        public async Task<int> SetData(int IDStudent, DateTime From, DateTime To, string ReSion)
        {
            return await ExecuteNonQuery("InsertXinPhepV2",
                new SqlParameter("@IDHocSinh", SqlDbType.Int) {Value = IDStudent},
                new SqlParameter("@NghiTu", SqlDbType.Date) {Value = From},
                new SqlParameter("@NghiDen", SqlDbType.Date) {Value = To},
                new SqlParameter("@LyDo", SqlDbType.NVarChar) {Value = ReSion});
        }

        public async Task<int> UpdateData(int ID, DateTime From, DateTime To, string ReSion)
        {
            return await ExecuteNonQuery("UpdateXinPhepV2",
                new SqlParameter("@ID", SqlDbType.Int) { Value = ID},
                new SqlParameter("@NghiTu", SqlDbType.Date) { Value = From },
                new SqlParameter("@NghiDen", SqlDbType.Date) { Value = To },
                new SqlParameter("@LyDo", SqlDbType.NVarChar) { Value = ReSion });
        }

        public async Task<int> DeleteData(int ID)
        {
            return await ExecuteNonQuery("HuyXinPhep",
                new SqlParameter("@ID", SqlDbType.Int) { Value = ID });
        }

        public async Task<ValueDTO<XinPhep>> GetData(int IDStudent)
        {
            ValueDTO<XinPhep> val = new ValueDTO<XinPhep>();
            DataTableSQL dtSql = await ExecuteQuery("SelectXinPhepTheoIDHS",
                new SqlParameter("@IDHocSinh", SqlDbType.Int) {Value = IDStudent});

            if (dtSql.Error == 0)
            {
                foreach (DataRow dr in dtSql.Data.Rows)
                {
                    val.ListT.Add(new XinPhep(dr));
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

    public class SortXinPhep : IComparer<XinPhep>
    {
        public int Compare(XinPhep x, XinPhep y)
        {
            if (x.NghiTu.CompareTo(y.NghiTu) == 0)
            {
                return x.TrangThai.CompareTo(y.TrangThai);
            }
            else
            {
                return x.NghiTu.CompareTo(y.NghiTu);
            }
        }
    }
}
