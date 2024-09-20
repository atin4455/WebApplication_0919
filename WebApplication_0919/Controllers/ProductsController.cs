using Microsoft.AspNetCore.Mvc;
using WebApplication_0919.Models;

namespace WebApplication_0919.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IWebHostEnvironment _env;

        public ProductsController(IWebHostEnvironment env)
        {
            this._env = env;
        }

        public IActionResult Index()
        {
            return View();
        }
        //GET:Products
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ProductCreateVm vm,IFormFile file1)
        {
            if (ModelState.IsValid)
            {
                //取得要存放路徑
                string uploadFolder = Path.Combine(_env.WebRootPath, "uploads");
                //存檔,並取得存檔後的檔名
                string newFileName = HandleUploadFile(file1, uploadFolder);
                //若沒上傳檔案,會回傳null,視為錯誤
                if (string.IsNullOrEmpty(newFileName))
                {
                    ModelState.AddModelError("FileName", "請上傳檔案");
                }
                else
                {
                    vm.FileName = newFileName;
                }

                //建立一筆紀錄
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        private string HandleUploadFile(IFormFile file, string uploadFolder)
        {
            if (file == null || file.Length == 0) return null;

            //todo判斷是不是圖檔

            string fullName = Path.Combine(uploadFolder, file.FileName);

            using(var stream =new FileStream(fullName, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return file.FileName;
        }
    }
}
