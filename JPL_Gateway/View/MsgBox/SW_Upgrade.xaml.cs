using System.Windows;
using System.Windows.Controls;

namespace JPL_Gateway.View.MsgBox
{
    /// <summary>
    /// SW_Upgrade.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class SW_Upgrade : Page
    {
        internal static SW_Upgrade swupgradebox;

        public SW_Upgrade()
        {
            InitializeComponent();
        }

        private void proceedBtn_Click(object sender, RoutedEventArgs e)
        {
            Settings_Page.settingpage.Setup_upgrade();
        }

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.mainwindow.frame1.IsEnabled = true;
            MainWindow.mainwindow.frame1.Opacity = 1;
            MainWindow.mainwindow.frame5.Content = null;
            MainWindow.mainwindow.frame5.Visibility = Visibility.Hidden;
        }
    }
}