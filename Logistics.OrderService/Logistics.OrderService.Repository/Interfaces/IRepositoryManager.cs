using System.Threading.Tasks;

namespace Logistics.Repository.Interfaces
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
