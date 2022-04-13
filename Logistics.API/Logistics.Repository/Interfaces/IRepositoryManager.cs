using System.Threading.Tasks;

namespace Logistics.Repository.Interfaces
{
    public interface IRepositoryManager
    {       
        IRouteRepository Routes { get; }
        ITransportRepository Transport { get; }
        Task SaveAsync();
    }
}
