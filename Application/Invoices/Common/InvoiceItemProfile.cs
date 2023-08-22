namespace Application.Invoices.Common
{
    internal class InvoiceItemProfile: Profile
    {
        public InvoiceItemProfile()
        {
            CreateMap<InvoiceItem, InvoiceItemDTO>();
        }
    }
}
