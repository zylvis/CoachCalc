using CoachCalcAPI.Data;
using CoachCalcAPI.Models;
using CoachCalcAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CoachCalcAPI.Repository
{
    public class ResultRepository: IResultRepository
    {
        private readonly ApplicationDbContext _db;

        public ResultRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task CreateAsync(Result entity)
        {

            await _db.Results.AddAsync(entity);
            await SaveAsync();
        }

        public async Task<List<Result>> GetAllAsync(Expression<Func<Result, bool>>? filter = null)
        {
            IQueryable<Result> query = _db.Results;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.ToListAsync();
        }

        public async Task<Result> GetAsync(Expression<Func<Result, bool>> filter = null, bool tracked = true)
        {
            IQueryable<Result> query = _db.Results;
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

        public async Task RemoveAsync(Result entity)
        {
            _db.Results.Remove(entity);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }

        public async Task<Result> UpdateAsync(Result entity)
        {
            _db.Results.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
