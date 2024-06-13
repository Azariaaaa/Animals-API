using FastEndpoints;
using FinalWorkshop.Database;
using FinalWorkshop.DTO.Requests.Animals;
using FinalWorkshop.DTO.Responses.Animals;
using Microsoft.EntityFrameworkCore;

namespace FinalWorkshop.EndPoints.Animals
{
    public class GetAnimalByNameEndPoint : Endpoint<GetAnimalByNameRequestDTO, GetAnimalByNameResponseDTO>
    {
        private readonly DatabaseContext _dbContext;

        public GetAnimalByNameEndPoint(DatabaseContext context)
        {
            _dbContext = context;
        }
        public override void Configure()
        {
            Post("/get/animals/name");
            AllowAnonymous();
        }

        public override async Task HandleAsync(GetAnimalByNameRequestDTO data, CancellationToken ct)
        {
            Animal? animal = _dbContext.Animals
                .Where(animal => animal.Name == data.Name)
                .Include(animal => animal.Race)
                .FirstOrDefault();

            GetAnimalByNameResponseDTO response = new GetAnimalByNameResponseDTO
            {
                Animal = animal
            };

            await SendAsync(response);
        }
    }
}
