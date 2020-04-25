using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Android.Graphics;
using MobileSoLienLac.Annotations;
using MobileSoLienLac.DTO;

namespace MobileSoLienLac.ViewModels
{ 
    public class PointViewModel : INotifyPropertyChanged
    {
        private double _dtbi;
        private double _dtbii;

        public double DTBI
        {
            get => _dtbi;
            set
            {
                _dtbi = value;
                OnPropertyChanged(nameof(DTBI));
            }
        }

        public double DTBII
        {
            get => _dtbii;
            set
            { 
                _dtbii = value;
                OnPropertyChanged(nameof(DTBII));
            }
        }

        public PointInSemester SemesterI { get; set; }
        public PointInSemester SemesterII { get; set; }

        public PointViewModel(List<BangDiem> lst, int IDSubject)
        {
            SemesterI = new PointInSemester();
            SemesterII = new PointInSemester();
            double CountPI = 0;
            double CountPII = 0;
            int CountHI = 0;
            int CountHII = 0;


            foreach (BangDiem TP in lst)
            {
                if (TP.IDMonHoc == IDSubject)
                {
                    if (TP.HocKyI == 1)
                    {
                        switch (TP.IDLoaiDiem)
                        {
                            case 1:
                                SemesterI.Mieng.Add(TP);
                                CountHI += App.lstLoaiDiems[0].HeSo;
                                CountPI += (App.lstLoaiDiems[0].HeSo * TP.Diem);

                                break;
                            case 2:
                                SemesterI.Phut.Add(TP);
                                CountHI += App.lstLoaiDiems[1].HeSo;
                                CountPI += (App.lstLoaiDiems[1].HeSo * TP.Diem);
                                break;
                            case 3:
                                SemesterI.Tiet.Add(TP);
                                CountHI += App.lstLoaiDiems[2].HeSo;
                                CountPI += (App.lstLoaiDiems[2].HeSo * TP.Diem);
                                break;
                            case 4:
                                SemesterI.HocKy.Add(TP);
                                CountHI += App.lstLoaiDiems[3].HeSo;
                                CountPI += (App.lstLoaiDiems[3].HeSo * TP.Diem);
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        switch (TP.IDLoaiDiem)
                        {
                            case 1:
                                SemesterII.Mieng.Add(TP);
                                CountHII += App.lstLoaiDiems[0].HeSo;
                                CountPII += (App.lstLoaiDiems[0].HeSo * TP.Diem);
                                break;
                            case 2:
                                SemesterII.Phut.Add(TP);
                                CountHII += App.lstLoaiDiems[1].HeSo;
                                CountPII += (App.lstLoaiDiems[1].HeSo * TP.Diem);
                                break;
                            case 3:
                                SemesterII.Tiet.Add(TP);
                                CountHII += App.lstLoaiDiems[2].HeSo;
                                CountPII += (App.lstLoaiDiems[2].HeSo * TP.Diem);
                                break;
                            case 4:
                                SemesterII.HocKy.Add(TP);
                                CountHII += App.lstLoaiDiems[3].HeSo;
                                CountPII += (App.lstLoaiDiems[3].HeSo * TP.Diem);
                                break;
                            default:
                                break;
                        }
                    }
                }
            }

            DTBI = Math.Round((CountPI / CountHI), 1);
            DTBII = Math.Round((CountPII / CountHII), 1);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class PointInSemester
    {
        public ObservableCollection<BangDiem> Mieng { get; set; }
        public ObservableCollection<BangDiem> Phut { get; set; }
        public ObservableCollection<BangDiem> Tiet { get; set; }
        public ObservableCollection<BangDiem> HocKy { get; set; }

        public PointInSemester()
        {
            Mieng = new ObservableCollection<BangDiem>();
            Phut = new ObservableCollection<BangDiem>();
            Tiet = new ObservableCollection<BangDiem>();
            HocKy = new ObservableCollection<BangDiem>();
        }
    }
}
