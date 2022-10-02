using CoachCalcAPI.Models;
using System.Linq.Expressions;

namespace CoachCalcAPI.Repository.IRepository
{
    public interface IExerciseRepository
    {
        Task<List<Exercise>> GetAllAsync(Expression<Func<Exercise, bool>>? filter = null);
        Task<Exercise> UpdateAsync(Exercise entity);
        Task<Exercise> GetAsync(Expression<Func<Exercise, bool>> filter = null, bool tracked = true);
        Task CreateAsync(Exercise entity);
        Task RemoveAsync(Exercise entity);
        Task SaveAsync();
    }
}
