using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModels.AccountViewModels
{
	public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
