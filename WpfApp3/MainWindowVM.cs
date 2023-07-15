using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WpfApp3.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace WpfApp3
{
    public partial class MainWindowVM : ObservableObject
    {
        [ObservableProperty]
        public ObservableCollection<User> users;
        [ObservableProperty]
        public User selectedUser = null;



        public void CloseMainWindow()
        {
            Application.Current.MainWindow.Close();
        }




        [RelayCommand]
        public void messege()
        {

            MessageBox.Show($"{selectedUser.FirstName} GPA value must be between 0 and 4.", "Error");
        }

        [RelayCommand]
        public void AddStudent()
        {
            var vm = new AddUserVM();
            vm.windowTitle = "ADD USER";
            AddUser window = new AddUser(vm);
            window.ShowDialog();

            if (vm.Student.FirstName != null)
            {
                users.Add(vm.Student);
            }
        }

        [RelayCommand]
        public void Delete()
        {
            users.Clear();
        }
        [RelayCommand]
         public void DeleteUser(object parameter)
        {
            
            var selectedStudent = parameter as User;

            if (selectedStudent != null)
            {
                
                users.Remove(selectedStudent);
            }
        }
        [RelayCommand]
        public void EditUser(object parameter)
        {
            var selectedStudent = parameter as User;
            if(selectedStudent != null)
            {
                var vm = new AddUserVM(selectedStudent);
                vm.windowTitle = "Edit Student";
                var window = new AddUser(vm);
                window.ShowDialog();

                int i = users.IndexOf(selectedStudent);
                users.RemoveAt(i);
                users.Insert(i, vm.Student);
            }
        }
        [RelayCommand]
        public void ExecuteEditStudentCommand()
        {
            if (selectedUser != null)
            {
                var vm = new AddUserVM(selectedUser);
                vm.windowTitle = "EDIT USER";
                var window = new AddUser(vm);

                window.ShowDialog();


                int index = users.IndexOf(selectedUser);
                users.RemoveAt(index);
                users.Insert(index, vm.Student);
            }
            else
            {
                MessageBox.Show("Please Select the student", "Warning!");
            }
        }

        public MainWindowVM()
        {
            users = new ObservableCollection<User>();
            BitmapImage image1 = new BitmapImage(new Uri("/Model/Images/1.png", UriKind.Relative));
            users.Add(new User("Adolf", "Hitler", "20.04.1889", image1));
            BitmapImage image2 = new BitmapImage(new Uri("/Model/Images/2.png", UriKind.Relative));
            users.Add(new User("Joseph", "Stalin", "12/1/2000", image2));
            BitmapImage image3 = new BitmapImage(new Uri("/Model/Images/3.png", UriKind.Relative));
            users.Add(new User("Mao", "SeDoung", "12/1/2000", image3));
            

        }
    }
}
