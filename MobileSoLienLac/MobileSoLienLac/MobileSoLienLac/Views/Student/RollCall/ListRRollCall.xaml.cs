using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileSoLienLac.DTO;
using MobileSoLienLac.Models.SQL;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileSoLienLac.Views.Student.RollCall
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListRRollCall : ContentPage
    {
        public class ItemPicker
        {
            public int ID { get; set; }
            public string Name { get; set; }

            public ItemPicker(int iD, string name)
            {
                ID = iD;
                Name = name;
            }

            public override string ToString() => Name;
        }

        public List<XinPhep> lst = new List<XinPhep>();
        XinPhep ItemSelect = new XinPhep();
        public ListRRollCall()
        {
            InitializeComponent();
            GetData(App.StudentSeclect.ID);

            #region Tao MenuStrip bằng picker

            // tạo item 
            PickerChooseTrue.ItemsSource = new List<ItemPicker>()
            {
                new ItemPicker(1, "Xem chi tiết"),
                new ItemPicker(2, "Hủy xin phép")
            };

            PickerChooseFalse.ItemsSource = new List<ItemPicker>()
            {
                new ItemPicker(1, "Sửa xin phép"),
                new ItemPicker(2, "Hủy xin phép")
            };

            #region Menu success

            PickerChooseTrue.SelectedIndexChanged += async (sender, args) =>
            {
                if (PickerChooseTrue.SelectedIndex != -1)
                {
                    int ValInt = ((ItemPicker)PickerChooseTrue.SelectedItem).ID;
                    switch (ValInt)
                    {
                        case 1:
                            await Navigation.PushAsync(new RegistrationRollCallPage(ItemSelect, false));
                            PickerChooseTrue.SelectedIndex = -1;
                            break;
                        case 2:
                        {
                            if (ItemSelect.NghiTu <= DateTime.Now)
                            {
                                await DisplayAlert("Thông báo",
                                    "Đơn này đa quá ngày yêu cầu hủy.\nChỉ có thể sử hoặc xem",
                                    "OK");
                                return;
                            }
                            int IntReturn = await ItemSelect.DeleteData(ItemSelect.ID);
                            if (IntReturn == 0)
                            {
                                await DisplayAlert("Thông báo",
                                    "Có lỗi xảy ra vui lòng thử lại",
                                    "OK");
                            }
                            else if (IntReturn < 0)
                            {
                                await DisplayAlert("Thông báo",
                                    new HandleError().IDErrorToNotify(IntReturn),
                                    "OK");
                            }
                            else
                            {
                                await DisplayAlert("Thông báo",
                                    "Đã gửi yêu cầu hủy xin phép cho GVCN, vui lòng đợi GVCN duyệt.",
                                    "OK");
                            }
                            PickerChooseTrue.SelectedIndex = -1;
                            break;
                        }
                        default:
                            break;
                    }
                }
            };
            

            #endregion

            #region Menu Wait success

            PickerChooseFalse.SelectedIndexChanged += async (sender, args) =>
            {
                if (PickerChooseFalse.SelectedIndex != -1)
                {
                    int ValInt = ((ItemPicker)PickerChooseFalse.SelectedItem).ID;
                    switch (ValInt)
                    {
                        case 1:
                            await Navigation.PushAsync(new RegistrationRollCallPage(ItemSelect, true));
                            PickerChooseFalse.SelectedIndex = -1;
                            break;
                        case 2:
                        {
                            if (ItemSelect.NghiTu <= DateTime.Now)
                            {
                                await DisplayAlert("Thông báo",
                                    "Đơn này đa quá ngày yêu cầu hủy.\nChỉ có thể sử hoặc xem",
                                    "OK");
                                return;
                            }
                            int IntReturn = await ItemSelect.DeleteData(ItemSelect.ID);
                            if (IntReturn == 0)
                            {
                                await DisplayAlert("Thông báo",
                                    "Có lỗi xảy ra vui lòng thử lại",
                                    "OK");
                            }
                            else if (IntReturn < 0)
                            {
                                await DisplayAlert("Thông báo",
                                    new HandleError().IDErrorToNotify(IntReturn),
                                    "OK");
                            }
                            else
                            {
                                await DisplayAlert("Thông báo",
                                    "Đã gửi yêu cầu hủy xin phép cho GVCN, vui lòng đợi GVCN duyệt.",
                                    "OK");
                            }
                            PickerChooseFalse.SelectedIndex = -1;
                            break;
                        }
                        default:
                            break;
                    }
                }
            };

            #endregion

            #endregion

            if (lst.Count == 0)
            {
                StackLayoutMain.Children.Remove(ListViewRRollCall);
                
            }
            else
            {
                lst.Sort(new SortXinPhep());
                lst.Reverse();
                ListViewRRollCall.ItemsSource = lst;
                StackLayoutMain.Children.Remove(lblNotify);
            }

            // event click item
            ListViewRRollCall.ItemSelected += async (sender, e) =>
            {
                if (ListViewRRollCall.SelectedItem != null)
                {
                    ItemSelect = (XinPhep)ListViewRRollCall.SelectedItem;
                    if (ItemSelect.TrangThai.Equals("Chờ duyệt"))
                    {
                        PickerChooseTrue.Focus();
                    }
                    else if (ItemSelect.TrangThai.Equals("Đã duyệt"))
                    {
                        PickerChooseFalse.Focus();
                    }
                    else
                    {
                        return;
                    }
                }

                ListViewRRollCall.SelectionMode = ListViewSelectionMode.None;
                ListViewRRollCall.SelectionMode = ListViewSelectionMode.Single;
            };

        }

        public async void GetData(int IDStudent)
        {
            ValueDTO<XinPhep> val = await new XinPhep().GetData(IDStudent);
            if (val.Error == 0)
            {
                lst = val.ListT;
            }
            else
            {
                await DisplayAlert("Thông báo", new HandleError().IDErrorToNotify(val.Error), "OK");
            }
        }

        //event click
        private async void MenuItem_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegistrationRollCallPage());
        }
    }
}