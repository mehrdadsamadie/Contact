using Contact.Entities.Model;
using Microsoft.EntityFrameworkCore;
using Repository.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Repository.Constructure
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected ContactContext ContactContext { get; set; }

        public RepositoryBase(ContactContext contactContext)
        {
            this.ContactContext = contactContext;
        }

        public IQueryable<T> FindAll()
        {
            return this.ContactContext.Set<T>().AsNoTracking();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return this.ContactContext.Set<T>().Where(expression).AsNoTracking();
        }

        public T Create(T entity)
        {
            this.ContactContext.Set<T>().Add(entity);
            return entity;
        }

        public void Update(T entity)
        {
            this.ContactContext.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            this.ContactContext.Set<T>().Remove(entity);
        }
    }
}
