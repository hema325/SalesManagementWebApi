using Application.Units.Commands.CreateUnit;
using Application.Units.Commands.DeleteUnit;
using Application.Units.Commands.UpdateUnit;
using Application.Units.Queries.GetDeletedUnitsWithPagination;
using Application.Units.Queries.GetUnitById;
using Application.Units.Queries.GetUnitsInExcelFile;
using Application.Units.Queries.GetUnitsWithPagination;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("units")]
    [HaveRoles(Roles.Admin,Roles.User)]
    public class UnitsController : ApiControllerBase
    {
        private readonly ISender _sender;

        public UnitsController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] CreateUnitCommand request)
        {
            var response = await _sender.Send(request);
            return Ok(response);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateUnitCommand request)
        {
            if (id != request.Id)
                return BadRequest("id is not valid");

            await _sender.Send(request);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _sender.Send(new DeleteUnitCommand(id));
            return NoContent();
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _sender.Send(new GetUnitByIdQuery(id));
            return Ok(response);
        }

        [HttpGet("paginate")]
        public async Task<IActionResult> Paginate([FromQuery] GetUnitsWithPaginationQuery reqeust)
        {
            var response = await _sender.Send(reqeust);
            return Ok(response);
        }

        [HttpGet("paginateDeleted")]
        [HaveRoles(Roles.Admin)]
        public async Task<IActionResult> PaginateDeleted([FromQuery] GetDeletedUnitsWithPaginationQuery reqeust)
        {
            var response = await _sender.Send(reqeust);
            return Ok(response);
        }

        [HttpGet("DownloadExcel")]
        [HaveRoles(Roles.Admin)]
        public async Task<IActionResult> DownloadExcel()
        {
            var response = await _sender.Send(new GetUnitsInExcelFileQuery());
            return File(response.Stream, response.ContentType, response.Name);
        }
    }
}
