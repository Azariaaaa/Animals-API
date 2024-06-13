using System.Net.Mail;

namespace FinalWorkshop.DTO.Requests.Users
{
    public class ConnectUserRequestDTO
    {
        public string Mail {  get; set; }
        public string Password { get; set; }
    }
}
