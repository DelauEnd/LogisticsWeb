using Logistics.Models.Enums;
using Logistics.Models.RequestDTO.CreateDTO;
using Logistics.Models.RequestDTO.UpdateDTO;
using Logistics.OrderService.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Logistics.OrderService.Controllers
{
    [Route("api/Categories"), Authorize]
    [ApiController]
    public class CargoCategoriesController : ControllerBase
    {
        private readonly ICargoCategoryService _cargoCategoryService;
        public CargoCategoriesController(ICargoCategoryService cargoCategoryService)
        {
            _cargoCategoryService = cargoCategoryService;
        }

        /// <summary>
        /// Get list of categories
        /// </summary>
        /// <returns>Returns cargoes list</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            return Ok(await _cargoCategoryService.GetAllCategories());
        }

        /// <summary>
        /// Create category
        /// | Required role: Administrator
        /// </summary>
        /// <param name="category"></param>
        /// <returns>Returns added category</returns>
        [HttpPost, Authorize(Roles = nameof(UserRole.Administrator))]
        public async Task<IActionResult> AddCategory([FromBody] CategoryForCreationDto category)
        {
            return Ok(await _cargoCategoryService.AddCategory(category));
        }

        /// <summary>
        /// Delete category by id
        /// | Required role: Administrator
        /// </summary>
        /// <param name="categoryId"></param>
        [HttpDelete("{categoryId}"), Authorize(Roles = nameof(UserRole.Administrator))]
        public async Task<IActionResult> DeleteCategoryById(int categoryId)
        {
            await _cargoCategoryService.DeleteCategoryById(categoryId);
            return Ok();
        }

        /// <summary>
        /// Update category by id
        /// | Required role: Administrator
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="category"></param>
        /// <returns>Returns updated category</returns>
        [HttpPut("{categoryId}"), Authorize(Roles = nameof(UserRole.Administrator))]
        public async Task<IActionResult> UpdateCargoCategoryById(int categoryId, CargoCategoryForUpdateDto category)
        {
            return Ok(await _cargoCategoryService.UpdateCargoCategoryById(categoryId, category));
        }
    }
}