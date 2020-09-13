using Derin.Business.BusinessLogic.Locator;
using Derin.Business.ViewModel.Administration;
using Derin.Web.Attributes;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace Derin.Web.Areas.Main.Controllers
{
    [Area("Main")]
    public class ContactUsController : BaseController
    {
        private IMemoryCache _memoryCache;
        private AdministrationBLLocator _adminlocator;
        private IHostingEnvironment _env;
        public ContactUsController(AdministrationBLLocator adminLocator, IMemoryCache memoryCache, IHostingEnvironment env) : base(env)
        {
            _env = env;
            _memoryCache = memoryCache;
            _adminlocator = adminLocator;
        }
        [ContactUsAttribute]
        public IActionResult Index()
        {
            ViewBag.ContactUs = JsonConvert.DeserializeObject<List<ContactUsVM>>(HttpContext.Session.GetString("ContactUsData"));
            return View();
        }

        private void SendMail()
        {
            SmtpClient client = new SmtpClient();
            client.Port = 587;
            client.Host = "smtp.live.com";
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential("canataselim@hotmail.com", "2032002941Ca!");

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("canataselim@hotmail.com");
            mailMessage.To.Add("canataselim@hotmail.com");
            mailMessage.Body = MailCreateBody("Can Ataselim", "canataselim@hotmail.com", "05425412999", "Bugün Günlerden Salı");
            mailMessage.IsBodyHtml = true;
            mailMessage.Subject = "subject";
            client.Send(mailMessage);
        }

        private string MailCreateBody(string FullName, string Email, string Phone, string Message)
        {
            string pathToFiles = Path.Combine(_env.WebRootPath, "MailTemplate\\UserMessage.html");

            string body = string.Empty;
            using (StreamReader reader = new StreamReader(pathToFiles))
            {
                body = reader.ReadToEnd();
            }

            body = body.Replace("{FullName}", FullName);
            body = body.Replace("{Email}", Email);
            body = body.Replace("{Phone}", Phone);
            body = body.Replace("{Message}", Message);


            return body;
        }
    }
}