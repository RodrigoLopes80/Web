
using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Fornecedores.Services


    {
        public class AuthMessageSender : IEmailSender
        {
            public AuthMessageSender(IOptions<EmailSettings> emailSettings)
            {
                _emailSettings = emailSettings.Value;
            }

            public EmailSettings _emailSettings { get; }

            public Task SendEmailAsync(string email, string subject, string message)
            {
                try
                {
                    Execute(email, subject, message).Wait();
                    return Task.FromResult(0);
                }
                catch (Exception)
                {
                    throw;
                }
            }

            public async Task Execute(string email, string subject, string message)
            {
                try
                {
                    string toEmail = string.IsNullOrEmpty(email) ? _emailSettings.ToEmail : email;

                    MailMessage mail = new MailMessage()
                    {
                        From = new MailAddress(_emailSettings.UsernameEmail, "Rodrigo Pires Lopes")
                    };

                    mail.To.Add(new MailAddress(toEmail));         

                    mail.Subject = "Rodrigo - " + subject;
                    mail.Body = message;
                    mail.IsBodyHtml = true;
                    mail.Priority = MailPriority.High;
                 

                    using (SmtpClient smtp = new SmtpClient(_emailSettings.PrimaryDomain, _emailSettings.PrimaryPort))
                    {
                        smtp.Credentials = new NetworkCredential(_emailSettings.UsernameEmail, _emailSettings.UsernamePassword);
                        smtp.EnableSsl = true;
                        await smtp.SendMailAsync(mail);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }

