using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Birthday.Web.Models
{
    public class CurrentBirthday
    {
        public string Html { get; set; }
        public string TemplateName { get; set; }
        public List<ImageInfo> ImageProps { get; set; }
    }
}