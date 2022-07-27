using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Place.Application.Commands;
using Place.Application.Configuration.Commands;
using Place.Application.Enums;
using Place.Application.Exceptions;
using Place.Domain.Interface;
using Place.Domain.Models;
using Product.Application.Configuration.Mapper;

namespace Place.Application.Handlers.CommandHandler;

public class UserRegisterCommandHandler : ICommandHandler<UserRegisterCommand, CommandResult>
{
    private readonly IUserDal _repository;
    private readonly IPasswordHasher<User> _passwordHasher;

    public UserRegisterCommandHandler(
        IUserDal repository,
        IPasswordHasher<User> passwordHasher)
    {
        _passwordHasher = passwordHasher;
        _repository = repository;
    }

    public async Task<CommandResult> Handle(UserRegisterCommand request, CancellationToken cancellationToken)
    {
        var us = await _repository.GetByUserName(request.UserName);
        if (us != null)
           return new CommandResult(AppEnum.CommandResultStatus.Fail, "This UserName is already Token");

        //var result = ObjectMapper.Mapper.Map<Domain.Models.User>(request);
        var hashedPassword = _passwordHasher.HashPassword(null, request.Password);

        var result = new User(request.Name, request.UserName, hashedPassword, true, DateTime.Now);
        
        var user = await _repository.Insert(result);
        if (user <= 0)
            return new CommandResult(AppEnum.CommandResultStatus.Fail);

        return new CommandResult(AppEnum.CommandResultStatus.Success, user);
    }
}