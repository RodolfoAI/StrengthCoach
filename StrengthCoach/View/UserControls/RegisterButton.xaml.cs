using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace StrengthCoach.View.UserControls
{
    /// <summary>
    /// Interaction logic for RegisterButton.xaml
    /// </summary>
    public partial class RegisterButton : UserControl, INotifyPropertyChanged
    {
        public RegisterButton()
        {
            DataContext = this;
            InitializeComponent();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private string buttonContent;

        public string ButtonContent
        {
            get { return buttonContent; }
            set
            {
                buttonContent = value;
                OnPropertyChanged();
            }
        }

        private void OnPropertyChanged([CallerMemberName] string? propertyname = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            ButtonContent = "Registrando";

            // Get the MainWindow
            Window mainWindow = Application.Current.MainWindow;

            // Find the DataEntry control named "studentAgeEntry"
            DataEntry studentAgeControl = mainWindow.FindName("studentAgeEntry") as DataEntry;

            // Find the DataEntry control named "studentNameEntry"
            DataEntry studentNameControl = mainWindow.FindName("studentNameEntry") as DataEntry;

            if (string.IsNullOrEmpty(studentNameControl.TextContent))
            {
                MessageBox.Show("Favor de ingresar nombre del estudiante.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrEmpty(studentAgeControl.TextContent))
            {
                MessageBox.Show("Favor de ingresar edad del estudiante.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string studentName = studentNameControl.TextContent;
            string studentAge = studentAgeControl.TextContent;

            //save in db

            MessageBox.Show("Registro exisotoso", "Exitoso", MessageBoxButton.OK, MessageBoxImage.Information);
            ButtonContent = "Registrar";
        }
    }
}