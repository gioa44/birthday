using Birthday.Properties.Resources;
using Birthday.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Birthday.Domain
{
    public enum Genders
    {
        [LocalizedDescription("Male", typeof(EnumResource))]
        Male = 1,
        [LocalizedDescription("Female", typeof(EnumResource))]
        Female = 2
    }
}
