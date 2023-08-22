namespace Domain.Entities
{
    public class Client: AuditableEntity
    {
        public string Name { get; set; }
        public Gender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public List<PhoneNumber> PhoneNumbers { get; set; }
        public string Address { get; set; }
        public List<Image> Images { get; set; }

        public int Age
        {
            get
            {
                var dateTime = DateTime.UtcNow;

                var age = dateTime.Year - DateOfBirth.Year;

                if (dateTime.Month < DateOfBirth.Month && dateTime.Day < DateOfBirth.Day)
                    --age;

                return age;
            }
        }
    }
}
