using FinPortal.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinPortal.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public int BankAccountId { get; set; }
        public int? BudgetItemId { get; set; }
        public string EnteredById { get; set; }
        [Required, MaxLength(25), MinLength(3)]
        public string Description { get; set; }
        public DateTime Date { get; set; }

        [Range(0.00, 100000.00)]
        public double Amount { get; set; }
        public bool Reconciled { get; set; }

        [Range(0.00, 100000.00)]
        public double? ReconciledAmount { get; set; }

        public TransactionType Type { get; set; }

        //Navigational Properties
        public virtual BankAccount Bank { get; set; }
        public virtual BudgetItem BudgetItem { get; set; }
        public virtual ApplicationUser EnteredBy { get; set; }

    }
}