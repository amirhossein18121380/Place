using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Place.Application.Configuration.Commands;
using Place.Application.Enums;

namespace Place.Application.Commands.Place
{
    public class AddPlaceCommand : CommandBase<CommandResult>
    {
        public string Title { get; set; }
        public string Address { get; set; }
        public short PlaceType { get; set; }
        public string Location { get; set; }
        public long UserId { get; set; }

        public AddPlaceCommand(string title, string address, short placeType, string location, long userId) 
        {
            Title = title;
            Address = address;
            PlaceType = placeType;
            Location = location;
            UserId = userId;
        }
    }
}
