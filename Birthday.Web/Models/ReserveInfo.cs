using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Birthday.Properties.Resources;

namespace Birthday.Web.Models
{
    public class ReserveInfo
    {
        [Required]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "", ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(GeneralResource))]
        [EmailAddress(ErrorMessage = "", ErrorMessageResourceName = "EmailNotValid", ErrorMessageResourceType = typeof(GeneralResource))]
        [Display(Name = "Email", ResourceType = typeof(GeneralResource))]
        public string Email { get; set; }
    }
}