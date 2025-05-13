using BaytonicECommerce.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaytonicECommerce.Repository
{
    public class GovernmentShippingRepository : IGovernmentShippingRepository
    {
        private readonly EcommerceContext context;
        public GovernmentShippingRepository(EcommerceContext _context)
        {
            context = _context;
        }
        public IEnumerable<GovernmentShipping> GetAll()
        {
            return context.GovernmentShippings;
        }
        public GovernmentShipping GetById(Object govID)
        {
            return context.GovernmentShippings.Find(govID);
        }
        public void Insert(GovernmentShipping gov)
        {
            context.GovernmentShippings.Add(gov);
            context.SaveChanges();
        }
        public void Update(GovernmentShipping gov)
        {
            context.Entry(gov).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();

        }
        public void Delete(object govID)
        {
            GovernmentShipping gov = context.GovernmentShippings.Find(govID);
            context.GovernmentShippings.Remove(gov);
            context.SaveChanges();

        }
    }
}
