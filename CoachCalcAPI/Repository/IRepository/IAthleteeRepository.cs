using CoachCalcAPI.Models;

namespace CoachCalcAPI.Repository.IRepository
{
    public interface IAthleteeRepository
    {
        ICollection<Athletee> GetAthletees();
        Athletee GetAthletee(string athleteeId);

        bool AthleteeExistsByLastName(string name);

        bool AthleteeExistsById(string id);
        bool CreateAthletee(Athletee athletee);
        bool UpdateAthletee(Athletee athletee);
        bool DeleteAthletee(Athletee athletee);
        bool Save();
    }
}
