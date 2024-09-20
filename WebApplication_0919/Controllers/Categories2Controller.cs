using Microsoft.AspNetCore.Mvc;
using WebApplication_0919.Models.EFModels.Services;
using WebApplication_0919.Models.EFModels;
using WebApplication_0919.Models.EFModels.Dtos;
using WebApplication_0919.Models;

namespace WebApplication_0919.Controllers
{
    public class Categories2Controller : Controller
    {
        private readonly CategoryService _service;


        public Categories2Controller(CategoryService service)
        {
            this._service = service;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CategoryVm vm)
        {
            CategoryDto dto = new CategoryDto
            {
                Name = vm.Name,
                DisplayOrder = vm.DisplayOrder
            };
            _service.Create(dto);

            return View();
        }
    }
}
