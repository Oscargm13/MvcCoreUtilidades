using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Mvc;

namespace MvcCoreUtilidades.Controllers
{
    public class MailsController : Controller
    {
        //necesitamos el  fichero de configuracion
        private IConfiguration configuration;
        public MailsController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SendMail()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SendMail(string to, string asunto, string mensaje)
        {
            MailMessage mail = new MailMessage();
            //DEBEMOS INDICAR EL FROM, ES DECIR, DE QUE CUENTA VIENE EL CORREO
            string user = this.configuration.GetValue<string>("MailSettings:Credentials:User");
            mail.From = new MailAddress(user);
            //LOS MENSAJES SON UNA COLECCION
            mail.To.Add(to);
            mail.Subject = asunto;
            mail.Body = mensaje;
            mail.IsBodyHtml = true;
            string password = this.configuration.GetValue<string>("MailSettings:Credential:Password");
            string host = this.configuration.GetValue<string>("MailSettings:Server:Host");
            int port = this.configuration.GetValue<int>("MailSettings:Server:Port");
            bool ssl = this.configuration.GetValue<bool>("MailSettings:Server:Ssl");
            bool defaulsCredentials = this.configuration.GetValue<bool>("MailSettings:Server:defaulsCredentials");
            //CREAMOS LA CLSE SERVIDOR SMTP
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = host;
            smtpClient.Port = port;
            smtpClient.EnableSsl = ssl;
            smtpClient.UseDefaultCredentials = defaulsCredentials;
            //CREAMOS LAS CREDENCIALES
            NetworkCredential credentials = new NetworkCredential(user, password);
            smtpClient.Credentials = credentials;
            await smtpClient.SendMailAsync(mail);
            ViewData["MENSAJE"] = "Mail enviado correctamente";
            return View();
        }
    }
}
