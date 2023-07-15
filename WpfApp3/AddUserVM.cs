using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WpfApp3.Model;
using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Media.Imaging;

namespace WpfApp3
{
    public partial class AddUserVM : ObservableObject

    {


        [ObservableProperty]
        public string firstname;


        [ObservableProperty]
        public string lastname;

        [ObservableProperty]
        public string dateofbirth;

        [ObservableProperty]
        public double gpa;

        [ObservableProperty]
        public string windowTitle;

        [ObservableProperty]
        public BitmapImage selectedImage;



        public AddUserVM(User u)
        {
            Student = u;

            firstname = Student.FirstName;
            lastname = Student.LastName;
            gpa = Student.GPA;
            dateofbirth = Student.DateOfBirth;
            selectedImage = Student.Image;

        }
        public AddUserVM()
        {

        }


        //get image 
        [RelayCommand]
        public void UploadPhoto()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image files | *.bmp; *.png; *.jpg";
            dialog.FilterIndex = 1;
            if (dialog.ShowDialog() == true)
            {
                selectedImage = new BitmapImage(new Uri(dialog.FileName));
            }
        }






        public User Student { get; set; }
        public Action CloseAction { get; set; }

        [RelayCommand]
        public void Save()
        {



            if (gpa < 0 || gpa > 4)
            {
                MessageBox.Show("Invalid Values");
                return;
            }
            if (Student == null)
            {

                Student = new User()
                {
                    FirstName = firstname,
                    LastName = lastname,
                    DateOfBirth = dateofbirth,
                    Image = selectedImage,
                    GPA = gpa

                };


            }
            else
            {

                Student.FirstName = firstname;
                Student.LastName = lastname;
                Student.GPA = gpa;
                Student.Image = selectedImage;
                Student.DateOfBirth = dateofbirth;



            }

            if (Student.FirstName != null)
            {

                CloseAction();
            }
            Application.Current.MainWindow.Show();


        }
    }
}
