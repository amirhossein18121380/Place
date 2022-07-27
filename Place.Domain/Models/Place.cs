using Place.Domain.SeedWork;

namespace Place.Domain.Models;

public class Place : Entity, IAggregateRoot
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string Address { get; set; }
    public short PlaceType { get; set; }
    public string Location { get; set; }
    public DateTime Date { get; set; }
    public long UserId { get; set; }

    public Place() { }

    public Place(string title, string address, short placeType, string location, DateTime date,long userId) : this()
    {
        Title = title;
        Address = address;
        PlaceType = placeType;
        Location = location;
        Date = date;
        UserId = userId;
    }
}