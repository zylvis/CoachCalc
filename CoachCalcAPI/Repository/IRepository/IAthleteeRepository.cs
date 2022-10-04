using CoachCalcAPI.Models;
using System.Linq.Expressions;

namespace CoachCalcAPI.Repository.IRepository
{
    public interface IAthleteeRepository
    {
        Task<List<Athletee>> GetAllAsync(Expression<Func<Athletee, bool>>? filter = null);
        Task<Athletee> UpdateAsync(Athletee entity);
        Task<Athletee> GetAsync(Expression<Func<Athletee, bool>> filter = null, bool tracked = true);
        Task CreateAsync(Athletee entity);
        Task RemoveAsync(Athletee entity);
        Task SaveAsync();
    }
}
