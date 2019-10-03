﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinPortal.Models
{
    public class Invitation
    {
        public int Id { get; set; }
        public int HouseholdId { get; set; }
        [Required]
        public Guid KeyCode { get; set; }
        [Required,EmailAddress]
        public string Email { get; set; }
        [AllowHtml]
        [MaxLength(200), MinLength(5)]
        public string Body { get; set; }
        [MaxLength(200), MinLength(5)]
        public string Subject { get; set; }

        public DateTime Created { get; set; }
        public DateTime? Accepted { get; set; }
        public DateTime Expires { get; set; }
        public bool Expired { get; set; }

        public virtual Household Household { get; set; }

    }
}