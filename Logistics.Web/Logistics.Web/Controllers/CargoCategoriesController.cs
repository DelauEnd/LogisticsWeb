using CargoTransportation.ActionFilters;
using Logistics.Models.RequestDTO.CreateDTO;
using Logistics.Models.RequestDTO.UpdateDTO;
using Logistics.Models.ResponseDTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CargoTransportation.Controllers
{
    [Route("Cargoes/Categories")]
    [ServiceFilter(typeof(AuthenticatedAttribute))]
    public class CargoCategoriesController : ExtendedControllerBase
    {
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var response = await request.CargoCategoriesRequestHandler.GetAllCategories();

            if (!response.IsSuccessStatusCode)
                return UnsuccesfullStatusCode(response);

            var category = JsonConvert.DeserializeObject<IEnumerable<CargoCategoryDto>>(await response.Content.ReadAsStringAsync());
            return View(category);
        }

        [HttpGet]
        [Route("Create")]
        [ServiceFilter(typeof(HasAdministratorRole))]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("Create")]
        [ValidateAntiForgeryToken]
        [ServiceFilter(typeof(HasAdministratorRole))]
        public async Task<ActionResult> Create(CategoryForCreationDto category)
        {
            HttpContent content = BuildHttpContent(category);
            var response = await request.CargoCategoriesRequestHandler.CreateCategory(content);

            if (!response.IsSuccessStatusCode)
                return UnsuccesfullStatusCode(response);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("{id}/Delete")]
        [ServiceFilter(typeof(HasAdministratorRole))]
        public async Task<ActionResult> Delete(int id)
        {
            var response = await request.CargoRequestHandler.DeleteCargoById(id);

            if (!response.IsSuccessStatusCode)
                return UnsuccesfullStatusCode(response);

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("{id}/Edit")]
        [ServiceFilter(typeof(HasAdministratorRole))]
        public async Task<ActionResult> Edit(int id)
        {
            var response = await request.CargoCategoriesRequestHandler.GetAllCategories();

            if (!response.IsSuccessStatusCode)
                return UnsuccesfullStatusCode(response);

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
        [ServiceFilter(typeof(HasAdministratorRole))]
        public async Task<ActionResult> Edit(int id, CargoCategoryForUpdateDto category)
        {
            HttpContent content = BuildHttpContent(category);
            var response = await request.CargoCategoriesRequestHandler.PutCategoryById(id, content);

            if (!response.IsSuccessStatusCode)
                return UnsuccesfullStatusCode(response);

            return RedirectToAction(nameof(Index));
        }
    }
}