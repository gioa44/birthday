using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Birthday.Web.Models
{
    public class TemplateSelect
    {
        public int? TemplateID { get; set; }
        public SelectList TemplateList { get; set; }
    }
}