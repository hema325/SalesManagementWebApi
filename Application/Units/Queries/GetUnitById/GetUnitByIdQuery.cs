using Application.Units.Common;

namespace Application.Units.Queries.GetUnitById
{
    public record GetUnitByIdQuery(int Id): IRequest<UnitDTO>;
}
