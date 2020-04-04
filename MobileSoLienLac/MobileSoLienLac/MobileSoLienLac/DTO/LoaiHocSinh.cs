using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using MobileSoLienLac.Models.SQL;

namespace MobileSoLienLac.DTO
{
    public class LoaiHocSinh : Helper
    {
        public int ID { get; set; }
        public string TenLoai { get; set; }

        public LoaiHocSinh() { ID = -1; TenLoai = ""; }

        public LoaiHocSinh(int iD, string TenLoai)
        {
            ID = iD;
            TenLoai = TenLoai;

        }

        public LoaiHocSinh(DataRow dr)
        {
            ID = Convert.IsDBNull(dr["ID"]) ? -1 : Convert.ToInt32(dr["ID"]);
            TenLoai = dr["TenLoai"].ToString();
        }

        #region Handle with Database

        public async Task<DataTable> GetData()
        {

            return await ExecuteQuery("SelectLoaiHocSinh",
                new SqlParameter("@ID", SqlDbType.Int) { Value = -1 });
        }

        public List<LoaiHocSinh> GetData(DataTable dt)
        {
            List<LoaiHocSinh> lst = new List<LoaiHocSinh>();
            foreach (DataRow dr in dt.Rows)
            {
                lst.Add(new LoaiHocSinh(dr));
            }

            return lst;
        }

        #endregion

    }
}
