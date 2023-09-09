using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TCX.CallTriggerCmd;

namespace FreeMateSoftPhone
{
    public class _3CXManager : IDisposable
    {
       
        private string SOFTPHONENAME = "3CXWin8Phone";
        private volatile bool _shouldStop;
        private volatile bool _ismute;
        private volatile bool _connected;

        public ICallTriggerService service;
        public ActiveCall activecall;
        public static bool isChangedCallStatus;

        public Dictionary<string, string> ActiveCallDic = new Dictionary<string, string>();

        public int CountActiveCalls;


        private bool disposed = false;

        public event EventHandler SoftphoneRemoved;

        public event EventHandler<_3CXSoftPhoneEventArgs> SoftphoneCallStateChanged;

        public ServiceCallback mServiceCallback;

        private string mStatus;

        public _3CXManager()
        {
         //   isOutGoingCall = true;
            mStatus = "";
        }

        /// <summary>
        /// Closes the connection to the device.
        /// </summary>
        public void Dispose()
        {
            _shouldStop = true;
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

        ~_3CXManager()
        {
            Dispose(false);
        }

        public bool OpenSoftphone()
        {
            Process[] ProcName = Process.GetProcessesByName(SOFTPHONENAME);
            if (ProcName.Length > 0)
            {
               // System.Diagnostics.Debug.WriteLine("3CX :: LIB :: OpenSoftphone");
               
                new Thread(IsRunning).Start();
                return true;
            }

            return false;
        }



        public void Init()
        {
            var binding = new NetNamedPipeBinding();
            var key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\3CX");
            var uri = key.GetValue("CallTriggerCmdUri");
            if (uri == null)
                throw new Exception("User specific 3CXPhone CallTrigger uri is not found");

            var address = new EndpointAddress(uri.ToString());
            mServiceCallback = new ServiceCallback();
           
         
            var channelFactory = new DuplexChannelFactory<ICallTriggerService>(mServiceCallback, binding, address);

            service = channelFactory.CreateChannel();
         
            service.Subscribe();
            _connected = false;

        }

        public void CallFunc()
        {
            // call 이 있을 경우만 동작하도록 한다.
            // activecall 이 null 인 경우도 동작하여 막음 
            if (activecall != null)
            {
                if (mStatus.Equals("Ringing"))
                {
                    service.Activate(activecall.CallID);
                    _connected = true;
                }
                else
                {

                    service.DropCall(activecall.CallID);
                    _connected = false;
                    var handle = SoftphoneCallStateChanged;
                    if (handle != null)
                    {
                        handle(this, new _3CXSoftPhoneEventArgs("EndCall"));
                    }
                    mStatus = "";
                }

            }
            
            

            /*
            if (CountActiveCalls > 0)
            {
                if (ActiveCallDic.Values.Last().Equals("Ringing"))
                {
                    ActiveCall ac = service.ActiveCalls.ToArray()[0];
                    service.Activate(ActiveCallDic.Keys.Last());
                    _connected = true;
                }
                else
                {
                    service.DropCall(ActiveCallDic.Keys.Last());
                    _connected = false;
                }
            }
             * */


        }

        public void MuteFunc()
        {
            if (mStatus.Equals("Connected"))
            {
                _ismute = !_ismute;
                service.Mute(activecall.CallID);
            }
            
            
        }

        public int getCountActiveCalls()
        {
            return CountActiveCalls;
        }

        public bool isConnected()
        {
            if (CountActiveCalls > 0)
            {
                if (ActiveCallDic.Values.Last().Equals("Connected"))
                {
                    _connected = true;
                }
            }

            return _connected;
        }

        public void ChangedCallStatus()
        {
        
        }

        public class ServiceCallback : IClientCallback
        {
            

            public void CurrentProfileChanged(int profileid)
            {
               // Console.WriteLine("3CX :: LIB ::Profile changed");
            }

            public void ProfileExtendedStatusChanged(int profileid, string status)
            {
             //   Console.WriteLine("3CX :: LIB ::Extended status changed");
            }

            public void CallStatusChanged()
            {
                Console.WriteLine("3CX :: LIB ::Call status changed yyy");

               // isChangedCallStatus = true;

                
            }

            public void MyPhoneStatusChanged()
            {
                Console.WriteLine("3CX :: LIB ::MyPhone status changed");
            }
        };

        private void IsRunning()
        {
            Thread.Sleep(1000);
            Init();

            Console.WriteLine("3CX :: LIB :: IsRunning ");
            while (!_shouldStop)
            {
                Process[] ProcName = Process.GetProcessesByName(SOFTPHONENAME);

                if (disposed) break;

                if (ProcName.Length > 0 && service != null)
                {

                    try
                    {
                       // Console.WriteLine("3CX :: LIB :: IsRunning run " + +ProcName.Length + "  " + service.ActiveCalls.Count);
                        if (service.ActiveCalls.Count == 0)
                        {
                            if (mStatus.Equals("Connected") || mStatus.Equals("Ringing") || mStatus.Equals("Dialing"))
                            {
                                var handle = SoftphoneCallStateChanged;
                                if (handle != null)
                                {
                                    handle(this, new _3CXSoftPhoneEventArgs("EndCall"));
                                }
                            }

                            mStatus = "";

                        }
                        else
                        {
                            foreach (var id in service.ActiveCalls)
                            {
                                Console.WriteLine("3CX :: LIB :: id.CallID {0} id.State {1}", id.CallID, id.State.ToString());

                                if (id.State.ToString().Equals("Ringing"))
                                {
                                    activecall = id;
                                    mStatus = "Ringing";
                                    var handle = SoftphoneCallStateChanged;
                                    if (handle != null)
                                    {
                                        handle(this, new _3CXSoftPhoneEventArgs(id.State.ToString()));
                                    }

                                    break;
                                }
                                else if (id.State.ToString().Equals("Connected"))
                                {
                                    activecall = id;
                                    mStatus = "Connected";
                                    var handle = SoftphoneCallStateChanged;
                                    if (handle != null)
                                    {
                                        handle(this, new _3CXSoftPhoneEventArgs(id.State.ToString()));
                                    }
                                    break;
                                }
                                else if (id.State.ToString().Equals("Dialing"))
                                {
                                    activecall = id;
                                    mStatus = "Dialing";
                                    var handle = SoftphoneCallStateChanged;
                                    if (handle != null)
                                    {
                                        handle(this, new _3CXSoftPhoneEventArgs(id.State.ToString()));
                                    }
                                    break;
                                }
                                //

                                /*
                                if (ActiveCallDic.ContainsKey((string)id.CallID))
                                {
                                    ActiveCallDic[(string)id.CallID] = id.State.ToString();
                                }
                                else
                                {
                                    ActiveCallDic.Add((string)id.CallID, id.State.ToString());
                                }
                                  */
                            }
                        }
                    }
                    catch (System.ServiceModel.FaultException ssfe)
                    {
                        System.Diagnostics.Debug.WriteLine("3CX :: LIB :: IsRunning killed ");
                        var handle = SoftphoneRemoved;
                        if (handle != null)
                        {
                            handle(this, EventArgs.Empty);
                        }
                        break;
                      
                    }

                }
                else
                {
                   // System.Diagnostics.Debug.WriteLine("3CX :: LIB :: IsRunning killed ");

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

    }

    public class _3CXSoftPhoneEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the PowerMateEventArgs class.
        /// </summary>
        /// <param name="state"></param>
        public _3CXSoftPhoneEventArgs(string status)
        {
            Status = status.ToString();
        }

        /// <summary>
        /// Gets the current PowerMate state.
        /// </summary>
        public String Status { get; private set; }
    }
}
