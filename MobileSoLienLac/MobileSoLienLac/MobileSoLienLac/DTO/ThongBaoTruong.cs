﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using MobileSoLienLac.Models.SQL;

namespace MobileSoLienLac.DTO
{
    public class ThongBaoTruong : Helper
    {
        public int ID { get; set; }
        public int IDLoaiThongBao { get; set; }
        public string NoiDung { get; set; }
        public DateTime Ngay { get; set; }

        public ThongBaoTruong()
        {
            ID = -1;
            IDLoaiThongBao = -1;
            NoiDung = "";
            Ngay = DateTime.Now;
        }

        public ThongBaoTruong(int iD, int iDLoaiThongBao, string noiDung, DateTime ngay)
        {
            ID = iD;
            IDLoaiThongBao = iDLoaiThongBao;
            NoiDung = noiDung;
            Ngay = ngay;
        }

        public ThongBaoTruong(DataRow dr)
        {
            ID = Convert.IsDBNull(dr["ID"]) ? -1 : Convert.ToInt32(dr["ID"]); ;
            IDLoaiThongBao = Convert.IsDBNull(dr["IDLoaiThongBao"]) ? -1 : Convert.ToInt32(dr["IDLoaiThongBao"]); ;
            NoiDung = dr["NoiDung"].ToString();
            Ngay = Convert.ToDateTime(dr["Ngay"]);
        }

        public async Task<DataTable> GetData()
        {
            return await ExecuteQuery("SelectThongBaoTruong",
                new SqlParameter("@ID", SqlDbType.Int){Value = -1});
        }

        public List<ThongBaoTruong> GetData(DataTable dt)
        {
            List<ThongBaoTruong> lst = new List<ThongBaoTruong>();
            foreach (DataRow dr in dt.Rows)
            {
                lst.Add(new ThongBaoTruong(dr));
            }

            return lst;
        }

        public ThongBaoTruong GetData(DataRow dr)
        {
            return new ThongBaoTruong(dr);
        }

        public async Task<DataTable> GetContent(int ID)
        {
            return await ExecuteQuery("SelectNoiDungThongBaoTruong",
                new SqlParameter("@ID", SqlDbType.Int) { Value =ID });
        }
    }
}