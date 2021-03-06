//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Birthday.Domain
{
    using System;
    using System.Collections.Generic;
    
    public partial class Template
    {
        public Template()
        {
            this.Birthdays = new HashSet<Birthday>();
            this.TemplateImages = new HashSet<TemplateImage>();
        }
    
        public int TemplateID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Html { get; set; }
        public int PictureCount { get; set; }
        public int TextCount { get; set; }
        public bool IsActive { get; set; }
    
        public virtual ICollection<Birthday> Birthdays { get; set; }
        public virtual ICollection<TemplateImage> TemplateImages { get; set; }
    }
}
