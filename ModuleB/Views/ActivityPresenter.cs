using EventAggregation.Infrastructure;
using ModuleB.Properties;
using Prism.Events;
using System.Diagnostics;
using System.Globalization;

namespace ModuleB.Views
{
    public class ActivityPresenter
    {
        private string _customerId;

        private IEventAggregator _eventAggregator;
        private SubscriptionToken _subsciptionToken;

        public ActivityPresenter(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public void FundAddedEventHandler(FundOrder fundOrder)
        {
            Debug.Assert(View != null);
            View.AddContent(fundOrder.TickerSymbol);
        }

        public bool FundOrderFilter(FundOrder fundOrder)
        {
            return fundOrder.CustomerId == _customerId;
        }

        public IActivityView View { get; set; }

        public string CustomerId
        {
            get => _customerId;
            set
            {
                _customerId = value;

                FundAddedEvent fundAddedEvent = _eventAggregator.GetEvent<FundAddedEvent>();

                if (_subsciptionToken != null)
                {
                    fundAddedEvent.Unsubscribe(_subsciptionToken);
                }

                _subsciptionToken = fundAddedEvent.Subscribe(
                    FundAddedEventHandler,
                    ThreadOption.UIThread,
                    false,
                    FundOrderFilter);

                View.SetTitle(string.Format(CultureInfo.CurrentCulture, Resources.ActivityTitle, CustomerId));
            }
        }
    }
}
