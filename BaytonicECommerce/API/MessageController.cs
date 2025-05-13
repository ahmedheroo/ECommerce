
using BaytonicECommerce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaytonicECommerce.Repository;
namespace BaytonicECommerce.API
{

    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageRepository messageRepository;

        public MessageController(IMessageRepository _messageRepository)
        {
            messageRepository = _messageRepository;
        }

        [HttpPost]
        [Route("api/[controller]/SendMessageFromClientToAdmin")]
        public IActionResult SendMessage([FromBody] Message newMessage)
        {

            newMessage.Date = DateTime.Now;
            try
            {
                messageRepository.Insert(newMessage);
                return Ok(newMessage);
            }
            catch
            {
                return BadRequest();
            }

        }

    }
}
