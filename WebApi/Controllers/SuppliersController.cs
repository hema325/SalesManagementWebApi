using Application.Suppliers.Commands.CreateSupplier;
using Application.Suppliers.Commands.DeleteSupplier;
using Application.Suppliers.Commands.UpdateSupplier;
using Application.Suppliers.Queries.GetDeletedSuppliersWithPagination;
using Application.Suppliers.Queries.GetSupplierById;
using Application.Suppliers.Queries.GetSuppliersInExcelFile;
using Application.Suppliers.Queries.GetSuppliersWithPagination;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("suppliers")]
    [HaveRoles(Roles.Admin)]
    public class SuppliersController : ApiControllerBase
    {
        private readonly ISender _sender;

        public SuppliersController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] CreateSupplierCommand request)
        {
            var response = await _sender.Send(request);
            return Ok(response);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateSupplierCommand request)
        {
            if (id != request.Id)
                return BadRequest("id is not valid");

            await _sender.Send(request);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _sender.Send(new DeleteSupplierCommand(id));
            return NoContent();
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _sender.Send(new GetSupplierByIdQuery(id));
            return Ok(response);
        }

        [HttpGet("paginate")]
        public async Task<IActionResult> Paginate([FromQuery] GetSuppliersWithPaginationQuery reqeust)
        {
            var response = await _sender.Send(reqeust);
            return Ok(response);
        }

        [HttpGet("paginateDeleted")]
        public async Task<IActionResult> PaginateDeleted([FromQuery] GetDeletedSuppliersWithPaginationQuery reqeust)
        {
            var response = await _sender.Send(reqeust);
            return Ok(response);
        }

        [HttpGet("DownloadExcel")]
        public async Task<IActionResult> DownloadExcel()
        {
            var response = await _sender.Send(new GetSuppliersInExcelFileQuery());
            return File(response.Stream, response.ContentType, response.Name);
        }
    }
}
