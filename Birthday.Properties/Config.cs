using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace Birthday.Properties
{
    public static class Config
    {
        public static string DefaultCulture
        {
            get
            {
                return (ConfigurationManager.GetSection("system.web/globalization") as GlobalizationSection).Culture;
            }
        }

        public static int PasswordLength { get { return int.Parse(ConfigurationManager.AppSettings["PasswordLength"]); } }
    }
}
