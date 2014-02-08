using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Birthday.Domain.Services
{

    public class TemplateService : DomainServiceBase<Template>
    {
        public TemplateService(BirthdayContext context)
            : base(context) { }
    }
}
