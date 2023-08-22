using Application.Clients.Commands.CreateClient;
using Application.Clients.Commands.DeleteClient;
using Application.Clients.Commands.UpdateClient;
using Application.Clients.Queries.GetClientById;
using Application.Clients.Queries.GetClientsInExcelFile;
using Application.Clients.Queries.GetClientsWithPagination;
using Application.Clients.Queries.GetDeletedClientsWithPagination;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("clients")]
    [HaveRoles(Roles.Admin, Roles.User)]
    public class ClientsController : ApiControllerBase
    {
        private readonly ISender _sender;

        public ClientsController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromForm] CreateClientCommand request)
        {
            var response = await _sender.Send(request);
            return Ok(response);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateClientCommand request)
        {
            if (id != request.Id)
                return BadRequest("id is not valid");

            await _sender.Send(request);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _sender.Send(new DeleteClientCommand(id));
            return NoContent();
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _sender.Send(new GetClientByIdQuery(id));
            return Ok(response);
        }

        [HttpGet("paginate")]
        public async Task<IActionResult> Paginate([FromQuery] GetClientsWithPaginationQuery reqeust)
        {
            var response = await _sender.Send(reqeust);
            return Ok(response);
        }

        [HttpGet("paginateDeleted")]
        [HaveRoles(Roles.Admin)]
        public async Task<IActionResult> PaginateDeleted([FromQuery] GetDeletedClientsWithPaginationQuery reqeust)
        {
            var response = await _sender.Send(reqeust);
            return Ok(response);
        }

        [HttpGet("DownloadExcel")]
        [HaveRoles(Roles.Admin)]
        public async Task<IActionResult> DownloadExcel()
        {
            var response = await _sender.Send(new GetClientsInExcelFileQuery());
            return File(response.Stream, response.ContentType, response.Name);
        }
    }
}
