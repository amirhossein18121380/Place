
using MediatR;

namespace Place.Domain.DomainEvent;

/// <summary>
/// Event used when an Product is created
/// </summary>
public class UserCreatedDomainEvent : INotification
{
    public string Name { get; set; }
    public string UserName { get; set; }
    public bool IsActive { get; set; }
    public DateTime RegisterDate { get; set; }
    public Models.User Product { get; }

    public UserCreatedDomainEvent(Models.User user,string name, string userName, bool isActive, DateTime registerDate)
    {
        Name = name;
        UserName = userName;
        IsActive = isActive;
        RegisterDate = registerDate;
    }
}