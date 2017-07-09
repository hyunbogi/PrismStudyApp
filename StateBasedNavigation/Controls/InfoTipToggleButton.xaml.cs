using System;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace StateBasedNavigation.Controls
{
    /// <summary>
    /// InfoTipToggleButton.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class InfoTipToggleButton : ToggleButton
    {
        private static WeakReference weakOpenToggleButton;

        public InfoTipToggleButton()
        {
            InitializeComponent();

            IsThreeState = false;
            Checked += InfoTipToggleButton_Checked;
            Unchecked += InfoTipToggleButton_Checked;
        }

        public static readonly DependencyProperty PopupProperty =
            DependencyProperty.Register(nameof(PopupProperty), typeof(Popup), typeof(InfoTipToggleButton));

        public Popup Popup
        {
            get { return (Popup)GetValue(PopupProperty); }
            set { SetValue(PopupProperty, value); }
        }

        private void InfoTipToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            if ((weakOpenToggleButton != null) &&
                (weakOpenToggleButton.IsAlive) &&
                (weakOpenToggleButton.Target != this))
            {
                ((ToggleButton)weakOpenToggleButton.Target).IsChecked = false;
            }

            if (Popup != null)
            {
                if (IsChecked.HasValue && IsChecked.Value)
                {
                    Popup.PlacementTarget = this;
                    Popup.Placement = PlacementMode.Bottom;
                    Popup.IsOpen = true;

                    weakOpenToggleButton = new WeakReference(this);
                }
                else
                {
                    Popup.IsOpen = false;
                    if ((weakOpenToggleButton != null) &&
                        (weakOpenToggleButton.IsAlive) &&
                        (weakOpenToggleButton.Target == this))
                    {
                        weakOpenToggleButton = null;
                    }
                }
            }
        }
    }
}
