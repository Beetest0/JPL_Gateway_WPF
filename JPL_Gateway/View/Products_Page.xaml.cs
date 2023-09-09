using System.Windows.Controls;

namespace JPL_Gateway.View
{
    /// <summary>
    /// Products_Page.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Products_Page : Page
    {
        internal static Products_Page products;

        public Products_Page()
        {
            InitializeComponent();
        }

        private void wired_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.jpltele.com/products/wired-headsets/");
        }

        private void wireless_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.jpltele.com/products/wireless-headsets/");
        }

        private void conference_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.jpltele.com/products/video-conferencing/");
        }

        private void spkphone_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.jpltele.com/products/speakerphones/");
        }

        private void accessorie_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.jpltele.com/products/accessories/");
        }

        private void lead_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.jpltele.com/products/bottom-leads/");
        }

        private void av_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.jpltele.com/products/av-cables/");
        }
    }
}
