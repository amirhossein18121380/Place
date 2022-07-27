using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Place.Application.Commands.Place;
using Place.Application.Configuration.Commands;
using Place.Application.Enums;
using Place.Domain.Interface;

namespace Place.Application.Handlers.CommandHandler.Place;

public class DeletePlaceCommandHandler : ICommandHandler<DeletePlaceCommand, CommandResult>
{
    private readonly IPlaceDal _repository;
    public DeletePlaceCommandHandler(IPlaceDal repository)
    {
        _repository = repository;
    }
    public async Task<CommandResult> Handle(DeletePlaceCommand request, CancellationToken cancellationToken)
    {

        var res = await _repository.Delete(request.Id);
        if (res == false)
            return new CommandResult(AppEnum.CommandResultStatus.Fail);
        return new CommandResult(AppEnum.CommandResultStatus.Success, "Delete News was successful.");
    }
}