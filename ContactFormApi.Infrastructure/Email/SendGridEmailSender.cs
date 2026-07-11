using ContactFormApi.Application.DTOs.Email;
using ContactFormApi.Application.Interfaces.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ContactFormApi.Infrastructure.Email
{
    public sealed class SendGridEmailSender : IEmailSender
    {
        private readonly SendGridOptions _options;
        private readonly ILogger<SendGridEmailSender> _logger;

        public SendGridEmailSender(
            IOptions<SendGridOptions> options,
            ILogger<SendGridEmailSender> logger)
        {
            _options = options.Value;
            _logger = logger;
        }

        public async Task SendAsync(
            EmailMessageDto message,
            CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(_options.ApiKey))
            {
                throw new InvalidOperationException(
                    "SendGrid API key is not configured.");
            }

            if (string.IsNullOrWhiteSpace(_options.FromEmail))
            {
                throw new InvalidOperationException(
                    "SendGrid sender email is not configured.");
            }

            if (message.ToEmails.Count == 0)
            {
                throw new InvalidOperationException(
                    "Email message has no recipients.");
            }

            var client = new SendGridClient(_options.ApiKey);

            var from = new EmailAddress(_options.FromEmail, _options.FromName);

            var recipients = message.ToEmails
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .Select(email => new EmailAddress(email))
                .ToList();

            var plainTextBody = WebUtility.HtmlDecode(
                System.Text.RegularExpressions.Regex.Replace(
                    message.HtmlBody,
                    "<.*?>",
                    string.Empty));

            var sendGridMessage = MailHelper.CreateSingleEmailToMultipleRecipients(
                from,
                recipients,
                message.Subject,
                plainTextBody,
                message.HtmlBody,
                showAllRecipients: false);

            if (!string.IsNullOrWhiteSpace(message.ReplyToEmail))
            {
                sendGridMessage.ReplyTo = new EmailAddress(message.ReplyToEmail, message.ReplyToName);
            }

            _logger.LogInformation(
                "Sending email to {RecipientCount} recipient(s)",
                recipients.Count);

            var response = await client.SendEmailAsync(sendGridMessage, ct);

            var responseBody = await response.Body.ReadAsStringAsync(ct);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError(
                    "SendGrid returned {StatusCode}: {ResponseBody}",
                    response.StatusCode,
                    responseBody);

                throw new InvalidOperationException(
                    $"SendGrid returned {response.StatusCode}");
            }

            _logger.LogInformation(
                "SendGrid accepted email with status {StatusCode}",
                response.StatusCode);
        }
    }
}
