using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using FinPortal.Enumerations;
using FinPortal.Models;
using FinPortal.Helpers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
{
    private SeedHelper sHelp = new SeedHelper();
    public Configuration() 
    {
        AutomaticMigrationsEnabled = true;
    }

    protected override void Seed(ApplicationDbContext context)
    {

        #region Household
        context.Households.AddOrUpdate(
            h => h.Name,

            new Household { Name = "Seeded House", Greeting = "Welcome to the Seeded house!"}

            );
        #endregion

        #region Roles
        var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

        if (!context.Roles.Any(r => r.Name == "Admin"))
        {
            roleManager.Create(new IdentityRole { Name = "Admin" });
        }
        if(!context.Roles.Any(r => r.Name == "HOH"))
        {
            roleManager.Create(new IdentityRole { Name = "HOH" });
        }
        if(!context.Roles.Any(r => r.Name == "Member"))
        {
            roleManager.Create(new IdentityRole { Name = "Member" });
        }
        if(!context.Roles.Any(r => r.Name == "Guest"))
        {
            roleManager.Create(new IdentityRole { Name = "Guest" });
        }

        #endregion

        #region Users
        UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

        if(!context.Users.Any(u => u.Email == "gzambrana93@gmail.com"))
        {
            userManager.Create(new ApplicationUser
            {
                UserName = "gzambrana93@gmail.com",
                Email = "gzambrana93@gmail.com",
                FirstName = "Gregorio",
                LastName = "Zambrana",
                Avatar = WebConfigurationManager.AppSettings["DefaultAvatar"]
            }, WebConfigurationManager.AppSettings["DefaultPassword"]);

        }
        var userAdmin = userManager.FindByEmail("gzambrana93@gmail.com").Id;
        userManager.AddToRole(userAdmin, "Admin");

        if (!context.Users.Any(u => u.Email == "SeededHoh@mailinator.com"))
        {
            userManager.Create(new ApplicationUser
            {
                UserName = "SeededHoh@mailinator.com",
                Email = "SeededHoh@mailinator.com",
                FirstName = "House",
                LastName = "Leader",
                Avatar = WebConfigurationManager.AppSettings["DefaultAvatar"]
            }, WebConfigurationManager.AppSettings["DefaultPassword"]);
        }
        var userHead = userManager.FindByEmail("SeededHoh@mailinator.com").Id;
        userManager.AddToRole(userHead, "HOH");

        if (!context.Users.Any(u => u.Email == "SeededMember@mailinator.com"))
        {
            userManager.Create(new ApplicationUser
            {
                UserName = "SeededMember@mailinator.com",
                Email = "SeededMember@mailinator.com",
                FirstName = "House",
                LastName = "Member",
                Avatar = WebConfigurationManager.AppSettings["DefaultAvatar"]
            }, WebConfigurationManager.AppSettings["DefaultPassword"]);
        }
        var userMember = userManager.FindByEmail("SeededMember@mailinator.com").Id;
        userManager.AddToRole(userMember, "Member");

        if (!context.Users.Any(u => u.Email == "SeededGuest@mailinator.com"))
        {
            userManager.Create(new ApplicationUser
            {
                UserName = "SeededGuest@mailinator.com",
                Email = "SeededGuest@mailinator.com",
                FirstName = "Lobby",
                LastName = "Guest",
                Avatar = WebConfigurationManager.AppSettings["DefaultAvatar"]
            }, WebConfigurationManager.AppSettings["DefaultPassword"]);
        }
        var userGuest = userManager.FindByEmail("SeededGuest@mailinator.com").Id;
        userManager.AddToRole(userGuest, "Guest");

        #endregion

        #region AddUserToHousehold

        var household = context.Households.FirstOrDefault(h => h.Name == "Seeded House" ).Id;

        sHelp.AddUserToHouse(userAdmin, household);
        sHelp.AddUserToHouse(userHead, household);
        sHelp.AddUserToHouse(userMember, household);


        #endregion

        #region Banks
        context.Banks.AddOrUpdate(
            b => b.Name,

            new BankAccount { HouseholdId = household, Name ="BB&T" , InitialBalance = 1000.00, CurrentBalance = 1500.00, ReconciledBalance = 1500.00, LowLevelBalance = 100.00, Type = AccountType.Checking},
            new BankAccount { HouseholdId = household, Name = "Wells Fargo", InitialBalance= 500.00, CurrentBalance = 500.00, ReconciledBalance = 500.00, LowLevelBalance = 50.00, Type = AccountType.Savings}

            );
        #endregion

        #region Budgets
        context.Budgets.AddOrUpdate(
            b => b.Name,

            new Budget { HouseholdId = household, Name = "Utilities", Description = "Aggregation of funds for Utilities"},
            new Budget { HouseholdId = household, Name = "Vehicle Maintenance", Description = "Aggregation of Vehicle service funds"},
            new Budget { HouseholdId = household, Name = "Personal Items", Description = "Aggregation of funds for Personal Items, e.g Laptop, Camera, Phone" }
            );
        #endregion
        context.SaveChanges();
        #region BudgetItems

        var budgets = context.Budgets.AsNoTracking();

        var ubudId = budgets.FirstOrDefault(b => b.Name == "Utilities").Id;
        var vbudId = budgets.FirstOrDefault(b => b.Name == "Vehicle Maintenance").Id;
        var pbudId = budgets.FirstOrDefault(b => b.Name == "Personal Items").Id;

        context.BudgetItems.AddOrUpdate(
            i => i.Name,
            
            //Utilities Budget Items
            new BudgetItem { BudgetId = ubudId , Name = "Water Bill", Description = "For use of city water", CurrentAmount = 120.00, TargetAmount = 100.00 },
            new BudgetItem { BudgetId = ubudId , Name = "Eletric Bill", Description = "For use of city electric", CurrentAmount = 200.00, TargetAmount = 100.00 },
            new BudgetItem { BudgetId = ubudId, Name = "Cable Bill", Description = "For use of Spectrum cable services", CurrentAmount = 80.00, TargetAmount = 50.00 },
           
            //Vehicle Maintenance Budget Items
            new BudgetItem { BudgetId = vbudId , Name = "Oil Change", Description = "For Oil Change Service", CurrentAmount = 100.00, TargetAmount = 70.00 },
            new BudgetItem { BudgetId = vbudId , Name = "Filter Replacement", Description = "For replacement of various car filter", CurrentAmount = 120.00, TargetAmount = 100.00 },
            new BudgetItem { BudgetId = vbudId, Name = "Tire Replacement", Description = "For replacement of tires", CurrentAmount = 495.00, TargetAmount = 300.00 },

            //Personal Budget Items
            new BudgetItem { BudgetId = pbudId , Name = "Canon Camera", Description = "For events, gatherings in church", CurrentAmount = 1200.00, TargetAmount = 700.00 },
            new BudgetItem { BudgetId = pbudId , Name = "Surface Laptop", Description = "For use of at work", CurrentAmount = 1000.00, TargetAmount = 800.00 },
            new BudgetItem { BudgetId = pbudId, Name = "Galaxy Phone", Description = "For personal use", CurrentAmount = 600.00, TargetAmount = 300.00 }

            );
        #endregion
        context.SaveChanges();

        #region Transactions
        var banks = context.Banks.AsNoTracking();
        var budItems = context.BudgetItems.AsNoTracking();
        //Bank Account Id
       
        var bbtId = banks.FirstOrDefault(b => b.Name == "BB&T").Id;
        var wellsId = banks.FirstOrDefault(b => b.Name == "Wells Fargo").Id;

        //BudgetItem Id
        var budItemId1 = budItems.FirstOrDefault(b => b.Name == "Water bill").Id;
        var budItemId2 = budItems.FirstOrDefault(b => b.Name == "Eletric Bill").Id;
        var budItemId3 = budItems.FirstOrDefault(b => b.Name == "Cable Bill").Id;
        var budItemId4 = budItems.FirstOrDefault(b => b.Name == "Oil Change").Id;
        var budItemId5 = budItems.FirstOrDefault(b => b.Name == "Filter Replacement").Id;
        var budItemId6 = budItems.FirstOrDefault(b => b.Name == "Tire Replacement").Id;
        var budItemId7 = budItems.FirstOrDefault(b => b.Name == "Canon Camera").Id;
        var budItemId8 = budItems.FirstOrDefault(b => b.Name == "Surface Laptop").Id;
        var budItemId9 = budItems.FirstOrDefault(b => b.Name == "Galaxy Phone").Id;


        context.Transactions.AddOrUpdate(
            t => t.Description,

            //Transactions for Utility Budget Items
            new Transaction { BankAccountId = bbtId, BudgetItemId = budItemId1, EnteredById = userHead, Date = DateTime.Now, Description = "Payment for Water Bill", Amount = 70.00, Reconciled = false, ReconciledAmount = 50.00, Type = TransactionType.Payment },
            new Transaction { BankAccountId = bbtId, BudgetItemId = budItemId2, EnteredById = userHead, Date = DateTime.Now, Description = "Payment for Electric Bill", Amount = 150.00, Reconciled = false, ReconciledAmount = 100.00, Type = TransactionType.Payment },
            new Transaction { BankAccountId = bbtId, BudgetItemId = budItemId3, EnteredById = userHead, Date = DateTime.Now, Description = "Payment for Cable Bill", Amount = 45.00, Reconciled = false, ReconciledAmount = 100.00, Type = TransactionType.Payment },
                                                                              
            //Transactions for Vehicle Maintenance Budget Items             
            new Transaction {  BankAccountId = bbtId, BudgetItemId = budItemId4, EnteredById = userMember, Date = DateTime.Now, Description = "Oil Change Servicing", Amount = 72.00, Reconciled = false, ReconciledAmount = 70.00, Type = TransactionType.Withdrawal },
            new Transaction {  BankAccountId = bbtId, BudgetItemId = budItemId5, EnteredById = userMember, Date = DateTime.Now, Description = "Car Cabin Filter Change", Amount = 50.00, Reconciled = false, ReconciledAmount = 100.00, Type = TransactionType.Withdrawal },
            new Transaction {  BankAccountId = bbtId, BudgetItemId = budItemId6, EnteredById = userMember, Date = DateTime.Now, Description = "Tire Replacement (2)", Amount = 170.00, Reconciled = false, ReconciledAmount = 300.00, Type = TransactionType.Withdrawal },

            //Transactions for Personal Budget Items
            new Transaction { BankAccountId = wellsId, BudgetItemId = budItemId7, EnteredById = userAdmin, Date = DateTime.Now, Description = "Stash for new camera", Amount = 200.00, Reconciled = false, ReconciledAmount = 700.00, Type = TransactionType.Deposit },
            new Transaction { BankAccountId = wellsId, BudgetItemId = budItemId8, EnteredById = userAdmin, Date = DateTime.Now, Description = "Stash for new laptop", Amount = 300.00, Reconciled = false, ReconciledAmount = 800.00, Type = TransactionType.Deposit },
            new Transaction { BankAccountId = wellsId, BudgetItemId = budItemId9, EnteredById = userAdmin, Date = DateTime.Now, Description = "Stash for new phone", Amount = 100.00, Reconciled = false, ReconciledAmount = 300.00, Type = TransactionType.Deposit }

            ); 



        #endregion
    }
}