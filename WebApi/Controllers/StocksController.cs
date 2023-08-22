using Application.Stocks.Commands.CreateStock;
using Application.Stocks.Commands.DeleteStock;
using Application.Stocks.Commands.UpdateStock;
using Application.Stocks.Queries.GetDeletedStocksWithPagination;
using Application.Stocks.Queries.GetStockById;
using Application.Stocks.Queries.GetStocksInExcelFile;
using Application.Stocks.Queries.GetStocksWithPagination;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("stocks")]
    [HaveRoles(Roles.Admin)]
    public class StocksController : ApiControllerBase
    {
        private readonly ISender _sender;

        public StocksController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] CreateStockCommand request)
        {
            var response = await _sender.Send(request);
            return Ok(response);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateStockCommand request)
        {
            if (id != request.Id)
                return BadRequest("id is not valid");

            await _sender.Send(request);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _sender.Send(new DeleteStockCommand(id));
            return NoContent();
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _sender.Send(new GetStockByIdQuery(id));
            return Ok(response);
        }

        [HttpGet("paginate")]
        public async Task<IActionResult> Paginate([FromQuery] GetStocksWithPaginationQuery reqeust)
        {
            var response = await _sender.Send(reqeust);
            return Ok(response);
        }

        [HttpGet("paginateDeleted")]
        public async Task<IActionResult> PaginateDeleted([FromQuery] GetDeletedStocksWithPaginationQuery reqeust)
        {
            var response = await _sender.Send(reqeust);
            return Ok(response);
        }

        [HttpGet("DownloadExcel")]
        public async Task<IActionResult> DownloadExcel()
        {
            var response = await _sender.Send(new GetStocksInExcelFileQuery());
            return File(response.Stream, response.ContentType, response.Name);
        }
    }
}
