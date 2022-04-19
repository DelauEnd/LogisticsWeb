using Logistics.OrderService.Repository.Interfaces.Repositories;
using System.Threading.Tasks;

namespace Logistics.OrderService.Repository.Interfaces
{
    public interface IRepositoryManager
    {
        ICargoRepository Cargoes { get; }
        IOrderRepository Orders { get; }
        ICargoCategoryRepository CargoCategories { get; }
        ICustomerRepository Customers { get; }
        Task SaveAsync();
    }
}
