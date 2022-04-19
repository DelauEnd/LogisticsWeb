using Logistics.Entities;
using Logistics.Entities.Models;
using Logistics.OrderService.Repository.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logistics.OrderService.Repository.Repositories
{
    public class CargoCategoryRepository : RepositoryBase<CargoCategory>, ICargoCategoryRepository
    {
        public CargoCategoryRepository(LogisticsDbContext repositoryContext)
            : base(repositoryContext)
        {

        }

        public void CreateCategory(CargoCategory category)
            => Create(category);

        public void DeleteCategory(CargoCategory category)
            => Delete(category);

        public async Task<IEnumerable<CargoCategory>> GetAllCategoriesAsync(bool trackChanges)
            => await FindAll(trackChanges)
                .ToListAsync();

        public async Task<CargoCategory> GetCategoryByCargoIdAsync(int id, bool trackChanges)
            => await FindByCondition(category =>
                 category.Cargoes.Any(cargo => cargo.Id == id), trackChanges)
                .SingleOrDefaultAsync();

        public async Task<CargoCategory> GetCategoryByIdAsync(int id, bool trackChanges)
            => await FindByCondition(category =>
                category.Id == id, trackChanges)
                .SingleOrDefaultAsync();
    }
}
