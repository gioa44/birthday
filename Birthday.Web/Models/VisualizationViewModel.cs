using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Birthday.Web.Models
{
    public class VisualizationViewModel
    {
        public string Html { get; set; }
        public List<ImageInfo> ImageProps { get; set; }
        public int? TemplateID { get; set; }
        public SelectList TemplateList { get; set; }
    }
}