using FinalWorkshop.Repository;

namespace FinalWorkshop.Service
{
    public class AnimalService
    {
        private readonly AnimalRepository _animalRepository;

        public AnimalService(AnimalRepository animalRepository)
        {
            _animalRepository = animalRepository;
        }

        public async Task<IEnumerable<Animal>> GetAllAsync()
        {
            return await _animalRepository.GetAllAsync();
        }

        public async Task<Animal?> GetByIdAsync(int id)
        {
            return await _animalRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(Animal animal)
        {
            await _animalRepository.AddAsync(animal);
        }

        public async Task UpdateAsync(Animal animal)
        {
            await _animalRepository.UpdateAsync(animal);
        }

        public async Task DeleteAsync(int id)
        {
            await _animalRepository.DeleteAsync(id);
        }
    }
}
