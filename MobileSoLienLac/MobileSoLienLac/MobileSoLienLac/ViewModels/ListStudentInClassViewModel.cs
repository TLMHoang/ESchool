using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using MobileSoLienLac.Models;
using MobileSoLienLac.Models.SQL;

namespace MobileSoLienLac.ViewModels
{
    public class ListStudentInClassViewModel
    {
        public List<ModelListStudent> ListStudents { get; set; }
        public string Message { get; set; }

        public ListStudentInClassViewModel()
        {
            ListStudents = new List<ModelListStudent>();
            Message = "";
        }

        public async Task<ListStudentInClassViewModel> GetData(int IDClass)
        {
            ModelListStudent val = new ModelListStudent();
            ListStudentInClassViewModel _value = new ListStudentInClassViewModel();
            DataTable dt = await val.GetData(IDClass);
            if (dt.Columns.Count == 1)
            {
                _value.Message = new HandleError().IDErrorToNotify(Convert.ToInt32(dt.Rows[0]["Error"]));
                return _value;
            }
            else
            {
                _value.ListStudents = val.GetData(dt);
                return _value;
            }
        }
    }
}
