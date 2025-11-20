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
    /// Interaction logic for SaveButton.xaml
    /// </summary>
    public partial class SaveButton : UserControl, INotifyPropertyChanged
    {
        public SaveButton()
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
            // Get the MainWindow instance
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;

            if (mainWindow != null)
            {
                ButtonContent = "Guardando";
                // Access the PunchScore property
                string punchScoreValue = mainWindow.PunchScore;

                // Access selected student
                ComboBox studentComboBox = mainWindow.FindName("studentComboBox") as ComboBox;
                //handle the null case safely
                string selectedStudent = studentComboBox?.SelectedItem?.ToString() ?? "No student selected";

                // Store values for use
                System.Diagnostics.Debug.WriteLine($"PunchScore: {punchScoreValue}");
                System.Diagnostics.Debug.WriteLine($"Selected Student: {selectedStudent}");

                //save in DB

                MessageBox.Show("Guardado exitosamente", "Exitoso", MessageBoxButton.OK, MessageBoxImage.Information);
                // Update button content
                ButtonContent = "Guardar";
                
                
            }
        }
    }
}
