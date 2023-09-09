using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SKYPE4COMLib;
using System.Diagnostics;
using System.Threading;

namespace FreeMateSoftPhone
{
    public class SkypeManager : IDisposable
    {
        // Volatile is used as hint to the compiler that this data
        // member will be accessed by multiple threads.
        private volatile bool _shouldStop;
        private volatile bool isOutGoingCall;
        private volatile bool _ismute;

        private volatile TCallStatus mStatus;
        private volatile Skype skype;
        private volatile Call mCall;
        private string preStatus;

        private bool disposed = false;


       // public event EventHandler SoftphoneAttached;

        public event EventHandler SoftphoneRemoved;

        public event EventHandler<SkypeSoftPhoneEventArgs> SoftphoneCallStateChanged;

        public event EventHandler SoftphoneMuteChanged;
        
        public SkypeManager()
        {
         //   isOutGoingCall = true;
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

        ~SkypeManager()
        {
            Dispose(false);
        }

        public void CloseDevice()
        {
            if (skype != null)
            {
               
                _shouldStop = true;
                
                skype = null;
                System.Diagnostics.Debug.WriteLine("lib :: CloseDevice ");
            }
        }


        public bool OpenSoftphone()
        {
            Process[] ProcName = Process.GetProcessesByName("Skype");
            if (ProcName.Length > 0)
            {
                skype = new Skype();
                System.Diagnostics.Debug.WriteLine("lib :: OpenSoftphone");
                
                if (!skype.Client.IsRunning)
                {
                    System.Diagnostics.Debug.WriteLine("lib :: OpenSoftphone return false");
                    return false;
                }

                try
                {
                    // some window need following Attach function !!!
                    // wait for the client to be connected and ready
                    skype.Attach();

                    skype.CallStatus += new _ISkypeEvents_CallStatusEventHandler(Skype_CallStatus);
                    //skype.OnlineStatus += new _ISkypeEvents_OnlineStatusEventHandler(Skype_OnLineStatus);
                    skype.UserStatus += new _ISkypeEvents_UserStatusEventHandler(Skype_UserStatus);
                    //skype.UserAuthorizationRequestReceived += new _ISkypeEvents_UserAuthorizationRequestReceivedEventHandler(Skype_UserAuthorizationRequestReceived);
                    preStatus = mStatus.ToString();
                }
                catch (System.Runtime.InteropServices.COMException ce)
                {
                    System.Diagnostics.Debug.WriteLine("lib :: COMException " + ce.ToString());
                    
                    return false;
                }

                new Thread(IsRunning).Start();
                return true;
            }

            return false;
        }

        private void Skype_UserStatus(TUserStatus Status)
        {
            System.Diagnostics.Debug.WriteLine("Skype_UserStatus" + Status);
        }

/*
        private void Skype_OnLineStatus(User pUser, TOnlineStatus Status)
        {
            System.Diagnostics.Debug.WriteLine("Skype_OnLineStatus" + Status);

        }
        */

        private void Skype_CallStatus(Call call, TCallStatus status)
        {
            System.Diagnostics.Debug.WriteLine("LIB :: Skype_CallStatus " + status);
            mCall = call;
            mStatus = status;

            if (status == TCallStatus.clsRouting)
            {
                isOutGoingCall = true;
            }
            else if (status == TCallStatus.clsCancelled)
            {
                isOutGoingCall = false;
            }

            var handle = SoftphoneCallStateChanged;
            if (handle != null)
            {
                handle(this, new SkypeSoftPhoneEventArgs(status));
            }
        }

        public void AnswerFunc()
        {
            if (mCall != null && mStatus == TCallStatus.clsRinging && !isOutGoingCall)
            {
                mCall.Answer();
            }
            else if (mCall != null && mStatus != TCallStatus.clsFinished)
            {

                mCall.Finish();
                isOutGoingCall = false;
            }
        }



        public bool MuteFunc()
        {
            _ismute = ((ISkype)skype).Mute;
            _ismute = !_ismute;
            ((ISkype)skype).Mute = _ismute;

            return _ismute;

        }

        public bool isMute()
        {
            _ismute = ((ISkype)skype).Mute;
            return _ismute;
        }

        public String getTCallStatus()
        {
           // System.Diagnostics.Debug.WriteLine("LIB :: getTCallStatus :: " + mStatus.ToString());
           
            return mStatus.ToString();
        }

        public bool getOutGoingCall()
        {
            return isOutGoingCall;
        }

        //=============================================

        private void IsRunning()
        {
            while (!_shouldStop)
            {
                //if (disposed) break;

                Process[] ProcName = Process.GetProcessesByName("Skype");
                try
                {
                    if (ProcName.Length > 0)
                    {
                        //System.Diagnostics.Debug.WriteLine("SKYPE IsRunning Run ");
                        // System.Diagnostics.Debug.WriteLine("IsRunning rung" + ((ISkype)skype).Mute+"  "+_ismute);

                        // call state check 

                        if (!preStatus.Equals(getTCallStatus()))
                        {
                            //System.Diagnostics.Debug.WriteLine("IsRunning rung" + preStatus + "  " + getTCallStatus());

                            preStatus = getTCallStatus();

                        }

                        // mute state
                        if (((ISkype)skype).Mute != _ismute)
                        {
                            _ismute = !_ismute;
                            var handle = SoftphoneMuteChanged;
                            if (handle != null)
                            {
                                handle(this, EventArgs.Empty);
                            }
                        }
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("IsRunning killed ");

                        var handle = SoftphoneRemoved;
                        if (handle != null)
                        {
                            handle(this, EventArgs.Empty);
                        }
                        break;
                    }
                    Thread.Sleep(1000);
                }
                catch (Exception e)
                {

                }
                
            }
            
        
        }
    }




    public class SkypeSoftPhoneEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the PowerMateEventArgs class.
        /// </summary>
        /// <param name="state"></param>
        public SkypeSoftPhoneEventArgs(TCallStatus status)
        {
            Status = status.ToString();
        }

        /// <summary>
        /// Gets the current PowerMate state.
        /// </summary>
        public String Status { get; private set; }
    }

    
}
