using WebApplication_0919.Models.EFModels.Dtos;

namespace WebApplication_0919.Models.EFModels.Services
{
    public class CategoryService 
    {
        private readonly ICategoryRepository _repo;

        public CategoryService(ICategoryRepository repo)
        {
        this._repo = repo;
        }

        public void Create(CategoryDto dto)
        {
            //todo在此加入商業邏輯

            //呼叫資料存取層
            _repo.Create(dto);
        }

    }

    public interface ICategoryRepository
    {
        void Create(CategoryDto dto);
    }

    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _db;

        public CategoryRepository(AppDbContext db)
        {
            this._db = db;
        }

        public void Create(CategoryDto dto)
        {
            var category = new Category
            {
                Name = dto.Name,
                DisplayOrder = dto.DisplayOrder
            };

            _db.Categories.Add(category);
            _db.SaveChanges();
        }
    }
}
