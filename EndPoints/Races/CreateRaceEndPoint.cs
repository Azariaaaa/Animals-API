using FastEndpoints;
using FinalWorkshop.Database;
using FinalWorkshop.DTO.Requests.Animals;
using FinalWorkshop.DTO.Requests.Races;

namespace FinalWorkshop.EndPoints.Races
{
    public class CreateRaceEndPoint : Endpoint<CreateRaceRequestDTO>
    {
        private readonly DatabaseContext _dbContext;

        public CreateRaceEndPoint(DatabaseContext context)
        {
            _dbContext = context;
        }
        public override void Configure()
        {
            Post("/create/race");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CreateRaceRequestDTO data, CancellationToken ct)
        {
            Race newRace = new Race 
            {
                Name = data.Name,
            };

            if (newRace != null)
            {
                try
                {
                    _dbContext.Races.Add(newRace);
                    await _dbContext.SaveChangesAsync();
                    await SendAsync(new { Message = "Ok" }, 201);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erreur :" + ex);
                    await SendAsync(new { Message = "Erreur lors de l'ajout d'une nouvelle race" }, 500);
                }
            }
            else
            {
                await SendAsync(new { Message = "Race a retourné null" }, 404);
            }

        }
    }
}
