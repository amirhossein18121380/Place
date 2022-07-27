using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Place.Application.Commands.Place;
using Place.Application.Configuration.Commands;

namespace Place.Application.Commands.Place;

public class DeletePlaceCommand :  CommandBase<CommandResult>
{
    public DeletePlaceCommand(long id)
    {
        Id = id;
    }
    public long Id { get; set; }
}

