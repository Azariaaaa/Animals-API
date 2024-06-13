namespace FinalWorkshop.DTO.Requests.Animals
{
    public class UpdateAnimalRequestDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int RaceId { get; set; }
    }
}
