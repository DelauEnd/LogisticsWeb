using Logistics.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Logistics.OrderService.Repository.Interfaces.Repositories
{
    public interface ICargoCategoryRepository
    {
        Task<CargoCategory> GetCategoryByCargoIdAsync(int id, bool trackChanges);
        Task<IEnumerable<CargoCategory>> GetAllCategoriesAsync(bool trackChanges);
        void CreateCategory(CargoCategory category);
        Task<CargoCategory> GetCategoryByIdAsync(int id, bool trackChanges);
        void DeleteCategory(CargoCategory category);
    }
}
