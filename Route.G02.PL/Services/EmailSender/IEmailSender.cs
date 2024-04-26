using System.Globalization;
using System.Threading.Tasks;

namespace Route.G02.PL.Services.EmailSender
{
	public interface IEmailSender
	{
		Task SendAsync(string from, string recipients, string subject, string body);
	}
}
