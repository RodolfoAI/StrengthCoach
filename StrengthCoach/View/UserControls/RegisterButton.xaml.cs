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
using StrengthCoach.Data;

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
                ButtonContent = "Registrar";
                return;
            }

            if (string.IsNullOrEmpty(studentAgeControl.TextContent))
            {
                MessageBox.Show("Favor de ingresar edad del estudiante.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                ButtonContent = "Registrar";
                return;
            }

            string studentName = studentNameControl.TextContent;
            
            if (!int.TryParse(studentAgeControl.TextContent, out int studentAge))
            {
                MessageBox.Show("La edad debe ser un número válido.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                ButtonContent = "Registrar";
                return;
            }

            try
            {
                await Task.Run(() => DatabaseService.RegisterStudent(studentName, studentAge));

                MessageBox.Show("Registro exitoso", "Exitoso", MessageBoxButton.OK, MessageBoxImage.Information);
                
                // Refresh the students list in MainWindow
                MainWindow mainWindowInstance = mainWindow as MainWindow;
                if (mainWindowInstance != null)
                {
                    mainWindowInstance.Students.Clear();
                    foreach (var student in DatabaseService.GetAllStudents())
                    {
                        mainWindowInstance.Students.Add(student);
                    }
                }

                // Clear the input fields
                studentNameControl.TextContent = "";
                studentAgeControl.TextContent = "";

                ButtonContent = "Registrar";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al registrar: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                ButtonContent = "Registrar";
            }
        }
    }
}