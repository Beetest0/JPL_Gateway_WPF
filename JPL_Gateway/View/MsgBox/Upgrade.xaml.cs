using System;
using System.Windows;
using System.Windows.Controls;

namespace JPL_Gateway.View.MsgBox
{
    /// <summary>
    /// Upgrade.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Upgrade : Page
    {
        internal static Upgrade upgrade;

        public Upgrade()
        {
            InitializeComponent();
        }

        public void upgrage_state(bool state)
        {
            if (state)
            {
                loading.Visibility = Visibility.Hidden;
                finish.Visibility = Visibility.Visible;
                updatemsg.Text = Cultures.Resources.StrPopup_update08;
            }
            else
            {
                loading.Visibility = Visibility.Visible;
                finish.Visibility = Visibility.Hidden;
                updatemsg.Text = Cultures.Resources.StrPopup_update07;
            }
        }

        private void okBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.mainwindow.frame1.IsEnabled = true;
            MainWindow.mainwindow.frame1.Opacity = 1;
            MainWindow.mainwindow.frame5.Content = null;
            MainWindow.mainwindow.frame5.Visibility = Visibility.Hidden;
        }
    }
}
