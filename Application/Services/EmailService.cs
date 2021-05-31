using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Application.Enums;
using Application.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Services.DataStructures;

namespace Application.Services
{
    internal class EmailService : IEmailService
    {
        private readonly IContext _context;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IContext context, ILogger<EmailService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<ServiceResult> SendForgottenPasswordAsync(string emailTo)
        {
            try
            {
                var user =await _context.Users.FirstOrDefaultAsync(x => x.Email == emailTo);
                if (user is null)
                    return new ServiceResult(ServiceResultType.NotFound)
                    {
                        Messages = new[]
                        {
                            "Usuário não encontrado."
                        }
                    };
                
                MailMessage email = new MailMessage();
                email.From = new MailAddress("suporteti.portonacional@gmail.com");
                email.To.Add(new MailAddress(emailTo));
                email.Subject = "Recuperação de Senha";


                email.Body = $"Saudações, aqui está a senha esquicida que foi lembrada pelo sistema: <b>{user.Password}</b> ." +
                    " Att. Equipe de Suporte Prefeitura Porto Nacional.";
                email.IsBodyHtml = true;
                email.Priority = MailPriority.High;

                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                smtpClient.EnableSsl = true;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential("suporteti.portonacional@gmail.com", "suporte@2021");

                smtpClient.Send(email);

                return new ServiceResult(ServiceResultType.Success);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return new ServiceResult(ServiceResultType.InternalError)
                {
                    Messages = new[]
                    {
                        "Erro ao mandar Email."
                    }
                };
            }
        }
    }
}