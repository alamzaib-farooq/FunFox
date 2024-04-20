using FunFox.Domain.Common.Interfaces;
using System.Linq.Expressions;

namespace FunFox.Application.Contracts.Repositories
{
    public interface IGenericRepository<T> where T : class, IEntity
    {
        Task<T> GetByIdAsync(int id);
        Task<List<T>> GetAllAsync();
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<List<T>> GetAllByWhereWithNoTrackingAsync(Expression<Func<T, bool>> predicate);
    }
}
