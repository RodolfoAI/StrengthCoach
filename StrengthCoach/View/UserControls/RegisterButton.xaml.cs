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
            DataEntry studentAgeEntry = mainWindow.FindName("studentAgeEntry") as DataEntry;

            // Find the DataEntry control named "studentNameEntry"
            DataEntry studentNameEntry = mainWindow.FindName("studentNameEntry") as DataEntry;

            if (studentAgeEntry != null && studentNameEntry != null)
            {
                string studentAgeEntryContent = studentAgeEntry.TextContent;
                string studentNameEntryContent = studentNameEntry.TextContent;
                // Use the content as needed
                System.Diagnostics.Debug.WriteLine($"Name content: {studentNameEntryContent}");
                System.Diagnostics.Debug.WriteLine($"Age content: {studentAgeEntryContent}");
                
                MessageBox.Show("Registro exisotoso", "Exitoso", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}

