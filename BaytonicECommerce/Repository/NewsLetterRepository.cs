using BaytonicECommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaytonicECommerce.Repository
{
    public class NewsLetterRepository : INewsLetterRepository
    {
        private readonly EcommerceContext context;
        public NewsLetterRepository(EcommerceContext _context)
        {
            context = _context;
        }
        public IEnumerable<NewsLetter> GetAll()
        {
            return context.NewsLetters.ToList();
        }
        public NewsLetter GetById(Object NewsLetterID)
        {
            return context.NewsLetters.Find(NewsLetterID);
        }
        public NewsLetter GetByEmail(string email)
        {
            return context.NewsLetters.Where(x=>x.ClientMail==email).FirstOrDefault();
        }
        public void Insert(NewsLetter newsLetter)
        {
            context.NewsLetters.Add(newsLetter);
            context.SaveChanges();
        }
        public void Update(NewsLetter newsLetter)
        {
            context.Entry(newsLetter).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();

        }
        public void Delete(object NewsLetterID)
        {
            NewsLetter newsLetter = context.NewsLetters.Find(NewsLetterID);
            context.NewsLetters.Remove(newsLetter);
            context.SaveChanges();

        }


    }
}
