using FastEndpoints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalWorkshop.DTO.Responses.Animals;
using FinalWorkshop.Model;
using FinalWorkshop.Database;
using Microsoft.EntityFrameworkCore;

namespace FinalWorkshop.EndPoints.Animals
{
    public class GetAllAnimalsEndPoint : EndpointWithoutRequest<List<Animal>>   
    {
        private readonly DatabaseContext _dbContext;

        public GetAllAnimalsEndPoint(DatabaseContext context)
        {
            _dbContext = context;
        }
        public override void Configure()
        {
            Get("/get/animals");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var animals = _dbContext.Animals.ToList();
            await SendAsync(animals);
        }
    }
}
