using FinPortal.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinPortal.Models
{
    public class BankAccount
    {
        public int Id { get; set; }
        public int HouseholdId { get; set; }
        public string Name { get; set; }
        [Range(0.00, 100000.00)]
        public double InitialBalance { get; set; }
        [Range(0.00, 100000.00)]
        public double CurrentBalance { get; set; }
        [Range(0.00, 100000.00)]
        public double ReconciledBalance { get; set; }
        [Range(0.00, 100000.00)]
        public double LowLevelBalance { get; set; }
        public AccountType Type { get; set; }

        //Navigational Property to Parent Model
        public virtual Household Household { get; set; }

        //Collection of child properties
        public virtual ICollection<Transaction> Transactions { get; set; }

        public BankAccount()
        {
           this.Transactions = new HashSet<Transaction>();
        }

    }
}