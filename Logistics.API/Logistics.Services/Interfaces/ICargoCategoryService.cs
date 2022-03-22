using Logistics.Models.RequestDTO.CreateDTO;
using Logistics.Models.RequestDTO.UpdateDTO;
using Logistics.Models.ResponseDTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Logistics.Services.Interfaces
{
    public interface ICargoCategoryService
    {
        public Task<IEnumerable<CargoCategoryDto>> GetAllCategories();
        public Task AddCategory(CategoryForCreationDto category);
        public Task DeleteCategoryById(int categoryId);
        public Task UpdateCargoCategoryById(int categoryId, CargoCategoryForUpdateDto category);
    }
}
