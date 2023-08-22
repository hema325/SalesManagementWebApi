namespace Application.Common.Profiles
{
    internal class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap(typeof(PaginatedList<>), typeof(PaginatedList<>));
        }
    }
}
