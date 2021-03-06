﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using MobileSoLienLac.Models.SQL;

namespace MobileSoLienLac.DTO
{// Có thể xóa
    public class LoaiThongBao : Helper
    {
        public int ID { get; set; }
        public string TenThongBao { get; set; }

        public LoaiThongBao() { ID = -1; TenThongBao = ""; }

        public LoaiThongBao(int iD, string tenLoaiThongBao)
        {
            ID = iD;
            TenThongBao = tenLoaiThongBao;

        }

        public LoaiThongBao(DataRow dr)
        {
            ID = Convert.IsDBNull(dr["ID"]) ? -1 : Convert.ToInt32(dr["ID"]);
            TenThongBao = dr["TenThongBao"].ToString();
        }

        #region Handle with Database

        //public async Task<ValueDTO<LoaiThongBao>> GetData()
        //{
        //    ValueDTO<LoaiThongBao> val = new ValueDTO<LoaiThongBao>();
        //    DataTableSQL dtSql = await ExecuteQuery("SelectLoaiThongBao",
        //        new SqlParameter("@ID", SqlDbType.Int) { Value = -1 });
        //    if (dtSql.Error == null)
        //    {
                
        //    }
        //    return val;
        //}

        //public List<LoaiThongBao> GetData(DataTable dt)
        //{
        //    List<LoaiThongBao> lst = new List<LoaiThongBao>();
        //    foreach (DataRow dr in dt.Rows)
        //    {
        //        lst.Add(new LoaiThongBao(dr));
        //    }

        //    return lst;
        //}

        #endregion
    }
}
