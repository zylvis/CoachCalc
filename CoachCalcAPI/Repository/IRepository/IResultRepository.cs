using CoachCalcAPI.Models;
using System.Linq.Expressions;

namespace CoachCalcAPI.Repository.IRepository
{
    public interface IResultRepository
    {
        Task<List<Result>> GetAllAsync(Expression<Func<Result, bool>>? filter = null);
        Task<Result> UpdateAsync(Result entity);
        Task<Result> GetAsync(Expression<Func<Result, bool>> filter = null, bool tracked = true);
        Task CreateAsync(Result entity);
        Task RemoveAsync(Result entity);
        Task SaveAsync();
    }
}
