using MediatR;
using Microsoft.AspNetCore.Mvc;
using Redarbor.Application.Employees.Commands;
using Redarbor.Application.Employees.Queries;
using Redarbor.Domain;

namespace Redarbor.Api.Controllers;

[ApiController]
[Route("api/[controller]")] // Genera la ruta /api/redarbor
public class RedarborController : ControllerBase
{
    private readonly IMediator _mediator;

    public RedarborController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // GET /api/redarbor
    [HttpGet]
    public async Task<ActionResult<List<Employee>>> GetAll()
    {
        var result = await _mediator.Send(new GetEmployeesQuery());
        return Ok(result);
    }

    // GET /api/redarbor/{id}
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Employee>> GetById(int id)
    {
        var result = await _mediator.Send(new GetEmployeeByIdQuery(id));
        if (result == null)
            return NotFound();

        return Ok(result);
    }

    // POST /api/redarbor
    [HttpPost]
    public async Task<ActionResult<Employee>> Create([FromBody] CreateEmployeeCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    // PUT /api/redarbor/{id}
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateEmployeeCommand command)
    {
        if (id != command.Id)
            return BadRequest("El ID proporcionado en la URL no coincide con el cuerpo del request.");

        var success = await _mediator.Send(command);
        if (!success)
            return NotFound();

        return NoContent(); // Retorna 204 según los requerimientos de la prueba
    }

    // DELETE /api/redarbor/{id}
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _mediator.Send(new DeleteEmployeeCommand(id));
        if (!success)
            return NotFound();

        return NoContent(); // Retorna 204 según los requerimientos de la prueba
    }
}