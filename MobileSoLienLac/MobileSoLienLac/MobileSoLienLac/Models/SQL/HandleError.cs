using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Android.Widget;

namespace MobileSoLienLac.Models.SQL
{
    public class HandleError
    {
        public int StringErrorToIDError(string strError)
        {

            if (strError.Contains("Snix_Connect"))
            {
                return -40;
            }
            //Error Could not read data
            if (strError.Contains("Snix_Read"))
            {
                return -35;
            }
            //Error Could not find stored procedure
            if (strError.Contains("Could not find stored procedure"))
            {
                return -45;
            }

            if (strError.Contains("Login failed for user"))
            {
                return -50;
            }

            return -1000;
        }

        public string IDErrorToNotify(int IError)
        {
            switch (IError)
            {
                case -40:
                    return "Không thê kết nối tới máy chủ kiểm tra lại mạng.";
                case -35:
                    return "Không thê kết nối tới máy chủ kiểm tra lại mạng.";
                case -45:
                case -50:
                    return "Có thể hệ thống đang bảo tri hoặc cập nhập. Kiểm tra bản cập nhập nếu không có báo với USteam để kiểm tra lại";
                default:
                    return "Lỗi không cụ thể.";
            }
        }

        //public string StringErrorToNotify(DataRow dr)
        //{
        //    if (dr["Error"].ToString().Contains("Snix_Connect"))
        //    {
        //        return "Không thê kết nối tới máy chủ kiểm tra lại mạng.";
        //    }
        //    if (dr["Error"].ToString().Contains("Snix_Read"))
        //    {
        //        return "Không thê kết nối tới máy chủ kiểm tra lại mạng.";
        //    }

        //    return "Lỗi";
        //}
    }
}
