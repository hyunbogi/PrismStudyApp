using OrderModule.Properties;
using OrderModule.Services;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;

namespace OrderModule.ViewModels
{
    public class OrderViewModel : BindableBase, IDataErrorInfo
    {
        private readonly IDictionary<string, string> _errors = new Dictionary<string, string>();

        private readonly Order _order;

        public OrderViewModel(Order order)
        {
            _order = order;

            SaveOrderCommand = new DelegateCommand<object>(Save, CanSave);
            PropertyChanged += OrderViewModel_PropertyChanged;

            Validate();
        }

        #region Properties

        public string OrderName
        {
            get => _order.Name;
            set
            {
                _order.Name = value;
                RaisePropertyChanged();
            }
        }

        public DateTime DeliveryDate
        {
            get => _order.DeliveryDate;
            set
            {
                if (_order.DeliveryDate != value)
                {
                    _order.DeliveryDate = value;
                    RaisePropertyChanged();
                }
            }
        }

        public int? Quantity
        {
            get => _order.Quantity;
            set
            {
                if (_order.Quantity != value)
                {
                    _order.Quantity = value;
                    RaisePropertyChanged();
                }
            }
        }

        public decimal? Price
        {
            get => _order.Price;
            set
            {
                if (_order.Price != value)
                {
                    _order.Price = value;
                    RaisePropertyChanged();
                }
            }
        }

        public decimal? Shipping
        {
            get => _order.Shipping;
            set
            {
                if (_order.Shipping != value)
                {
                    _order.Shipping = value;
                    RaisePropertyChanged();
                }
            }
        }

        public decimal Total
        {
            get
            {
                if (Price != null && Quantity != null)
                {
                    return (Price.Value * Quantity.Value) + (Shipping ?? 0);
                }
                return 0;
            }
        }

        #endregion

        private void OrderViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            string propertyName = e.PropertyName;
            if (propertyName == nameof(Price) ||
                propertyName == nameof(Quantity) ||
                propertyName == nameof(Shipping))
            {
                RaisePropertyChanged(nameof(Total));
            }

            Validate();
            SaveOrderCommand.RaiseCanExecuteChanged();
        }

        public event EventHandler<DataEventArgs<OrderViewModel>> Saved;

        public DelegateCommand<object> SaveOrderCommand { get; private set; }

        private bool CanSave(object arg)
        {
            return _errors.Count == 0 && Quantity > 0;
        }

        private void Save(object obj)
        {
            Console.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} saved.", OrderName));
            OnSaved(new DataEventArgs<OrderViewModel>(this));
        }

        private void OnSaved(DataEventArgs<OrderViewModel> e)
        {
            Saved?.Invoke(this, e);
        }

        #region IDataErrorInfo members

        public string Error => throw new NotImplementedException();

        public string this[string columnName]
        {
            get => _errors.ContainsKey(columnName) ? _errors[columnName] : null;
            set => _errors[columnName] = value;
        }

        #endregion

        private void Validate()
        {
            if (Price == null || Price <= 0)
            {
                this[nameof(Price)] = Resources.InvalidPriceRange;
            }
            else
            {
                ClearError(nameof(Price));
            }

            if (Quantity == null || Quantity <= 0)
            {
                this[nameof(Quantity)] = Resources.InvalidQuantityRange;
            }
            else
            {
                ClearError(nameof(Quantity));
            }
        }

        private void ClearError(string columnName)
        {
            if (_errors.ContainsKey(columnName))
            {
                _errors.Remove(columnName);
            }
        }
    }
}
