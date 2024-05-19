using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Walkerscott.Article.Domain.Interfaces;
using Walkerscott.Article.Infrastructure.Persistence;

namespace Walkerscott.Article.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _dbContext;

        public Repository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<T> AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<(IEnumerable<T>, int)> GetArticlesAsync(int pageNumber, int pageSize, bool descending = true)
        {
            var query = _dbContext.Set<T>().AsQueryable();

            //if (descending)
            //{
            //    query = query.OrderByDescending(o => o.da);
            //}
            //else
            //{
            //    query = query.OrderBy(o => o.CreatedDate);
            //}

            var totalCount = await _dbContext.Set<T>().CountAsync();
            var articles = await _dbContext.Set<T>()
                .OrderByDescending(e => EF.Property<DateTime>(e, "CreatedDate"))
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return (articles, totalCount);
        }

        public async Task<(IEnumerable<T>, int)> GetArticlesAsync(Expression<Func<T, bool>> predicate, int pageNumber, int pageSize, bool descending = true)
        {
            var query = _dbContext.Set<T>().Where(predicate);

            var totalCount = await query.CountAsync();
            var data = await query
                .OrderByDescending(e => EF.Property<DateTime>(e, "CreatedDate"))
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (data, totalCount);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
    }
}
