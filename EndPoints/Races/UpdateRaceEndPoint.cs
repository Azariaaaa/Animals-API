using FastEndpoints;
using FinalWorkshop.Database;
using FinalWorkshop.DTO.Requests.Animals;
using FinalWorkshop.DTO.Requests.Races;

namespace FinalWorkshop.EndPoints.Races
{
    public class UpdateRaceEndPoint : Endpoint<UpdateRaceRequestDTO>
    {
        private readonly DatabaseContext _dbContext;

        public UpdateRaceEndPoint(DatabaseContext context)
        {
            _dbContext = context;
        }
        public override void Configure()
        {
            Post("/update/race");
            AllowAnonymous();
        }

        public override async Task HandleAsync(UpdateRaceRequestDTO data, CancellationToken ct)
        {
            Race? raceToUpdate = await _dbContext.Races.FindAsync(data.Id);

            if (raceToUpdate != null)
            {
                raceToUpdate.Name = data.Name;

                try
                {
                    _dbContext.Races.Update(raceToUpdate);
                    await _dbContext.SaveChangesAsync();
                    await SendAsync(new { Message = "Ok" }, 201);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erreur :" + ex);
                    await SendAsync(new { Message = "Erreur lors de la mise à jour de la race" }, 500);
                }
            }
            else
            {
                await SendAsync(new { Message = "Race introuvable" }, 404);
            }
        }
    }
}
