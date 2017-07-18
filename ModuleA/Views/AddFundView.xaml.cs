using System;
using System.Windows;
using System.Windows.Controls;

namespace ModuleA.Views
{
    /// <summary>
    /// Interaction logic for AddFundView.xaml
    /// </summary>
    public partial class AddFundView : UserControl, IAddFundView
    {
        private AddFundPresenter _presenter;

        public AddFundView()
        {
            InitializeComponent();
            AddButton.Click += new RoutedEventHandler(AddButton_Click);
        }

        public AddFundView(AddFundPresenter presenter)
            : this()
        {
            _presenter = presenter;
            _presenter.View = this;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddFund(this, null);
        }

        #region IAddFundView members

        public event EventHandler AddFund = delegate { };

        public string Customer
        {
            get
            {
                var selectedItem = CustomerCbx.SelectedItem as ComboBoxItem;
                return (selectedItem != null) ? selectedItem.Content.ToString() : string.Empty;
            }
        }

        public string Fund
        {
            get
            {
                var selectedItem = FundCbx.SelectedItem as ComboBoxItem;
                return (selectedItem != null) ? selectedItem.Content.ToString() : string.Empty;
            }
        }

        #endregion
    }
}
