using FinalWorkshop.Repository;

namespace FinalWorkshop.Service
{
    public class RaceService
    {
        private readonly RaceRepository _raceRepository;

        public RaceService(RaceRepository raceRepository)
        {
            _raceRepository = raceRepository;
        }

        public async Task<IEnumerable<Race>> GetAllAsync()
        {
            return await _raceRepository.GetAllAsync();
        }

        public async Task<Race?> GetByIdAsync(int id)
        {
            return await _raceRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Race race)
        {
            await _raceRepository.AddAsync(race);
        }

        public async Task UpdateAsync(Race race)
        {
            await _raceRepository.UpdateAsync(race);
        }

        public async Task DeleteAsync(int id)
        {
            await _raceRepository.DeleteAsync(id);
        }
    }
}
