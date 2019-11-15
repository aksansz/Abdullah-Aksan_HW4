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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Random random = new Random();
            Student student = new Student();
            student.Id = random.Next(1, 200);
            student.Name = txtStudentName.Text;
            student.Surname = txtStudentSurname.Text;
            student.BirthDate = dtpBirthDate.SelectedDate.Value;

            CetDb db = new CetDb();
            db.Students.Add(student);
            
            db.SaveChanges();
            MessageBox.Show("Öğrenci Kaydedildi.");
            lblStudentId.Content = "";
            txtStudentName.Text = "";
            txtStudentSurname.Text = "";
            dtpBirthDate.SelectedDate = DateTime.Now;
            LoadStudents(); 
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadStudents();
        }

        private void LoadStudents()
        {
            CetDb db = new CetDb();
            List<Student> students = db.Students.ToList();
            dgStudents.ItemsSource = students;
        }

        private void dgStudents_Selected(object sender, RoutedEventArgs e)
        {
           
        }

        private void dgStudents_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            Student student = dgStudents.SelectedItem as Student;
            if (student != null)
            {
                lblStudentId.Content = student.Id;
                txtStudentName.Text = student.Name;
                txtStudentSurname.Text = student.Surname;
                dtpBirthDate.SelectedDate = student.BirthDate;
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Student student = dgStudents.SelectedItem as Student;
            if (student != null)
            {
                CetDb db = new CetDb();
                db.Students.Remove(student);
                db.SaveChanges();
                MessageBox.Show("Öğrenci Silindi!");
                LoadStudents();

            }
            else
            {
                MessageBox.Show("Silmek için öğrenci seçmelisin!");
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Student student = dgStudents.SelectedItem as Student;
            if (student != null)
            {
                CetDb db = new CetDb();
                var studentnew = db.Students.Find(student.Id);
                studentnew.Name = txtStudentName.Text;
                studentnew.Surname = txtStudentSurname.Text;
                studentnew.BirthDate = dtpBirthDate.SelectedDate.Value;
                db.SaveChanges();
                LoadStudents();
                MessageBox.Show("Güncellendi.");
            }
            else
            {
                MessageBox.Show("güncellemek için öğrenci seçmelisin!");
            }
        }

        private void btnOpenCourses_Click(object sender, RoutedEventArgs e)
        {
            CourseWindow course = new CourseWindow();
            course.Show();
        }
    }
}
