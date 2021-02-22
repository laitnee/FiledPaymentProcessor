using FiledPaymentProcessor.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FiledPaymentProcessor.Core.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        Task Add(TEntity entity);

        Task Delete(TEntity entity);

        Task Delete(Guid Id);

        Task<IEnumerable<TEntity>> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");

        Task<IEnumerable<TEntity>> GetAll();

        Task<TEntity> GetById(Guid Id);

        Task<IEnumerable<TEntity>> GetWithRawSql(string query,
        params object[] parameters);

        void Update(TEntity entity);
    }
}
