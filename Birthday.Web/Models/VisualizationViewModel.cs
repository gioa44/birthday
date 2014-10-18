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
        public List<BirthdayText> Texts { get; set; }
        public string TemplateName { get; set; }
    }
}