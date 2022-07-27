using MediatR;
using Microsoft.AspNetCore.Mvc;
using Place.API.CustomAttribute;
using Place.API.ExtensionMethods;
using Place.Application.Commands.Place;
using Place.Application.Handlers.CommandHandler.Place;
using Place.Domain.ViewModels;

namespace Place.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReserveController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IConfiguration _configuration;
        public ReserveController(IMediator mediator, IConfiguration configuration)
        {
            _mediator = mediator;
            _configuration = configuration;
        }

        [HttpPost]
        [VIIAuthorize]
        [Route("Reserve")]
        public async Task<IActionResult> Reserve(ReserveViewModel request )
        {
            var result = await _mediator.Send(
                new ReserveCommand(
                    HttpContext.GetUser().UserId,
                    request.PlaceId,
                    request.ReserveTime,
                    request.Cost
                ));
            return Ok(result);
        }
    }
}
