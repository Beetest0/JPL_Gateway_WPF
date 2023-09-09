using System;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace JPL_Gateway.View
{
    /// <summary>
    /// Support_Page.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Support_Page : Page
    {
        internal static Support_Page supportpage;
        public Support_Page()
        {
            InitializeComponent();
        }

        private void Troubleshooting_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MainWindow.mainwindow.frame1.Navigate(new Uri("/View/Trouble_Page.xaml", UriKind.Relative));
        }

        private void FAQ_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.jpltele.com/support/faqs/");
        }

        private void Compatibility_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.jpltele.com/compatibility-tool/");
        }

        private void Where_to_buy_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.jpltele.com/resources/where-to-buy/");
        }

        private void Contact_Us_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MainWindow.mainwindow.frame1.Navigate(new Uri("/View/Contact_us_Page.xaml", UriKind.Relative));
        }

        private void Warranty_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.jpltele.com/warranty-terms-conditions/");
        }
    }
}
