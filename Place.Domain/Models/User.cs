using Place.Domain.DomainEvent;
using Place.Domain.SeedWork;

namespace Place.Domain.Models;


public class User : Entity, IAggregateRoot
{
    public string Name { get; set; }
    public string UserName { get; set; }
    public string Password { get; private set; }
    public bool IsActive { get; set; }
    public DateTime RegisterDate { get; set; }
    public User() { }

    public User(string name, string userName, string password, bool isActive, DateTime registerDate) : this()
    {
        Name = name;
        UserName = userName;
        Password = password;
        IsActive = isActive;
        RegisterDate = registerDate;

        // Add the ProductCreatedDomainEvent to the domain events collection 
        // to be raised/dispatched when comitting changes into the Database [ After DbContext.SaveChanges() ]
        CreateUserDomainEvent(name, userName, isActive, registerDate);
    }



    // Add the ProductCreatedDomainEvent to the domain events collection 
    // to be raised/dispatched when comitting changes into the Database [ After DbContext.SaveChanges() ]
    private void CreateUserDomainEvent(string name, string userName, bool isActive, DateTime registerDate)
    {
        var orderStartedDomainEvent = new UserCreatedDomainEvent(this, name, userName, isActive, registerDate);

        this.AddDomainEvent(orderStartedDomainEvent);
    }

}
