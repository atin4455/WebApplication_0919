using System.ComponentModel.DataAnnotations;

namespace WebApplication_0919.Models
{
    public class CategoryVm
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int DisplayOrder { get; set; }
    }
}
