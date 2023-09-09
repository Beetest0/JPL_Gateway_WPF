using System;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using System.Reflection;

namespace JPL_Gateway.View
{
    /// <summary>
    /// SplashScreen_Page.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class SplashScreen_Page : Page
    {
        internal static SplashScreen_Page splash;
        AxWMPLib.AxWindowsMediaPlayer player = null;

        public SplashScreen_Page()
        {
            InitializeComponent();
            InitMediaPlayer();
        }

        void InitMediaPlayer()
        {
            player = formsHost.Child as AxWMPLib.AxWindowsMediaPlayer;
            player.uiMode = "none";
            player.settings.setMode("loop", false);
            player.stretchToFit = true;
            player.enableContextMenu = false;
            player.PlayStateChange += new AxWMPLib._WMPOCXEvents_PlayStateChangeEventHandler(player_PlayStateChange);
        }

        void player_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            if (e.newState == (int)WMPLib.WMPPlayState.wmppsMediaEnded)
            {
                MainWindow.mainwindow.frame4.Visibility = Visibility.Hidden;
                MainWindow.mainwindow.frame4.Content = null;
                splash = null;
            }
        }

        private string GetFileSize(double byteCount)
        {
            string size = "0 Bytes";
            if (byteCount >= 1073741824.0)
                size = String.Format("{0:##.##}", byteCount / 1073741824.0) + " GB";
            else if (byteCount >= 1048576.0)
                size = String.Format("{0:##.##}", byteCount / 1048576.0) + " MB";
            else if (byteCount >= 1024.0)
                size = String.Format("{0:##.##}", byteCount / 1024.0) + " KB";
            else if (byteCount > 0 && byteCount < 1024.0)
                size = byteCount.ToString() + " Bytes";

            return size;
        }



        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var path = Path.GetTempPath() + "JPL.mp4";
            FileInfo fileInfo = new FileInfo(path);
            var resourceName = "JPL_Gateway.Resources.JPL_Opening.mp4";

            if (!fileInfo.Exists)
            {
                using (var fstream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
                {
                    var ext = resourceName.Substring(resourceName.LastIndexOf("."));
                    var pathfile = Path.GetTempPath() + "JPL" + ext;
                    using (FileStream outputFileStream = new FileStream(pathfile, FileMode.Create))
                    {
                        fstream.CopyTo(outputFileStream);
                    }
                    player.URL = pathfile;
                }
            }
            else
            {
                string fileSize = GetFileSize(fileInfo.Length);
                string resourceSize = GetFileSize(resourceName.Length);
                if (fileSize != resourceSize)
                {
                    using (var fstream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
                    {
                        var ext = resourceName.Substring(resourceName.LastIndexOf("."));
                        var pathfile = Path.GetTempPath() + "JPL" + ext;
                        using (FileStream outputFileStream = new FileStream(pathfile, FileMode.Create))
                        {
                            fstream.CopyTo(outputFileStream);
                        }
                        player.URL = pathfile;
                    }
                }
            }
            player.URL = path;
        }
    }
}
