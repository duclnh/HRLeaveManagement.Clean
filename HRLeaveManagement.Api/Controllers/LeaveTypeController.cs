using HRLeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;
using HRLeaveManagement.Application.Features.LeaveType.Commands.DeleteLeaveType;
using HRLeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType;
using HRLeaveManagement.Application.Features.LeaveType.Queries;
using HRLeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveType;
using HRLeaveManagement.Application.Features.LeaveType.Queries.GetLeaveTypeDetails;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HRLeaveManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveTypeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LeaveTypeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<LeaveTypeDTO>>> Get(CancellationToken cancellationToken = default)
        {
            var leaveTypes = await _mediator.Send(new GetLeaveTypeQuery(), cancellationToken);

            return Ok(leaveTypes);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<LeaveTypeDTO>> Get(int id, CancellationToken cancellationToken = default)
        {
            var leaveType = await _mediator.Send(new GetLeaveTypeDetailsQuery(id), cancellationToken);

            return Ok(leaveType);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Post([FromBody] CreateLeaveTypeCommand command, CancellationToken cancellationToken = default)
        {
            var response = await _mediator.Send(command, cancellationToken);

            return CreatedAtAction(nameof(Get), new { id = response }, response);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Put([FromBody] UpdateLeaveTypeCommand command, CancellationToken cancellationToken = default)
        {
            await _mediator.Send(command, cancellationToken);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Delete(int id, CancellationToken cancellationToken = default)
        {
            await _mediator.Send(new DeleteLeaveTypeCommand { Id = id}, cancellationToken);
            return NoContent();
        }
    }
}
