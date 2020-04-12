using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using MobileSoLienLac.DTO;
using MobileSoLienLac.Models;
using MobileSoLienLac.Models.SQL;

namespace MobileSoLienLac.ViewModels
{
    public class NotifyViewModel
    {
        public int ID { get; set; }
        public string TenThongBao { get; set; }
        public DateTime Ngay { get; set; }


        public NotifyViewModel() { }
        public NotifyViewModel(int iD, string tenThongBao, DateTime ngay)
        {
            ID = iD;
            TenThongBao = tenThongBao;
            Ngay = ngay;
        }

        public NotifyModel val = new NotifyModel();
        public async Task<ValueDTO<NotifyModel>> NotifySchool()
        {
            return await val.GetDataSchool();
        }

        public async Task<ValueDTO<NotifyModel>> NotifyClass()
        {
            return await val.GetDataClass(App.StudentSeclect.IDLop);
        }

        public async Task<ValueDTO<NotifyModel>> NotifyStudent()
        {
            return await val.GetDataStudent(App.StudentSeclect.ID);
        }
    }
}
