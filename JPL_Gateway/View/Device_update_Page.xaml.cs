using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.IO;
using System.Drawing;

namespace JPL_Gateway.View
{
    /// <summary>
    /// Device_update_Page.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Device_update_Page : Page
    {
        internal delegate void sendCommandDele(string text);
        //internal event sendCommandDele sendCommand;

        //private string softlinkversion;
        public string connecteddevicename;
        private string connecteddevicePID;
        private string connecteddeviceVID;
        private string connecteddeviceversion;
        //private int selectedindex;

        private string updatebinname;
        //private string updatesetupexe;

        public string commonAppData = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
        public string statusfile;// = commonAppData + "\\SoftLinkDeviceST";

        //private bool isCheckUpdates;

        private string msg1;
        private string msg2;
        private bool msg_state;

        private JObject json;
        private JObject[] jsonArr;
        private string mBinFileName = "";

        private DispatcherTimer timer;

        internal static Device_update_Page deviceupdatepage;

        public Device_update_Page()
        {
            InitializeComponent();

            jsonArr = new JObject[2];

            msg_state = false;
            statusfile = commonAppData + "\\SoftLinkDeviceST";

            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = TimeSpan.FromMilliseconds(1000);
        }


        public void CheckComboBox()
        {
            if (connecteddevicename.Length == 0)
                return;

            if (connecteddevicename.Length > 0)
            {
                softphone.Items.Clear();
                add_combobox("MS Teams");
                softphone.SelectedIndex = 0;
            }

            if (connecteddevicename.Contains("DSU-09"))
            {
                if ((connecteddeviceVID.Equals("2EA1") && connecteddevicePID.Equals("0121")) ||
                    (connecteddeviceVID.Equals("2EA1") && connecteddevicePID.Equals("012D")) ||
                    (connecteddeviceVID.Equals("0B0E") && connecteddevicePID.Equals("012B")))
                {
                    softphone.Items.Clear();
                    add_combobox("MS Teams");
                    add_combobox("Cisco IP Communicator");
                    add_combobox("Cisco Jabber");
                    if (connecteddeviceVID.Equals("2EA1") && connecteddevicePID.Equals("012D"))
                    {
                        currentversion.Text = connecteddeviceversion + " : Cisco IP Communicator";
                        softphone.SelectedIndex = 1;
                    }
                    else if (connecteddeviceVID.Equals("0B0E") && connecteddevicePID.Equals("012B"))
                    {
                        currentversion.Text = connecteddeviceversion + " : Cisco Jabber";
                        softphone.SelectedIndex = 2;
                    }
                    else
                    {
                        currentversion.Text = connecteddeviceversion + " : MS Teams";
                        softphone.SelectedIndex = 0;
                    }
                }
            }
            else if (connecteddevicename.Contains("DSU-10"))
            {
                if ((connecteddeviceVID.Equals("2EA1") && connecteddevicePID.Equals("0131")) ||
                    (connecteddeviceVID.Equals("2EA1") && connecteddevicePID.Equals("013B")))
                {
                    softphone.Items.Clear();
                    add_combobox("MS Teams");
                    add_combobox("Cisco IP Communicator");
                    if (connecteddeviceVID.Equals("2EA1") && connecteddevicePID.Equals("013B"))
                    {
                        currentversion.Text = connecteddeviceversion + " : Cisco IP Communicator";
                        softphone.SelectedIndex = 1;
                    }
                    else
                    {
                        currentversion.Text = connecteddeviceversion + " : MS Teams";
                        softphone.SelectedIndex = 0;
                    }
                }
            }
            else if (connecteddevicename.Contains("BL-054"))
            {
                if ((connecteddeviceVID.Equals("2EA1") && connecteddevicePID.Equals("0142")) ||
                    (connecteddeviceVID.Equals("0B0E") && connecteddevicePID.Equals("0142")))
                {
                    softphone.Items.Clear();
                    add_combobox("MS Teams");
                    add_combobox("Cisco Jabber");
                    if (connecteddeviceVID.Equals("0B0E") && connecteddevicePID.Equals("0142"))
                    {
                        currentversion.Text = connecteddeviceversion + " : Cisco Jabber";
                        softphone.SelectedIndex = 1;
                    }
                    else
                    {
                        currentversion.Text = connecteddeviceversion + " : MS Teams";
                        softphone.SelectedIndex = 0;
                    }
                }
            }
            else if (connecteddevicename.Equals("JPL-400-USB"))
            {
                if ((connecteddeviceVID.Equals("2EA1") && connecteddevicePID.Equals("0129")) ||
                    (connecteddeviceVID.Equals("0B0E") && connecteddevicePID.Equals("0129")))
                {
                    softphone.Items.Clear();
                    add_combobox("MS Teams");
                    add_combobox("Cisco Jabber");
                    if (connecteddeviceVID.Equals("0B0E") && connecteddevicePID.Equals("0129"))
                    {
                        currentversion.Text = connecteddeviceversion + " : Cisco Jabber";
                        softphone.SelectedIndex = 1;
                    }
                    else
                    {
                        currentversion.Text = connecteddeviceversion + " : MS Teams";
                        softphone.SelectedIndex = 0;
                    }
                }
            }
            else if (connecteddevicename.Equals("DSU-2x"))
            {
                if (connecteddeviceVID.Equals("2EA1") && connecteddevicePID.Equals("0700"))
                {
                    softphone.Items.Clear();
                    add_combobox("MS Teams");
                    add_combobox("ZULU");
                    currentversion.Text = connecteddeviceversion + " : MS Teams";
                    softphone.SelectedIndex = 0;
                }
            }
            else if (connecteddevicename.Contains("XXX1-"))
            {
                if (connecteddeviceVID.Equals("3015") && connecteddevicePID.Equals("0011"))
                {
                    softphone.Items.Clear();
                    add_combobox("MS Teams");
                    add_combobox("ZULU");
                    currentversion.Text = connecteddeviceversion + " : ZULU";
                    softphone.SelectedIndex = 1;
                }
            }
            else if (connecteddevicename.Contains("DW-800U"))
            {
                if (connecteddeviceVID.Equals("2EA1") && connecteddevicePID.Equals("0220"))
                {
                    softphone.Items.Clear();
                    add_combobox("MS Teams");
                    add_combobox("Cisco Jabber");
                    currentversion.Text = connecteddeviceversion + " : MS Teams";
                    softphone.SelectedIndex = 0;
                }
                else if (connecteddeviceVID.Equals("0B0E") && connecteddevicePID.Equals("0220"))
                {
                    softphone.Items.Clear();
                    add_combobox("MS Teams");
                    add_combobox("Cisco Jabber");
                    currentversion.Text = connecteddeviceversion + " : Cisco Jabber";
                    softphone.SelectedIndex = 1;
                }
            }
            else if (connecteddevicename.Contains("X-500U"))
            {
                if (connecteddeviceVID.Equals("2EA1") && connecteddevicePID.Equals("0221"))
                {
                    softphone.Items.Clear();
                    add_combobox("MS Teams");
                    add_combobox("Cisco Jabber");
                    currentversion.Text = connecteddeviceversion + " : MS Teams";
                    softphone.SelectedIndex = 0;
                }
                else if (connecteddeviceVID.Equals("0B0E") && connecteddevicePID.Equals("0221"))
                {
                    softphone.Items.Clear();
                    add_combobox("MS Teams");
                    add_combobox("Cisco Jabber");
                    currentversion.Text = connecteddeviceversion + " : Cisco Jabber";
                    softphone.SelectedIndex = 1;
                }
            }
            else if (connecteddevicename.Contains("Discover Adapt30"))
            {
                if (connecteddeviceVID.Equals("2EA1") && connecteddevicePID.Equals("0222"))
                {
                    softphone.Items.Clear();
                    add_combobox("MS Teams");
                    add_combobox("Cisco Jabber");
                    currentversion.Text = connecteddeviceversion + " : MS Teams";
                    softphone.SelectedIndex = 0;
                }
                else if (connecteddeviceVID.Equals("0B0E") && connecteddevicePID.Equals("0222"))
                {
                    softphone.Items.Clear();
                    add_combobox("MS Teams");
                    add_combobox("Cisco Jabber");
                    currentversion.Text = connecteddeviceversion + " : Cisco Jabber";
                    softphone.SelectedIndex = 1;
                }
            }
            else if (connecteddevicename.Contains("JPL-Explore"))
            {
                if (connecteddeviceVID.Equals("2EA1") && connecteddevicePID.Equals("0225"))
                {
                    softphone.Items.Clear();
                    add_combobox("MS Teams");
                    add_combobox("Cisco Jabber");
                    currentversion.Text = connecteddeviceversion + " : MS Teams";
                    softphone.SelectedIndex = 0;
                }
                else if (connecteddeviceVID.Equals("0B0E") && connecteddevicePID.Equals("0225"))
                {
                    softphone.Items.Clear();
                    add_combobox("MS Teams");
                    add_combobox("Cisco Jabber");
                    currentversion.Text = connecteddeviceversion + " : Cisco Jabber";
                    softphone.SelectedIndex = 1;
                }
            }
            else if (connecteddevicename.Contains("BT-220"))
            {
                if ((connecteddeviceVID.Equals("2EA1") && connecteddevicePID.Equals("0421")) ||
                    (connecteddeviceVID.Equals("05A6") && connecteddevicePID.Equals("001A")))
                {
                    softphone.Items.Clear();
                    add_combobox("MS Teams");
                    add_combobox("Cisco Webex");
                    if (connecteddeviceVID.Equals("05A6") && connecteddevicePID.Equals("001A"))
                    {
                        currentversion.Text = connecteddeviceversion + " : Cisco Webex";
                        softphone.SelectedIndex = 1;
                    }
                    else
                    {
                        currentversion.Text = connecteddeviceversion + " : MS Teams";
                        softphone.SelectedIndex = 0;
                    }
                }
            }
        }

        public void GetSupportedSoftphone()
        {
            string devvid = connecteddeviceVID;
            string devpid = connecteddevicePID;
            string devver = connecteddeviceversion;

            Console.WriteLine("Original : PID={0}, VID={1}, Version={2}", devpid, devvid, devver);
            if (connecteddevicename.Length == 0)
                return;

            if (connecteddevicename.Contains("DSU-09"))
            {
                if (connecteddeviceVID.Equals("2EA1") && connecteddevicePID.Equals("0121"))
                {
                    // Preferred Softphone : MS Teams -> Cisco IP Communicator
                    if (softphone.SelectedIndex == 1)
                    {
                        devvid = "2EA1";
                        devpid = "012D";
                        devver = "0001";
                    }
                    // Preferred Softphone : MS Teams -> Cisco IP Jabber
                    else if (softphone.SelectedIndex == 2)
                    {
                        devvid = "0B0E";
                        devpid = "012B";
                        devver = "0001";
                    }
                }
                else if (connecteddeviceVID.Equals("2EA1") && connecteddevicePID.Equals("012D"))
                {
                    // Preferred Softphone : Cisco IP Communicator -> MS Teams
                    if (softphone.SelectedIndex == 0)
                    {
                        devvid = "2EA1";
                        devpid = "0121";
                        devver = "0001";
                    }
                    // Preferred Softphone : Cisco IP Communicator -> Cisco IP Jabber
                    else if (softphone.SelectedIndex == 2)
                    {
                        devvid = "0B0E";
                        devpid = "012B";
                        devver = "0001";
                    }
                }
                else if (connecteddeviceVID.Equals("0B0E") && connecteddevicePID.Equals("012B"))
                {
                    // Preferred Softphone : Cisco Jabber -> MS Teams
                    if (softphone.SelectedIndex == 0)
                    {
                        devvid = "2EA1";
                        devpid = "0121";
                        devver = "0001";
                    }
                    // Preferred Softphone : Cisco Jabber -> Cisco IP Communicator
                    else if (softphone.SelectedIndex == 1)
                    {
                        devvid = "2EA1";
                        devpid = "012D";
                        devver = "0001";
                    }
                }
            }
            else if (connecteddevicename.Contains("DSU-10"))
            {
                if (connecteddeviceVID.Equals("2EA1") && connecteddevicePID.Equals("0131"))
                {
                    // Preferred Softphone : MS Teams -> Cisco IP Communicator
                    if (softphone.SelectedIndex == 1)
                    {
                        devvid = "2EA1";
                        devpid = "013B";
                        devver = "0001";
                    }
                }
                else if (connecteddeviceVID.Equals("2EA1") && connecteddevicePID.Equals("013B"))
                {
                    // Preferred Softphone : Cisco IP Communicator -> MS Teams
                    if (softphone.SelectedIndex == 0)
                    {
                        devvid = "2EA1";
                        devpid = "0131";
                        devver = "0001";
                    }
                }
            }
            else if (connecteddevicename.Contains("BL-054"))
            {
                if (connecteddeviceVID.Equals("2EA1") && connecteddevicePID.Equals("0142"))
                {
                    // Preferred Softphone : MS Teams -> Cisco Jabber
                    if (softphone.SelectedIndex == 1)
                    {
                        devvid = "0B0E";
                        devpid = "0142";
                        devver = "0001";
                    }
                }
                else if (connecteddeviceVID.Equals("0B0E") && connecteddevicePID.Equals("0142"))
                {
                    // Preferred Softphone : Cisco Jabber -> MS Teams
                    if (softphone.SelectedIndex == 0)
                    {
                        devvid = "2EA1";
                        devpid = "0142";
                        devver = "0001";
                    }
                }
            }
            else if (connecteddevicename.Equals("JPL-400-USB"))
            {
                if (connecteddeviceVID.Equals("2EA1") && connecteddevicePID.Equals("0129"))
                {
                    // Preferred Softphone : MS Teams -> Cisco Jabber
                    if (softphone.SelectedIndex == 1)
                    {
                        devvid = "0B0E";
                        devpid = "0129";
                        devver = "0001";
                    }
                }
                else if (connecteddeviceVID.Equals("0B0E") && connecteddevicePID.Equals("0129"))
                {
                    // Preferred Softphone : Cisco Jabber -> MS Teams
                    if (softphone.SelectedIndex == 0)
                    {
                        devvid = "2EA1";
                        devpid = "0129";
                        devver = "0001";
                    }
                }
            }
            else if (connecteddevicename.Equals("DSU-2x") || connecteddevicename.Contains("XXX1-"))
            {
                if (connecteddeviceVID.Equals("2EA1") && connecteddevicePID.Equals("0700"))
                {
                    // Preferred Softphone : MS Teams -> ZULU
                    if (softphone.SelectedIndex == 1)
                    {
                        devvid = "3015";
                        devpid = "0011";
                        devver = "0001";
                    }
                }
                else if (connecteddeviceVID.Equals("3015") && connecteddevicePID.Equals("0011"))
                {
                    // Preferred Softphone : ZULU -> MS Teams
                    if (softphone.SelectedIndex == 0)
                    {
                        devvid = "2EA1";
                        devpid = "0700";
                        devver = "0001";
                    }
                }
            }
            else if (connecteddevicename.Contains("DW-800U"))
            {
                if (connecteddeviceVID.Equals("2EA1") && connecteddevicePID.Equals("0220"))
                {
                    // Preferred Softphone : MS Teams -> Cisco Jabber
                    if (softphone.SelectedIndex == 1)
                    {
                        devvid = "0B0E";
                        devpid = "0220";
                        devver = "0001";
                    }
                }
                else if (connecteddeviceVID.Equals("0B0E") && connecteddevicePID.Equals("0220"))
                {
                    // Preferred Softphone : Cisco Jabber -> MS Teams
                    if (softphone.SelectedIndex == 0)
                    {
                        devvid = "2EA1";
                        devpid = "0220";
                        devver = "0001";
                    }
                }
            }
            else if (connecteddevicename.Contains("X-500U"))
            {
                if (connecteddeviceVID.Equals("2EA1") && connecteddevicePID.Equals("0221"))
                {
                    // Preferred Softphone : MS Teams -> Cisco Jabber
                    if (softphone.SelectedIndex == 1)
                    {
                        devvid = "0B0E";
                        devpid = "0221";
                        devver = "0001";
                    }
                }
                else if (connecteddeviceVID.Equals("0B0E") && connecteddevicePID.Equals("0221"))
                {
                    // Preferred Softphone : Cisco Jabber -> MS Teams
                    if (softphone.SelectedIndex == 0)
                    {
                        devvid = "2EA1";
                        devpid = "0221";
                        devver = "0001";
                    }
                }
            }
            else if (connecteddevicename.Contains("Discover Adapt30"))
            {
                if (connecteddeviceVID.Equals("2EA1") && connecteddevicePID.Equals("0222"))
                {
                    // Preferred Softphone : MS Teams -> Cisco Jabber
                    if (softphone.SelectedIndex == 1)
                    {
                        devvid = "0B0E";
                        devpid = "0222";
                        devver = "0001";
                    }
                }
                else if (connecteddeviceVID.Equals("0B0E") && connecteddevicePID.Equals("0222"))
                {
                    // Preferred Softphone : Cisco Jabber -> MS Teams
                    if (softphone.SelectedIndex == 0)
                    {
                        devvid = "2EA1";
                        devpid = "0222";
                        devver = "0001";
                    }
                }
            }
            else if (connecteddevicename.Contains("JPL-Explore"))
            {
                if (connecteddeviceVID.Equals("2EA1") && connecteddevicePID.Equals("0225"))
                {
                    // Preferred Softphone : MS Teams -> Cisco Jabber
                    if (softphone.SelectedIndex == 1)
                    {
                        devvid = "0B0E";
                        devpid = "0225";
                        devver = "0001";
                    }
                }
                else if (connecteddeviceVID.Equals("0B0E") && connecteddevicePID.Equals("0225"))
                {
                    // Preferred Softphone : Cisco Jabber -> MS Teams
                    if (softphone.SelectedIndex == 0)
                    {
                        devvid = "2EA1";
                        devpid = "0225";
                        devver = "0001";
                    }
                }
            }
            // BTD-1000 Series ======================================================================
            else if (connecteddevicename.Contains("BT-220"))
            {
                if (connecteddeviceVID.Equals("2EA1") && connecteddevicePID.Equals("0421"))
                {
                    // Preferred Softphone : MS Teams -> Cisco Webex
                    if (softphone.SelectedIndex == 1)
                    {
                        devvid = "05A6";
                        devpid = "001A";
                        devver = "0001";
                    }
                }
                else if (connecteddeviceVID.Equals("05A6") && connecteddevicePID.Equals("001A"))
                {
                    // Preferred Softphone : Cisco Webex -> MS Teams
                    if (softphone.SelectedIndex == 0)
                    {
                        devvid = "2EA1";
                        devpid = "0421";
                        devver = "0001";
                    }
                }
            }



            Console.WriteLine("xxx Modified : PID={0}, VID={1}, Version={2}", devpid, devvid, devver);

            // swyoon
            //json = UpdateSoftphone.checkUpdateVersion(devvid, devpid, devver);

            json = UpdateService.checkUpdateVersionBinary(devvid, devpid, devver);
            if (json != null)
            {
                jsonArr[0] = json;
                if (json["update"].ToString().Equals("True"))
                {
                    mBinFileName = json["filename"].ToString();

                    Update_device_name.Text = connecteddevicename + " (" + json["versionname"].ToString() + ")";
                    latestversion.Text = json["versionname"].ToString();

                    setBinFile(mBinFileName);
                    setUpdateInfo(0, json["desc"].ToString());
                }
                else
                {
                    Update_device_name.Text = connecteddevicename + " (" + devver + ")";
                    latestversion.Text = devver;
                }
            }
            setArrJson(jsonArr);
        }

        private void add_combobox(string softphonename)
        {
            StackPanel stkbox = new StackPanel();
            stkbox.Orientation = Orientation.Horizontal;
            stkbox.Height = 20;
            softphone.Items.Add(stkbox);

            TextBlock stkboxtext = new TextBlock();
            stkboxtext.Text = softphonename;
            stkboxtext.FontSize = 13;
            stkboxtext.Width = 121;
            stkboxtext.TextAlignment = TextAlignment.Center;
            stkbox.Children.Add(stkboxtext);
        }

        public void setConnectedDevice(string devicename, string vid, string pid, string version)
        {
            connecteddevicename = devicename;
            connecteddeviceversion = version;
            connecteddeviceVID = vid;
            connecteddevicePID = pid;
        }

        public void setBinFile(string binfile)
        {
            if (binfile != null)
            {
                if (binfile.Length > 0)
                {
                    updatebinname = binfile;
                }
            }
        }

        public void setUpdateInfo(int idx, string msg)
        {
            if (idx == 0)
                msg1 = msg;
            else if (idx == 1)
                msg2 = msg;

            lbUpdateInfo.Content = msg;
            MsgBox.FW_Upgrade.fwupgradebox.updateinfo.Text = msg;
        }

        public void setArrJson(JObject[] arrjson)
        {
            this.jsonArr = arrjson;

            if (jsonArr[0] != null)
            {
                //Console.WriteLine("Softlink = {0}", jsonArr[1]["update"].ToString());
                if (jsonArr[0]["update"].ToString().Equals("True"))
                {
                    FirmwareBtn.Style = Resources["Update_Now"] as Style;
                    UpdateInfo_grid.Visibility = Visibility.Visible;
                }
                else
                {
                    FirmwareBtn.Style = Resources["Up_to_date"] as Style;
                    UpdateInfo_grid.Visibility = Visibility.Hidden;
                }
            }
        }

        public void checkUpdateVersion(int devicenum)
        {
            Console.WriteLine("checkUpdateVersion");
            string devPID = MainWindow.mainwindow.devices[devicenum].PID.ToString("X4");
            string devVID = MainWindow.mainwindow.devices[devicenum].VID.ToString("X4");
            string devVER = MainWindow.mainwindow.devices[devicenum].versionNumber.ToString("X4");

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
                    mBinFileName = json["filename"].ToString();

                    Update_device_name.Text = connecteddevicename + " (" + json["versionname"].ToString() + ")";
                    latestversion.Text = json["versionname"].ToString();

                    setBinFile(mBinFileName);
                    setUpdateInfo(0, json["desc"].ToString());
                }
                else
                {
                    Update_device_name.Text = connecteddevicename + " (" + devVER + ")";
                    latestversion.Text = devVER;
                }
            }
            setArrJson(jsonArr);
        }

        public void firmware_upgrade()
        {
            if (jsonArr[0]["update"].ToString().Equals("True"))
            {
                Console.WriteLine(connecteddevicePID);
                Process SoftlinkDownloader = new Process();
                SoftlinkDownloader.StartInfo.FileName = "Gateway_Downloader.exe";

                if (connecteddevicePID != null)
                {
                    if (connecteddevicePID.Equals("0422") || connecteddevicePID.Equals("0423") || connecteddevicePID.Equals("0425"))
                    {
                        SoftlinkDownloader.StartInfo.FileName = "BTUpdater_Gateway.exe";
                    }

                    if (connecteddevicePID.Equals("0420") || connecteddevicePID.Equals("0421") || connecteddevicePID.Equals("0424") || connecteddevicePID.Equals("001A"))
                    {
                        SoftlinkDownloader.StartInfo.FileName = "BTDDFU_Gateway.exe";
                        MainWindow.BTisUpgrading = true;

                    }
                    else if (connecteddevicePID[0] == '1')
                    {
                        SoftlinkDownloader.StartInfo.FileName = "Gateway_DownloaderXX.exe";
                    }
                }

                FileInfo fileInfo = new FileInfo(SoftlinkDownloader.StartInfo.FileName);
                if (!fileInfo.Exists)
                    SoftlinkDownloader.StartInfo.FileName = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + "\\" + MainWindow.SoftLinkPath + "\\" + SoftlinkDownloader.StartInfo.FileName;
                Console.WriteLine("binary file : " + updatebinname);

                SoftlinkDownloader.StartInfo.Arguments = updatebinname;
                Console.WriteLine("update file : " + updatebinname);
                if (updatebinname != null)
                {
                    if (updatebinname.Length > 0)
                    {

                        try
                        {
                            SoftlinkDownloader.Start();
                            timer.Start();
                        }
                        catch (System.ComponentModel.Win32Exception)
                        {
                            timer.Stop();
                            Console.WriteLine("Timer stop (Btn Click)");
                            //lbWarnning.Visible = false;
                        }

                        if (File.Exists(statusfile))
                        {
                            Console.WriteLine("status file exist ");
                        }
                    }
                    else
                    {
                    }
                }
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (msg_state == false)
            {
                msg_state = true;
                MainWindow.mainwindow.sidebutton.IsEnabled = false;
                MsgBox.Upgrade.upgrade.upgrage_state(false);
                MainWindow.mainwindow.frame5.Content = MsgBox.Upgrade.upgrade;
            }
            Console.WriteLine("Timer start");
            if (File.Exists(statusfile))
            {
                string text = System.IO.File.ReadAllText(statusfile);
                Console.WriteLine(text);
                if (text.StartsWith("close"))
                {
                    //완료 된 경우
                    timer.Stop();
                    Console.WriteLine("Timer stop");
                    MainWindow.BTisUpgrading = false;
                    msg_state = false;
                    MainWindow.mainwindow.sidebutton.IsEnabled = true;
                    MsgBox.Upgrade.upgrade.upgrage_state(true);
                    FirmwareBtn.Style = Resources["Up_to_date"] as Style;
                }
            }
            else
            {
                timer.Stop();
                msg_state = false;
                Console.WriteLine("Timer stop else");
            }
        }

        private void FirmwareBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.mainwindow.frame1.Opacity = 0.6;
            MainWindow.mainwindow.frame5.IsEnabled = true;
            MainWindow.mainwindow.frame5.Visibility = Visibility.Visible;
            MainWindow.mainwindow.frame5.Content = MsgBox.FW_Upgrade.fwupgradebox;
            MainWindow.mainwindow.frame1.IsEnabled = false;
        }

        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.mainwindow.frame1.Content = Select_device_Page.selectdevicepage;
        }

        private void softphone_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GetSupportedSoftphone();
        }
    }
}