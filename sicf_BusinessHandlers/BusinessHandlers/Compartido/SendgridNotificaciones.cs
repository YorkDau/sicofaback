using Azure.Core;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;

using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;


namespace sicf_BusinessHandlers.BusinessHandlers.Compartido
{
    public class SendgridNotificaciones : ISendgridNotificaciones
    {
        private readonly IConfiguration Configuration;

        public SendgridNotificaciones(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public async Task<bool> enviarNotificacionCorreoElectronico()
        {
            bool response = true;
            await sendgridSendMail();

            return response;
        }

        public async Task<bool> EnviarCambioContrasena(string email, string temporalPass)
        {
            bool response = true;
            await RestaurarContrasena(email, temporalPass);

            return response;
        }

        public async Task<bool> EnviarContrasena(string email, string pass) {

            bool response = true;
            await EntregarContrasena(email, pass);

            return response;
        }





        private async Task sendgridSendMail()
        {
            try
            {
                var apiKey = Configuration["Sendgrid:apiKey"];
                var client = new SendGridClient(apiKey);

                var from = new EmailAddress(Configuration["Sendgrid:senderMail"], "Remitente Comisaria");
                var subject = "Sending with SendGrid is Fun";
                var to = new EmailAddress("isapzmz@gmail.com", "Usuario Comisaria");
                var plainTextContent = "and easy to do anywhere, even with C#";
                var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

                var response = await client.SendEmailAsync(msg);
            }
            catch (Exception ex)
            {

                throw ex;
            }
           
        }

        private async Task RestaurarContrasena(string correo, string temporalPass)
        {
            var email = new MimeMessage();
            try
            {
                email.From.Add(MailboxAddress.Parse(Configuration.GetSection("Email:UserName").Value));
                email.To.Add(MailboxAddress.Parse(correo));
                email.Body = new TextPart(TextFormat.Html) { Text = "tu contraseña Sicofa es: " + temporalPass };

                var smtp = new SmtpClient();

                var host = Configuration.GetSection("Email:Host").Value;
                var port = Convert.ToInt32(Configuration.GetSection("Email:Port").Value);
                var user = Configuration.GetSection("Email:UserName").Value;
                var pass = Configuration.GetSection("Email:PassWord").Value;

                smtp.Connect(host, port, SecureSocketOptions.StartTls);

                smtp.Authenticate(user, pass);
                smtp.Send(email);
                smtp.Disconnect(true);



            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        private async Task EntregarContrasena(string correo, string passs)
        {
            var email = new MimeMessage();
            try
            {
                email.From.Add(MailboxAddress.Parse(Configuration.GetSection("Email:UserName").Value));
                email.To.Add(MailboxAddress.Parse(correo));
                email.Body = new TextPart(TextFormat.Html) {Text = "tu contraseña Sicofa es: " + passs + " por favor cambiarla "};

                var smtp = new SmtpClient();

                var host = Configuration.GetSection("Email:Host").Value;
                var port = Convert.ToInt32(Configuration.GetSection("Email:Port").Value);
                var user = Configuration.GetSection("Email:UserName").Value;
                var pass = Configuration.GetSection("Email:PassWord").Value;

                smtp.Connect(host, port, SecureSocketOptions.StartTls);

                smtp.Authenticate(user, pass);
                smtp.Send(email);
                smtp.Disconnect(true);


               
            }
            catch (Exception ex)
            {

                throw ex;
            }

          
        }
    }
}
