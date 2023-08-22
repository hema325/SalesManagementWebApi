using ClosedXML.Excel;
using System.Data;

namespace Infrastructure.Common.Services
{
    internal class FileBuilder: IFileBuilder
    {
        public Stream BuildExcelFile<TEntity>(IEnumerable<TEntity> data)
        {
            var dataProperities = typeof(TEntity).GetProperties();

            var dataTable = new DataTable(typeof(TEntity).Name);
            dataTable.Columns.AddRange(dataProperities.Select(p => new DataColumn(p.Name)).ToArray());

            foreach (var item in data)
            {
                var dataRow = dataTable.NewRow();
                Array.ForEach(dataProperities, prop => dataRow[prop.Name] = prop.GetValue(item));
                dataTable.Rows.Add(dataRow);
            }

            using var workbook = new XLWorkbook();
            workbook.Worksheets.Add(dataTable);

            var stream = new MemoryStream();
            workbook.SaveAs(stream);

            stream.Seek(0, SeekOrigin.Begin);
            return stream;
        }
    }
}
