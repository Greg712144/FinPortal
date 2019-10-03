using FinPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinPortal.ViewModels
{
    public class DataVM
    {

        public virtual Household Household { get; set; }
        public ICollection<BankAccount> Banks { get; set; }
        public ICollection<Budget> Budgets { get; set; }
        public ICollection<BudgetItem> BudgetItems { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
        public ICollection<ApplicationUser> Members { get; set; }

        public DataVM()
        {
            Banks = new HashSet<BankAccount>();
            Budgets = new HashSet<Budget>();
            BudgetItems = new HashSet<BudgetItem>();
            Transactions = new HashSet<Transaction>();
            Members = new HashSet<ApplicationUser>();
        }

    }
}