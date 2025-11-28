using StrengthCoach.View.UserControls;
using StrengthCoach.Data;
using StrengthCoach.Models;
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
        private RankingCategory currentCategory = RankingCategory.General;

        public MainWindow()
        {
            DataContext = this;
            students = new ObservableCollection<string>();
            studentRanking = new ObservableCollection<StudentRankingViewModel>();

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

        private ObservableCollection<StudentRankingViewModel> studentRanking;

        public ObservableCollection<StudentRankingViewModel> StudentRanking
        {
            get { return studentRanking; }
            set
            {
                studentRanking = value;
                OnPropertyChanged();
            }
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

            List<string> studentsFromDb = DatabaseService.GetAllStudents();

            foreach (var student in studentsFromDb)
                Students.Add(student);

            RefreshRanking();
        }

        public void RefreshRanking()
        {
            List<StudentRankingViewModel> rankingData = DatabaseService.GetStudentRanking(currentCategory);
            StudentRanking.Clear();
            foreach (var entry in rankingData)
                StudentRanking.Add(entry);
        }

        private void RankingCategory_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                currentCategory = button.Name switch
                {
                    "GeneralButton" => RankingCategory.General,
                    "KidsButton" => RankingCategory.Kids,
                    "SuperKidsButton" => RankingCategory.SuperKids,
                    "TeenagersButton" => RankingCategory.Teenagers,
                    "AdultsButton" => RankingCategory.Open,
                    _ => RankingCategory.General
                };

                RefreshRanking();
                UpdateButtonStyles();
            }
        }

        private void UpdateButtonStyles()
        {
            GeneralButton.Background = new SolidColorBrush(currentCategory == RankingCategory.General ? 
                Color.FromArgb(255, 26, 58, 82) : Color.FromArgb(255, 68, 68, 68));
            KidsButton.Background = new SolidColorBrush(currentCategory == RankingCategory.Kids ?
                Color.FromArgb(255, 26, 58, 82) : Color.FromArgb(255, 68, 68, 68));
            SuperKidsButton.Background = new SolidColorBrush(currentCategory == RankingCategory.SuperKids ? 
                Color.FromArgb(255, 26, 58, 82) : Color.FromArgb(255, 68, 68, 68));
            TeenagersButton.Background = new SolidColorBrush(currentCategory == RankingCategory.Teenagers ? 
                Color.FromArgb(255, 26, 58, 82) : Color.FromArgb(255, 68, 68, 68));
            AdultsButton.Background = new SolidColorBrush(currentCategory == RankingCategory.Open ? 
                Color.FromArgb(255, 26, 58, 82) : Color.FromArgb(255, 68, 68, 68));
        }
    }
}