using System;
using System.Windows;
using System.Windows.Controls;

namespace JPL_Gateway.View.MsgBox
{
    /// <summary>
    /// Setting_Reset.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Setting_Reset : Page
    {
        internal static Setting_Reset resetbox;

        public Setting_Reset()
        {
            InitializeComponent();
        }

        private void resetBtn_Click(object sender, RoutedEventArgs e)
        {
            if (MainWindow.mainwindow.isSetting_access == true)
            {
                Device_settings_Page.devicesettingspage.Reset_Setting();
            }
            Device_settings_Page.devicesettingspage.Reset_Softphone_Setting();
            AppConfiguration.RemoveAppConfig("FreSoftphone");


            MainWindow.mainwindow.frame1.IsEnabled = true;
            MainWindow.mainwindow.frame1.Opacity = 1;
            MainWindow.mainwindow.frame5.Content = null;
            MainWindow.mainwindow.frame5.Visibility = Visibility.Hidden;
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
