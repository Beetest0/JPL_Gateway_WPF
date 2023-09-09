using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeMateSoftPhone
{
    public class BriaManager : IDisposable
    {

        private bool disposed = false;

        public event EventHandler<BriaSoftPhoneEventArgs> SoftphoneStateChanged;
        public event EventHandler<BriaSoftPhoneMuteEventArgs> SoftphoneMuteStateChanged;


        private BriaAPI briaAPI;

        #region EVENTS_DELEGATE_DEFINITIONS

        private delegate void OnConnectedDelegate();
        private OnConnectedDelegate onConnectedDelegate;

        private delegate void OnErrorDelegate();
        private OnErrorDelegate onErrorDelegate;

        private delegate void OnDisconnectedDelegate();
        private OnDisconnectedDelegate onDisconnectedDelegate;

        private delegate void OnPhoneStatusDelegate(BriaAPI.PhoneStatusEventArgs args);
        private OnPhoneStatusDelegate onPhoneStatusDelegate;

        private delegate void OnCallStatusDelegate(BriaAPI.CallStatusEventArgs args);
        private OnCallStatusDelegate onCallStatusDelegate;

        private delegate void OnCallOptionsStatusDelegate(BriaAPI.CallOptionsStatusEventArgs args);
        private OnCallOptionsStatusDelegate onCallOptionsStatusDelegate;

        private delegate void OnAudioPropertiesStatusDelegate(BriaAPI.AudioPropertiesStatusEventArgs args);
        private OnAudioPropertiesStatusDelegate onAudioPropertiesStatusDelegate;

        private delegate void OnMissedCallsStatusDelegate(BriaAPI.MissedCallsStatusEventArgs args);
        private OnMissedCallsStatusDelegate onMissedCallsStatusDelegate;

        private delegate void OnVoiceMailStatusDelegate(BriaAPI.VoiceMailStatusEventArgs args);
        private OnVoiceMailStatusDelegate onVoiceMailStatusDelegate;

        private delegate void OnCallHistoryStatusDelegate(BriaAPI.CallHistoryStatusEventArgs args);
        private OnCallHistoryStatusDelegate onCallHistoryStatusDelegate;

        private delegate void OnSystemSettingsStatusDelegate(BriaAPI.SystemSettingsStatusEventArgs args);
        private OnSystemSettingsStatusDelegate onSystemSettingsStatusDelegate;

        private delegate void OnErrorReceivedDelegate(BriaAPI.ErrorReceivedEventArgs args);
        private OnErrorReceivedDelegate onErrorReceivedDelegate;

        #endregion

        #region GUI_STATE_VARIABLES

        // General State
        private bool m_IsConnectedToBria = false;
        private bool m_IsActiveOnThePhone = false;
        private String m_lastNumberEntered = "";

        // Phone State
        private bool m_IsPhoneReady = false;
        private bool m_AreCallsAllowed = false;
        private Int16 m_ErrorCode;
        private Int16 m_MaxLines;
        private String m_CallNotAllowedReason;
        private BriaAPI.AccountStates m_AccountStatus;

        // Phone Lines
        public class RemoteParty
        {
            public String Number;
            public String DisplayName;
            public BriaAPI.CallStates State;
            public Int64 TimeInitiated;
        }
        public class PhoneLine
        {
            public String Id;
            public BriaAPI.HoldStates HoldState;
            public List<RemoteParty> RemoteParties;

            public PhoneLine(String id)
            {
                Id = id;
                RemoteParties = new List<RemoteParty>();
            }

            public Boolean IsRinging;
        }
        private PhoneLine[] phoneLines = new PhoneLine[6];

        // Call Options
        private bool m_IsAnonymousEnabled = false;
        private bool m_IsLettersToNumbersEnabled = false;
        private bool m_IsAutoAnswerEnabled = false;

        // Audio Properties
        private bool m_SpeakerModeEnabled = false;
        private bool m_MicrophoneIsMuted = false;
        private bool m_SpeakerIsMuted = false;
        private Int16 m_SpeakerVolume = 100;
        private Int16 m_MicrophoneVolume = 100;

        // Missed Calls
        private int m_MissedCallsCount = 0;

        // VoiceMail
        private bool m_MessagesAreWaiting = false;

        #endregion

        public BriaManager()
        {
            briaAPI = new BriaAPI();

            briaAPI.OnStatusChanged += new EventHandler<BriaAPI.StatusChangedEventArgs>(OnStatusChanged);

            briaAPI.OnPhoneStatus += new EventHandler<BriaAPI.PhoneStatusEventArgs>(OnPhoneStatus);
            briaAPI.OnCallStatus += new EventHandler<BriaAPI.CallStatusEventArgs>(OnCallStatus);
            briaAPI.OnCallOptionsStatus += new EventHandler<BriaAPI.CallOptionsStatusEventArgs>(OnCallOptionsStatus);
            briaAPI.OnAudioPropertiesStatus += new EventHandler<BriaAPI.AudioPropertiesStatusEventArgs>(OnAudioPropertiesStatus);
            
            briaAPI.OnMissedCallsStatus += new EventHandler<BriaAPI.MissedCallsStatusEventArgs>(OnMissedCallsStatus);
            briaAPI.OnVoiceMailStatus += new EventHandler<BriaAPI.VoiceMailStatusEventArgs>(OnVoiceMailStatus);
            briaAPI.OnCallHistoryStatus += new EventHandler<BriaAPI.CallHistoryStatusEventArgs>(OnCallHistoryStatus);
            briaAPI.OnSystemSettingsStatus += new EventHandler<BriaAPI.SystemSettingsStatusEventArgs>(OnSystemSettingsStatus);
            briaAPI.OnErrorReceived += new EventHandler<BriaAPI.ErrorReceivedEventArgs>(OnErrorReceived);

            briaAPI.OnError += new EventHandler(OnError);
            briaAPI.OnDisconnected += new EventHandler(OnDisconnected);
            briaAPI.OnConnected += new EventHandler(OnConnected);

            briaAPI.Start();


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

        ~BriaManager()
        {
            Dispose(false);
        }

        //==================================================

        public bool OpenSoftphone()
        {
            

            return false;
        }


        //==================================================

        #region BRIA_API_WRAPPER_EVENT_HANDLERS

        // Most of these event handlers are used only to 'transport' the event onto the Main Thread
        // using a Delegate pattern. Only 'OnStatusChanged' does not follow this model.

        private void OnConnected(object sender, EventArgs args)
        {
            System.Diagnostics.Debug.WriteLine("LIB :: OnConnected ");
            var handle = SoftphoneStateChanged;
            if (handle != null)
            {
                handle(this, new BriaSoftPhoneEventArgs("OnConnected"));
            }

        }

        private void OnError(object sender, EventArgs args)
        {
            System.Diagnostics.Debug.WriteLine("LIB :: OnError ");
        }

        private void OnDisconnected(object sender, EventArgs args)
        {
            System.Diagnostics.Debug.WriteLine("LIB :: OnDisconnected ");
            var handle = SoftphoneStateChanged;
            if (handle != null)
            {
                handle(this, new BriaSoftPhoneEventArgs("OnDisconnected"));
            }
        }

        private void OnStatusChanged(object sender, BriaAPI.StatusChangedEventArgs args)
        {
            // This event can largely be handled by the Wrapper and the default handling there,
            // but for the CallHistoryStatusChanged we don't want to automtaically fetch the
            // entire list on every change, only when we want to display it, so that event we
            // claim to handle here

            // Adding the SystemSettingsStatusChanged to this category also

            if ((args.StatusType == BriaAPI.StatusTypes.callHistory)
               || (args.StatusType == BriaAPI.StatusTypes.systemSettings))
            {
                args.Handled = true;
            }
        }

        private void OnPhoneStatus(object sender, BriaAPI.PhoneStatusEventArgs args)
        {
            System.Diagnostics.Debug.WriteLine("LIB :: OnPhoneStatus ");
        }

        private void OnCallStatus(object sender, BriaAPI.CallStatusEventArgs args)
        {
            //System.Diagnostics.Debug.WriteLine("LIB :: OnCallStatus ");
            Boolean[] lineInUse = new Boolean[6];

            List<BriaAPI.Call> callList = args.CallList;

            //System.Diagnostics.Debug.WriteLine("LIB :: OnCallStatus callList " + callList.Count);

            if (callList.Count == 0)
            {
                var handle = SoftphoneStateChanged;
                if (handle != null)
                {
                    handle(this, new BriaSoftPhoneEventArgs("EndCall"));
                }
            
            }


            foreach (BriaAPI.Call call in callList)
            {
                Boolean existingCall = false;
                PhoneLine phoneLine = null;

                // Check if the call was previously in the list
                for (int i = 0; i < 6; i++)
                {
                    phoneLine = phoneLines[i];

                    if ((phoneLine != null) && (phoneLine.Id == call.CallId))
                    {
                        
                        phoneLine.RemoteParties.Clear();

                        lineInUse[i] = true;
                        existingCall = true;

                       

                        break;
                    }
                }

              
                // If the call is not already existing, we have to add as new call
                if (!existingCall)
                {
                    PhoneLine newPhoneLine = new PhoneLine(call.CallId);
                    phoneLine = newPhoneLine;

                    // Find empty slot to put it in
                    for (int i = 0; i < 6; i++)
                    {
                        if (phoneLines[i] == null)
                        {
                            phoneLines[i] = phoneLine;
                            lineInUse[i] = true;
                            


                            break;
                        }
                    }
                }

               
                
                // And fill in the information
                phoneLine.HoldState = call.HoldState;

                foreach (BriaAPI.CallParticipant participant in call.ParticipantList)
                {
                    RemoteParty remoteParty = new RemoteParty();
                    remoteParty.Number = participant.Number;
                    remoteParty.DisplayName = participant.DisplayName;
                    remoteParty.TimeInitiated = participant.TimeInitiated;
                    remoteParty.State = participant.CallState;

                    phoneLine.RemoteParties.Add(remoteParty);

                    //System.Diagnostics.Debug.WriteLine("LIB :: OnCallStatus remoteParty.State " + remoteParty.State);
                    //
                    if ((phoneLine.RemoteParties.Count == 1) && (remoteParty.State == BriaAPI.CallStates.Ringing))
                    {
                        phoneLine.IsRinging = true;
                        //System.Diagnostics.Debug.WriteLine("LIB :: OnCallStatus IsRinging ");
                        var handle = SoftphoneStateChanged;
                        if (handle != null)
                        {
                            handle(this, new BriaSoftPhoneEventArgs("IsRinging"));
                        }

                    }
                    else if ((phoneLine.RemoteParties.Count == 1) && (remoteParty.State == BriaAPI.CallStates.Connected))
                    {
                       
                        var handle = SoftphoneStateChanged;
                        if (handle != null)
                        {
                            handle(this, new BriaSoftPhoneEventArgs("Connected"));
                        }

                    }
                    else
                    {
                        phoneLine.IsRinging = false;
                    }
                }
            }

            // Finally clear out any phoneLine that is no longer active
            for (int i = 0; i < 6; i++)
            {
                if (lineInUse[i] == false)
                {
                    phoneLines[i] = null;
                }
                
            }

        }

        private void OnCallOptionsStatus(object sender, BriaAPI.CallOptionsStatusEventArgs args)
        {
            System.Diagnostics.Debug.WriteLine("LIB :: OnCallOptionsStatus ");
        }

        private void OnAudioPropertiesStatus(object sender, BriaAPI.AudioPropertiesStatusEventArgs args)
        {
            System.Diagnostics.Debug.WriteLine("LIB :: OnAudioPropertiesStatus ");

            m_SpeakerModeEnabled = args.IsSpeakerModeEnabled;
            m_MicrophoneIsMuted = args.IsMicrophoneMuted;
            m_SpeakerIsMuted = args.IsSpeakerMuted;
            m_SpeakerVolume = args.SpeakerVolume;
            m_MicrophoneVolume = args.MicrophoneVolume;
            System.Diagnostics.Debug.WriteLine("LIB :: OnAudioPropertiesStatus m_MicrophoneIsMuted " + m_MicrophoneIsMuted);

            var handle = SoftphoneMuteStateChanged;
            if (handle != null)
            {
                handle(this, new BriaSoftPhoneMuteEventArgs(m_MicrophoneIsMuted));
            }

        }

        private void OnMissedCallsStatus(object sender, BriaAPI.MissedCallsStatusEventArgs args)
        {
            System.Diagnostics.Debug.WriteLine("LIB :: OnMissedCallsStatus ");
        }

        private void OnVoiceMailStatus(object sender, BriaAPI.VoiceMailStatusEventArgs args)
        {
            System.Diagnostics.Debug.WriteLine("LIB :: OnVoiceMailStatus ");
        }

        private void OnCallHistoryStatus(object sender, BriaAPI.CallHistoryStatusEventArgs args)
        {
            System.Diagnostics.Debug.WriteLine("LIB :: OnCallHistoryStatus ");
        }

        private void OnSystemSettingsStatus(object sender, BriaAPI.SystemSettingsStatusEventArgs args)
        {
            System.Diagnostics.Debug.WriteLine("LIB :: OnSystemSettingsStatus ");
        }

        private void OnErrorReceived(object sender, BriaAPI.ErrorReceivedEventArgs args)
        {
            System.Diagnostics.Debug.WriteLine("LIB :: OnErrorReceived ");
        }

        #endregion


        public void CallFunc()
        {
            for (int i = 0; i < 6; i++)
            {

                PhoneLine phoneLine = phoneLines[i] as PhoneLine;
                if (phoneLine != null)
                {
                   
                    if (phoneLine.IsRinging)
                    {
                        System.Diagnostics.Debug.WriteLine("LIB :: CallFunc " + phoneLine.Id.ToString());
                        phoneLine.IsRinging = false;
                        briaAPI.RequestAnswerCall(phoneLine.Id, false);
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("LIB :: CallFunc end " + phoneLine.Id.ToString());
                        briaAPI.RequestEndCall(phoneLine.Id);
                    }

                }
            }
        }

        public void MuteFunc(bool mute)
        {
            briaAPI.RequestSetMicrophoneMute(mute);
        }
       
    }

    public class BriaSoftPhoneEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the PowerMateEventArgs class.
        /// </summary>
        /// <param name="state"></param>
        public BriaSoftPhoneEventArgs(string status)
        {
            Status = status;
        }

        /// <summary>
        /// Gets the current PowerMate state.
        /// </summary>
        public String Status { get; private set; }
    }

    
    public class BriaSoftPhoneMuteEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the PowerMateEventArgs class.
        /// </summary>
        /// <param name="state"></param>
        public BriaSoftPhoneMuteEventArgs(bool mute)
        {
            Mute = mute;
        }

        /// <summary>
        /// Gets the current PowerMate state.
        /// </summary>
        public bool Mute { get; private set; }
    }
}
 