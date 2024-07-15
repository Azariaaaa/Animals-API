using FastEndpoints;
using FinalWorkshop.Database;
using FinalWorkshop.DTO.Requests.Animals;
using FinalWorkshop.Service;

namespace FinalWorkshop.EndPoints.Animals
{
    public class CreateAnimalEndPoint : Endpoint<CreateAnimalRequestDTO>
    {
        public readonly DatabaseContext _dbContext;
        private readonly AnimalService _animalService;
        private readonly RaceService _raceService;

        public CreateAnimalEndPoint(DatabaseContext context, AnimalService animalService, RaceService raceService)
        {
            _dbContext = context;
            _animalService = animalService;
            _raceService = raceService;
        }
        public override void Configure()
        {
            Post("/create/animal");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CreateAnimalRequestDTO data, CancellationToken ct)
        {
            var newAnimalRace = _dbContext.Races
                .Where(race => race.Name == data.RaceName)
                .FirstOrDefault();

            if (newAnimalRace == null)
            {
                Race race = new Race
                {
                    Name = data.RaceName,
                };

                _dbContext.Races.Add(race);
                await _dbContext.SaveChangesAsync();

                newAnimalRace = _dbContext.Races
                    .Where(race => race.Name == data.RaceName)
                    .FirstOrDefault();
            }

            Animal newAnimal = new Animal
            {
                Name= data.Name,
                Description = data.Description,
                Race = newAnimalRace
            };

            _dbContext.Animals.Add(newAnimal);
            await _dbContext.SaveChangesAsync();

            await SendAsync(new { Message = "Ok" }, 201);
        }
    }
}
