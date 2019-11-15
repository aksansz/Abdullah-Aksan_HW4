using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CetStudents
{
    /// <summary>
    /// Interaction logic for CourseWindow.xaml
    /// </summary>
    public partial class CourseWindow : Window
    {
        public CourseWindow()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Random random = new Random();
            Course Course = new Course();
            Course.Id = Convert.ToInt32(txtCourseId.Text);
            Course.Name = txtCourseName.Text;
            Course.Quota = txtCourseQuota.Text;
            Course.Credit = Convert.ToInt32(txtCourseCredit.Text);

            CetDb db = new CetDb();
            db.Courses.Add(Course);

            db.SaveChanges();
            MessageBox.Show("Ders Eklendi.");
            txtCourseId.Text = "";
            txtCourseName.Text = "";
            txtCourseQuota.Text = "";
            txtCourseCredit.Text = "";
            LoadStudents();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadStudents();
        }

        private void LoadStudents()
        {
            CetDb db = new CetDb();
            List<Course> courses = db.Courses.ToList();
            dgStudents.ItemsSource = courses;
        }

        private void dgStudents_Selected(object sender, RoutedEventArgs e)
        {

        }

        private void dgStudents_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            Course course = dgStudents.SelectedItem as Course ;
            if (course != null)
            {
                txtCourseId.Text= course.Id.ToString();
                txtCourseName.Text = course.Name;
                txtCourseQuota.Text = course.Quota;
                txtCourseCredit.Text = course.Credit.ToString();
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Course course = dgStudents.SelectedItem as Course;
            if (course != null)
            {
                CetDb db = new CetDb();
                db.Courses.Remove(course);
                db.SaveChanges();
                MessageBox.Show("Ders Silindi!");
                LoadStudents();

            }
            else
            {
                MessageBox.Show("Silmek için ders seçmelisin!");
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Course course = dgStudents.SelectedItem as Course;
            if (course != null)
            {
                CetDb db = new CetDb();
                var coursenew = db.Courses.Find(course.Id);
                coursenew.Name = txtCourseName.Text;
                coursenew.Quota = txtCourseQuota.Text;
                coursenew.Credit = Convert.ToInt32(txtCourseId.Text);
                db.SaveChanges();
                LoadStudents();
                MessageBox.Show("Güncellendi.");
            }
            else
            {
                MessageBox.Show("güncellemek için ders seçmelisin!");
            }
        }

    }
}
