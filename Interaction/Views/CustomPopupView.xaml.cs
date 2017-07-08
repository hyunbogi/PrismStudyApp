using Prism.Interactivity.InteractionRequest;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Interaction.Views
{
    /// <summary>
    /// Interaction logic for CustomPopupView.xaml
    /// </summary>
    public partial class CustomPopupView : UserControl, IInteractionRequestAware
    {
        public CustomPopupView()
        {
            InitializeComponent();
        }

        #region IInteractionRequestAware members

        public INotification Notification { get; set; }
        public Action FinishInteraction { get; set; }

        #endregion

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            FinishInteraction?.Invoke();
        }
    }
}
