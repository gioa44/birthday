using Birthday.Domain;
using Birthday.Properties.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Birthday.Web.Models
{
    public class PersonInfo
    {
        public Nullable<Genders> GenderID { get; set; }
        public Nullable<int> Age { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNumber { get; set; }
        public string FacebookUrl { get; set; }

        [Required(ErrorMessage = "", ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(GeneralResource))]
        [EmailAddress(ErrorMessage = "", ErrorMessageResourceName = "EmailNotValid", ErrorMessageResourceType = typeof(GeneralResource))]
        [Display(Name = "Email", ResourceType = typeof(GeneralResource))]
        public string Email { get; set; }
    }
}