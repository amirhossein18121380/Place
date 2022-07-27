using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Place.API.ExtensionMethods;
using Place.Application.Commands;
using Place.Application.Configuration.Commands;
using Place.Application.Dto;
using Place.Application.Enums;
using Place.Application.Exceptions;
using Place.Application.Identities.Interface;
using Place.Domain.Interface;
using Place.Domain.Models;
using Place.Domain.ViewModels;

namespace Place.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : BaseController
{
    private readonly IJWTHelper _jwtHelper;
    private readonly IMediator _mediator;

    public AccountController(IMediator mediator, IJWTHelper jwtHelper)
    {
        _jwtHelper = jwtHelper;
        _mediator = mediator;
    }


    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(UserLoginViewModel request, CancellationToken token)
    {
        var result = await _mediator.Send(new UserLoginCommand(request.UserName, request.Password),
            token);
        if (result.Status == AppEnum.CommandResultStatus.Success)
        {
            var jwtSecurityToken = _jwtHelper.CreateToken(result.Data.CastTo<User>(), "K%PTdadededsad##=GpX*BsAFXjxdQNsSWgTNFU&pdz4KK+T");
          

            var userDto = result.Data.CastTo<User>().AsDto();
            var userProfile = new UserProfileDto()
            {
                Token = jwtSecurityToken
            };

            var finalResult = new CommandResult(AppEnum.CommandResultStatus.Success, userProfile);

           
            return Ok(finalResult.ApiResult);
        }
        throw new WrongUsernameOrPasswordException();
    }


    [HttpPost("Register")]
    [AllowAnonymous]
    public async Task<ActionResult<CommandResult>> Register(RegisterViewModel request, CancellationToken token)
    {
        var result = await _mediator.Send(new UserRegisterCommand(request.Name,request.UserName, request.Password),
            token);
        if (result.Status == AppEnum.CommandResultStatus.Success)
        {
            var finalResult = new CommandResult(AppEnum.CommandResultStatus.Success, result);
            return Ok(finalResult.ApiResult);
        }
        return new CommandResult(AppEnum.CommandResultStatus.Success, result);
    }

}

