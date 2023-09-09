using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using NAudio.CoreAudioApi;
using NAudio.Wave;


namespace JPL_Gateway.View
{
    /// <summary>
    /// Interaction logic for Trouble_Page.xaml
    /// </summary>
    public partial class Trouble_Page : Page
    {
        internal static Trouble_Page trouble;

        private MMDevice selected_render_Device;
        private MMDevice selected_capture_Device;

        private AudioMeterInformation meterInformation_render;
        private AudioMeterInformation meterInformation_capture;

        private WaveOutEvent _waveOutEvent;
        private WaveInEvent _waveInEvent;
        private AudioFileReader audioFileReader;

        private WaveFileWriter _waveWriter;

        public Trouble_Page()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // Initialize the deviceComboBox with the available audio devices
            MMDeviceEnumerator enumerator = new MMDeviceEnumerator();
            foreach (MMDevice device in enumerator.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active))
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = device.FriendlyName;
                item.Tag = device;
                comboBox1.Items.Add(item);
            }

            foreach (MMDevice device in enumerator.EnumerateAudioEndPoints(DataFlow.Capture, DeviceState.Active))
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = device.FriendlyName;
                item.Tag = device;
                comboBox2.Items.Add(item);
            }

            if (Connected_Page.conpage.DeviceName1.Text != null)
            {
                for (int i = 0; i < comboBox1.Items.Count; i++)
                {
                    if (comboBox1.Items[i].ToString().Contains(Connected_Page.conpage.DeviceName1.Text))
                    {
                        comboBox1.SelectedIndex = i;
                        break;
                    }
                }

                for (int i = 0; i < comboBox2.Items.Count; i++)
                {
                    if (comboBox2.Items[i].ToString().Contains(Connected_Page.conpage.DeviceName1.Text))
                    {
                        comboBox2.SelectedIndex = i;
                        break;
                    }
                }
            }
        }

        private void RenderDeviceComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Get the selected device and its meter information
            ComboBoxItem selectedItem = (ComboBoxItem)comboBox1.SelectedItem;
            selected_render_Device = (MMDevice)selectedItem.Tag;
            meterInformation_render = selected_render_Device.AudioMeterInformation;

            // Update the outputLevelProgressBar based on the meter information
            //CompositionTarget.Rendering += UpdateOutput1Level;
            UpdateOutput1Level(null, null); // Call the UpdateOutputLevel method to update the progress bar immediately
        }

        private void CaptureDeviceComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Get the selected device and its meter information
            ComboBoxItem selectedItem = (ComboBoxItem)comboBox2.SelectedItem;
            selected_capture_Device = (MMDevice)selectedItem.Tag;
            meterInformation_capture = selected_capture_Device.AudioMeterInformation;

            // Update the outputLevelProgressBar based on the meter information
            //CompositionTarget.Rendering += UpdateOutput2Level;
            UpdateOutput2Level(null, null); // Call the UpdateOutputLevel method to update the progress bar immediately
        }

        private void UpdateOutput1Level(object sender, object e)
        {
            if (selected_render_Device.DataFlow == DataFlow.Render)
            {
                // Update the outputLevelProgressBar based on the selected render device's meter information
                progressBar1.Value = meterInformation_render.MasterPeakValue * 100;
            }
        }

        private void UpdateOutput2Level(object sender, object e)
        {
            if (selected_capture_Device.DataFlow == DataFlow.Capture)
            {
                // Update the outputLevelProgressBar based on the selected capture device's meter information
                progressBar2.Value = meterInformation_capture.MasterPeakValue * 100;
            }
        }

        private void UpdateOutput3Level(object sender, object e)
        {
            if (selected_render_Device.DataFlow == DataFlow.Render)
            {
                // Update the outputLevelProgressBar based on the selected capture device's meter information
                progressBar2.Value = meterInformation_render.MasterPeakValue * 100;
            }
        }

        private static DateTime Delay(int MS)
        {
            DateTime thisMoment = DateTime.Now;
            TimeSpan duration = new TimeSpan(0, 0, 0, 0, MS);
            DateTime afterMoment = thisMoment.Add(duration);

            while (afterMoment >= thisMoment)
            {
                if (Application.Current != null)
                {
                    Application.Current.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Background, new Action(delegate { }));
                }
                thisMoment = DateTime.Now;
            }

            return DateTime.Now;
        }

        private int GetDeviceNumberByName(string deviceName, string type)
        {
            if (type.Equals("Render"))
            {
                for (int i = 0; i < WaveOut.DeviceCount; i++)
                {
                    var capabilities = WaveOut.GetCapabilities(i);
                    if (capabilities.ProductName.Equals(deviceName))
                    {
                        return i;
                    }
                }
            }
            else if (type.Equals("Capture"))
            {
                for (int i = 0; i < WaveIn.DeviceCount; i++)
                {
                    var capabilities = WaveIn.GetCapabilities(i);
                    if (capabilities.ProductName.Equals(deviceName))
                    {
                        return i;
                    }
                }
            }
            return -1;
        }

        public void chk_audio()
        {
            MainWindow.mainwindow.frame1.Opacity = 0.6;
            MainWindow.mainwindow.frame5.IsEnabled = true;
            MainWindow.mainwindow.frame5.Visibility = Visibility.Visible;
            MainWindow.mainwindow.frame5.Content = MsgBox.Warning_Msg.warning;
            MainWindow.mainwindow.frame1.IsEnabled = false;
        }

        private void update_gui(bool state)
        {
            if (state == true)
            {
                MainWindow.mainwindow.sidebutton.IsEnabled = true;
                Speaker_test.Style = Application.Current.Resources["Apply_Button"] as Style;
                Speaker_test.IsEnabled = true;
                backBtn.IsEnabled = true;

                mic_test.Style = Application.Current.Resources["Apply_Button"] as Style;
                mic_test.IsEnabled = true;

                comboBox1.IsEnabled = true;
                comboBox2.IsEnabled = true;
            }
            else
            {
                MainWindow.mainwindow.sidebutton.IsEnabled = false;
                Speaker_test.Style = Application.Current.Resources["Disable_Button"] as Style;
                Speaker_test.IsEnabled = false;
                backBtn.IsEnabled = false;

                mic_test.Style = Application.Current.Resources["Disable_Button"] as Style;
                mic_test.IsEnabled = false;

                comboBox1.IsEnabled = false;
                comboBox2.IsEnabled = false;
            }
        }

        void _waveInEvent_DataAvailable(object sender, WaveInEventArgs e)
        {
            Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
            {
                _waveWriter.WriteData(e.Buffer, 0, e.BytesRecorded);
            }));
        }

        private void _waveInEvent_RecordingStopped(object sender, EventArgs e)
        {
            Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
            {
                _waveInEvent.Dispose();
                _waveInEvent = null;
                _waveWriter.Close();
                _waveWriter = null;
            }));
        }

        private void Speaker_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (comboBox1.SelectedItem != null)
                {
                    ComboBoxItem item = (ComboBoxItem)comboBox1.SelectedItem;
                    string selectedSpeaker = item.Content.ToString();

                    CompositionTarget.Rendering += UpdateOutput1Level;

                    // Create WaveOutEvent instance and set selected speaker
                    _waveOutEvent = new WaveOutEvent();
                    _waveOutEvent.DeviceNumber = GetDeviceNumberByName(selectedSpeaker, "Render");

                    // 선택된 wav 파일 재생
                    audioFileReader = new AudioFileReader(@"Speaker_test.wav");
                    _waveOutEvent.Init(audioFileReader);
                    _waveOutEvent.Play();

                    update_gui(false);
                    Speaker_test.Content = Cultures.Resources.StrTrouble11;

                    // wav 재생상태 체크
                    while (_waveOutEvent.PlaybackState == PlaybackState.Playing)
                    {
                        Delay(1000);
                    }

                    // wav 정지 및 초기화
                    if (_waveOutEvent != null)
                    {
                        _waveOutEvent.Stop();
                        _waveOutEvent.Dispose();
                        _waveOutEvent = null;

                        audioFileReader.Dispose();
                        audioFileReader = null;
                    }

                    CompositionTarget.Rendering -= UpdateOutput1Level;

                    update_gui(true);
                    Speaker_test.Content = Cultures.Resources.StrTrouble04;

                    progressBar1.Value = 0;
                    progressBar2.Value = 0;
                }
                else
                {
                    chk_audio();
                }
            }
            catch(Exception)
            {
                update_gui(true);
                chk_audio();
            }
        }

        private void Mic_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (comboBox1.SelectedItem != null)
                {
                    ComboBoxItem item_render = (ComboBoxItem)comboBox1.SelectedItem;
                    string selectedSpeaker = item_render.Content.ToString();

                    ComboBoxItem item_capture = (ComboBoxItem)comboBox2.SelectedItem;
                    string selectedMicrophone = item_capture.Content.ToString();

                    // Mic 장치 Volume Meter 이벤트 추가
                    CompositionTarget.Rendering += UpdateOutput2Level;

                    _waveInEvent = new WaveInEvent();
                    _waveInEvent.DeviceNumber = GetDeviceNumberByName(selectedMicrophone, "Capture");

                    _waveInEvent.DataAvailable -= _waveInEvent_DataAvailable;
                    _waveInEvent.DataAvailable += _waveInEvent_DataAvailable;
                    _waveInEvent.RecordingStopped -= _waveInEvent_RecordingStopped;
                    _waveInEvent.RecordingStopped += _waveInEvent_RecordingStopped;
                    var wavePath = Path.GetTempPath() + "test.wav";
                    _waveInEvent.WaveFormat = new WaveFormat(8000, 1);
                    _waveWriter = new WaveFileWriter(wavePath, _waveInEvent.WaveFormat);

                    _waveInEvent.StartRecording();

                    update_gui(false);
                    mic_test.Content = Cultures.Resources.StrTrouble09;

                    Delay(5000);

                    if (_waveInEvent != null)
                    {
                        _waveInEvent.StopRecording();
                    }

                    CompositionTarget.Rendering -= UpdateOutput2Level;
                    progressBar1.Value = 0;
                    progressBar2.Value = 0;

                    Delay(1000);

                    CompositionTarget.Rendering += UpdateOutput3Level;

                    // PlayBack...
                    mic_test.Content = Cultures.Resources.StrTrouble10;
                    // Create WaveOutEvent instance and set selected speaker
                    _waveOutEvent = new WaveOutEvent();
                    _waveOutEvent.DeviceNumber = GetDeviceNumberByName(selectedSpeaker, "Render");

                    // 선택된 wav 파일 재생
                    audioFileReader = new AudioFileReader(wavePath);
                    _waveOutEvent.Init(audioFileReader);
                    _waveOutEvent.Play();

                    // wav 재생상태 체크
                    while (_waveOutEvent.PlaybackState == PlaybackState.Playing)
                    {
                        Delay(1000);
                    }

                    // wav 정지 및 초기화
                    if (_waveOutEvent != null)
                    {
                        _waveOutEvent.Stop();
                        _waveOutEvent.Dispose();
                        _waveOutEvent = null;
                    }

                    if (audioFileReader != null)
                    {
                        audioFileReader.Dispose();
                        audioFileReader = null;
                    }

                    CompositionTarget.Rendering -= UpdateOutput3Level;

                    update_gui(true);
                    mic_test.Content = Cultures.Resources.StrTrouble07;

                    progressBar1.Value = 0;
                    progressBar2.Value = 0;
                }
                else
                {
                    chk_audio();
                }
            }
            catch(Exception)
            {
                test_clear();
                chk_audio();
            }
        }

        public void test_clear()
        {
            try
            {
                //Stop Speaker and Clear
                if (_waveOutEvent != null)
                {
                    _waveOutEvent.Stop();
                    _waveOutEvent.Dispose();
                    _waveOutEvent = null;
                }

                if (audioFileReader != null)
                {
                    audioFileReader.Dispose();
                    audioFileReader = null;
                }

                //Stop Mic and Clear
                if (_waveInEvent != null)
                {
                    _waveInEvent.DataAvailable -= _waveInEvent_DataAvailable;
                    _waveInEvent.RecordingStopped -= _waveInEvent_RecordingStopped;

                    _waveInEvent.StopRecording();
                    _waveInEvent.Dispose();
                    _waveInEvent = null;
                }

                CompositionTarget.Rendering -= UpdateOutput1Level;
                CompositionTarget.Rendering -= UpdateOutput2Level;
                CompositionTarget.Rendering -= UpdateOutput3Level;

                //Reset GUI
                Speaker_test.Content = Cultures.Resources.StrTrouble04;
                mic_test.Content = Cultures.Resources.StrTrouble07;
                update_gui(true);

                progressBar1.Value = 0;
                progressBar2.Value = 0;
            }
            catch(Exception)
            {
            }
        }

        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.mainwindow.frame1.Content = Support_Page.supportpage;
        }
    }
}