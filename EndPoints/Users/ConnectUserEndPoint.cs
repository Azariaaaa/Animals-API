using FastEndpoints;
using FinalWorkshop.Database;
using FinalWorkshop.DTO.Requests.Users;
using FinalWorkshop.DTO.Responses.Users;

namespace FinalWorkshop.EndPoints.Users
{
    public class ConnectUserEndPoint : Endpoint<ConnectUserRequestDTO, ConnectUserResponseDTO>
    {
        private readonly DatabaseContext _dbContext;

        public ConnectUserEndPoint(DatabaseContext context)
        {
            _dbContext = context;
        }
        public override void Configure()
        {
            Post("/connect");
            AllowAnonymous();
        }

        public override async Task HandleAsync(ConnectUserRequestDTO data, CancellationToken ct)
        {
            User user = _dbContext.Users.Where(x => x.Mail == data.Mail).FirstOrDefault();
            bool isPasswordCorrect = BCrypt.Net.BCrypt.Verify(data.Password, user.Password);

            if (!isPasswordCorrect)
            {
                ConnectUserResponseDTO response = new ConnectUserResponseDTO
                {
                    IsConnected = false
                };
                await SendAsync(response);
            }
            else
            {
                ConnectUserResponseDTO response = new ConnectUserResponseDTO
                {
                    IsConnected = true
                };
                await SendAsync(response);
            }
        }
    }
}
