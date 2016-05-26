using System.Threading.Tasks;

namespace Gonzigonz.SampleApp.IdentityServicesInterfaces
{
	public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
