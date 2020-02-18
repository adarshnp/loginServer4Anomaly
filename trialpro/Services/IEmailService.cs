using System.Threading.Tasks;

namespace trialpro.Services
{
    public interface IEmailService
    {
        Task Send(EmailMessage emailMessage);
    }
}