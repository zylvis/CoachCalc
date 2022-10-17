using CoachCalcAPI.Data;
using CoachCalcAPI.Models;
using CoachCalcAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Security.Claims;

namespace CoachCalcAPI.Repository
{
    public class ExerciseRepository : IExerciseRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ExerciseRepository(ApplicationDbContext db, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task CreateAsync(Exercise entity)
        {
            string userId = _httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value;
            entity.UserId = userId;
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
            string userId = _httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value;
            entity.UserId = userId;
            _db.Exercises.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
