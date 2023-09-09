using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace FreeMateSoftPhone
{
    public class Bria6Manager : IDisposable
    {
        private string SOFTPHONENAME = "Bria";

        private bool disposed = false;

        public event EventHandler<Bria6SoftPhoneEventArgs> SoftphoneStateChanged;
        public event EventHandler<Bria6SoftPhoneMuteEventArgs> SoftphoneMuteStateChanged;


        private Bria6API briaAPI;

        #region EVENTS_DELEGATE_DEFINITIONS

        private delegate void OnConnectedDelegate();
        private OnConnectedDelegate onConnectedDelegate;

        private delegate void OnErrorDelegate();
        private OnErrorDelegate onErrorDelegate;

        private delegate void OnDisconnectedDelegate();
        private OnDisconnectedDelegate onDisconnectedDelegate;

        private delegate void OnPhoneStatusDelegate(Bria6API.PhoneStatusEventArgs args);
        private OnPhoneStatusDelegate onPhoneStatusDelegate;

        private delegate void OnCallStatusDelegate(Bria6API.CallStatusEventArgs args);
        private OnCallStatusDelegate onCallStatusDelegate;

        private delegate void OnCallOptionsStatusDelegate(Bria6API.CallOptionsStatusEventArgs args);
        private OnCallOptionsStatusDelegate onCallOptionsStatusDelegate;

        private delegate void OnAudioPropertiesStatusDelegate(Bria6API.AudioPropertiesStatusEventArgs args);
        private OnAudioPropertiesStatusDelegate onAudioPropertiesStatusDelegate;

        private delegate void OnMissedCallsStatusDelegate(Bria6API.MissedCallsStatusEventArgs args);
        private OnMissedCallsStatusDelegate onMissedCallsStatusDelegate;

        private delegate void OnVoiceMailStatusDelegate(Bria6API.VoiceMailStatusEventArgs args);
        private OnVoiceMailStatusDelegate onVoiceMailStatusDelegate;

        private delegate void OnCallHistoryStatusDelegate(Bria6API.CallHistoryStatusEventArgs args);
        private OnCallHistoryStatusDelegate onCallHistoryStatusDelegate;

        private delegate void OnSystemSettingsStatusDelegate(Bria6API.SystemSettingsStatusEventArgs args);
        private OnSystemSettingsStatusDelegate onSystemSettingsStatusDelegate;

        private delegate void OnErrorReceivedDelegate(Bria6API.ErrorReceivedEventArgs args);
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
        private Bria6API.AccountStates m_AccountStatus;

        // Phone Lines
        public class RemoteParty
        {
            public String Number;
            public String DisplayName;
            public Bria6API.CallStates State;
            public Int64 TimeInitiated;
        }
        public class PhoneLine
        {
            public String Id;
            public Bria6API.HoldStates HoldState;
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

        public Bria6Manager()
        {
            briaAPI = new Bria6API();

            briaAPI.OnStatusChanged += new EventHandler<Bria6API.StatusChangedEventArgs>(OnStatusChanged);

            briaAPI.OnPhoneStatus += new EventHandler<Bria6API.PhoneStatusEventArgs>(OnPhoneStatus);
            briaAPI.OnCallStatus += new EventHandler<Bria6API.CallStatusEventArgs>(OnCallStatus);
            briaAPI.OnCallOptionsStatus += new EventHandler<Bria6API.CallOptionsStatusEventArgs>(OnCallOptionsStatus);
            briaAPI.OnAudioPropertiesStatus += new EventHandler<Bria6API.AudioPropertiesStatusEventArgs>(OnAudioPropertiesStatus);

            briaAPI.OnMissedCallsStatus += new EventHandler<Bria6API.MissedCallsStatusEventArgs>(OnMissedCallsStatus);
            briaAPI.OnVoiceMailStatus += new EventHandler<Bria6API.VoiceMailStatusEventArgs>(OnVoiceMailStatus);
            briaAPI.OnCallHistoryStatus += new EventHandler<Bria6API.CallHistoryStatusEventArgs>(OnCallHistoryStatus);
            briaAPI.OnSystemSettingsStatus += new EventHandler<Bria6API.SystemSettingsStatusEventArgs>(OnSystemSettingsStatus);
            briaAPI.OnErrorReceived += new EventHandler<Bria6API.ErrorReceivedEventArgs>(OnErrorReceived);

            briaAPI.OnError += new EventHandler(OnError);
            briaAPI.OnDisconnected += new EventHandler(OnDisconnected);
            briaAPI.OnConnected += new EventHandler(OnConnected);

            ConnectToBria();

        }

        public void ConnectToBria()
        {
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

        ~Bria6Manager()
        {
            Dispose(false);
        }

        //==================================================

        public bool OpenSoftphone()
        {
            Process[] ProcName = Process.GetProcessesByName(SOFTPHONENAME);
            if (ProcName.Length > 0)
            {
                //new Thread(IsRunning).Start();
                return true;
            }
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
                handle(this, new Bria6SoftPhoneEventArgs("OnConnected"));
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
                handle(this, new Bria6SoftPhoneEventArgs("OnDisconnected"));
            }
        }

        private void OnStatusChanged(object sender, Bria6API.StatusChangedEventArgs args)
        {
            // This event can largely be handled by the Wrapper and the default handling there,
            // but for the CallHistoryStatusChanged we don't want to automtaically fetch the
            // entire list on every change, only when we want to display it, so that event we
            // claim to handle here

            // Adding the SystemSettingsStatusChanged to this category also

            if ((args.StatusType == Bria6API.StatusTypes.callHistory)
               || (args.StatusType == Bria6API.StatusTypes.systemSettings))
            {
                args.Handled = true;
            }
        }

        private void OnPhoneStatus(object sender, Bria6API.PhoneStatusEventArgs args)
        {
            System.Diagnostics.Debug.WriteLine("LIB :: OnPhoneStatus ");
        }

        private void OnCallStatus(object sender, Bria6API.CallStatusEventArgs args)
        {
            System.Diagnostics.Debug.WriteLine("LIB :: OnCallStatus ");
            Boolean[] lineInUse = new Boolean[6];

            List<Bria6API.Call> callList = args.CallList;

            //System.Diagnostics.Debug.WriteLine("LIB :: OnCallStatus callList " + callList.Count);

            if (callList.Count == 0)
            {
                var handle = SoftphoneStateChanged;
                if (handle != null)
                {
                    handle(this, new Bria6SoftPhoneEventArgs("EndCall"));
                }

            }


            foreach (Bria6API.Call call in callList)
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

                foreach (Bria6API.CallParticipant participant in call.ParticipantList)
                {
                    RemoteParty remoteParty = new RemoteParty();
                    remoteParty.Number = participant.Number;
                    remoteParty.DisplayName = participant.DisplayName;
                    remoteParty.TimeInitiated = participant.TimeInitiated;
                    remoteParty.State = participant.CallState;

                    phoneLine.RemoteParties.Add(remoteParty);

                    //System.Diagnostics.Debug.WriteLine("LIB :: OnCallStatus remoteParty.State " + remoteParty.State);
                    //
                    if ((phoneLine.RemoteParties.Count == 1) && (remoteParty.State == Bria6API.CallStates.Ringing))
                    {
                        phoneLine.IsRinging = true;
                        //System.Diagnostics.Debug.WriteLine("LIB :: OnCallStatus IsRinging ");
                        var handle = SoftphoneStateChanged;
                        if (handle != null)
                        {
                            handle(this, new Bria6SoftPhoneEventArgs("IsRinging"));
                        }

                    }
                    else if ((phoneLine.RemoteParties.Count == 1) && (remoteParty.State == Bria6API.CallStates.Connected))
                    {

                        var handle = SoftphoneStateChanged;
                        if (handle != null)
                        {
                            handle(this, new Bria6SoftPhoneEventArgs("Connected"));
                            phoneLine.IsRinging = false;
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

        private void OnCallOptionsStatus(object sender, Bria6API.CallOptionsStatusEventArgs args)
        {
            System.Diagnostics.Debug.WriteLine("LIB :: OnCallOptionsStatus ");
        }

        private void OnAudioPropertiesStatus(object sender, Bria6API.AudioPropertiesStatusEventArgs args)
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
                handle(this, new Bria6SoftPhoneMuteEventArgs(m_MicrophoneIsMuted));
            }

        }

        private void OnMissedCallsStatus(object sender, Bria6API.MissedCallsStatusEventArgs args)
        {
            System.Diagnostics.Debug.WriteLine("LIB :: OnMissedCallsStatus ");
        }

        private void OnVoiceMailStatus(object sender, Bria6API.VoiceMailStatusEventArgs args)
        {
            System.Diagnostics.Debug.WriteLine("LIB :: OnVoiceMailStatus ");
        }

        private void OnCallHistoryStatus(object sender, Bria6API.CallHistoryStatusEventArgs args)
        {
            System.Diagnostics.Debug.WriteLine("LIB :: OnCallHistoryStatus ");
        }

        private void OnSystemSettingsStatus(object sender, Bria6API.SystemSettingsStatusEventArgs args)
        {
            System.Diagnostics.Debug.WriteLine("LIB :: OnSystemSettingsStatus ");
        }

        private void OnErrorReceived(object sender, Bria6API.ErrorReceivedEventArgs args)
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

    public class Bria6SoftPhoneEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the PowerMateEventArgs class.
        /// </summary>
        /// <param name="state"></param>
        public Bria6SoftPhoneEventArgs(string status)
        {
            Status = status;
        }

        /// <summary>
        /// Gets the current PowerMate state.
        /// </summary>
        public String Status { get; private set; }
    }


    public class Bria6SoftPhoneMuteEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the PowerMateEventArgs class.
        /// </summary>
        /// <param name="state"></param>
        public Bria6SoftPhoneMuteEventArgs(bool mute)
        {
            Mute = mute;
        }

        /// <summary>
        /// Gets the current PowerMate state.
        /// </summary>
        public bool Mute { get; private set; }
    }
}