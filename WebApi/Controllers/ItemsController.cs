using Application.Items.Commands.CreateItem;
using Application.Items.Commands.DeleteItem;
using Application.Items.Commands.UpdateItem;
using Application.Items.Queries.GetDeletedItemsWithPagination;
using Application.Items.Queries.GetItemById;
using Application.Items.Queries.GetItemsInExcelFile;
using Application.Items.Queries.GetItemsReport;
using Application.Items.Queries.GetItemsWithPagination;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("items")]
    [HaveRoles(Roles.Admin, Roles.User)]
    public class ItemsController : ApiControllerBase
    {
        private readonly ISender _sender;

        public ItemsController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromForm] CreateItemCommand request)
        {
            var response = await _sender.Send(request);
            return Ok(response);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateItemCommand request)
        {
            if (id != request.Id)
                return BadRequest("id is not valid");

            await _sender.Send(request);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _sender.Send(new DeleteItemCommand(id));
            return NoContent();
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _sender.Send(new GetItemByIdQuery(id));
            return Ok(response);
        }

        [HttpGet("paginate")]
        public async Task<IActionResult> Paginate([FromQuery] GetItemsWithPaginationQuery reqeust)
        {
            var response = await _sender.Send(reqeust);
            return Ok(response);
        }

        [HttpGet("paginateDeleted")]
        [HaveRoles(Roles.Admin)]
        public async Task<IActionResult> PaginateDeleted([FromQuery] GetDeletedItemsWithPaginationQuery reqeust)
        {
            var response = await _sender.Send(reqeust);
            return Ok(response);
        }

        [HttpGet("DownloadExcel")]
        [HaveRoles(Roles.Admin)]
        public async Task<IActionResult> DownloadExcel()
        {
            var response = await _sender.Send(new GetItemsInExcelFileQuery());
            return File(response.Stream, response.ContentType, response.Name);
        }

        [HttpGet("Report")]
        [HaveRoles(Roles.Admin)]
        public async Task<IActionResult> Report([FromQuery] GetItemsReportQuery request)
        {
            var response = await _sender.Send(request);
            return Ok(response);
        }
    }
}
