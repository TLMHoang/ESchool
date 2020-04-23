using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileSoLienLac.DTO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileSoLienLac.Views.Student
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PointPage : ContentPage
    {
        List<List<BangDiem>> lst = new List<List<BangDiem>>();
        public PointPage()
        {
            InitializeComponent();

            Title = "Điểm - " + App.StudentSeclect.FistName();
            
            GetData();

            if (lst.Count != 0)
            {
                lblNotify.IsVisible = false;
            }
        }

        public async void GetData()
        {
            ValueDTO<BangDiem> val = await new BangDiem().GetData(App.StudentSeclect.ID);

            int Count = val.ListT.Count;
            int lstIndex = 0;
            foreach (MonHoc m in App.lstMonHocs)
            {
                if (lst.Count == lstIndex)
                {
                    lst.Add(new List<BangDiem>());
                }

                for (int j = 0; j < Count; j++)
                {
                    if (val.ListT[j].IDMonHoc == m.ID)
                    {
                        lst[lstIndex].Add(val.ListT[j]);

                        val.ListT.RemoveAt(j);

                        Count--;
                        j--;
                    }
                }

                if (lst[lstIndex].Count == 0)
                {
                    lst.RemoveAt(lstIndex);
                }
                else
                {
                    lstIndex++;
                }
            }
            CreatePage();
        }


        public void CreatePage()
        {
            foreach (MonHoc m in App.lstMonHocs)
            {
                // Create Item Main
                StackLayout slo = new StackLayout();
                Frame frm = new Frame();
                frm.Content = slo;
                LayoutMain.Children.Add(frm);

                //event show content in Item
                TapGestureRecognizer tap = new TapGestureRecognizer();
                tap.Tapped += SelectSubject_OnTapped;
                frm.GestureRecognizers.Add(tap);

                #region Title follow Subject

                StackLayout sloIS = new StackLayout();
                sloIS.Orientation = StackOrientation.Horizontal;
                //Label
                Label lbl = new Label();
                lbl.Text = m.TenMon;
                lbl.FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label));
                sloIS.Children.Add(lbl);

                //Icon
                Image img = new Image();
                img.Source = "more.png";
                img.HeightRequest = img.WidthRequest = 30;
                img.HorizontalOptions = LayoutOptions.EndAndExpand;
                img.VerticalOptions = LayoutOptions.CenterAndExpand;
                sloIS.Children.Add(img);

                slo.Children.Add(sloIS);

                #endregion


                StackLayout sloSemester = new StackLayout();
                sloSemester.Margin = new Thickness(10, 0, 0, 0);
                sloSemester.IsVisible = false;
                for (int i = 1; i >= 0; i--)
                {
                    StackLayout sloItemS = new StackLayout();

                    //event tapped semester
                    TapGestureRecognizer tapSemester = new TapGestureRecognizer();
                    tapSemester.Tapped += SelectSemester_OnTapped;
                    sloItemS.GestureRecognizers.Add(tapSemester);


                    #region Title Name HK + DTB

                    StackLayout sloHK = new StackLayout();
                    sloHK.Orientation = StackOrientation.Horizontal;
                    //label name
                    Label lblSemester = new Label();
                    lblSemester.Text = "Học Kỳ " + ((i == 1) ? "I" : "II");
                    lblSemester.FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));
                    //lblSemester.TextColor = Color.Red;
                    sloHK.Children.Add(lblSemester);
                    //Lbl Point
                    Label lblDTB = new Label();
                    lblDTB.FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));
                    lblDTB.HorizontalOptions = LayoutOptions.EndAndExpand;
                    sloHK.Children.Add(lblDTB);

                    #endregion

                    // add child
                    sloItemS.Children.Add(sloHK);
                    

                    StackLayout slPoint = new StackLayout();
                    slPoint.IsVisible = false;
                    
                    sloItemS.Children.Add(slPoint);

                    #region Load SPoint
                    //Select list point of Subject
                    List<BangDiem> lstBD = lst.FirstOrDefault(p => p[0].IDMonHoc == m.ID);

                    // value DTB
                    double CountPoint = 0;
                    int HS = 0;

                    // output Point
                    foreach (LoaiDiem d in App.lstLoaiDiems)
                    {
                        Grid GrPoint = new Grid();
                        GrPoint.ColumnDefinitions.Add(new ColumnDefinition(){Width = new GridLength(1.5, GridUnitType.Star)});
                        GrPoint.ColumnDefinitions.Add(new ColumnDefinition(){Width = new GridLength(3.5, GridUnitType.Star) });
                        GrPoint.RowDefinitions.Add(new RowDefinition(){Height = GridLength.Auto});

                        //Label
                        Label lblSPoint = new Label();
                        lblSPoint.Text = d.TenLoaiDiem;
                        lblSPoint.FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));
                        lblSPoint.HorizontalTextAlignment = TextAlignment.Start;
                        lblSPoint.VerticalTextAlignment = TextAlignment.Center;
                        lblSPoint.Margin = new Thickness(10,0,0,0);

                        ScrollView sc = new ScrollView();
                        sc.Orientation = ScrollOrientation.Both;
                        FlexLayout flo = new FlexLayout();

                        if (lstBD != null)
                        {
                            foreach (BangDiem point in lstBD)
                            {
                                if (point.IDLoaiDiem == d.ID && point.HocKyI == i )
                                {
                                    Label lblPoint = new Label();
                                    lblPoint.WidthRequest = 50;
                                    lblPoint.HorizontalTextAlignment = TextAlignment.Center;
                                    lblPoint.VerticalTextAlignment = TextAlignment.Center;
                                    lblPoint.Text = point.Diem.ToString();
                                    lblPoint.BackgroundColor = ConvetNumToColor(point.Diem);
                                    flo.Children.Add(lblPoint);

                                    // setup for DiemTB
                                    CountPoint += (point.Diem * d.HeSo);
                                    HS += d.HeSo;
                                }
                            }
                        }

                        sc.Content = flo;
                        GrPoint.Children.Add(lblSPoint);
                        GrPoint.Children.Add(sc, 1,0);
                        slPoint.Children.Add(GrPoint);
                    }

                    #endregion
                    sloSemester.Children.Add(sloItemS);

                    // add DTB
                    string strDTB = Math.Round((CountPoint / HS), 1).ToString();
                    if (strDTB == "NaN")
                    {
                        lblDTB.Text = "Chưa có điểm";
                    }
                    else
                    {
                        lblDTB.Text = strDTB;
                        lblDTB.TextColor = Color.Red;
                    }
                }
                slo.Children.Add(sloSemester);
            }
        }

        private void SelectSubject_OnTapped(object sender, EventArgs e)
        {
            Frame eventFrame = (Frame) sender;

            StackLayout slo = (StackLayout) eventFrame.Children[0];

            #region Bug - not active

            //if (slo.Children[1].IsVisible)
            //{
            //}
            //else
            //{
            //    foreach (var Item in LayoutMain.Children)
            //    {
            //        if (Item is Frame)
            //        {
            //            Frame indexFrame = (Frame) sender;
            //            StackLayout ISlo = (StackLayout)indexFrame.Children[0];
            //            StackLayout valslo = (StackLayout)ISlo.Children[1];
            //            valslo.IsVisible = !valslo.IsVisible;
            //            //ISlo.Children[1].IsVisible = false;

            //        }
            //    }
            //}

            #endregion

            slo.Children[1].IsVisible = !slo.Children[1].IsVisible;
            foreach (var iChild in ((StackLayout)slo.Children[1]).Children)
            {
                if (iChild is StackLayout)
                {
                    StackLayout sloChild = (StackLayout) iChild;
                    sloChild.Children[1].IsVisible = false;
                }
            }

            Image img = ((Image) ((StackLayout) slo.Children[0]).Children[1]);

            if (img.Source.ToString().Contains("more.png"))
            {
                img.Source = "less.png";
            }
            else
            {
                img.Source = "more.png";

            }
        }

        private void SelectSemester_OnTapped(object sender, EventArgs e)
        {
            StackLayout slo = (StackLayout)sender;

            slo.Children[1].IsVisible = !slo.Children[1].IsVisible;
        }


        public Color ConvetNumToColor(double num)
        {
            if (num >= 8)
            {
                return Color.LimeGreen;
            }
            else if (num >= 7)
            {
                return Color.LightGreen;
            }
            else if (num >=5)
            {
                return Color.Orange;

            }
            else
            {
                return Color.Red;
            }

        }
        //Not use
        //private void SelectPoint_OnTapped(object sender, EventArgs e)
        //{
        //    StackLayout slo = (StackLayout)sender;

        //}
    }
}