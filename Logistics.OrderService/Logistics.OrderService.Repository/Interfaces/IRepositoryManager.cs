using System.Threading.Tasks;

namespace Logistics.Repository.Interfaces
{
    public interface IRepositoryManager
    {
        ICargoRepository Cargoes { get; }
        IOrderRepository Orders { get; }
        Task SaveAsync();
    }
}
