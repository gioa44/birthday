using Birthday.Properties.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Birthday.Web.Models
{
    public class VisualizationLogin
    {
        [Required(ErrorMessage = "", ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(GeneralResource))]
        [EmailAddress(ErrorMessage = "", ErrorMessageResourceName = "EmailNotValid", ErrorMessageResourceType = typeof(GeneralResource))]
        [Display(Name = "Email", ResourceType = typeof(GeneralResource))]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}