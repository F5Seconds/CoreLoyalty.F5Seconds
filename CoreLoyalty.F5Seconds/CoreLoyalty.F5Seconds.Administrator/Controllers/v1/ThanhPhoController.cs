using System.Threading.Tasks;
using CoreLoyalty.F5Seconds.Application.Features.CoreLoyalty.DiaChis.ThanhPhos.Commands.CreateThanhPho;
using CoreLoyalty.F5Seconds.Application.Features.CoreLoyalty.DiaChis.ThanhPhos.Commands.DeleteThanhPho;
using CoreLoyalty.F5Seconds.Application.Features.CoreLoyalty.DiaChis.ThanhPhos.Commands.GetAllThanhPhos;
using CoreLoyalty.F5Seconds.Application.Features.CoreLoyalty.DiaChis.ThanhPhos.Commands.GetThanhPhoById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VietCapital.Partner.F5Seconds.Application.Features.ThanhPhos.Queries.GetAllThanhPhos;

namespace CoreLoyalty.F5Seconds.Administrator.Controllers.v1
{
    [ApiVersion("1.0")]
    // [Authorize]
    public class ThanhPhoController : BaseApiController
    {
        // GET: api/<controller>
        [HttpGet]
        [AllowAnonymous]

        public async Task<IActionResult> Get([FromQuery] GetAllThanhPhosParameter filter)
        {

            return Ok(await Mediator.Send(new GetAllThanhPhosQuery() { PageSize = filter.PageSize, PageNumber = filter.PageNumber ,Search = filter.Search}));
        }
        // GET api/<controller>/5
        [HttpGet("{id}")]
        [AllowAnonymous]

        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetThanhPhoByIdQuery { Id = id }));
        }
        // POST api/<controller>
        [HttpPost]
        // [Authorize]
        [AllowAnonymous]

        public async Task<IActionResult> Post(CreateThanhPhoCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
        // PUT api/<controller>/5
        [HttpPut("{id}")]
        // [Authorize]
        [AllowAnonymous]

        public async Task<IActionResult> Put(int id, UpdateThanhPhoCommand command)
        {
           if (id != command.Id)
           {
               return BadRequest();
           }
           return Ok(await Mediator.Send(command));
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        // [Authorize]
        [AllowAnonymous]

        public async Task<IActionResult> Delete(int id)
        {
           return Ok(await Mediator.Send(new DeleteThanhPhoByIdCommand { Id = id }));
        }
    }
}
