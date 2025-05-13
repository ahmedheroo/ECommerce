using BaytonicECommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaytonicECommerce.Repository
{
  public interface IMessageRepository
    {
        IEnumerable<Message> GetAll();
        IEnumerable<Message> GetIncomingMessages();
        Message GetById(object id);
        void Insert(Message obj);
        void Update(Message obj);
        void Delete(object id);
        void DeleteToRecycle(object MessageID);
    }
}
