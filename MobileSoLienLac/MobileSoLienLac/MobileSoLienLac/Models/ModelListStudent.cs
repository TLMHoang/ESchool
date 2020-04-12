using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using MobileSoLienLac.DTO;
using MobileSoLienLac.Models.SQL;

namespace MobileSoLienLac.Models
{
    public class ModelListStudent : Helper
    {
        public string Ten { get; set; }
        public DateTime NgaySinh { get; set; }

        public ModelListStudent() { }

        public ModelListStudent(DataRow dr)
        {
            Ten = dr["Ten"].ToString();
            NgaySinh = Convert.ToDateTime(dr["NgaySinh"]);
        }

        #region Handle with Database

        public async Task<ValueDTO<ModelListStudent>> GetData(int IDClass)
        {
            ValueDTO<ModelListStudent> val = new ValueDTO<ModelListStudent>();
            DataTableSQL dtSQL = await ExecuteQuery("SelectHocSinhTrongLop",
                new SqlParameter("@IDLop", SqlDbType.Int) { Value = IDClass });
            if (dtSQL.Error == 0)
            {
                foreach (DataRow dr in dtSQL.Data.Rows)
                {
                    val.ListT.Add(new ModelListStudent(dr));
                }
            }
            else
            {
                val.Error = dtSQL.Error;
            }

            return val;
        }


        #endregion
    }

    #region Sort list

    public class SortListStudent : IComparer<ModelListStudent>
    {
        public int Compare(ModelListStudent x, ModelListStudent y)
        {
            string[] strX = SplitString(x.Ten);
            string[] strY = SplitString(y.Ten);

            int IntReturn = CompareString(strX[1], strY[1]);
            if (IntReturn == 0)
            {
                if (strX[0] == null && strY[0] == null)
                {
                    return x.NgaySinh.CompareTo(y.NgaySinh);
                }
                if (strX[0] == null)
                {
                    return -1;
                }

                if (strY[0] == null)
                {
                    return 1;
                }

                IntReturn = CompareString(strX[0], strY[0]);

                if (IntReturn == 0)
                {
                    return x.NgaySinh.CompareTo(y.NgaySinh);
                }
                else
                {
                    return IntReturn;
                }
            }
            else
            {
                return IntReturn;
            }
        }

        // Tách chuỗi ra là 2 họ và tên
        public string[] SplitString(string Name)
        {
            string[] s = Name.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (s.Length == 1)
            {
                return new[] {null, s[0]};

            }
            return new[] {s[0], s[s.Length - 1]};
        }


        //So Sánh 2 chuỗi theo từng ký tự
        public int CompareString(string x, string y)
        {
            char[] ax = (x.ToLower()).ToCharArray();
            char[] ay = (y.ToLower()).ToCharArray();
            //Kiểm tra xem dộ dài của chuỗi nào dài hơn
            if (ay.Length >= ax.Length)
            {
                //so sánh 2 chuỗi theo từ ký tự trong chuỗi
                for (int i = 0; i < ax.Length; i++)
                {
                    if (ax[i] > ay[i])
                    {
                        return 1;
                    }
                    else if (ax[i] < ay[i])
                    {
                        return -1;
                    }
                }
            }
            else
            {
                //so sánh 2 chuỗi theo từ ký tự trong chuỗi
                for (int i = 0; i < ay.Length; i++)
                {
                    if (ax[i] > ay[i])
                    {
                        return 1;
                    }
                    else if (ax[i] < ay[i])
                    {
                        return -1;
                    }
                }
            }
            //2 cuỗi bằng nhau
            return 0;
        }
    }

    #endregion
}
