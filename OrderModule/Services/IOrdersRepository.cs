using System.Collections.Generic;

namespace OrderModule.Services
{
    public interface IOrdersRepository
    {
        IEnumerable<Order> GetOrdersToEdit();
    }
}
