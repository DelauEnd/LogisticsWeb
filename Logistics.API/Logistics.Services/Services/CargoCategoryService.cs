using AutoMapper;
using Logistics.Entities.Models;
using Logistics.Models.RequestDTO.CreateDTO;
using Logistics.Models.RequestDTO.UpdateDTO;
using Logistics.Models.ResponseDTO;
using Logistics.Repository.Interfaces;
using Logistics.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Logistics.Services.Services
{
    public class CargoCategoryService : ICargoCategoryService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public CargoCategoryService(IRepositoryManager repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task AddCategory(CategoryForCreationDto categoryToCreate)
        {
            CargoCategory category = _mapper.Map<CargoCategory>(categoryToCreate);
            _repository.CargoCategories.CreateCategory(category);
            await _repository.SaveAsync();
        }

        public async Task DeleteCategoryById(int categoryId)
        {
            CargoCategory category = await _repository.CargoCategories.GetCategoryByIdAsync(categoryId, false);
            _repository.CargoCategories.DeleteCategory(category);
            await _repository.SaveAsync();
        }

        public async Task<IEnumerable<CargoCategoryDto>> GetAllCategories()
        {
            var categories = await _repository.CargoCategories.GetAllCategoriesAsync(false);
            var categoriesDto = _mapper.Map<IEnumerable<CargoCategoryDto>>(categories);
            return categoriesDto;
        }

        public async Task UpdateCargoCategoryById(int categoryId, CargoCategoryForUpdateDto category)
        {
            var categoryToUpdate = await _repository.CargoCategories.GetCategoryByIdAsync(categoryId, false);
            _mapper.Map(category, categoryToUpdate);
            await _repository.SaveAsync();
        }
    }
}
