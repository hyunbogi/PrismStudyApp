using OrderModule.Commands;
using OrderModule.Services;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Data;
using System.Windows.Input;

namespace OrderModule.ViewModels
{
    public class OrdersEditorViewModel : BindableBase
    {
        private readonly IOrdersRepository _ordersRepository;
        private readonly OrdersCommandProxy _commandProxy;

        private ObservableCollection<OrderViewModel> _orders;

        public OrdersEditorViewModel(IOrdersRepository ordersRepository, OrdersCommandProxy commandProxy)
        {
            _ordersRepository = ordersRepository;
            _commandProxy = commandProxy;

            PopulateOrders();

            Orders = new ListCollectionView(_orders);
            Orders.CurrentChanged += SelectedOrder_Changed;
            Orders.MoveCurrentTo(null);

            ProcessOrderCommand = new DelegateCommand<object>(ProcessOrder);
        }

        private void SelectedOrder_Changed(object sender, EventArgs e)
        {
            SelectedOrder = Orders.CurrentItem as OrderViewModel;
            RaisePropertyChanged(nameof(SelectedOrder));
        }

        public ICollectionView Orders { get; private set; }

        public OrderViewModel SelectedOrder { get; private set; }

        public ICommand ProcessOrderCommand { get; private set; }

        private void ProcessOrder(object parameter)
        {
            Debug.WriteLine($"Processing order {SelectedOrder.OrderName} with parameter {parameter}");
        }

        private void PopulateOrders()
        {
            _orders = new ObservableCollection<OrderViewModel>();

            foreach (Order order in _ordersRepository.GetOrdersToEdit())
            {
                var orderViewModel = new OrderViewModel(order);
                _orders.Add(orderViewModel);

                orderViewModel.Saved += Order_Saved;

                _commandProxy.SaveAllOrdersCommand.RegisterCommand(orderViewModel.SaveOrderCommand);
            }
        }

        private void Order_Saved(object sender, Prism.Events.DataEventArgs<OrderViewModel> e)
        {
            if (e != null && e.Value != null)
            {
                OrderViewModel order = e.Value;
                if (_orders.Contains(order))
                {
                    order.Saved -= Order_Saved;
                    _commandProxy.SaveAllOrdersCommand.UnregisterCommand(order.SaveOrderCommand);
                    _orders.Remove(order);
                }
            }
        }
    }
}
