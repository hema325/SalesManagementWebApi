
namespace Application.Items.Queries.GetItemsReport
{
    internal class GetItemsReportQueryResponseProfile:Profile
    {
        public GetItemsReportQueryResponseProfile()
        {
            CreateMap<Item, GetItemsReportQueryResponse>()
                .ForMember(r => r.BuyingQuantity, o => o.MapFrom(i => i.InvoiceItems.Where(invc => invc.Invoice.Type == Domain.Enums.Invoices.Buying).Sum(invc => invc.Quantity)))
                .ForMember(r => r.SellingQuantity, o => o.MapFrom(i => i.InvoiceItems.Where(invc => invc.Invoice.Type == Domain.Enums.Invoices.Selling).Sum(invc => invc.Quantity)))
                .ForMember(r => r.BuyingPrices, o => o.MapFrom(i => i.InvoiceItems.Where(invc => invc.Invoice.Type == Domain.Enums.Invoices.Buying).Select(invc => invc.Price - invc.Price * invc.Invoice.Discount / 100)))
                .ForMember(r => r.SellingPrices, o => o.MapFrom(i => i.InvoiceItems.Where(invc => invc.Invoice.Type == Domain.Enums.Invoices.Selling).Select(invc => invc.Price - invc.Price * invc.Invoice.Discount / 100)))
                .ForMember(r => r.Profit, o => o.MapFrom(i => i.InvoiceItems.Where(invc => invc.Invoice.Type == Domain.Enums.Invoices.Selling).Select(invc => invc.Price - invc.Price * invc.Invoice.Discount / 100).Sum() -
                                                              i.InvoiceItems.Where(invc => invc.Invoice.Type == Domain.Enums.Invoices.Buying || invc.Invoice.Type == Domain.Enums.Invoices.Returning).Select(invc => invc.Price - invc.Price * invc.Invoice.Discount / 100).Sum()))
                .ForMember(r => r.Images, o => o.MapFrom(i => i.Images.Select(img => img.Path)));
        }
    }
}
