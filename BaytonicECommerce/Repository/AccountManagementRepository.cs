using BaytonicECommerce.Data;
using BaytonicECommerce.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaytonicECommerce.Repository
{
    public class AccountManagementRepository : IAccountManagementRepository
    {
        private readonly EcommerceContext context;
        public AccountManagementRepository(EcommerceContext _context)
        {
            context = _context;
        }

        public IEnumerable<AspNetUserRole> GetAllAdmins()
        {
            //return context.AspNetUsers.Include(x => x.AspNetUserRoles.Where(x=>x.Role.Name=="Admin")).ThenInclude(x => x.Role).ToList();
            return context.AspNetUserRoles.Include(x => x.Role).Where(x => x.Role.Name == "Admin").Include(x => x.User).ToList();
        }
        public IEnumerable<AspNetUserRole> GetAllUsers()
        {
            //  return context.AspNetUsers.Include(x => x.AspNetUserRoles.Where(x => x.Role.Name == "User")).ThenInclude(x=>x.Role).ToList();
            return context.AspNetUserRoles.Include(x => x.Role).Where(x => x.Role.Name == "User").Include(x => x.User).ToList();

        }

        public AspNetUser GetUserById(object id)
        {
            return context.AspNetUsers.Find(id);
        }
        public IEnumerable<AspNetRole> GetAllRoles()
        {
            return context.AspNetRoles;
        }
        public string GetRoleNameUsingRoleId(string roleId)
        {
            return context.AspNetRoles.Where(x=>x.Id==roleId).FirstOrDefault().Name;
        }
        public string GetUserRoleId(string userId)
        {
            return context.AspNetUserRoles.Where(x => x.UserId == userId).FirstOrDefault().RoleId;
        }
    }
}
