using Logistics.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Logistics.Repository.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order> GetOrderByIdAsync(int id, bool trackChanges);
        Task<IEnumerable<Order>> GetAllOrdersAsync(bool trackChanges);
        void CreateOrder(Order order);
        void DeleteOrder(Order order);
    }
}
