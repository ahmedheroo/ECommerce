using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using BaytonicECommerce.Models;
using BaytonicECommerce.ViewModels;

namespace BaytonicECommerce.Controllers
{
    public class BaseController : Controller
    {
        public MainLayoutVM MainLayoutVM { get; set; }
        public BaseController()
        {
        }
        public void NotifyAlert(string message, string title,NotificationType notificationType=NotificationType.success)
        {
            var msg = new { 
                message = message, 
                title = title, 
                icon = notificationType.ToString(), 
                type = notificationType.ToString(),
                provider = GetProviderAlert()
            };

            TempData["Message"] = JsonConvert.SerializeObject(msg);
        }

        public void NotifyToastr(string message, string title,
                                    NotificationType notificationType = NotificationType.success)
        {
            var msg = new
            {
                message = message,
                title = title,
                icon = notificationType.ToString(),
                type = notificationType.ToString(),
                provider = GetProviderToastr()
            };

            TempData["Message"] = JsonConvert.SerializeObject(msg);
        }
        private string GetProviderAlert()
        {
            var builder = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                            .AddEnvironmentVariables();

            IConfigurationRoot configuration = builder.Build();

            var value = configuration["NotificationProviderAlert"];

            return value;
        }
        private string GetProviderToastr()
        {
            var builder = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                            .AddEnvironmentVariables();

            IConfigurationRoot configuration = builder.Build();

            var value = configuration["NotificationProviderToastr"];

            return value;
        }
    }
}