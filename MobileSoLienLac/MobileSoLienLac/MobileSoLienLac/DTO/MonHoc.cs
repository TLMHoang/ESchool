using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using MobileSoLienLac.Models.SQL;

namespace MobileSoLienLac.DTO
{
    public class MonHoc : Helper
    {
        public int ID { get; set; }
        public string TenMon { get; set; }
        public byte LoaiDiem { get; set; }

        public MonHoc() { }

        public MonHoc(int iD, string tenMon, byte loaiDiem)
        {
            ID = iD;
            TenMon = tenMon;
            LoaiDiem = loaiDiem;
        }

        public MonHoc(DataRow dr)
        {
            ID = Convert.IsDBNull(dr["ID"]) ? -1 : Convert.ToInt32(dr["ID"]);
            TenMon = dr["TenMon"].ToString();
            LoaiDiem = Convert.ToByte(dr["LoaiDiem"]);
        }

        public async Task<ValueDTO<MonHoc>> GetData()
        {
            DataTableSQL dtSql = await ExecuteQuery("SelectMonhocv2");
            ValueDTO<MonHoc> val = new ValueDTO<MonHoc>();
            if (dtSql.Error == 0)
            {
                foreach (DataRow dr in dtSql.Data.Rows)
                {
                    val.ListT.Add(new MonHoc(dr));
                }
            }
            else
            {
                val.Error = dtSql.Error;
            }

            return val;
        }
    }

    public class SortMonHoc : IComparer<MonHoc>
    {
        public int Compare(MonHoc x, MonHoc y)
        {
            return x.ID.CompareTo(y.ID);
        }
    }
}
