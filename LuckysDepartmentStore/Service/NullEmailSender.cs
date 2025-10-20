using Microsoft.AspNetCore.Identity.UI.Services;

namespace LuckysDepartmentStore.Service
{
    public class NullEmailSender : IEmailSender
    {
        private readonly ILogger<NullEmailSender> _logger;
        public NullEmailSender(ILogger<NullEmailSender> logger) => _logger = logger;

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // Logging the email instead of sending it
            _logger.LogInformation("NullEmailSender: To={Email}, Subject={Subject}", email, subject);
            _logger.LogDebug("Body: {Body}", htmlMessage);
            return Task.CompletedTask;
        }
    }
}
