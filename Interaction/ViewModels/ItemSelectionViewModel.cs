using Interaction.Notifications;
using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using System;
using System.Windows.Input;

namespace Interaction.ViewModels
{
    public class ItemSelectionViewModel : BindableBase, IInteractionRequestAware
    {
        private ItemSelectionNotification _notification;

        public ItemSelectionViewModel()
        {
            SelectItemCommand = new DelegateCommand(AcceptSelectedItem);
            CancelCommand = new DelegateCommand(CancelInteraction);
        }

        public string SelectedItem { get; set; }

        #region Commands

        public ICommand SelectItemCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }

        #endregion

        #region IInteractionRequestAware members

        public INotification Notification
        {
            get => _notification;
            set
            {
                if (value is ItemSelectionNotification)
                {
                    _notification = value as ItemSelectionNotification;
                    RaisePropertyChanged();
                }
            }
        }

        public Action FinishInteraction { get; set; }

        #endregion

        private void AcceptSelectedItem()
        {
            if (_notification != null)
            {
                _notification.SelectedItem = SelectedItem;
                _notification.Confirmed = true;
            }
            FinishInteraction?.Invoke();
        }

        private void CancelInteraction()
        {
            if (_notification != null)
            {
                _notification.SelectedItem = null;
                _notification.Confirmed = false;
            }
            FinishInteraction?.Invoke();
        }
    }
}
