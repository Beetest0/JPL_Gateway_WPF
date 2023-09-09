using System.Windows;
using System.Windows.Controls;

namespace JPL_Gateway.View.MsgBox
{
    /// <summary>
    /// Interaction logic for Warning_Msg.xaml
    /// </summary>
    public partial class Warning_Msg : Page
    {
        internal static Warning_Msg warning;

        public Warning_Msg()
        {
            InitializeComponent();
        }

        private void closeBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.mainwindow.frame1.IsEnabled = true;
            MainWindow.mainwindow.frame1.Opacity = 1;
            MainWindow.mainwindow.frame5.Content = null;
            MainWindow.mainwindow.frame5.Visibility = Visibility.Hidden;
        }
    }
}
