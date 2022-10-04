﻿using CoachCalcAPI.Data;
using CoachCalcAPI.Models;
using CoachCalcAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CoachCalcAPI.Repository
{
    public class AthleteeRepository : IAthleteeRepository
    {
        private readonly ApplicationDbContext _db;

        public AthleteeRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task CreateAsync(Athletee entity)
        {
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
            _db.Athletees.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
