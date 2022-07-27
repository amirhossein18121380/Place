using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Place.Application.Commands.Place;
using Place.Application.Configuration.Commands;
using Place.Application.Enums;
using Place.Domain.Interface;
using Product.Application.Configuration.Mapper;

namespace Place.Application.Handlers.CommandHandler.Place;

public class UpdatePlaceCommandHandler : ICommandHandler<UpdatePlaceCommand, CommandResult>
{
    private readonly IPlaceDal _repository;
    public UpdatePlaceCommandHandler(IPlaceDal repository)
    {
        _repository = repository;
    }
    public async Task<CommandResult> Handle(UpdatePlaceCommand request, CancellationToken cancellationToken)
    {
        //var result = ObjectMapper.Mapper.Map<Domain.Models.Place>(request);

        var result = new Domain.Models.Place(request.Title, request.Address, request.PlaceType, request.Location,
            DateTime.Now, request.UserId);
        result.Id = request.Id;
        var res = await _repository.Update(result);
        if (res <= 0)
            return new CommandResult(AppEnum.CommandResultStatus.Fail);
        return new CommandResult(AppEnum.CommandResultStatus.Success, "Updated was successful.");
    }
}