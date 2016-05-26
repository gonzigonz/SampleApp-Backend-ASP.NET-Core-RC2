using System.Threading.Tasks;

namespace Gonzigonz.SampleApp.IdentityServicesInterfaces
{
	public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}
