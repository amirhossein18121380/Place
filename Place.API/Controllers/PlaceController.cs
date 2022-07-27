using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Place.API.CustomAttribute;
using Place.API.ExtensionMethods;
using Place.Application.Commands.Place;
using Place.Application.Configuration.Commands;
using Place.Application.Configuration.Queries;
using Place.Application.Enums;
using Place.Application.Helper;
using Place.Application.Queries;
using Place.Domain.ViewModels;

namespace Place.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaceController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IConfiguration _configuration;
        public PlaceController(IMediator mediator,IConfiguration configuration)
        {
            _mediator = mediator;
            _configuration = configuration;
        }


        [HttpPost]
        [Route("GetPlaces")]
        [VIIAuthorize]
        public async Task<ActionResult<QueryResult>> GetPlaces(GetPlaceFilterViewModel model)
        {
            var result = await _mediator.Send(new GetPlacesQuery<GetPlaceFilterViewModel>(model));
            if (result.Status == AppEnum.QueryResultStatus.Fail)
                return new QueryResult(result.Message);
            return Ok(result.Data);
        }


        [HttpPut("EditPlace")]
        [VIIAuthorize]
        public async Task<IActionResult> EditPlace(EditPlaceViewModel request, CancellationToken token)
        {

            var result = await _mediator.Send(
                new UpdatePlaceCommand(
                    request.Title,
                    request.Address,
                    request.PlaceType,
                    request.Location,
                    HttpContext.GetUser().UserId,
                    request.Id
                ), token);
            return Ok(result.ApiResult);
        }

        [HttpPost]
        [Route("AddPlace")]
        [VIIAuthorize]
        public async Task<ActionResult<CommandResult>> AddPlace(AddPlaceViewModel request)
        {
            var result = await _mediator.Send(
                new AddPlaceCommand(
                    request.Title,
                    request.Address,
                    request.PlaceType,
                    request.Location,
                    //HttpContext.GetUser().UserId
                    1
                ));
            return Ok(result.ApiResult);
            //return new CommandResult(result.ApiResult);
        }


        [HttpDelete]
        [VIIAuthorize]
        public async Task<IActionResult> Delete(long id)
        {
            var result = await _mediator.Send(new DeletePlaceCommand(id));
            return Ok(result);
        }
    }


}
