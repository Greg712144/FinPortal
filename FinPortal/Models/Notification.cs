using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinPortal.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public string ReceiverId { get; set; }
        [MaxLength(200), MinLength(5)]
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public bool Read { get; set; }
        public virtual ApplicationUser Receiver { get; set; }
    }
}