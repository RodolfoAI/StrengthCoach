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
    /// Interaction logic for StartButton.xaml
    /// </summary>
    public partial class StartButton : UserControl, INotifyPropertyChanged
    {
        public StartButton()
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

            if (ButtonContent == "Parar") 
            {
                ButtonContent = "Deteniendo lectura";

                // disestablish connection with microcontroller

                // reset reading
                mainWindow.PunchScore = $"Fuerza generada: ";
                ButtonContent = "Iniciar";
            }
            else 
            {
                ButtonContent = "Iniciando lectura";

                // establish connection with microcontroller

                //implement arduino reading
                mainWindow.PunchScore = $"Fuerza generada: {20}";

                // Update button content
                ButtonContent = "Parar";
            }
        }
    }

}
