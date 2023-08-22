namespace Application.Suppliers.Common
{
    internal class SupplierProfile: Profile
    {
        public SupplierProfile()
        {
            CreateMap<Supplier, SupplierDTO>()
                .ForMember(dto => dto.PhoneNumbers, o => o.MapFrom(s => s.PhoneNumbers.Select(ph => ph.Value)));
        }
    }
}
