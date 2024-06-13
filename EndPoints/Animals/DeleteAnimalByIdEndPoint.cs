using FastEndpoints;
using FinalWorkshop.Database;
using FinalWorkshop.DTO.Requests.Animals;
using FinalWorkshop.Model;

namespace FinalWorkshop.EndPoints.Animals
{
    public class DeleteAnimalByIdEndPoint : Endpoint<DeleteAnimalByIdRequestDTO>
    {
        private readonly DatabaseContext _dbContext;

        public DeleteAnimalByIdEndPoint(DatabaseContext context)
        {
            _dbContext = context;
        }
        public override void Configure()
        {
            Post("/delete/animal");
            AllowAnonymous();
        }

        public override async Task HandleAsync(DeleteAnimalByIdRequestDTO data, CancellationToken ct)
        {
            Animal? animalToDelete = _dbContext.Animals
                .Where(animal => animal.Id == data.Id)
                .FirstOrDefault(); 

            if (animalToDelete != null)
            {
                _dbContext.Animals.Remove(animalToDelete);
                await _dbContext.SaveChangesAsync();
                await SendAsync(new { Message = "Ok" }, 201);
            }

            await SendAsync("L'animal demandé n'éxiste pas.");
        }
    }
}
