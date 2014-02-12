using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Birthday.Domain.Services
{
    public abstract class DomainServiceBase<T> : BirthdayContextContainer, IDisposable where T : class /*T=Model*/
    {
         public DomainServiceBase(BirthdayContextContainer container)
            : base(container)
        {
        }

        public DomainServiceBase()
            : base(new BirthdayContext())
        { }

        public DomainServiceBase(BirthdayContext context)
            : base(context)
        { }

        public virtual IQueryable<T> GetAll()
        {
            return _DbContext.Set<T>();
        }

        public virtual T Get(dynamic id)
        {
            return _DbContext.Set<T>().Find(id);
        }

        public virtual void Add(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _DbContext.Set<T>().Add(entity);
        }

        public virtual void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _DbContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _DbContext.Set<T>().Remove(entity);
        }

        public virtual void Delete(IEnumerable<T> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException("entity");
            }

            foreach (var entity in entities)
            {
                Delete(entity);
            }
        }
    }
}
