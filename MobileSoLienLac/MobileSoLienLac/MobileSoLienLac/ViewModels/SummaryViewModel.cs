using System;
using System.Collections.Generic;
using System.Text;
using MobileSoLienLac.DTO;
using MobileSoLienLac.Models.SQL;

namespace MobileSoLienLac.ViewModels
{
    public class SummaryViewModel
    {
        public ThongTinHS HK { get; set; }
        public List<DTBMon> LstDTBI { get; set; }
        public List<DTBMon> LstDTBII { get; set; }
        public List<DTBMon> LstDTBCN { get; set; }
        public DTBTong Sum { get; set; }
        public string Error { get; set; }

        public SummaryViewModel()
        {
            GetData();
        }

        public async void GetData()
        {
            HK = App.StudentSeclect;
            Error = "";
            ValueDTO<DTBTong> valT = await new DTBTong().GetData(HK.ID, HK.IDLop);
            if (valT.Error == 0)
            {
                Sum = valT.ListT[0];
            }
            else
            {
                Error = new HandleError().IDErrorToNotify(valT.Error);
                return;
            }

            ValueDTO<DTBMon> valM = await new DTBMon().GetData(HK.ID, HK.IDLop, 1);
            if (valT.Error == 0)
            {
                LstDTBI = valM.ListT;
            }
            else
            {
                Error = new HandleError().IDErrorToNotify(valM.Error);
                return;
            }

            valM = await new DTBMon().GetData(HK.ID, HK.IDLop, 2);
            if (valT.Error == 0)
            {
                LstDTBII = valM.ListT;
            }
            else
            {
                Error = new HandleError().IDErrorToNotify(valM.Error);
                return;
            }

            valM = await new DTBMon().GetData(HK.ID, HK.IDLop, 0);
            if (valT.Error == 0)
            {
                LstDTBCN = valM.ListT;
            }
            else
            {
                Error = new HandleError().IDErrorToNotify(valM.Error);
                return;
            }
        }
    }
}
