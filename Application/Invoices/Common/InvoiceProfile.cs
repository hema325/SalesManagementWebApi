namespace Application.Invoices.Common
{
    internal class InvoiceProfile: Profile
    {
        public InvoiceProfile()
        {
            CreateMap<Invoice, InvoiceDTO>()
                .ForMember(dto => dto.Type, o => o.MapFrom(i => i.Type.ToString()));
        }
    }
}
