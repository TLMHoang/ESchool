﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using MobileSoLienLac.Models.SQL;

namespace MobileSoLienLac.DTO
{
    public class DiemDanh : Helper
    {
        public int ID { get; set; }
        public int IDHocSinh { get; set; }
        public DateTime NgayNghi { get; set; }
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
            IDHocSinh = Convert.IsDBNull(dr["IDHocSinh"]) ? -1 : Convert.ToInt32(dr["IDHocSinh"]);
            NgayNghi = Convert.ToDateTime(dr["NgayNghi"]);
            Phep = Convert.ToByte(dr["Phep"]);
        }


        public async Task<ObjectSQL<int>> GetAbsent(int IDStudent, int Month, byte Phep)
        {
            DateTime date = DateTime.Now;
            if (date.Month < 8)
            {
                date = new DateTime(date.Year, Month, 1);
            }
            else
            {
                date = new DateTime(date.Year - 1, Month, 1);
            }

            return await ExecuteScalar<int>("GetNgayNghi",
                new SqlParameter("@IDHocSinh", SqlDbType.Int) {Value = IDStudent},
                new SqlParameter("@StartDate", SqlDbType.Date) {Value = date},
                new SqlParameter("@EndDate", SqlDbType.Date) {Value = new DateTime(date.Year, Month + 1, 1)},
                new SqlParameter("@Phep", SqlDbType.Bit) {Value = Phep}
            );
        }

        public async Task<ValueDTO<DiemDanh>> GetData(int IDHocSinh)
        {
            ValueDTO<DiemDanh> val = new ValueDTO<DiemDanh>();
            DataTableSQL dtSql = await ExecuteQuery("SelectChiTiepDiemDanh",
                new SqlParameter("@IDHocSinh", SqlDbType.Int) {Value = IDHocSinh});

            if (dtSql.Error == 0)
            {
                foreach (DataRow dr in dtSql.Data.Rows)
                {
                    val.ListT.Add(new DiemDanh(dr));
                }
            }
            else
            {
                val.Error = dtSql.Error;
            }

            return val;
        }
    }

    public class SortDiemDanh : IComparer<DiemDanh>
    {
        public int Compare(DiemDanh x, DiemDanh y)
        {
            return x.NgayNghi.CompareTo(y.NgayNghi);
        }
    }
}
