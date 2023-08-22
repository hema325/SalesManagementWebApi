namespace Application.Units.Commands.CreateUnit
{
    public record CreateUnitCommand(string Name, string Notes): IRequest<int>;
}
