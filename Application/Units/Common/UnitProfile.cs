namespace Application.Units.Common
{
    internal class UnitProfile: Profile
    {
        public UnitProfile()
        {
            CreateMap<ItemUnit, UnitDTO>();
        }
    }
}
