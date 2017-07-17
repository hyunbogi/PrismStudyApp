using Prism.Commands;

namespace OrderModule.Commands
{
    public static class OrdersCommand
    {
        public static CompositeCommand SaveAllOrdersCommand = new CompositeCommand();
    }

    public class OrdersCommandProxy
    {
        public virtual CompositeCommand SaveAllOrdersCommand
        {
            get => OrdersCommand.SaveAllOrdersCommand;
        }
    }
}
