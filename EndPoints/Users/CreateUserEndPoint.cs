using FastEndpoints;
using FinalWorkshop.Database;
using FinalWorkshop.DTO.Requests.Users;
using FinalWorkshop.Model;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using FinalWorkshop.DTO.Responses.Users;


namespace FinalWorkshop.EndPoints.Users
{
    public class CreateUserEndPoint : Endpoint<CreateUserRequestDTO, CreateUserResponseDTO>
    {
        private readonly DatabaseContext _dbContext;

        public CreateUserEndPoint(DatabaseContext context)
        {
            _dbContext = context;
        }
        public override void Configure()
        {
            Post("/create/user");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CreateUserRequestDTO data, CancellationToken ct)
        {
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(data.Password);

            var doesThisUserAlreadyExist = await _dbContext.Users.AnyAsync(user => user.Mail == data.Mail);

            if (!doesThisUserAlreadyExist)
            {
                User user = new User
                {
                    Mail = data.Mail,
                    Password = hashedPassword
                };

                _dbContext.Users.Add(user);
                await _dbContext.SaveChangesAsync();

                CreateUserResponseDTO response = new CreateUserResponseDTO
                {
                    IsRegistered = true,
                };

                await SendAsync(response,201);
            }
            else
            {
                CreateUserResponseDTO response = new CreateUserResponseDTO
                {
                    IsRegistered = false,
                };

                await SendAsync(response,400);
            }
        }
    }
}
