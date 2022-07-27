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
public class AddPlaceCommandHandler : ICommandHandler<AddPlaceCommand, CommandResult>
{
    private readonly IPlaceDal _repository;
    public AddPlaceCommandHandler(IPlaceDal repository)
    {
        _repository = repository;
    }
    public async Task<CommandResult> Handle(AddPlaceCommand request, CancellationToken cancellationToken)
    {
        //var result = ObjectMapper.Mapper.Map<Domain.Models.Place>(request);

        var result = new Domain.Models.Place(request.Title, request.Address, request.PlaceType, request.Location,
            DateTime.Now, request.UserId);

        var res = await _repository.Insert(result);
        if (res <= 0)
            return new CommandResult(AppEnum.CommandResultStatus.Fail);
        return new CommandResult(AppEnum.CommandResultStatus.Success, "Added successfully.");
    }
}
