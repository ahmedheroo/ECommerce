using BaytonicECommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaytonicECommerce.Repository
{
  public interface INewsLetterRepository
    {
        IEnumerable<NewsLetter> GetAll();
        NewsLetter GetById(object id);
        void Insert(NewsLetter obj);
        void Update(NewsLetter obj);
        NewsLetter GetByEmail(string email);
        void Delete(object id);
    }
}
