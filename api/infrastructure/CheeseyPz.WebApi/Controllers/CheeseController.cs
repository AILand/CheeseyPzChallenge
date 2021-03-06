using Application.UseCases.Cheeses.Commands;
using Application.UseCases.Cheeses.Queries;
using CheeseyPz.WebApi.Core;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;


namespace CheeseyPz.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheeseController : ControllerBase
    {
        private readonly IApplicationUseCaseHandler handler;

        public CheeseController(IApplicationUseCaseHandler handler)
        {
            this.handler=handler;
        }

        [HttpGet]
        public async Task<ActionResult> Get(CancellationToken cancellationToken)
        {
            return Ok(await this.handler.HandleAsync(new GetCheesesListQuery(), cancellationToken));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(Guid id, CancellationToken cancellationToken)
        {
            return Ok(await handler.HandleAsync(new GetCheeseQuery { Id = id }, cancellationToken));
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromForm] UpsertCheeseCommand command, CancellationToken cancellationToken)
        {
            return Ok(await handler.HandleAsync(command, cancellationToken));
        }

        [HttpPut]
        public async Task<ActionResult> Upsert([FromBody] UpsertCheeseCommand command, CancellationToken cancellationToken)
        {
            return Ok(await handler.HandleAsync(command, cancellationToken));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            return Ok(await handler.HandleAsync(new DeleteCheeseCommand { Id = id }, cancellationToken));
        }
    }
}
