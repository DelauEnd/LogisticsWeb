using Logistics.Entities;
using Logistics.Entities.Models;
using Logistics.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Logistics.Repository.Repositories
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(LogisticsDbContext repositoryContext)
            : base(repositoryContext)
        {

        }

        public void CreateOrder(Order order)
            => Create(order);

        public void DeleteOrder(Order order)
            => Delete(order);

        public async Task<IEnumerable<Order>> GetAllOrdersAsync(bool trackChanges)
            => await FindAll(trackChanges)
                .Include(route => route.Destination).Include(route => route.Sender)
                .ToListAsync();


        public async Task<Order> GetOrderByIdAsync(int id, bool trackChanges)
            => await FindByCondition(order => order.Id == id, trackChanges)
                .Include(order => order.Cargoes).ThenInclude(cargo => cargo.Category)
                .Include(route => route.Destination).Include(route => route.Sender)
                .SingleOrDefaultAsync();
    }
}
