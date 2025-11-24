using StrengthCoach.View.UserControls;
using StrengthCoach.Data;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StrengthCoach
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public MainWindow()
        {
            DataContext = this;
            students = new ObservableCollection<string>();

            InitializeComponent();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public string PunchScore
        {
            get { return punchScoreText.Text; }
            set
            {
                punchScoreText.Text = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<string> students;

        public ObservableCollection<string> Students
        {
            get { return students; }
            set { students = value; }
        }

        private void OnPropertyChanged([CallerMemberName] string? propertyname = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DatabaseService.InitializeDatabase();

            // Load students from database
            List<string> studentsFromDb = DatabaseService.GetAllStudents();

            foreach (var student in studentsFromDb)
                Students.Add(student);
        }
    }
}