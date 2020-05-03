using System.Threading.Tasks;

namespace MFR.EmailSender
{
    public interface ISendMail 
    {
        Task SendMailAsync(string fromAddress, string toAddress, string subject, string message);
    }
}
