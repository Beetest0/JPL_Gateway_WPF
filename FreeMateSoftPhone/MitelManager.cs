using Mitel.Communicator.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FreeMateSoftPhone
{
    public class MitelManager : IDisposable
    {
        private volatile bool _shouldStop;
        private volatile bool isOutGoingCall;
        private volatile bool _ismute;

        private bool disposed = false;

        private ApiClient client = new ApiClient();
        private Events events = new Events();
        public DeviceEndpoint mDeviceEndpoint;

        public event EventHandler<SoftPhoneStatusEventArgs> SoftphoneStateChanged;


        public MitelManager()
        {
            _shouldStop = false;
            SubscribeToClientEvents();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

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

        ~MitelManager()
        {
            Dispose(false);
        }


        private void SubscribeToClientEvents()
        {

            client.AddChatParticipant += (sender, args) => events.OnAddChatSessionParticipant(args.SessionId, args.Contacts);
            client.AutoAnswer += (sender, args) => events.OnAutoAnswer(args.Endpoint, args.Enabled);
            client.SetAutoAnswerFailure += (sender, args) => events.OnSetAutoAnswerFailure(args.Endpoint, args.Error);
            client.ChatAutoReplyMessage += (sender, args) => events.OnAutoReplyChatMessage(args.SessionId, args.Contact, args.Message, args.Epoch);
            client.ChatFileLinkMessage += (sender, args) => events.OnChatFileLinkMessage(args.SessionId, args.Contact, args.Url, args.Epoch, args.FileSize);
            client.ChatMessage += (sender, args) => events.OnChatMessage(args.SessionId, args.Contact, args.Message, args.Epoch);
            client.ChatSessionCreated += (sender, args) => events.OnChatSessionCreated(args.SessionId, args.Contacts);
            client.ChatSessionDestroyed += (sender, args) => events.OnChatSessionRemoved(args.SessionId);
            client.ChatTypingIndicated += (sender, args) => events.OnChatTypingIndication(args.SessionId, args.Contact, args.TypingState);
            client.EndpointsListChanged += (sender, args) => events.OnEndpointsListChanged();

            client.ConnectedCall += (sender, args) => OnConnected(args.Endpoint, args.SessionId, args.Name, args.Number);

            client.DeviceOnline += (sender, args) => events.OnDeviceOnline(args.Endpoint, args.Enabled);
            client.SipSoftphoneActivated += (sender, args) => events.OnSipSoftphoneActivated(args.SipSoftphoneDn, args.NewOwnerDescription);
            client.SetSipSoftphoneSettingsFailure += (sender, args) => events.OnSetSipSoftphoneSettingsFailure(args.Endpoint, args.Error);
            
            client.DisconnectedCall += (sender, args) => events.OnDisconnected(args.Endpoint, args.SessionId);
            
            client.DoNotDisturb += (sender, args) => events.OnDoNotDisturb(args.Endpoint, args.Enabled);
            client.SetDoNotDisturbFailure += (sender, args) => events.OnSetDoNotDisturbFailure(args.Endpoint, args.Error);
            client.Forwarding += (sender, args) => events.OnForwarding(args.Endpoint, args.ForwardingInfo);
            client.SetForwardingFailure += (sender, args) => events.OnSetForwardingFailure(args.Endpoint, args.Error);
            client.CallOriginated += (sender, args) => events.OnCallOriginated(args.Endpoint, args.SessionId);
            
            client.IncomingCall += (sender, args) => OnIncomingCall(args.Endpoint, args.SessionId, args.Name, args.Number);
            
            client.LoggedOn += (sender, args) => events.OnLoggedOn(args.LoggedOn);
            client.UserPresenceStatusReady += (sender, args) => events.OnUserPresenceStatusReady();

            client.PresenceStatusUpdate += (sender, args) => OnPresenceStatusUpdate(args.UserId, args.UserStatus);

            client.RemoveChatParticipant += (sender, args) => events.OnRemoveChatSessionParticipant(args.SessionId, args.Contacts);
            client.RingBackCall += (sender, args) => events.OnRingback(args.Endpoint, args.SessionId, args.Name, args.Number);
            client.UCATerminating += (sender, args) => events.OnUCATerminating();
            client.LogonCancelled += (sender, args) => events.OnLogonCancelled();
        }


        public void CloseDevice()
        {

            _shouldStop = true;


            System.Diagnostics.Debug.WriteLine("lib :: CloseDevice ");

            
        }


        //=====================================================================================

        public void OnIncomingCall(DeviceEndpoint deviceEndpoint, string sessionID, string name, string number)
        {
            mDeviceEndpoint = deviceEndpoint;
           /*
            this.DisplayMessage(string.Format(
               "OnIncomingCall: DeviceEndpoint = {0}, Session ID= {1}, Caller Name = {2}, Caller Number = {3}",
               deviceEndpoint == null ? string.Empty : deviceEndpoint.Number,
               sessionID,
               name,
               number));
            */
            
           
        }

        public void OnConnected(DeviceEndpoint deviceEndpoint, string sessionID, string name, string number)
        {
            /*
            this.DisplayMessage(string.Format(
               "OnConnected: DeviceEndpoint = {0}, Session ID= {1}, Other Name = {2}, Other Number = {3}",
               deviceEndpoint == null ? string.Empty : deviceEndpoint.Number,
               sessionID,
               name,
               number));
             * */
            
        }


      

        public void OnDisconnected(DeviceEndpoint deviceEndpoint, string sessionID)
        {
            /*
            this.DisplayMessage(string.Format("OnDisconnected: DeviceEndpoint = {0}, Session ID= {1}", deviceEndpoint == null ? string.Empty : deviceEndpoint.Number, sessionID));
             * */
           
        }


        public void OnPresenceStatusUpdate(string userId, UserStatus status)
        {
            //Console.WriteLine("OnPresenceStatusUpdate: {0} {1}", userId, DisplayPresenceStatus(status));
            var handle = SoftphoneStateChanged;
            if (handle != null)
            {
                handle(this, new SoftPhoneStatusEventArgs(status.TelephonyState.ToString()));
            }
        }



        private void DisplayMessage(string msg)
        {
            Console.WriteLine(msg);
        }

        public void AnserCall()
        {
            client.Answer(mDeviceEndpoint);
        }

        public void HangupCall()
        {
            client.Hangup(mDeviceEndpoint);
            mDeviceEndpoint = null;
        }

        //=====================================================================================


        public class Events
        {
            string eventHandlerDisplay;

            public Events()
            {
                eventHandlerDisplay = "Console"; // "MessageBox";          
            }

            /// <summary>
            /// The on user presence status ready.
            /// </summary>
            public void OnUserPresenceStatusReady()
            {
                this.DisplayMessage("User presence status ready.");
            }

            /// <summary>
            /// The on presence status update.
            /// </summary>
            /// <param name="userId">
            /// The user id.
            /// </param>
            /// <param name="status">
            /// The status.
            /// </param>
            public void OnPresenceStatusUpdate(string userId, UserStatus status)
            {
                Console.WriteLine("OnPresenceStatusUpdate: {0} {1}", userId, DisplayPresenceStatus(status));
            }

            private string DisplayPresenceStatus(UserStatus status)
            {
                string retVal = status.UserId;
                retVal += "\n\t" + status.TelephonyState;
                retVal += "\n\t" + status.ImState;
                retVal += "\n\t" + (string.IsNullOrWhiteSpace(status.Advisory) ? "No advisory" : status.Advisory);
                retVal += "\n\t" + (string.IsNullOrWhiteSpace(status.CustomText) ? "No custom text" : status.CustomText);

                return retVal;
            }

            /// <summary>
            /// The on endpoints list changed event.
            /// </summary>
            public void OnEndpointsListChanged()
            {
                this.DisplayMessage("Endpoints list changed.");
            }

            /// <summary>
            /// The call originated event. 
            /// </summary>
            /// <param name="deviceEndpoint">The DeviceEndpoint initiating dialing activity.</param>
            /// <param name="sessionID">The session ID for the call.</param>
            public void OnCallOriginated(DeviceEndpoint deviceEndpoint, string sessionID)
            {
                this.DisplayMessage(string.Format(
                   "OnCallOriginated: DeviceEndpoint = {0}, Session ID= {1}",
                   deviceEndpoint == null ? string.Empty : deviceEndpoint.Number,
                   sessionID));
            }

            /// <summary>
            /// The incoming call event. 
            /// </summary>
            /// <param name="deviceEndpoint">The DeviceEndpoint receiving the call.</param>
            /// <param name="sessionID">The session ID for the call.</param>
            /// <param name="name">The name, if available, of the incoming caller.</param>
            /// <param name="number">The number of the incoming caller.</param>
            public void OnIncomingCall(DeviceEndpoint deviceEndpoint, string sessionID, string name, string number)
            {
               // mDeviceEndpoint = deviceEndpoint;
                this.DisplayMessage(string.Format(
                   "OnIncomingCall: DeviceEndpoint = {0}, Session ID= {1}, Caller Name = {2}, Caller Number = {3}",
                   deviceEndpoint == null ? string.Empty : deviceEndpoint.Number,
                   sessionID,
                   name,
                   number));
               
            }

            /// <summary>
            /// The connected call event.
            /// </summary>
            /// <param name="deviceEndpoint">The DeviceEndpoint with the connected call.</param>
            /// <param name="sessionID">The session ID for the call.</param>
            /// <param name="name">The name, if available, of the other call party.</param>
            /// <param name="number">The number of the other call party.</param>
            public void OnConnected(DeviceEndpoint deviceEndpoint, string sessionID, string name, string number)
            {
                this.DisplayMessage(string.Format(
                   "OnConnected: DeviceEndpoint = {0}, Session ID= {1}, Other Name = {2}, Other Number = {3}",
                   deviceEndpoint == null ? string.Empty : deviceEndpoint.Number,
                   sessionID,
                   name,
                   number));
            }

            /// <summary>
            /// The disconnected call event.
            /// </summary>
            /// <param name="deviceEndpoint">The DeviceEndpoint whose call was disconnected.</param>
            /// <param name="sessionID">The session ID for the call.</param>
            public void OnDisconnected(DeviceEndpoint deviceEndpoint, string sessionID)
            {
                this.DisplayMessage(string.Format("OnDisconnected: DeviceEndpoint = {0}, Session ID= {1}", deviceEndpoint == null ? string.Empty : deviceEndpoint.Number, sessionID));
            }

            /// <summary>
            /// The ringback call event.
            /// </summary>
            /// <param name="deviceEndpoint">The DeviceEndpoint which is receiving ringback tone.</param>
            /// <param name="sessionID">The session ID for the call.</param>
            /// <param name="name">The name, if available, of the other call party.</param>
            /// <param name="number">The number of the other call party</param>
            public void OnRingback(DeviceEndpoint deviceEndpoint, string sessionID, string name, string number)
            {
                this.DisplayMessage(string.Format(
                   "OnRingback: DeviceEndpoint = {0}, Session ID= {1}, Other Name = {2}, Other Number = {3}",
                   deviceEndpoint == null ? string.Empty : deviceEndpoint.Number,
                   sessionID,
                   name,
                   number));
            }

            /// <summary>
            /// The Do Not Disturb event.
            /// </summary>
            /// <param name="deviceEndpoint">The DeviceEndpoint which set the do not disturb value.</param>
            /// <param name="enabled">true if DnD is enabled; otherwise false.</param>
            public void OnDoNotDisturb(DeviceEndpoint deviceEndpoint, bool enabled)
            {
                this.DisplayMessage(string.Format(
                   "OnDoNotDisturb: DeviceEndpoint = {0}, Enabled = {1}",
                   deviceEndpoint == null ? string.Empty : deviceEndpoint.Number,
                   enabled));
            }

            /// <summary>
            /// The Set Do Not Disturb failure event.
            /// </summary>
            /// <param name="deviceEndpoint">The DeviceEndpoint used for the set operation.</param>
            /// <param name="error">The error message returned by the operation.</param>
            public void OnSetDoNotDisturbFailure(DeviceEndpoint deviceEndpoint, string error)
            {
                this.DisplayMessage(string.Format(
                   "OnSetDoNotDisturbFailure: DeviceEndpoint = {0}, Error = {1}",
                   deviceEndpoint == null ? string.Empty : deviceEndpoint.Number,
                   error));
            }

            /// <summary>
            /// The Auto Answer event.
            /// </summary>
            /// <param name="deviceEndpoint">The DeviceEndpoint which set the auto answer value.</param>
            /// <param name="enabled">true if auto answer is enabled; otherwise false.</param>
            public void OnAutoAnswer(DeviceEndpoint deviceEndpoint, bool enabled)
            {
                this.DisplayMessage(string.Format(
                   "OnAutoAnswer: DeviceEndpoint = {0}, Enabled = {1}",
                   deviceEndpoint == null ? string.Empty : deviceEndpoint.Number,
                   enabled));
            }

            /// <summary>
            /// The Set Auto Answer failure event.
            /// </summary>
            /// <param name="deviceEndpoint">The DeviceEndpoint used for the set operation.</param>
            /// <param name="error">The error message returned by the operation.</param>
            public void OnSetAutoAnswerFailure(DeviceEndpoint deviceEndpoint, string error)
            {
                this.DisplayMessage(string.Format(
                   "OnSetAutoAnswerFailure: DeviceEndpoint = {0}, Error = {1}",
                   deviceEndpoint == null ? string.Empty : deviceEndpoint.Number,
                   error));
            }

            /// <summary>
            /// The device online/offline event.
            /// </summary>
            /// <param name="deviceEndpoint">The DeviceEndpoint whose online state changed.</param>
            /// <param name="online">true if the device is online; otherwise false.</param>
            public void OnDeviceOnline(DeviceEndpoint deviceEndpoint, bool online)
            {
                this.DisplayMessage(string.Format(
                   "OnDeviceOnline: DeviceEndpoint = {0}, Online = {1}",
                   deviceEndpoint == null ? string.Empty : deviceEndpoint.Number,
                   online));
            }

            /// <summary>
            /// The device forwarding changed event.
            /// </summary>
            /// <param name="deviceEndpoint">The DeviceEndpoint which changed the forwarding values.</param>
            /// <param name="fwdInfo">The new call forwarding information. Contains the latest call forwarding 
            /// data including unchanged values received in the event.</param>
            public void OnForwarding(DeviceEndpoint deviceEndpoint, IList<CallForwardingInfo> fwdInfoList)
            {
                foreach (CallForwardingInfo fwdInfo in fwdInfoList)
                {
                    this.DisplayMessage(string.Format(
                       "OnForwarding: DeviceEndpoint = {0}, FwdInfo Type: {1}, Number = {2}, Activate = {3}",
                       deviceEndpoint == null ? string.Empty : deviceEndpoint.Number,
                       fwdInfo.FwdType.ToString(), fwdInfo.FwdNumber, fwdInfo.ActivateForward));
                }
            }

            /// <summary>
            /// The Set Forwarding failure event.
            /// </summary>
            /// <param name="deviceEndpoint">The DeviceEndpoint used for the set operation.</param>
            /// <param name="error">The error message returned by the operation.</param>
            public void OnSetForwardingFailure(DeviceEndpoint deviceEndpoint, string error)
            {
                this.DisplayMessage(string.Format(
                   "OnSetForwardingFailure: DeviceEndpoint = {0}, Error = {1}",
                   deviceEndpoint == null ? string.Empty : deviceEndpoint.Number,
                   error));
            }

            /// <summary>
            /// The SIP soft phone activated on another device event.
            /// </summary>
            /// <param name="sipSoftPhoneDn">The SIP soft phone DN.</param>
            /// <param name="newOwnerDescription">The new owner description.</param>
            public void OnSipSoftphoneActivated(string sipSoftphoneDn, string newOwnerDescription)
            {
                this.DisplayMessage(string.Format(
                   "OnSipSoftphoneActivated: SipSoftphoneDn = {0}, NewOwnerDescription = {1}", sipSoftphoneDn, newOwnerDescription));
            }

            /// <summary>
            /// The Set SIP soft phone settings failure event.
            /// </summary>
            /// <param name="deviceEndpoint">The DeviceEndpoint used for the set operation.</param>
            /// <param name="error">The error message returned by the operation.</param>
            public void OnSetSipSoftphoneSettingsFailure(DeviceEndpoint deviceEndpoint, string error)
            {
                this.DisplayMessage(string.Format(
                   "OnSetSipSoftphoneSettingsFailure: DeviceEndpoint = {0}, Error = {1}",
                   deviceEndpoint == null ? string.Empty : deviceEndpoint.Number,
                   error));
            }

            /// <summary>
            /// The chat session created event. 
            /// </summary>
            /// <param name="sessionId">The chat session ID for the chat sesson.</param>
            /// <param name="contactList">The list of participants.</param>
            public void OnChatSessionCreated(string sessionId, IList<ContactInfo> contactList)
            {
                string contacts = string.Empty;

                if (contactList != null)
                {
                    contactList.ToList().ForEach(contact => contacts += string.Format("{0} {1}, ", contact.FirstName, contact.LastName));
                }

                this.DisplayMessage(string.Format("OnChatSessionCreated - sessionId: {0}, participants: {1}", sessionId, contacts));
            }

            /// <summary>
            /// The chat session removed event. 
            /// </summary>
            /// <param name="sessionId">The chat session ID.</param>
            public void OnChatSessionRemoved(string sessionId)
            {
                this.DisplayMessage(string.Format("OnChatSessionRemoved - sessionId: {0}", sessionId));
            }

            /// <summary>
            /// The chat message event. 
            /// </summary>
            /// <param name="sessionId">The chat session ID for the chat sesson.</param>
            /// <param name="contact">The sender of the mesage.</param>      
            /// <param name="message">The chat message.</param>
            /// <param name="epoch">The chat message creation time in Unix format.</param>
            public void OnChatMessage(string sessionId, ContactInfo contact, string message, long epoch)
            {
                this.DisplayMessage(string.Format("OnChatMessage: SessionID: {0}, From: {1} {2}, Message: {3}, Time: {4}",
                   sessionId, contact.FirstName, contact.LastName, message, this.ConvertEpochTime(epoch)));
            }

            /// <summary>
            /// The add chat session participant event. 
            /// </summary>
            /// <param name="sessionId">The chat session ID for the chat sesson.</param>
            /// <param name="contactList">The contacts to add to the chat session.</param>
            public void OnAddChatSessionParticipant(string sessionId, IList<ContactInfo> contactList)
            {
                string contacts = string.Empty;

                if (contactList != null)
                {
                    contactList.ToList().ForEach(contact => contacts += string.Format("{0} {1}, ", contact.FirstName, contact.LastName));
                }

                this.DisplayMessage(string.Format("OnAddChatSessionParticipant: SessionID: {0}, Participants: {1}", sessionId, contacts));
            }

            /// <summary>
            /// The remove chat session participant event. 
            /// </summary>
            /// <param name="sessionId">The chat session ID for the chat sesson.</param>
            /// <param name="contactList">The contacts to remove from the chat session.</param>
            public void OnRemoveChatSessionParticipant(string sessionId, IList<ContactInfo> contactList)
            {
                string contacts = string.Empty;

                if (contactList != null)
                {
                    contactList.ToList().ForEach(contact => contacts += string.Format("{0} {1}, ", contact.FirstName, contact.LastName));
                }

                this.DisplayMessage(string.Format("OnRemoveChatSessionParticipant: SessionID: {0}, Participants: {1}", sessionId, contacts));
            }

            /// <summary>
            /// The chat typing state event. 
            /// </summary>
            /// <param name="sessionId">The chat session ID for the chat sesson.</param>
            /// <param name="contact">The contact typing the message.</param>      
            /// <param name="state">The chat typing state.</param>
            public void OnChatTypingIndication(string sessionId, ContactInfo contact, ChatTypingState state)
            {
                this.DisplayMessage(string.Format("OnChatTypingIndication - SessionID: {0}, {1} {2} chat typing state: {3}", sessionId, contact.FirstName, contact.LastName, state.ToString()));
            }

            /// <summary>
            /// The chat file link message event. 
            /// </summary>
            /// <param name="sessionId">The chat session ID for the chat sesson.</param>
            /// <param name="contact">The sender of the link message.</param>      
            /// <param name="url">The URL to the file location.</param>      
            /// <param name="epoch">The file send/receive time in Unix time.</param>      
            /// <param name="fileSize">The file size.</param>
            public void OnChatFileLinkMessage(string sessionId, ContactInfo contact, string url, long epoch, long fileSize)
            {
                this.DisplayMessage(string.Format("OnChatFileLinkMessage - SessionID: {0}, From: {1} {2}, Url: {3}, Time: {4}, File size: {5}",
                   sessionId, contact.FirstName, contact.LastName, url, this.ConvertEpochTime(epoch), fileSize));
            }

            /// <summary>
            /// The auto reply chat message event. 
            /// </summary>
            /// <param name="sessionId">The chat session ID for the chat sesson.</param>
            /// <param name="typingContact">The sender of the message.</param>      
            /// <param name="message">The auto reply message.</param>
            /// <param name="epoch">The auto reply message creation time in Unix format.</param>
            public void OnAutoReplyChatMessage(string sessionId, ContactInfo contact, string message, long epoch)
            {
                this.DisplayMessage(string.Format("OnAutoReplyChatMessage: SessionID: {0}, From: {1} {2}, Auto reply message: {3}, Time: {4}",
                   sessionId, contact.FirstName, contact.LastName, message, this.ConvertEpochTime(epoch)));
            }

            /// <summary>
            /// Called when the logged on state of UCA changes.
            /// </summary>
            /// <param name="isLoggedOn">true if UCA is logged on; otherwise false.</param>
            public void OnLoggedOn(bool isLoggedOn)
            {
            }

            /// <summary>
            /// Called when UCA is terminating.
            /// </summary>
            public void OnUCATerminating()
            {
                this.DisplayMessage("OnUCATerminating");
            }

            /// <summary>
            /// Display a message on a MessageBox or on the console.
            /// </summary>
            /// <param name="msg">Message to display.</param>
            private void DisplayMessage(string msg)
            {
                if (this.eventHandlerDisplay == "MessageBox")
                {
               //     MessageBox.Show(msg);
                }
                else
                {
                    Console.WriteLine(msg);
                }
            }

            /// <summary>
            /// Converts the epoch time to window time.
            /// </summary>
            /// <param name="epoch">The Unix time in milliseconds.</param>
            /// <returns>The equivalent time in the window time format.</returns>
            private DateTime ConvertEpochTime(long epoch)
            {
                DateTime baseTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

                return baseTime.AddMilliseconds(epoch).ToLocalTime();
            }

            public void OnLogonCancelled()
            {
                this.DisplayMessage("The logon was cancelled by the user.");
            }
        }

    }

    
}
