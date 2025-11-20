using System.Windows;
using System.Windows.Controls;

namespace StrengthCoach.View.UserControls
{
    /// <summary>
    /// Interaction logic for WindowHeader.xaml
    /// </summary>
    public partial class WindowHeader : UserControl
    {
        public WindowHeader()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets a reference to the parent Window hosting this UserControl.
        /// </summary>
        private Window GetParentWindow()
        {
            return Window.GetWindow(this);
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            Window parentWindow = GetParentWindow();

            if (parentWindow != null)
                parentWindow.WindowState = WindowState.Minimized;
        }

        private void btnMaximize_Click(object sender, RoutedEventArgs e)
        {
            Window parentWindow = GetParentWindow();

            if (parentWindow != null)
            {
                if (parentWindow.WindowState == WindowState.Normal)
                    parentWindow.WindowState = WindowState.Maximized;
                else
                    parentWindow.WindowState = WindowState.Normal;
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Window parentWindow = GetParentWindow();

            parentWindow?.Close();
        }
    }
}