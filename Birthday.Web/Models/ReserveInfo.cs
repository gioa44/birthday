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

        [Required(ErrorMessage = "", ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(GeneralResource))]
        [Display(Name = "FirstName", ResourceType = typeof(GeneralResource))]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "", ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(GeneralResource))]
        [Display(Name = "LastName", ResourceType = typeof(GeneralResource))]
        public string LastName { get; set; }

        public bool Confirm { get; set; }
    }
}