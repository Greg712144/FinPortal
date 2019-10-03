using FinPortal.Enumerations;
using FinPortal.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinPortal.Helpers
{
    public class SeedHelper
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private UserManager<ApplicationUser> userManager = new
         UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new
         ApplicationDbContext()));


        public bool IsUserInHouse(string userId, int projectId)
        {
            var house =  db.Households.Find(projectId);
            var flag = house.Members.Any(m => m.Id == userId);

            return(flag);
        }
        public void AddUserToHouse(string userId, int projectId)
        {
            if(!IsUserInHouse(userId, projectId))
            {
                Household hous = db.Households.Find(projectId);
                var newMember = db.Users.Find(userId);

                hous.Members.Add(newMember);
                db.SaveChanges();
            }          
        }

        public bool IsUserInRole(string userId, string roleName)
        {
            return userManager.IsInRole(userId, roleName);
        }
        public bool AddUserToRole(string userId, RoleName roleName)
        {
            var result = userManager.AddToRole(userId, roleName.ToString());
            return result.Succeeded;
        }
    }
}