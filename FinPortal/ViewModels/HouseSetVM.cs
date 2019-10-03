using FinPortal.Enumerations;
using FinPortal.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinPortal.ViewModels
{
    public class HouseSetVM
    {
        //Set Bank Account
        public int HouseholdId { get; set; }
        public string BankName { get; set; }
        [Range(0.00, 100000.00)]
        public double InitialBalance { get; set; }
        [Range(0.00, 100000.00)]
        public double CurrentBalance { get; set; }
        [Range(0.00, 100000.00)]
        public double ReconciledBalance { get; set; }
        [Range(0.00, 100000.00)]
        public double LowLevelBalance { get; set; }
        public AccountType Type { get; set; }

        //Set Budget
        public string BudName { get; set; }

        [MaxLength(200), MinLength(5)]
        public string Description { get; set; }

        //Set Budget Items
        public string BudItemName { get; set; }
        [Required, MaxLength(200), MinLength(5)]
        public string BudItemDescription { get; set; }
        [Range(0.00, 100000.00)]
        public double TargetAmount { get; set; }
        [Range(0.00, 100000.00)]
        public double CurrentAmount { get; set; }

        public virtual Household Household { get; set; }
    }
}