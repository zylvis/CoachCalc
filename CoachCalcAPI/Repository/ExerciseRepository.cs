using CoachCalcAPI.Data;
using CoachCalcAPI.Models;
using CoachCalcAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CoachCalcAPI.Repository
{
    public class ExerciseRepository : IExerciseRepository
    {
        private readonly ApplicationDbContext _db;

        public ExerciseRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task CreateAsync(Exercise entity)
        {
            
            await _db.Exercises.AddAsync(entity);
            await SaveAsync();
        }

        public async Task<List<Exercise>> GetAllAsync(Expression<Func<Exercise, bool>>? filter = null)
        {
            IQueryable<Exercise> query = _db.Exercises;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.ToListAsync();
        }

        public async Task<Exercise> GetAsync(Expression<Func<Exercise, bool>> filter = null, bool tracked = true)
        {
            IQueryable<Exercise> query = _db.Exercises;
            if (!tracked)
            {
                query = query.AsNoTracking();
            }
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.FirstOrDefaultAsync();
        }

        public async Task RemoveAsync(Exercise entity)
        {
            _db.Exercises.Remove(entity);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }

        public async Task<Exercise> UpdateAsync(Exercise entity)
        {
            _db.Exercises.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
