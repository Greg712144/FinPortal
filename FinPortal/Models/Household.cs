using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinPortal.Models
{
    public class Household
    {
        public int Id { get; set; }
        [Required, MaxLength(40), MinLength(2)]
        public string Name { get; set; }
        public string Greeting { get; set; }



        //Collection Properties
        public virtual ICollection<BankAccount> Bank { get; set; }
        public virtual ICollection<ApplicationUser> Members { get; set; }
        public virtual ICollection<Budget> Budgets { get; set; }
        public virtual ICollection<Invitation> Invitations { get; set; }


        public Household()
        {
            this.Bank = new HashSet<BankAccount>();
            this.Members = new HashSet<ApplicationUser>();
            this.Budgets = new HashSet<Budget>();
            this.Invitations = new HashSet<Invitation>();
        }
    }
}