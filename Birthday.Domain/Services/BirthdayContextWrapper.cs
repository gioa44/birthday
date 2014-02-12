using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Birthday.Domain.Services
{
    public class BirthdayContextContainer
    {
        protected readonly BirthdayContext _DbContext;

        public DbContextConfiguration Configuration
        {
            get
            {
                return _DbContext.Configuration;
            }
        }

        public BirthdayContextContainer(BirthdayContextContainer container)
            : this(container._DbContext)
        {
        }

        public BirthdayContextContainer()
            : this(new BirthdayContext())
        { }

        public BirthdayContextContainer(BirthdayContext context)
        {
            _DbContext = context;
        }

        public virtual void SaveChanges()
        {
            _DbContext.SaveChanges();
        }

        public void Dispose()
        {
            _DbContext.Dispose();
        }
    }
}
