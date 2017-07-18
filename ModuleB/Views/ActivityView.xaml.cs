using System.Windows.Controls;

namespace ModuleB.Views
{
    /// <summary>
    /// Interaction logic for ActivityView.xaml
    /// </summary>
    public partial class ActivityView : UserControl, IActivityView
    {
        private ActivityPresenter _presenter;

        public ActivityView()
        {
            InitializeComponent();
        }

        public ActivityView(ActivityPresenter presenter)
            : this()
        {
            _presenter = presenter;
            _presenter.View = this;
        }

        #region IActivityView members

        public void AddContent(string content)
        {
            ContentPanel.Children.Add(new TextBlock { Text = content });
        }

        public void SetCustomerId(string customerId)
        {
            _presenter.CustomerId = customerId;
        }

        public void SetTitle(string title)
        {
            ActivityLabel.Content = title;
        }

        #endregion
    }
}
