using CoachCalcAPI.Data;
using CoachCalcAPI.Models;
using CoachCalcAPI.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Security.Claims;

namespace CoachCalcAPI.Repository
{
    public class AthleteeRepository : IAthleteeRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AthleteeRepository(ApplicationDbContext db, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task CreateAsync(Athletee entity)
        {
            string userId = _httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value;
            entity.UserId = userId;
            await _db.Athletees.AddAsync(entity);
            await SaveAsync();
        }

        public async Task<List<Athletee>> GetAllAsync(Expression<Func<Athletee, bool>>? filter = null)
        {
            IQueryable<Athletee> query = _db.Athletees;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.ToListAsync();
        }

        public async Task<Athletee> GetAsync(Expression<Func<Athletee, bool>> filter = null, bool tracked = true)
        {
            IQueryable<Athletee> query = _db.Athletees;
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

        public async Task RemoveAsync(Athletee entity)
        {
            _db.Athletees.Remove(entity);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }

        public async Task<Athletee> UpdateAsync(Athletee entity)
        {
            string userId = _httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value;
            entity.UserId = userId;
            _db.Athletees.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
