using Bridge.Core.Resources;
using Bridge.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bridge.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryResource>>> GetAll()
        {
            var categories = await _categoryService.GetAll();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryResource?>> GetById(Guid id)
        {
            var category = await _categoryService.GetById(id);
            if (category == null) return NotFound();
            return Ok(category);
        }

        [HttpGet("{id}/requests")]
        public async Task<ActionResult<IEnumerable<RequestResource>>> GetRequestsByCategoryId(Guid id)
        {
            var requests = await _categoryService.GetRequestsByCategoryId(id);
            return Ok(requests);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Add(CategoryResource entity)
        {
            return Ok(await _categoryService.Add(entity));
        }

        [HttpPut]
        public async Task<ActionResult<int>> Update(CategoryResource entity)
        {
            return Ok(await _categoryService.Update(entity));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> DeleteById(Guid id)
        {
            return Ok(await _categoryService.DeleteById(id));
        }
    }
}
