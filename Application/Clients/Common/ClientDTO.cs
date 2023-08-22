namespace Application.Clients.Common
{
    public class ClientDTO
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string Gender { get; init; }
        public DateTime DateOfBirth { get; init; }
        public List<string> PhoneNumbers { get; init; }
        public string Address { get; init; }
        public List<string> Images { get; init; }
        public int Age { get; init; }
        public DateTime CreatedOn { get; init; }
    }
}
