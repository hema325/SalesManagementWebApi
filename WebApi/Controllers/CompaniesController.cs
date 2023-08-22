using Application.Companies.Commands.CreateCompany;
using Application.Companies.Commands.DeleteCompany;
using Application.Companies.Commands.UpdateCompany;
using Application.Companies.Queries.GetCompaniesInExcelFile;
using Application.Companies.Queries.GetCompaniesWithPagination;
using Application.Companies.Queries.GetCompanyById;
using Application.Companies.Queries.GetDeletedCompaniesWithPagination;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("companies")]
    [HaveRoles(Roles.Admin, Roles.User)]
    public class CompaniesController : ApiControllerBase
    {
        private readonly ISender _sender;

        public CompaniesController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] CreateCompanyCommand request)
        {
            var response = await _sender.Send(request);
            return Ok(response);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCompanyCommand request)
        {
            if (id != request.Id)
                return BadRequest("id is not valid");

            await _sender.Send(request);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _sender.Send(new DeleteCompanyCommand(id));
            return NoContent();
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _sender.Send(new GetCompanyByIdQuery(id));
            return Ok(response);
        }

        [HttpGet("paginate")]
        public async Task<IActionResult> Paginate([FromQuery] GetCompaniesWithPaginationQuery reqeust)
        {
            var response = await _sender.Send(reqeust);
            return Ok(response);
        }

        [HttpGet("paginateDeleted")]
        [HaveRoles(Roles.Admin)]
        public async Task<IActionResult> PaginateDeleted([FromQuery] GetDeletedCompaniesWithPaginationQuery reqeust)
        {
            var response = await _sender.Send(reqeust);
            return Ok(response);
        }

        [HttpGet("DownloadExcel")]
        [HaveRoles(Roles.Admin)]
        public async Task<IActionResult> DownloadExcel()
        {
            var response = await _sender.Send(new GetCompaniesInExcelFileQuery());
            return File(response.Stream, response.ContentType, response.Name);
        }
    }
}
