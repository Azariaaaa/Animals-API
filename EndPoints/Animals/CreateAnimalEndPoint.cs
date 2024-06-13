using FastEndpoints;
using FinalWorkshop.Database;
using FinalWorkshop.DTO.Requests.Animals;

namespace FinalWorkshop.EndPoints.Animals
{
    public class CreateAnimalEndPoint : Endpoint<CreateAnimalRequestDTO>
    {
        private readonly DatabaseContext _dbContext;

        public CreateAnimalEndPoint(DatabaseContext context)
        {
            _dbContext = context;
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
