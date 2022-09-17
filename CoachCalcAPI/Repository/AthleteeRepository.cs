using CoachCalcAPI.Data;
using CoachCalcAPI.Models;
using CoachCalcAPI.Repository.IRepository;

namespace CoachCalcAPI.Repository
{
    public class AthleteeRepository : IAthleteeRepository
    {
        private readonly ApplicationDbContext _db;

        public AthleteeRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public bool AthleteeExistsById(string id)
        {
            return _db.Athletees.Any(a => a.Id == id);
        }

        public bool AthleteeExistsByLastName(string name)
        {
            bool value = _db.Athletees.Any(a => a.LastName.ToLower().Trim() == name.ToLower().Trim());
            return value;
        }

        public bool CreateAthletee(Athletee athletee)
        {
            _db.Athletees.Add(athletee);
            return Save();
        }

        public bool DeleteAthletee(Athletee athletee)
        {
            _db.Athletees.Remove(athletee);
            return Save();
        }

        public Athletee GetAthletee(string athleteeId)
        {
            return _db.Athletees.FirstOrDefault(a => a.Id == athleteeId);
        }

        public ICollection<Athletee> GetAthletees()
        {
            return _db.Athletees.ToList();
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0;
        }

        public bool UpdateAthletee(Athletee athletee)
        {
            _db.Athletees.Update(athletee);
            return Save();
        }
    }
}
