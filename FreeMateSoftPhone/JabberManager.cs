using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FreeMateSoftPhone
{
    public class JabberManager : IDisposable
    {
        private bool disposed = false;

        private RegistryKey rkey;

        private volatile bool _shouldStop;
        private volatile bool _incomming;
        private volatile string _callstatus;
        private volatile string _pre_callstatus;
        private volatile string _ismute;

        public event EventHandler<JabberSoftPhoneEventArgs> SoftphoneCallStateChanged;
        public event EventHandler<JabberSoftPhoneMuteEventArgs> SoftphoneMuteStateChanged;
        public event EventHandler SoftphoneRemoved;

        public JabberManager()
        {
            rkey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\SoftLink_Jabber");

            //new Thread(IsRunning).Start();
            _pre_callstatus = "onOnHook";
            _callstatus = "onOnHook";
        }

        /// <summary>
        /// Closes the connection to the device.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Closes any connected devices.
        /// </summary>
        /// <param name="disposing"></param>
        private void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    //CloseDevice();
                }

                disposed = true;
            }
        }

        ~JabberManager()
        {
            Dispose(false);
        }

        //==================================================


        public bool OpenDevice()
        {
            Process[] ProcName = Process.GetProcessesByName("CiscoJabber");

            if (ProcName.Length > 0)
            {
                System.Diagnostics.Debug.WriteLine("LIB :: IsRunning rung");
                new Thread(IsRunning).Start();
                return true;

            }
            return false;
        }

        private void IsRunning()
        {
            while (true)
            {
                Process[] ProcName = Process.GetProcessesByName("CiscoJabber");

                if (ProcName.Length > 0)
                {
                     //System.Diagnostics.Debug.WriteLine("LIB :: IsRunning run" );

                    // call state check 
                     _callstatus = rkey.GetValue("callstatus").ToString();
                     System.Diagnostics.Debug.WriteLine(" _callstatus " + _callstatus);
                     if (!_pre_callstatus.Equals(_callstatus))
                     {
                         var handle = SoftphoneCallStateChanged;
                         if (handle != null)
                         {
                             handle(this, new JabberSoftPhoneEventArgs(_callstatus));
                         }
                     }
                     _pre_callstatus = _callstatus;
                     if (_ismute != rkey.GetValue("ismute").ToString())
                     {
                         _ismute = rkey.GetValue("ismute").ToString();
                         var handle = SoftphoneMuteStateChanged;
                         if (handle != null)
                         {
                             handle(this, new JabberSoftPhoneMuteEventArgs(_ismute));
                         }
                     }
                     System.Diagnostics.Debug.WriteLine("LIB :: IsRunning _ismute " + _ismute);
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("LIB ::  IsRunning killed JABBER ");
                    var handle = SoftphoneRemoved;
                    if (handle != null)
                    {
                        handle(this, EventArgs.Empty);
                    }
                    break;


                }
                Thread.Sleep(1000);
            }


        }

        public string getCallStatus()
        {
            return _callstatus.ToString();
        }

        public bool isMute()
        {
            if (_ismute.Equals("mute"))
            {
                return true;
            }
            return false;
        }
    }

    public class JabberSoftPhoneEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the PowerMateEventArgs class.
        /// </summary>
        /// <param name="state"></param>
        public JabberSoftPhoneEventArgs(string status)
        {
            Status = status;
        }

        /// <summary>
        /// Gets the current PowerMate state.
        /// </summary>
        public String Status { get; private set; }
    }

    public class JabberSoftPhoneMuteEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the PowerMateEventArgs class.
        /// </summary>
        /// <param name="state"></param>
        public JabberSoftPhoneMuteEventArgs(string mute)
        {
            Mute = mute;
        }

        /// <summary>
        /// Gets the current PowerMate state.
        /// </summary>
        public String Mute { get; private set; }
    }
}
