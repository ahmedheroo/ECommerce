using BaytonicECommerce.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaytonicECommerce.Repository
{
  public interface IAccountManagementRepository
    {
        IEnumerable<AspNetUserRole> GetAllAdmins();
        IEnumerable<AspNetUserRole> GetAllUsers();
        AspNetUser GetUserById(object id);
        IEnumerable<AspNetRole> GetAllRoles();
        string GetRoleNameUsingRoleId(string roleId);
        string GetUserRoleId(string userId);

    }
}
