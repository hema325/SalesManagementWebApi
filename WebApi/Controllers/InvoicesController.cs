using Application.Invoices.Commands.CreateInvoice;
using Application.Invoices.Commands.DeleteInvoice;
using Application.Invoices.Commands.UpdateInvoice;
using Application.Invoices.Queries.GetDeletedInvoicesInExcelFile;
using Application.Invoices.Queries.GetDeletedInvoicesWithPagination;
using Application.Invoices.Queries.GetInvoiceById;
using Application.Invoices.Queries.GetInvoicesWithPagination;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("invoices")]
    [HaveRoles(Roles.Admin, Roles.User)]
    public class InvoicesController : ApiControllerBase
    {
        private readonly ISender _sender;

        public InvoicesController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] CreateInvoiceCommand request)
        {
            var response = await _sender.Send(request);
            return Ok(response);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateInvoiceCommand request)
        {
            if (id != request.Id)
                return BadRequest("id is not valid");

            await _sender.Send(request);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _sender.Send(new DeleteInvoiceCommand(id));
            return NoContent();
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _sender.Send(new GetInvoiceByIdQuery(id));
            return Ok(response);
        }

        [HttpGet("paginate")]
        [HaveRoles(Roles.Admin)]
        public async Task<IActionResult> Paginate([FromQuery] GetInvoicesWithPaginationQuery reqeust)
        {
            var response = await _sender.Send(reqeust);
            return Ok(response);
        }

        [HttpGet("paginateDeleted")]
        [HaveRoles(Roles.Admin)]
        public async Task<IActionResult> PaginateDeleted([FromQuery] GetDeletedInvoicesWithPaginationQuery reqeust)
        {
            var response = await _sender.Send(reqeust);
            return Ok(response);
        }

        [HttpGet("DownloadExcel")]
        [HaveRoles(Roles.Admin)]
        public async Task<IActionResult> DownloadExcel()
        {
            var response = await _sender.Send(new GetInvoicesInExcelFileQuery());
            return File(response.Stream, response.ContentType, response.Name);
        }

    }
}
