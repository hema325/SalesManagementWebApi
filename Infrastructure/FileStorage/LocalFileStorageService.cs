using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Infrastructure.FileStorage
{
    internal class LocalFileStorageService: IFileStorage
    {
        private readonly FileStorageSettings _fileSttings;

        public LocalFileStorageService(IOptions<FileStorageSettings> fileOptions)
        {
            _fileSttings = fileOptions.Value;
        }

        public async Task<string> SaveAsync(IFormFile file)
        {
            var newFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var fileType = Capitalize(file.ContentType.Substring(0, file.ContentType.IndexOf('/')) + 's');

            var directoryPath = Path.Combine(_fileSttings.RootPath, fileType);
            var absoluteDirectoryPath = Path.Combine(Directory.GetCurrentDirectory(), directoryPath);

            if (!Directory.Exists(absoluteDirectoryPath))
                Directory.CreateDirectory(absoluteDirectoryPath);

            var filePath = Path.Combine(directoryPath, newFileName);
            var absoluteFilePath = Path.Combine(Directory.GetCurrentDirectory(), filePath);

            using var fileStram = File.Create(absoluteFilePath);
            await file.CopyToAsync(fileStram);

            return filePath;
        }
        
        public void Delete(string relativePath)
        {
            var absolutePath = Path.Combine(Directory.GetCurrentDirectory(), relativePath);
            File.Delete(absolutePath);
        }

        private string Capitalize(string value)
        {
            var words = value.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var capitalizedWords = words.Select(words => char.ToUpper(words[0]) + words.Substring(1).ToLower());

            return string.Join(string.Empty, capitalizedWords);
        }

    }
}
