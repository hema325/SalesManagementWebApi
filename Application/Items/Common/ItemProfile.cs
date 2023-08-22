namespace Application.Items.Common
{
    internal class ItemProfile: Profile
    {
        public ItemProfile()
        {
            CreateMap<Item, ItemDTO>()
                .ForMember(dto => dto.Images, o => o.MapFrom(i => i.Images.Select(i => i.Path)))
                .ForMember(dto=>dto.Quantity,o=>o.MapFrom(i=>i.InvoiceItems.Where(itm=>itm.Invoice.Type == Domain.Enums.Invoices.Buying).Sum(i=>i.Quantity) +
                i.InvoiceItems.Where(itm => itm.Invoice.Type == Domain.Enums.Invoices.Returning).Sum(i => i.Quantity) -
                i.InvoiceItems.Where(itm => itm.Invoice.Type == Domain.Enums.Invoices.Selling).Sum(i => i.Quantity)));
        }
    }
}
