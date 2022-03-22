using Logistics.Entities;
using Logistics.Entities.Models;
using Logistics.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Logistics.Repository.Repositories
{
    public class TransportRepository : RepositoryBase<Transport>, ITransportRepository
    {
        public TransportRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public void CreateTransport(Transport transport)
            => Create(transport);

        public void DeleteTransport(Transport transport)
            => Delete(transport);

        public async Task<IEnumerable<Transport>> GetAllTransportAsync(bool trackChanges)
            => await FindAll(trackChanges)
                .ToListAsync();

        public async Task<Transport> GetTransportByIdAsync(int id, bool trackChanges)
            => await FindByCondition(transport => transport.Id == id, trackChanges)
                .SingleOrDefaultAsync();

        public async Task<Transport> GetTransportByRegistrationNumberAsync(string number, bool trackChanges)
            => await FindByCondition(transport => transport.RegistrationNumber == number, trackChanges)
                .SingleOrDefaultAsync();
    }
}
