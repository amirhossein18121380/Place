using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Place.Application.Configuration.Commands;
using Place.Application.Configuration.Queries;

namespace Place.Application.Commands.Place
{
    public class ReserveCommand : CommandBase<CommandResult>
    {
        public ReserveCommand(long userId, long placeId, DateTime reserveTime, decimal cost)
        {
            UserId = userId;
            PlaceId = placeId;
            ReserveTime = reserveTime;
            Cost = cost;
        }
        public long UserId { get; set; }
        public long PlaceId { get; set; }
        public DateTime ReserveTime { get; set; }
        public decimal Cost { get; set; }
    }
}
