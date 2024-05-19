using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Walkerscott.Article.Domain.Entities;

namespace Walkerscott.Article.Domain.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<(IEnumerable<T>, int)> GetArticlesAsync(int pageNumber, int pageSize, bool descending = true);

        Task<(IEnumerable<T>, int)> GetArticlesAsync(Expression<Func<T, bool>> predicate, int pageNumber, int pageSize, bool descending = true);
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> ListAllAsync();
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
