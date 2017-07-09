using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using System;
using System.Windows.Input;

namespace StateBasedNavigation.ViewModels
{
    public class SendMessageViewModel : BindableBase, IConfirmation, IInteractionRequestAware
    {
        private string _message;

        public SendMessageViewModel()
        {
            OKCommand = new DelegateCommand(SendMessage);
            CancelCommand = new DelegateCommand(Cancel);
        }

        public string Message
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }

        public ICommand OKCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }

        #region IInteractionRequestAware members

        public INotification Notification { get; set; }
        public Action FinishInteraction { get; set; }

        #endregion

        #region IConfirmation members

        public bool Confirmed { get; set; }
        public string Title { get; set; }
        public object Content { get; set; }

        #endregion

        private void SendMessage()
        {
            Confirmed = true;
            FinishInteraction();
        }

        private void Cancel()
        {
            Confirmed = false;
            FinishInteraction();
        }
    }
}
