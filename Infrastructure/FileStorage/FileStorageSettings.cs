namespace Infrastructure.FileStorage
{
    internal class FileStorageSettings
    {
        public const string SectionName = "FileStorage";

        public string RootPath { get; init; }
    }
}
