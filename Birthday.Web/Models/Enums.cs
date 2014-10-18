using Birthday.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Birthday.Web.Models
{
    public enum VisualizationSteps
    {
        [LocalizedDescription("PersonInfo", typeof(Birthday.Properties.Resources.EnumResource))]
        PersonInfo,
        [LocalizedDescription("Template", typeof(Birthday.Properties.Resources.EnumResource))]
        Template,
        [LocalizedDescription("TemplateFill", typeof(Birthday.Properties.Resources.EnumResource))]
        TemplateFill,
        [LocalizedDescription("Complete", typeof(Birthday.Properties.Resources.EnumResource))]
        Complete
    };
}