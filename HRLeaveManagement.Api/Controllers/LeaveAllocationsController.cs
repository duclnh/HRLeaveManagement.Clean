using HRLeaveManagement.Application.Features.LeaveAllocation;
using HRLeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;
using HRLeaveManagement.Application.Features.LeaveAllocation.Commands.DeleteLeaveAllocation;
using HRLeaveManagement.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation;
using HRLeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocation;
using HRLeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocationDetails;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HRLeaveManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveAllocationsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LeaveAllocationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<LeaveAllocationDTO>>> Get(bool isLoggingUser = false, CancellationToken cancellationToken = default)
        {
            var leaveAllocations = await _mediator.Send(new GetAllLeaveAllocationQuery(), cancellationToken);

            return Ok(leaveAllocations);            
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<LeaveAllocationDTO>>> Get(int id, CancellationToken cancellationToken = default)
        {
            var leaveAllocation = await _mediator.Send(new GetLeaveAllocationDetailsQuery { Id = id}, cancellationToken);

            return Ok(leaveAllocation);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<LeaveAllocationDTO>>> Add(CreateLeaveAllocationCommand command , CancellationToken cancellationToken = default)
        {
            var response = await _mediator.Send(command, cancellationToken);

            return CreatedAtAction(nameof(Get), new {id = response.Id}, response);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Update(UpdateLeaveAllocationCommand command, CancellationToken cancellationToken = default)
        {
            await _mediator.Send(command, cancellationToken);

            return NoContent();
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Delete(int id, CancellationToken cancellationToken = default)
        {
            await _mediator.Send(new DeleteLeaveAllocationCommand { Id = id}, cancellationToken);

            return NoContent();
        }
    }
}
