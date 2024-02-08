using Microsoft.AspNetCore.Identity.UI.Services;

namespace ConselvaBudget.Services
{
    public class EmailSenderService : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // Doing a no-op implementation initially.
            return Task.CompletedTask;
        }
    }
}
