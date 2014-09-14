using Birthday.Properties.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Birthday.Web.Models
{
    public class ContactViewModel
    {
        [Required(ErrorMessage = "", ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(GeneralResource))]
        [Display(Name = "FirstNameLastName", ResourceType = typeof(GeneralResource))]
        public string FullName { get; set; }

        [Required(ErrorMessage = "", ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(GeneralResource))]
        [EmailAddress(ErrorMessage = "", ErrorMessageResourceName = "EmailNotValid", ErrorMessageResourceType = typeof(GeneralResource))]
        [Display(Name = "Email", ResourceType = typeof(GeneralResource))]
        public string Email { get; set; }

        [Required(ErrorMessage = "", ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(GeneralResource))]
        [Display(Name = "Title", ResourceType = typeof(GeneralResource))]
        public string Title { get; set; }

        [Required(ErrorMessage = "", ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(GeneralResource))]
        [Display(Name = "Message", ResourceType = typeof(GeneralResource))]
        public string Message { get; set; }
    }
}