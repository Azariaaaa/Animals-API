using FastEndpoints;
using FinalWorkshop.Database;
using FinalWorkshop.DTO.Requests.Animals;

namespace FinalWorkshop.EndPoints.Animals
{
    public class UpdateAnimalEndPoint : Endpoint<UpdateAnimalRequestDTO>
    {
        private readonly DatabaseContext _dbContext;

        public UpdateAnimalEndPoint(DatabaseContext context)
        {
            _dbContext = context;
        }
        public override void Configure()
        {
            Post("/update/animal");
            AllowAnonymous();
        }

        public override async Task HandleAsync(UpdateAnimalRequestDTO data, CancellationToken ct)
        {
            Animal? animalToUpdate = await _dbContext.Animals.FindAsync(data.Id);

            if (animalToUpdate != null)
            {
                animalToUpdate.Name = data.Name;
                animalToUpdate.Description = data.Description;
                animalToUpdate.RaceId = data.RaceId;

                try
                {
                    _dbContext.Animals.Update(animalToUpdate);
                    await _dbContext.SaveChangesAsync();
                    await SendAsync(new { Message = "Ok" }, 201);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erreur :" + ex);
                    await SendAsync(new { Message = "Erreur lors de la mise à jour de l'animal" }, 500);
                }
            }
            else
            {
                await SendAsync(new { Message = "Animal introuvable" }, 404);
            }
        }
    }
}
