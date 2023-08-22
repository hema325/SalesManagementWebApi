using Microsoft.AspNetCore.Http;

namespace Application.Common.Interfaces
{
    public interface IFileStorage
    {
        void Delete(string relativePath);
        Task<string> SaveAsync(IFormFile file);
    }
}
