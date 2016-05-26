using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModels.AccountViewModels
{
	public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
