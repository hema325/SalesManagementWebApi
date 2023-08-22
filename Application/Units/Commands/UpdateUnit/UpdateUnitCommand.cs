namespace Application.Units.Commands.UpdateUnit
{
    public record UpdateUnitCommand(int Id, string Name, string Notes):IRequest;
}
