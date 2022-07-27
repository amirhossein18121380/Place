using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Place.Application.Configuration.Commands;

namespace Place.Application.Commands;
public class UserLoginCommand : CommandBase<CommandResult>
{
    public UserLoginCommand(string userName, string password)
    {
        UserName = userName;
        Password = password;
    }
    public string UserName { get; private set; }
    public string Password { get; private set; }
}