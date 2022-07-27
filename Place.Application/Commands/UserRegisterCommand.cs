

using Place.Application.Configuration.Commands;

namespace Place.Application.Commands;

public class UserRegisterCommand : CommandBase<CommandResult>
{
    public UserRegisterCommand(string name,string userName, string password)
    {
        Name = name;
        UserName = userName;
        Password = password;
    }
    public string Name { get; private set; }
    public string UserName { get; private set; }
    public string Password { get; private set; }
}