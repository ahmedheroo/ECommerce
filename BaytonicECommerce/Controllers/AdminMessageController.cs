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
    public class AdminMessageController : BaseController
    {
        private readonly IMessageRepository messageRepository;
        private readonly ILogger<AdminMessageController> _logger;

        public AdminMessageController(IMessageRepository _messageRepository, ILogger<AdminMessageController> logger)
        {
            messageRepository = _messageRepository;
            _logger = logger;
        }
        public IActionResult Index()
        {
            IEnumerable<Message> allMessages = messageRepository.GetAll();
            return View(allMessages);
        }
        [HttpGet]
        public IActionResult IncomingMessages()
        {
            List<Message> NewMessages = messageRepository.GetIncomingMessages().ToList();

            return View(NewMessages);
        }
        [HttpGet]
        public IActionResult GetMessage(long id)
        {
            Message message = messageRepository.GetById(id);
            if (message != null)
            {
                if (message.Seen == false)
                {
                    message.Seen = true;
                    //  message.SeenByUserId = userManager.GetUserId(User);
                    messageRepository.Update(message);
                }
            }
            return View(message);
        }
     
        public IActionResult DeleteMessageToRecycle(long id)
        {
            messageRepository.DeleteToRecycle(id);

            return RedirectToAction("Index");

        }
        public IActionResult DeleteMessagepermanently(long id)
        {
            messageRepository.Delete(id);

            return RedirectToAction("Index");
        }
    }
}
