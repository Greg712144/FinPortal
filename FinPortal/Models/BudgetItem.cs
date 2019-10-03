using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinPortal.Models
{
    public class BudgetItem
    {
        public int Id { get; set; }
        public int BudgetId { get; set; }
        [Required,MaxLength(40), MinLength(5)]
        public string Name { get; set; }
        [Required, MaxLength(200), MinLength(5)]
        public string Description { get; set; }
        [Range(0.00, 100000.00)]
        public double TargetAmount { get; set; }
        [Range(0.00, 100000.00)]
        public double CurrentAmount { get; set; }


        public virtual Budget Budget { get; set; } 

        public virtual ICollection<Transaction> Transactions { get; set; }

        public BudgetItem()
        {
            this.Transactions = new HashSet<Transaction>();
        }



    }
}