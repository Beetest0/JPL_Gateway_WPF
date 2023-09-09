using System;
using System.Windows;
using System.Windows.Controls;
using System.Collections;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Threading;

namespace JPL_Gateway.View
{
    /// <summary>
    /// Connected_Page.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Connected_Page : Page
    {
        internal static Connected_Page conpage;

        public Page selectdevicepage = new Page();

        internal delegate void updateUsbStatusCallback(string devicename, string devicepid, ArrayList arrlist);
        internal delegate void updateSoftPhoneStatusCallback(bool hook, bool mute);

        internal delegate void sendCommandDele(string text);
        //internal event sendCommandDele sendCommand;


        internal delegate void sendclickDele(string softphone);
        //internal event sendclickDele sendclick;

        //private Dictionary<string, SoftphoneItem> mDicSoftphone = new Dictionary<string, SoftphoneItem>();
        private ArrayList mArrSoftphoneList = new ArrayList();
        private string[] arrTitle = new string[7];
        private string[] arrKey = new string[7];

        //private string lync;
        //private bool isShowSelectLync = false;

        //private FormLync frmlync;

        //private string mBinFileName;


        //private int SelectedCount = 0;
        //private int device_num;

        //private bool isSkype8;

        // ============================================
        public Connected_Page()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
        }

        public void Device_num(int devices_num)
        {
            if (devices_num == 1)
            {
                Device1.Margin = new Thickness(310, 130, 0, 0);
                Device1.Visibility = Visibility.Visible;
                Device2.Visibility = Visibility.Hidden;
            }
            else if (devices_num == 2)
            {
                Device1.Margin = new Thickness(110, 130, 0, 0);
                Device2.Margin = new Thickness(510, 130, 0, 0);
                Device1.Visibility = Visibility.Visible;
                Device2.Visibility = Visibility.Visible;
            }
            else if (devices_num == 3)
            {

            }
            else if (devices_num == 4)
            {

            }
            else if (devices_num == 5)
            {

            }
        }

        public void UsbStatusUpdate(string devicename, string devicevid, string devicepid, string version, int device_num, bool select_device, ArrayList arrlist)
        {
            string data = "";
            string sku = "";
            string image_path = "";

            if (devicename.Length > 0)
            {
                if (devicename.Contains("BL-052"))
                {
                    image_path = "/Resources/Device/DSU-08M.png";
                }
                else if (devicename.Contains("BL-054"))
                {
                    image_path = "/Resources/Device/DSU-11MBL-054MS.png";
                }
                //=============================================================================
                else if (devicename.Equals("BL-05MS"))
                {
                    image_path = "/Resources/Device/BL-05.png";
                }
                else if (devicename.Equals("JPL-400M"))
                {
                    image_path = "/Resources/Device/JPL400M.png";
                }
                else if (devicename.Equals("JPL-400B"))
                {
                    image_path = "/Resources/Device/JPL400B.png";
                }
                else if (devicename.Equals("JPL-400-USB"))
                {
                    image_path = "/Resources/Device/JPL-400-USB.png";
                }
                //=============================================================================
                else if (devicename.Equals("BL-053"))
                {
                    image_path = "/Resources/Device/BL-053.png";
                }
                else if (devicename.Equals("BL-05"))
                {
                    image_path = "/Resources/Device/JPL-611-IB.png";
                }
                else if (devicename.Equals("BL-053C+P"))
                {
                    image_path = "/Resources/Device/JPL-611-IB.png";
                }
                //=============================================================================
                else if (devicename.Equals("BL-055"))
                {
                    image_path = "/Resources/Device/BL-055.png";
                }
                else if (devicename.Equals("BL-056"))
                {
                    image_path = "/Resources/Device/BL-056.png";
                }
                else if (devicename.Equals("JPL-Connect-2"))
                {
                    image_path = "/Resources/Device/JPL-Connect-2.png";
                }
                //=============================================================================
                else if (devicename.Equals("X-400"))
                {
                    image_path = "/Resources/Device/Lync_new.png";
                }
                //=============================================================================
                else if (devicename.Equals("X-450"))
                {
                    image_path = "/Resources/Device/DW780.png";
                    data = "DECT Wireless Headset";
                }
                else if (devicename.Equals("JPL X450"))
                {
                    image_path = "/Resources/Device/X450.png";
                }
                //X-500 ====================================================================
                else if (devicename.Equals("X-500U"))
                {
                    image_path = "/Resources/Device/X500_Base_with_USB_Module.png";
                    data = "DECT Wireless Headset";
                }
                else if (devicename.Equals("X-500U-Jabber"))
                {
                    image_path = "/Resources/Device/X500_Base_with_USB_Module.png";
                    data = "DECT Wireless Headset";
                }
                //JPL-Explore ====================================================================
                else if (devicename.Contains("JPL-Explore"))
                {
                    image_path = "/Resources/Device/JPL-Explore.png";
                    data = "DECT Wireless Headset";
                    sku = "575-385-005";
                }
                //=============================================================================
                else if (devicename.Equals("JPL Companion"))
                {
                    image_path = "/Resources/Device/VoicePro-575.png";
                }
                //EHS Cable ====================================================================
                else if (devicename.Equals("EHS-CI-01"))
                {
                    image_path = "/Resources/Device/EHS-USB.png";
                }
                else if (devicename.Equals("EHS-SN-00"))
                {
                    image_path = "/Resources/Device/EHS-USB.png";
                }
                //Old BT Dongle =================================================================
                else if (devicename.Equals("BT-200U"))
                {
                    image_path = "/Resources/Device/BT200.png";
                    data = "Bluetooth USB Dongle";
                }
                //BT Dongle ====================================================================
                else if (devicename.Equals("BT-220"))
                {
                    image_path = "/Resources/Device/BT-220.png";
                    data = "Bluetooth USB Dongle";
                    sku = "575-341-001";
                }
                else if (devicename.Equals("BTD-1000"))
                {
                    image_path = "/Resources/Device/BT-220.png";
                    data = "Bluetooth USB Dongle";
                    sku = "575-341-001";
                }
                //BT Headset ===================================================================
                else if (devicename.Equals("BT-500D"))
                {
                    image_path = "/Resources/Device/BT500D.png";
                    data = "Bluetooth Wireless Headset";
                    sku = "575-342-001";
                }
                //=============================================================================
                else if (devicename.Equals("BL-PTT-USB"))
                {
                    image_path = "/Resources/Device/BT500D.png";
                    data = "USB PTT";
                }
                //=============================================================================
                else if (devicename.Contains("Scout"))
                {
                    image_path = "/Resources/Device/JPL-Scout.png";
                    data = "USB PTT";
                    sku = "575-406-200";
                }
                //=============================================================================
                else if (devicename.Contains("COMMANDER"))
                {
                    image_path = "/Resources/Device/Commander.png";
                    data = "Professional Office / Contact Centre Headset";
                    sku = "575-344-004";
                }
                //=============================================================================
                else
                {
                    image_path = "/Resources/Device/No_Image.png";
                }
            }

            if (select_device == false)
            {
                if (device_num == 0)
                {
                    Conn_DeviceModel1.Content = new BitmapImage(new Uri(image_path, UriKind.Relative));
                    DeviceName1.Text = devicename;
                    DeviceData1.Text = data;
                    SKU1.Text = sku;
                }

                else if (device_num == 1)
                {
                    Conn_DeviceModel2.Content = new BitmapImage(new Uri(image_path, UriKind.Relative));
                    DeviceName2.Text = devicename;
                    DeviceData2.Text = data;
                    SKU2.Text = sku;
                }
            }
            // --------------------------------------------------------------------------------------------------------------------------------------- //
            // Select_device_Page
            else if (select_device == true)
            {
                //Select_device_Page.selectdevicepage.DeviceName.Text = "JPL_ABCDEFGHIJKLMNOPQR###############################";
                Select_device_Page.selectdevicepage.DeviceName.Text = devicename;
                Select_device_Page.selectdevicepage.DeviceData.Text = data;
                Select_device_Page.selectdevicepage.SKU.Text = sku;
                // --------------------------------------------------------------------------------------------------------------------------------------- //
                // Device_update_Page
                Device_update_Page.deviceupdatepage.DeviceName.Text = devicename;
                Device_update_Page.deviceupdatepage.currentversion.Text = version;
                Device_update_Page.deviceupdatepage.Device_image.Source = new BitmapImage(new Uri(image_path, UriKind.Relative));
                // --------------------------------------------------------------------------------------------------------------------------------------- //
                // Device_settings_Page
                Device_settings_Page.devicesettingspage.DeviceName.Text = devicename;
                // --------------------------------------------------------------------------------------------------------------------------------------- //
                Device_update_Page.deviceupdatepage.setConnectedDevice(devicename, devicevid, devicepid, version);
                Device_update_Page.deviceupdatepage.CheckComboBox();
            }
        }

        // --------------------------------------------------------------------------------------------------------------------------------------- //
        // 이미지 이동 함수
        public static void MoveTo(System.Windows.Controls.Image target, double newX, double newY)
        {
            Vector offset = VisualTreeHelper.GetOffset(target);
            var top = offset.Y;
            var left = offset.X;
            TranslateTransform trans = new TranslateTransform();
            target.RenderTransform = trans;
            DoubleAnimation anim1 = new DoubleAnimation(0, newY - top, TimeSpan.FromSeconds(0.5));
            DoubleAnimation anim2 = new DoubleAnimation(0, newX - left, TimeSpan.FromSeconds(0.5));
            trans.BeginAnimation(TranslateTransform.YProperty, anim1);
            trans.BeginAnimation(TranslateTransform.XProperty, anim2);
        }
        // --------------------------------------------------------------------------------------------------------------------------------------- //

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            /*
            if (device_num == 1)
            {
                MoveTo(device_image, 0, 0);
                device_num = 2;
            }
            else if (device_num ==2)
            {
                MoveTo(device_image, 600, 0);
                device_num = 3;
            }
            else if (device_num == 3)
            {
                MoveTo(device_image, 0, 600);
                device_num = 4;
            }
            else if (device_num == 4)
            {
                MoveTo(device_image, 600, 600);
                device_num = 1;
            }
            */
        }

        private void Device_Select(int device_num)
        {
            MainWindow.mainwindow.isSelectdevice = device_num;
            /*
            MainWindow.mainwindow.isSetting_access = false;
            //Select_device_Page.selectdevicepage.Setting_icon(false);
            if (MainWindow.mainwindow.isSetting_access == false)
            {
                MainWindow.mainwindow.Check_beep_setting(device_num);
                Thread.Sleep(200);
                MainWindow.mainwindow.Check_eq_setting(device_num);
            }
            */
            UsbStatusUpdate(MainWindow.mainwindow.devices[device_num].product,
                                            MainWindow.mainwindow.devices[device_num].VID.ToString("X4"),
                                            MainWindow.mainwindow.devices[device_num].PID.ToString("X4"),
                                            MainWindow.mainwindow.devices[device_num].versionNumber.ToString("X4"), 0, true, null);
            Device_update_Page.deviceupdatepage.checkUpdateVersion(device_num);
        }

        private void Conn_DeviceModel1_Click(object sender, RoutedEventArgs e)
        {
            Device_Select(0);
            MainWindow.mainwindow.frame1.Content = Select_device_Page.selectdevicepage;
        }

        private void Conn_DeviceModel2_Click(object sender, RoutedEventArgs e)
        {
            Device_Select(1);
            MainWindow.mainwindow.frame1.Content = Select_device_Page.selectdevicepage;
        }

        private void Conn_update1_Click(object sender, RoutedEventArgs e)
        {
            Device_Select(0);
            MainWindow.mainwindow.frame1.Content = Device_update_Page.deviceupdatepage;
        }

        private void Conn_update2_Click(object sender, RoutedEventArgs e)
        {
            Device_Select(1);
            MainWindow.mainwindow.frame1.Content = Device_update_Page.deviceupdatepage;
        }
    }
}
