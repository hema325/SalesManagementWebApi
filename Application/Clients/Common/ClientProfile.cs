namespace Application.Clients.Common
{
    internal class ClientProfile: Profile
    {
        public ClientProfile()
        {
            CreateMap<Client, ClientDTO>()
                .ForMember(dto => dto.Gender, o => o.MapFrom(c => c.Gender.ToString()))
                .ForMember(dto => dto.PhoneNumbers, o => o.MapFrom(c => c.PhoneNumbers.Select(ph => ph.Value)))
                .ForMember(dto => dto.Images, o => o.MapFrom(c => c.Images.Select(i => i.Path)));
        }
    }
}
