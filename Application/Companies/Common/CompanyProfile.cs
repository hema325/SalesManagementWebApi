namespace Application.Companies.Common
{
    internal class CompanyProfile: Profile
    {
        public CompanyProfile()
        {
            CreateMap<Company, CompanyDTO>();
        }
    }
}
