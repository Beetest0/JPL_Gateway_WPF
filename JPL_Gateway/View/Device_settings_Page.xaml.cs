using System;
using System.Windows;
using System.Windows.Controls;
using System.Threading;

namespace JPL_Gateway.View
{
    /// <summary>
    /// Device_settings_Page.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Device_settings_Page : Page
    {
        internal static Device_settings_Page devicesettingspage;

        private string[] arrKey = new string[7];

        public bool KeytoneToggle;
        public bool RingtoneToggle;
        public bool BeepToggle;
        public bool Beepapply;
        public bool EQToggle;
        public bool eqapply;
        public bool eq_preset_changed;
        public bool Apply_Toggle;
        public bool advsetting_Toggle;
        public bool advsetting_changed;
        //===================== EQ trigger ===========================
        public byte eq_band1;
        public byte eq_band2;
        public byte eq_band3;
        public byte eq_band4;
        public byte eq_band5;
        //===================== EQ1 (OFF) ===========================
        public byte eq1_band1 = 24;
        public byte eq1_band2 = 24;
        public byte eq1_band3 = 24;
        public byte eq1_band4 = 24;
        public byte eq1_band5 = 24;
        //===================== EQ2 (Communication) ========================
        public byte eq2_band1 = 24; //40; // 33;
        public byte eq2_band2 = 24; // 29; //23;
        public byte eq2_band3 = 40; // 34; // 22;
        public byte eq2_band4 = 40; //34; // 22;
        public byte eq2_band5 = 24; //48; // 26;
        //===================== EQ3 (Movie) =================
        public byte eq3_band1 = 40; // 24; // 15;
        public byte eq3_band2 = 29; // 24; // 29;
        public byte eq3_band3 = 34; // 40; // 30;
        public byte eq3_band4 = 34; // 40; //32;
        public byte eq3_band5 = 48; // 24; // 15;
        //===================== EQ4 (Music) ==========================
        public byte eq4_band1 = 37; // 19;
        public byte eq4_band2 = 24; // 25;
        public byte eq4_band3 = 39; // 29;
        public byte eq4_band4 = 45; // 27;
        public byte eq4_band5 = 48; // 33;
        //===========================================================

        public Device_settings_Page()
        {
            InitializeComponent();
            Beep_toggle(false);
        }

        public void Setting_icon1(bool Enable)
        {
            if (Enable)
            {
                EqGrid.Visibility = Visibility.Visible;
                BeepGrid.Visibility = Visibility.Visible;

                Thickness margin = SoftphoneGrid.Margin;
                margin.Top = 475;
                SoftphoneGrid.Margin = margin;
            }
            else
            {
                EqGrid.Visibility = Visibility.Hidden;
                BeepGrid.Visibility = Visibility.Hidden;

                Thickness margin = SoftphoneGrid.Margin;
                margin.Top = 120;
                SoftphoneGrid.Margin = margin;
            }
        }

        public void Disable_eqset()
        {
            fr100.IsEnabled = false;
            fr100.Value = 24;
            fr350.IsEnabled = false;
            fr350.Value = 24;
            fr1000.IsEnabled = false;
            fr1000.Value = 24;
            fr3500.IsEnabled = false;
            fr3500.Value = 24;
            fr13000.IsEnabled = false;
            fr13000.Value = 24;
        }

        public void Enable_eqset()
        {
            fr100.IsEnabled = true;
            fr350.IsEnabled = true;
            fr1000.IsEnabled = true;
            fr3500.IsEnabled = true;
            fr13000.IsEnabled = true;
        }

        public void Set_off()
        {
            fr100.Value = eq1_band1;
            fr350.Value = eq1_band2;
            fr1000.Value = eq1_band3;
            fr3500.Value = eq1_band4;
            fr13000.Value = eq1_band5;
        }

        private void EQ_apply_Click(object sender, RoutedEventArgs e)
        {
            EQ_Apply();
        }

        private void EQ_Apply()
        {
            if (eq_preset.SelectedIndex.Equals(1)) // USER
            {
                byte set = 0x01;
                byte eq1 = (byte)fr100.Value;
                byte eq2 = (byte)fr350.Value;
                byte eq3 = (byte)fr1000.Value;
                byte eq4 = (byte)fr3500.Value;
                byte eq5 = (byte)fr13000.Value;

                MainWindow.mainwindow.Apply_eq_setting(set, eq1, eq2, eq3, eq4, eq5);
            }
            else if (eq_preset.SelectedIndex.Equals(0)) // OFF
            {
                byte set = 0x01;
                MainWindow.mainwindow.Apply_eq_setting(set, eq1_band1, eq1_band2, eq1_band3, eq1_band4, eq1_band5);
            }
            else if (eq_preset.SelectedIndex.Equals(2)) // Communication
            {
                byte set = 0x02;
                MainWindow.mainwindow.Apply_eq_setting(set, eq2_band1, eq2_band2, eq2_band3, eq2_band4, eq2_band5);
            }
            else if (eq_preset.SelectedIndex.Equals(3)) // Movie
            {
                byte set = 0x03;
                MainWindow.mainwindow.Apply_eq_setting(set, eq3_band1, eq3_band2, eq3_band3, eq3_band4, eq3_band5);
            }
            else if (eq_preset.SelectedIndex.Equals(4)) // Music
            {
                byte set = 0x04;
                MainWindow.mainwindow.Apply_eq_setting(set, eq4_band1, eq4_band2, eq4_band3, eq4_band4, eq4_band5);
            }
            EQ_toggle(false);
        }

        private void EQ_Change(object sender, EventArgs e)
        {
            //EQ_toggle(true);
            eq_preset_changed = false;

            if (eq_preset.SelectedIndex.Equals(0)) // Default
            {
                eq_preset_changed = true;

                eq_band1 = eq1_band1;
                eq_band2 = eq1_band2;
                eq_band3 = eq1_band3;
                eq_band4 = eq1_band4;
                eq_band5 = eq1_band5;

                fr100.Value = eq1_band1;
                fr350.Value = eq1_band2;
                fr1000.Value = eq1_band3;
                fr3500.Value = eq1_band4;
                fr13000.Value = eq1_band5;
            }
            else if (eq_preset.SelectedIndex.Equals(2))
            {
                eq_band1 = eq2_band1;
                eq_band2 = eq2_band2;
                eq_band3 = eq2_band3;
                eq_band4 = eq2_band4;
                eq_band5 = eq2_band5;

                fr100.Value = eq2_band1;
                fr350.Value = eq2_band2;
                fr1000.Value = eq2_band3;
                fr3500.Value = eq2_band4;
                fr13000.Value = eq2_band5;
            }
            else if (eq_preset.SelectedIndex.Equals(3))
            {
                eq_band1 = eq3_band1;
                eq_band2 = eq3_band2;
                eq_band3 = eq3_band3;
                eq_band4 = eq3_band4;
                eq_band5 = eq3_band5;

                fr100.Value = eq3_band1;
                fr350.Value = eq3_band2;
                fr1000.Value = eq3_band3;
                fr3500.Value = eq3_band4;
                fr13000.Value = eq3_band5;
            }
            else if (eq_preset.SelectedIndex.Equals(4))
            {
                eq_band1 = eq4_band1;
                eq_band2 = eq4_band2;
                eq_band3 = eq4_band3;
                eq_band4 = eq4_band4;
                eq_band5 = eq4_band5;

                fr100.Value = eq4_band1;
                fr350.Value = eq4_band2;
                fr1000.Value = eq4_band3;
                fr3500.Value = eq4_band4;
                fr13000.Value = eq4_band5;
            }
            eq_preset_changed = true;
        }

        public void EQ_toggle(bool status)
        {
            if (status == true)
            {
                EQ_apply.Style = Application.Current.Resources["Apply_Button"] as Style;
                EQ_apply.IsEnabled = true;
            }
            else if (status == false)
            {
                EQ_apply.Style = Application.Current.Resources["Disable_Button"] as Style;
                EQ_apply.IsEnabled = false;
            }
        }

        private void Beep_apply_Click(object sender, RoutedEventArgs e)
        {
            Beep_Apply();
        }

        private void Beep_Apply()
        {
            byte key = 0;
            byte ring = 0;
            byte mute = 0;

            if (Keytone.IsChecked == false)
                key = 0;
            else if (Keytone.IsChecked == true)
                key = 1;

            if (Ringtone.IsChecked == false)
                ring = 0;
            else if (Ringtone.IsChecked == true)
                ring = 1;

            if (Muteinter.SelectedIndex.Equals(0))
                mute = 0;
            else if (Muteinter.SelectedIndex.Equals(1))
                mute = 1;
            else if (Muteinter.SelectedIndex.Equals(2))
                mute = 2;
            else if (Muteinter.SelectedIndex.Equals(3))
                mute = 3;

            MainWindow.mainwindow.Apply_beep_setting(key, ring, mute);

            Beep_toggle(false);
        }

        private void Beep_Change(object sender, RoutedEventArgs e)
        {
            Beep_toggle(true);
        }

        private void Beep_Change(object sender, EventArgs e)
        {
            Beep_toggle(true);
        }

        private void Beep_toggle(bool status)
        {
            if (status == true)
            {
                Beep_apply.Style = Application.Current.Resources["Apply_Button"] as Style;
                Beep_apply.IsEnabled = true;
            }
            else if (status == false)
            {
                Beep_apply.Style = Application.Current.Resources["Disable_Button"] as Style;
                Beep_apply.IsEnabled = false;
            }
        }

        private void Softphone_toggle(bool status)
        {
            if (status == true)
            {
                Softphone_apply.Style = Application.Current.Resources["Apply_Button"] as Style;
                Softphone_apply.IsEnabled = true;
            }
            else if (status == false)
            {
                Softphone_apply.Style = Application.Current.Resources["Disable_Button"] as Style;
                Softphone_apply.IsEnabled = false;
            }
        }

        private void Reset_settings_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.mainwindow.frame1.Opacity = 0.6;
            MainWindow.mainwindow.frame5.IsEnabled = true;
            MainWindow.mainwindow.frame5.Visibility = Visibility.Visible;
            MainWindow.mainwindow.frame5.Content = MsgBox.Setting_Reset.resetbox;
            MainWindow.mainwindow.frame1.IsEnabled = false;
        }

        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.mainwindow.frame1.Content = Select_device_Page.selectdevicepage;
        }

        private void ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if(EQ_apply != null)
            EQ_toggle(true);
            if (eq_preset_changed == true)
            {
                if (fr100.Value != eq_band1 ||
                    fr350.Value != eq_band2 ||
                    fr1000.Value != eq_band3 ||
                    fr3500.Value != eq_band4 ||
                    fr13000.Value != eq_band5
                    )
                    eq_preset.SelectedIndex = 1;
                eq_preset_changed = false;
            }
        }

        public void Reset_Setting()
        {
            MainWindow.mainwindow.Apply_eq_setting(0x01, eq1_band1, eq1_band2, eq1_band3, eq1_band4, eq1_band5);
            MainWindow.mainwindow.Apply_beep_setting(0x00, 0x00, 0x00);
            Thread.Sleep(200);

            MainWindow.mainwindow.Check_beep_setting(MainWindow.mainwindow.isSelectdevice);
            Thread.Sleep(200);
            MainWindow.mainwindow.Check_eq_setting(MainWindow.mainwindow.isSelectdevice);

            Beep_toggle(false);
            EQ_toggle(false);
        }

        public void Reset_Softphone_Setting()
        {
            CheckOneX.Visibility = Visibility.Hidden;
            CheckOneXComm.Visibility = Visibility.Hidden;
            CheckIXWorkplace.Visibility = Visibility.Hidden;
            CheckJabber.Visibility = Visibility.Hidden;
            CheckBria.Visibility = Visibility.Hidden;
            CheckBria6.Visibility = Visibility.Hidden;
            Check3CX.Visibility = Visibility.Hidden;

            MainWindow.mainwindow.SelectedSoftPhone = "";
            Softphonelist.Text = "";
            Softphone_toggle(false);
        }

        public void Softphone_Apply_Click(object sender, RoutedEventArgs e)
        {
            Softphone_Apply();
        }

        public void Select_softphonelist(int num)
        {
            if (num == 0)
            {
                CheckOneX.Visibility = Visibility.Visible;
                Softphonelist.SelectedIndex = 0;
            }
            else if (num ==1)
            {
                CheckOneXComm.Visibility = Visibility.Visible;
                Softphonelist.SelectedIndex = 1;
            }
            else if (num == 2)
            {
                CheckIXWorkplace.Visibility = Visibility.Visible;
                Softphonelist.SelectedIndex = 2;
            }
            else if (num == 3)
            {
                CheckJabber.Visibility = Visibility.Visible;
                Softphonelist.SelectedIndex = 3;
            }
            else if (num == 4)
            {
                CheckBria.Visibility = Visibility.Visible;
                Softphonelist.SelectedIndex = 4;
            }
            else if (num == 5)
            {
                CheckBria6.Visibility = Visibility.Visible;
                Softphonelist.SelectedIndex = 5;
            }
            else if (num == 6)
            {
                Check3CX.Visibility = Visibility.Visible;
                Softphonelist.SelectedIndex = 6;
            }
        }

        private void Softphone_Apply()
        {
            CheckOneX.Visibility = Visibility.Hidden;
            CheckOneXComm.Visibility = Visibility.Hidden;
            CheckIXWorkplace.Visibility = Visibility.Hidden;
            CheckJabber.Visibility = Visibility.Hidden;
            CheckBria.Visibility = Visibility.Hidden;
            CheckBria6.Visibility = Visibility.Hidden;
            Check3CX.Visibility = Visibility.Hidden;

            if (Softphonelist.SelectedIndex == 0)
            {
                CheckOneX.Visibility = Visibility.Visible;
                MainWindow.mainwindow.Set_State_Softphone("StateOneX");
                FreSoftphone_save("StateOneX");
            }
            else if (Softphonelist.SelectedIndex == 1)
            {
                CheckOneXComm.Visibility = Visibility.Visible;
                MainWindow.mainwindow.Set_State_Softphone("StateOneXComm");
                FreSoftphone_save("StateOneXComm");
            }
            else if (Softphonelist.SelectedIndex == 2)
            {
                CheckIXWorkplace.Visibility = Visibility.Visible;
                MainWindow.mainwindow.Set_State_Softphone("StateWorkplace");
                FreSoftphone_save("StateWorkplace");
            }
            else if (Softphonelist.SelectedIndex == 3)
            {
                CheckJabber.Visibility = Visibility.Visible;
                MainWindow.mainwindow.Set_State_Softphone("StateJabber");
                FreSoftphone_save("StateJabber");
            }
            else if (Softphonelist.SelectedIndex == 4)
            {
                CheckBria.Visibility = Visibility.Visible;
                MainWindow.mainwindow.Set_State_Softphone("StateBria4");
                FreSoftphone_save("StateBria4");
            }
            else if (Softphonelist.SelectedIndex == 5)
            {
                CheckBria6.Visibility = Visibility.Visible;
                MainWindow.mainwindow.Set_State_Softphone("StateBria");
                FreSoftphone_save("StateBria");
            }
            else if (Softphonelist.SelectedIndex == 6)
            {
                Check3CX.Visibility = Visibility.Visible;
                MainWindow.mainwindow.Set_State_Softphone("State3CX");
                FreSoftphone_save("State3CX");
            }

            Softphone_toggle(false);
        }

        private void FreSoftphone_save(string softphone)
        {
            AppConfiguration.SetAppConfig("FreSoftphone", softphone);
        }

        private void Softphonelist_DropDownClosed(object sender, EventArgs e)
        {
            Softphone_toggle(true);
        }

        private void Sava_All_Click(object sender, RoutedEventArgs e)
        {
            if (MainWindow.mainwindow.isSetting_access == true)
            {
                EQ_Apply();
                Beep_Apply();
            }
            Softphone_Apply();
        }
    }
}