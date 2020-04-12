using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MobileSoLienLac.DTO;
using MobileSoLienLac.Models;

namespace MobileSoLienLac.ViewModels
{
    public class ListTeacherViewModel
    {
        public async Task<ValueDTO<ModelListTeacher>> GetData(int IDClass)
        {
            return await new ModelListTeacher().GetData(IDClass);
        }
    }
}
