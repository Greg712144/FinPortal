using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinPortal.Models
{
    public class Budget
    {
        public int Id { get; set; }

        public int HouseholdId { get; set; }

        [Required, MaxLength(40), MinLength(5)]
        public string Name { get; set; }

        [MaxLength(200),MinLength(5)]
        public string Description { get; set; }

        //Navigational Property to Parent model
        public virtual Household Household { get; set; }
       
        //Collection of child model
         public virtual ICollection<BudgetItem> BudgetItems { get; set; }

        public Budget()
        {
            this.BudgetItems = new HashSet<BudgetItem>();
        }
    }
}