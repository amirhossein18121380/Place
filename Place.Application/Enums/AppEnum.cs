using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Place.Application.Enums;
public class AppEnum
{
    public enum CommandResultStatus
    {
        Success = 200,
        Pending = 202,
        Fail = 400
    }
    public enum QueryResultStatus
    {
        Pending = 102,
        Success = 200,
        Fail = 400,
        NotFound = 404
    }
}
