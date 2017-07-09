using StateBasedNavigation.Infrastructure;
using System;
using System.Collections.Generic;

namespace StateBasedNavigation.Models
{
    public interface IChatService
    {
        event EventHandler ConnectionStatusChanged;

        event EventHandler<MessageReceivedEventArgs> MessageReceived;

        bool Connected { get; set; }

        void GetContacts(Action<IOperationResult<IEnumerable<Contact>>> callback);

        void SendMessage(Contact contact, string message, Action<IOperationResult> callback);
    }

    public class MessageReceivedEventArgs : EventArgs
    {
        public MessageReceivedEventArgs(Contact contact, string message)
        {
            Message = new ReceivedMessage(contact, message);
        }

        public ReceivedMessage Message { get; private set; }
    }
}
