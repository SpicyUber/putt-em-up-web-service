using Infrastructure.Persistence.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories.Implementations
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        protected readonly PuttEmUpDbContext context;

        public GenericRepository(PuttEmUpDbContext context)
        {
            this.context = context;
        }

        public virtual void Add(T entity)
        {
            context.Set<T>().Add(entity);
        }
        public virtual void Delete(T entity)
        {
            context.Set<T>().Remove(entity);
        }
        public virtual IEnumerable<T> GetAll()
        {
            return context.Set<T>().ToList();
        }
        public virtual T GetById(long id)
        {
            return context.Set<T>().Find(id);
        }
        public virtual void Update(T entity)
        {
            context.Set<T>().Update(entity);
        }

        public virtual IQueryable<T> Query()
        {
            return context.Set<T>();
        }

    }
}
