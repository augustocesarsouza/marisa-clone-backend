using brevo_csharp.Client;
using brevo_csharp.Model;
using Marisa.Domain.Entities;
using Marisa.Domain.InfoErrors;
using Marisa.Infra.Data.UtilityExternal.Interface;
using Microsoft.Extensions.Configuration;

namespace Marisa.Infra.Data.UtilityExternal
{
    public class SendEmailBrevo : ISendEmailBrevo
    {
        private readonly ITransactionalEmailApiUti _transactionalEmailApiUti;
        private readonly IConfiguration _configuration;

        public SendEmailBrevo(ITransactionalEmailApiUti transactionalEmailApiUti, IConfiguration configuration)
        {
            _transactionalEmailApiUti = transactionalEmailApiUti;
            _configuration = configuration;
        }

        public InfoErrors SendEmail(User user, string url)
        {
            try
            {
                //var keyApi = _configuration["Brevo:KeyApi"];
                var keyApi = Environment.GetEnvironmentVariable("BREVO_KEYAPI") ?? _configuration["Brevo:KeyApi"];

                if (!Configuration.Default.ApiKey.ContainsKey("api-key"))
                    Configuration.Default.ApiKey["api-key"] = keyApi;

                string SenderName = "augusto";
                string SenderEmail = "augustocesarsantana90@gmail.com";
                SendSmtpEmailSender emailSender = new SendSmtpEmailSender(SenderName, SenderEmail);

                if (string.IsNullOrEmpty(user.Name) || string.IsNullOrEmpty(user.Email))
                    return InfoErrors.Fail("Erro name ou email invalido");

                string ToName = user.Name;
                string ToEmail = user.Email;
                SendSmtpEmailTo emailReciver1 = new SendSmtpEmailTo(ToEmail, ToName);
                var To = new List<SendSmtpEmailTo>();
                To.Add(emailReciver1);

                string TextContent = "Clique no token disponivel: " + url;
                string Subject = "Seu token de confirmação";

                var sendSmtpEmail = new SendSmtpEmail(emailSender, To, null, null, null, TextContent, Subject);

                var result = _transactionalEmailApiUti.SendTransacEmailWrapper(sendSmtpEmail);

                if (!result.IsSucess)
                    return InfoErrors.Fail(result.Message ?? "erro ao enviar email");

                return InfoErrors.Ok("Tudo certo com o envio do email");
            }
            catch (Exception ex)
            {
                return InfoErrors.Fail($"Erro no envio do email, ERROR: ${ex.Message}");
            }
        }

        public InfoErrors SendCode(User user, int codeRandon)
        {
            try
            {
                var keyApi = Environment.GetEnvironmentVariable("BREVO_KEYAPI") ?? _configuration["Brevo:KeyApi"];

                if (!Configuration.Default.ApiKey.ContainsKey("api-key"))
                    Configuration.Default.ApiKey["api-key"] = keyApi;

                string SenderName = "lucas";
                string SenderEmail = "lucasdaniel545@gmail.com";
                SendSmtpEmailSender emailSender = new SendSmtpEmailSender(SenderName, SenderEmail);

                //if (string.IsNullOrEmpty(user.Name) || string.IsNullOrEmpty(user.Email))
                //    return InfoErrors.Fail("Erro name ou email invalido");

                if (string.IsNullOrEmpty(user.Email))
                    return InfoErrors.Fail("error name or email is null");

                string ToName = user.Name ?? "anonimato";
                string ToEmail = user.Email ?? "";
                SendSmtpEmailTo emailReciver1 = new SendSmtpEmailTo(ToEmail, ToName);
                var To = new List<SendSmtpEmailTo>();
                To.Add(emailReciver1);

                string TextContent = "Seu numero de Confirmação: " + codeRandon.ToString();
                string Subject = "SEU NUMERO ALEATORIO DE CONFIRMAÇÃO";

                var sendSmtpEmail = new SendSmtpEmail(emailSender, To, null, null, null, TextContent, Subject);

                var result = _transactionalEmailApiUti.SendTransacEmailWrapper(sendSmtpEmail);

                if (!result.IsSucess)
                    return InfoErrors.Fail(result.Message ?? "erro ao enviar email");

                return InfoErrors.Ok("Tudo certo com o envio do email");
            }
            catch (Exception ex)
            {
                return InfoErrors.Fail($"Erro no envio do email, ERROR: ${ex.Message}");
            }
        }
    }
}
