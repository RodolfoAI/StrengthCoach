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
    /// Interaction logic for DeleteButton.xaml
    /// </summary>
    public partial class DeleteButton : UserControl, INotifyPropertyChanged
    {
        public DeleteButton()
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
   // Get the MainWindow
      Window mainWindow = Application.Current.MainWindow;

         // Find the student combo box
      ComboBox studentComboBox = mainWindow.FindName("studentComboBox") as ComboBox;

   if (studentComboBox.SelectedItem == null || string.IsNullOrEmpty(studentComboBox.SelectedItem.ToString()))
        {
           MessageBox.Show("Favor de seleccionar un estudiante para eliminar.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
     return;
      }

          string studentName = studentComboBox.SelectedItem.ToString();

         // Confirm deletion
          MessageBoxResult result = MessageBox.Show(
       $"¿Estás seguro de que deseas eliminar al estudiante '{studentName}'? Se eliminarán todos sus registros.",
    "Confirmar eliminación",
    MessageBoxButton.YesNo,
     MessageBoxImage.Warning);

        if (result != MessageBoxResult.Yes)
      {
  return;
    }

     ButtonContent = "Eliminando";

     try
            {
  await Task.Run(() => DatabaseService.DeleteStudent(studentName));

                MessageBox.Show("Estudiante eliminado exitosamente", "Exitoso", MessageBoxButton.OK, MessageBoxImage.Information);

   // Refresh the students list in MainWindow
                MainWindow mainWindowInstance = mainWindow as MainWindow;
          if (mainWindowInstance != null)
    {
        mainWindowInstance.Students.Clear();
     foreach (var student in DatabaseService.GetAllStudents())
   {
 mainWindowInstance.Students.Add(student);
                 }

         // Refresh the ranking as well
    mainWindowInstance.RefreshRanking();
  }

 // Clear the combo box selection
                studentComboBox.SelectedItem = null;

          ButtonContent = "Eliminar";
  }
            catch (Exception ex)
    {
          MessageBox.Show($"Error al eliminar: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
       ButtonContent = "Eliminar";
   }
 }
    }
}
