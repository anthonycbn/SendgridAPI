using SendGrid;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SendgridEmaiWebAPI.Services
{
    public interface IEmailSender
    {
        Task<Response> SendEmailAsync(List<string> to, List<string> cc, string subject, string message);
    }
}
