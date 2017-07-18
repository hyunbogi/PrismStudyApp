using EventAggregation.Infrastructure;
using Prism.Events;
using System;

namespace ModuleA.Views
{
    public class AddFundPresenter
    {
        private IAddFundView _view;
        private IEventAggregator _eventAggregator;

        public AddFundPresenter(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        private void AddFund(object sender, EventArgs e)
        {
            var fundOrder = new FundOrder
            {
                CustomerId = View.Customer,
                TickerSymbol = View.Fund
            };

            if (!string.IsNullOrEmpty(fundOrder.CustomerId) &&
                !string.IsNullOrEmpty(fundOrder.TickerSymbol))
            {
                _eventAggregator.GetEvent<FundAddedEvent>().Publish(fundOrder);
            }
        }

        public IAddFundView View
        {
            get => _view;
            set
            {
                _view = value;
                _view.AddFund += AddFund;
            }
        }
    }
}
