using System.Windows;
using System.Windows.Controls;

namespace JPL_Gateway.View
{
    /// <summary>
    /// Contact_us_Page.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Contact_us_Page : Page
    {
        internal static Contact_us_Page contactpage;

        public Contact_us_Page()
        {
            InitializeComponent();
        }

        private void chat_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.jpltele.com/");
        }

        private void website_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.jpltele.com/");
        }

        private void Contact_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.jpltele.com/contact/");
        }

        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.mainwindow.frame1.Content = Support_Page.supportpage;
        }
    }
}