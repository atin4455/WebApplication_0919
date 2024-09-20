using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication_0919.Models;
using WebApplication_0919.Models.EFModels;

namespace WebApplication_0919.Controllers.Apis
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesApiController : ControllerBase
    {
        private readonly AppDbContext _db;

        public CategoriesApiController(AppDbContext db)
        {
            this._db = db;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var data = _db.Categories
                .OrderBy(c => c.DisplayOrder)
                .Select(c => new { c.Id, c.Name })
                .ToList();

            return Ok(data);
        }

        [HttpGet("{id}")]
        public IActionResult Get([FromRoute] int id)
        {
            var data = _db.Categories
                .Where(c => c.Id == id)
                .FirstOrDefault();

            if (data == null)
            {
                return NotFound();
            }

            return Ok(data);
        }
        ///// <summary>
        ///// 錯的寫法,GET/api/Categories?id=2
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //[HttpGet("{id}")]
        //public IActionResult Get([FromQuery] int id)
        //{
        //    var data = _db.Categories
        //        .Where(c => c.Id == id)
        //        .FirstOrDefault();

        //    if (data == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(data);
        //}



        /// <summary>
        /// 另一種寫法,也是對的,但比較不推薦
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// GET/api/Categories/Load?id=1
        [HttpGet("Load")]
        public IActionResult Get2([FromQuery] int id)
        {
            var data = _db.Categories
                .Where(c => c.Id == id)
                .FirstOrDefault();

            if (data == null)
            {
                return NotFound();
            }

            return Ok(data);
        }


        //POST /api/CategoriesApi
        [HttpPost]
        public IActionResult Create([FromBody] CategoryVm vm)
        {
            if (ModelState.IsValid)
            {
                var category = new Category
                {
                    Name = vm.Name,
                    DisplayOrder = vm.DisplayOrder
                };

                _db.Categories.Add(category);
                _db.SaveChanges();

                return Ok(category);
            }
            return BadRequest(ModelState);
        }

        // PUT /api/CategoriesApi/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] CategoryVm vm)
        {
            var categoryInDb = _db.Categories.FirstOrDefault(c => c.Id == id);
            if (categoryInDb == null) return NotFound();

            if (ModelState.IsValid)
            {
                categoryInDb.Name = vm.Name;
                categoryInDb.DisplayOrder = vm.DisplayOrder;

                _db.SaveChanges();

                return Ok(categoryInDb);
            }

            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var categoryInDb = _db.Categories.FirstOrDefault(c => c.Id == id);
            if (categoryInDb == null) return NotFound();

            try
            {
                _db.Categories.Remove(categoryInDb);
                _db.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new
                    {
                        Message = "發生錯誤",
                        Details = ex.Message
                    });
            }
        }

    }
}
