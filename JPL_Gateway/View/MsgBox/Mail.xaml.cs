using System.Windows;
using System.Windows.Controls;

namespace JPL_Gateway.View.MsgBox
{
    /// <summary>
    /// Mail.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Mail : Page
    {
        internal static Mail mail;

        public Mail()
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
