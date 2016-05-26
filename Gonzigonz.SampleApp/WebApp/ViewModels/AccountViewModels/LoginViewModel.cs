using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModels.AccountViewModels
{
	public class LoginViewModel
    {
        [Required]
		[Display(Name = "Username")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
