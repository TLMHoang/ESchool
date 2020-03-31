using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileSoLienLac.DTO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileSoLienLac.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChooseStudent : ContentPage
    {
        public class Student
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public string Lop { get; set; }


            public Student(int iD, string name, string lop)
            {
                ID = iD;
                Name = name;
                Lop = lop;
            }

            public static List<Student> GetListStudent()
            {
                List<Student> lst = new List<Student>();
                foreach (ThongTinHS i in App.lstStudents)
                {
                    lst.Add(new Student(i.ID, i.Ten, i.NoiSinh));
                }

                return lst;
            }
        }

        public class SetStudent
        {
            public List<Student> lstSetStudents { get; set; }
            public SetStudent()
            {
                lstSetStudents = Student.GetListStudent();
            }
        }

        public ChooseStudent()
        {
            InitializeComponent();

            BindingContext = new SetStudent();

            ListViewStudent.ItemSelected += async (sender, e) =>
            {
                App.StudentSeclect = App.lstStudents.FirstOrDefault(p => p.ID == ((Student)e.SelectedItem).ID);
                MessagingCenter.Send(this, "Update");
                await Navigation.PopModalAsync();
            };
        }
    }
}