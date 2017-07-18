using Prism.Events;

namespace EventAggregation.Infrastructure
{
    public class FundAddedEvent : PubSubEvent<FundOrder>
    {
    }
}
