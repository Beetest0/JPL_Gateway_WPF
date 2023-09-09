using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Diagnostics;
using System.Threading;
using HeadSetInterfaceLib;

namespace FreeMateSoftPhone
{
    public class OneXCommManager : IDisposable
    {
        // Volatile is used as hint to the compiler that this data
        // member will be accessed by multiple threads.
        private volatile bool _shouldStop;
        private volatile bool isOutGoingCall;
        private volatile bool _ismute;


        private string preStatus;

        private bool disposed = false;

        private AvayaHeadsetInterface mAvayaHeadsetInterface;
        private short nConnectionId;

        public event EventHandler SoftphoneAttached;
        public event EventHandler SoftphoneRemoved;


        public event EventHandler<SoftPhoneStatusEventArgs> SoftphoneStateChanged;
        public event EventHandler<SoftPhoneMuteEventArgs> SoftphoneMuteStateChanged;

        public bool isEvented = false;
        
        public OneXCommManager()
        {
            _shouldStop = false;
           
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
                    CloseDevice();
                }

                disposed = true;
            }
        }

        ~OneXCommManager()
        {
            Dispose(false);
        }

        public void CloseDevice()
        {
            if (mAvayaHeadsetInterface != null)
            {
               
                _shouldStop = true;

                mAvayaHeadsetInterface = null;
                System.Diagnostics.Debug.WriteLine("lib :: CloseDevice ");
            }
        }


        public bool OpenSoftphone()
        {
            // onexcui OneXAgentUI
            Process[] ProcName = Process.GetProcessesByName("onexcui");
            if (ProcName.Length > 0)
            {
                mAvayaHeadsetInterface = new AvayaHeadsetInterface();

                System.Diagnostics.Debug.WriteLine("lib :: ONEXCOMM :: OpenSoftphone");
                System.Diagnostics.Debug.WriteLine("lib :: ONEXCOMM :: OpenSoftphone");
                System.Diagnostics.Debug.WriteLine("lib :: ONEXCOMM :: OpenSoftphone");

                mAvayaHeadsetInterface.Register();

                mAvayaHeadsetInterface.EstablishedEvent += new _IAvayaHeadsetInterfaceEvents_EstablishedEventEventHandler(EstablishedEvent);
                mAvayaHeadsetInterface.HeldEvent += new _IAvayaHeadsetInterfaceEvents_HeldEventEventHandler(HeldEvent);

                mAvayaHeadsetInterface.IncomingSessionEvent += new _IAvayaHeadsetInterfaceEvents_IncomingSessionEventEventHandler(IncomingSessionEvent);
                mAvayaHeadsetInterface.MuteStateEvent += new _IAvayaHeadsetInterfaceEvents_MuteStateEventEventHandler(MuteStateEvent);

                mAvayaHeadsetInterface.SessionCreatedEvent += new _IAvayaHeadsetInterfaceEvents_SessionCreatedEventEventHandler(SessionCreatedEvent);
                mAvayaHeadsetInterface.SessionEndedEvent += new _IAvayaHeadsetInterfaceEvents_SessionEndedEventEventHandler(SessionEndedEvent);
                mAvayaHeadsetInterface.SessionUpdatedEvent += new _IAvayaHeadsetInterfaceEvents_SessionUpdatedEventEventHandler(SessionUpdatedEvent);

                mAvayaHeadsetInterface.UnheldEvent += new _IAvayaHeadsetInterfaceEvents_UnheldEventEventHandler(UnheldEvent);
            }

            return false;
        }

        public void CallFunc(bool isAnswer)
        {
            if (isAnswer)
            {
                mAvayaHeadsetInterface.AnswerCall(nConnectionId);
            }
            else
            {
                mAvayaHeadsetInterface.HangupCall(nConnectionId);
            }
            
        }

        public void MuteFunc(bool isMute)
        {
           // System.Diagnostics.Debug.WriteLine("lib :: MuteFunc isMute " + isMute);

            if (isMute)
            {
                mAvayaHeadsetInterface.MuteCall(1, 1);
            }

            else
            {
                mAvayaHeadsetInterface.UnmuteCall(1, 1);
            }
               
        }

        public void EstablishedEvent(short nConnectionId)
        {
            System.Diagnostics.Debug.WriteLine("lib :: EstablishedEvent nConnectionId " + nConnectionId);
            this.nConnectionId = nConnectionId;
            var handle = SoftphoneStateChanged;
            if (handle != null)
            {
                handle(this, new SoftPhoneStatusEventArgs("ConnectedCall"));
            }
        }
        public void HeldEvent(short nConnectionId)
        {
            System.Diagnostics.Debug.WriteLine("lib :: HeldEvent nConnectionId " + nConnectionId);
        }

        public void IncomingSessionEvent(short nConnectionId)
        {
            System.Diagnostics.Debug.WriteLine("lib :: IncomingSessionEvent nConnectionId " + nConnectionId);
            this.nConnectionId = nConnectionId;
            var handle = SoftphoneStateChanged;
            if (handle != null)
            {
                handle(this, new SoftPhoneStatusEventArgs("IncommingCall"));
            }
        }

        public void MuteStateEvent(ushort nMute)
        {
            if (!isEvented)
            {
                System.Diagnostics.Debug.WriteLine("lib :: MuteStateEvent nMute " + nMute);
                var handle = SoftphoneMuteStateChanged;
                if (handle != null)
                {
                    if (nMute == 1)
                        handle(this, new SoftPhoneMuteEventArgs(true));
                    else
                        handle(this, new SoftPhoneMuteEventArgs(false));
                }
                isEvented = true;
                new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;
                    Thread.Sleep(500);
                    isEvented = false;
                }).Start();
            }
            
            

            
            

            
            
            
        }

        public void SessionCreatedEvent(short nConnectionId)
        {
            System.Diagnostics.Debug.WriteLine("lib :: SessionCreatedEvent nConnectionId " + nConnectionId);
        }

        public void SessionEndedEvent(short nConnectionId)
        {
            System.Diagnostics.Debug.WriteLine("lib :: SessionEndedEvent nConnectionId " + nConnectionId);
            var handle = SoftphoneStateChanged;
            if (handle != null)
            {
                handle(this, new SoftPhoneStatusEventArgs("CallEnd"));
                System.Diagnostics.Debug.WriteLine("LIB :: CallEnd 2 ");
            }
        }

        public void SessionUpdatedEvent(short nConnectionId)
        {
           // System.Diagnostics.Debug.WriteLine("lib :: SessionUpdatedEvent nConnectionId " + nConnectionId);
        }

        public void UnheldEvent(short nConnectionId)
        {
            System.Diagnostics.Debug.WriteLine("lib :: UnheldEvent nConnectionId " + nConnectionId);
        }



    }

    public class SoftPhoneStatusEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the PowerMateEventArgs class.
        /// </summary>
        /// <param name="state"></param>
        public SoftPhoneStatusEventArgs(string status)
        {
            Status = status;
        }

        /// <summary>
        /// Gets the current PowerMate state.
        /// </summary>
        public String Status { get; private set; }
    }

    public class SoftPhoneMuteEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the PowerMateEventArgs class.
        /// </summary>
        /// <param name="state"></param>
        public SoftPhoneMuteEventArgs(bool mute)
        {
            Mute = mute;
        }

        /// <summary>
        /// Gets the current PowerMate state.
        /// </summary>
        public bool Mute { get; private set; }
    }
    
}
