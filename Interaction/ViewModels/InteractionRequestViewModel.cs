using Interaction.Notifications;
using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using System.Windows.Input;

namespace Interaction.ViewModels
{
    public class InteractionRequestViewModel : BindableBase
    {
        private string _resultMessage;

        public InteractionRequestViewModel()
        {
            ConfirmationRequest = new InteractionRequest<IConfirmation>();
            NotificationRequest = new InteractionRequest<INotification>();
            CustomPopupViewRequest = new InteractionRequest<INotification>();
            ItemSelectionRequest = new InteractionRequest<ItemSelectionNotification>();

            RaiseConfirmationCommand = new DelegateCommand(RaiseConfirmation);
            RaiseNotificationCommand = new DelegateCommand(RaiseNotification);
            RaiseCustomPopupViewCommand = new DelegateCommand(RaiseCustomPopupView);
            RaiseItemSelectionCommand = new DelegateCommand(RaiseItemSelection);
        }

        public string InteractionResultMessage
        {
            get => _resultMessage;
            set => SetProperty(ref _resultMessage, value);
        }

        public InteractionRequest<IConfirmation> ConfirmationRequest { get; private set; }
        public InteractionRequest<INotification> NotificationRequest { get; private set; }
        public InteractionRequest<INotification> CustomPopupViewRequest { get; private set; }
        public InteractionRequest<ItemSelectionNotification> ItemSelectionRequest { get; private set; }

        public ICommand RaiseNotificationCommand { get; private set; }
        public ICommand RaiseConfirmationCommand { get; private set; }
        public ICommand RaiseCustomPopupViewCommand { get; private set; }
        public ICommand RaiseItemSelectionCommand { get; private set; }

        private void RaiseConfirmation()
        {
            ConfirmationRequest.Raise(
                new Confirmation { Content = "Confirmation Message", Title = "Confirmation" },
                c => { InteractionResultMessage = c.Confirmed ? "The user accepted." : "The user cancelled."; });
        }

        private void RaiseNotification()
        {
            NotificationRequest.Raise(
                new Notification { Content = "Notification Message", Title = "Notification" },
                n => { InteractionResultMessage = "The user was notified."; });
        }

        private void RaiseCustomPopupView()
        {
            InteractionResultMessage = "";
            CustomPopupViewRequest.Raise(
                new Notification { Content = "Message for the CustomPopupView", Title = "Custom Popup" });
        }

        private void RaiseItemSelection()
        {
            var notification = new ItemSelectionNotification();
            notification.Items.Add("Item1");
            notification.Items.Add("Item2");
            notification.Items.Add("Item3");
            notification.Items.Add("Item4");
            notification.Items.Add("Item5");
            notification.Items.Add("Item6");

            notification.Title = "Items";

            InteractionResultMessage = "";
            ItemSelectionRequest.Raise(
                notification,
                returned =>
                {
                    if (returned != null && returned.Confirmed && returned.SelectedItem != null)
                    {
                        InteractionResultMessage = $"The user selected: {returned.SelectedItem}";
                    }
                    else
                    {
                        InteractionResultMessage = "The user cancelled the operation or didn't select an item.";
                    }
                });
        }
    }
}
