using System.ComponentModel.DataAnnotations.Schema;
using Place.Domain.SeedWork;

namespace Place.Domain.Models;

[Table("Reservation")]
public class Reservation : Entity, IAggregateRoot
{
    public long UserId { get; set; }
    public long PlaceId { get; set; }
    public DateTime ReserveTime { get; set; }
    public DateTime CreateOn { get; set; }
    public decimal Cost { get; set; }

    public Reservation() { }

    public Reservation(long userId, long placeId, DateTime reserveTime, DateTime createOn, decimal cost) : this()
    {
        UserId = userId;
        PlaceId = placeId;
        ReserveTime = reserveTime;
        CreateOn = createOn;
        Cost = cost;
    }
}