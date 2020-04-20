using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileSoLienLac.DTO;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace MobileSoLienLac.Views.Student
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TimeTablePage : ContentPage
    {
        private List<List<ThoiKhoaBieu>> lst = new List<List<ThoiKhoaBieu>>();

        public TimeTablePage()
        {
            InitializeComponent();
            GetList();
            if (lst.Count == 0)
            {
                scvTimeTable.IsVisible = false;
            }
            else
            {
                lblNotify.IsVisible = false;
                FillData();
            }
        }

        public async void GetList()
        {
            ValueDTO<ThoiKhoaBieu> val = await new ThoiKhoaBieu().GetData(App.StudentSeclect.IDLop);

            if (val.Error == 0)
            {
                for (int i = 0; i < 7; i++)
                {
                    lst.Add(new List<ThoiKhoaBieu>());
                }

                foreach (ThoiKhoaBieu Item in val.ListT)
                {
                    lst[Item.Thu - 1].Add(Item);
                }
            }
        }

        public void FillData()
        {
            lvThu.ItemsSource = lst[0];
            lvThu2.ItemsSource = lst[1];
            lvThu3.ItemsSource = lst[2];
            lvThu4.ItemsSource = lst[3];
            lvThu5.ItemsSource = lst[4];
            lvThu6.ItemsSource = lst[5];
            lvThu7.ItemsSource = lst[6];

        }

        private void ListView_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            ListView lv = (ListView)sender;
            lv.IsVisible = false;
        }

        #region Event show list one day of Week

        public void HidenAll()
        {
            lvThu.IsVisible = false;
            lvThu2.IsVisible = false;
            lvThu3.IsVisible = false;
            lvThu4.IsVisible = false;
            lvThu5.IsVisible = false;
            lvThu6.IsVisible = false;
            lvThu7.IsVisible = false;
        }

        private void FrmThu2_OnTapped(object sender, EventArgs e)
        {
            if (!lvThu2.IsVisible)
            {
                imgThu2.Source = "less.png";
                HidenAll();
            }
            else
            {
                imgThu2.Source = "more.png";
            }
            lvThu2.IsVisible = !lvThu2.IsVisible;
        }

        private void FrmThu3_OnTapped(object sender, EventArgs e)
        {
            if (!lvThu3.IsVisible)
            {
                imgThu3.Source = "less.png";
                HidenAll();
            }
            else
            {
                imgThu3.Source = "more.png";
            }
            lvThu3.IsVisible = !lvThu3.IsVisible;
        }

        private void FrmThu4_OnTapped(object sender, EventArgs e)
        {
            if (!lvThu4.IsVisible)
            {
                imgThu4.Source = "less.png";
                HidenAll();
            }
            else
            {
                imgThu4.Source = "more.png";
            }
            lvThu4.IsVisible = !lvThu4.IsVisible;
        }

        private void FrmThu5_OnTapped(object sender, EventArgs e)
        {
            if (!lvThu5.IsVisible)
            {
                imgThu5.Source = "less.png";
                HidenAll();
            }
            else
            {
                imgThu5.Source = "more.png";
            }
            lvThu5.IsVisible = !lvThu5.IsVisible;
        }

        private void FrmThu6_OnTapped(object sender, EventArgs e)
        {
            if (!lvThu6.IsVisible)
            {
                imgThu6.Source = "less.png";
                HidenAll();
            }
            else
            {
                imgThu6.Source = "more.png";
            }
            lvThu6.IsVisible = !lvThu6.IsVisible;
        }

        private void FrmThu7_OnTapped(object sender, EventArgs e)
        {
            if (!lvThu7.IsVisible)
            {
                imgThu7.Source = "less.png";
                HidenAll();
            }
            else
            {
                imgThu7.Source = "more.png";
            }
            lvThu7.IsVisible = !lvThu7.IsVisible;
        }

        private void FrmThu_OnTapped(object sender, EventArgs e)
        {
            if (!lvThu.IsVisible)
            {
                imgThu.Source = "less.png";
                HidenAll();
            }
            else
            {
                imgThu.Source = "more.png";
            }
            lvThu.IsVisible = !lvThu.IsVisible;
        }

        #endregion
    }
}