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
    public class ListStudentInClassViewModel
    {
        public async Task<ValueDTO<ModelListStudent>> GetData(int IDClass)
        {
            return await new ModelListStudent().GetData(IDClass);
        }
    }
}
