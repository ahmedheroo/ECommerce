using BaytonicECommerce.Models;
using BaytonicECommerce.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaytonicECommerce.ViewModels;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace BaytonicECommerce.Controllers
{
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class AdminNewsLetterController : BaseController
    {
        private readonly INewsLetterRepository newsLetterRepository;
        private readonly ILogger<AdminNewsLetterController> _logger;

        public AdminNewsLetterController(INewsLetterRepository _newsLetterRepository, ILogger<AdminNewsLetterController> logger)
        {
            newsLetterRepository = _newsLetterRepository;
            _logger = logger;
        }
        public IActionResult Index()
        {
            IEnumerable<NewsLetter> newsLetters = newsLetterRepository.GetAll();
            return View(newsLetters);
        }
        public IActionResult DeleteMessagepermanently(long id)
        {
            newsLetterRepository.Delete(id);

            return RedirectToAction("Index");
        }
        public IActionResult ConfirmMail(long id)
        {
            NewsLetter newsLetter = newsLetterRepository.GetById(id);
            if (newsLetter != null)
            {
                
                    newsLetter.IsActive = true;
                    //  message.SeenByUserId = userManager.GetUserId(User);
                    newsLetterRepository.Update(newsLetter);
                    NotifyToastr("success", "Subscribed Successfully");            
            }
            return RedirectToAction("Index");
        }
    }
}
