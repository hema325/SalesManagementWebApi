namespace Application.Common.Interfaces
{
    public interface ICurrentUser
    {
        string Id { get; }
        string UserName { get; }
    }
}
