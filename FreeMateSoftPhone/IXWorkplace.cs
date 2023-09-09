using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO.Pipes;
using Newtonsoft.Json.Linq;

namespace FreeMateSoftPhone
{
    public class IXWorkplace : IDisposable
    {

        // Volatile is used as hint to the compiler that this data
        // member will be accessed by multiple threads.
        private volatile bool _shouldStop;
        private volatile bool initialized;
        private volatile bool isIncommingCall;
        private volatile bool isconnected;
        private volatile string _ismute;
        private volatile string log;

        private volatile RegistryKey rkey;
        private volatile string clientId;
        private volatile string callId = "";
        private volatile string callState = "";
        private volatile string muteState = "";
        private volatile string responsecode = "";
        private List<string> voiceCalls = new List<string>();

        NamedPipeClientStream pipeClient;
        StreamReader sr;
        StreamWriter sw;

        Thread pipeRun;
        Thread pipeWorkplace;

        private bool disposed = false;

        public event EventHandler SoftphoneRemoved;
        public event EventHandler<IXSoftPhoneEventArgs> SoftphoneStateChanged;
        public event EventHandler<IXSoftPhoneMuteEventArgs> SoftphoneMuteStateChanged;

        public IXWorkplace()
        {
            pipeClient = new NamedPipeClientStream(".", "AvayaCSDK-" + Environment.UserName, PipeDirection.InOut, PipeOptions.Asynchronous);
            sr = new StreamReader(pipeClient);
            sw = new StreamWriter(pipeClient);
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
                    //CloseDevice();
                    _shouldStop = true;
                }
                pipeRun.Abort();
                disposed = true;
            }
        }

        ~IXWorkplace()
        {
            Dispose(false);
        }

        //==================================================

        public bool OpenSoftphone()
        {
            Process[] ProcName = Process.GetProcessesByName("Avaya IX Workplace");

            Console.WriteLine("Open Avaya IX Workplace");
            if (ProcName.Length > 0)
            {
                //pipeClient = new NamedPipeClientStream(".", "AvayaCSDK-" + Environment.UserName, PipeDirection.InOut, PipeOptions.None);
                //sr = new StreamReader(pipeClient);
                //sw = new StreamWriter(pipeClient);

                initialized = false;

                pipeWorkplace = new Thread(new ThreadStart(IXRunning));
                pipeWorkplace.Start();

                // This is no need. It will be done by IsRunning().
                //new Thread(RegisterClient).Start();

                pipeRun = new Thread(new ThreadStart(IsRunning));
                pipeRun.Start();

                return true;
            }

            return false;
        }

        public bool CloseSoftphone()
        {
            Process[] ProcName = Process.GetProcessesByName("Avaya IX Workplace");

            Console.WriteLine("Close Avaya IX Workplace");
            if (ProcName.Length == 0)
            {
                pipeRun.Abort();
                return true;
            }

            return false;
        }


        //==================================================
        public void RegisterClient()
        {
            JObject rtnjson;

            string registerRequestString = "{ \"vnd.avaya.clientresources.RegisterRequest.v1.1\" : { \"applicationId\" : \"Softlink\", \"transactionId\" : \"101\" } } \0";

            if (pipeClient.IsConnected != true)
            {
                pipeClient.Connect();
            }

            Console.WriteLine("Register String = {0}", registerRequestString);
            try
            {
                sw.Write(registerRequestString);
                sw.Flush();
            }
            catch (Exception ex) { throw ex; }

            Thread.Sleep(200);

            StringBuilder messageBuilder = new StringBuilder();
            char tempchar;
            do
            {
                tempchar = (char)sr.Read();
                if (tempchar == -1 || tempchar == '\0')
                    break;

                messageBuilder.Append(tempchar);
            }
            while (true);

            string text;
            text = messageBuilder.ToString();

            pipeWorkplace.Abort();
            initialized = true;

            Console.WriteLine("Register Response = {0}", text);
            rtnjson = JObject.Parse(text);

            string checkResponse;
            try
            {
                checkResponse = rtnjson["vnd.avaya.clientresources.RegisterResponse.v1.1"]["transactionId"].ToString();
            }
            catch (Exception ex1)
            {
                //Console.WriteLine("Exception 1");
                checkResponse = "100";
            }
            Console.WriteLine("Result = {0}", checkResponse);

            if (checkResponse.ToString().Equals("101"))
            {
                clientId = "Avaya IX";
                Console.WriteLine("Client ID = {0}", clientId);

                var handle = SoftphoneStateChanged;
                if (handle != null)
                {
                    handle(this, new IXSoftPhoneEventArgs("RegisterClientOk"));
                }
            }
            else
            {
                // 무조건 IX Workplace 를 다시 실행해야 하는 상황 
                var handle = SoftphoneStateChanged;
                if (handle != null)
                {
                    handle(this, new IXSoftPhoneEventArgs("RegisterClientFail"));
                }
            }
        }

        private void UnregisterClient()
        {
            string unregisterString = "{ \"vnd.avaya.clientresources.UnregisterRequest.v1.1\" : { \"applicationId\" : \"Softlink\", \"transactionId\" : \"101\" } } \0";

            if (pipeClient.IsConnected != true)
            {
                pipeClient.Connect();
            }

            Console.WriteLine("Unregister String = {0}", unregisterString);
            try
            {
                sw.Write(unregisterString);
                sw.Flush();
            }
            catch (Exception ex) { throw ex; }

            Thread.Sleep(200);
            clientId = "Avaya IX";
        }


        private void GetNextNotification()
        {
            JObject rtnjson;

            if (pipeClient.IsConnected != true)
            {
                pipeClient.Connect();
            }

            StringBuilder messageBuilder = new StringBuilder();
            char tempchar;
            do
            {
                tempchar = (char)sr.Read();
                if (tempchar == -1 || tempchar == '\0')
                    break;

                messageBuilder.Append(tempchar);
            }
            while (true);

            string text;
            text = messageBuilder.ToString();

            rtnjson = JObject.Parse(text);
            string checkResponse;
            string checkString = "";

            // Mute Information Frame
            try
            {
                checkResponse = rtnjson["vnd.avaya.clientresources.call.MuteReponse.v1.1"].ToString();
            }
            catch (Exception ex1)
            {
                checkResponse = "";
            }

            if (!string.IsNullOrEmpty(checkResponse))
            {
                Console.WriteLine("Mute Information = {0}", text);
                if (checkResponse.Equals("true"))
                {
                    Console.WriteLine("Muted");
                    var handle = SoftphoneMuteStateChanged;
                    if (handle != null)
                    {
                        handle(this, new IXSoftPhoneMuteEventArgs(true));
                    }
                }
                else if (checkResponse.Equals("false"))
                {
                    Console.WriteLine("Unmuted");
                    var handle = SoftphoneMuteStateChanged;
                    if (handle != null)
                    {
                        handle(this, new IXSoftPhoneMuteEventArgs(false));
                    }
                }
                return;
            }

            // Call Information Frame
            try
            {
                checkResponse = rtnjson["vnd.avaya.clientresources.call.UpdatedEvent.v1.1"].ToString();
            }
            catch (Exception ex1)
            {
                checkResponse = "";
            }
            if (!string.IsNullOrEmpty(checkResponse))
            {
                checkString = "vnd.avaya.clientresources.call.UpdatedEvent.v1.1";
            }
            // Call Answering Response Frame
            try
            {
                checkResponse = rtnjson["vnd.avaya.clientresources.call.AcceptResponse.v1.1"].ToString();
            }
            catch (Exception ex1)
            {
                checkResponse = "";
            }
            if (!string.IsNullOrEmpty(checkResponse))
            {
                checkString = "vnd.avaya.clientresources.call.AcceptResponse.v1.1";
            }
            // Call Create Response Frame
            try
            {
                checkResponse = rtnjson["vnd.avaya.clientresources.call.CreateResponse.v1.1"].ToString();
            }
            catch (Exception ex1)
            {
                checkResponse = "";
            }
            if (!string.IsNullOrEmpty(checkResponse))
            {
                checkString = "vnd.avaya.clientresources.call.CreateResponse.v1.1";
            }
            // Call Termination Response Frame
            try
            {
                checkResponse = rtnjson["vnd.avaya.clientresources.call.TerminateResponse.v1.1"].ToString();
            }
            catch (Exception ex1)
            {
                checkResponse = "";
            }
            if (!string.IsNullOrEmpty(checkResponse))
            {
                checkString = "vnd.avaya.clientresources.call.TerminateResponse.v1.1";
            }

            if(!string.IsNullOrEmpty(checkString))
            {
                Console.WriteLine("Call Information = {0}", text);
                callState = rtnjson[checkString]["vnd.avaya.clientresources.Call.v1.1"]["callState"].ToString();
                muteState = rtnjson[checkString]["vnd.avaya.clientresources.Call.v1.1"]["muted"].ToString();
                string tcallId = rtnjson[checkString]["vnd.avaya.clientresources.Call.v1.1"]["callId"].ToString();
                string audioId = rtnjson[checkString]["vnd.avaya.clientresources.Call.v1.1"]["audioDirection"].ToString();

                Console.WriteLine("Call Information - CallState = {0}, MuteState = {1}, CallId = {2}", callState, muteState, tcallId);

                if (callState.Equals("alerting"))
                {
                    Console.WriteLine("Incoming call");
                    callId = tcallId;
                    isconnected = false;
                    var handle = SoftphoneStateChanged;
                    if (handle != null)
                    {
                        System.Diagnostics.Debug.WriteLine("LIB :: IncommingCall " + callId);
                        handle(this, new IXSoftPhoneEventArgs("IncommingCall"));
                        isIncommingCall = true;
                    }
                }
                else if (callState.Equals("initiating"))
                {
                    Console.WriteLine("Outgoing call");
                    callId = tcallId;
                    var handle = SoftphoneStateChanged;
                    if (handle != null)
                    {
                        System.Diagnostics.Debug.WriteLine("LIB :: OutgoingCall " + callId); 
                        handle(this, new IXSoftPhoneEventArgs("OutgoingCall"));
                        isIncommingCall = false;
                        isconnected = true;
                    }
                }
                else if (callState.Equals("established") && !audioId.Equals("inactive"))
                {
                    Console.WriteLine("Connected Call");
                    callId = tcallId;
                    var handle = SoftphoneStateChanged;
                    if (handle != null)
                    {
                        System.Diagnostics.Debug.WriteLine("LIB :: OutgoingCall " + callId);
                        handle(this, new IXSoftPhoneEventArgs("ConnectedCall"));
                        isIncommingCall = false;
                        isconnected = true;
                    }
                }
                else if ((callState.Equals("idle") || callState.Equals("ending") || callState.Equals("ended")) && tcallId.Equals(callId))
                {
                    Console.WriteLine("Call Ended");
                    callId = tcallId;
                    if (isconnected || isIncommingCall)
                    {
                        var handle = SoftphoneStateChanged;
                        if (handle != null)
                        {
                            System.Diagnostics.Debug.WriteLine("LIB :: CallEnd 1 " + callId);
                            handle(this, new IXSoftPhoneEventArgs("CallEnd"));
                            isIncommingCall = false;
                            isconnected = false;
                        }
                    }
                }

                if (muteState.Equals("true"))
                {
                    var handle = SoftphoneMuteStateChanged;
                    if (handle != null)
                    {
                        handle(this, new IXSoftPhoneMuteEventArgs(true));
                    }
                }
                if (muteState.Equals("false"))
                {
                    var handle = SoftphoneMuteStateChanged;
                    if (handle != null)
                    {
                        handle(this, new IXSoftPhoneMuteEventArgs(false));
                    }
                }
            }
            else
            {
                //   Console.WriteLine("x ResponseCode :: " + responsecode+"  " + voiceCalls.Count);
            }
        }

        public void MuteFunc(bool isMute)
        {
            if (pipeClient.IsConnected != true)
            {
                pipeClient.Connect();
            }

            if (clientId != null)
            {
                string muteString;

                if (isMute)
                {
                    muteString = "{ \"vnd.avaya.clientresources.call.MuteRequest.v1.1\" : { \"callId\": \"" + callId + "\", \"muted\": \"true\", \"transactionId\": \"101\" } } \0";
                }
                else
                {
                    muteString = "{ \"vnd.avaya.clientresources.call.MuteRequest.v1.1\" : { \"callId\": \"" + callId + "\", \"muted\": \"false\", \"transactionId\": \"101\" } } \0";
                }

                Console.WriteLine("Mute String = {0}", muteString);
                if (pipeClient.IsConnected != true)
                {
                    pipeClient.Connect();
                }

                try
                {
                    sw.Write(muteString);
                    sw.Flush();
                }
                catch (Exception ex) { throw ex; }

            }
        }

        public void CallFunc()
        {
            //if (voiceCalls.Count == 0) return;

            if (pipeClient.IsConnected != true)
            {
                pipeClient.Connect();
            }

            string CallString;

            Console.WriteLine("Call Function");
            if (isIncommingCall && !isconnected)
            {
                if (isIncommingCall)
                {
                    CallString = "{ \"vnd.avaya.clientresources.call.AcceptRequest.v1.1\" : {  \"callId\": \"" + callId + "\", \"transactionId\": \"101\" } } \0";
                }
                else
                {
                    CallString = "{ \"vnd.avaya.clientresources.call.CreateRequest.v1.1\" : { \"remotePartyNumber\": \"*\", \"transactionId\": \"101\"  } } \0";
                }

                isconnected = true;

                var handle = SoftphoneStateChanged;
                if (handle != null)
                {
                    handle(this, new IXSoftPhoneEventArgs("ConnectedCall"));
                    //System.Diagnostics.Debug.WriteLine("LIB :: ConnectedCall ");
                }
            }
            else if (isconnected)
            {
                isconnected = false;
                isIncommingCall = false;
                CallString = "{ \"vnd.avaya.clientresources.call.TerminateRequest.v1.1\" : { \"callId\": \"" + callId + "\", \"transactionId\": \"101\" } } \0";

                var handle = SoftphoneStateChanged;

                if (handle != null)
                {
                    handle(this, new IXSoftPhoneEventArgs("CallEnd"));
                    System.Diagnostics.Debug.WriteLine("LIB :: CallEnd 2 ");
                }

                //  voiceCalls.Remove(voiceCalls[voiceCalls.Count - 1]);
            }
            else
            {
                CallString = "No Call";
                return;
            }

            Console.WriteLine("Call String = {0}", CallString);
            try
            {
                sw.Write(CallString);
                sw.Flush();
            }
            catch (Exception ex) { throw ex; }
        }


        //========================================================
        private void IsRunning()
        {
            while (!_shouldStop)
            {
                Process[] ProcName = Process.GetProcessesByName("Avaya IX Workplace");

                if (ProcName.Length > 0)
                {
                    //Console.WriteLine("IsRunning run + " + clientId);

                    if (clientId != null)
                    {
                        GetNextNotification();
                    }
                    else
                    {
                        RegisterClient();
                    }
                }
                else
                {
                    //System.Diagnostics.Debug.WriteLine("IsRunning killed ");

                    //var handle = SoftphoneRemoved;
                    //if (handle != null)
                    //{
                    //    handle(this, EventArgs.Empty);
                    //}
                    //break;
                }
                Thread.Sleep(500);
            }
            Console.WriteLine("IsRunning finished");
        }

        private void IXRunning()
        {
            Thread.Sleep(20000);
            initialized = true;

            var handle = SoftphoneStateChanged;
            if (handle != null)
            {
                handle(this, new IXSoftPhoneEventArgs("textnull"));
                clientId = null;
            }
            Console.WriteLine("Workplace is not response");
        }
    }

    public class IXSoftPhoneEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the PowerMateEventArgs class.
        /// </summary>
        /// <param name="state"></param>
        public IXSoftPhoneEventArgs(string status)
        {
            Status = status;
        }

        /// <summary>
        /// Gets the current PowerMate state.
        /// </summary>
        public String Status { get; private set; }
    }

    public class IXSoftPhoneMuteEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the PowerMateEventArgs class.
        /// </summary>
        /// <param name="state"></param>
        public IXSoftPhoneMuteEventArgs(bool mute)
        {
            Mute = mute;
        }

        /// <summary>
        /// Gets the current PowerMate state.
        /// </summary>
        public bool Mute { get; private set; }
    }
}
