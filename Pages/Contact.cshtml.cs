using Microsoft.AspNetCore.Mvc;
using MyPortfolio.Models;
using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Configuration;

namespace MyPortfolio.Pages
{
    public class ContactModel : BasePageModel
    {
        private readonly IConfiguration _config;

        public ContactModel(IConfiguration config)
        {
            _config = config;
        }

        public string SuccessMessage { get; set; } = string.Empty;
        public string ErrorMessage { get; set; } = string.Empty;

        public IActionResult OnGet()
        {
            return CheckLogin() ?? Page();
        }

        public IActionResult OnPost(string SenderName, string SenderEmail, string Subject, string Message)
        {
            var check = CheckLogin();
            if (check != null) return check;

            try
            {
                var smtpHost      = _config["EmailSettings:SmtpHost"] ?? "";
                var smtpPort      = int.Parse(_config["EmailSettings:SmtpPort"] ?? "587");
                var senderEmail   = _config["EmailSettings:SenderEmail"] ?? "";
                var senderPass    = _config["EmailSettings:SenderPassword"] ?? "";
                var receiverEmail = _config["EmailSettings:ReceiverEmail"] ?? "";

                // បង្កើត Email
                var email = new MimeMessage();
                email.From.Add(new MailboxAddress(SenderName, senderEmail));
                email.To.Add(new MailboxAddress("Cheng", receiverEmail));
                email.Subject = Subject ?? "(No Subject)";
                email.Body = new TextPart("html")
                {
                    Text = $@"
                        <h3>Message from Portfolio Contact Form</h3>
                        <p><strong>Name:</strong> {SenderName}</p>
                        <p><strong>Email:</strong> {SenderEmail}</p>
                        <p><strong>Subject:</strong> {Subject}</p>
                        <hr/>
                        <p><strong>Message:</strong></p>
                        <p>{Message}</p>
                    "
                };

                // Send Email
                using var smtp = new SmtpClient();
                smtp.Connect(smtpHost, smtpPort, MailKit.Security.SecureSocketOptions.StartTls);
                smtp.Authenticate(senderEmail, senderPass);
                smtp.Send(email);
                smtp.Disconnect(true);

                SuccessMessage = $"Thank you {SenderName}! Your message has been received. We'll get back to you soon.";
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Failed to send message. Error: {ex.Message}";
            }

            return Page();
        }
    }
}