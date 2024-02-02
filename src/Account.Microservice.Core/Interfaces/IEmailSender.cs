using System.Threading.Tasks;

namespace Account.Microservice.Core.Interfaces;

public interface IEmailSender
{
  Task SendEmailAsync(string fromEmail,
           string fromName,
           string toEmail,
           string username,
           string password,
           string host,
           int port,
           bool enableSsl,
           string subject,
           string body);
}
