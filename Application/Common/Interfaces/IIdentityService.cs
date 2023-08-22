namespace Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task AddToRoleAsync(string id, Roles role);
        Task AddToRolesAsync(string id, IEnumerable<Roles> roles);
        Task ChangePasswordAsync(string id, string currentPassword, string newPassword);
        Task<bool> IsInRoleAsync(string id, Roles role);
        Task<bool> IsUserNameExsitsAsync(string userName);
    }
}
