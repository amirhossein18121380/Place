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

namespace Place.Application.Handlers.CommandHandler;

public class UserLoginCommandHandler : ICommandHandler<UserLoginCommand, CommandResult>
{
    private readonly IUserDal _repository;
    private readonly IPasswordHasher<User> _passwordHasher;

    public UserLoginCommandHandler(
        IUserDal repository,
        IPasswordHasher<User> passwordHasher)
    {
        _passwordHasher = passwordHasher;
        _repository = repository;
    }

    public async Task<CommandResult> Handle(UserLoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _repository.GetByUserName(request.UserName);
        if (user == null)
            throw new WrongUsernameOrPasswordException();

        var passwordVerificationResult = _passwordHasher.VerifyHashedPassword(user,
            user.Password, request.Password.ToString());
        if (passwordVerificationResult == PasswordVerificationResult.Failed ||
            passwordVerificationResult == PasswordVerificationResult.SuccessRehashNeeded)
            throw new WrongUsernameOrPasswordException();
        return new CommandResult(AppEnum.CommandResultStatus.Success, user);
    }
}
