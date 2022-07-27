


using Place.Application.Enums;

namespace Place.Application.Configuration.Queries
{
    public class QueryResult
    {
        public int Count { get; set; }
        public AppEnum.QueryResultStatus Status { get; set; }
        public int StatusCode => (int)Status;
        public string Message { get; set; }
        public object Data { get; set; }
        //public object DataAndCount => new { Data = this.Data, Count = this.Count };

        public QueryResult()
        {

        }

        public QueryResult(object data)
        {
            if (data == null)
            {
                Status = AppEnum.QueryResultStatus.NotFound;
                Data = null;
                Message = "اطلاعاتی برای ارائه وجود ندارد";
            }
            else
            {
                Status = AppEnum.QueryResultStatus.Success;
                Data = data;
                Message = string.Empty;
            }
        }

        public QueryResult(AppEnum.QueryResultStatus status)
        {
            if (status == AppEnum.QueryResultStatus.NotFound)
            {
                Status = AppEnum.QueryResultStatus.NotFound;
                Message = "There is not any information";
            }
            else
            {
                Status = AppEnum.QueryResultStatus.Success;
            }
        }


        public QueryResult(object data, int count)
        {

            if (count == 0)
            {
                Status = AppEnum.QueryResultStatus.Success;
                Data = new List<object>();
                Message = "اطلاعاتی برای ارائه وجود ندارد";
            }
            else if (count == -1)
            {
                Status = AppEnum.QueryResultStatus.Success;
                Data = data;
                Message = string.Empty;
                Count = 0;
            }
            else
            {
                Status = AppEnum.QueryResultStatus.Success;
                Data = data;
                Message = string.Empty;
                Count = count;
            }
        }


        public QueryResult(AppEnum.QueryResultStatus status, bool isList = false)
        {
            switch (status)
            {
                case AppEnum.QueryResultStatus.Pending:
                    break;
                case AppEnum.QueryResultStatus.Success:
                    break;
                case AppEnum.QueryResultStatus.Fail:
                    break;
                case AppEnum.QueryResultStatus.NotFound:
                    if (isList)
                    {
                        Data = new List<object>();
                        Status = AppEnum.QueryResultStatus.Success;
                    }
                    else
                    {
                        Data = null;
                        Status = AppEnum.QueryResultStatus.NotFound;
                    }
                    Message = "اطلاعاتی برای ارائه وجود ندارد";
                    break;
                default:
                    break;
            }
        }
    }
}
