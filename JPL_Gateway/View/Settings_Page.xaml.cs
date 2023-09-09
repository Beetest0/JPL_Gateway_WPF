using System;
using System.Windows;
using System.Windows.Controls;
using System.Diagnostics;
using System.IO;
using System.Globalization;
using Newtonsoft.Json.Linq;
using Microsoft.Win32;
using JPL_Gateway.Cultures;

namespace JPL_Gateway.View
{
    /// <summary>
    /// Settings_Page.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Settings_Page : Page
    {
        internal static Settings_Page settingpage;

        private string msg1;
        private string msg2;
        private string updatesetupexe;

        private JObject[] jsonArr;

        public Settings_Page()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            CurrentVersion.Content = MainWindow.mainwindow.mSoftLinkVersion;
            lbNoti01.Visibility = Visibility.Hidden;
            lbNoti02.Visibility = Visibility.Hidden;
            noti.Visibility = Visibility.Hidden;
            chrome_grid.Visibility = Visibility.Hidden; //Chrome Extension Update UI 옵션
        }

        public void setArrJson(JObject[] arrjson)
        {
            this.jsonArr = arrjson;

            if (jsonArr[1] != null)
            {
                //Console.WriteLine("Softlink = {0}", jsonArr[1]["update"].ToString());
                if (jsonArr[1]["update"].ToString().Equals("True"))
                {
                    gateway_update.Style = Resources["Update_Now"] as Style;
                }
                else
                {
                    gateway_update.Style = Resources["Up_to_date"] as Style;
                }
            }
        }

        public void setUpdatesetupexe(string filename)
        {
            if (filename != null)
            {
                if (filename.Length > 0)
                {
                    updatesetupexe = filename;
                }
                else
                {
                }
            }
            else
            {
            }
        }

        public void setUpdateInfo(int idx, string msg)
        {
            if (idx == 0)
                msg1 = msg;
            else if (idx == 1)
                msg2 = msg;

            MsgBox.SW_Upgrade.swupgradebox.updateinfo.Text = msg;
        }

        private void gateway_update_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.mainwindow.frame1.Opacity = 0.6;
            MainWindow.mainwindow.frame5.IsEnabled = true;
            MainWindow.mainwindow.frame5.Visibility = Visibility.Visible;
            MainWindow.mainwindow.frame5.Content = MsgBox.SW_Upgrade.swupgradebox;
            MainWindow.mainwindow.frame1.IsEnabled = false;
        }

        public void Setup_upgrade()
        {
            Console.WriteLine(updatesetupexe);
            Process SoftlinkDownloader = new Process();
            SoftlinkDownloader.StartInfo.FileName = Path.GetTempPath() + "\\" + updatesetupexe;
            if (updatesetupexe != null)
            {
                if (updatesetupexe.Length > 0)
                {
                    SoftlinkDownloader.Start();
                }
            }
        }

        private void startup_Checked(object sender, RoutedEventArgs e)
        {
            if (startup.IsChecked == true)
            {
                var path = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";
                //var local = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\JPL_Gateway\JPL_Gateway.exe";
                var local = System.Windows.Forms.Application.ExecutablePath.ToString();
                RegistryKey key = Registry.CurrentUser.OpenSubKey(path, true);
                key.SetValue("JPL_Gateway", local);
                //key.SetValue("JPL_Gateway", System.Windows.Forms.Application.ExecutablePath.ToString());
            }
            else if (startup.IsChecked == false)
            {
                var path = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";
                RegistryKey key = Registry.CurrentUser.OpenSubKey(path, true);
                key.DeleteValue("JPL_Gateway", false);
            }
        }

        public void Set_Lang()
        {
            if (AppConfiguration.GetAppConfig("language").Equals("en-GB"))
                combobox_language.SelectedIndex = 0;
            else if (AppConfiguration.GetAppConfig("language").Equals("es-ES"))
                combobox_language.SelectedIndex = 1;
            else if (AppConfiguration.GetAppConfig("language").Equals("fr-FR"))
                combobox_language.SelectedIndex = 2;
            else if (AppConfiguration.GetAppConfig("language").Equals("de-DE"))
                combobox_language.SelectedIndex = 3;
        }

        public void Set_Country()
        {
            if (AppConfiguration.GetAppConfig("contry").Equals("UK"))
                combobox_country.SelectedIndex = 0;
            else if (AppConfiguration.GetAppConfig("contry").Equals("ES"))
                combobox_country.SelectedIndex = 1;
            else if (AppConfiguration.GetAppConfig("contry").Equals("FR"))
                combobox_country.SelectedIndex = 2;
            else if (AppConfiguration.GetAppConfig("contry").Equals("DE"))
                combobox_country.SelectedIndex = 3;
        }

        public void Save_setting_config(string set ,string lang)
        {
            // Language Change
            if (set.Equals("language"))
            {
                AppConfiguration.SetAppConfig("language", lang);
                CultureInfo language = new CultureInfo(lang);
                CultureResources.ChangeCulture(language);
                //Settings.Default.DefaultCulture = language;
                //Settings.Default.Save();
                MainWindow.mainwindow.Reflash_context();
            }
            // Contry Change
            else if (set.Equals("contry"))
            {
                AppConfiguration.SetAppConfig("contry", lang);
            }
        }

        private void combobox_language_DropDownClosed(object sender, EventArgs e)
        {
            //Console.WriteLine("DropDownClosed Value is : " + combobox_language.SelectedValue);
            //Console.WriteLine("DropDownClosed Item is : " + combobox_language.SelectedItem);
            //Console.WriteLine("DropDownClosed index is : " + combobox_language.SelectedIndex);

            if (combobox_language.SelectedIndex == 0)
            {
                Save_setting_config("language", "en-GB");
                Console.WriteLine("Selected language : en-GB " + "(" + combobox_language.SelectedIndex + ")");
            }
            else if (combobox_language.SelectedIndex == 1)
            {
                Save_setting_config("language", "es-ES");
                Console.WriteLine("Selected language : es-ES " + "(" + combobox_language.SelectedIndex + ")");
            }
            else if (combobox_language.SelectedIndex == 2)
            {
                Save_setting_config("language", "fr-FR");
                Console.WriteLine("Selected language : fr-FR " + "(" + combobox_language.SelectedIndex + ")");
            }
            else if (combobox_language.SelectedIndex == 3)
            {
                Save_setting_config("language", "de-DE");
                Console.WriteLine("Selected language : de-DE " + "(" + combobox_language.SelectedIndex + ")");
            }
        }

        private void combobox_country_DropDownClosed(object sender, EventArgs e)
        {
            if (combobox_country.SelectedIndex == 0)
            {
                Save_setting_config("contry", "UK");
                Console.WriteLine("Selected Contry : UK " + "(" + combobox_country.SelectedIndex + ")");
            }
            else if (combobox_country.SelectedIndex == 1)
            {
                Save_setting_config("contry", "ES");
                Console.WriteLine("Selected Contry : ES " + "(" + combobox_country.SelectedIndex + ")");
            }
            else if (combobox_country.SelectedIndex == 2)
            {
                Save_setting_config("contry", "FR");
                Console.WriteLine("Selected Contry : FR " + "(" + combobox_country.SelectedIndex + ")");
            }
            else if (combobox_country.SelectedIndex == 3)
            {
                Save_setting_config("contry", "GE");
                Console.WriteLine("Selected Contry : GE " + "(" + combobox_country.SelectedIndex + ")");
            }
        }
    }
}
