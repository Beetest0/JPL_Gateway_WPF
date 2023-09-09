using System.Windows;
using System.Windows.Controls;
using System.Threading;

namespace JPL_Gateway.View
{
    /// <summary>
    /// Select_device_Page.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Select_device_Page : Page
    {
        internal static Select_device_Page selectdevicepage;

        public Select_device_Page()
        {
            InitializeComponent();
        }

        private void Device_update_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MainWindow.mainwindow.frame1.Content = Device_update_Page.deviceupdatepage;
        }

        private void Device_settings_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            int device_num = MainWindow.mainwindow.isSelectdevice;

            MainWindow.mainwindow.isSetting_access = false;
            //Device_settings_Page.devicesettingspage.Setting_icon1(false);
            Device_settings_Page.devicesettingspage.Setting_icon1(true);
            if (MainWindow.mainwindow.isSetting_access == false)
            {
                MainWindow.mainwindow.Check_beep_setting(device_num);
                Thread.Sleep(200);
                MainWindow.mainwindow.Check_eq_setting(device_num);
            }
            MainWindow.mainwindow.frame1.Content = Device_settings_Page.devicesettingspage;
        }

        public void Setting_icon(bool Enable)
        {
            /*
            if (isEnable)
            {
                Device_settings.Visibility = Visibility.Visible;
                Settings_text.Visibility = Visibility.Visible;

                Thickness margin = Device_update.Margin;
                margin.Left = 190;
                Thickness margin_text = Updates_text.Margin;
                margin_text.Left = 190;
                Device_update.Margin = margin;
                Updates_text.Margin = margin_text;
            }
            else
            {
                Device_settings.Visibility = Visibility.Hidden;
                Settings_text.Visibility = Visibility.Hidden;

                Thickness margin = Device_update.Margin;
                margin.Left = 385;
                Thickness margin_text = Updates_text.Margin;
                margin_text.Left = 385;
                Device_update.Margin = margin;
                Updates_text.Margin = margin_text;
            }
                */
        }

        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.mainwindow.frame1.Content = Connected_Page.conpage;
        }
    }
}
