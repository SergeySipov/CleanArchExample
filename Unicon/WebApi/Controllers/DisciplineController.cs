using Application.Features.Discipline.Commands.CreateDiscipline;
using Application.Features.Discipline.Commands.DeleteDiscipline;
using Application.Features.Discipline.Queries.GetDisciplineWithPagination;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

public class DisciplineController : ApiControllerBase
{
    private readonly IMediator _mediator;

    public DisciplineController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetPlayersWithPagination(GetDisciplinesWithPaginationQuery query)
    {
        var paginatedList = await _mediator.Send(query);
        return Ok(paginatedList);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateDisciplineCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var command = new DeleteDisciplineCommand { DisciplineId = id };
        await _mediator.Send(command);

        return Ok();
    }
}
