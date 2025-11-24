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
                
                string punchScoreText = mainWindow.PunchScore;

                string[] punchScoreTextSplit = punchScoreText.Split(':');
                string punchScoreValue = punchScoreTextSplit[1].Trim();

                // Validate PunchScore
                if (string.IsNullOrWhiteSpace(punchScoreValue))
                {
                    MessageBox.Show("El puntaje no puede estar vacío.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    ButtonContent = "Guardar";
                    return;
                }

                // Access selected student
                ComboBox studentComboBox = mainWindow.FindName("studentComboBox") as ComboBox;

                // Validate student selection
                if (studentComboBox?.SelectedItem?.ToString() == null)
                {
                    MessageBox.Show("Seleccione un estudiante antes de guardar.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    ButtonContent = "Guardar";
                    return;
                }

                string selectedStudent = studentComboBox.SelectedItem.ToString();

                // Store values for use
                System.Diagnostics.Debug.WriteLine($"PunchScore: {punchScoreText}");
                System.Diagnostics.Debug.WriteLine($"Selected Student: {selectedStudent}");

                // Save in DB


                MessageBox.Show("Guardado exitosamente", "Exitoso", MessageBoxButton.OK, MessageBoxImage.Information);

                // Update UI
                ButtonContent = "Guardar";
                mainWindow.PunchScore = "Fuerza generada: ";


            }
        }
    }
}
