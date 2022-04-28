using Logistics.Models.Enums;
using Logistics.Models.RequestDTO.CreateDTO;
using Logistics.Models.RequestDTO.UpdateDTO;
using Logistics.Models.ResponseDTO;
using Logistics.Web.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RequestHandler.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CargoTransportation.Controllers
{
    [Authorize]
    [Route("CargoCategories")]
    public class CargoCategoriesController : Controller
    {
        private readonly ICargoCategoriesRequestHandler _categoriesHandler;

        public CargoCategoriesController(ICargoCategoriesRequestHandler categoriesHandler)
        {
            _categoriesHandler = categoriesHandler;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var response = await _categoriesHandler.GetAllCategories();

            if (!response.IsSuccessStatusCode)
                return new StatusCodeResult((int)response.StatusCode);

            var category = JsonConvert.DeserializeObject<IEnumerable<CargoCategoryDto>>(await response.Content.ReadAsStringAsync());
            return View(category);
        }

        [HttpGet]
        [Route("Create")]
        [Authorize(Roles = nameof(UserRole.Administrator))]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("Create")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = nameof(UserRole.Administrator))]
        public async Task<ActionResult> Create(CategoryForCreationDto category)
        {
            HttpContent content = HttpContentBuilder.BuildContent(category);
            var response = await _categoriesHandler.CreateCategory(content);

            if (!response.IsSuccessStatusCode)
                return new StatusCodeResult((int)response.StatusCode);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("{id}/Delete")]
        [Authorize(Roles = nameof(UserRole.Administrator))]
        public async Task<ActionResult> Delete(int id)
        {
            var response = await _categoriesHandler.DeleteCategoryById(id);

            if (!response.IsSuccessStatusCode)
                return new StatusCodeResult((int)response.StatusCode);

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("{id}/Edit")]
        [Authorize(Roles = nameof(UserRole.Administrator))]
        public async Task<ActionResult> Edit(int id)
        {
            var response = await _categoriesHandler.GetAllCategories();

            if (!response.IsSuccessStatusCode)
                return new StatusCodeResult((int)response.StatusCode);

            var categories = JsonConvert.DeserializeObject<IEnumerable<CargoCategoryDto>>(await response.Content.ReadAsStringAsync());
            var category = categories.Where(categ => categ.Id == id).FirstOrDefault();

            var categoryToUpdate = new CargoCategoryForUpdateDto
            {
                Title = category.Title
            };

            return View(categoryToUpdate);
        }

        [HttpPost]
        [Route("{id}/Edit")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = nameof(UserRole.Administrator))]
        public async Task<ActionResult> Edit(int id, CargoCategoryForUpdateDto category)
        {
            HttpContent content = HttpContentBuilder.BuildContent(category);
            var response = await _categoriesHandler.PutCategoryById(id, content);

            if (!response.IsSuccessStatusCode)
                return new StatusCodeResult((int)response.StatusCode);

            return RedirectToAction(nameof(Index));
        }
    }
}