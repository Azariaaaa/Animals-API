using FastEndpoints;
using FinalWorkshop.Database;
using Microsoft.EntityFrameworkCore;

namespace FinalWorkshop.EndPoints.Races
{
    public class GetAllRacesEndPoint : EndpointWithoutRequest
    {
        private readonly DatabaseContext _dbContext;

        public GetAllRacesEndPoint(DatabaseContext context)
        {
            _dbContext = context;
        }
        public override void Configure()
        {
            Post("/get/races");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            List<Race> allRaces = _dbContext.Races
                .Include(x => x.Animals)
                .ToList();

            if(allRaces != null)
            {
                await SendAsync(allRaces);
            }
            else
            {
                await SendAsync(new { Message = "Aucune race n'a été trouvé" }, 404);
            }
        }
    }
}
