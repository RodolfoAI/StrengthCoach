using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Ports;
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
    /// Interaction logic for StartButton.xaml
    /// </summary>
    public partial class StartButton : UserControl, INotifyPropertyChanged
    {
        public StartButton()
        {
            DataContext = this;
            InitializeComponent();
        }

        private SerialPort serialPort;

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
            // Reset display reading
            mainWindow.PunchScore = $"Fuerza generada: ";

            ButtonContent = "Iniciando lectura";

            // Establish connection with arduino
            serialPort = new SerialPort("COM5", 9600);
            try { 
                serialPort.Open();
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show($"Error al conectar con el dispositivo, posiblemente el puerto serial esta en uso por otra aplicacion: {ex.Message}", 
                    "Error de conexión", MessageBoxButton.OK, MessageBoxImage.Error);
                ButtonContent = "Iniciar";
                return;
            }

            // send start command (1) to arduino
            serialPort.WriteLine("1");

            var score = serialPort.ReadLine();
            string displayKilograms = "0.0";
            if (decimal.TryParse(score, out decimal grams))
            {
                // Convert grams to kilograms
                decimal kilograms = grams / 1000m;

                // Round to one decimal place
                // The MidpointRounding.AwayFromZero ensures 1.25 rounds to 1.3
                decimal roundedKilograms = Math.Round(kilograms, 1, MidpointRounding.AwayFromZero);

                // 3. Format the result for display 
                // The "F1" format specifier ensures exactly one decimal place
                displayKilograms = roundedKilograms.ToString("F1");
            }

            mainWindow.PunchScore = $"Fuerza generada: {displayKilograms}";

            // send stop command (0) to arduino
            serialPort.WriteLine("0");

            serialPort.Close();
       
            ButtonContent = "Iniciar";
        }
    }

}
