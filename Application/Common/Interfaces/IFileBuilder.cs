namespace Application.Common.Interfaces
{
    public interface IFileBuilder
    {
        Stream BuildExcelFile<TEntity>(IEnumerable<TEntity> data);
    }
}
