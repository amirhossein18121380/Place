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
public class ReserveCommandHandler : ICommandHandler<ReserveCommand, CommandResult>
{
    private readonly IReservationDal _repository;
    public ReserveCommandHandler(IReservationDal repository)
    {
        _repository = repository;
    }
    public async Task<CommandResult> Handle(ReserveCommand request, CancellationToken cancellationToken)
    {
        //var result = ObjectMapper.Mapper.Map<Domain.Models.Place>(request);

        var result = new Domain.Models.Reservation(request.UserId, request.PlaceId, request.ReserveTime, DateTime.Now, request.Cost);

        var reserves = await _repository.GetAll();

        foreach (var x in reserves)
        {
            if (request.ReserveTime == x.ReserveTime)
            {
                return new CommandResult(AppEnum.CommandResultStatus.Fail, "this time is already reserved");
            }
        }

        var res = await _repository.Add(result);
        if (res <= 0)
            return new CommandResult(AppEnum.CommandResultStatus.Fail);
        return new CommandResult(AppEnum.CommandResultStatus.Success, "Added successfully.");
    }
}