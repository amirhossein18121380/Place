


using Place.Application.Enums;

namespace Place.Application.Configuration.Commands
{
    public class CommandResult
    {
        public CommandResult()
        {

        }
        public CommandResult(AppEnum.CommandResultStatus status)
        {
            Status = status;
        }

        public CommandResult(AppEnum.CommandResultStatus status, string message)
        {
            Status = status;
            Message = message;
            Data = message;
        }
        public CommandResult(AppEnum.CommandResultStatus status, object data)
        {
            Status = status;
            Data = data;
            if (data is string)
                Message = data.ToString();
        }

        public static CommandResult Ok => new CommandResult(AppEnum.CommandResultStatus.Success);
        public AppEnum.CommandResultStatus Status { get; set; }
        public int StatusCode => (int)Status;
        public string Message { get; set; }
        public object Data { get; set; }
        public object ApiResult => new { Message = Message, Data = Data, Status = Status };
    }
}
