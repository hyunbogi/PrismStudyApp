using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using StateBasedNavigation.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;

namespace StateBasedNavigation.ViewModels
{
    public class ChatViewModel : BindableBase
    {
        private readonly IChatService _chatService;
        private readonly InteractionRequest<SendMessageViewModel> _sendMessageRequest;
        private readonly InteractionRequest<ReceivedMessage> _showReceivedMessageRequest;

        private readonly ObservableCollection<Contact> _contacts;
        private readonly CollectionView _contactsView;
        private readonly DelegateCommand<bool?> _showDetailsCommand;

        private bool _showDetails;
        private bool _sendingMessage;

        public ChatViewModel(IChatService chatService)
        {
            _contacts = new ObservableCollection<Contact>();
            _contactsView = new CollectionView(_contacts);
            _sendMessageRequest = new InteractionRequest<SendMessageViewModel>();
            _showReceivedMessageRequest = new InteractionRequest<ReceivedMessage>();
            _showDetailsCommand = new DelegateCommand<bool?>(ExecuteShowDetails, CanExecuteShowDetails);

            _contactsView.CurrentChanged += OnCurrentContactChanged;

            _chatService = chatService;
            _chatService.Connected = true;
            _chatService.ConnectionStatusChanged += (s, e) => RaisePropertyChanged(nameof(ConnectionStatus));
            _chatService.MessageReceived += OnMessageReceived;
            _chatService.GetContacts(
                result =>
                {
                    if (result.Error == null)
                    {
                        foreach (var item in result.Result)
                        {
                            _contacts.Add(item);
                        }
                    }
                });
        }

        public ObservableCollection<Contact> Contacts => _contacts;

        public ICollectionView ContactsView => _contactsView;

        public IInteractionRequest SendMessageRequest => _sendMessageRequest;

        public IInteractionRequest ShowReceivedMessageRequest => _showReceivedMessageRequest;

        public string ConnectionStatus
        {
            get => _chatService.Connected ? "Available" : "Unavailable";
            set => _chatService.Connected = (value == "Available");
        }

        public Contact CurrentContact => _contactsView.CurrentItem as Contact;

        public bool ShowDetails
        {
            get => _showDetails;
            set => SetProperty(ref _showDetails, value);
        }

        public bool SendingMessage
        {
            get => _sendingMessage;
            set => SetProperty(ref _sendingMessage, value);
        }

        public ICommand ShowDetailsCommand => _showDetailsCommand;

        private void ExecuteShowDetails(bool? show)
        {
            if (show != null)
            {
                ShowDetails = show.Value;
            }
        }

        private bool CanExecuteShowDetails(bool? show)
        {
            return ContactsView.CurrentItem != null;
        }

        public void SendMessage()
        {
            var contact = CurrentContact;
            var viewModel = new SendMessageViewModel { Title = $"Send message to {contact.Name}" };

            _sendMessageRequest.Raise(
                viewModel,
                sendMessage =>
                {
                    if (sendMessage.Confirmed)
                    {
                        SendingMessage = true;
                        _chatService.SendMessage(
                            contact,
                            sendMessage.Message,
                            result =>
                            {
                                SendingMessage = false;
                            });
                    }
                });
        }

        private void OnMessageReceived(object sender, MessageReceivedEventArgs e)
        {
            _showReceivedMessageRequest.Raise(e.Message);
        }

        private void OnCurrentContactChanged(object sender, System.EventArgs e)
        {
            RaisePropertyChanged(nameof(CurrentContact));
            _showDetailsCommand.RaiseCanExecuteChanged();
        }
    }
}
