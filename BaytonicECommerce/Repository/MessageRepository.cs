using BaytonicECommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaytonicECommerce.Repository
{
    public class MessageRepository : IMessageRepository
    {
        private readonly EcommerceContext context;
        public MessageRepository(EcommerceContext _context)
        {
            context = _context;
        }
        public IEnumerable<Message> GetAll()
        {
            return context.Messages.ToList();
        }
       public IEnumerable<Message> GetIncomingMessages()
        {
            return context.Messages.Where(x => x.Seen == false).ToList();
        }
        public Message GetById(Object MessageID)
        {
            return context.Messages.Find(MessageID);
        }
        public void Insert(Message Message)
        {
            context.Messages.Add(Message);
            context.SaveChanges();

        }
        public void Update(Message Message)
        {
            context.Entry(Message).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();

        }
        public void Delete(object MessageID)
        {
            Message Message = context.Messages.Find(MessageID);
            context.Messages.Remove(Message);
            context.SaveChanges();

        }
        public void DeleteToRecycle(object MessageID)
        {
            Message Message = context.Messages.Find(MessageID);
            Message.IsDeleted = true;
            Update (Message);

        }

    }
}
