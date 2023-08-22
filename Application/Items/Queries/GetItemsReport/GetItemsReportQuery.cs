namespace Application.Items.Queries.GetItemsReport
{
    public record GetItemsReportQuery(DateTime From, DateTime To): IRequest<List<GetItemsReportQueryResponse>>;
}
