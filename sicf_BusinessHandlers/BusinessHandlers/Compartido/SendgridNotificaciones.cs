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
                // 1. Obtención simplificada de la configuración
                var host = Configuration["Email:Host"];
                var port = int.Parse(Configuration["Email:Port"]);
                var user = Configuration["Email:UserName"];
                var pass = Configuration["Email:PassWord"];

                // 2. Construcción del encabezado del correo
                email.From.Add(new MailboxAddress("SIGFA", user));
                email.To.Add(MailboxAddress.Parse(correo));
                email.Subject = "Restablecimiento de Contraseña SIGFA"; // Asunto específico

                // 3. Creación del cuerpo del correo con formato HTML (Mensaje de RESTAURACIÓN)
                email.Body = new TextPart(TextFormat.Html)
                {
                    Text = $@"
                <!DOCTYPE html>
                <html lang='es'>
                <head>
                    <meta charset='UTF-8' />
                    <meta name='viewport' content='width=device-width, initial-scale=1.0' />
                </head>
                <body style='margin: 0; padding: 0; background-color: #f3f4f6; font-family: Arial, sans-serif;'>

                    <table width='100%' cellpadding='0' cellspacing='0' style='background-color:#f3f4f6; padding: 20px 0;'>
                        <tr>
                            <td align='center'>

                                <table width='600' cellpadding='0' cellspacing='0' style='background-color:#ffffff; border-radius:10px; overflow:hidden; box-shadow:0 4px 15px rgba(0,0,0,0.08);'>

                                    <tr>
                                        <td style='background-color:#005bbd; padding:20px 30px; text-align:center;'>
                                            <h1 style='color:#ffffff; margin:0; font-size:20px; font-weight:600;'>SIGFA - Restablecimiento de Clave</h1>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td style='padding:30px 40px; color:#333333; font-size:15px; line-height:1.6;'>

                                            <p>Estimado usuario,</p>

                                            <p>Hemos generado una nueva contraseña para acceder al sistema <strong>SIGFA</strong> debido a su solicitud de restablecimiento. A continuación encontrará su nueva clave de acceso:</p>

                                            <div style='margin:25px 0; text-align:center;'>
                                                <span style='font-size:22px; font-weight:bold; color:#d32f2f; background:#fbeaea; padding:12px 25px; border-radius:8px; display:inline-block;'>
                                                    {temporalPass}
                                                </span>
                                            </div>

                                            <p>Por motivos de seguridad, le recomendamos **cambiar esta contraseña inmediatamente** una vez inicie sesión.</p>
                                    
                                            <p>Si usted no solicitó este cambio, por favor contacte inmediatamente con el equipo de soporte para revisar su cuenta.</p>

                                            <br />

                                            <p>Saludos cordiales,<br><strong>Equipo SIGFA</strong></p>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td style='background-color:#f3f4f6; text-align:center; padding:15px; font-size:12px; color:#777;'>
                                            © SIGFA - Sistema de Información. Todos los derechos reservados.
                                        </td>
                                    </tr>

                                </table>
                            </td>
                        </tr>
                    </table>

                </body>
                </html>"
                };

                // 4. Conexión y envío asíncrono con SmtpClient y bloque 'using'
                using (var smtp = new SmtpClient())
                {
                    SecureSocketOptions secureOption = port == 465 ? SecureSocketOptions.SslOnConnect : SecureSocketOptions.StartTls;

                    await smtp.ConnectAsync(host, port, secureOption);

                    await smtp.AuthenticateAsync(user, pass);
                    await smtp.SendAsync(email);
                    await smtp.DisconnectAsync(true);
                }
            }
            catch
            {
                throw; // Preserva el stack trace original
            }
        }

        private async Task EntregarContrasena(string correo, string passs)
        {
            var email = new MimeMessage();

            try
            {
                var host = Configuration["Email:Host"];
                var port = int.Parse(Configuration["Email:Port"]);
                var user = Configuration["Email:UserName"];
                var pass = Configuration["Email:PassWord"];

                email.From.Add(new MailboxAddress("SIGFA", user));
                email.To.Add(MailboxAddress.Parse(correo));
                email.Subject = "Nueva Contraseña SIGFA";

                email.Body = new TextPart(TextFormat.Html)
                {
                    Text = $@"
                <!DOCTYPE html>
                <html lang='es'>
                <head>
                    <meta charset='UTF-8' />
                    <meta name='viewport' content='width=device-width, initial-scale=1.0' />
                </head>
                <body style='margin: 0; padding: 0; background-color: #f3f4f6; font-family: Arial, sans-serif;'>

                    <table width='100%' cellpadding='0' cellspacing='0' style='background-color:#f3f4f6; padding: 20px 0;'>
                        <tr>
                            <td align='center'>

                                <table width='600' cellpadding='0' cellspacing='0' style='background-color:#ffffff; border-radius:10px; overflow:hidden; box-shadow:0 4px 15px rgba(0,0,0,0.08);'>

                                    <tr>
                                        <td style='background-color:#005bbd; padding:20px 30px; text-align:center;'>
                                            <h1 style='color:#ffffff; margin:0; font-size:20px; font-weight:600;'>SIGFA - Sistema de Información</h1>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td style='padding:30px 40px; color:#333333; font-size:15px; line-height:1.6;'>

                                            <p>Estimado usuario,</p>

                                            <p>Se ha generado una nueva contraseña para acceder al sistema <strong>SIGFA</strong>. A continuación encontrarás tu nueva clave de acceso:</p>

                                            <div style='margin:25px 0; text-align:center;'>
                                                <span style='font-size:22px; font-weight:bold; color:#d32f2f; background:#fbeaea; padding:12px 25px; border-radius:8px; display:inline-block;'>
                                                    {passs}
                                                </span>
                                            </div>

                                            <p>Por motivos de seguridad, te recomendamos cambiar esta contraseña una vez inicies sesión.</p>

                                            <p>Si no solicitaste este cambio, por favor contacta inmediatamente con el equipo de soporte para revisar tu cuenta.</p>

                                            <br />

                                            <p>Saludos cordiales,<br><strong>Equipo SIGFA</strong></p>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td style='background-color:#f3f4f6; text-align:center; padding:15px; font-size:12px; color:#777;'>
                                            © SIGFA - Sistema de Información. Todos los derechos reservados.
                                        </td>
                                    </tr>

                                </table>
                            </td>
                        </tr>
                    </table>

                </body>
                </html>"
                };

                using (var smtp = new SmtpClient())
                {
                    SecureSocketOptions secureOption = port == 465 ? SecureSocketOptions.SslOnConnect : SecureSocketOptions.StartTls;

                    await smtp.ConnectAsync(host, port, secureOption);

                    await smtp.AuthenticateAsync(user, pass);
                    await smtp.SendAsync(email);
                    await smtp.DisconnectAsync(true);
                }
            }
            catch
            {
                throw; // preserva stack original
            }
        }


    }
}
