using BaytonicECommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaytonicECommerce.Repository
{
  public interface IGovernmentShippingRepository
    {
        IEnumerable<GovernmentShipping> GetAll();
        GovernmentShipping GetById(object id);
        void Insert(GovernmentShipping obj);
        void Update(GovernmentShipping obj);
        void Delete(object id);
    }
}
