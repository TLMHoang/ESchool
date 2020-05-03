using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using MobileSoLienLac.Models.SQL;

namespace MobileSoLienLac.DTO
{
    public class FunctionOrder : Helper
    {
        public int ID { get; set; }
        public string NameFunc { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public FunctionOrder() { }

        public FunctionOrder(int id, string nameFunc, DateTime startDate, DateTime endDate)
        {
            ID = id;
            NameFunc = nameFunc;
            StartDate = startDate;
            EndDate = endDate;
        }

        public FunctionOrder(DataRow dr)
        {
            ID = Convert.IsDBNull(dr["ID"]) ? -1 : Convert.ToInt32(dr["ID"]);
            NameFunc = dr["NameFunc"].ToString();
            StartDate = Convert.ToDateTime(dr["StartDate"]);
            EndDate = Convert.ToDateTime(dr["EndDate"]);
        }


        public async Task<ValueDTO<FunctionOrder>> GetData()
        {
            DataTableSQL dtSql = await ExecuteQuery("SelectFunctionOrder");
            ValueDTO<FunctionOrder> val = new ValueDTO<FunctionOrder>();

            if (dtSql.Error == 0)
            {
                foreach (DataRow dr in dtSql.Data.Rows)
                {
                    val.ListT.Add(new FunctionOrder(dr));
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
