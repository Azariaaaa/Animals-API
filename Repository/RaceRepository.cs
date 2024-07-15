using FinalWorkshop.Database;
using Microsoft.EntityFrameworkCore;

namespace FinalWorkshop.Repository
{
    public class RaceRepository
    {
        private readonly DatabaseContext _context;

        public RaceRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Race>> GetAllAsync()
        {
            return await _context.Races.ToListAsync();
        }

        public async Task<Race?> GetByIdAsync(int id)
        {
            return await _context.Races.FindAsync(id);
        }

        public async Task AddAsync(Race race)
        {
            _context.Races.Add(race);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Race race)
        {
            _context.Races.Update(race);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var race = await _context.Races.FindAsync(id);
            if (race != null)
            {
                _context.Races.Remove(race);
                await _context.SaveChangesAsync();
            }
        }
    }
}
