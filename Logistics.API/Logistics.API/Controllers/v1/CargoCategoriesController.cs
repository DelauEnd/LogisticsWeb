﻿using Logistics.Models.Enums;
using Logistics.Models.RequestDTO.CreateDTO;
using Logistics.Models.RequestDTO.UpdateDTO;
using Logistics.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Logistics.API.Controllers.v1
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
        /// <returns>Returns requested category</returns>
        [HttpPost, Authorize(Roles = nameof(UserRole.Administrator))]
        public async Task<IActionResult> AddCategory([FromBody] CategoryForCreationDto category)
        {
            await _cargoCategoryService.AddCategory(category);
            return Ok();
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
        [HttpPut("{categoryId}"), Authorize(Roles = nameof(UserRole.Administrator))]
        public async Task<IActionResult> UpdateCargoCategoryById(int categoryId, CargoCategoryForUpdateDto category)
        {
            await _cargoCategoryService.UpdateCargoCategoryById(categoryId, category);
            return Ok();
        }
    }
}