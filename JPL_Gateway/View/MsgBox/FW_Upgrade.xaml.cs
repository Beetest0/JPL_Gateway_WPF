using System.Windows;
using System.Windows.Controls;

namespace JPL_Gateway.View.MsgBox
{
    /// <summary>
    /// FW_Upgrade.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class FW_Upgrade : Page
    {
        internal static FW_Upgrade fwupgradebox;

        public FW_Upgrade()
        {
            InitializeComponent();
        }

        private void proceedBtn_Click(object sender, RoutedEventArgs e)
        {
            Device_update_Page.deviceupdatepage.firmware_upgrade();
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
