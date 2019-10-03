using FinPortal.Enumerations;
using FinPortal.Models;
using FinPortal.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinPortal.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult Setup()
        {

            return View();
        }

        public ActionResult Lobby()
        {
            var lobbyist = db.Users.Where(u => u.HouseholdId == null).Select(user => new LobbyUser
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Avatar = user.Avatar,
                Email = user.Email
            });

            return View(lobbyist);
        }

        public ActionResult Dash()
        {
            var userId = User.Identity.GetUserId();
            var householdId = db.Users.Find(userId).HouseholdId;
            var house = db.Households.Find(householdId);

            var data = new DataVM
            {
                Household = house,
                Banks = db.Banks.ToList(),
                Budgets = db.Budgets.ToList(),
                BudgetItems = db.BudgetItems.ToList(),
                Transactions = db.Transactions.ToList(),
                Members = db.Users.ToList()
            };

            return View(data);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}