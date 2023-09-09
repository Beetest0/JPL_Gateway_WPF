using System;
using System.Linq;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using System.Threading;
using Newtonsoft.Json.Linq;
using System.Collections;
using HIDInterface;
using System.Windows.Navigation;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Diagnostics;
using FreeMateSoftPhone;
using JPL_Gateway.Cultures;
using System.Reflection;
using System.Net;
using System.Globalization;

namespace JPL_Gateway
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public enum DWMWINDOWATTRIBUTE
        {
            DWMWA_WINDOW_CORNER_PREFERENCE = 33
        }

        public enum DWM_WINDOW_CORNER_PREFERENCE
        {
            DWMWCP_DEFAULT = 0,
            DWMWCP_DONOTROUND = 1,
            DWMWCP_ROUND = 2,
            DWMWCP_ROUNDSMALL = 3
        }

        [DllImport("dwmapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern long DwmSetWindowAttribute(IntPtr hwnd,
                                                         DWMWINDOWATTRIBUTE attribute,
                                                         ref DWM_WINDOW_CORNER_PREFERENCE pvAttribute,
                                                         uint cbAttribute);

        private Point startPos;
        Screen[] screens = Screen.AllScreens;

        internal delegate void updateGUIDele(string devicename, string deviceversion);
        internal delegate void updateHookDele(bool hook);

        internal HIDDevice.interfaceDetails[] devices;
        HIDDevice device0;
        HIDDevice device1;
        public bool isRemoved;
        public int isSelectdevice;

        internal static MainWindow mainwindow;
        NotifyIcon ni;

        // ============================================
        public Page conpage = new Page();
        public Page disconpage = new Page();
        public Page supportpage = new Page();
        public Page settingspage = new Page();
        public Page selectdevicepage = new Page();
        public Page deviceupdatepage = new Page();
        public Page contactpage = new Page();
        public Page devicesettingspage = new Page();
        public Page testpage = new Page();
        public Page resetbox = new Page();
        public Page swupgradebox = new Page();
        public Page fwupgradebox = new Page();
        public Page products = new Page();
        public Page splash = new Page();
        public Page upgrade = new Page();
        public Page mail = new Page();
        public Page warning = new Page();
        public Page trouble = new Page();
        // ============================================

        public bool Devicebtn_status;
        public bool Supportbtn_status;
        public bool Settingsbtn_status;

        // ============================================
        // ============================================
        public string SelectedSoftPhone = "";

        private int activeSoftphoneCount = 0;
        // ============================================
        private Softphoneitem softphoneitemSkype;
        private Softphoneitem softphoneitemTeams;
        private Softphoneitem softphoneitemOneX;
        private Softphoneitem softphoneitemOneXComm;
        private Softphoneitem softphoneitemIXWorkplace;
        private Softphoneitem softphoneitem3CX;
        private Softphoneitem softphoneitemJabber;
        private Softphoneitem softphoneitemBria;
        private Softphoneitem softphoneitemBria6;
        private Softphoneitem softphoneitemMitel;

        private ArrayList arrSoftPhone = new ArrayList();
        // ============================================

        private bool isComm;
        private OneXManager mOneXManager;
        private OneXCommManager mOneXCommManager;
        private IXWorkplace mIXWorkplace;
        private JabberManager mJabberManager;
        private BriaManager mBriaManager;
        private Bria6Manager mBria6Manager;
        private _3CXManager m_3CXManager;
        private SkypeManager mSkypeManager;
        private MitelManager mMitelManager;
        private bool skypeconnectflag = true;
        private bool isSkype8 = false;
        // ============================================

        public string lync;

        private string Pre_CallState = "";
        private string CallState = "";
        private Boolean Ring;
        private Boolean Mute;
        private Boolean HookOff;

        private int keypressedcount = 0;
        private byte lastHookCheckByte = 0;

        private Boolean isMuteKeyPressed = false;
        private Boolean hookoffcallback = false;

        private bool isAddedCalled = false;
        private bool isRemovedCalled = false;
        private bool isSetting_enable = false;
        public bool isSetting_access = false;

        private JObject json;
        private JObject[] jsonArr;
        private string mBinFileName = "";
        public string mSoftLinkVersion = "1.0.4";  // swyoon not use this field refer to AssemblyInfo.cs
        public static string SoftLinkPath = "JPL_Gateway";
        public static int mUpdateCount = 0;
        public static bool BTisUpgrading;

        private bool startup_softphone;

        private int mRemoveAfter3CXCount = 0;

        DispatcherTimer timer = new DispatcherTimer();
        DispatcherTimer timer_Keypress = new DispatcherTimer();

        // ============================================
        // ============================================

        public MainWindow()
        {
            //CultureResources.ResourceProvider.DataChanged += new EventHandler(ResourceProvider_DataChanged);
            //Debug.WriteLine(string.Format("Set culture to default [{0}]:", Properties.Settings.Default.DefaultCulture));

            InitializeComponent();
            string lang = AppConfiguration.GetAppConfig("language");
            CultureInfo ci = new CultureInfo(lang);
            CultureResources.ChangeCulture(ci);

            //=====================================================================
            // Avaya One-X Agent
            // Avaya One-X Communicator
            // Cisco Jabber
            // CounterPath Bria
            // Bria6.x.x
            // 3CXPhone for Windows
            // Skype
            // Teams : actually not supported. It is only for blocking softlink when Teams is activated.
            //=====================================================================
            softphoneitemOneX = new Softphoneitem("OneXAgentUI", "Avaya One-X Agent");
            softphoneitemOneXComm = new Softphoneitem("OneXComm", "Avaya One-X Communicator");
            softphoneitemIXWorkplace = new Softphoneitem("IXWorkplace", "AVaya IX Workplace");
            softphoneitem3CX = new Softphoneitem("3CXWin8Phone", "3CXPhone for Windows");
            softphoneitemJabber = new Softphoneitem("CiscoJabber", "Cisco Jabber");
            softphoneitemBria = new Softphoneitem("Bria4", "CounterPath Bria");
            softphoneitemBria6 = new Softphoneitem("Bria", "Bria");
            softphoneitemMitel = new Softphoneitem("Mitel", "MiCollab Client");
            softphoneitemSkype = new Softphoneitem("Skype", "Skype");
            softphoneitemTeams = new Softphoneitem("Teams", "Microsoft Teams");
            //=====================================================================
            arrSoftPhone.Add(softphoneitemOneX);
            arrSoftPhone.Add(softphoneitemOneXComm);
            arrSoftPhone.Add(softphoneitemIXWorkplace);
            arrSoftPhone.Add(softphoneitem3CX);
            arrSoftPhone.Add(softphoneitemJabber);
            arrSoftPhone.Add(softphoneitemBria);
            arrSoftPhone.Add(softphoneitemBria6);
            arrSoftPhone.Add(softphoneitemSkype);
            arrSoftPhone.Add(softphoneitemTeams);
            arrSoftPhone.Add(softphoneitemMitel);
            //==============================================

            mainwindow = this;

            isRemoved = true;

            IntPtr hWnd = new WindowInteropHelper(GetWindow(this)).EnsureHandle();
            var attribute = DWMWINDOWATTRIBUTE.DWMWA_WINDOW_CORNER_PREFERENCE;
            var preference = DWM_WINDOW_CORNER_PREFERENCE.DWMWCP_ROUND;
            DwmSetWindowAttribute(hWnd, attribute, ref preference, sizeof(uint));

            View.Connected_Page.conpage = new View.Connected_Page();
            View.Nodevice_Page.disconpage = new View.Nodevice_Page();
            View.Support_Page.supportpage = new View.Support_Page();
            View.Settings_Page.settingpage = new View.Settings_Page();
            View.Select_device_Page.selectdevicepage = new View.Select_device_Page();
            View.Device_update_Page.deviceupdatepage = new View.Device_update_Page();
            View.Contact_us_Page.contactpage = new View.Contact_us_Page();
            View.Device_settings_Page.devicesettingspage = new View.Device_settings_Page();
            View.Updates_Page.testpage = new View.Updates_Page();
            View.MsgBox.Setting_Reset.resetbox = new View.MsgBox.Setting_Reset();
            View.MsgBox.SW_Upgrade.swupgradebox = new View.MsgBox.SW_Upgrade();
            View.MsgBox.FW_Upgrade.fwupgradebox = new View.MsgBox.FW_Upgrade();
            View.MsgBox.Upgrade.upgrade = new View.MsgBox.Upgrade();
            View.MsgBox.Mail.mail = new View.MsgBox.Mail();
            View.MsgBox.Warning_Msg.warning = new View.MsgBox.Warning_Msg();
            View.Products_Page.products = new View.Products_Page();
            View.SplashScreen_Page.splash = new View.SplashScreen_Page();
            View.Trouble_Page.trouble = new View.Trouble_Page();

            SetNotification();

            Devicebtn_status = false;
            Supportbtn_status = false;
            Settingsbtn_status = false;
            BTisUpgrading = false;
            startup_softphone = false;

            lync = "0";
            jsonArr = new JObject[2];

            Mute = false;
            HookOff = false;

            CallState = "";
            Pre_CallState = "";

            View.Device_update_Page.deviceupdatepage.setBinFile(mBinFileName);

            Check_startup();
            SW_updatecheck();
            Usb_DeviceAdded();

            View.Settings_Page.settingpage.Set_Lang();
            View.Settings_Page.settingpage.Set_Country();

            frame4.Visibility = Visibility.Visible;
            frame4.Content = View.SplashScreen_Page.splash;

            timer_Keypress.Tick += new EventHandler(timerKeyPressCheck);
            timer_Keypress.Interval = TimeSpan.FromMilliseconds(500);

            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = TimeSpan.FromMilliseconds(2000);
            timer.Start();
        }

        [DllImport("user32.dll")]
        static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);
        [DllImport("user32.dll")]
        static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);
        [DllImport("user32.dll")]
        static extern int TrackPopupMenu(IntPtr hMenu, uint uFlags, int x, int y, int nReserved, IntPtr hWnd, IntPtr prcRect);

        #region App Window Control
        void HandleNavigating(object sender, NavigatingCancelEventArgs e)
        {
            if (e.NavigationMode == NavigationMode.Forward)
            {
                e.Cancel = true;
            }
            else if (e.NavigationMode == NavigationMode.Back)
            {
                e.Cancel = true;
            }
        }

        private void ShowMe()
        {
            this.Show();
            Console.WriteLine(WindowState);
            this.WindowState = WindowState.Normal;
            Console.WriteLine(WindowState);
            this.Topmost = true;
            this.Topmost = false;
            /*
            if (WindowState == WindowState.Minimized)
            {
                WindowState = WindowState.Normal;
            }
            */
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        private void Window_LocationChanged(object sender, EventArgs e)
        {
            int sum = 0;
            foreach (var item in screens)
            {
                sum += item.WorkingArea.Width;
                if (sum >= this.Left + this.Width / 2)
                {
                    this.MaxHeight = item.WorkingArea.Height;
                    break;
                }
            }
        }

        private void System_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                startPos = e.GetPosition(null);
                //==============================================================================================
                //더블 클릭 시 화면 최대화
                /*
                if (e.ClickCount >= 2)
                {
                    this.WindowState = (this.WindowState == WindowState.Normal) ? WindowState.Maximized : WindowState.Normal;
                }
                else
                {
                    startPos = e.GetPosition(null);
                }
                */
                //==============================================================================================
            }
            else if (e.ChangedButton == MouseButton.Right)
            {
                var pos = PointToScreen(e.GetPosition(this));
                IntPtr hWnd = new System.Windows.Interop.WindowInteropHelper(this).Handle;
                IntPtr hMenu = GetSystemMenu(hWnd, false);
                int cmd = TrackPopupMenu(hMenu, 0x100, (int)pos.X, (int)pos.Y, 0, hWnd, IntPtr.Zero);
                if (cmd > 0) SendMessage(hWnd, 0x112, (IntPtr)cmd, IntPtr.Zero);
            }
        }

        private void System_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (this.WindowState == WindowState.Maximized && Math.Abs(startPos.Y - e.GetPosition(null).Y) > 2)
                {
                    var point = PointToScreen(e.GetPosition(null));

                    this.WindowState = WindowState.Normal;

                    this.Left = point.X - this.ActualWidth / 2;
                    this.Top = point.Y - border.ActualHeight / 2;
                }
                DragMove();
            }
        }

        private void Maximize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = (this.WindowState == WindowState.Normal) ? WindowState.Maximized : WindowState.Normal;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Mimimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                main.BorderThickness = new Thickness(0);
                main.Margin = new Thickness(7, 7, 7, 0);
                //rectMax.Visibility = Visibility.Hidden;
                //rectMin.Visibility = Visibility.Visible;
            }
            else
            {
                main.BorderThickness = new Thickness(1);
                main.Margin = new Thickness(0);
                //rectMax.Visibility = Visibility.Visible;
                //rectMin.Visibility = Visibility.Hidden;
            }
        }

        private void MainFrame_OnNavigating(object sender, NavigatingCancelEventArgs e)
        {
            var ta = new ThicknessAnimation();
            ta.Duration = TimeSpan.FromSeconds(0.3);
            ta.DecelerationRatio = 0.7;
            ta.To = new Thickness(0, 0, 0, 0);
            if (e.NavigationMode == NavigationMode.New)
            {
                ta.From = new Thickness(500, 0, 0, 0);
            }
            else if (e.NavigationMode == NavigationMode.Back)
            {
                ta.From = new Thickness(0, 0, 500, 0);
            }
                        (e.Content as Page).BeginAnimation(MarginProperty, ta);
        }
        #endregion
        #region Tray Icon
        //=============================================================================
        // Application Tray Icon
        //=============================================================================
        public void SetNotification()
        {
            //NotifyIcon ni = new NotifyIcon();
            ni = new NotifyIcon();
            //ni.Icon = new System.Drawing.Icon("/Resources/JPL_icon.ico");
            ni.Icon = new System.Drawing.Icon(System.Windows.Application.GetResourceStream(new Uri("Resources/JPL_icon.ico", UriKind.Relative)).Stream);
            ni.Visible = true;
            ni.DoubleClick += delegate (object sender, EventArgs eventArgs)
            {
                this.Show();
                this.WindowState = WindowState.Normal;
            };
            ni.ContextMenu = SetContextMenu(ni);
            ni.Text = "JPL Gateway";
        }

        private System.Windows.Forms.ContextMenu SetContextMenu(NotifyIcon ni)
        {
            System.Windows.Forms.ContextMenu menu = new System.Windows.Forms.ContextMenu();
            System.Windows.Forms.MenuItem item1 = new System.Windows.Forms.MenuItem();

            item1.Text = Cultures.Resources.StrContext01;
            item1.Click += delegate (object click, EventArgs eventArgs)
            {
                this.Show();
                //this.WindowState = WindowState.Normal;
            };
            menu.MenuItems.Add(item1);

            System.Windows.Forms.MenuItem item2 = new System.Windows.Forms.MenuItem();
            item2.Text = Cultures.Resources.StrContext02;
            item2.Click += delegate (object click, EventArgs eventArgs)
            {
                ni.Visible = false;
                ni.Dispose();
                System.Windows.Application.Current.Shutdown();
            };
            menu.MenuItems.Add(item2);

            return menu;
        }

        public void Reflash_context()
        {
            ni.ContextMenu = SetContextMenu(ni);
        }
        #endregion
        #region USB Detection

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            HwndSource Source = HwndSource.FromHwnd(new WindowInteropHelper(this).Handle);
            if (Source != null)
            {
                IntPtr windowHandle = Source.Handle;
                Source.AddHook(WndProc);

                UsbNotification.UsbNotification.RegisterUsbDeviceNotification(windowHandle);
            }
        }

        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wparam, IntPtr lparam, ref bool handled)
        {
            if (msg == NativeMethods.WM_SHOWME)
            {
                ShowMe();
            }

            if (msg == UsbNotification.UsbNotification.WmDevicechange)
            {
                //OnDeviceChange((int)wparam);
                switch ((int)wparam)
                {
                    case UsbNotification.UsbNotification.DbtDeviceremovecomplete:
                        if (BTisUpgrading == true)
                        {
                            Console.WriteLine("BT device is upgrading...");
                            break;
                        }

                        if (!isRemovedCalled)
                        {
                            Console.WriteLine("Removed");
                            Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
                            {
                                View.Trouble_Page.trouble.test_clear();
                                Usb_DeviceRemoved();
                            }));
                        }
                        break;

                    case UsbNotification.UsbNotification.DbtDevicearrival:
                        if (BTisUpgrading == true)
                        {
                            Console.WriteLine("BT device is upgrading...");
                            break;
                        }

                        if (!isAddedCalled)
                        {
                            Console.WriteLine("Added");
                            new Thread(() =>
                            {
                                Thread.CurrentThread.IsBackground = true;
                                Thread.Sleep(2000);

                                Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
                                {
                                    Usb_DeviceAdded();
                                }));
                                isAddedCalled = false;

                                //Setting_Check();

                            }).Start();
                        }
                        break;
                }
            }
            handled = false;
            return IntPtr.Zero;
        }

        private void Usb_DeviceRemoved()
        {
            devices = HIDDevice.getConnectedDevices();
            if (devices != null)
            {
                if (devices.Length > 0)
                {
                    Close_msgbox();
                    if (isRemoved == false)
                    {
                        frame1.Content = View.Connected_Page.conpage;
                    }
                    else if (isRemoved == true)
                    {
                        frame1.Content = View.Nodevice_Page.disconpage;
                    }
                    View.Connected_Page.conpage.Device_num(devices.Length);
                    for (int i = 0; i <= devices.Length - 1; i++)
                    {
                        string devPID = devices[i].PID.ToString("X4");
                        string devVID = devices[i].VID.ToString("X4");
                        string devVER = devices[i].versionNumber.ToString("X4");

                        if (devVID.Equals("2EA1") || devVID.Equals("33CB"))
                            isSetting_enable = true;
                        else
                            isSetting_enable = false;

                        // swyoon
                        //json = UpdateSoftphone.checkUpdateVersion(devVID, devPID, devVER);
                        json = UpdateService.checkUpdateVersionBinary(devVID, devPID, devVER);

                        if (json != null)
                        {
                            jsonArr[0] = json;
                            if (json["update"].ToString().Equals("True"))
                            {
                                mUpdateCount++;
                            }
                        }
                        View.Connected_Page.conpage.UsbStatusUpdate(devices[i].product, devices[i].VID.ToString("X4"), devices[i].PID.ToString("X4"), devices[i].versionNumber.ToString("X4"), i, false, arrSoftPhone);
                        if (i.Equals(0))
                        {
                            device0 = null;
                            device0 = new HIDDevice(devices[i].devicePath, true);
                            device0.dataReceived += new HIDDevice.dataReceivedEvent(device_dataReceived);

                            if (mUpdateCount == 0)
                            {
                                View.Connected_Page.conpage.Conn_update0.Source = new BitmapImage(new Uri("/Resources/Button_Icon/Updates_DB_Small.png", UriKind.Relative));
                            }
                            else if (mUpdateCount <= 1)
                            {
                                View.Connected_Page.conpage.Conn_update0.Source = new BitmapImage(new Uri("/Resources/Button_Icon/Updates_DB_Small_N.png", UriKind.Relative));
                            }
                            mUpdateCount = 0;
                        }
                        else if (i.Equals(1))
                        {
                            device1 = null;
                            device1 = new HIDDevice(devices[i].devicePath, true);
                            device1.dataReceived += new HIDDevice.dataReceivedEvent(device_dataReceived);

                            if (mUpdateCount == 0)
                            {
                                View.Connected_Page.conpage.Conn_update1.Source = new BitmapImage(new Uri("/Resources/Button_Icon/Updates_DB_Small.png", UriKind.Relative));
                            }
                            else if (mUpdateCount <= 1)
                            {
                                View.Connected_Page.conpage.Conn_update1.Source = new BitmapImage(new Uri("/Resources/Button_Icon/Updates_DB_Small_N.png", UriKind.Relative));
                            }
                            mUpdateCount = 0;
                        }
                    }
                }
            }
            //Console.WriteLine("Remove Devices Length is : " + devices.Length);
            if (devices.Length == 0)
            {
                Close_msgbox();
                isSetting_access = false;
                isRemoved = true;
                isSetting_enable = false;
                frame1.Content = View.Nodevice_Page.disconpage;
                frame5.Visibility = Visibility.Hidden;
            }
        }

        private void Usb_DeviceAdded()
        {
            mUpdateCount = 0;

            devices = HIDDevice.getConnectedDevices();
            //Console.WriteLine("Added Devices Length is : " +devices.Length);
            if (devices != null)
            {
                if (devices.Length > 0)
                {
                    Close_msgbox();
                    frame1.Content = View.Connected_Page.conpage;
                    View.Connected_Page.conpage.Device_num(devices.Length);
                    for (int i = 0; i <= devices.Length - 1; i++)
                    {
                        string devPID = devices[i].PID.ToString("X4");
                        string devVID = devices[i].VID.ToString("X4");
                        string devVER = devices[i].versionNumber.ToString("X4");

                        if (devVID.Equals("2EA1") || devVID.Equals("33CB"))
                            isSetting_enable = true;
                        else
                            isSetting_enable = false;


                        //BL-056 Vendor ID 혼용 Check
                        if (devPID.Equals("0800") || devPID.Equals("0900"))
                        {
                            if (devVID.Equals("2EA1"))
                            {
                                devVID = "33CB";
                            }
                        }

                        // swyoon
                        //json = UpdateSoftphone.checkUpdateVersion(devVID, devPID, devVER);
                        json = UpdateService.checkUpdateVersionBinary(devVID, devPID, devVER);

                        if (json != null)
                        {
                            jsonArr[0] = json;
                            if (json["update"].ToString().Equals("True"))
                            {
                                mUpdateCount++;
                            }
                        }

                        View.Connected_Page.conpage.UsbStatusUpdate(devices[i].product, devices[i].VID.ToString("X4"), devices[i].PID.ToString("X4"), devices[i].versionNumber.ToString("X4"), i, false, arrSoftPhone);
                        if (i.Equals(0))
                        {
                            device0 = null;
                            device0 = new HIDDevice(devices[i].devicePath, true);
                            device0.dataReceived += new HIDDevice.dataReceivedEvent(device_dataReceived);

                            if (mUpdateCount == 0)
                            {
                                View.Connected_Page.conpage.Conn_update0.Source = new BitmapImage(new Uri("/Resources/Button_Icon/Updates_DB_Small.png", UriKind.Relative));
                            }
                            else if (mUpdateCount <= 1)
                            {
                                View.Connected_Page.conpage.Conn_update0.Source = new BitmapImage(new Uri("/Resources/Button_Icon/Updates_DB_Small_N.png", UriKind.Relative));
                            }
                            mUpdateCount = 0;
                        }
                        else if (i.Equals(1))
                        {
                            device1 = null;
                            device1 = new HIDDevice(devices[i].devicePath, true);
                            device1.dataReceived += new HIDDevice.dataReceivedEvent(device_dataReceived);

                            if (mUpdateCount == 0)
                            {
                                View.Connected_Page.conpage.Conn_update1.Source = new BitmapImage(new Uri("/Resources/Button_Icon/Updates_DB_Small.png", UriKind.Relative));
                            }
                            else if (mUpdateCount <= 1)
                            {
                                View.Connected_Page.conpage.Conn_update1.Source = new BitmapImage(new Uri("/Resources/Button_Icon/Updates_DB_Small_N.png", UriKind.Relative));
                            }
                            mUpdateCount = 0;
                        }
                    }
                    mUpdateCount = 0;
                    isRemoved = false;

                    View.Device_settings_Page.devicesettingspage.Setting_icon1(false);
                    //View.Select_device_Page.selectdevicepage.Setting_icon(false);
                }
            }
            else
            {
            }
        }
        #endregion
        #region Software Update
        public void SW_updatecheck()
        {
            // swyoon 20230216
            //JObject jsonSoltlink = UpdateSoftphone.checkUpdateVersion("0000", "0004", mSoftLinkVersion);
            mSoftLinkVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();

            View.Settings_Page.settingpage.CurrentVersion.Content = mSoftLinkVersion;

            JObject jsonSoltlink = UpdateService.checkPgmUpdateVersion(mSoftLinkVersion);

            if (jsonSoltlink != null)
            {
                jsonArr[1] = jsonSoltlink;

                if (jsonSoltlink["update"].ToString().Equals("True"))
                {
                    View.Settings_Page.settingpage.NextVersion.Content = jsonSoltlink["versionname"].ToString();
                    View.Settings_Page.settingpage.setUpdatesetupexe(jsonSoltlink["filename"].ToString());
                    View.Settings_Page.settingpage.setUpdateInfo(1, jsonSoltlink["desc"].ToString());

                    // add download funciton 
                    new Thread(() =>
                    {
                        Thread.CurrentThread.IsBackground = true;

                        using (var client2 = new WebClient())
                        {
                            // neet setup filedownload url 
                            string downpath = jsonSoltlink["filepath"].ToString();
                            string filename2 = jsonSoltlink["filename"].ToString();

                            string result = Path.GetTempPath();
                            string downloadurl = String.Format("{0}{1} ", UpdateService.SERVERADDR, downpath);
                            if (!File.Exists(result + filename2))
                            {
                                client2.DownloadFile(downloadurl, result + filename2);
                            }
                        }
                    }).Start();
                }
                else
                {
                    View.Settings_Page.settingpage.NextVersion.Content = mSoftLinkVersion;
                }
            }
            // swyoon if server is not working 
            else
            {
                //Console.WriteLine("xxx null return");
                View.Settings_Page.settingpage.NextVersion.Content = mSoftLinkVersion;
            }

            View.Settings_Page.settingpage.setArrJson(jsonArr);
        }
        #endregion
        #region Message Box
        private void Close_msgbox()
        {
            frame1.IsEnabled = true;
            frame1.Opacity = 1;
            mainwindow.frame5.Content = null;
            frame5.Visibility = Visibility.Hidden;
        }
        #endregion
        #region Startup Function
        public static void Check_startup()
        {
            var key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run");
            if (key.GetValue("JPL_Gateway") == null)
                View.Settings_Page.settingpage.startup.IsChecked = false;
            else
                View.Settings_Page.settingpage.startup.IsChecked = true;
        }

        private void Softphone_Startup()
        {
            if (AppConfiguration.GetAppConfig("FreSoftphone").Equals("StateOneX"))
            {
                View.Device_settings_Page.devicesettingspage.Select_softphonelist(0);
                if (softphoneitemOneX.ISRUN)
                    Set_State_Softphone("StateOneX");
            }
            if (AppConfiguration.GetAppConfig("FreSoftphone").Equals("StateOneXComm"))
            {
                View.Device_settings_Page.devicesettingspage.Select_softphonelist(1);
                if (softphoneitemOneXComm.ISRUN)
                    Set_State_Softphone("StateOneXComm");
            }
            if (AppConfiguration.GetAppConfig("FreSoftphone").Equals("StateWorkplace"))
            {
                View.Device_settings_Page.devicesettingspage.Select_softphonelist(2);
                if (softphoneitemIXWorkplace.ISRUN)
                    Set_State_Softphone("StateWorkplace");
            }
            if (AppConfiguration.GetAppConfig("FreSoftphone").Equals("StateJabber"))
            {
                View.Device_settings_Page.devicesettingspage.Select_softphonelist(3);
                if (softphoneitemJabber.ISRUN)
                    Set_State_Softphone("StateJabber");
            }
            if (AppConfiguration.GetAppConfig("FreSoftphone").Equals("StateBria4"))
            {
                View.Device_settings_Page.devicesettingspage.Select_softphonelist(4);
                if (softphoneitemBria.ISRUN)
                    Set_State_Softphone("StateBria4");
            }
            if (AppConfiguration.GetAppConfig("FreSoftphone").Equals("StateBria"))
            {
                View.Device_settings_Page.devicesettingspage.Select_softphonelist(5);
                if (softphoneitemBria6.ISRUN)
                    Set_State_Softphone("StateBria");
            }
            if (AppConfiguration.GetAppConfig("FreSoftphone").Equals("State3CX"))
            {
                View.Device_settings_Page.devicesettingspage.Select_softphonelist(6);
                if (softphoneitem3CX.ISRUN)
                    Set_State_Softphone("State3CX");
            }

            startup_softphone = true;
        }
        #endregion
        #region HID Command
        private void device_dataReceived(byte[] message)
        {
            Console.WriteLine("Device Received Message : " + "0x" + message[0] + ", 0x" + message[1] + ", 0x" + message[2]);

            if (HookOff)
            {
                Console.WriteLine("Hook OFF Received Msg {0} {1} {2}", message[0], message[1], message[2]);

                if (keypressedcount == 0)
                {
                    Console.WriteLine("keypressedcount {0} ", keypressedcount);
                    Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
                    {
                        timer_Keypress.Start();
                    }));


                }
                keypressedcount++;

                lastHookCheckByte = message[2];
            }
            else
            {
                if (Ring)
                {
                    Ring = false;
                    LEDRingFunc(false);
                    HookOff = true;
                    LEDHookOffFunc(true);

                    usb_ButtonHook();
                }
            }

            //Console.WriteLine("Device [1] Received Message : " + "0x" + message[0] + ", 0x" + message[1] + ", 0x" + message[2]);
            byte key = (byte)(message[12] & 0x01);
            byte ring = (byte)(message[12] >> 1 & 0x01);
            byte check = (byte)(message[12] >> 2 & 0x03);
            byte interval = (byte)(message[12] >> 5 & 0x03);

            if (message[0] == 0x01 && check == 1) //Check setting device
            {
                isSetting_access = true;
                Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
                {
                    //View.Select_device_Page.selectdevicepage.Setting_icon(true);
                    View.Device_settings_Page.devicesettingspage.Setting_icon1(true);
                }));
                //////////////////////////////////////////////// Beep Read ////////////////////////////////////////////////////////////
                if (key == 0)
                {
                    Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
                    {
                        View.Device_settings_Page.devicesettingspage.Keytone.IsChecked = false;
                    }));
                }
                else if (key == 1)
                {
                    Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
                    {
                        View.Device_settings_Page.devicesettingspage.Keytone.IsChecked = true;
                    }));
                }

                if (ring == 0)
                {
                    Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
                    {
                        View.Device_settings_Page.devicesettingspage.Ringtone.IsChecked = false;
                    }));
                }
                else if (ring == 1)
                {
                    Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
                    {
                        View.Device_settings_Page.devicesettingspage.Ringtone.IsChecked = true;
                    }));
                }

                if (interval == 0)
                {
                    Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
                    {
                        View.Device_settings_Page.devicesettingspage.Muteinter.SelectedIndex = 0;
                    }));
                }
                else if (interval == 1)
                {
                    Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
                    {
                        View.Device_settings_Page.devicesettingspage.Muteinter.SelectedIndex = 1;
                    }));
                }
                else if (interval == 2)
                {
                    Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
                    {
                        View.Device_settings_Page.devicesettingspage.Muteinter.SelectedIndex = 2;
                    }));
                }
                else if (interval == 3)
                {
                    Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
                    {
                        View.Device_settings_Page.devicesettingspage.Muteinter.SelectedIndex = 3;
                    }));
                }
            }
            //////////////////////////////////////////////// EQ Read ////////////////////////////////////////////////////////////
            if (message[0] == 0x01 && check == 2)
            {
                Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
                {
                    View.Device_settings_Page.devicesettingspage.Enable_eqset();

                    View.Device_settings_Page.devicesettingspage.fr100.Value = message[7];
                    View.Device_settings_Page.devicesettingspage.fr350.Value = message[8];
                    View.Device_settings_Page.devicesettingspage.fr1000.Value = message[9];
                    View.Device_settings_Page.devicesettingspage.fr3500.Value = message[10];
                    View.Device_settings_Page.devicesettingspage.fr13000.Value = message[11];
                    View.Device_settings_Page.devicesettingspage.EQ_toggle(false);
                }));

                View.Device_settings_Page.devicesettingspage.eq_preset_changed = true;

                //Console.WriteLine("Frequency Value = "+ fr100.Value + " " + fr350.Value + " " + fr1000.Value + " " + fr3500.Value + " " + fr13000.Value);
                //Console.WriteLine("EQ Setting = 0x0" + message[7] + " 0x0" + message[8] + " 0x0" + message[9] + " 0x0" + message[10] + " 0x0" + message[11]);
                if (message[6] == 0x00)
                {
                    Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
                    {
                        View.Device_settings_Page.devicesettingspage.Set_off();
                        View.Device_settings_Page.devicesettingspage.eq_preset.SelectedIndex = 0;
                    }));

                    View.Device_settings_Page.devicesettingspage.eq_preset_changed = true;
                }
                else if (message[6] == 0x01)
                    if (message[7] == 0x18 && message[8] == 0x18 && message[9] == 0x18 && message[10] == 0x18 && message[11] == 0x18)
                    {
                        Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
                        {
                            View.Device_settings_Page.devicesettingspage.eq_preset.SelectedIndex = 0;
                        }));
                    }
                    else
                    {
                        Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
                        {
                            View.Device_settings_Page.devicesettingspage.eq_preset.SelectedIndex = 1;
                        }));
                    }
                else if (message[6] == 0x02)
                    Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
                    {
                        View.Device_settings_Page.devicesettingspage.eq_preset.SelectedIndex = 2;
                    }));
                else if (message[6] == 0x03)
                    Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
                    {
                        View.Device_settings_Page.devicesettingspage.eq_preset.SelectedIndex = 3;
                    }));
                else if (message[6] == 0x04)
                    Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
                    {
                        View.Device_settings_Page.devicesettingspage.eq_preset.SelectedIndex = 4;
                    }));
            }
        }
        public void Check_beep_setting(int devicenum)
        {
            if (isSetting_enable == true)
            {
                try
                {
                    byte[] writeData = { 0x06, 0x00, 0x00, 0x01 };
                    if (devicenum.Equals(0))
                    {
                        if (device0 != null)
                            device0.write(writeData);
                    }
                    else if (devicenum.Equals(1))
                    {
                        if (device1 != null)
                            device1.write(writeData);
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        public void Apply_beep_setting(byte keytoggle, byte ringtoggle, byte muteinter)
        {
            byte[] writeData = { 0x06, 0x00, 0x01, 0x00, keytoggle, ringtoggle, 0x00, muteinter };
            if (isSelectdevice.Equals(0))
            {
                if (device0 != null)
                    device0.write(writeData);
            }
            else if (isSelectdevice.Equals(1))
            {
                if (device1 != null)
                    device1.write(writeData);
            }
        }

        public void Check_eq_setting(int devicenum)
        {
            if (isSetting_enable == true)
            {
                try
                {
                    byte[] writeData = { 0x06, 0x00, 0x00, 0x02 };
                    if (devicenum.Equals(0))
                    {
                        if (device0 != null)
                            device0.write(writeData);
                    }
                    else if (devicenum.Equals(1))
                    {
                        if (device1 != null)
                            device1.write(writeData);
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        public void Apply_eq_setting(byte set, byte eq1, byte eq2, byte eq3, byte eq4, byte eq5)
        {
            byte[] writeData = { 0x06, 0x00, 0x02, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, set, eq1, eq2, eq3, eq4, eq5 };
            if (isSelectdevice.Equals(0))
            {
                if (device0 != null)
                    device0.write(writeData);
            }
            else if (isSelectdevice.Equals(1))
            {
                if (device1 != null)
                    device1.write(writeData);
            }
        }
        #endregion
        #region Menu Button
        private void Device_Click(object sender, RoutedEventArgs e)
        {
            Close_msgbox();
            if (isRemoved == false)
            {
                frame1.Content = View.Connected_Page.conpage;
            }
            else if (isRemoved == true)
            {
                frame1.Content = View.Nodevice_Page.disconpage;
            }
        }

        private void Support_Click(object sender, RoutedEventArgs e)
        {
            Close_msgbox();
            frame1.Navigate(new Uri("/View/Support_Page.xaml", UriKind.Relative));
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            Close_msgbox();
            frame1.Content = View.Settings_Page.settingpage;
        }

        private void Products_Click(object sender, RoutedEventArgs e)
        {
            Close_msgbox();
            frame1.Navigate(new Uri("/View/Products_Page.xaml", UriKind.Relative));
            //frame1.Content = View.Products_Page.products;
        }

        private void PBMenuLogo_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("http://www.jpltele.com/");
        }
        #endregion
        #region Get Softphone
        //=============================================================================================
        // GetStrCurrentSoftphone
        //=============================================================================================
        public void GetStrCurrentSoftphone()
        {
            //Console.WriteLine("==GetStrCurrentSoftphone==");
            Process[] ProcOneXAgentUI = Process.GetProcessesByName("OneXAgentUI");
            Process[] ProcOneXCommUI = Process.GetProcessesByName("onexcui");
            Process[] ProcIXWorkplace = Process.GetProcessesByName("Avaya IX Workplace");
            Process[] ProcCiscoJabber = Process.GetProcessesByName("CiscoJabber");
            Process[] ProcBria = Process.GetProcessesByName("Bria4");
            Process[] ProcBria6 = Process.GetProcessesByName("Bria");
            Process[] Proc3CXWin8Phone = Process.GetProcessesByName("3CXWin8Phone");

            int chkcount = 0;
            //==========================================================================
            if (ProcOneXAgentUI.Length > 0)
            {
                isComm = false;
                softphoneitemOneX.ISRUN = true;
                View.Device_settings_Page.devicesettingspage.ReadyOneX.Text = "(Ready)";
                chkcount++;
            }
            else
            {
                softphoneitemOneX.ISRUN = false;
                if (SelectedSoftPhone.Equals("OneXAgentUI"))
                    SelectedSoftPhone = "";
                View.Device_settings_Page.devicesettingspage.ReadyOneX.Text = "(Not ready)";
            }
            //==========================================================================
            if (ProcOneXCommUI.Length > 0)
            {
                isComm = true;
                softphoneitemOneXComm.ISRUN = true;
                View.Device_settings_Page.devicesettingspage.ReadyOneXComm.Text = "(Ready)";
                chkcount++;
            }
            else
            {
                softphoneitemOneXComm.ISRUN = false;
                if (SelectedSoftPhone.Equals("OneXComm"))
                    SelectedSoftPhone = "";
                View.Device_settings_Page.devicesettingspage.ReadyOneXComm.Text = "(Not ready)";
            }
            //==========================================================================
            if (ProcIXWorkplace.Length > 0)
            {
                isComm = true;
                softphoneitemIXWorkplace.ISRUN = true;
                View.Device_settings_Page.devicesettingspage.ReadyIXWorkplace.Text = "(Ready)";
                chkcount++;
            }
            else
            {
                if (softphoneitemIXWorkplace.ISRUN && softphoneitemIXWorkplace.ISSELECTED)
                {
                    mIXWorkplace.CloseSoftphone();
                    Console.WriteLine("IX Workplace is closed");
                    softphoneitemIXWorkplace.ISSELECTED = false;
                }
                softphoneitemIXWorkplace.ISRUN = false;
                if (SelectedSoftPhone.Equals("IXWorkplace"))
                    SelectedSoftPhone = "";
                View.Device_settings_Page.devicesettingspage.ReadyIXWorkplace.Text = "(Not ready)";
            }
            //==========================================================================
            if (ProcCiscoJabber.Length > 0)
            {
                softphoneitemJabber.ISRUN = true;
                View.Device_settings_Page.devicesettingspage.ReadyJabber.Text = "(Ready)";
                chkcount++;
            }
            else
            {
                softphoneitemJabber.ISRUN = false;
                if (SelectedSoftPhone.Equals("CiscoJabber"))
                    SelectedSoftPhone = "";
                View.Device_settings_Page.devicesettingspage.ReadyJabber.Text = "(Not ready)";
            }
            //==========================================================================
            if (ProcBria.Length > 0)
            {
                softphoneitemBria.ISRUN = true;
                View.Device_settings_Page.devicesettingspage.ReadyBria.Text = "(Ready)";
                chkcount++;
            }
            else
            {
                softphoneitemBria.ISRUN = false;
                if (SelectedSoftPhone.Equals("Bria4"))
                    SelectedSoftPhone = "";
                View.Device_settings_Page.devicesettingspage.ReadyBria.Text = "(Not ready)";
            }
            //==========================================================================
            if (ProcBria6.Length > 0)
            {
                softphoneitemBria6.ISRUN = true;
                View.Device_settings_Page.devicesettingspage.ReadyBria6.Text = "(Ready)";
                chkcount++;
            }
            else
            {
                softphoneitemBria6.ISRUN = false;
                if (SelectedSoftPhone.Equals("Bria"))
                    SelectedSoftPhone = "";
                View.Device_settings_Page.devicesettingspage.ReadyBria6.Text = "(Not ready)";
            }
            //==========================================================================
            if (Proc3CXWin8Phone.Length > 0)
            {
                if (mRemoveAfter3CXCount > 0)
                {
                    mRemoveAfter3CXCount--;
                    softphoneitem3CX.ISRUN = false;
                    softphoneitem3CX.ISSELECTED = false;
                    View.Device_settings_Page.devicesettingspage.Ready3CX.Text = "(Not ready)";
                }
                else
                {
                    softphoneitem3CX.ISRUN = true;
                    View.Device_settings_Page.devicesettingspage.Ready3CX.Text = "(Ready)";
                    chkcount++;
                }
            }
            else
            {
                softphoneitem3CX.ISRUN = false;
                if (SelectedSoftPhone.Equals("3CXWin8Phone"))
                    SelectedSoftPhone = "";
                View.Device_settings_Page.devicesettingspage.Ready3CX.Text = "(Not ready)";
            }
            //==========================================================================


            if (SelectedSoftPhone == "")
            {
                if (startup_softphone == false)
                {
                    Softphone_Startup();
                }
                else
                {
                    if (ProcOneXAgentUI.Length > 0)
                    {
                        Set_State_Softphone("StateOneX");
                    }
                    else if (ProcOneXCommUI.Length > 0)
                    {
                        Set_State_Softphone("StateOneXComm");
                    }
                    else if (ProcIXWorkplace.Length > 0)
                    {
                        Set_State_Softphone("StateWorkplace");
                    }
                    else if (ProcCiscoJabber.Length > 0)
                    {
                        Set_State_Softphone("StateJabber");
                    }
                    else if (ProcBria.Length > 0)
                    {
                        Set_State_Softphone("StateBria4");
                    }
                    else if (ProcBria6.Length > 0)
                    {
                        Set_State_Softphone("StateBria");
                    }
                    else if (Proc3CXWin8Phone.Length > 0)
                    {
                        Set_State_Softphone("State3CX");
                    }
                }
            }

            if (chkcount == 0)
            {
                SelectedSoftPhone = "";
            }
        }
        #endregion
        #region Softphone State
        public void Set_State_Softphone(string softphone)
        {
            bool StateOneX = false;
            bool StateOneXComm = false;
            bool StateWorkplace = false;
            bool StateJabber = false;
            bool State3CX = false;
            bool StateBria4 = false;
            bool StateBria = false;

            if (softphone.Equals("StateOneX"))
                StateOneX = true;
            if (softphone.Equals("StateOneXComm"))
                StateOneXComm = true;
            if (softphone.Equals("StateWorkplace"))
                StateWorkplace = true;
            if (softphone.Equals("StateJabber"))
                StateJabber = true;
            if (softphone.Equals("State3CX"))
                State3CX = true;
            if (softphone.Equals("StateBria4"))
                StateBria4 = true;
            if (softphone.Equals("StateBria"))
                StateBria = true;

            State_OneXAgentUI(StateOneX);
            State_OneXComm(StateOneXComm);
            State_Workplace(StateWorkplace);
            State_Jabber(StateJabber);
            State_3CX(State3CX);
            State_Bria4(StateBria4);
            State_Bria(StateBria);
        }

        public void State_OneXAgentUI(bool state)
        {
            if (state)
            {
                if (softphoneitemOneX.ISRUN && !SelectedSoftPhone.Equals("OneXAgentUI"))
                {
                    softphoneitemOneX.ISSELECTED = true;
                    SelectedSoftPhone = "OneXAgentUI";
                    mOneXManager = new OneXManager();

                    bool connected = mOneXManager.OpenSoftphone();
                    if (connected)
                    {
                        mOneXManager.SoftphoneStateChanged -= new EventHandler<OneXSoftPhoneEventArgs>(OnexSoftphoneStateChanged);
                        mOneXManager.SoftphoneStateChanged += new EventHandler<OneXSoftPhoneEventArgs>(OnexSoftphoneStateChanged);
                        mOneXManager.SoftphoneRemoved -= new EventHandler(OnexSoftphoneRemoved);
                        mOneXManager.SoftphoneRemoved += new EventHandler(OnexSoftphoneRemoved);
                    }
                    else
                    {
                        Console.WriteLine("APP ::  not running !!! ");
                    }
                }
                else
                    Console.WriteLine("Softphone OneXAgent  is NOT RUN ! !");
            }
            else
            {
                softphoneitemOneX.ISSELECTED = false;
                if (mOneXManager != null)
                {
                    mOneXManager.Dispose();
                    mOneXManager.SoftphoneStateChanged -= new EventHandler<OneXSoftPhoneEventArgs>(OnexSoftphoneStateChanged);
                    mOneXManager.SoftphoneRemoved -= new EventHandler(OnexSoftphoneRemoved);
                }
            }
        }

        public void State_OneXComm(bool state)
        {
            if (state)
            {
                if (softphoneitemOneXComm.ISRUN && !SelectedSoftPhone.Equals("OneXComm"))
                {
                    try
                    {
                        softphoneitemOneXComm.ISSELECTED = true;
                        SelectedSoftPhone = "OneXComm";
                        mOneXCommManager = new OneXCommManager();
                        mOneXCommManager.OpenSoftphone();
                        mOneXCommManager.SoftphoneStateChanged -= new EventHandler<SoftPhoneStatusEventArgs>(OnexCommSoftphoneStateChanged);
                        mOneXCommManager.SoftphoneStateChanged += new EventHandler<SoftPhoneStatusEventArgs>(OnexCommSoftphoneStateChanged);
                        mOneXCommManager.SoftphoneMuteStateChanged -= new EventHandler<SoftPhoneMuteEventArgs>(OnexCommSoftphoneMuteStateChanged);
                        mOneXCommManager.SoftphoneMuteStateChanged += new EventHandler<SoftPhoneMuteEventArgs>(OnexCommSoftphoneMuteStateChanged);
                    }
                    catch (System.Runtime.InteropServices.COMException sce)
                    {
                        softphoneitemOneXComm.ISSELECTED = false;
                        SelectedSoftPhone = "";
                        System.Windows.Forms.MessageBox.Show("Please, Restart.");
                    }
                }
                else
                    Console.WriteLine("Softphone OneXComm  is NOT RUN ! !");
            }
            else
            {
                softphoneitemOneXComm.ISSELECTED = false;
                if (mOneXCommManager != null)
                {
                    mOneXCommManager.Dispose();
                    mOneXCommManager.SoftphoneStateChanged -= new EventHandler<SoftPhoneStatusEventArgs>(OnexCommSoftphoneStateChanged);
                    mOneXCommManager.SoftphoneMuteStateChanged -= new EventHandler<SoftPhoneMuteEventArgs>(OnexCommSoftphoneMuteStateChanged);
                }
            }
        }

        public void State_Workplace(bool state)
        {
            if (state)
            {
                if (softphoneitemIXWorkplace.ISRUN && !SelectedSoftPhone.Equals("IXWorkplace"))
                {
                    try
                    {
                        softphoneitemIXWorkplace.ISSELECTED = true;
                        SelectedSoftPhone = "IXWorkplace";
                        mIXWorkplace = new IXWorkplace();
                        Console.WriteLine("IX Workplace Softphone is opened");
                        mIXWorkplace.OpenSoftphone();
                        mIXWorkplace.SoftphoneStateChanged -= new EventHandler<IXSoftPhoneEventArgs>(IXSoftphoneStateChanged);
                        mIXWorkplace.SoftphoneStateChanged += new EventHandler<IXSoftPhoneEventArgs>(IXSoftphoneStateChanged);
                        mIXWorkplace.SoftphoneMuteStateChanged -= new EventHandler<IXSoftPhoneMuteEventArgs>(IXSoftphoneMuteStateChanged);
                        mIXWorkplace.SoftphoneMuteStateChanged += new EventHandler<IXSoftPhoneMuteEventArgs>(IXSoftphoneMuteStateChanged);
                    }
                    catch (System.Runtime.InteropServices.COMException sce)
                    {
                        //Console.WriteLine("xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx" + sce.ToString());
                        softphoneitemIXWorkplace.ISSELECTED = false;
                        SelectedSoftPhone = "";
                        System.Windows.Forms.MessageBox.Show("Please, Restart.");
                    }
                }
                else
                    Console.WriteLine("Softphone Workplace is NOT RUN ! !");
            }
            else
            {
                softphoneitemIXWorkplace.ISSELECTED = false;
                if (mIXWorkplace != null)
                {
                    mIXWorkplace.Dispose();
                    mIXWorkplace.SoftphoneStateChanged -= new EventHandler<IXSoftPhoneEventArgs>(IXSoftphoneStateChanged);
                    mIXWorkplace.SoftphoneMuteStateChanged -= new EventHandler<IXSoftPhoneMuteEventArgs>(IXSoftphoneMuteStateChanged);
                }
            }
        }

        public void State_Jabber(bool state)
        {
            if (state)
            {
                if (softphoneitemJabber.ISRUN && !SelectedSoftPhone.Equals("CiscoJabber"))
                {
                    softphoneitemJabber.ISSELECTED = true;
                    SelectedSoftPhone = "CiscoJabber";
                    mJabberManager = new JabberManager();
                    bool connected = mJabberManager.OpenDevice();
                    if (connected)
                    {
                        mJabberManager.SoftphoneCallStateChanged -= new EventHandler<JabberSoftPhoneEventArgs>(JabberSoftphoneCallStateChanged);
                        mJabberManager.SoftphoneCallStateChanged += new EventHandler<JabberSoftPhoneEventArgs>(JabberSoftphoneCallStateChanged);
                        mJabberManager.SoftphoneMuteStateChanged -= new EventHandler<JabberSoftPhoneMuteEventArgs>(JabberSoftphoneMuteStateChanged);
                        mJabberManager.SoftphoneMuteStateChanged += new EventHandler<JabberSoftPhoneMuteEventArgs>(JabberSoftphoneMuteStateChanged);
                        mJabberManager.SoftphoneRemoved -= new EventHandler(JabberSoftphoneRemoved);
                        mJabberManager.SoftphoneRemoved += new EventHandler(JabberSoftphoneRemoved);
                    }
                }
                else
                    Console.WriteLine("Softphone Cisco Jabber is NOT RUN ! !");
            }
            else
            {
                softphoneitemJabber.ISSELECTED = false;
                if (mJabberManager != null)
                {
                    mJabberManager.Dispose();
                    mJabberManager.SoftphoneCallStateChanged -= new EventHandler<JabberSoftPhoneEventArgs>(JabberSoftphoneCallStateChanged);
                    mJabberManager.SoftphoneMuteStateChanged -= new EventHandler<JabberSoftPhoneMuteEventArgs>(JabberSoftphoneMuteStateChanged);
                    mJabberManager.SoftphoneRemoved -= new EventHandler(JabberSoftphoneRemoved);
                }
            }
        }

        public void State_3CX(bool state)
        {
            if (state)
            {
                if (softphoneitem3CX.ISRUN && !SelectedSoftPhone.Equals("3CXWin8Phone"))
                {
                    softphoneitem3CX.ISSELECTED = true;
                    SelectedSoftPhone = "3CXWin8Phone";
                    m_3CXManager = new _3CXManager();
                    Console.WriteLine("3CXWin8Phone");
                    bool connected = m_3CXManager.OpenSoftphone();

                    if (connected)
                    {
                        m_3CXManager.SoftphoneCallStateChanged -= new EventHandler<_3CXSoftPhoneEventArgs>(_3CXSoftphoneCallStateChanged);
                        m_3CXManager.SoftphoneCallStateChanged += new EventHandler<_3CXSoftPhoneEventArgs>(_3CXSoftphoneCallStateChanged);
                        m_3CXManager.SoftphoneRemoved -= new EventHandler(_3CXSoftphoneRemoved);
                        m_3CXManager.SoftphoneRemoved += new EventHandler(_3CXSoftphoneRemoved);
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("3CX :: APP :: not running !!! ");
                    }
                }
                else
                    Console.WriteLine("Softphone 3CX Window is NOT RUN ! !");
            }
            else
            {
                softphoneitem3CX.ISSELECTED = false;
                if (m_3CXManager != null)
                {
                    m_3CXManager.Dispose();
                    m_3CXManager.SoftphoneCallStateChanged -= new EventHandler<_3CXSoftPhoneEventArgs>(_3CXSoftphoneCallStateChanged);
                    m_3CXManager.SoftphoneRemoved -= new EventHandler(_3CXSoftphoneRemoved);
                }
            }
        }

        public void State_Bria4(bool state)
        {
            if (state)
            {
                if (softphoneitemBria.ISRUN && !SelectedSoftPhone.Equals("Bria4"))
                {
                    softphoneitemBria.ISSELECTED = true;
                    SelectedSoftPhone = "Bria4";
                    mBriaManager = new BriaManager();
                    mBriaManager.SoftphoneStateChanged -= new EventHandler<BriaSoftPhoneEventArgs>(BriaSoftphoneStateChanged);
                    mBriaManager.SoftphoneStateChanged += new EventHandler<BriaSoftPhoneEventArgs>(BriaSoftphoneStateChanged);
                    mBriaManager.SoftphoneMuteStateChanged -= new EventHandler<BriaSoftPhoneMuteEventArgs>(BriaSoftphoneMuteChanged);
                    mBriaManager.SoftphoneMuteStateChanged += new EventHandler<BriaSoftPhoneMuteEventArgs>(BriaSoftphoneMuteChanged);
                }
                else
                    Console.WriteLine("Softphone Bria4(Old Version) is NOT RUN ! !");
            }
            else
            {
                softphoneitemBria.ISSELECTED = false;
                if (mBriaManager != null)
                {
                    mBriaManager.Dispose();
                    mBriaManager.SoftphoneStateChanged -= new EventHandler<BriaSoftPhoneEventArgs>(BriaSoftphoneStateChanged);
                    mBriaManager.SoftphoneMuteStateChanged -= new EventHandler<BriaSoftPhoneMuteEventArgs>(BriaSoftphoneMuteChanged);
                }
            }
        }

        public void State_Bria(bool state)
        {
            if (state)
            {
                if (softphoneitemBria6.ISRUN && !SelectedSoftPhone.Equals("Bria"))
                {
                    softphoneitemBria6.ISSELECTED = true;
                    SelectedSoftPhone = "Bria";
                    mBria6Manager = new Bria6Manager();
                    mBria6Manager.SoftphoneStateChanged -= new EventHandler<Bria6SoftPhoneEventArgs>(Bria6SoftphoneStateChanged);
                    mBria6Manager.SoftphoneStateChanged += new EventHandler<Bria6SoftPhoneEventArgs>(Bria6SoftphoneStateChanged);
                    mBria6Manager.SoftphoneMuteStateChanged -= new EventHandler<Bria6SoftPhoneMuteEventArgs>(Bria6SoftphoneMuteChanged);
                    mBria6Manager.SoftphoneMuteStateChanged += new EventHandler<Bria6SoftPhoneMuteEventArgs>(Bria6SoftphoneMuteChanged);
                }
                else
                    Console.WriteLine("Softphone Bria(New Version) is NOT RUN ! !");
            }
            else
            {
                softphoneitemBria6.ISSELECTED = false;
                if (mBria6Manager != null)
                {
                    mBria6Manager.Dispose();
                    mBria6Manager.SoftphoneStateChanged -= new EventHandler<Bria6SoftPhoneEventArgs>(Bria6SoftphoneStateChanged);
                    mBria6Manager.SoftphoneMuteStateChanged -= new EventHandler<Bria6SoftPhoneMuteEventArgs>(Bria6SoftphoneMuteChanged);
                }
            }
        }
        #endregion
        #region Softphone Callback
        //=================================================================================
        // Softphone CallBack
        //=================================================================================
        // 1. MITEL
        void mMitelManager_SoftphoneStateChanged(object sender, SoftPhoneStatusEventArgs e)
        {
            CallState = e.Status.ToString();
            if (CallState.Equals("Ringing"))
            {
                Ring = true;
                Thread t = new Thread(new ThreadStart(ringinProcessor));
                t.Start();
            }
            else if (CallState.Equals("OffHook"))
            {
                Ring = false;
                Mute = false;
                HookOff = true;
                LEDHookOffFunc(true);

                hookoffcallback = true;
                new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;
                    Thread.Sleep(2000);
                    hookoffcallback = false;

                }).Start();


            }
            // Call END
            else if (CallState.Equals("OnHook"))
            {
                // wkyoon need to check text display 
                // lbBriaReady.Text = "Not ready";

                Mute = false;
                HookOff = false;

                Thread t = new Thread(new ThreadStart(callendProcessor));
                t.Start();

            }

        }
        //=========================================================================================
        // 3CX Callback 
        //=========================================================================================
        private void _3CXSoftphoneRemoved(object sender, EventArgs e)
        {
            Console.WriteLine("==_3CXSoftphoneRemoved==");
            mRemoveAfter3CXCount = 10;
            softphoneitem3CX.ISRUN = false;
            softphoneitem3CX.ISSELECTED = false;
        }

        private void _3CXSoftphoneCallStateChanged(object sender, _3CXSoftPhoneEventArgs e)
        {
            if (!CallState.Equals(e.Status))
            {
                Pre_CallState = CallState;
                CallState = e.Status;

                if (e.Status.Equals("Ringing"))
                {
                    Ring = true;
                    Thread t = new Thread(new ThreadStart(ringinProcessor));
                    t.Start();
                }
                else if (e.Status.Equals("Connected"))
                {
                    Ring = false;
                    Mute = false;
                    HookOff = true;

                    LEDHookOffFunc(true);
                    hookoffcallback = true;

                    new Thread(() =>
                    {
                        Thread.CurrentThread.IsBackground = true;
                        Thread.Sleep(2000);
                        hookoffcallback = false;

                    }).Start();

                }
                else if (e.Status.Equals("Dialing"))
                {
                    Mute = false;
                    HookOff = true;
                    LEDHookOffFunc(true);
                    hookoffcallback = true;

                    new Thread(() =>
                    {
                        Thread.CurrentThread.IsBackground = true;
                        Thread.Sleep(2000);
                        hookoffcallback = false;

                    }).Start();

                }
                else
                {
                    Mute = false;
                    HookOff = false;
                    Thread t = new Thread(new ThreadStart(callendProcessor));
                    t.Start();
                }

            }

        }
        //=========================================================================================
        // Bria Callback 
        //=========================================================================================
        private void BriaSoftphoneMuteChanged(object sender, BriaSoftPhoneMuteEventArgs e)
        {
            Mute = e.Mute;
            LEDMuteFunc(Mute);
        }

        private void BriaSoftphoneStateChanged(object sender, BriaSoftPhoneEventArgs e)
        {
            CallState = e.Status.ToString();
            if (CallState.Equals("OnConnected"))
            {

                Mute = false;
                HookOff = true;
                LEDHookOffFunc(true);
                hookoffcallback = true;

                new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;
                    Thread.Sleep(2000);
                    hookoffcallback = false;

                }).Start();

            }
            else if (CallState.Equals("OnDisconnected"))
            {
                // wkyoon need to check text display 
                // lbBriaReady.Text = "Not ready";

                Mute = false;
                HookOff = false;
                Thread t = new Thread(new ThreadStart(callendProcessor));
                t.Start();

            }
            else if (CallState.Equals("Connected"))
            {
                Ring = false;
                Mute = false;
                HookOff = true;
                LEDHookOffFunc(true);
                hookoffcallback = true;

                new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;
                    Thread.Sleep(2000);
                    hookoffcallback = false;

                }).Start();

            }
            // EndCall
            else if (CallState.Equals("EndCall"))
            {

                Mute = false;
                HookOff = false;

                Thread t = new Thread(new ThreadStart(callendProcessor));
                t.Start();

            }
            // IsRinging
            else if (CallState.Equals("IsRinging"))
            {
                Ring = true;
                Thread t = new Thread(new ThreadStart(ringinProcessor));
                t.Start();
            }
        }

        //=========================================================================================
        // Bria6 Callback 
        //=========================================================================================
        private void Bria6SoftphoneMuteChanged(object sender, Bria6SoftPhoneMuteEventArgs e)
        {
            Mute = e.Mute;
            LEDMuteFunc(Mute);
        }

        private void Bria6SoftphoneStateChanged(object sender, Bria6SoftPhoneEventArgs e)
        {
            CallState = e.Status.ToString();
            if (CallState.Equals("OnConnected"))
            {
                //Console.WriteLine("Bria6 OnConnected !");
            }

            else if (CallState.Equals("OnDisconnected"))
            {
                // wkyoon need to check text display 
                // lbBriaReady.Text = "Not ready";

                Mute = false;
                HookOff = false;
                Thread t = new Thread(new ThreadStart(callendProcessor));
                t.Start();
                softphoneitemBria6.ISRUN = false;
                softphoneitemBria6.ISSELECTED = false;
            }

            else if (CallState.Equals("Connected"))
            {
                Ring = false;
                Mute = false;
                HookOff = true;
                LEDHookOffFunc(true);
                hookoffcallback = true;

                new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;
                    Thread.Sleep(2000);
                    hookoffcallback = false;

                }).Start();

            }
            else if (CallState.Equals("EndCall"))
            {

                Mute = false;
                HookOff = false;

                Thread t = new Thread(new ThreadStart(callendProcessor));
                t.Start();

            }
            else if (CallState.Equals("IsRinging"))
            {
                Ring = true;
                Thread t = new Thread(new ThreadStart(ringinProcessor));
                t.Start();
            }
        }
        //=========================================================================================
        // Jabber Callback 
        //=========================================================================================
        private void JabberSoftphoneRemoved(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            softphoneitemJabber.ISRUN = false;
            softphoneitemJabber.ISSELECTED = false;
        }

        private void JabberSoftphoneMuteStateChanged(object sender, JabberSoftPhoneMuteEventArgs e)
        {
            if (e.Mute.Equals("unmute"))
            {
                Mute = false;
            }
            else
            {
                Mute = true;
            }
        }

        private void JabberSoftphoneCallStateChanged(object sender, JabberSoftPhoneEventArgs e)
        {
            if (e.Status.Equals("onConnected"))
            {
                Ring = false;
                HookOff = true;
                LEDHookOffFunc(true);
                hookoffcallback = true;

                new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;
                    Thread.Sleep(2000);
                    hookoffcallback = false;

                }).Start();

            }
            else if (e.Status.Equals("onIncoming"))
            {
                Ring = true;
                Thread t = new Thread(new ThreadStart(ringinProcessor));
                t.Start();
            }
            else if (e.Status.Equals("onOnHook"))
            {
                HookOff = false;
                Thread t = new Thread(new ThreadStart(callendProcessor));
                t.Start();
            }
        }

        //=========================================================================================
        // OneX Callback 
        //=========================================================================================
        private void OnexSoftphoneRemoved(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            softphoneitemOneX.ISRUN = false;
            softphoneitemOneX.ISSELECTED = false;
        }

        private void OnexSoftphoneStateChanged(object sender, OneXSoftPhoneEventArgs e)
        {
            Pre_CallState = CallState;
            CallState = e.Status.ToString();
            if (CallState.Equals("textnull"))
            {
                System.Diagnostics.Debug.WriteLine("APP :: CallState textnull ");
            }
            else if (CallState.Equals("IncommingCall"))
            {
                Ring = true;
                Thread t = new Thread(new ThreadStart(ringinProcessor));
                t.Start();
            }
            else if (CallState.Equals("OutgoingCall"))
            {
                Mute = false;
                HookOff = true;
                LEDHookOffFunc(true);
                hookoffcallback = true;

                new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;
                    Thread.Sleep(2000);
                    hookoffcallback = false;
                }).Start();
            }
            // ConnectedCall
            else if (CallState.Equals("ConnectedCall"))
            {
                Ring = false;
                Mute = false;
                HookOff = true;
                LEDHookOffFunc(true);
                hookoffcallback = true;

                new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;
                    Thread.Sleep(2000);
                    hookoffcallback = false;
                }).Start();

            }
            else
            {
                Mute = false;
                HookOff = false;

                Thread t = new Thread(new ThreadStart(callendProcessor));
                t.Start();
            }
        }

        //===============================================================================
        // OneX Comm Callback
        //===============================================================================

        private void OnexCommSoftphoneStateChanged(object sender, SoftPhoneStatusEventArgs e)
        {
            CallState = e.Status.ToString();

            if (CallState.Equals("textnull"))
            {
                System.Diagnostics.Debug.WriteLine("APP :: CallState textnull ");
            }
            else if (CallState.Equals("IncommingCall"))
            {
                Ring = true;
                Thread t = new Thread(new ThreadStart(ringinProcessor));
                t.Start();
            }
            else if (CallState.Equals("OutgoingCall"))
            {
                Mute = false;
                HookOff = true;
                LEDHookOffFunc(true);
                hookoffcallback = true;

                new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;
                    Thread.Sleep(2000);
                    hookoffcallback = false;

                }).Start();
            }
            // ConnectedCall
            else if (CallState.Equals("ConnectedCall"))
            {
                Ring = false;
                Mute = false;
                HookOff = true;
                LEDHookOffFunc(true);
                hookoffcallback = true;

                new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;
                    Thread.Sleep(2000);
                    hookoffcallback = false;

                }).Start();
            }
            else
            {
                Mute = false;
                HookOff = false;

                Thread t = new Thread(new ThreadStart(callendProcessor));
                t.Start();
            }
        }

        private void OnexCommSoftphoneMuteStateChanged(object sender, SoftPhoneMuteEventArgs e)
        {
            if (e.Mute)
            {

                Mute = true;

            }
            else
            {
                Mute = false;

            }
            LEDMuteFunc(Mute);
        }

        //=========================================================================================
        // IX Workplace Callback 
        //=========================================================================================
        private void IXSoftphoneRemoved(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            softphoneitemIXWorkplace.ISRUN = false;
            softphoneitemIXWorkplace.ISSELECTED = false;
        }

        private void IXSoftphoneStateChanged(object sender, IXSoftPhoneEventArgs e)
        {
            Pre_CallState = CallState;
            CallState = e.Status.ToString();
            System.Diagnostics.Debug.WriteLine("APP :: CallState {0}", CallState);
            if (CallState.Equals("textnull"))
            {
                System.Diagnostics.Debug.WriteLine("APP :: CallState textnull ");
                System.Windows.Forms.MessageBox.Show("Please Restart Avaya IX Workplace");
            }
            else if (CallState.Equals("IncommingCall"))
            {
                Ring = true;
                Thread t = new Thread(new ThreadStart(ringinProcessor));
                t.Start();
            }
            else if (CallState.Equals("OutgoingCall"))
            {
                Mute = false;
                HookOff = true;
                LEDHookOffFunc(true);
                hookoffcallback = true;

                new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;
                    Thread.Sleep(1000);
                    hookoffcallback = false;
                }).Start();
            }
            // ConnectedCall
            else if (CallState.Equals("ConnectedCall"))
            {
                Ring = false;
                Mute = false;
                HookOff = true;
                LEDHookOffFunc(true);
                hookoffcallback = true;

                new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;
                    Thread.Sleep(1000);
                    hookoffcallback = false;
                }).Start();

            }
            else
            {
                Mute = false;
                HookOff = false;

                Thread t = new Thread(new ThreadStart(callendProcessor));
                t.Start();
            }
        }

        private void IXSoftphoneMuteStateChanged(object sender, IXSoftPhoneMuteEventArgs e)
        {
            if (e.Mute)
            {

                Mute = true;

            }
            else
            {
                Mute = false;

            }
            LEDMuteFunc(Mute);
        }

        //=========================================================================================
        // Skype Callback not work 20181126
        //=========================================================================================
        private void skypeManager_MuteStateChanged(object sender, EventArgs e)
        {
            Mute = !Mute;
            LEDMuteFunc(Mute);
        }

        private void skypeManager_Removed(object sender, EventArgs e)
        {
            Mute = false;
            HookOff = false;
            softphoneitemSkype.ISSELECTED = false;
            //
        }

        private void skypeManager_CallStateChanged(object sender, SkypeSoftPhoneEventArgs e)
        {
            Console.WriteLine("============== skypeManager_CallStateChanged ================" + e.Status);
            // clsRouting
            if (e.Status.Equals("clsRinging"))
            {
                Console.WriteLine("============== clsRinging ================");

                Ring = true;
                Thread t = new Thread(new ThreadStart(ringinProcessor));
                t.Start();

            }
            else if (e.Status.Equals("clsCancelled"))
            {
                LEDRingFunc(false);
            }
            //clsInProgress
            else if (e.Status.Equals("clsInProgress"))
            {
                Ring = false;
                Mute = false;
                HookOff = true;
                LEDHookOffFunc(true);
                hookoffcallback = true;

                new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;
                    Thread.Sleep(2000);
                    hookoffcallback = false;

                }).Start();


            }
            else if (e.Status.Equals("clsFailed") || e.Status.Equals("clsFinished"))
            {

                Mute = false;
                HookOff = false;

                // 20180403 [[
                // wkyoon skpye issue hook off 

                Thread t = new Thread(new ThreadStart(callendProcessor));
                t.Start();
            }
            //
            else
            {
                LEDHookOffFunc(false);
            }
        }
        //==============================================================================
        //  Jabber :: front to 
        //==============================================================================
        private void FrontJabberApp()
        {
            string processName = "CiscoJabber";
            string processFilePath = @"C:\\Program Files\\Cisco Systems\\Cisco Jabber\\CiscoJabber.exe";
            // X86 checkk needed 
            //get the process
            Process bProcess = Process.GetProcessesByName(processName).FirstOrDefault();
            //check if the process is nothing or not.
            if (bProcess != null)
            {
                //get the  hWnd of the process
                IntPtr hwnd = bProcess.MainWindowHandle;
                if (hwnd == IntPtr.Zero)
                {
                    //the window is hidden so try to restore it before setting focus.
                    NativeMethods.ShowWindow(bProcess.Handle, 9);
                }

                //set user the focus to the window
                NativeMethods.SetForegroundWindow((IntPtr)bProcess.MainWindowHandle);
            }
            else
            {
                //tthe process is nothing, so start it
                //Process.Start(processName);
            }
        }

        private bool isInstallSkype8()
        {

            // wkyoon reqkey 에 version 정보를 저장하자. 
            //string fpath = "c:\\"+Environment.SpecialFolder.ProgramFilesX86 + @"\Microsoft\Skype for Desktop\Skype.exe";
            string fpath = @"C:\Program Files (x86)\Microsoft\Skype for Desktop\Skype.exe";

            if (File.Exists(fpath))
            {
                FileVersionInfo myFileVersionInfo = FileVersionInfo.GetVersionInfo(fpath);

                if (myFileVersionInfo.FileVersion.StartsWith("8"))
                {
                    return true;
                }
            }
            return false;
        }
        //=========================================================================================

        #endregion
        #region Timer

        private void timer_Tick(object sender, EventArgs e)
        {
            GetStrCurrentSoftphone();
        }

        private void timerKeyPressCheck(object sender, EventArgs e)
        {
            if (keypressedcount == 2)
            {
                if (Mute)
                    usb_ButtonMute();
                else
                    usb_ButtonMute();
            }
            else if (keypressedcount == 1)
            {
                if (lastHookCheckByte == 0x00)
                {
                    // hook on key
                    HookOff = false;
                    LEDHookOffFunc(HookOff);

                    usb_ButtonHook();
                }
            }
            keypressedcount = 0;
            timer_Keypress.Stop();
        }

        #endregion
        #region USB Call Function
        //==============================================================================
        //  USB Call Function
        //==============================================================================
        private void ringinProcessor()
        {

            System.Console.WriteLine("-----  hook on-----");
            if (softphoneitem3CX.ISRUN && softphoneitem3CX.ISSELECTED)
            {
                //LEDHookOffFunc(false);
                //Thread.Sleep(400);

                System.Console.WriteLine("----- mute off -----");
                LEDMuteFunc(false);
                Thread.Sleep(400);

                System.Console.WriteLine("----- ringing -----");
                LEDRingFunc(true);
            }
            else
            {
                LEDHookOffFunc(false);
                Thread.Sleep(400);

                System.Console.WriteLine("----- mute off -----");
                LEDMuteFunc(false);
                Thread.Sleep(400);

                System.Console.WriteLine("----- ringing -----");
                LEDRingFunc(true);
            }
        }

        private void callendProcessor()
        {

            if (softphoneitem3CX.ISRUN && softphoneitem3CX.ISSELECTED)
            {
                if (Pre_CallState.Equals("Ringing"))
                {
                    LEDRingFunc(false);
                    Thread.Sleep(400);
                }
                else
                {
                    LEDHookOffFunc(false);
                    Thread.Sleep(400);
                }

                System.Console.WriteLine("----- mute off -----");
                LEDMuteFunc(false);

            }
            else
            {
                LEDRingFunc(false);
                Thread.Sleep(400);
                System.Console.WriteLine("----- ringing hook on-----");
                LEDHookOffFunc(false);
                Thread.Sleep(400);

                System.Console.WriteLine("----- mute off -----");
                LEDMuteFunc(false);
            }
        }

        private void usb_ButtonMute()
        {
            if (softphoneitemMitel.ISRUN && softphoneitemMitel.ISSELECTED)
            {
                //mMitelManager
            }
            else if (softphoneitemTeams.ISRUN && softphoneitemTeams.ISSELECTED)
            {
                // Do nothing..
            }
            else if (softphoneitemSkype.ISRUN && softphoneitemSkype.ISSELECTED)
            {
                String status = mSkypeManager.getTCallStatus();
                if (status.Equals("clsInProgress"))
                {
                    Mute = mSkypeManager.isMute();
                    mSkypeManager.MuteFunc();
                    Mute = !Mute;
                    LEDMuteFunc(Mute);
                }
            }
            else if (softphoneitemOneX.ISRUN && softphoneitemOneX.ISSELECTED)
            {
                if (CallState.Equals("ConnectedCall") || CallState.Equals("OutgoingCall"))
                {
                    Mute = !Mute;
                    mOneXManager.MuteFunc(Mute);
                    LEDMuteFunc(Mute);
                }
            }
            else if (softphoneitemOneXComm.ISRUN && softphoneitemOneXComm.ISSELECTED)
            {
                if (CallState.Equals("ConnectedCall"))
                {
                    Mute = !Mute;
                    mOneXCommManager.MuteFunc(Mute);
                    LEDMuteFunc(Mute);
                }
            }
            else if (softphoneitemIXWorkplace.ISRUN && softphoneitemIXWorkplace.ISSELECTED)
            {
                if (CallState.Equals("ConnectedCall"))
                {
                    Mute = !Mute;
                    mIXWorkplace.MuteFunc(Mute);
                    LEDMuteFunc(Mute);
                }
            }
            else if (softphoneitem3CX.ISRUN && softphoneitem3CX.ISSELECTED)
            {
                if (CallState.Equals("Connected"))
                {
                    Mute = !Mute;
                    m_3CXManager.MuteFunc();
                    LEDMuteFunc(Mute);
                }
            }
            else if (softphoneitemJabber.ISRUN && softphoneitemJabber.ISSELECTED)
            {
                Mute = !Mute;
                FrontJabberApp();
                CustomKey.CtrlPlusKey(Keys.Down);
                LEDMuteFunc(Mute);
            }
            else if (softphoneitemBria.ISRUN && softphoneitemBria.ISSELECTED)
            {
                Mute = !Mute;
                mBriaManager.MuteFunc(Mute);
                LEDMuteFunc(Mute);
            }
            else if (softphoneitemBria6.ISRUN && softphoneitemBria6.ISSELECTED)
            {
                Mute = !Mute;
                LEDMuteFunc(Mute);
                mBria6Manager.MuteFunc(Mute);
            }
        }

        private void usb_ButtonHook()
        {
            Console.WriteLine("---------- usb_ButtonHook");
            if (softphoneitemMitel.ISRUN && softphoneitemMitel.ISSELECTED)
            {
                //Console.WriteLine("xxxxxxxxxxxxxxxxx " + CallState);

                if (CallState.Equals("Ringing"))
                {

                    mMitelManager.AnserCall();
                    // connectedfrm.debugLog2("call connect 1");
                    HookOff = true;
                    Mute = false;
                }
                // ConnectedCall
                else if (CallState.Equals("OffHook"))
                {
                    mMitelManager.HangupCall();
                    //  connectedfrm.debugLog2("call connect 2");
                    HookOff = false;
                    Mute = false;
                }
            }
            else if (softphoneitemSkype.ISRUN && softphoneitemSkype.ISSELECTED)
            {
                String status = mSkypeManager.getTCallStatus();
                if (status.Equals("clsRinging"))
                {
                    mSkypeManager.AnswerFunc();
                    HookOff = true;
                    Mute = false;
                }
                else if (status.Equals("clsInProgress"))
                {
                    mSkypeManager.AnswerFunc();
                    HookOff = false;
                    Mute = false;
                }
                // clsFinished
                else //if (status.Equals("clsFinished"))
                {
                    //HookOff = false;
                }
            }
            else if (softphoneitemOneX.ISRUN && softphoneitemOneX.ISSELECTED)
            {

                if (CallState.Equals("IncommingCall"))
                {
                    Console.WriteLine("xxxxxxxxxx 1");
                    mOneXManager.CallFunc();
                    // connectedfrm.debugLog2("call connect 1");
                    HookOff = true;
                    Mute = false;
                }
                else if (CallState.Equals("OutgoingCall"))
                {
                    Console.WriteLine("xxxxxxxxxx 2");
                    mOneXManager.CallFunc();
                    //  connectedfrm.debugLog2("call connect 2");
                    HookOff = false;
                    Mute = false;
                }
                // ConnectedCall
                else if (CallState.Equals("ConnectedCall"))
                {
                    Console.WriteLine("xxxxxxxxxx 3");
                    mOneXManager.CallFunc();
                    //  connectedfrm.debugLog2("call connect 2");
                    HookOff = false;
                    Mute = false;
                }
            }
            else if (softphoneitemOneXComm.ISRUN && softphoneitemOneXComm.ISSELECTED)
            {
                if (CallState.Equals("IncommingCall"))
                {
                    mOneXCommManager.CallFunc(true);
                    HookOff = true;
                    Mute = false;
                }
                else if (CallState.Equals("ConnectedCall"))
                {
                    mOneXCommManager.CallFunc(false);
                    HookOff = false;
                    Mute = false;
                }


            }
            else if (softphoneitemIXWorkplace.ISRUN && softphoneitemIXWorkplace.ISSELECTED)
            {
                Console.WriteLine("Hook is pressed");
                if (CallState.Equals("IncommingCall"))
                {
                    Console.WriteLine("xxxxxxxxxx 1");
                    mIXWorkplace.CallFunc();
                    // connectedfrm.debugLog2("call connect 1");
                    HookOff = true;
                    Mute = false;
                }
                else if (CallState.Equals("OutgoingCall"))
                {
                    Console.WriteLine("xxxxxxxxxx 2");
                    mIXWorkplace.CallFunc();
                    //  connectedfrm.debugLog2("call connect 2");
                    HookOff = false;
                    Mute = false;
                }
                // ConnectedCall
                else if (CallState.Equals("ConnectedCall"))
                {
                    Console.WriteLine("xxxxxxxxxx 3");
                    mIXWorkplace.CallFunc();
                    //  connectedfrm.debugLog2("call connect 2");
                    HookOff = false;
                    Mute = false;
                }
                else
                {
                    Console.WriteLine("xxxxxxxxxx 4");
                    mIXWorkplace.CallFunc();
                    //  connectedfrm.debugLog2("call connect 2");
                    HookOff = false;
                    Mute = false;
                }
            }
            else if (softphoneitem3CX.ISRUN && softphoneitem3CX.ISSELECTED)
            {
                // Ringing
                if (CallState.Equals("Ringing"))
                {
                    m_3CXManager.CallFunc();
                    HookOff = true;
                    Mute = false;
                }
                else if (CallState.Equals("Connected"))
                {
                    m_3CXManager.CallFunc();
                    HookOff = false;
                    Mute = false;
                }
                // Dialing
                else if (CallState.Equals("Dialing"))
                {
                    m_3CXManager.CallFunc();
                    HookOff = false;
                    Mute = false;
                }

            }
            else if (softphoneitemJabber.ISRUN && softphoneitemJabber.ISSELECTED)
            {
                if (mJabberManager.getCallStatus().Equals("onIncoming"))
                {
                    FrontJabberApp();
                    CustomKey.CtrlPlusKey(Keys.L);
                    HookOff = true;
                }
                else if (mJabberManager.getCallStatus().Equals("onConnected"))
                {

                    FrontJabberApp();
                    CustomKey.CtrlPlusKey(Keys.K);
                    HookOff = false;
                    Mute = false;
                }
            }
            else if (softphoneitemBria.ISRUN && softphoneitemBria.ISSELECTED)
            {
                if (CallState.Equals("IsRinging"))
                {
                    mBriaManager.CallFunc();
                    HookOff = true;
                }
                // Connected
                else if (CallState.Equals("Connected"))
                {
                    mBriaManager.CallFunc();
                    HookOff = false;
                    Mute = false;
                }
            }
            else if (softphoneitemBria6.ISRUN && softphoneitemBria6.ISSELECTED)
            {
                if (CallState.Equals("IsRinging"))
                {
                    mBria6Manager.CallFunc();
                    HookOff = true;
                }
                // Connected
                else if (CallState.Equals("Connected"))
                {
                    mBria6Manager.CallFunc();
                    HookOff = false;
                    Mute = false;
                }
            }
        }
        #endregion
        #region Call Control
        //==============================================================================
        //  HID Call Control
        //==============================================================================
        private void LEDMuteFunc(bool mute)
        {
            if (mute)
            {
                byte[] writeData = { 0x03, 0x01, 0x00 };
                if (device0 != null)
                    device0.write(writeData);
            }
            else
            {
                byte[] writeData = { 0x03, 0x00, 0x00 };
                if (device0 != null)
                    device0.write(writeData);
            }
        }

        private void LEDHookOffFunc(bool hookoff)
        {
            if (hookoff)
            {
                byte[] writeData = { 0x02, 0x01, 0x00 };
                if (device0 != null)
                    device0.write(writeData);
            }
            else
            {
                byte[] writeData = { 0x02, 0x00, 0x00 };
                if (device0 != null)
                    device0.write(writeData);
            }
        }

        private void LEDRingFunc(bool ring)
        {
            if (ring)
            {
                byte[] writeData = { 0x04, 0x01, 0x00 };
                if (device0 != null)
                    device0.write(writeData);
            }
            else
            {
                byte[] writeData = { 0x04, 0x00, 0x00 };
                if (device0 != null)
                    device0.write(writeData);
            }
        }

        #endregion
    }
}