using System.ComponentModel.DataAnnotations;

namespace WebApplication_0919.Models.EFModels.ViewModels
{
    public class LoginVm
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "請輸入帳號")]
        public string Account { get; set; }

        [Required(ErrorMessage = "請輸入密碼")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
