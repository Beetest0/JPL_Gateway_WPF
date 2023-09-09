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

namespace FreeMateSoftPhone
{
    public class OneXManager : IDisposable
    {

        // Volatile is used as hint to the compiler that this data
        // member will be accessed by multiple threads.
        private volatile bool _shouldStop;
        private volatile bool isIncommingCall;
        private volatile bool isconnected;
        private volatile string _ismute;
        private volatile string log;

        private volatile RegistryKey SettingsRegistryKey;
        private volatile RegistryKey rkey;
        private volatile string uriPrefix;
        private volatile string clientId;
        private volatile string responsecode = "";
        private List<string> voiceCalls = new List<string>();

        private bool disposed = false;

        public event EventHandler SoftphoneRemoved;
        public event EventHandler<OneXSoftPhoneEventArgs> SoftphoneStateChanged;


         public OneXManager()
        {
            SettingsRegistryKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Avaya\Avaya one-X Agent\Settings", true);
            uriPrefix = "http://127.0.0.1:" + SettingsRegistryKey.GetValue("APIPort") + "/onexagent/api/";
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

                disposed = true;
            }
        }

        ~OneXManager()
        {
            Dispose(false);
        }

        //==================================================

        public bool OpenSoftphone()
        {
            Process[] ProcName = Process.GetProcessesByName("OneXAgentUI");
           
            if (ProcName.Length > 0)
            {
                RegisterClient();
                

                new Thread(IsRunning).Start();
                return true;
            }

            return false;
        }


        //==================================================
        public void RegisterClient()
        {
            Random rnd = new Random();
            int month = rnd.Next(1, 100);

            string requestUri = uriPrefix + "registerclient?name=sample" + month;
            string text = MakeRequest(requestUri);

            //System.Diagnostics.Debug.WriteLine("ONEX :LIB :RegisterClient text " + requestUri);

            if (text != null)
            {
                clientId = GetAttrValue(text, "ClientId");
                //System.Diagnostics.Debug.WriteLine("RegisterClient clientId " + clientId);

                if (clientId.Length > 0)
                {
                  //  System.Diagnostics.Debug.WriteLine("정상적으로 입려되는 경우 ");
                   // rkey.SetValue("clientId", clientId);

                    var handle = SoftphoneStateChanged;
                    if (handle != null)
                    {
                        handle(this, new OneXSoftPhoneEventArgs("RegisterClientOk"));
                    }

                }
                else
                {
                    // 무조건 onex를 다시 실행해야 하는 상황 
                    //System.Diagnostics.Debug.WriteLine("무조건 onex를 다시 실행해야 하는 상황 ");
                    clientId = "";
                    //rkey.SetValue("clientId", clientId);
                    var handle = SoftphoneStateChanged;
                    if (handle != null)
                    {
                        handle(this, new OneXSoftPhoneEventArgs("RegisterClientFail"));
                    }
                }
            }
        }




        private void UnregisterClient()
        {
            string requestUri = uriPrefix + "unregisterclient?clientid=" + clientId;
            string text = MakeRequest(requestUri);
            rkey.SetValue("clientId", "");
        }


        private void GetNextNotification()
        {
            string requestUri = uriPrefix + "nextnotification?clientid=" + clientId;
            string text = MakeRequest(requestUri);
          
            if (text == null)
            {

                responsecode = "2";
               
                var handle = SoftphoneStateChanged;
                if (handle != null)
                {
                    handle(this, new OneXSoftPhoneEventArgs("textnull"));
                    clientId = null;
                }

            }
            else
            {
                responsecode = GetAttrValue(text, "ResponseCode");
                //System.Diagnostics.Debug.WriteLine("LIB :: responsecode " + responsecode);
                //System.Diagnostics.Debug.WriteLine("LIB :: text " + text);
                

                if (!(text.IndexOf("QueueEmpty") > -1))
                {
                    
                    // Console.WriteLine("ResponseCode :: " + responsecode);
                    if (text.IndexOf("VoiceInteractionCreated") > -1)
                    {
                        string Outbound = GetAttrValue(text, "Outbound");
                        //System.Diagnostics.Debug.WriteLine("LIB :: Outbound " + Outbound);
                        //System.Diagnostics.Debug.WriteLine("LIB :: IncommingCall ");


                        string interactionId = GetAttrValue(text, "ObjectId");
                        voiceCalls.Add(interactionId);
                       
                        isconnected = false;

                        var handle = SoftphoneStateChanged;
                        if (handle != null)
                        {
                            if (Outbound.Equals("False"))
                            {
                                System.Diagnostics.Debug.WriteLine("LIB :: IncommingCall " + interactionId);
                                handle(this, new OneXSoftPhoneEventArgs("IncommingCall"));
                                isIncommingCall = true;

                            }
                            else
                            {
                                System.Diagnostics.Debug.WriteLine("LIB :: OutgoingCall " + interactionId);
                                handle(this, new OneXSoftPhoneEventArgs("OutgoingCall"));
                                isIncommingCall = false;
                                isconnected = true;
                            }
                            
                        }


                    }
                    else if (text.IndexOf("VoiceInteractionTerminated") > -1 || text.IndexOf("VoiceInteractionMissed") > -1)
                    {
                       
                        string interactionId = GetAttrValue(text, "ObjectId");

                        System.Diagnostics.Debug.WriteLine("LIB :: CallEnd 1 " + interactionId);

                        voiceCalls.Remove(interactionId);

                        isIncommingCall = false;
                        isconnected = false;

                        var handle = SoftphoneStateChanged;
                        if (handle != null)
                        {
                            handle(this, new OneXSoftPhoneEventArgs("CallEnd"));
                            
                        }

                    }
                }
                else
                {
                    //   Console.WriteLine("x ResponseCode :: " + responsecode+"  " + voiceCalls.Count);
                }
            }




        }

        public void MuteFunc(bool isMute)
        {
            if (clientId != null)
            {
                string requestUri =  "";

                if (isMute)
                {
                    requestUri = uriPrefix + "voice/mute?clientid=" + clientId;
                }
                else
                {
                    requestUri = uriPrefix + "voice/unmute?clientid=" + clientId;
                }


                string text = MakeRequest(requestUri);
              

            }
        }

        public void CallFunc()
        {
            if (voiceCalls.Count == 0) return;

            if (isIncommingCall && !isconnected )
            {
                Console.WriteLine("CallFunc isIncommingCall {0}  {1}", voiceCalls.Count, voiceCalls[voiceCalls.Count - 1]);

                isconnected = true;
                string requestUri = uriPrefix + "voice/accept?clientid=" + clientId + "&interactionid=" + voiceCalls[voiceCalls.Count - 1];
                string text = MakeRequest(requestUri);
                //isIncommingCall = false;

                var handle = SoftphoneStateChanged;
                if (handle != null)
                {
                    handle(this, new OneXSoftPhoneEventArgs("ConnectedCall"));
                    //System.Diagnostics.Debug.WriteLine("LIB :: ConnectedCall ");
                }

            }
            else
            {

                isconnected = false;
                isIncommingCall = false;
                string requestUri = uriPrefix + "voice/release?clientid=" + clientId + "&interactionid=" + voiceCalls[voiceCalls.Count - 1];
                string text = MakeRequest(requestUri);

                var handle = SoftphoneStateChanged;


                if (handle != null)
                {
                    handle(this, new OneXSoftPhoneEventArgs("CallEnd"));
                    System.Diagnostics.Debug.WriteLine("LIB :: CallEnd 2 ");
                }

              //  voiceCalls.Remove(voiceCalls[voiceCalls.Count - 1]);
            }


        }


        //========================================================


        private string MakeRequest(string requestUri)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUri);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream stream = response.GetResponseStream();

                StreamReader reader = new StreamReader(stream);
                string text = reader.ReadToEnd();
                stream.Close();
                return text;
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    var response = ex.Response as HttpWebResponse;
                    if (response != null)
                    {
                        Console.WriteLine("HTTP Status Code: " + (int)response.StatusCode);
                    }
                    else
                    {
                        // no http status code available
                    }
                }
                else
                {
                    // no http status code available
                }
            }
            catch (Exception e)
            {

            }

            return null;
        }

        private string GetAttrValue(string text, string attrName)
        {
            int attrStart = text.IndexOf(attrName + "=\"") + (attrName + "=\"").Length;
            int attrEnd = text.IndexOf("\"", attrStart);
            return text.Substring(attrStart, attrEnd - attrStart);
        }

        private void IsRunning()
        {
            while (!_shouldStop)
            {
                Process[] ProcName = Process.GetProcessesByName("OneXAgentUI");

                if (ProcName.Length > 0)
                {
                    //System.Diagnostics.Debug.WriteLine("IsRunning run + " + clientId);

                    if (clientId!=null)
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

    public class OneXSoftPhoneEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the PowerMateEventArgs class.
        /// </summary>
        /// <param name="state"></param>
        public OneXSoftPhoneEventArgs(string status)
        {
            Status = status;
        }

        /// <summary>
        /// Gets the current PowerMate state.
        /// </summary>
        public String Status { get; private set; }
    }
}
