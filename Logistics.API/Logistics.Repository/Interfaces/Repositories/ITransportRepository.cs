using Logistics.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Logistics.Repository.Interfaces
{
    public interface ITransportRepository
    {
        Task<IEnumerable<Transport>> GetAllTransportAsync(bool trackChanges);
        Task<Transport> GetTransportByIdAsync(int id, bool trackChanges);
        Task<Transport> GetTransportByRegistrationNumberAsync(string number, bool trackChanges);
        void CreateTransport(Transport transport);
        void DeleteTransport(Transport transport);
    }
}
